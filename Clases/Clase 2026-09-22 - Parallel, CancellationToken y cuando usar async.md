---
date: 2026-09-22
type: class-note
tags:
  - pdr
  - clase
  - asincronia
  - paralelismo
  - cancellationtoken
ai-first: true
confidence: high
duracion: "~53 min (grabacion cortada en el descanso)"
fuente: "Grabacion del showcase de Vimeo 12367503, video 1229279012, subtitulos autogenerados es-x-autogen"
---

# Clase 2026-09-22 - Parallel, CancellationToken y cuando usar async

## For future agent
Acta de la clase del **2026-09-22** (martes), la que faltaba entre el 17-09 y el 24-09. Continua [[Tema 3 - Asincronia y paralelismo]]: asincronia vs paralelismo, la clase `Parallel` (`For`, `ForEach`, `Invoke`), PLINQ, **`CancellationToken`** completo con buenas practicas y tokens enlazados, y cuando conviene o no usar `async`. **La grabacion termina alrededor del minuto 52, justo cuando el docente anuncia un descanso de 5 minutos**: la nota cubre solo la primera parte de la clase y no hay segundo video en el showcase (as of 2026-10-13). Hablan dos docentes, [[Enrique Latorres]] y presumiblemente [[Natalia Franco]] (sin confirmar). Escrita desde transcripcion automatica: nombres de metodos y cifras pueden estar mal transcriptos y van marcados.

Esta clase es materia del **Obligatorio 2** (asincronia y cancelacion), no del 1: en el Obligatorio 1 `Task` esta prohibido. Ver [[Clase 2026-10-08 - Letra del Obligatorio 2 y patron RPC sobre RabbitMQ]].

## Asincronia no es paralelismo
- La **concurrencia** alterna entre tareas; el **paralelismo** es hacer dos cosas literalmente a la vez. Dijo que la asincronia **no significa que haya paralelismo**: da la *percepcion* de tareas simultaneas, y lo valioso es **dejar una tarea andando, hacer otra cosa y pedir el resultado despues**. (stated)
- En maquinas modernas hay **las dos cosas a la vez**: varios nucleos y cada nucleo saltando entre tareas. La programacion asincrona es **una abstraccion arriba de hilos** del sistema. (stated)
- La ganancia: como el hilo **no se bloquea** en la E/S, no hay cambio de contexto y se aprovecha mas el procesador. Un hilo de asincronia parece un hilo de alto uso de CPU. Si el sistema esta cargado, igual el quantum se lo quita y se lo devuelve. (stated)

## Ejemplo de `HttpClient`
- `client.GetStringAsync(url)` devuelve un `Task<string>`. Mientras baja la pagina se hace trabajo independiente, y despues el `await`: si ya termino, sigue; si no, espera. (stated)
- **Convencion de nombres**: muchas bibliotecas traen metodo sincrono y asincrono; hay que **buscar el asincrono**, que lleva `Async` al final. (stated)
- **El `await` bloquea la tarea, no el hilo.** Si hay otras tareas, el hilo sigue ejecutando segmentos de ellas. (stated)

## La clase `Parallel`
- **`Parallel.ForEach`**: un `foreach` en paralelo sobre una coleccion.
- **`Parallel.For`**: como el `for`, el rango es **cerrado a la izquierda y abierto a la derecha**: `To exclusive`. El docente dudo un momento y lo verifico en clase: de 0 a 10 corre 0..9. (stated)
- Ejemplo del tamano de un directorio: `Parallel.For` sobre `Directory.GetFiles(args[0])`, cada iteracion suma el tamano de un archivo en `totalSize`. Como todos los hilos suman a la vez, hace falta exclusion mutua: **`Interlocked.Add`**. (stated)
- Ejemplo con `Parallel.ForEach` haciendo **ping** a una lista de sitios, tambien con `Interlocked`, y comparado con un `foreach` normal que escribe direccion, tiempo y estado.
- **`Parallel.Invoke`**: lanza varias tareas en paralelo, cada una con su parametro (`DoTask` con nombre y milisegundos: A 1000, B 1500, C 500). Muestra el **ID del hilo** con `Environment.CurrentManagedThreadId` (transcripto "Google Manager Friend ID", correccion probable). En maquina liviana casi todas corren en el mismo hilo o en hilos reutilizados del ThreadPool; con carga pueden repartirse y una tarea puede terminar en otro hilo que el que la empezo. (stated)
- **Cuidado en el delay dentro de `Parallel.Invoke`**: el metodo no es `async`, asi que no se hace `await Task.Delay`; se espera de forma sincrona sobre el `Task.Delay` (la transcripcion dice "task.delay.await", TBD cual era la forma exacta, presumiblemente `.Wait()`).
- **Nivel**: `Parallel` es **de mas bajo nivel que `Task`**; esta pensado para procesos continuos **relativamente cortos**. Usa ThreadPool. (stated)
- **PLINQ**: una consulta paralela sobre datos en memoria; el ejemplo cuenta los numeros pares (modulo 2 igual a cero) en paralelo. El docente pregunto si conocian LINQ y la respuesta fue que no: es para consultas sobre datos en memoria. (stated)

## `CancellationToken`
**Que es.** Un mecanismo de .NET para decirle a una tarea u operacion que **pare**. Casos que nombraron: cerrar servicios ("mucho de lo que vamos a usar en el obligatorio"), cortar un bucle de larga duracion, cancelar operaciones en segundo plano, **coordinar el cierre de varias tareas** pasandoles el mismo token. (stated)

