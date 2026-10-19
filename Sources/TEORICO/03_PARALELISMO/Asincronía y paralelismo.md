Introducción a
paralelismo y asincronía

Sumario • Fundamentos del Paralelismo
• La librería Task Parallel vs Thread Pool
• Diferentes formas de definir tareas.
• Programación asíncrona. Fundamentos
• Sincronizar tareas (Task.WaitAll vs Task.WhenAll)
• CancellationToken y cómo usarla
• Consideraciones al usar async/await
• Ejecutar tareas en paralelo (Pararell.For, ForEach, Paralell
LINQ)
• Task vs Threads
• Diseño de funciones asincrónicas

• El término paralelismo se refiere a técnicas para hacer que los
programas sean más rápidos al realizar MÚLTIPLES TAREAS al
MISMO TIEMPO.
Paralelismo.
Fundamentos

• Thread pooling
Refiere al proceso de crear una colección de threads en una
aplicación y reutilizarlos según convenga.
Paralelismo.
Fundamentos
https://www.c-sharpcorner.com/UploadFile/1d42da/threading-pooling-in-C-Sharp/

• System.Threading.Thread
• Multithreading se refiere a la ejecución concurrente/paralela
de uno o más hilos o instrucciones.
• Pero…. es muy costosa computacionalmente
Paralelismo.
Fundamentos

• ThreadPool.QueueUserWorkItem
• Asynchronous delegate
• Background worker
• Task Parallel Library (TPL)
Paralelismo.
Fundamentos

• Task Parallel Library
• Ofrece formas más eficientes de generar y ejecutar Threads: el Código se
escribe como una secuencia de tareas a pesar de ser asíncrono.
• Detrás de escena, los objetos de tipo Task se encolan en un thread pool.
• Este thread pool es una forma más eficiente de manejar los hilos que la
tradicional.
Task Parallel
vs
Thread Pool

Beneficios de Tasks:
• Auto-tuning del ThreadPool
• Mas control:
• Status
• Return values
Tasks
• Cancellation
• Exception handling
• Scheduling
• Continuation

¿Qué pasa
aquí?

¿Qué pasa
aquí?

¿Como hacer para
que continúe luego
de terminada la
tarea interna?

Diferentes
formas de
usar la clase
Task

Delay: equivalente asíncrono del Thread.Sleep
Diferentes
formas de
usar la clase
Task

Diferentes
formas de
usar la clase
Task

• async
• Para métodos que retornan: void(*), Task o Task<Result>
• async no cambia la declaración del método
• Le indica al compilador que trate await como palabra
reservada en lugar de como un identificador, similar a
unsafe.
async &
• await:
await • Punto de posible suspensión/reanudación
• No necesita el uso de Start.
(*) no hacer hacer async void

Programación
asíncrona.
Fundamentos
https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/

Programación
asíncrona.
Fundamentos
¿Si el código anterior demoraba 30 min. en
hacer el desayuno, cuanto demora este?

Programación
asíncrona.
Fundamentos

Programación
asíncrona.
Fundamentos

Concurrencia
vs
¿Cual es la diferencia?
Paralelismo
vs
Vamos a ver algunos autores
Asincronía primero …

"concurrency is the composition of independently
executing processes*, while parallelism is the
simultaneous execution of (possibly related)
computations. Concurrency is about dealing with lots
of things at once. Parallelism is about doing lots of
things at once"
— Rob Pike (co-creador de Go, Conferencia
"Concurrency is not parallelism" 2013)
* "proceso" como sinónimo de tarea o función

"Concurrency is about structure, parallelism is about
execution.
Concurrency provides a way to structure a solution to
solve a problem that may (but not necessarily) be
parallelizable."
— Rob Pike (co-creador de Go, Conferencia
"Concurrency is not parallelism" 2013)

"Concurrency is when the execution of two or more pieces of
code act as if they run at the same time.
Parallelism is when they do run at the same time."
— The Pragmatic Programmer Capitulo 6 Concurrency

• Concurrencia y paralelismo están
relacionados con tareas que se ejecutan
simultáneamente PERO
Concurrencia
• Paralelismo se refiere a tareas que se
ejecutan al mismo tiempo. Las tareas están
vs
activas al mismo tiempo.
Paralelismo
• Concurrencia se refiere a que las tareas se
ejecutan en el mismo periodo de tiempo.
Las tareas están "lógicamente" activas la
mismo tiempo

