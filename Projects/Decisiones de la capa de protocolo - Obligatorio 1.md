---
date: 2026-09-21
type: decision-log
tags:
  - pdr
  - obligatorio-1
  - protocolo
ai-first: true
confidence: verified
---

# Decisiones de la capa de protocolo - Obligatorio 1

## For future agent
Razonamiento de los cambios de la rama `fix/protocol-review`, rescatado de una nota de trabajo del **15-09-2026** que se borro el 21-09. Sirve directo para el punto 6 de la documentacion que pide la letra, diseno del protocolo de comunicacion, y para el punto 4, decisiones tomadas. Los tres cambios estan **sin commitear** en el working tree al 21-09. Ver [[Estado del Obligatorio 1 - 2026-09-21]].

**Actualizacion as of 2026-09-27 (verificado via git):** los tres cambios ya estan mergeados a `main`. Entraron por el PR #24 (rama `fix/protocol-review`), merge `a1fdbf3` del 2026-09-22. `main` esta ahora en `8e80066` (PR #29, 2026-09-27). La frase "sin commitear... al 21-09" de arriba queda como registro historico de ese momento, ya superada.

## Lo que ya estaba bien y no se toco
- `Frame.ToBytes()` calcula el campo LARGO en **bytes**, no en caracteres. Es el error que el docente demostro en vivo el 08-09.
- `NetworkHelper` acumula en loop tanto en `Send` como en `Receive`.
- `FrameHandler` lee primero los 9 bytes fijos y recien despues los datos.
- Todo con `InvariantCulture` y relleno de ceros a la izquierda, que es el alineado a la derecha con relleno 0 que pide la letra.
- Serializacion a mano con largo por campo. Sin JSON, sin `BinaryFormatter`.

## Decision 1: listas por paginado, no por N+1 tramas

**El problema.** El campo LARGO tiene 4 digitos, asi que una trama no puede llevar mas de 9999 bytes de datos. Un `VehicleDto` en el peor caso ocupa unos 750 caracteres, casi todo por la descripcion de 500. `SearchVehicles` tiraba `ProtocolException` alrededor de los 13 vehiculos, y lo mismo iba a pasar con `GetReservations` y `GetActivity`. Era un problema real de cara a la defensa, porque el plan de testing carga varios modelos.

**Las dos opciones que se evaluaron.**

| | N+1 tramas | Paginado con offset |
|---|---|---|
| Como | una trama con la cantidad, despues una por item | el cliente pide desde un offset, el servidor devuelve una pagina y el indice siguiente |
| A favor | la lista se arma entera adentro del lock, asi que es un snapshot consistente | mantiene **una respuesta por pedido**; cada respuesta se cierra sola |
| En contra | si el cliente lee mal la cantidad, **el stream queda desincronizado** y arrastra todo lo que venga despues | recalcula el filtro en cada pagina, asi que un item puede repetirse o saltarse si otro cliente modifica el catalogo mientras se lista |

**Lo que se eligio: paginado.** Razon principal: el docente insistio el 10-09 en que un byte de corrimiento "desincroniza todo el resto del streaming". El paginado no puede desincronizar el stream porque respeta la simetria de una respuesta por pedido. Ademas ya estaba implementado y mergeado en `Develop` por el PR #19, con la parte dificil bien hecha: `VehicleRequestHandler` mide los bytes de cada item antes de meterlo en la pagina.

El 21-09 se **borraron** `FrameHandler.SendItems` y `FrameHandler.ReceiveItems`, que eran la implementacion de N+1 tramas y no tenian ningun llamador.

**Limitacion conocida, va en el punto 2 de la documentacion:** el paginado puede repetir o saltar un item si el catalogo cambia entre dos paginas.

**Pendiente:** llevar el mismo patron a `GetReservations` y `GetActivity`. Conviene sacar el paginado a un helper en `Common` en vez de copiarlo tres veces.

## Decision 2: `ValidationRules` como unica fuente de los largos

**El problema.** Los largos estaban duplicados: en `ValidationRules` de `Edison.Common`, que usa el cliente, y en `User.cs` y `VehicleModel.cs` de `Domain`, que usa el servidor. Los valores coincidian, pero el dia que alguien cambiara uno solo, el cliente validaria distinto que el servidor y el usuario veria un rechazo sin explicacion.

**Lo que se hizo.** `ValidationRules` quedo como unica fuente. Se eliminaron las 4 constantes propias de `User.cs` y las 5 de `VehicleModel.cs`; `Reservation.cs` y `ActivityLog.cs` dejaron de referenciar `User.*` y `VehicleModel.*`; y `MaxActivityDetailLength = 200`, que vivia suelta en `ActivityLog`, paso a `ValidationRules`. **Los valores no cambiaron**, es solo unificacion.

**Consecuencia de arquitectura a documentar:** `Edison.Server/Domain/Entities/` ahora depende de `Edison.Common`. Es una decision deliberada: las reglas de validacion son un contrato compartido entre las dos puntas, no logica privada del servidor.

## Decision 3: `Frame.Error` trunca el mensaje

**El problema.** Si el mensaje de error superaba los 9999 bytes, `ToBytes()` lanzaba `ProtocolException` justo mientras se estaba informando otra falla: un error dentro del manejo de un error. Choca con el requerimiento en comun de la letra, que pide notificar al cliente y seguir funcionando.

**Lo que se hizo.** `Frame.Error` trunca a `Protocol.MaxErrorMessageLength`, que es `MaxDataLength - 1` porque el primer caracter del payload es el status. El truncado **cuenta bytes, no caracteres**.

El 21-09 se reescribio el truncado, que era cuadratico: por cada caracter que sacaba volvia a contar los bytes de todo el string, y al cortar de a un `char` podia **partir un par surrogate** y dejar medio caracter, que UTF-8 convierte en `U+FFFD`. Ahora corta en una sola pasada con presupuesto de bytes y respeta los pares surrogate. Verificado con 50.000 caracteres ASCII, 20.000 enies, 20.000 emojis y mezclado: todos entran en 9998 bytes sin partir ningun caracter.

## Pendiente que la nota original dejaba abierto
Los `App.config` ya existen, uno en `Edison.Server` y otro en `Edison.Client`, agregados por los PR #18 y #19. Lo que **sigue faltando** es la clave de los segundos de procesamiento de reservas de CR5, que el docente marco como obligatoria el 03-09 y el 08-09.

## Connections
- [[Estado del Obligatorio 1 - 2026-09-21]] - el informe de estado completo
- [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]] - la consigna contra el PDF oficial
- [[Como escribir sockets al estilo del curso PDR]]
- [[Errores caros en Programacion de Redes]]
- [[Clase 2026-09-08 - Protocolo propietario, framing y Receive parcial]]
- [[Clase 2026-09-10 - Maquina de estados del protocolo, Common y app.config]]
