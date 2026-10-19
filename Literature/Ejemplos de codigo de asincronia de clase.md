---
date: 2026-10-13
type: literature
tags:
  - pdr
  - asincronia
  - tema-3
  - codigo
  - obligatorio-2
  - docker
  - streams
ai-first: true
confidence: high
fuente: "`Sources/TEORICO/03_PARALELISMO/EJEMPLOS_04_ASYNC_EJS/` (29 proyectos, archivo `04_Async_ejs.zip`, Teams 17-09-2026); `Sources/TEORICO/03_PARALELISMO/EJEMPLOS_THREADEXAMPLES/` (10 proyectos, `ThreadExamples.zip`, Teams 17-09); `Sources/TEORICO/01_INTRODUCCION/EJEMPLOS_STREAM/` (`FileStreamTextMenu.cs` y `SimpleTableCRUD.cs`, Teams 18-09). Contrastado con `Sources/OBLIGATORIO/Obligatorio 2 Agosto 2026.pdf.md`. Ingeridos el 2026-10-13"
---

# Ejemplos de codigo de asincronia de clase

## For future agent
Analisis de los **41 ejemplos de codigo** que la catedra reparte para el Tema 3 (asincronia) mas dos ejemplos de streams del Tema 1: 29 proyectos de `04_Async_ejs`, 10 de `ThreadExamples` y 2 archivos de `EJEMPLOS_STREAM`. Es la nota hermana de [[Ejemplos de codigo de sockets de clase]]. Importa porque `04_Async_03_00Server`/`Client` es la **base directa para migrar EDISON (Obligatorio 2) de `Thread` a `async/await`** y `04_Async_04_Docker` es el unico ejemplo de Docker de la catedra. Todo lo que figura sin marca sale del codigo, con `archivo:linea`; lo que es inferencia o conocimiento general de .NET va marcado como **(inferido)**. Vigencia: **as of 2026-10-13**; el codigo es de la catedra y no cambia dentro del semestre. Los numeros de linea se refieren a los archivos tal como estan en `Sources/`.

Rutas abreviadas en la nota: `ASYNC/` = `Sources/TEORICO/03_PARALELISMO/EJEMPLOS_04_ASYNC_EJS/`, `THREADEX/` = `.../EJEMPLOS_THREADEXAMPLES/`, `STREAM/` = `Sources/TEORICO/01_INTRODUCCION/EJEMPLOS_STREAM/`.

## Que hay en las carpetas

| Carpeta | Contenido | Plataforma |
|---|---|---|
| `ASYNC/` | 29 proyectos de consola, uno por carpeta, en 5 familias numeradas `04_Async_00` a `04_Async_04` mas `04_Async_0_Ejemplo00/01` | `net8.0` en los 29. 25 llevan `PublishAot` y `InvariantGlobalization`; no los llevan `04_Async_04_Docker`, `04_Async_04_Example` ni `04_Async_0_Ejemplo00/01` |
| `THREADEX/` | 10 proyectos con una `.sln` y un `global.json` | `net9.0`; `global.json` fija SDK 9.0.0 con `rollForward: latestMinor` |
| `STREAM/` | 2 archivos `.cs` sueltos, sin `.csproj` | Sin definir |

Los archivos no traen linea de autor. La autoria de `STREAM/` (Enrique Latorres) sale de la ficha de ingesta, no del codigo. El aviso de ingesta hablaba de "~32 proyectos"; contadas las carpetas de `ASYNC/` son **29**.

Tres cosas de contexto que cambian como se lee el material:
- `ASYNC/04_Async_00_Ejemplo03_parallel03/...cs:6` trae una ruta tipo `~/RiderProjects/...bin/Debug/net8.0`: el docente trabaja con Rider y .NET 8.
- La mayoria de los ejemplos terminan en `Console.ReadKey()` para que la ventana no se cierre. No es parte de la leccion.
- Los de `THREADEX/` estan comentados en ingles, los de `ASYNC/` en espanol.

## Familia A - `Task` y `async/await` basicos

| Proyecto | Que ensena | API clave |
|---|---|---|
| `04_Async_0_Ejemplo00` | `new Task(...)` + `Start()` con `Thread.Sleep` adentro. `Main` es `async Task` pero **no hace `await`**: termina y mata el proceso antes de imprimir "La tarea interna" (`Program.cs:17`, el `await task` esta comentado en `:16`) | `Task`, `Start` |
| `04_Async_0_Ejemplo01` | Igual, pero con `Console.ReadLine()` activo (`:17`): el proceso espera y la tarea llega a imprimir | `Task`, `Start` |
| `04_Async_00_Ejemplo00a` | Linea base **sincronica**: `Thread.Sleep(4s)` dentro de `UnMetodo` (`:23`), `Main` espera | `Thread.Sleep` |
| `04_Async_00_Ejemplo00b` | `async void` + `await Task.Delay(4s)` (`:20-23`): `Main` imprime "terminó" **antes** de que `UnMetodo` termine | `async void`, `Task.Delay` |
| `04_Async_00_Ejemplo00c` | `async void` pero con `Thread.Sleep` (`:26`): como no hay `await`, el metodo corre sincronico y bloquea. El comentario lo explica (`:23-25`) | `async void` sin `await` |
| `04_Async_00_Ejemplo00` | Variante de `00b` donde `UnMetodo` hace `await Wait()` y `Wait` es `async Task` (`:27,31-35`). Deja comentadas las alternativas con `Thread.Sleep` y `Task.Delay` (`:25-26`) | `async Task` helper |
| `04_Async_00_Ejemplo01` | Dos `async Task<string>` lanzadas **sin `await`** (`:31-32`), `Main` hace su propio trabajo con `await Task.Delay` (`:39`) y recien despues `await task1; await task2` (`:46-48`) | `Task<T>`, `await` diferido |
| `04_Async_00_Ejemplo02` | `await client.GetStringAsync(url)` en un helper `async Task<string>` (`:14-19`). Con `using HttpClient`. Necesita Internet (`example.com`) | `HttpClient.GetStringAsync` |
| `04_Async_00_Ejemplo02a` | Lanza la descarga, hace trabajo independiente sincronico (`DoIndependentWork`, `:22`) y recien despues `await getStringTask` (`:24`). No libera el `HttpClient` (`:17`, sin `using`) | `Task<string>` guardada y esperada despues |