"Asynchronous programming
A form of concurrency that uses futures or
callbacks to avoid unnecessary threads
Asincronia A future (or promise) is a type that represents
some operation that will complete in the
future.[...]it doesn’t block the original thread"
— Stephen Cleary, Concurrency in C#
Cookbook, O'Reilly Media

1. Un servidor web atiende múltiples solicitudes de
usuarios. Se comunica con la BD y API de terceros.
2. Una aplicación de escritorio descarga un archivo
mientras el usuario sigue usando la interfaz.
Ejemplos
3. Entrenar un modelo de machine learning
Concurrencia
4. Una app de edición de fotos aplica filtros a varias
imágenes
Paralelismo
5. Una app de mensajería revisa nuevos mensajes,
Asincronía
actualiza el estado de contactos y sincroniza archivos al
mismo tiempo.
6. Multiplicar matrices

Lo que ocurre
en un método
asincrónico
https://learn.microsoft.com/es-es/dotnet/csharp/asynchronous-programming/task-asynchronous-pr
ogramming-model#BKMK_WhatHappensUnderstandinganAsyncMethod

"Doing lots of work by dividing it up among
multiple threads that run concurrently.
Procesamiento
[it] uses multithreading to maximize the use of
multiple processor cores.
en paralelo
— Stephen Cleary, Concurrency in C#
Cookbook, O'Reilly Media

Paralelismo.
Parallel.For

Paralelismo.
Parallel.ForEach

Paralelismo.
ParallelLINQ

Paralelismo.
ParallelLINQ

• Indica que una operación debe ser
cancelada
Cancellation
Token

• Procesos que involucran I/O
• Tareas que involucran alto procesamiento
de recursos
Escenarios
comunes
donde aplica la
programación
asíncrona.

• Todo método que se defina con async debe
tener un await en su cuerpo.
•
async void se usa solo en los eventos (ej:
button click)
• Se debe escribir código que espere por
| Notas  | tareas de forma no bloqueante. |     |     |
| ------ | ------------------------------ | --- | --- |
importantes
|     | Usar: | En vez de: | Cuando se quiera: |
| --- | ----- | ---------- | ----------------- |
sobre el uso de
|     | await | Task.Wait o Task.Result | Devolver el resultado  |
| --- | ----- | ----------------------- | ---------------------- |
de una background task
| async/await | await Task.WhenAny | Task.WhenAny | Esperar que se  |
| ----------- | ------------------ | ------------ | --------------- |
complete alguna tarea
en una lista
|     | await Task.WhenAll | Task.WaitAll | Esperar que todas las  |
| --- | ------------------ | ------------ | ---------------------- |
tareas de una lista se
complete
|     | await Task.Delay | Thread.Sleep | Esperar durante un  |
| --- | ---------------- | ------------ | ------------------- |
interval de tiempo

• Añadir Async al final del nombre de un
método async - Convención en .NET (depende
del lenguaje)
• Cuidado con usar .ConfigureAwait(false/true)
sin saber el por qué.
Notas
importantes
sobre el uso de
async/await

• La clase Thread se usa para crear y manipular recursos
del Sistema Operativo. Una Task representa una
operación asíncrona y forma parte de la librería
Task.Parallel que ofrece un conjunto de APIs para
ejecutar tareas de forma asíncrona y en paralelo.
• Una tarea puede devolver un resultado. No hay un
Threads
mecanismo directo de devolver un resultado desde un
Thread.
vs • Las tareas soportan cancelación con el uso de
Cancellation Tokens. Los hilos no.
• Una tarea puede tener múltiples procesos sucediendo
Tasks
al mismo tiempo. Los Threads solo pueden tener una
tarea ejecutándose a la vez.
• Es muy fácil implementar tareas asíncronas usando
async/await
• Una Task es un concepto de alto nivel, un Thread no.

• Escriba su método sincrónicamente
• Reemplace las llamadas a métodos
sincrónicos con llamadas a métodos
asincrónicos y esperarlas (await).
Diseño de
• A excepción del controlador de eventos de
funciones
nivel superior (top level event handler),
actualice el tipo de retorno de su método
asincrónicas
asíncrono a Task o Task<Result> para que
esté disponible.