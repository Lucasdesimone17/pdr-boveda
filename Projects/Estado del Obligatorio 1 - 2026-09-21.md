---
date: 2026-09-21
updated: 2026-09-22
type: status-report
tags:
  - pdr
  - obligatorio-1
  - estado
ai-first: true
confidence: verified
fuente: "Reauditoria de la rama Develop, commit a1fdbf3, contra la letra oficial y las 9 actas de clase"
---

# Estado del Obligatorio 1 - EDISON

## For future agent
> **OBSOLETO desde el 2026-09-22.** Su conclusion principal ya no vale: los PR #21 a #24 cablearon reservas, imagenes e historial, que este informe lista como faltantes. El estado vigente esta en [[Estado del Obligatorio 1 - 2026-09-22]]. Este informe se conserva por dos cosas que siguen siendo utiles: la seccion **Que exige la entrega**, que no cambio, y las **cinco dudas para Teams**.

Informe de estado del Obligatorio 1, **reauditado el 2026-09-22** sobre la rama `Develop` en el commit `a1fdbf3`, a seis dias de la entrega. La version anterior de esta nota auditaba `6ebe4b5` y quedo obsoleta cuando el grupo mergeo los PR #21, #22, #23 y #24. El nombre del archivo conserva la fecha original porque hay seis notas que lo enlazan como `[[Estado del Obligatorio 1 - 2026-09-21]]`.

El proyecto entrega el **2026-09-28** y el **codigo esta practicamente completo**: los diez requerimientos de cliente y los cuatro de servidor viajan por socket. Lo que falta ya casi no es codigo: es **documentacion, `.gitignore`, merge a `main` y Release**, que pesan tanto como la aplicacion en la correccion.

El doc de Claude en `https://claude.ai/code/artifact/7ac8c469-b570-49bc-93ad-f97435f8eb6a` corresponde a la auditoria vieja de `6ebe4b5` y **esta desactualizado**.

## Estado actual

`Develop` esta en `a1fdbf3` y compila con **0 errores y 0 warnings**. Tres proyectos: `Edison.Client`, `Edison.Server`, `Edison.Common`.

Verificado el 22-09 sobre el codigo:

- **Prohibiciones duras cumplidas.** Cero apariciones de `Task`, `async`, `await`, `BinaryFormatter` o serializadores JSON en todo el arbol.
- **`Thread` explicito.** Hilo dedicado al `Accept`, un hilo por cliente, y ahora tambien el hilo del `ReservationProcessor`.
- **Cierre controlado, SR4.** Comando `salir`, banderas con `Volatile`, `Join` de todos los hilos, `Shutdown` y `Dispose` de cada socket. El `ReservationProcessor` es `IDisposable` y se cierra con `using` en `Program.cs`.
- **`Send` y `Receive` en loop por offset**, en `NetworkHelper`. Recibir 0 bytes se trata como conexion cerrada.
- **Trama igual a la tabla de la letra.** 9 bytes de encabezado, relleno con ceros a la izquierda, largo contado en bytes en `Frame.ToBytes`.
- **Configuracion externa.** `App.config` en las dos puntas, leido con `ConfigurationManager`.
- **Concurrencia protegida.** `lock` en repositorios y servicios, mas `InventoryLock` para inventario y reservas.
- **Una sola instancia de `ReservationRepository`** compartida entre `VehicleService` y `ReservationService` desde `Program.cs`. Era el bug numero 1 del informe anterior: **arreglado**.
- **Transferencia de archivos robusta.** `FileTransferHelper` es stream-based con overloads `SendFile(Stream, long)` y `ReceiveFile(Stream, long)`; abre el destino con `FileMode.CreateNew` y descarta el parcial si la transferencia se corta, tragando `IOException` y `UnauthorizedAccessException` del borrado para no tapar la excepcion original.
- **Subida de imagen transaccional.** `ImageRequestHandler` recibe a un `.part`, hace `File.Move` al nombre definitivo dentro del lock, y limpia el temporal y la imagen anterior en el `finally`.
- **La transferencia de bytes ocurre fuera del lock.** Solo la validacion de dueno y la apertura del `FileStream` entran a `InventoryLock`, asi que una imagen de 10 MB no congela el servidor.
- **Validacion de imagenes en dos capas.** `ImageRules` valida nombre, extension y tamano hasta 10 MiB; `ImageStorage.GetPath` bloquea recorrido de directorios, y los archivos se guardan con nombre GUID, no con el nombre que manda el cliente.
- **Timeouts de 30 s** en `ReceiveTimeout` y `SendTimeout` durante las transferencias de imagen, restaurados a 0 en el `finally`.