Hilo conductor de la familia: **`async` sin `await` no hace nada asincronico**, y `Main` puede terminar antes que la tarea. Las cuatro variantes `00`, `00a`, `00b`, `00c` (todas "tomado de dotnettutorials.net", `:5`) se leen juntas.

## Familia B - `Parallel`

| Proyecto | Que ensena | API clave |
|---|---|---|
| `04_Async_00_Ejemplo03_parallel00` | Cuadrados de una lista, imprime el id del hilo (`:23-28`) | `Parallel.ForEach`, `Environment.CurrentManagedThreadId` |
| `..._parallel01` | Rango 0..9 (`:11`) | `Parallel.For` |
| `..._parallel02` | Tres tareas con duraciones distintas (`:10-14`). `DoTask` espera con `Task.Delay(ms).Wait()` (`:22`) porque no es `async` | `Parallel.Invoke` |
| `..._parallel03` | Suma el tamano de todos los archivos de un directorio pasado por argumento; el acumulador compartido se protege con `Interlocked.Add(ref totalSize, size)` (`:26-36`). Valida `args.Length` y `Directory.Exists` (`:12-22`) | `Parallel.For`, `Interlocked` |

`Parallel` es para trabajo de CPU repartido entre hilos del pool y **bloquea al que llama** hasta que todos terminan. Los ejemplos no usan `await`. Lo dijo el docente en [[Clase 2026-09-22 - Parallel, CancellationToken y cuando usar async]].

## Familia C - el desayuno (`04_Async_01_*`)

Ejemplo canonico de Microsoft (link en `Desayuno01.cs:11`) en dos versiones. Se miden con `DateTime.Now` al principio y al final (`Desayuno00.cs:20,38`; `Desayuno01.cs:22,63`).

| Proyecto | Que ensena |
|---|---|
| `04_Async_01_Desayuno00` | **Todo secuencial.** Cinco `Task.Delay(3000).Wait()` (`:62,72,78,87,90`): tostadas, panceta (2 lados), huevos (2 pasos). Suma 15 s (calculado de los delays) |
| `04_Async_01_Desayuno01` | **Mismo trabajo con `async/await`.** Lanza huevos, panceta y tostada sin esperar (`:28-32`), las guarda en `List<Task>` (`:34`) y hace un loop con `Task.WhenAny` (`:40`), identifica cual termino comparando referencias (`:42-52`), hace `await finishedTask` para propagar excepciones (`:55`) y la saca de la lista (`:57`). Duracion de 6 s, la de la tarea mas larga (calculado de los delays). Cafe y jugo quedan sincronicos a proposito (`:23`) |

Dos patrones reutilizables: **`WhenAny` en loop** para procesar tareas a medida que terminan, y **composicion**: `HacerTostadaConMantecaYMermeladaAsync` hace `await TostarPanAsync` y despues pasos sincronicos (`:68-75`).

## Familia D - `Task`, continuations y cancelacion (`04_Async_02_*`)

