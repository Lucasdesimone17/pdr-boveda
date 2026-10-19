---
date: 2026-09-22
type: status-report
tags:
  - pdr
  - obligatorio-1
  - estado
ai-first: true
confidence: verified
commit: a1fdbf3
fuente: "Re-auditoria de la rama Develop en a1fdbf3, con 11 agentes, contra la letra oficial y las 9 actas de clase"
relations:
  superseded_by:
    - "[[Estado del Obligatorio 1 - 2026-09-27]]"
---

# Estado del Obligatorio 1 - EDISON (22-09)

## For future agent
> **Reemplazado el 2026-09-24 por [[Estado del Obligatorio 1 - 2026-09-24]].** El PR #25 cerro el lock global sobre lecturas, el codigo muerto, el `.gitignore` de los compilados y los dos opcionales, y la documentacion quedo escrita. Sigue abierto lo de `Take` fijo en `GetReservations` y `GetActivity`, y los compilados todavia no estan agregados al repositorio. A su vez, esa nota fue reemplazada el 2026-09-27 por **[[Estado del Obligatorio 1 - 2026-09-27]]**, que es el estado vigente.

Re-auditoria del **2026-09-22**, a seis dias de la entrega. **Reemplaza a [[Estado del Obligatorio 1 - 2026-09-21]]**, que quedo obsoleto en su conclusion principal: ese informe decia que reservas, imagenes e historial estaban escritos pero sin cablear, y los PR #21 a #24 los cablearon.

Metodo: cinco auditores sobre una copia de solo lectura de `Develop`, cada uno con un verificador adversarial que ademas recalifico los estados siguiendo la cadena entera desde el menu del cliente hasta el repositorio. Once agentes, ninguno refuto ningun hallazgo ni corrigio ningun estado, lo que en esta ronda significa que los auditores fueron conservadores y no que nadie chequeo.

**Salvedad de alcance:** esta ronda cubrio lo que tocaron los PR #21 a #24. **CR1, CR3, CR4, CR7 y SR1 no se volvieron a evaluar**; su estado vigente es el de la ronda del 21-09, donde CR1, CR3, CR4 y CR7 estaban completos.

**Salvedad de commit:** la copia auditada es `a1fdbf3`, la punta de `Develop`. El workflow quedo etiquetado como `d679608` porque se lanzo con ese numero, pero el snapshot se tomo despues de que entrara el PR #24, asi que **incluye los tres commits de `fix/protocol-review`**.

## Veredicto

Cambio real y grande. Los tres flujos que faltaban estan cableados de punta a punta, con evidencia de linea en cada salto: **CR2, CR5, CR6, CR8, CR9, CR10 y SR3 completos**. De los 22 requisitos re-verificados, 19 completos, 2 parciales y 1 no implementado: **85 a 90% de cobertura sobre lo evaluado**.

Lo que queda **mal hecho** son dos cosas de codigo: dos handlers que paginan con un numero fijo en vez de medir bytes, y un lock global que sigue bloqueando las lecturas. Lo que **no esta** es un entregable, no codigo: los compilados en Release dentro del repositorio.

## Cobertura