## Que exige la entrega

**Formal.** Entrega por Gestion el 28/09/2026 hasta las 21:00; si no se entrega por Gestion el puntaje es 0. Maximo 40 MB. Puntaje entre 5 y 25. Grupos de hasta 3 personas del mismo dictado. Ante un problema, escribir a `adjuntos_ei@ort.edu.uy` antes de las 20:00 de ese dia.

**Repositorio.** Uno solo, privado, dentro de la organizacion `ArqSis-PDR-2026-2`, con el numero de estudiante en el nombre y todos los integrantes invitados. Commits representativos de que parte hizo cada uno, o pull requests indicando colaboradores. Al entregar hay que hacer un **Release de GitHub con codigo fuente, compilados y documentacion**, y la rama `main` no se toca despues del hito.

**Compilados.** El repositorio debe incluir las aplicaciones compiladas en modo Release, con todo lo necesario para ejecutarlas, ejecutables de Windows incluidos.

**Documentacion.** La letra la llama tan importante como la aplicacion. Seis puntos en orden: alcance, errores conocidos, supuestos de la letra, arquitectura, diseno detallado de los componentes y diseno del protocolo. Ademas hay que explicar las decisiones: por que ese protocolo, como se manejan los errores y cuales son los mecanismos de concurrencia. Se espera uso adecuado de diagramas. **El link al repositorio va en la primera pagina.** Si se uso IA generativa hay que citar la herramienta y el contexto de uso.

**Criterios de correccion.** Uso explicito de `Thread` y no `Task`. Prohibido `binary formatter`. Prohibidos los parsers de serializacion existentes, como JSON. En la defensa se corre un plan de testing y se cuenta cuantos puntos pasa el sistema. Se evalua diseno, porcentaje de cumplimiento, funcionamiento, buenas practicas de multihilo y sockets, y la documentacion.

**Defensa.** Obligatoria y eliminatoria: no presentarse es perder todos los puntos. Presencial, ejecutando la aplicacion en varias maquinas del laboratorio con Windows 10 u 11. La nota es individual y todos los integrantes tienen que conocer toda la solucion. Hay que configurar la IP del servidor en el cliente en el momento, porque el laboratorio usa DHCP.

## Cobertura requerimiento por requerimiento

| ID | Requerimiento | Estado | Nota |
| --- | --- | --- | --- |
| CR1 | Autenticacion | Completo | Register, Login y Logout por socket |
| CR2 | Alta de modelo | Completo | El alta viaja y la imagen opcional se sube con `UploadImage` desde `VehicleClient` |
| CR3 | Modificacion de modelo | Completo | Con dueno y stock mayor a reservas |
| CR4 | Baja de modelo | Completo | Valida reservas activas y borra la imagen |
| CR5 | Solicitud de reserva | Completo | Handler, hilo temporizador y `ReservationDelaySeconds` en configuracion |
| CR6 | Cancelacion de reserva | Completo | Valida dueno y libera la unidad si la reserva estaba confirmada |
| CR7 | Busqueda y filtrado | Completo | Cinco criterios, resultados paginados por bytes |
| CR8 | Consultar vehiculo | Completo | Detalle mas `DownloadImage` desde `VehicleDetailMenu` |
| CR9 | Estado de reservas | Completo | `GetReservations` ordenado por fecha descendente |
| CR10 | Historial de actividades | Completo | `GetActivity` con handler y opcion 3 del menu principal |
| SR1 | Funcionalidad completa | Completo | Ya no arrastra requerimientos sin cablear |
| SR2 | Conexiones multiples | Completo | Hilo por cliente; ver el riesgo del lock global |
| SR3 | Eliminacion de archivos | Completo | |
| SR4 | Cierre controlado | Completo | Incluye el hilo del procesador de reservas |

De los requerimientos en comun: **configuracion** quedo completa, con `ReservationDelaySeconds` validada al arranque; **desconexiones** y **manejo de errores** siguen cubiertos, el servidor responde una trama de error y sigue atendiendo.