| Proyecto | Que ensena | API clave |
|---|---|---|
| `Ejemplo00` | `await Task.Run(() => ...)` como forma corta de crear, iniciar y esperar (`:19`). Deja comentado el equivalente con `new Task` + `Start` + `await` y la pregunta "¿Qué pasa si no la iniciamos?" (`:9-15`) | `Task.Run` |
| `Ejemplo01` | `new Task` + `Start` + `await task` (`:8-14`). Pregunta abierta "¿Qué pasó?" en `:11` | `Task`, `Start` |
| `Ejemplo02` | `Main` sincronico: `new Task(PrintInfo)`, `Start()`, imprime "Hilo Principal terminado" y espera una tecla (`:7-12`) | `Task`, `Start` |
| `Ejemplo03` | **Tres `ContinueWith` con opciones** sobre una tarea que siempre termina bien: `OnlyOnCanceled`, `OnlyOnFaulted`, `OnlyOnRanToCompletion` (`:15-28`). Solo corre la ultima; espera con `completedTask.Wait()` (`:30`) | `ContinueWith`, `TaskContinuationOptions` |
| `Ejemplo04` | La misma matriz de tres continuations aplicada a **tres tareas**: completada (`:13`), con excepcion (`:32-35`) y cancelada con `CancellationTokenSource` (`:52-69`). Usa `token.ThrowIfCancellationRequested()` (`:58`) y pasa el token a `Task.Run(..., token)` (`:60`). Es el ejemplo que muestra los tres estados finales de un `Task` | `ContinueWith`, `CancellationTokenSource`, `ThrowIfCancellationRequested` |
| `Ejemplo05` | Encadenar con `ContinueWith`: `Task.Run(() => 12).ContinueWith(antecedente => string)` (`:11-22`), declarado como `Task<string>`; el docente anota la confusion de tipos en `:10,15`. `await task1` (`:26`) | `ContinueWith`, `antecedente.Result` |
| `Ejemplo05_continuation` | Tres `async Task<T>` independientes, `await Task.WhenAll(...)` (`:15`) y despues un reporte armado en `Task.Run` con `.Result` (`:18-22`), seguro porque ya terminaron. `await reportTask` (`:34`) | `Task.WhenAll` |
| `Ejemplo05_continueWith` | El mismo ejemplo con `Main` **no asincronico**: `Task.WhenAll(...).ContinueWith(prev => ...).Wait()` (`:16,34`) y las tareas envueltas en `Task.Run(async () => ...)` (`:42,52,62`) | `WhenAll` + `ContinueWith` + `Wait` |
| `Ejemplo06` | `new Task<int>(Func)` con `Start` (`:11-21`); `new Task<int>(obj => ..., 300)` con **estado** que se castea (`:27-37`); `Task.FromResult(...).Result` (`:55`, comentario `:52-53`: el `await` "no espera nada"); `await Task.Run<int>(funcion)` (`:64`) | `Task<T>`, `Task.FromResult`, estado |
| `Ejemplo07cancellation` | **Cancelacion cooperativa.** `CancellationTokenSource` (`:16`), token a `Task.Run(() => DoSomething(token), token)` (`:20`), `source.Cancel()` desde `Main` (`:25`). `DoSomething` chequea `token.IsCancellationRequested` despues de cada `await Task.Delay(100)` (`:40-48`) y hace `return` | `CancellationTokenSource`, `IsCancellationRequested` |

Observacion: este ejemplo **no pasa el token a `Task.Delay`** (`:40`), asi que la cancelacion se nota recien al terminar cada tick de 100 ms. Y como `DoSomething` hace `return` y no lanza, la tarea termina como **RanToCompletion, no como Canceled** (inferido de la semantica de .NET). Los `using System.Net.Sockets` y `System.Text` de `:4-5` sobran en este archivo; **(inferido)** es una plantilla copiada del ejemplo de sockets.

### `04_Async_04_Example`

`async Task Main` lanza `HacerUnaCosa(10)` y `HacerOtraMas(10)` (`:11-12`; comentario `:10`: "Las tareas se inician inmediatamente"). `HacerUnaCosa` lanza a su vez `HacerOtraCosa` (`:37`) y la espera al final (`:49`). Cada metodo imprime con indentacion distinta (`:20,45,61,74`) para ver el entrelazado en consola, mientras `Main` hace su propia productoria con `await Task.Delay(100)` (`:16-21`). Muestra **tareas anidadas y concurrentes con un solo hilo logico**, sin `Task.Run`.

## Familia E - `THREADEX/` (excepciones, continuations y ThreadPool)

Material de la clase del 17-09 ([[Clase 2026-09-17 - Task en detalle, continuations, ThreadPool y TPL]]). Estos 10 proyectos son `net9.0` y estan comentados en ingles.

| Proyecto | Que ensena | API clave |
|---|---|---|
| `ContinueWithExample01` | `ContinueWith` explicito sobre `Task` sin resultado (`Program.cs:19`) | `ContinueWith` |
| `ContinueWithExample02` | **`await` como continuation implicita**: todo lo que sigue es la continuacion (`:20-22`, comentario "CONTINUATION BOUNDARY") | `await` |
| `ContinueWithExample03` | `ContinueWith` sobre `Task<int>` que devuelve `Task<string>`, con `antecedent.Result` (`:18-25`) | `ContinueWith`, `Task<T>` |
| `ContinueWithExample04` | Lo mismo que `03` con dos `await` en secuencia sobre metodos que devuelven `Task<T>` (`:10,14,19-36`). Mismo resultado sin `ContinueWith` | `await` encadenado |
| `ContinueWithExample05` | `OnlyOnRanToCompletion` y `OnlyOnFaulted` sobre una tarea que lanza (`:18-24`); muestra como llegar a la excepcion original con `t.Exception.InnerException.Message` (`:23`) | `TaskContinuationOptions`, `AggregateException` |
| `MultipleTasksExceptionDemo` | Dos tareas que fallan, `await Task.WhenAll` (`:11,15`); para ver **todas** las excepciones, `allTasks.Exception.InnerExceptions` (`:22-28`). `await` solo relanza la primera (inferido) | `WhenAll`, `AggregateException` |
| `SimpleAsyncExceptionDemo` | `await` **desenvuelve** la excepcion: el `catch (InvalidOperationException)` funciona (`:14-16`) | excepciones en `async` |
| `SimpleAsyncExceptionDemo1` | Igual, pero la tarea se lanza **sin `await`** (`:13`), `Main` hace otra cosa y la excepcion recien aflora en `await t1` (`:17`) | excepcion guardada en la tarea |
| `ThreadException` | Menu de 6 escenarios con `Thread` (primer plano/fondo, excepcion manejada/no manejada, hilo largo). La no manejada tumba el proceso en ambos tipos de hilo (comentario `:110-111`). Fondo vs primer plano: el de fondo muere si `Main` termina (`:128-146`) | `Thread`, `IsBackground` |
| `ThreadPoolExample` | `ThreadPool.QueueUserWorkItem(FetchWebPage, state)` (`:42`) con un objeto de estado `WorkState` (`:35-39,88-92`) y `CountdownEvent` para saber cuando terminaron (`:28,48,81`). Una URL esta puesta para fallar (`:24`). Necesita Internet (`httpbin.org`) | `ThreadPool`, `CountdownEvent` |

