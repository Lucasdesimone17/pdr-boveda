---
date: 2026-09-27
type: status-report
tags:
  - status-report
  - pdr
  - obligatorio-1
  - auditoria
  - defensa
ai-first: true
confidence: verified
commit: 8e80066
fuente: "Workflow de auditoria wf_7a783608-7d5 del 2026-09-27 (37 agentes: 8 revisores, verificadores adversariales por hallazgo y un critico de completitud), corrido sobre main 8e80066, la carpeta Entrega Obligatorio 1, la letra y las actas de Clases/"
relations:
  depends_on:
    - "[[Estado del Obligatorio 1 - 2026-09-27]]"
  relates_to:
    - "[[2026-09-27 - Obligatorio 1 EDISON]]"
---

# Auditoria de cierre del Obligatorio 1 - EDISON (27-09, noche)

## For future agent
Auditoria completa de todo lo hecho el **2026-09-27** sobre el Obligatorio 1 ([[Obligatorio 1 - Sistema de reservas de vehiculos electricos]]), corrida despues de la sesion de cierre de [[2026-09-27 - Obligatorio 1 EDISON]]. Dice que quedo verificado como correcto, que falta para entregar el 28-09 y que riesgos hay para la defensa del 01-10. Cada hallazgo bloqueante o importante paso por un verificador adversarial; aca va la severidad **despues** de verificar. Vale para `main` en `8e80066`; si `main` cambia, re-verificar.

## Veredicto (as of 2026-09-27)
- **El codigo esta bien.** El fix `c470b57` hace lo que dice, compila sin warnings, sin comentarios, y los binarios commiteados tienen el mismo IL que un build fresco de `main`.
- **La prueba real de los win-x64 commiteados paso**: dos clientes a la vez, flujo completo, tramas basura, cortes a mitad de trama, carrera por la ultima unidad, `salir` con clientes conectados y reinicio con estado. Nada se cayo.
- **Lo que falta es de entrega y logistica**, no de codigo.

## Bloqueantes (confirmados por verificador)
1. **Punto 9 (uso de IA) con `[COMPLETAR]`** en `DOCUMENTACION.md` linea 424, y por lo tanto en el PDF y en el zip de Gestion. La letra exige citar herramientas y contexto de uso. Lo redacta el grupo; despues regenerar PDF y zip (comandos en `Entrega Obligatorio 1\LEEME.txt`; el script solo corre en la PC de Lucas y el zip se rehace a mano).
2. **No existe tag ni GitHub Release.** La letra lo pide y la defensa se prueba bajando el Release (acta [[Clase 2026-09-24 - Middleware orientado a mensajes, RabbitMQ y AMQP]]). `gh` no esta instalado: crearlo por la web, sobre `main` `8e80066`, recien cuando el PDF final este listo.
3. **Subir el zip a Gestion** antes del 28-09 21:00 (sin entrega por Gestion la nota es 0). Antes, **cada integrante confirma que figura Inscripto** en la evaluacion: el paso 4 del RECORDATORIO de la letra (`Sources/OBLIGATORIO/Obligatorio 1 Agosto 2026.pdf.md`) valida la inscripcion al "Crear equipo". Problemas: mail a `adjuntos_ei@ort.edu.uy` antes de las 20:00. Subir temprano: en la clase del 27-08 se aviso que el sistema se satura cerca de la hora.

## Importantes (confirmados)
- **Anotarse en la planilla de defensa de Teams** para el jueves 01-10 (respaldo martes 06-10). Va todo el grupo. Ninguna nota del 27-09 registra que se haya hecho.
- **Ensayar en el laboratorio** (clase del martes 29-09): bajar el Release en dos PC cableadas, en `Edison.Server.dll.config` poner `ServerIpAddress` = `0.0.0.0`, en `Edison.Client.dll.config` la IP del server, aceptar firewall/SmartScreen. Los cuatro `.dll.config` commiteados traen `127.0.0.1:20000` (por diseno; la docu linea 381 ya explica 0.0.0.0 y el firewall).