## Prueba de ejecucion del 22-09

Se levanto el servidor y se corrieron clientes reales contra el, con la entrada de consola redirigida. **Los catorce requerimientos pasaron.** El servidor no imprimio un solo error en toda la corrida y cerro con `salir` y codigo de salida 0.

Lo que se ejecuto de punta a punta:

- **CR1.** Registro de varios usuarios, login, logout. Rechaza usuario duplicado y valida largo de usuario y contrasena.
- **CR2.** Alta con imagen: el archivo llega al servidor y se guarda con nombre GUID.
- **CR3.** Modificacion valida; rechaza al que no es el creador; rechaza stock menor a las unidades reservadas con `El stock debe ser mayor a las unidades reservadas.`
- **CR4.** Baja bloqueada con `No se puede eliminar Tesla Model 3 porque tiene reservas activas.`; bloqueada tambien para quien no es el creador; baja exitosa sin reservas.
- **CR5.** Reserva creada como Pendiente y confirmada sola a los 5 segundos, que es el valor de `ReservationDelaySeconds`. **El rechazo por falta de stock tambien se probo:** con 2 unidades disponibles y tres clientes reservando a la vez, quedaron dos Confirmadas y una Rechazada.
- **CR6.** Cancelacion con confirmacion; la unidad vuelve al inventario, verificado en el detalle del modelo.
- **CR7.** Listado sin filtros, filtro por nombre con resultados, y filtro combinado nombre mas precio maximo sin resultados.
- **CR8.** Detalle completo y descarga de imagen: el archivo bajado es **byte a byte identico** al original, mismo MD5.
- **CR9.** Estado de las reservas propias con las transiciones Pendiente, Confirmada, Cancelada y Rechazada.
- **CR10.** Historial por usuario con login, alta, modificacion, reserva solicitada, confirmada y cancelada.
- **SR2.** Tres clientes simultaneos contra el mismo modelo, sin corrupcion de inventario ni errores en el servidor.
- **SR3.** Al eliminar el modelo desaparece su archivo de imagen del disco del servidor.
- **SR4.** Cierre con `salir`: cierra conexiones, hace `Join` de los hilos, imprime `Conexiones cerradas.` y sale con codigo 0.
- **Validacion de imagenes.** Un `.txt` se rechaza con `Use una imagen PNG, JPG, GIF, WEBP o BMP.`
- **Desconexiones.** Clientes que cierran la entrada de golpe no dejan rastro de error en el servidor, que sigue atendiendo.

Tres cosas menores que aparecieron corriendolo, ninguna es un incumplimiento:

1. **Publicar con una imagen invalida aborta el alta entera.** `VehicleClient.Save` valida la imagen antes de mandar el `CreateVehicle` y hace `return`, asi que el modelo no se crea aunque la imagen sea opcional. Hay que volver a cargar todo el formulario. Decision defendible, pero conviene saberla.
2. **El alta con imagen deja dos entradas en el historial**, `Modelo creado` y `Modelo modificado` con el mismo segundo, porque la subida se implementa reusando `Modify`. Ruido en CR10.
3. **Sin persistencia, las imagenes quedan huerfanas.** Al reiniciar el servidor el catalogo arranca vacio, pero los archivos de la corrida anterior siguen en la carpeta de imagenes y ya no los referencia nadie.

## Pulido del 22-09, rama `feature/pulido-final`

Cinco cosas corregidas despues de la reauditoria, todas probadas corriendo el sistema. Compila con 0 errores y 0 warnings.