Las dos parejas utiles para comparar son `ThreadException` contra `SimpleAsyncExceptionDemo*` (una excepcion no manejada en un `Thread` tumba el proceso; en un `Task` queda guardada en el `Task` hasta que alguien hace `await`) y `ContinueWithExample03` contra `04` (la misma logica con `ContinueWith` y con `await`).

## El cliente y servidor asincronicos (`04_Async_03_00Client` / `04_Async_03_00Server`)

**Es la base directa para migrar EDISON.** Es un eco de texto sobre TCP, puerto 9000 (`Server.cs:21`), con prefijo de largo de 4 bytes y UTF-8. Un solo mensaje por vuelta: cliente manda, servidor repite.

### Como el servidor acepta clientes
1. `TcpListener(IPAddress.Any, port)` y `listener.Start()` (`Server.cs:23-25`). `IPAddress.Any` es lo que hace falta dentro de un contenedor (inferido).
2. `AcceptClientsAsync(listener, shutdownCts.Token)` se lanza **sin `await`** y su `Task` se guarda (`:30`). `Main` sigue y se queda leyendo consola.
3. Loop `while (!token.IsCancellationRequested)` con `await listener.AcceptTcpClientAsync(token)` (`:63,68`). Reemplaza al `while(true)` + `Accept` bloqueante + `Thread` por cliente de [[Ejemplos de codigo de sockets de clase]].
4. Por cada cliente, `HandleClientAsync(client, token)` se lanza sin `await` y su `Task` se agrega a `static List<Task> clientTasks` (`:72-74`). **Es el reemplazo directo del `Thread` por cliente.**

### Como lee y escribe
- Stream: `using NetworkStream stream = client.GetStream()` (`:93`).
- Lee 4 bytes de largo con `ReadAsync(lengthBuffer, 0, 4, token)` (`:102`). `read == 0` significa desconexion (`:104-108`).
- `BitConverter.ToInt32` (`:109`), buffer del largo anunciado (`:111`), segundo `ReadAsync(buffer, 0, msgLength, token)` (`:113`), `Encoding.UTF8.GetString` (`:121`).
- Eco: escribe el largo `BitConverter.GetBytes(read)` (`:126`) y despues el contenido (`:128`).
- Cliente: `ConnectAsync("127.0.0.1", 9000)` (`Client.cs:15`), `Console.ReadLine()` sincronico (`:24`), `WriteAsync` del largo y del contenido (`:33-34`), `ReadAsync` del largo y de la respuesta (`:38,42`). Sin token.

### Cierre y cancelacion (lo que SR4 del Obligatorio 1 no tenia y el Obligatorio 2 deja ignorar por consola)
- Un `CancellationTokenSource shutdownCts` **estatico y unico** (`Server.cs:14`) cuyo token se pasa al accept y a todos los handlers.
- `Main` hace `Console.ReadLine()` en un loop hasta que llega `exit` (`:33-49`). Entonces ejecuta, **en este orden**: `listener.Stop()` (`:42`) y `shutdownCts.Cancel()` (`:45`).
- Los handlers bloqueados en `ReadAsync(..., token)` se cancelan con `OperationCanceledException` (`:131`). El accept sale por `catch ... when (ex is OperationCanceledException or ObjectDisposedException or SocketException)` (`:79-84`): `Stop()` provoca la excepcion de socket o de objeto liberado y el filtro la absorbe.
- `Main` espera: `await acceptClientsTask` y `await Task.WhenAll(clientTasks)` (`:51-52`). Cada handler cierra con `finally { client.Close(); }` (`:148-152`).

### Lo reutilizable
| Pieza | Donde |
|---|---|
| Loop de accept con `AcceptTcpClientAsync(token)` | `Server.cs:60-86` |
| Un `Task` por cliente, guardado en una lista para esperarlo al cerrar | `:72-74`, `:52` |
| Un `CancellationTokenSource` compartido que corta accept y lecturas a la vez | `:14,45,68,102,113` |
| Filtro de excepcion `when (ex is OperationCanceledException or ...)` | `:79-80` |
| Cadena de `catch`: `OperationCanceledException`, `IOException`, `SocketException`, `Exception` y `finally` que cierra el cliente | `:131-152` |
| Prefijo de largo de 4 bytes y `read == 0` como desconexion | `:102-108` (mismo framing que `03_Sockets_02`) |
| `await ConnectAsync` + `try/catch SocketException/IOException` + `finally` en el cliente | `Client.cs:10-66` |

