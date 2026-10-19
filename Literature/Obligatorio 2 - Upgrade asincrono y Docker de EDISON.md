---
date: 2026-10-13
type: literature
tags:
  - pdr
  - evaluacion
  - obligatorio
  - obligatorio-2
  - tema-3
  - docker
ai-first: true
confidence: stated
status: en curso
entrega: 2026-10-29
puntaje_max: 15
puntaje_min: 5
sistema: EDISON
github_org: ArqSis-PDR-2026-2
fuente: "Letra oficial en PDF: `Sources/OBLIGATORIO/Obligatorio 2 Agosto 2026.pdf.md` (original en `PDR/PDR/OBLIGATORIO/CONSIGNA/Obligatorio 2 Agosto 2026.pdf`, recibido el 2026-10-13). Aclaraciones orales de la clase del 08-10-2026 (Vimeo, showcase 12367503)"
---

# Obligatorio 2 - Upgrade asincrono y Docker de EDISON

## For future agent
Consigna del **Obligatorio 2** de Programacion de Redes 2026: es una **actualizacion del sistema EDISON** del [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]]. Hay que pasarlo a `async`/`await`, dockerizar cliente y servidor, agregar una base de datos en Docker y sumar la cancelacion de reservas pendientes con comprobante. **Es la referencia operativa hasta el 2026-10-29 a las 21:00.**

Convencion, igual que en la nota del Obligatorio 1:
- **(letra)**: textual del PDF, manda siempre.
- **(clase 08-10)**: aclaracion oral del docente en [[Clase 2026-10-08 - Letra del Obligatorio 2 y patron RPC sobre RabbitMQ]].
- **(inferencia)**: deduccion propia, sin respaldo directo.

## Condiciones
| Que | Valor | Fuente |
|---|---|---|
| Entrega | **2026-10-29, hasta las 21:00**, por `gestion.ort.edu.uy` | letra |
| Puntaje | **minimo 5, maximo 15** | letra |
| Archivo | max 40 MB. Ver la contradiccion de formato mas abajo | letra |
| Grupo | hasta 3 personas **del mismo dictado** | letra |
| Repositorio | **el mismo de la entrega anterior**, en la organizacion `ArqSis-PDR-2026-2` | letra |
| Release | crear un release al entregar; `main` no se toca despues del hito | letra |
| Documento | PDF con **el link al repo en la primera pagina** | letra |
| Defensa | **se evalua junto con la tercera entrega**; "Defensas: a coordinar" | letra + clase 08-10 |
| IA | citar herramienta y contexto de uso; revisar todo lo generado | letra |

## Objetivos (letra)
- Manejo del patron asincronico con `async` / `await`.
- Docker.

## Requerimientos
**1. Migrar a asincronico (letra).** Toda la estructura basada en `Thread` pasa al modelo asincrono de .NET: **sustituir todos los threads por `Task`** y modelar todo con `async`/`await`.
- Puntos a migrar en la copia local del repo (as of 2026-10-13, inferencia a partir del codigo):
  - `Edison.Server/Program.cs:64`: el hilo de `Accept`.
  - `Edison.Server/Program.cs:78`: un `Thread` por cliente.
  - `Edison.Server/Program.cs:102` y `:115`: los `Join`.
  - `Edison.Server/BusinessLogic/ReservationProcessor.cs:16`: el hilo que cada 200 ms llama a `ProcessDueReservations`.
- Clase 08-10: hacer primero la conversion, que la IA *"en general lo hace bastante bien"*, y despues revisar si conviene repensar alguna logica.
- Clase 22-09: no hacer todo `async` por reflejo. Va en entrada/salida y en tareas pesadas, no en cuentas cortas.

**2. Dockerizar cliente y servidor (letra).**
- **Cambiar los archivos de configuracion por una herramienta mas adecuada para contenedores.** Hoy se usa `app.config` con `ConfigurationManager` en `Edison.Common/SettingsManager.cs:15`. La opcion obvia son **variables de entorno**, pero la letra no la nombra (inferencia).
- **Se puede ignorar el cierre del servidor por consola.**
- **Las imagenes de los modelos del servidor tienen que poder verse desde la maquina host**, y lo mismo **los archivos del requerimiento 5**. Hay que **documentar como**. Presumiblemente con un volumen o bind mount (inferencia).
- **No usar multi-stage builds.** En clase 08-10: hacer el release para Linux y copiarlo con `COPY` dentro de la imagen. El multi-stage queda para el proximo obligatorio.
  - **Trampa:** los tres Dockerfile del ejemplo de la catedra, en `04_Async_04_Docker`, **son multi-stage**. No copiarlos tal cual: hay que reescribirlos en una sola etapa. Ver [[Ejemplos de codigo de asincronia de clase]].
  - El servidor async del ejemplo, `04_Async_03_00Server`, sirve de base, pero **hace un unico `ReadAsync` sin loop**: es el mismo bug del `Receive` parcial. EDISON ya lo resuelve en `NetworkHelper`, asi que conviene migrar ese helper en vez de copiar el ejemplo.
- Mejora pedida en clase 08-10: terminar las aplicaciones con un `ReadLine` o `ReadKey`. En un contenedor, si la app sale, no se lee la consola; asi se puede hacer `docker attach` y ver el mensaje.