1. **Persistencia y datos de prueba.** `StateStore` escribe el estado completo, usuarios, vehiculos, reservas e historial, en `data/edison-data.txt` al cerrar, y lo levanta al arrancar. El formato es el mismo campo-con-largo-adelante del protocolo, reusando `DataBuilder` y `DataReader`, asi que **no se agrega ningun serializador**, que la letra prohibe. Se escribe primero a un `.tmp` y recien despues se reemplaza, para no dejar el archivo a medio escribir. Las unidades reservadas no se guardan: se recalculan contando las reservas confirmadas, asi el inventario nunca puede quedar desincronizado. Si el archivo esta ilegible se aparta con sufijo `.corrupto-<fecha>` y el servidor arranca igual. Cuando no hay archivo, `SeedData` carga tres usuarios y cuatro modelos, todos con contrasena `edison123`. La clave nueva es `DataFile` en el `App.config` del servidor.
2. **La busqueda y el detalle salieron del lock global.** `VehicleRequestHandler` solo entra a `InventoryLock` para las operaciones que escriben; `GetVehicle` y `SearchVehicles` corren afuera, y `VehicleService.Search` tampoco lo toma. Cada repositorio sigue protegiendo su propia lista. Es exactamente lo que el docente midio el 03-09.
3. **Codigo muerto borrado.** Se elimino `FileStreamHelper` y `Protocol.CalculateParts`, que habian quedado sin llamador tras la reescritura stream-based de `FileTransferHelper`.
4. **`SettingsManager` ahora dice que clave falta.** Antes devolvia cadena vacia y el error explotaba mas tarde en `IPAddress.Parse` o `int.Parse` sin mencionar la clave. Ahora tira `Falta la clave ServerPort en el archivo de configuracion, o esta vacia.` Es el error que se va a ver en el laboratorio cuando se edite el `.config` a mano.
5. **Los dos detalles que apareceron probando.** Una imagen invalida ya no aborta el alta entera: avisa y guarda el modelo sin imagen. Y publicar con imagen deja **una sola** entrada en el historial: la subida ahora usa `IVehicleService.SetImage`, que cambia el archivo sin registrar una modificacion del modelo.

Verificado corriendo: arranque en limpio con los datos de prueba, alta con imagen invalida que igual se guarda, historial con una sola entrada, reserva confirmada, **cierre y reinicio del servidor con el estado intacto**, incluidas las unidades reservadas y la imagen de un modelo, cancelacion que libera la unidad, busqueda con filtros, descarga de imagen y baja que borra el archivo.

## Revision adversarial del pulido, 22-09

El diff de `feature/pulido-final` contra `a1fdbf3` se paso por una revision independiente con instruccion de buscar solo defectos de correctitud. Salieron cuatro cosas. Las dos primeras eran bugs reales introducidos por el pulido y **ya estan arregladas** en el commit `62a9ec8`:

1. **El estado se guardaba con el procesador de reservas todavia vivo.** El `using var processor` de `Program.Run` libera recien al salir del metodo, o sea despues de `SaveState`. Si una reserva se confirmaba durante el guardado, esa confirmacion se perdia; peor, como `Save` toma los cuatro `GetAll()` en instantes distintos, el archivo podia quedar con el historial diciendo "reserva confirmada" y la reserva guardada como pendiente. Ahora el procesador se cierra antes de guardar y su `Dispose` es idempotente.
2. **Lecturas rasgadas al sacar las consultas del lock.** `Search` y `ToDto` leian las entidades vivas mientras `Modify` les escribe los campos uno por uno bajo el lock: se podia devolver el precio nuevo con la descripcion vieja, o un trio stock/disponibles/reservadas que no cerraba. La copia a DTO paso a hacerse adentro del lock; afuera quedaron el armado de la pagina y la medicion en bytes, que es la parte cara. La mejora de no serializar el servidor durante una busqueda se mantiene.

La tercera es la que hay que **documentar como riesgo conocido**: la persistencia escribe las **contrasenas en texto plano** en `data/edison-data.txt`. Antes vivian solo en memoria. La letra no exige hash y no se cambio, pero es un cambio real de exposicion y tiene que figurar en el punto de errores conocidos de la documentacion.

La cuarta fue confirmar que estaban limpios la alineacion campo a campo del formato de persistencia, `ApplyReservedUnits`, la numeracion de ids despues del `Load`, el flujo `.part` / `File.Move` / `SetImage` de las imagenes y el BOM del UTF-8.

Despues de los arreglos se reprobo: cuatro clientes reservando el mismo modelo con stock 2 mas dos clientes martillando busquedas dieron **dos confirmadas y dos rechazadas**, con los lectores viendo siempre datos coherentes y cero errores; y una reserva dejada pendiente al apagar quedo guardada, se confirmo al levantar y el historial cerro con la reserva.

## Bugs y riesgos abiertos