### Lo que hay que corregir antes de reutilizarlo (bugs visibles)
1. **`Receive` parcial, otra vez.** `ReadAsync` se llama **una sola vez** y se asume que llegaron los 4 bytes del largo y luego los `msgLength` del mensaje (`Server.cs:102,113`; `Client.cs:38,42`). Es el mismo bug que `03_Sockets_02` planta a proposito. Con `await` el problema sigue igual: hay que acumular en loop hasta completar. En .NET 7 y posteriores existe `Stream.ReadExactlyAsync` (conocimiento general, inferido) y `NetworkHelper` de la nota de sockets ya resuelve la version `Socket`.
2. **El largo viene del cliente y no se valida** (`Server.cs:109-111`): negativo o enorme revienta en `new byte[msgLength]`. Cae en el `catch (Exception)` generico (`:144`) en vez de cortar la conexion con un mensaje claro.
3. **`clientTasks` nunca se poda** (`:74`): crece con cada cliente conectado. Para un servidor de larga vida hay que sacar las tareas terminadas (inferido).
4. **Lista compartida sin lock.** Hoy solo escribe el loop de accept y lee `Main` despues del `await`, asi que no hay carrera (`:12,74,52`). Si EDISON agrega o quita desde los handlers hay que protegerla.
5. **`Console.ReadLine()` sincronico dentro de `async Main`** (`:35`). Con stdin cerrado (contenedor sin `-i`) `ReadLine` devuelve `null`, `null?.ToLower()` no es `"exit"` y el loop `while (true)` gira sin parar (`:33-48`). **(inferido)** Sirve para el requisito de la letra de poder ignorar el cierre por consola en Docker.
6. **El eco usa `read` y no `msgLength`** (`:121,126,128`): con una lectura parcial devolveria menos bytes de los enviados.
7. **`WriteAsync` del largo sin token** (`:126`) mientras todos los demas lo reciben. Menor.
8. **Mensaje enganoso en `:131-135`**: el `catch` de `OperationCanceledException` imprime "se perdió la conexión" cuando en realidad es el cierre del servidor.9. **Cliente: `message` puede ser `null`** si `ReadLine` devuelve `null` (`Client.cs:24`); el `GetBytes(message)` de `:30` fallaria, y el proyecto trae `Nullable` en `enable`. **No chequea `read == 0`** de `:38,42`, asi que si el servidor cerro, el cliente no se entera por esa via.
10. **IP y puerto fijos**: `127.0.0.1:9000` en el cliente (`:15`), `9000` en el servidor (`:21`). Dentro de Docker `127.0.0.1` del cliente no apunta al contenedor del servidor (inferido). La letra pide cambiar los archivos de configuracion por algo mas adecuado a contenedores; el ejemplo **no muestra ninguna alternativa** (ni variables de entorno ni argumentos).
11. **Un solo tipo de mensaje**: texto UTF-8 con largo. EDISON necesita su propio protocolo; la letra autoriza seguir con el que ya tenian.
12. **Orden de bytes**: `BitConverter` directo (little-endian de la maquina), igual que la mayoria de los ejemplos de sockets; contradiccion ya anotada en [[Ejemplos de codigo de sockets de clase]].

### Correspondencia con lo que pide el Obligatorio 2
| Pide la letra | Que da el ejemplo | Falta |
|---|---|---|
| Sustituir todos los `Thread` por `Task` y modelar con `async/await` | Un `Task` por cliente en vez de un `Thread` (`:72`); `ReadAsync`/`WriteAsync`/`AcceptTcpClientAsync` | Un `Task` por cliente se **crea pero nunca se espera dentro del loop**; el patron es correcto pero la lista de `clientTasks` hay que gestionarla |
| Req. 5: cancelar una reserva pendiente cancela el mecanismo que la rechazaria, con la cancelacion de `Task` | El patron `CancellationTokenSource` + `IsCancellationRequested`/`ThrowIfCancellationRequested` de `Ejemplo04`/`Ejemplo07` | **Ningun ejemplo cancela una tarea individual**: todos cancelan "todo". Un `CancellationTokenSource` por reserva pendiente es una **propuesta (inferido)**, no esta en el codigo |
| Cerrado de servidor por consola: se puede ignorar | `Stop()` + `Cancel()` + `WhenAll` | — |
| Docker | Ver seccion siguiente | Port mapping, configuracion por entorno |

## `04_Async_04_Docker`

Una aplicacion de consola **no asincronica** (`Program.cs`) que pide por consola un directorio y lista sus archivos (`Directory.GetFiles(input, "*", SearchOption.AllDirectories)`, `:20`). La leccion no es el codigo sino **como se empaqueta y como se comparten archivos con el host**. El `.csproj` es `net8.0` con `DockerDefaultTargetOS = Linux` (`.csproj:10`) y sin `PublishAot`.

| Archivo | Que hace | Etapas |
|---|---|---|
| `Dockerfile` | Estilo "Visual Studio": `base` (`dotnet/runtime:8.0`, `USER $APP_UID`), `build` (`sdk:8.0`, `restore` + `build -c Release`), `publish` (`dotnet publish /p:UseAppHost=false`), `final` (`COPY --from=publish`, `ENTRYPOINT ["dotnet", "04_Async_04_Docker.dll"]`) | **Multi-stage, 4 etapas** (`Dockerfile:1,5,14,18`) |
| `Dockerfile.linux` | `sdk:8.0` copia todo y hace `dotnet publish -c Release -o out`; segunda etapa `runtime:8.0` copia `out` y arranca con `dotnet 04_Async_04_Docker.dll` | **Multi-stage, 2 etapas** (`:2,7,9`) |
| `Dockerfile.windows` | **Identico** a `Dockerfile.linux` salvo el comentario de la primera linea y el nombre | **Multi-stage, 2 etapas**; usa las mismas imagenes `sdk:8.0` y `runtime:8.0`, sin nada especifico de Windows. Si realmente construye una imagen de Windows depende del modo del daemon (TBD, inferido) |
| `build-lnx.sh` / `build-win.bat` | `docker build -t mydockerapp-lnx -f Dockerfile.linux .` y `-t mydockerapp-win -f Dockerfile.windows .` | El contexto es la carpeta del proyecto |
| `dockerrun.sh` / `dockerrun.bat` | `docker run -it --rm -v <carpeta>/shared:/app/shared -v mydockerapp_data:/app/data mydockerapp-<so>` | Monta un **bind mount** `./shared` y un **volumen con nombre** `mydockerapp_data` |