| ID | Requerimiento | Estado | Evidencia o nota |
|---|---|---|---|
| CR1 | Autenticacion | Completo | Verificado el 21-09, fuera del alcance de esta ronda |
| CR2 | Alta de modelo con imagen | **Completo** | `VehicleMenu.cs:107` a `ImageRequestHandler.cs:33-71` |
| CR3 | Modificacion de modelo | Completo | Verificado el 21-09 |
| CR4 | Baja de modelo | Completo | Verificado el 21-09 |
| CR5 | Solicitud de reserva | **Completo** | `ReservationProcessor` arrancado en `Program.cs:50`, tiempo desde `App.config` |
| CR6 | Cancelacion de reserva | **Completo** | `ReservationService.Cancel` valida dueno y estado, y libera la unidad |
| CR7 | Busqueda y filtrado | Completo | Verificado el 21-09, cinco criterios |
| CR8 | Consultar vehiculo y bajar imagen | **Completo** | `VehicleDetailMenu` a `ImageRequestHandler.cs:72-88` |
| CR9 | Estado de reservas | **Completo** | `ReservationRequestHandler.cs:43-52` |
| CR10 | Historial de actividades | **Completo** | `ActivityRequestHandler.cs:17-38`, `Record()` en 8 puntos |
| SR1 | Funcionalidad completa | Completo por arrastre | No re-evaluado; ya no arrastra CR5, CR6, CR9 ni CR10 |
| SR2 | Conexiones multiples | **Parcial** | Un hilo por cliente OK; el lock global sigue envolviendo lecturas |
| SR3 | Borrado de imagenes | **Completo** | `VehicleService.cs:95-115` |
| SR4 | Cierre controlado | **Completo** | `Program.cs:85-107` mas `ReservationProcessor.Dispose` |
| Comun | Configuracion externa | **Completo** | IP, puerto y `ReservationDelaySeconds` en los dos `App.config` |
| Comun | Desconexiones | Completo | `ClientHandler` atrapa y libera |
| Comun | Manejo de errores | Completo | `Frame.Error` y el servidor sigue atendiendo |
| Protocolo | Trama, largo en bytes, relleno, loop por offset | **Completo** | `Protocol.cs:11-23`, `Frame.cs:92-113`, `NetworkHelper.cs:19-53` |
| Protocolo | Maquina de estados de sesion | **Completo** | Los tres handlers nuevos exigen sesion iniciada |
| Protocolo | Listas dentro del limite de 9999 bytes | **Parcial** | `SearchVehicles` si acota; `GetReservations` y `GetActivity` no |
| Prohibiciones | `Task`, `async`, `await`, `BinaryFormatter`, JSON, GUI | **Completo** | Sin apariciones en todo el arbol |
| Entrega | Compilados Release en el repositorio | **No implementado** | `.gitignore:8` los excluye sin excepcion |

## Lo que se resolvio desde el 21-09

- **El proceso de aprobacion ya corre.** `Program.cs:50` arranca `ReservationProcessor`, que llama a `ProcessDueReservations()` cada 200 ms (`ReservationProcessor.cs:13-27`). Al procesarse, una reserva sin unidades se rechaza, como exige CR5.
- **Aparecio la clave de configuracion.** `ReservationDelaySeconds=5` en `Edison.Server/App.config:7`, leida y validada en `Program.cs:42-45`. Era obligatoria segun las clases del 03-09 y 08-09.
- **El repositorio de reservas es una sola instancia.** `Program.cs:34` la crea una vez y la comparte entre `VehicleService` y `ReservationService`. El riesgo de estado partido en dos quedo cerrado.
- **`FileTransferHelper` reescrito por el grupo**, con sobrecargas sobre `Stream`, recepcion a un `.part` temporal de nombre unico y `File.Move` al final. Ya borra el parcial ante excepcion, asi que el arreglo propio del 21-09 quedo redundante.
- **Las constantes centralizadas y el truncado de `Frame.Error` entraron por el PR #24.**

## Lo que falta