1. **El `ReservationProcessor` despierta cada 200 ms** y toma el lock global en cada vuelta, aunque no haya reservas para procesar. Con varios clientes en la defensa suma contencion gratis. Alcanza con espaciar el `Wait` o con salir temprano si no hay pendientes.
2. **Lock anidado.** El handler toma `InventoryLock` y el servicio lo vuelve a tomar adentro, y ahora tambien `SetImage`. No hay deadlock porque `lock` en C# es reentrante sobre el mismo hilo, pero conviene saberlo antes de que lo pregunten.
3. **`new SocketException()` sin parametro** en `NetworkHelper`, dos veces: toma el ultimo error de Win32, asi que el mensaje va a mentir cuando haya que diagnosticar algo en vivo.
4. **El largo de campo interno se cuenta en caracteres.** `DataBuilder.Add` escribe `text.Length` y `DataReader` corta por caracteres sobre la cadena ya decodificada. Es internamente consistente y no rompe, porque el largo de la trama si va en bytes, pero contradice la regla que el docente repitio el 08-09. Hay que dejarlo documentado y poder defenderlo. Ahora ademas es el formato del archivo de estado, no solo del protocolo.
5. **Carpetas de proyecto huerfanas** en la copia de trabajo local: `Edison.BusinessLogic`, `Edison.DataAccess`, `Edison.Domain`, `Edison.IBusinessLogic`, `Edison.IDataAccess` y `Edison.Protocol`, con solo `bin` y `obj`, fuera de `Edison.sln`. **No estan en el repositorio**, solo en el disco.
6. **Las imagenes huerfanas siguen sin limpiarse.** Con persistencia el caso es mucho mas raro, pero si el archivo de estado se pierde o se aparta por corrupto, los archivos de imagen de la corrida anterior quedan en la carpeta sin que los referencie nadie.

## Entregables que todavia no existen

Esto no es codigo, y pesa tanto como el codigo en la correccion. **Es el bloque critico que queda.**

1. **La documentacion no existe.** No hay ningun documento en la rama. La letra la llama tan importante como la aplicacion y pide seis puntos en orden, mas las decisiones de protocolo, de manejo de errores y de concurrencia, con diagramas. Insumo ya escrito: [[Decisiones de la capa de protocolo - Obligatorio 1]].
2. **Falta la citacion de IA generativa:** que herramientas se usaron y en que contexto.
3. **El `.gitignore` bloquea los compilados.** Ignora `[Bb]in/`, `[Oo]bj/` y `[Rr]elease/` sin ninguna excepcion, asi que hoy es imposible subir el Release que la letra exige. Hay que agregar negaciones para la carpeta de Release del cliente y del servidor, con sus `.config`.
4. **Falta el Release de GitHub** con codigo fuente, compilados y documentacion.
5. **`main` esta 58 commits atras de `Develop`.** Hay que mergear antes del hito y despues no tocarla.
6. **Revisar que haya commits representativos de cada integrante**, porque la nota del obligatorio es individual y la letra lo pide explicitamente.

## Plan de los seis dias

El bloque de cableado por socket se cerro entre el 21 y el 22 de septiembre. Lo que queda esta ordenado por puntos rescatados sobre trabajo invertido.