**Conclusion para el Obligatorio 2: los tres Dockerfile son multi-stage** (usan `COPY --from=` desde una etapa anterior). La letra dice **"No se debe utilizar multi-stage builds de Docker"**. Hay que reescribirlos en una sola etapa; no se pueden copiar tal cual. Un Dockerfile de una sola etapa tendria una unica linea `FROM`, por ejemplo la imagen SDK que compila y arranca (imagen mas pesada), o una imagen runtime a la que se le copia lo publicado fuera de Docker. Ambas son **propuestas (inferido)**; el material no trae ninguna.

### Lo que el ejemplo si ensena
- **Compartir archivos con el host**: `-v $(pwd)/shared:/app/shared` (`dockerrun.sh:3`). Es la tecnica que la letra exige documentar para acceder a las imagenes de modelos y a los comprobantes desde el host. En el ejemplo, escribir `/app/shared` en la consola lista lo que haya en la carpeta del host (inferido: el programa solo lista, nunca escribe).
- **Datos persistentes con volumen con nombre**: `mydockerapp_data:/app/data` (`:4`). Sirve para lo que no deba perderse al borrar el contenedor.
- `--rm` y `-it` (`:2`): contenedor interactivo y descartable.
- `USER $APP_UID` (`Dockerfile:2`): el contenedor corre sin root; `Dockerfile.linux` no lo declara.

### Lo que el ejemplo no cubre
- **Ningun `docker run -p` ni `EXPOSE`**: no hay publicacion de puertos. EDISON es cliente/servidor TCP, asi que el mapeo del puerto del servidor es trabajo nuevo.
- **Ni `docker-compose.yml`** (la letra lo deja opcional), **ni base de datos**, **ni red entre contenedores**. El requisito 3 de la letra (MySQL en Docker) no tiene ejemplo.
- **Configuracion por entorno**: el `Program.cs` no lee variables de entorno ni argumentos.
- El `.csproj` referencia `..\.dockerignore` (`:14`), que **no esta** en la carpeta ingerida.

### Bugs visibles
- `Program.cs:24-31`: el `catch` esta vacio y el `finally` **siempre** imprime "Could not scan directory" y llama a `Console.ResetColor()`, incluso cuando el listado salio bien. Probable intencion: avisar solo en caso de error (inferido).
- `Dockerfile:12` y `:16`: la etapa `build` compila y la etapa `publish` compila de nuevo; la primera compilacion sobra (inferido).
- `build-win.bat:1`: la linea `REM #!/bin/bash` es un resto copiado del `.sh`.

## Marcas de autocritica y trampas escritas por los docentes

Son la lista de lo que **no hay que copiar**, con su ubicacion:

| Donde | Que dice o hace | Por que importa |
|---|---|---|
| `ASYNC/04_Async_0_Ejemplo00/Program.cs:12` y `04_Async_0_Ejemplo01/Program.cs:12` | `// Bloquea el hilo. ¡No haga esto en su proyecto!` sobre `Thread.Sleep(1000)` dentro de una `Task` | En `async` se usa `await Task.Delay` |
| `04_Async_0_Ejemplo00/Program.cs:17` | `// termina el main y termina el proceso.` | Trampa: `Main` sin `await` mata la tarea |
| `04_Async_00_Ejemplo00c/...cs:23-26` | Explica que `Thread.Sleep` dentro de un metodo `async` "duerme el hilo principal" | `async` sin `await` no libera nada |
| `04_Async_02_Ejemplo05/...cs:23-24` | `.Result` "no es conveniente en programas de GUI o web" | Bloquear con `.Result` |
| `04_Async_02_Ejemplo04/...cs:21,28` | "Lo correcto es capturar la excepción" y `catch { }` vacio | El `catch` vacio es mala practica incluso en el ejemplo |
| `04_Async_03_00Server/...cs:77-78` | `// revisar que estén todas las excepciones posibles de ocurrir en este contexto...` | El propio docente marca la lista de excepciones del accept como **incompleta** |
| `04_Async_02_Ejemplo00/...cs:13` y `Ejemplo01/...cs:11` | `// <-- ¿Qué pasa si no la iniciamos?` / `// <-- ¿Qué pasó?` | Preguntas abiertas, probable material de parcial. En .NET un `Task` creado con `new Task` y nunca iniciado **no termina** (conocimiento general, inferido) |
| `04_Async_00_Ejemplo01/...cs:44` | "Main se bloquea y cuando la tarea termine..." | Comentario impreciso: el `await` suspende la tarea, no bloquea el hilo (lo dijo el docente en la clase del 22-09) |
| `04_Async_02_Ejemplo01/...cs:5` | `// Warning CS1998 ... lacks 'await'` | Comentario **desfasado**: ese `Main` si tiene `await` (`:14`). El aviso corresponde a `04_Async_0_Ejemplo00/01` y a `ContinueWithExample05` |
| `THREADEX/ThreadException/Program.cs:18-19` | Los escenarios 5 y 6 se rotulan "(Crashes Process)" pero `RunLongThread` (`:124-156`) no lanza ninguna excepcion | Etiqueta incorrecta; el efecto real es que el hilo de fondo muere si `Main` termina |