## Menores con accion recomendada
- **Repo privado:** la API anonima de GitHub da 404; para bajar el Release en el laboratorio alguien tiene que iniciar sesion en GitHub (tener 2FA a mano). Plan B: pendrive con los assets del propio Release.
- **SmartScreen / Control de aplicaciones:** un DLL sin firmar recien compilado fue bloqueado en la PC de Lucas (`0x800711C7`); los `Edison.*.exe` si corrieron. El critico verifico que el zip bajado de Internet corre aun con Smart App Control. Saber usar Propiedades > Desbloquear o "Mas informacion > Ejecutar de todas formas".
- **Cliente parece colgado 21 s** si la IP esta mal o el firewall bloquea: despues sale con mensaje. En la defensa, eso es IP/firewall, no codigo.
- **Punto 2 (Errores conocidos) incompleto:** dice que ningun requerimiento quedo sin implementar y lista 4 limitaciones, pero no los defectos ya conocidos: stock 0 trabado, enum en ingles, mensaje raro al caer el server. Sumarlos en la misma regeneracion del PDF.
- **Errores factuales de la docu contra el codigo** (arreglar en la misma regeneracion):
  - Fila 99 del catalogo (linea 292) dice "Hoy no se emite", pero `ClientHandler.cs:152` responde RES 99 "Esta operacion todavia no esta disponible." si llega REQ 99.
  - 8.2 paso 3 (linea 389) dice que el server "espera a que terminen las conexiones"; `Program.cs:107-115` las corta (Shutdown + Dispose) y despues hace Join. 7.3 lo dice bien.
  - No se documenta que reservar sin unidades da error en el momento (`ReservationService.cs:58-61`) y no crea reserva Rechazada; Rechazada es solo para pendientes que al procesarse no encuentran stock. Agregarlo como supuesto.
  - 7.3 no nombra el lock de `SessionService` ni el del diccionario de conexiones. "Despacho por rango de comando" no es literal: se listan los comandos uno por uno. Subir imagen exige unidades disponibles y no se menciona. No todas las fechas de entrega del seed son distintas.
  - Largo de campo: `DataBuilder.cs:22` cuenta **caracteres**, el largo de la trama cuenta **bytes** (`'ñandú'` viaja como `0005` con 6 bytes). Revisar que la docu lo diga asi.