- [x] Compartir una sola instancia de `ReservationRepository` en `Program.cs`.
- [x] Cablear reservas por socket: CR5, CR6 y CR9 (PR #21).
- [x] Hilo del proceso de aprobacion mas la clave `ReservationDelaySeconds` en la configuracion del servidor.
- [x] Cablear el historial, CR10 (PR #22).
- [x] Cablear imagenes sobre `FileTransferHelper`: subir en CR2 y bajar en CR8 (PR #23).
- [x] Hecho el 21-09: el archivo a medio recibir se descarta y la excepcion se vuelve a lanzar (PR #24).
- [ ] **Escribir la documentacion completa:** los seis puntos, las decisiones de protocolo, errores y concurrencia, diagramas, el link al repositorio en la primera pagina y la citacion de IA. Es el pendiente mas grande y el de mayor peso.
- [x] **`.gitignore` arreglado el 22-09.** Se desanda `[Bb]in/` nivel por nivel para dejar pasar `bin/Release/` del cliente y del servidor: 26 archivos, 3,5 MB, con los dos `.exe` y los dos `.dll.config`. `obj/` y `Debug/` siguen ignorados, igual que el `data/` y el `images/` que el servidor genera dentro del Release.
- [x] Datos de prueba precargados, persistencia simple, busqueda fuera del lock, codigo muerto borrado y `SettingsManager` con mensaje util. Ver "Pulido del 22-09".
- [x] Probar los catorce requerimientos contra el servidor real: hecho el 22-09, todos pasaron. Ver "Prueba de ejecucion del 22-09".
- [ ] **Probar en dos maquinas con Windows** cambiando solo los `App.config`, sin recompilar. La corrida del 22-09 fue toda en `127.0.0.1`, asi que esto sigue sin verificar y es literalmente lo que se prueba en la defensa.
- [ ] **Merge a `main`, Release de GitHub** con fuente, compilados y documentacion, y despues no tocar `main`.
- [ ] Armar el plan de testing propio: los diez CR y los cuatro SR, con el paso a paso que se va a correr en la defensa.
- [ ] Borrar o justificar `FileStreamHelper` y `Protocol.CalculateParts`, que quedaron sin llamador.
- [ ] Opcional pero barato: sacar la busqueda de solo lectura del lock global y espaciar el poll del `ReservationProcessor`.
- [ ] Opcional: darle un codigo al `SocketException`, hacer que `SettingsManager` avise que clave falta, y limpiar las carpetas huerfanas del disco.
- [ ] Opcional: datos de prueba precargados para la defensa.

Si el tiempo aprieta, lo que no se puede dejar afuera es **la documentacion, el `.gitignore` y el Release**: el codigo ya cubre el porcentaje de cumplimiento, y esos tres son los unicos que pueden costar puntos por no existir.

## Dudas para preguntar por Teams

1. **Formato del archivo de entrega.** La letra se contradice tres veces: la caratula dice zip, rar o pdf; la seccion de entrega dice un archivo PDF; el recordatorio dice un zip o rar con el PDF adentro. Mitigacion que cumple las tres: subir un zip con el PDF adentro.
2. **Grupos del mismo dictado.** La letra lo exige, pero el 18-08 en clase dijeron que se podian mezclar turnos. Si el grupo se armo mezclado, conviene confirmarlo antes.
3. **CR10 menciona compras realizadas** y no existe ningun requerimiento de compra entre CR1 y CR9. Si no hay respuesta, declararlo como supuesto en la documentacion.
4. **Roles de vendedor y comprador.** La letra no los define. El 27-08 dijeron que es decision del grupo y que hay que documentarla.
5. **Largo de campo interno en caracteres.** Confirmar si aceptan la variante textual sobre la cadena decodificada, o si quieren el conteo en bytes tambien en los campos internos.

## Como se hizo esta reauditoria

Lectura directa del arbol de `Develop` en `a1fdbf3`: `Program.cs` del servidor, `ClientHandler`, los cuatro handlers de red, `ReservationService`, `ReservationProcessor`, `ImageRequestHandler`, `ImageStorage`, `ImageRules`, `FileTransferHelper`, `DataBuilder`, `SettingsManager`, `InventoryLock`, los tres menus del cliente, los dos `App.config` y el `.gitignore`. Mas `dotnet build`, el grep de prohibiciones sobre todo el arbol y el conteo de commits entre `origin/main` y `origin/Develop`. Los hallazgos de la auditoria anterior que no cambiaron se conservan tal cual, incluidas sus correcciones de atribucion: el lock global sale de la clase del 03-09 y no de la letra, y el largo de la trama en bytes esta bien porque lo que se cuenta en caracteres son los subcampos internos de `DATOS`, formato que la letra deja libre al grupo.

## Connections
- [[Programacion de Redes 2026]] - proyecto del cursado
- [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]] - la consigna completa contra el PDF oficial
- [[Reglas y prohibiciones del curso PDR 2026]] - las reglas duras, item por item
- [[Errores caros en Programacion de Redes]] - los errores que los docentes demostraron
- [[Decisiones de la capa de protocolo - Obligatorio 1]] - insumo directo del punto 6 de la documentacion
- [[Clase 2026-09-10 - Maquina de estados del protocolo, Common y app.config]]
- [[Clase 2026-09-15 - Transferencia de archivos por chunks e introduccion a asincronia]]
- [[Clase 2026-09-17 - Task en detalle, continuations, ThreadPool y TPL]]