### Otros defectos que se ven en el codigo
- **`ContinueWithExample05/Program.cs`**: `Main` es `async` pero no hace `await` de las continuations (`:18-24`), asi que el proceso puede terminar sin imprimir nada. Ademas `return 100;` en `:14` es codigo inalcanzable.
- Codigo inalcanzable tras `throw`: `SimpleAsyncExceptionDemo/Program.cs:37`, `SimpleAsyncExceptionDemo1/Program.cs:40`, `ThreadException/Program.cs:114`.
- **`async void`** en `04_Async_00_Ejemplo00`, `00b` y `00c` (`:20`/`:20`/`:19`): no se puede esperar ni atrapar su excepcion. Sirve para mostrar el problema, no para usarlo en EDISON.
- **Bloqueos sincronos**: `.Wait()` en `Desayuno00` (baseline a proposito), `parallel02:22`, `Ejemplo03:30`, `Ejemplo04:26,45,73`, `Ejemplo05_continueWith:34`; `.Result` en `Ejemplo06:55`. En `ThreadPoolExample/Program.cs:67-68` `HttpClient.GetAsync(...).Result` **bloquea un hilo del pool** esperando una operacion asincronica.
- `Ejemplo03/...cs:15-23`: las dos continuations que no aplican (`OnlyOnCanceled`, `OnlyOnFaulted`) nunca corren y quedan canceladas (inferido).
- `ThreadPoolExample` y `Ejemplo02a` dependen de Internet; sin red, fallan por razones ajenas al tema.

### Lo que el Obligatorio 2 prohibe, y donde aparece en el material
La letra: "evitar el uso de la clase `Thread`", `BinaryFormatter` prohibido, y "no se permitira que se utilicen parsers existentes (como puede ser Json)".

| Prohibido | Aparece en | Nota |
|---|---|---|
| Clase `Thread` | `THREADEX/ThreadException/Program.cs:68,103,128` (`new Thread`, `IsBackground`, `Start`) | Es una demo de threads, no de async: no copiar a EDISON |
| `Thread.Sleep` dentro de una `Task` | `ContinueWithExample01-04`, `04_Async_0_Ejemplo00/01`, `02_Ejemplo01`, `00_Ejemplo02a:34` | La letra nombra la **clase** `Thread`. Si `Thread.Sleep` tambien cuenta es **TBD**; lo seguro es `await Task.Delay` |
| `Thread.CurrentThread.ManagedThreadId` | `THREADEX/ThreadPoolExample/Program.cs:60` | `Environment.CurrentManagedThreadId` evita la clase `Thread` y la usan los ejemplos de `Parallel` (`parallel00:27`) |
| `ThreadPool.QueueUserWorkItem` | `ThreadPoolExample/Program.cs:42` | No es la clase `Thread`, pero tampoco es `Task`; la letra pide `Task`. **TBD** si lo toleran |
| `BinaryFormatter` | **No aparece en ningun archivo** | Busqueda por ese nombre sin resultados |
| JSON u otros parsers | **No aparece en ningun archivo** | Busqueda por `Json` sin resultados |

`SimpleTableCRUD.cs` serializa con `Marshal` (ver abajo): no es un parser de texto, pero la letra no dice nada de `Marshal`. **TBD** si lo aceptan.

## Los dos ejemplos de streams (`STREAM/`)

Sin `async`: ambos usan la API **sincronica** (`Read`, `Write`, `ReadLine`), sin `ReadAsync` ni `WriteAsync`. Son del Tema 1 ([[Complementario - Introduccion y Streams]]).

### `FileStreamTextMenu.cs` - modos de apertura
Menu de 11 opciones que recorre una por una las combinaciones de `FileMode`:
- `CreateNew` falla si existe (`:219-241`); `Create` reemplaza (`:247-269`); `Open` falla si no existe (`:275-306`); `OpenOrCreate` (`:312-350`, con `stream.Position = stream.Length` en `:342` para escribir al final); `Truncate` pide confirmacion antes (`:356-394`); `Append` (`:400-422`).
- `FileAccess.Read/Write/ReadWrite` segun el caso.
- El texto se escribe **como bytes**: `Encoding.UTF8.GetBytes(line + Environment.NewLine)` y `stream.Write(data, 0, data.Length)` (`:185-187`). Se lee con `StreamReader.ReadLine` (`:203-210`).
- La opcion 9 sobrescribe a partir de una **posicion en bytes** con `stream.Position = position` (`:521-523`) y avisa que **no inserta, pisa** (`:529-532`). Una posicion que cae en medio de un caracter UTF-8 de varios bytes lo corromperia (inferido).
- `using` en cada `FileStream`; un `try/catch` central muestra `ex.Message` (`:19-79`).
- Defectos menores: `SelectTextFileExample` esta definida pero nunca se llama (`:539-548`); la opcion 10 llama a `SelectTextFile()` y descarta el resultado (`:59-61`).