1. **Critico. Los compilados Release no estan en el repositorio.** `.gitignore:8` ignora `[Rr]elease/` sin ninguna excepcion. La letra lo exige textual en la seccion Compilados y en el punto 6 de la entrega, que pide un Release de GitHub con codigo fuente, compilados y documentacion. Arreglo: excepcionar el patron, por ejemplo `!Edison.Server/bin/Release/**` y `!Edison.Client/bin/Release/**`, o publicar a una carpeta dedicada, y commitear los ejecutables.
2. **Alto. `GetReservations` y `GetActivity` paginan con un numero fijo.** `ReservationRequestHandler.cs:49` usa `Take(20)` y `ActivityRequestHandler.cs:28` usa `Take(10)`, sin medir el tamano codificado. Con descripciones o detalles largos la pagina puede pasar los 9999 bytes de `DATOS` y la trama revienta, tirando la conexion entera. Arreglo: copiar el patron de `VehicleRequestHandler.cs:62-76`, que mide con `Protocol.Encoding.GetByteCount` y recorta antes de superar `MaxDataLength`.
3. **Alto. El lock global sigue envolviendo las lecturas.** `VehicleRequestHandler.cs:25` mete todo el `switch` dentro de `InventoryLock`, incluidos `GetVehicle` y `SearchVehicles`, que son de solo lectura. Choca con SR2, que pide que las acciones de un cliente no afecten el rendimiento de los demas, y con la clase del 03-09, la de la demo medida de 40 s contra 4,3. Arreglo: tomar el lock global solo en las escrituras.
4. **Bajo. Codigo sin llamadores.** `Edison.Common/FileStreamHelper.cs` completo, `Protocol.CalculateParts` (`Protocol.cs:37`), y las sobrecargas por path de `FileTransferHelper` (`FileTransferHelper.cs:13`). La regla del curso es poder explicar cada linea, y en la defensa preguntan justo por esto. Arreglo: borrarlos.
5. **Bajo, y no es codigo. CR10 menciona "compras realizadas"** y no existe ningun `ActivityType` de compra, porque tampoco existe un requerimiento de compra entre CR1 y CR9. Es un hueco de la letra, reconocido en la clase del 27-08. Hay que **declararlo como supuesto** en el punto 3 de la documentacion, interpretandolo como reserva confirmada.

## Plan hasta el 28-09

Quedan seis dias. Los primeros cuatro son alcanzables en uno o dos dias de trabajo real.

- [ ] **Imprescindible.** Compilar Release de los dos proyectos, excepcionar el `.gitignore` y subir los binarios. Requisito explicito de entrega, una hora de trabajo.
- [ ] **Imprescindible.** Acotar `GetReservations` y `GetActivity` por bytes, copiando `SearchVehicles`. Riesgo real de romper la conexion en la defensa con listas largas.
- [ ] **Imprescindible.** Sacar las lecturas del lock global. Pega directo en SR2. Probarlo con dos clientes concurrentes.
- [ ] **Imprescindible.** Escribir la documentacion, con los seis puntos, las decisiones y la citacion de IA. Material listo en [[Decisiones de la capa de protocolo - Obligatorio 1]]. Es lo que mas puntos mueve y lo unico que sigue en cero.
- [ ] **Imprescindible.** Declarar el supuesto de "compras = reserva confirmada" en el punto 3. Quince minutos.
- [ ] **Imprescindible.** Probar en dos maquinas del laboratorio cambiando solo los `App.config`, sin recompilar.
- [ ] Opcional. Borrar `FileStreamHelper`, `Protocol.CalculateParts` y las sobrecargas muertas.
- [ ] Opcional. Datos de prueba precargados.

## Dudas que siguen abiertas para Teams
Las cinco del informe del 21-09 siguen vigentes: formato del archivo de entrega, grupos del mismo dictado, "compras realizadas" de CR10, los roles de vendedor y comprador, y si aceptan el largo de campo interno contado en caracteres. Detalle en [[Estado del Obligatorio 1 - 2026-09-21]].

## Connections
- [[Estado del Obligatorio 1 - 2026-09-21]] - el informe que este reemplaza
- [[Decisiones de la capa de protocolo - Obligatorio 1]] - el porque del protocolo, material para la documentacion
- [[2026-09-21 - Obligatorio 1 EDISON]] - el log de la sesion
- [[Programacion de Redes 2026]] · [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]]
- [[Reglas y prohibiciones del curso PDR 2026]] · [[Errores caros en Programacion de Redes]]