**Pasos.**
1. Crear un `CancellationTokenSource` (`cts`).
2. Sacar el token: `cts.Token`.
3. Pasarlo por parametro a la tarea (`Task.Run(..., token)` y al metodo).
4. Dentro de la tarea, **comprobar**: `token.IsCancellationRequested` en un bucle, o `token.ThrowIfCancellationRequested()`.
5. Para pedir la cancelacion: `cts.Cancel()`.

- **El source es una fabrica de tokens.** Se pueden generar varios tokens y mandarlos a distintas tareas; un solo `Cancel()` los activa todos. (stated)
- **Un token cancelado queda cancelado: no se reutiliza.** Hay que crear otro. (stated)
- **Metodos asincronos de bibliotecas** (por ejemplo un `Accept` o un `Read` de `TcpListener`/`TcpClient` asincronos) aceptan el token de forma opcional. Si estan bloqueados esperando, **la cancelacion los saca con una excepcion** (`OperationCanceledException`, transcripto "cancel exception"). Sin eso quedarian esperando un paquete que quiza nunca llegue. (stated)
- **`Task.Delay(1000, token)`**: si se cancela antes del segundo, lanza excepcion y sale del delay. (stated)

**Buenas practicas** (de las diapositivas, leidas y comentadas):
- Usarlo en tareas **de larga duracion o asincronas**.
- **Comprobar el token en los bucles.** `ThrowIfCancellationRequested` lanza la excepcion; los metodos de biblioteca cancelables ya lo hacen solos.
- **`catch` de `OperationCanceledException`** para manejar la cancelacion sin que explote.
- **No asumir que la tarea termina al instante.** Una tarea no se entera hasta que llega a una verificacion; puede tardar. Escribir el codigo para que tarde lo menos posible es responsabilidad del programador. (stated)
- **Esperar a que terminen.** Patron para el servidor: guardar todos los `client handler` en una lista, al cerrar mandar la cancelacion a todos y **`Task.WhenAll`** para esperar que cada uno haya terminado; recien ahi cerrar el servidor y **persistir los datos**. (stated)
- Cancela **de forma ordenada**, no abrupta como `Thread.Abort`: permite cerrar sockets y guardar datos de forma consistente. (stated)
- Dicho por la segunda docente, **dudoso**: que pertenece a `System.Threading` y que en Rider hay que agregar un NuGet. TBD verificar; no confirmado por el material.

**Tokens enlazados (`linked token`).** Caso que **no esta en las diapositivas** y el docente agrego a mano:
- El servidor tiene una tarea listener con un `while`: hace `Accept`, y por cada cliente un `Task.Run` con el `TcpClient` y el token. Otro hilo atiende la consola: si se escribe el comando de salida hace `Cancel()` y todos los handlers se enteran.
- Pero un `client handler` puede tener a su vez **otra razon de cancelar**: el cliente remoto pide un informe largo y despues manda un mensaje "ya me aburri, cancelalo". Eso cancela **solo esa tarea**, no el servidor entero.
- Solucion: dentro del handler se crea un **nuevo `CancellationTokenSource` vinculado al token recibido**, y se le pide el token nuevo. Ese token nuevo se cancela **si cancela el servidor o si el cliente cancela el informe**; cancelar el local no cancela el de arriba. Se puede anidar a varios niveles. (stated)
- El nombre que dio al final: **Linked Cancellation Token** (en la API, `CancellationTokenSource.CreateLinkedTokenSource`; el docente no recordaba el metodo y dijo que despues pone un ejemplo o que se lo pregunten a la IA). (stated)
- Distinguio el **timeout** (el cliente se cansa de esperar) del caso de "ya no me interesa". (stated)

## Cuando usar `async` y cuando no
- La segunda docente aviso que **no hay que poner todo `async` por reflejo**. Se usa en procesos de **entrada/salida** (usuario, base de datos, red) y en tareas de **alto procesamiento**. En un metodo que suma o hace una cuenta corta **no tiene sentido**. En el Obligatorio 2 hay que decidir por metodo. (stated)
- Ejemplo: descargar una pagina con una API, guardar el HTML y contar cuantas veces aparece ".NET".

## Avisos
- Al arranque pidio **ver el `TcpClient` y el `TcpListener` en formato asincrono** ("esas dos armas") y **terminar la resolucion de un papel/practico**; la transcripcion es confusa, TBD de que material se trataba.
- Primero pregunto por el avance del grupo; nada sobre fechas nuevas.

## Salvedades de la transcripcion
- Grabacion cortada al minuto ~52 (descanso anunciado). No consta que se haya grabado la segunda mitad.
- `Task.Delay(...).Wait()` y `Environment.CurrentManagedThreadId` son correcciones probables de lo transcripto.
- No se registra el pasaje de lista ni nombres de estudiantes.

## Connections
- [[Tema 3 - Asincronia y paralelismo]] · [[Concurrencia de Thread a Task]] · [[Reglas y prohibiciones del curso PDR 2026]]
- [[Clase 2026-09-17 - Task en detalle, continuations, ThreadPool y TPL]] - la clase anterior; ya traia una primera pasada de `CancellationToken`.
- [[Clase 2026-09-24 - Middleware orientado a mensajes, RabbitMQ y AMQP]] - la siguiente.
- [[Programacion de Redes 2026]]
