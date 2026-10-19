---
date: 2026-09-30
type: methodology
tags:
  - pdr
  - obligatorio-1
  - defensa
  - estudio
ai-first: true
confidence: high
fuente: "Codigo de main 0756597 del repo [repo-del-grupo], letra en Sources/OBLIGATORIO/Obligatorio 1 Agosto 2026.pdf.md y actas de Clases/, repasados con Lucas el 28 al 30-09"
relations:
  relates_to:
    - "[[Estado del Obligatorio 1 - 2026-09-30]]"
    - "[[Obligatorio 1 - Sistema de reservas de vehiculos electricos]]"
---

# Estudio para la defensa del Obligatorio 1

## For future agent
Lo que Lucas ya repaso del codigo de EDISON para la defensa del **01-10-2026**, en el orden en que lo fue entendiendo, y como le sirve que se lo expliquen. Sirve para seguir el estudio en otra sesion sin repetir. Vale para `main` `0756597`.

## Como explicarle (preferencias observadas)
- Sabe poco de redes: viene de DA1 y DA2, donde la interfaz llamaba directo a la logica. Lo nuevo para el es cliente-servidor, sockets y muchos hilos.
- Respuestas **muy breves**, en castellano simple, "a lo bruto". Analogias le funcionan: **restaurante** (menus = carta, ServerConnection = telefono, socket = linea, Common = idioma de las comandas, Program del server = dueno que abre el local, Networking = mozo de la mesa, BusinessLogic = jefe de cocina, DataAccess = despensa, Domain = productos) y **carta y correo** (Frame = carta, FrameHandler = sobre, NetworkHelper = correo).
- Le sirve ver: el archivo y la linea exacta, ejemplos concretos con los datos de prueba, y la herramienta espia (ver [[2026-09-28 - Obligatorio 1 EDISON]]).
- Corregirlo directo cuando entiende mal; tiende a confusiones previsibles (ver abajo).

## Ya entendido
- **Proyectos**: `Edison.Client`, `Edison.Common`, `Edison.Server`. Client y Server no se referencian; los dos dependen solo de Common. Se encuentran al ejecutarse, por el socket TCP (IP y puerto del `App.config`).
- **Capas del servidor**: Networking (recibe, traduce, redirige, contesta) -> BusinessLogic (decide las reglas) -> DataAccess (guarda) ; las tres usan Domain. Nunca al reves. BusinessLogic es la unica capa que no usa Common.
- **Cliente**: `Program` (menu inicial, login, errores), `Menus/` (solo pantallas, validan formato), `VehicleClient`/`ReservationClient`/`ActivityClient` (arman el pedido y hablan con el server), `ServerConnection` (el socket; no sabe que usuario esta logueado), `ConsoleInput`. El cliente tiene un solo hilo.
- **Hilos**: al arrancar el server hay 3 (principal que lee `salir`, `Accept`, `ReservationProcessor`). Por cada cliente que se conecta, el hilo de `Accept` crea uno nuevo (`Program.cs:70` Accept, `:78` new Thread, `:80` `ClientHandler.Run`). Terminan cuando el cliente se va. `Program` interviene solo al conectar; despues los mensajes van directo al `ClientHandler.Run` de ese cliente.
- **ClientHandler**: `Run` (bucle de la conexion, llama a `Handle` por cada pedido; imagenes van a `_images.Handle`) y `Handle` (resuelve login, registro, logout; delega el resto). Campos: socket, servicio de usuarios, 4 handlers, `_username`.
- **Protocolo**: trama = HEADER (`REQ`/`RES`, request/response, definidos por el grupo en `Protocol.cs` a partir de la sugerencia de la letra) + CMD (2 digitos, enum `Command`) + LARGO (4 digitos, bytes de DATOS) + DATOS. DATOS = campos con largo de 4 digitos en caracteres (`0008vendedor0009edison123`). Respuesta empieza con `1` o `0`.
- **Framing**: TCP es un chorro sin cortes y corta donde quiere; el LARGO dice donde termina cada mensaje. `FrameHandler.Receive` lee 9 bytes fijos y despues exactamente LARGO bytes. Pregunta muy probable.
- **Clases de Common**: `Protocol` (constantes), `Command` (acciones), `Frame` (un mensaje; no manda nada), `DataBuilder`/`DataReader` (arman y leen los DATOS, sobrecargas por tipo), `FrameHandler` (manda y recibe Frames), `NetworkHelper` (manda y recibe bytes con bucle hasta completar; `NetworkHelper.cs:44` es donde entran los bytes), `ImageRules`, `ValidationRules` (usadas por los dos lados), `SettingsManager`.
- **Pedido y respuesta**: el servidor nunca inicia; por eso no avisa cuando se confirma una reserva ni cuando cierra.
- **Reservas**: toda reserva nace Pendiente; "vence el plazo" = pasaron `ReservationDelaySeconds` desde `CreatedAt` (`Reservation.IsDueForProcessing`); el hilo `ReservationProcessor` la confirma o rechaza. Nadie la acepta a mano. Pendiente -> Rechazada ocurre si hay mas pendientes que unidades (la pendiente no descuenta stock). Confirmada queda asi hasta cancelar; no hay estado final "entregada" porque la letra no lo define. No se cancela una Rechazada ni una Cancelada. Dos fechas: creacion y ultima actualizacion (CR9).
- **Patron State**, `InventoryLock` (candado global), fecha de entrega prometida (dato informativo del modelo, CR2 y filtro CR7), imagenes (por que son complicadas: fuera de la trama, dos fases, cortes, lock, GUID).
- **Persistencia**: en memoria por decision pedagogica del docente (clase del 20-08); archivo `data/edison-data.txt` al cerrar con `salir`. Sin base de datos, sin Docker.
- **Configuracion**: `127.0.0.1` es el default para una PC; entre PC, `0.0.0.0` en el server y la IP real en el cliente, sin recompilar. Es el requerimiento de la letra, no un parche.

## Confusiones que tuvo (repasar)
- Creer que despues de arrancar el server no se crean mas hilos, o que los crea el cliente.
- Creer que `Run` llama al menu o que el menu llama a BusinessLogic.
- Creer que `Program` recibe cada mensaje.
- "Common solo se relaciona con Networking" (tambien Domain, DataAccess y Program).

## Todavia no repasado
- `ReservationRequestHandler`, `ImageRequestHandler` y `ActivityRequestHandler` linea por linea.
- `VehicleService`, `ReservationService` y `UserService` en detalle; `SessionService`.
- `StateStore` (formato del archivo) y el cierre ordenado de `Program.cs` linea por linea.
- Manejo de errores en tres niveles (punto 7.2 de la documentacion).
- Preguntas tipo defensa sobre teoria: proceso vs hilo, foreground/background, `lock` vs `Mutex`/`Semaphore`, deadlock.