- **Indice del PDF se pasa dos lineas** y deja la pagina 3 casi vacia (369 caracteres). `revisar_pdf.py` lo marca.
- **`LEEME.txt` de la carpeta de entrega esta viejo:** dice que el fix esta sin commitear en `fix/robustez-final` y que `main` esta en `57bd085`. Corregir a `main` `8e80066`, PR #28 y #29.
- **Santiago commitea con `[email]`** (18 commits) ademas de su noreply (9). Si ese email no esta verificado en su cuenta de GitHub, esos commits no le quedan vinculados y la nota es individual. Accion: [[Santiago Louro]] abre `cb35a28` en GitHub; si su nombre no es link a su perfil, agrega el email en Settings > Emails (vincula retroactivo, sin reescribir nada).
- **Commit `e349a32` de [[Paula Poli]] con `Co-Authored-By: Claude`** en `main`: GitHub muestra a Claude como contribuidor. No reescribir `main` (romperia clones y PR antes del Release); es decision de Paula. Ya registrado en el dev log.
- **Develop no existe en origin** (el repo borra ramas al mergear). Despues del Release, recrear `Develop` desde `main` por la web y no tocar `main` (la letra pide main solo para releases). `cb35a28` de Santiago entro a `main` sin PR.
- **Compilar en Release ensucia los binarios trackeados.** Quien compile en su clon hace `git restore Edison.Client/bin Edison.Server/bin` antes de commitear.
- **`docs/` se ignora solo en el clon de Lucas** (`.git/info/exclude:7`), no en `.gitignore`. Avisar al grupo que no copien `docs/` a sus clones.
- **Ramas remotas viejas:** `feature-Serve_client_comunication` y `feature/business-logic-completion` mergeadas; `feature/vehicle-business-logic` tiene `01f9830` de Paula fuera de `main` (PR #12, reemplazado por `e137570`). Confirmar que no quede ningun PR abierto; borrar ramas solo despues del Release.
- **Consultas con los docentes solo hasta el domingo 27-09** (acta del 24-09). Dudas de ultimo momento, hoy por Teams.
- **Clase del 22-09 sin acta** en `Clases/`. Puede traer info de entrega o defensa; ingestar antes del lunes.

## Riesgos de defensa y parche candidato (no aplicado)
Verificados en codigo y con binarios en scratch. Ninguno cae el proceso; todos se ven feos en la planilla de testing.
- Cancelar una reserva rechazada o cancelada dice `No se puede cancelar una reserva en estado Rejected.` (`TerminalReservationState.cs:21` interpola el enum).
- Al cerrar el server con `salir`, el cliente muestra un mensaje de Windows ("La operacion se completo correctamente" o "Se ha anulado una conexion establecida..."), porque `NetworkHelper.cs:26` y `:47` hacen `throw new SocketException()` sin codigo. La docu linea 340 promete un "mensaje legible". El cliente recien se entera en la proxima opcion.
- Un modelo creado con stock 0 queda trabado: no aparece en el listado, no se puede modificar ni ponerle imagen, solo borrar (`VehicleService.cs:43-58` no chequea stock; `:69-72` y `ImageRequestHandler.cs:113` exigen unidades).
- El server no hace `Shutdown` antes de `Dispose` del socket del cliente (`ClientHandler.cs` finally).
- Otros menores sin parche: el catch-all de `ClientHandler.cs:90-93` no imprime tipo ni mensaje; errores de carpeta invalida salen en ingles (texto de .NET); el server acepta fecha de entrega pasada y precio con 3 decimales por protocolo crudo (la validacion de formato esta solo en el cliente); modificar a stock 0 se rechaza aunque no haya reservas; el listado muestra `$ 38.900,00` pero al cargar se rechaza `38.900`; durante 5 s el otro cliente ve "reservadas: 0"; un puerto gigante en `.dll.config` tira stack trace; maquina de estados de sesion implicita; I/O de disco dentro del lock global.

Parche candidato de 4 archivos (14 lineas mas, 1 cambiada), compilado con 0 errores y probado en scratch con dos clientes:

```diff
--- a/Edison.Server/Networking/ClientHandler.cs
+++ b/Edison.Server/Networking/ClientHandler.cs
@@ -103,6 +103,9 @@
             finally
             {
+                try { _socket.Shutdown(SocketShutdown.Both); }
+                catch(SocketException) { }
+                catch(ObjectDisposedException) { }
                 _socket.Dispose();
             }
--- a/Edison.Server/Domain/States/TerminalReservationState.cs
+++ b/Edison.Server/Domain/States/TerminalReservationState.cs
@@ -18,5 +18,10 @@
     private ValidationException Invalid(string action) =>
-        new ValidationException($"No se puede {action} una reserva en estado {Status}.");
+        new ValidationException($"No se puede {action} una reserva {Status switch
+        {
+            ReservationStatus.Rejected => "rechazada",
+            ReservationStatus.Cancelled => "cancelada",
+            _ => "finalizada"
+        }}.");
--- a/Edison.Server/BusinessLogic/VehicleService.cs
+++ b/Edison.Server/BusinessLogic/VehicleService.cs
@@ -44,6 +44,7 @@
         ArgumentNullException.ThrowIfNull(data);
+        if(data.Stock <= 0) throw new ValidationException("El stock inicial debe ser mayor a cero.");
--- a/Edison.Client/Program.cs
+++ b/Edison.Client/Program.cs
@@ -21,6 +21,10 @@
+        catch(SocketException error) when(error.SocketErrorCode is SocketError.Success or SocketError.ConnectionReset or SocketError.ConnectionAborted)
+        {
+            Console.WriteLine("Se perdió la conexión con el servidor.");
+        }
```

**Si se aplica, un solo lote y antes del Release:** aplicar, `dotnet build -c Release` y `dotnet publish -c Release -r win-x64 --self-contained`, copiar solo `Edison.Server.dll/.pdb` y `Edison.Client.dll/.pdb` a las 4 carpetas `bin/Release` (`Edison.Common` no cambia), probar con dos clientes, PR a `main`, y recien ahi el Release. Si no hay tiempo, no tocar nada y llevar las explicaciones. El 27-09 el usuario habia decidido arreglar solo lo grave (ver [[2026-09-27 - Obligatorio 1 EDISON]] seccion 4); esta auditoria no cambia esa decision, solo deja el parche listo. Si se aplica el de stock 0, sumar una linea al supuesto 3 de la docu.

## Verificado como correcto (ok)
- `FrameHandler.cs:27-30`: una parte fija valida es ASCII y siempre da 9 caracteres; tramas validas con multibyte en los datos se parsean bien aun en trozos de 1 y 3 bytes.
- Catch-all de `ClientHandler`: cierra solo esa conexion, el `finally` hace Logout y Dispose, el apagado sigue limpio, `InventoryLock` libera ante excepcion.
- Precio en cliente (es-UY): acepta `38,90`, `38.90`, `38900`; rechaza `38.900`, `1.234,56`, mas de 2 decimales, negativos, `1e5`.
- `DownloadImage` valida la carpeta **antes** del Send: no hay desincronizacion del stream. Comillas del Explorador quitadas en subida y descarga.
- CR1-CR10 y SR1-SR4 implementados y probados con los win-x64 del repo. Thread explicito, sin `Task`, sin serializadores, sin comentarios.
- win-x64 autocontenido de verdad; no hay estado de runtime commiteado; clon limpio de `main` compila; `.sln` sin proyectos fantasma. Las seis carpetas viejas de la raiz (`Edison.BusinessLogic/`, etc.) son restos locales de bin/obj, fuera del `.sln`.
- La docu tiene los 6 puntos de la letra, refleja el fix `c470b57`, y `docs/DOCUMENTACION.md` del repo es identica a la de la carpeta de entrega. Diagramas 1-4 y 7 coinciden con el codigo; el 6 no muestra que la propiedad se revalida en el segundo `InventoryLock`.
- Autoria en git representativa de los tres (25 commits Lucas, 22 Paula, 27 Santiago entre sus dos emails, as of 2026-09-27).
- El mapa de EDISON (artifact `https://claude.ai/artifact/LhAjUhjFDyzpQCDU6d2bVM`) no tiene referencias a `fix/robustez-final` sin commitear.

## Connections
- [[Estado del Obligatorio 1 - 2026-09-27]] - estado vigente que esta auditoria complementa
- [[2026-09-27 - Obligatorio 1 EDISON]] - la sesion auditada
- [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]] · [[Programacion de Redes 2026]]
- [[Clase 2026-09-24 - Middleware orientado a mensajes, RabbitMQ y AMQP]] · [[Clase 2026-08-27 - Letra del Obligatorio 1 y practica con Thread]]
- [[Paula Poli]] · [[Santiago Louro]] · [[Enrique Latorres]]