### `SimpleTableCRUD.cs` - registros binarios de tamano fijo
- Un `struct Record` con `[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]` y dos cadenas de **largo fijo 20** (`[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]`): `int ID`, `Name`, `LastName`, `float Salary` (`:16-28`). Con 1 byte por caracter ANSI son **48 bytes por registro** (calculado: 4+20+20+4, el programa no lo imprime).
- El texto se valida a 19 caracteres y se rellena con `'\0'` (`:82-98`).
- Conversion `Record` <-> `byte[]` con `Marshal.AllocHGlobal`, `StructureToPtr`, `Copy`, `PtrToStructure` y `FreeHGlobal` (`:137-153,433-471`).
- **El numero de registro es la posicion**: `position = recordNumber * size` y `fs.Seek(position, SeekOrigin.Begin)` (`:195,205,265,275`). Lectura y modificacion saltan directo al registro sin recorrer el archivo. `Append` agrega (`:130-133`) y `ListRecords` recorre secuencialmente (`:392-416`).
- `Modify` abre `ReadWrite` con un `BinaryReader` y un `BinaryWriter` sobre el mismo `FileStream` (`:257-262`).
- Detecta archivo truncado: `fs.Length % size != 0` avisa (`:369-373`); `position + size > fs.Length` -> "el registro no existe" (`:198,268`).

Limites que se ven: **no hay opcion de borrar** (el menu es escribir, leer, modificar, listar; `:38-42`) aunque se llama "CRUD"; el `ID` no se valida como unico; el `fDeleteOld: true` de `Marshal.StructureToPtr` (`:147,441`) sobre memoria recien reservada es un patron riesgoso (inferido); `CharSet.Ansi` limita los caracteres que entran (inferido); varios `int.Parse(Console.ReadLine())` sin `TryParse` (`:108,177,246,303`) quedan cubiertos solo por el `catch` general.

### Como se relacionan con transferencia de archivos y persistencia
- **Transferencia**: las primitivas son las mismas que usa `FileStreamHelper` en [[Ejemplos de codigo de sockets de clase]] (`Read(path, offset, length)` y `Write`): `FileStream`, `Position`/`Seek`, `Read`/`Write(buffer, offset, count)` y `using`. Posicionar y escribir un bloque de tamano conocido en una posicion es lo que se hace con cada parte de 32 KB. Ver [[Clase 2026-09-15 - Transferencia de archivos por chunks e introduccion a asincronia]].
- **Migracion a `async`**: ninguno de los dos usa `ReadAsync`/`WriteAsync`. Para el Obligatorio 2 serian la version `async` de lo mismo (`FileStream.ReadAsync/WriteAsync`, tambien con `CancellationToken`). **Propuesta (inferido)**: el material no trae el ejemplo.
- **Persistencia sin parsers**: es el ejemplo de la catedra de guardar datos sin JSON ni `BinaryFormatter`: tamano fijo + posicion = acceso directo. Para el Obligatorio 2 los **datos (no las imagenes)** van a una base MySQL o equivalente en Docker (letra, punto 3), asi que este ejemplo sirve de modelo para lo que **sigue** en archivos: imagenes y comprobantes. Es el mismo principio de campos de largo fijo del encabezado `TIPO(3) + LARGO(4)` del protocolo de sockets (analogia propia, inferido).

## Huecos del material
- **No hay ejemplo de cancelar una tarea individual** (la reserva pendiente del requisito 5): todos los ejemplos cancelan "todo el servidor" o "una sola tarea de demo".
- **No hay `ReadExactly` ni loop de acumulacion** en el codigo `async` (el bug de `Receive` parcial se repite).
- **No hay base de datos ni docker-compose** en los ejemplos de Docker.
- **No hay ejemplo de archivos grandes por red con `async`**: los de transferencia son sincronicos en `Socket`.
- **No hay configuracion por entorno** pese a que la letra la pide para reemplazar los archivos de configuracion.
- Los docentes no muestran **como migrar** un servidor de `Thread` a `Task` paso a paso; el ejemplo `03` parte de cero.

## Connections
- [[Tema 3 - Asincronia y paralelismo]] - la teoria que estos ejemplos ilustran
- [[Concurrencia de Thread a Task]] - la sintesis del paso de `Thread` a `Task`
- [[Ejemplos de codigo de sockets de clase]] - nota hermana: el servidor con `Thread` por cliente que `04_Async_03` reemplaza
- [[Complementario - Docker y Cloud]] - contexto de Docker del curso
- [[Complementario - Introduccion y Streams]] - los streams que usan los dos ejemplos de `STREAM/`
- [[Obligatorio 2 - Upgrade asincrono y Docker de EDISON]] - la consigna que estos ejemplos preparan
- [[Clase 2026-09-17 - Task en detalle, continuations, ThreadPool y TPL]] - la clase donde se dieron `ContinueWith`, excepciones y ThreadPool
- [[Clase 2026-09-22 - Parallel, CancellationToken y cuando usar async]] - la clase de `Parallel` y cancelacion
- [[Clase 2026-09-15 - Transferencia de archivos por chunks e introduccion a asincronia]] - transferencia por partes y arranque del tema
- [[Clase 2026-10-08 - Letra del Obligatorio 2 y patron RPC sobre RabbitMQ]] - la letra del Obligatorio 2 explicada en clase