**3. Base de datos (letra).** MySQL o la que se prefiera, **corriendo en Docker**. Se migra a la base el guardado de datos, **excepto las imagenes**.
- Clase 08-10: recomienda **Oracle** porque ya se conoce de Base de Datos 2 y hay ejemplos de C# con Oracle.
- Clase 08-10: alcanza con que **al menos alguna entidad** viva en la base.
- Hoy la persistencia es `StateStore`, en `Edison.Server/DataAccess/StateStore.cs`, segun la copia local.

**4. Mejoras a la entrega anterior (letra).** Las que senalen los docentes, o lo que haya faltado del Obligatorio 1. La devolucion de las defensas se anuncio para la clase siguiente al 08-10, y **no esta registrada** (as of 2026-10-13).

**5. Cancelacion y comprobante (letra).**
- Cancelar una reserva **pendiente** tiene que **cancelar el mecanismo que la haria rechazarse**. En clase 08-10 dijo *"rechazarse o aceptarla"*.
- Las reservas **aprobadas generan un archivo como comprobante**.
- Hay que **aprovechar la cancelacion de tareas de `Task`**, es decir `CancellationToken`.

Detalle oral (clase 08-10):
- El periodo de aprobacion tiene que ser **bastante mas largo** que unos segundos.
- El cliente manda la solicitud, despues puede mandar **un mensaje de cancelacion**, y eso cancela la `Task` del servidor.
- El servidor guarda en memoria **que cliente pidio que**.
- El `sleep` pasa a ser **un loop con delays que evalua el token**.
- Hay que deshacer lo necesario y **avisar al cliente** de la cancelacion.
- **Opcional sugerido:** que el cliente pueda **descargar el comprobante**, como una imagen.

**Choque de diseno con el codigo actual (inferencia).** Hoy no hay una tarea por reserva: `ReservationProcessor` hace un **sondeo cada 200 ms** sobre todas las reservas vencidas. El requerimiento pide cancelar **"el mecanismo"** usando la cancelacion de `Task`. La lectura que coincide con lo dicho en clase es **una `Task` por reserva pendiente**, con su propio `CancellationTokenSource` guardado por reserva. Esa tarea hace un `Task.Delay(plazo, token)` o un loop con delays, y la cancelacion del cliente llama a `Cancel()`. Si el servidor tambien se apaga, conviene un token **enlazado** al del servidor: ver [[Clase 2026-09-22 - Parallel, CancellationToken y cuando usar async]]. Es una decision abierta del grupo.

## Requerimientos funcionales (letra)
- **Servidor:** los mismos del obligatorio anterior **mas el requerimiento 5**.
- **Cliente:** los mismos del obligatorio anterior. En clase 08-10 aclaro que hay que **agregar la solicitud de cancelacion**.

## Protocolo (letra)
Se puede seguir con el anterior o cambiarlo. Hacen falta al menos un comando nuevo de cancelacion y quiza uno para descargar el comprobante (inferencia).

## Documentacion (letra)
- **Que se modifico en el codigo, por que, y que beneficio trae** sobre el codigo anterior.
- Hay que entregar **los Dockerfile de cliente y servidor y una guia para ejecutar con Docker**. Opcional: `docker-compose.yml`.
- Clase 08-10: el docente **recomienda Compose**, porque sin el, un nombre de contenedor distinto rompe la conexion del cliente. Tambien sugiere **comparar codigo viejo y nuevo** y **dejar el codigo viejo en un branch congelado**.
- La documentacion del Obligatorio 1 sirve de base (clase 08-10). Formato y voz: [[Decision - formato y voz de la documentacion del Obligatorio 1]].

## Evaluacion: prohibiciones (letra)
- **Prohibido usar la clase `Thread`.** Es lo contrario del Obligatorio 1.
- Prohibido `BinaryFormatter`. Se esperan **streams**.
- **Prohibidos los parsers existentes para serializar objetos, por ejemplo JSON.**

## Contradicciones y huecos de la letra
- **Defensa.** La cabecera repite *"La defensa es obligatoria y eliminatoria"*, pero la seccion DEFENSA dice que **se evalua junto con la tercera entrega**, e "Informacion importante" dice *"Defensas: a coordinar"*. El docente lo confirmo en clase: no hay defensa propia del 2. Resuelve el item 15 de [[Huecos y contradicciones del material PDR]] para el Obligatorio 2.
- **Formato del archivo.** Las condiciones dicen *"zip, rar o pdf"*. ENTREGA dice *"un archivo PDF"*. El RECORDATORIO dice *"un unico archivo en formato zip o rar"* con los PDF adentro. Es la misma contradiccion que el item 21 de la letra del Obligatorio 1, y la mitigacion es la misma: **zip con el PDF adentro**.
- **Cancelacion.** La letra dice *"el mecanismo que la haria rechazarse"*; en clase dijo *"rechazarse o aceptarla"*. En la practica es el mismo mecanismo: el temporizador que decide.

## Connections
- [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]] - el sistema que se actualiza
- [[Clase 2026-10-08 - Letra del Obligatorio 2 y patron RPC sobre RabbitMQ]] - lectura comentada de la letra
- [[Clase 2026-09-22 - Parallel, CancellationToken y cuando usar async]] · [[Clase 2026-09-17 - Task en detalle, continuations, ThreadPool y TPL]] - la teoria de `Task` y de la cancelacion
- [[Ejemplos de codigo de asincronia de clase]] - los ejemplos de la catedra: el cliente/servidor async y el Dockerfile
- [[Tema 3 - Asincronia y paralelismo]] · [[Complementario - Docker y Cloud]] · [[Concurrencia de Thread a Task]]
- [[Reglas y prohibiciones del curso PDR 2026]] · [[Programacion de Redes 2026]]
