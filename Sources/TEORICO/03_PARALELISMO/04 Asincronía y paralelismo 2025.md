<!-- Slide number: 1 -->

# Introducción a paralelismo y  asincronía

### Notes:

<!-- Slide number: 2 -->

# Sumario
Fundamentos del Paralelismo
La librería Task Parallel vs Thread Pool
Diferentes formas de definir tareas.
Programación asíncrona. Fundamentos
Sincronizar tareas (Task.WaitAll vs Task.WhenAll)
CancellationToken y cómo usarla
Consideraciones al usar async/await
Ejecutar tareas en paralelo (Pararell.For, ForEach, Paralell LINQ)
Task vs Threads
Diseño de funciones asincrónicas

### Notes:

<!-- Slide number: 3 -->

# Paralelismo. Fundamentos
El término paralelismo se refiere a técnicas para hacer que los programas sean más rápidos al realizar MÚLTIPLES TAREAS al MISMO TIEMPO.

![](GoogleShape113p15.jpg)

![](GoogleShape114p15.jpg)

### Notes:
1. Concepto de paralelismo. Dibujar muchas tareas para un procesador y las tareas distribuidas para varios

<!-- Slide number: 4 -->
Thread pooling
Refiere al proceso de crear una colección de threads en una aplicación y reutilizarlos según convenga.

# Paralelismo. Fundamentos

![](GoogleShape123p16.jpg)
https://www.c-sharpcorner.com/UploadFile/1d42da/threading-pooling-in-C-Sharp/

### Notes:
The class System.Threading.Thread is the most basic way to run a thread, but it comes with a cost. The creation and destruction of threads incur high resource utilisation, mainly CPU overhead. To avoid this penalty, which can be expensive in terms of performance when threads are being created and destroyed extensively, .NET has presented the ThreadPool class. This class allocates a certain number of threads and manages them for you.
Although ThreadPool has advantages, in some scenarios, it is better to stick the good old Thread creation practice. It is especially relevant when you need finer control on the thread execution. For example, setting the thread to be a foreground thread; the ThreadPool instantiates only background threads, so you should use Thread object when a foreground thread is required (read here about the meaning of background and foreground threads). Other scenarios are setting the thread’s priority, or aborting a specific thread. This low-level control cannot be done if you use a ThreadPool object.

<!-- Slide number: 5 -->

# Paralelismo. Fundamentos
System.Threading.Thread
Multithreading se refiere a la ejecución concurrente/paralela de uno o más hilos o instrucciones.
Pero…. es muy costosa computacionalmente

![](GoogleShape133p17.jpg)

### Notes:
The class System.Threading.Thread is the most basic way to run a thread, but it comes with a cost. The creation and destruction of threads incur high resource utilisation, mainly CPU overhead. To avoid this penalty, which can be expensive in terms of performance when threads are being created and destroyed extensively, .NET has presented the ThreadPool class. This class allocates a certain number of threads and manages them for you.
Although ThreadPool has advantages, in some scenarios, it is better to stick the good old Thread creation practice. It is especially relevant when you need finer control on the thread execution. For example, setting the thread to be a foreground thread; the ThreadPool instantiates only background threads, so you should use Thread object when a foreground thread is required (read here about the meaning of background and foreground threads). Other scenarios are setting the thread’s priority, or aborting a specific thread. This low-level control cannot be done if you use a ThreadPool object.

<!-- Slide number: 6 -->

# Paralelismo. Fundamentos
ThreadPool.QueueUserWorkItem
Asynchronous delegate
Background worker
Task Parallel Library (TPL)

![](GoogleShape142p18.jpg)

### Notes:
Diferentes formas de trabajar con thread pool en C#

Using the ThreadPool class is the classic clean approach to create a thread in a thread; calling ThreadPool.QueueUserWorkItem method will create a new thread and add it to a queue. After queuing the work-item, the method will run when the thread pool is available; if the thread pool is occupied or reach its upper limit, the thread will wait.
The ThreadPool object can be limit the number of threads running under it; it can be configured by calling ThreadPool.SetMaxThreadmethod. The execution of this method is ignored(return False) if the parameters are lesser than the number of CPUs on the machine. To obtain the number of CPUs of the machine, call Environment.ProcessorCount.
The BackgroundWorker class (under System.ComponentModel namespace) uses thread pool too. It abstracts the thread creation and monitoring process when exposing events that report the thread’s process, which makes this class suitable for GUI responses; for example, reflecting a long-running process that executes in the background.

<!-- Slide number: 7 -->

# Task Parallel   vs Thread Pool
Task Parallel Library
Ofrece formas más eficientes de generar y ejecutar Threads: el Código se escribe como una secuencia de tareas a pesar de ser asíncrono.
Detrás de escena, los objetos de tipo Task se encolan en un thread pool.
Este thread pool es una forma más eficiente de manejar los hilos que la tradicional.

![](GoogleShape151p19.jpg)

### Notes:
Una tarea o “task” es una tarea potencialmente paralelizable. El Framework decide si debemos  o no realizar en forma paralela (usando threads de forma transparente) para completer la tarea en curso.
nos evitamos inicializar de forma explicita hilos con el consiguiente ahorro de procesamiento
Utilizando un threadpool especial: el del CLR ( el mismo que se usa para QueueWorkerUseItem, mejorado a partir de la version 4.0 del framework).
The Task asynchronous programming model (TAP) provides an abstraction over asynchronous code. You write code as a sequence of statements, just like always. You can read that code as though each statement completes before the next begins. The compiler performs many transformations because some of those statements may start work and return a Task that represents the ongoing work.
In short, the Task Parallel Library (TPL) provides more efficient ways to generate and run threads than the traditional Thread class. Behind the scenes, Tasks objects are queued to a thread pool. This thread pool is enhanced with algorithms that determine and adjust the number of threads to run, and provide load balancing to maximise throughput. These features make the Tasks relatively lightweight and handle effectively threads than before.

<!-- Slide number: 8 -->

# Tasks
Beneficios de Tasks:
Auto-tuning del ThreadPool
Mas control:
Status
Return values
Cancellation
Exception handling
Scheduling
Continuation

![](GoogleShape160p20.jpg)

### Notes:
scheduler para gestionar las tareas
continuation (concatenar)pueden devolver valores…..claro antes podian tomar una variable y modificarla.

<!-- Slide number: 9 -->

![](GoogleShape168p21.jpg)
# ¿Qué pasa aquí?

![](GoogleShape169p21.jpg)

### Notes:
C# task is one of the central elements of the task-based asynchronous pattern first introduced in the .NET Framework 4.
C# task object typically executes asynchronously on a thread pool thread rather than synchronously on the main application thread.
Although, we can directly offload the work to thread pool using the queue user work item method. However, this method has its weaknesses as we can’t tell whether the operation has finished or what a return value is.
This is where a C# task can be helpful. The C# task can tell you if the work is completed and if the operation returns a result. A Task is an object that represents some work that should be done.
TaskFactory nos permite inicializar rapidamente Tasks que permiten ejecucion en paralelo si asi fuera necesario.

<!-- Slide number: 10 -->

![](GoogleShape179p22.jpg)
# ¿Qué pasa aquí?

![](GoogleShape180p22.jpg)

### Notes:
C# task is one of the central elements of the task-based asynchronous pattern first introduced in the .NET Framework 4.
C# task object typically executes asynchronously on a thread pool thread rather than synchronously on the main application thread.
Although, we can directly offload the work to thread pool using the queue user work item method. However, this method has its weaknesses as we can’t tell whether the operation has finished or what a return value is.
This is where a C# task can be helpful. The C# task can tell you if the work is completed and if the operation returns a result. A Task is an object that represents some work that should be done.
TaskFactory nos permite inicializar rapidamente Tasks que permiten ejecucion en paralelo si asi fuera necesario.

<!-- Slide number: 11 -->

![](GoogleShape189p23.jpg)
# ¿Como hacer para que continúe luego de terminada la tarea interna?

![](GoogleShape190p23.jpg)

### Notes:
C# task is one of the central elements of the task-based asynchronous pattern first introduced in the .NET Framework 4.
C# task object typically executes asynchronously on a thread pool thread rather than synchronously on the main application thread.
Although, we can directly offload the work to thread pool using the queue user work item method. However, this method has its weaknesses as we can’t tell whether the operation has finished or what a return value is.
This is where a C# task can be helpful. The C# task can tell you if the work is completed and if the operation returns a result. A Task is an object that represents some work that should be done.
TaskFactory nos permite inicializar rapidamente Tasks que permiten ejecucion en paralelo si asi fuera necesario.

<!-- Slide number: 12 -->

![](GoogleShape198p24.jpg)
# Diferentes formas de usar la clase Task

![](GoogleShape200p24.jpg)

![](GoogleShape199p24.jpg)

### Notes:
C# task is one of the central elements of the task-based asynchronous pattern first introduced in the .NET Framework 4.
C# task object typically executes asynchronously on a thread pool thread rather than synchronously on the main application thread.
Although, we can directly offload the work to thread pool using the queue user work item method. However, this method has its weaknesses as we can’t tell whether the operation has finished or what a return value is.
This is where a C# task can be helpful. The C# task can tell you if the work is completed and if the operation returns a result. A Task is an object that represents some work that should be done.
TaskFactory nos permite inicializar rapidamente Tasks que permiten ejecucion en paralelo si asi fuera necesario.

<!-- Slide number: 13 -->

![](GoogleShape208p25.jpg)
# Diferentes formas de usar la clase Task
Delay: equivalente asíncrono del Thread.Sleep

![](GoogleShape209p25.jpg)

### Notes:
Task.Delay espera o It will create a task that completes after a specified number of milliseconds.
Task.Wait
If we want to make the Main thread execution wait until the other tasks complete their execution, we can achieve that by using Task.Wait method. The Wait method will block other executions until the assigned task has completed its execution.

Following is the example of using the Wait() method to make the program execution wait unconditionally until the task completes its execution.

<!-- Slide number: 14 -->

![](GoogleShape218p26.jpg)
# Diferentes formas de usar la clase Task

### Notes:
Task.Continue
In C#, you can create a continuation by calling the ContinueWith method that is going to execute when its antecedent has completed its execution.
In the following example, the antecedent task i.e. task1 return an integer value. When it completes its executions, then it passes that value to the continuation task and that continuation task does some operation and returns the result as a string.
Output: The Square of 12 is : 144
Las Tasks nos permiten establecer ordenes de ejecución. Esto se logra indicando si una tarea ejecuta a continuación de otra.

<!-- Slide number: 15 -->

# Diferentes formas de usar la clase Task

![](GoogleShape226p27.jpg)

### Notes:
Task.Continue
In C#, you can create a continuation by calling the ContinueWith method that is going to execute when its antecedent has completed its execution.
In the following example, the antecedent task i.e. task1 return an integer value. When it completes its executions, then it passes that value to the continuation task and that continuation task does some operation and returns the result as a string.
Output: The Square of 12 is : 144
Las Tasks nos permiten establecer ordenes de ejecución. Esto se logra indicando si una tarea ejecuta a continuación de otra.

<!-- Slide number: 16 -->

![](GoogleShape235p28.jpg)
# Diferentes formas de usar la clase Task

![](GoogleShape234p28.jpg)

![](GoogleShape236p28.jpg)

![](GoogleShape237p28.jpg)

### Notes:
Task.Result

Task.FromResult (https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.fromresult?view=net-8.0) se usa cuando queres directamente devolver un valor en forma de tarea, aunque no haya una llamada asincronica, ej. I/O

<!-- Slide number: 17 -->

async
Para métodos que retornan: void(*), Task o Task<Result>
async no cambia la declaración del método
Le indica al compilador que trate await como palabra reservada en lugar de como un identificador, similar a unsafe.
await:
Punto de posible suspensión/reanudación
No necesita el uso de Start.

(*) no hacer hacer async void
# async & await

### Notes:
el obligatorio no van a tener un new thread…deberia ser todo con tasky cambiar el new socket por tcp listener y tcp client

<!-- Slide number: 18 -->

# Programación asíncrona. Fundamentos

![](GoogleShape253p30.jpg)

![](GoogleShape254p30.jpg)
https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/

### Notes:
If you have experience with cooking, you'd execute those instructions asynchronously. You'd start warming the pan for eggs, then start the bacon. You'd put the bread in the toaster, then start the eggs. At each step of the process, you'd start a task, then turn your attention to tasks that are ready for your attention.
Cooking breakfast is a good example of asynchronous work that isn't parallel. One person (or thread) can handle all these tasks. Continuing the breakfast analogy, one person can make breakfast asynchronously by starting the next task before the first completes. The cooking progresses whether or not someone is watching it. As soon as you start warming the pan for the eggs, you can begin frying the bacon. Once the bacon starts, you can put the bread into the toaster.
For a parallel algorithm, you'd need multiple cooks (or threads). One would make the eggs, one the bacon, and so on. Each one would be focused on just that one task. Each cook (or thread) would be blocked synchronously waiting for bacon to be ready to flip, or the toast to pop.
Computers don't interpret those instructions the same way people do. The computer will block on each statement until the work is complete before moving on to the next statement. That creates an unsatisfying breakfast. The later tasks wouldn't be started until the earlier tasks had completed. It would take much longer to create the breakfast, and some items would have gotten cold before being served.
If you want the computer to execute the above instructions asynchronously, you must write asynchronous code.

<!-- Slide number: 19 -->

![](GoogleShape263p31.jpg)
# Programación asíncrona. Fundamentos
¿Si el código anterior demoraba 30 min. en hacer el desayuno, cuanto demora este?

### Notes:
Explicar tareaas no bloqueantes
 explicar el uso de async y await
Explicar el uso de async al final del nombre del método
Await se usa solo en metodos async.
Await envia la ejecucion a segundo plano y nos devuelve el control al metodo que invoca.
Siempre se usan juntos!
Let's start by updating this code so that the thread doesn't block while tasks are running. The await keyword provides a non-blocking way to start a task, then continue execution when that task completes. A simple asynchronous version of the make a breakfast code would look like the following snippet.
This code doesn't block while the eggs or the bacon are cooking. This code won't start any other tasks though. You'd still put the toast in the toaster and stare at it until it pops. But at least, you'd respond to anyone that wanted your attention. In a restaurant where multiple orders are placed, the cook could start another breakfast while the first is cooking.
Now, the thread working on the breakfast isn't blocked while awaiting any started task that hasn't yet finished. For some applications, this change is all that's needed. A GUI application still responds to the user with just this change. However, for this scenario, you want more. You don't want each of the component tasks to be executed sequentially. It's better to start each of the component tasks before awaiting the previous task's completion.

<!-- Slide number: 20 -->

![](GoogleShape273p32.jpg)
# Programación asíncrona. Fundamentos

![](GoogleShape272p32.jpg)

![](GoogleShape274p32.jpg)

### Notes:
Combinacion de tareas: Aplicar la Manteca y la mermelada cuando el pan este tostado. Resaltar el async y el uso de generic con task!!!
You await each task before using its result. The next step is to create methods that represent the combination of other work. Before serving breakfast, you want to await the task that represents toasting the bread before adding butter and jam. You can represent that work with the following code.
The preceding method has the async modifier in its signature. That signals to the compiler that this method contains an await statement; it contains asynchronous operations. This method represents the task that toasts the bread, then adds butter and jam. This method returns a Task<TResult> that represents the composition of those three operations.

<!-- Slide number: 21 -->

![](GoogleShape283p33.jpg)

![](GoogleShape282p33.jpg)
# Programación asíncrona. Fundamentos

### Notes:
Uso eficiente de las tasks ucando se ejecutan varias simultaneamente.
WhenAll, which returns a Task that completes when all the tasks in its argument list have completed, as shown in the following code:
Another option is to use WhenAny, which returns a Task<Task> that completes when any of its arguments completes. You can await the returned task, knowing that it has already finished. The following code shows how you could use WhenAny to await the first task to finish and then process its result. After processing the result from the completed task, you remove that completed task from the list of tasks passed to WhenAny.

<!-- Slide number: 22 -->

# ConcurrenciavsParalelismovsAsincronía
Concurrencia y paralelismo están relacionados con tareas que se ejecutan simultáneamente PERO
Paralelismo se refiere a tareas que se ejecutan “al mismo tiempo” mientras que
Concurrencia se refiere a que las tareas están compartiendo el mismo hilo de ejecución mientras se están ejecutando en paralelo.
La Asincronía  presenta la impresión de tareas en paralelo que se ejecutan simultáneamente, sin embargo lo sucede es que su ejecución no bloquea el flujo de un programa. Asincronía no implica paralelismo.

### Notes:
Consider two tasks, T1 and T2, that have to be executed by an application. These two tasks are in concurrent execution if one is in an execution state while the other is waiting for its turn. As a result, one of the tasks completes ahead of the other. By contrast, the two tasks are in parallel execution if both execute simultaneously. Asynchronous methods aren't directly related to the previous two concepts, asynchrony is used to present the impression of concurrent or parallel tasking but effectively an asynchronous method call is normally used for a process that needs to do work away from the current application and we don't want to wait and block our application awaiting the response.
For example, getting data from a database could take time but we don't want to block our UI waiting for the data. The async call takes a call-back reference and returns execution back to your code as soon as the request has been placed with the remote system. Your UI can continue to respond to the user while the remote system does whatever processing is required, once it returns the data to your call-back method then that method can update the UI (or handoff that update) as appropriate.

<!-- Slide number: 23 -->

![](GoogleShape299p35.jpg)
# Lo que ocurre en un método asincrónico

https://learn.microsoft.com/es-es/dotnet/csharp/asynchronous-programming/task-asynchronous-programming-model#BKMK_WhatHappensUnderstandinganAsyncMethod

### Notes:
04_Async_00_Ejemplo02
04_Async_00_Ejemplo02a

<!-- Slide number: 24 -->

![](GoogleShape308p36.jpg)
# Paralelismo.Parallel.For

![](GoogleShape309p36.jpg)

### Notes:
04_Async_00_Ejemplo03_parallel02
/04_Async_00_Ejemplo03_parallel03

Parallelism is the ability to have parallel execution of tasks on systems that have multiple cores. Support for parallel programming in .NET was introduced in .NET Framework 4. Parallel programming in .NET allows us to use system resources more efficiently and with better programmatic control. This article talks about how we can work with parallelism in .NET Core applications.
The Task Parallel Library (TPL) supports data parallelism through the System.Threading.Tasks.Parallel class. This class provides method-based parallel implementations of for and foreach loops (For and For Each in Visual Basic). You write the loop logic for a Parallel.For or Parallel.ForEach loop much as you would write a sequential loop. You do not have to create threads or queue work items. In basic loops, you do not have to take locks. The TPL handles all the low-level work for you.
When a parallel loop runs, the TPL partitions the data source so that the loop can operate on multiple parts concurrently. Behind the scenes, the Task Scheduler partitions the task based on system resources and workload. When possible, the scheduler redistributes work among multiple threads and processors if the workload becomes unbalanced.

<!-- Slide number: 25 -->

# Paralelismo.Parallel.ForEach

![](GoogleShape317p37.jpg)

### Notes:
04_Async_00_Ejemplo03_parallel00

Parallelism is the ability to have parallel execution of tasks on systems that have multiple cores. Support for parallel programming in .NET was introduced in .NET Framework 4. Parallel programming in .NET allows us to use system resources more efficiently and with better programmatic control. This article talks about how we can work with parallelism in .NET Core applications.
The Task Parallel Library (TPL) supports data parallelism through the System.Threading.Tasks.Parallel class. This class provides method-based parallel implementations of for and foreach loops (For and For Each in Visual Basic). You write the loop logic for a Parallel.For or Parallel.ForEach loop much as you would write a sequential loop. You do not have to create threads or queue work items. In basic loops, you do not have to take locks. The TPL handles all the low-level work for you.
When a parallel loop runs, the TPL partitions the data source so that the loop can operate on multiple parts concurrently. Behind the scenes, the Task Scheduler partitions the task based on system resources and workload. When possible, the scheduler redistributes work among multiple threads and processors if the workload becomes unbalanced.

<!-- Slide number: 26 -->

using System;
using System.Threading.Tasks;

class Program
{
   static void Main(string[] args)
   {
       Console.WriteLine("Comenzando Parallel.Invoke...\n");

       Parallel.Invoke(
           () => DoTask("Tarea A", 1000),
           () => DoTask("Tarea B", 1500),
           () => DoTask("Tarea C", 500)
       );

       Console.WriteLine("\nParallel.Invoke completado!");
   }

   static void DoTask(string taskName, int delayMilliseconds)
   {
       Console.WriteLine(
$"{taskName} comenzada en el hilo {Environment.CurrentManagedThreadId}");
       Task.Delay(delayMilliseconds).Wait(); // Simulamos algo de trabajo
       Console.WriteLine(
$"{taskName} terminada en hilo {Environment.CurrentManagedThreadId}");
   }
}
# Paralelismo.Parallel.Invoke

### Notes:
04_Async_00_Ejemplo03_parallel02

Parallelism is the ability to have parallel execution of tasks on systems that have multiple cores. Support for parallel programming in .NET was introduced in .NET Framework 4. Parallel programming in .NET allows us to use system resources more efficiently and with better programmatic control. This article talks about how we can work with parallelism in .NET Core applications.
The Task Parallel Library (TPL) supports data parallelism through the System.Threading.Tasks.Parallel class. This class provides method-based parallel implementations of for and foreach loops (For and For Each in Visual Basic). You write the loop logic for a Parallel.For or Parallel.ForEach loop much as you would write a sequential loop. You do not have to create threads or queue work items. In basic loops, you do not have to take locks. The TPL handles all the low-level work for you.
When a parallel loop runs, the TPL partitions the data source so that the loop can operate on multiple parts concurrently. Behind the scenes, the Task Scheduler partitions the task based on system resources and workload. When possible, the scheduler redistributes work among multiple threads and processors if the workload becomes unbalanced.

<!-- Slide number: 27 -->

# Paralelismo.ParallelLINQ

![](GoogleShape333p39.jpg)

### Notes:
PLINQ ejecuta intentando utilizar todos los cores/cpus de la maquina.Parallel LINQ (PLINQ) is a parallel implementation of the Language-Integrated Query (LINQ) pattern. PLINQ implements the full set of LINQ standard query operators as extension methods for the System.Linq namespace and has additional operators for parallel operations. PLINQ combines the simplicity and readability of LINQ syntax with the power of parallel programming.

División de tareas en segmentos paralelizables

Ventaja: Ejecución significativamente mas rapida

Desventaja: Introduce complejidad inherente al paralelismo (concurrencia y sincronizacion).

<!-- Slide number: 28 -->

# Paralelismo.ParallelLINQ

![](GoogleShape341p40.jpg)

![](GoogleShape342p40.jpg)

### Notes:
PLINQ nos permite modificar el grado de paralelismo. De esta forma podemos preservar recursos de la maquina para el resto de los procesos
PLINQ nos permite modificar tambien el orden de la consulta (Ordered PLINQ). De esta forma podemos generar un resultado ordenado
El Framework .NET incluye clases para manejar colecciones concurrentes, que son “fully thread-safe”
Sin embargo, estas colecciones NO superan en performance a sus contrapartes no-thread safe.
En escenarios multihilo, es preferible usar las colecciones estándar.
Internamente, estan implementadas con listas enlazadas, por lo que consumen mas memoria que sus contrapartes estandar.

<!-- Slide number: 29 -->

Indica que una operación debe ser cancelada. Es una clase derivada de Event.
# CancellationToken

![](GoogleShape351p41.jpg)

### Notes:
04_Async_02_Ejemplo07cancellation
https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/events/
https://learn.microsoft.com/en-us/search/?terms=CancellationToken

You have to pass the CancellationToken to the Task, which will periodically monitors the token to see whether cancellation is requested.

In this case, the operation will end when cancellation is requested and the Task will have a RanToCompletion state. If you want to be acknowledged that your task has been cancelled, you have to use ThrowIfCancellationRequested to throw an OperationCanceledException exception.
Una cosa que debemos tener en cuenta es que una vez que se cancela una tarea, la propiedad IsCancellationRequested nos devolverá siempre true, ya que es una propiedad de sólo lectura, por lo que para reutilizar CancellationToken, deberíamos volver a crear un objeto de tipo CancellationTokenSource para luego asignarle a CancellationToken un nuevo token.Es decir, cada tarea que deseemos procesar y en su caso cancelar, debe llevar su propio token.

<!-- Slide number: 30 -->

Crear un CancellationTokenSource
CancellationTokenSource cts = new CancellationTokenSource();

Generar un Token y pasarlo a toda tarea que deba responder a él.
cts.Token

Dentro de la tarea, comprueba token.IsCancellationRequested o pásalo a operaciones cancelables como:
await Task.Delay(1000, token); // Cancela limpiamente si se solicita

Para disparar la operación se ejecuta:
cts.Cancel();
# CancellationToken

### Notes:
04_Async_02_Ejemplo07cancellation
https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/events/
https://learn.microsoft.com/en-us/search/?terms=CancellationToken

You have to pass the CancellationToken to the Task, which will periodically monitors the token to see whether cancellation is requested.

In this case, the operation will end when cancellation is requested and the Task will have a RanToCompletion state. If you want to be acknowledged that your task has been cancelled, you have to use ThrowIfCancellationRequested to throw an OperationCanceledException exception.
Una cosa que debemos tener en cuenta es que una vez que se cancela una tarea, la propiedad IsCancellationRequested nos devolverá siempre true, ya que es una propiedad de sólo lectura, por lo que para reutilizar CancellationToken, deberíamos volver a crear un objeto de tipo CancellationTokenSource para luego asignarle a CancellationToken un nuevo token.Es decir, cada tarea que deseemos procesar y en su caso cancelar, debe llevar su propio token.

<!-- Slide number: 31 -->

Mejores prácticas
Pasar siempre CancellationToken en métodos de larga ejecución o asíncronos.
Comprueba IsCancellationRequested en los bucles
Utilizar ThrowIfCancellationRequested() dentro de los métodos cuando proceda
Catch OperationCanceledException para manejar la cancelación esperada sin bloquearse
No ignores la cancelación ni asumas que las tareas terminan al instante

# CancellationToken
| Funcionalidad | Beneficio |
| --- | --- |
| Cancelación Cooperativa | La tarea evalúa el token y se detiene a sí misma ordenadamente |
| Evita el Matar Forzado | A diferencia del Thread.Abort, no interrumpe abruptamente la tarea. Es responsabilidad del programador organizar la salida ordenada de la tarea. |
| Liberación de Recursos | Perimte que se liberen ordenadamente los recursos, se cierren los archivos sockets y otros objetos, y que se descarguen y hagan persistentes datos y logs. |
| Apagado Coordinado | Un token puede controlar a todas las tareas vinculadas al evento. |

### Notes:
04_Async_02_Ejemplo07cancellation
https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/events/
https://learn.microsoft.com/en-us/search/?terms=CancellationToken

You have to pass the CancellationToken to the Task, which will periodically monitors the token to see whether cancellation is requested.

In this case, the operation will end when cancellation is requested and the Task will have a RanToCompletion state. If you want to be acknowledged that your task has been cancelled, you have to use ThrowIfCancellationRequested to throw an OperationCanceledException exception.
Una cosa que debemos tener en cuenta es que una vez que se cancela una tarea, la propiedad IsCancellationRequested nos devolverá siempre true, ya que es una propiedad de sólo lectura, por lo que para reutilizar CancellationToken, deberíamos volver a crear un objeto de tipo CancellationTokenSource para luego asignarle a CancellationToken un nuevo token.Es decir, cada tarea que deseemos procesar y en su caso cancelar, debe llevar su propio token.

<!-- Slide number: 32 -->

Un CancellationToken es un mecanismo proporcionado por .NET para indicar a una tarea u operación que debe dejar de ejecutarse. Esto es especialmente útil para:
Cerrar servicios
Interrumpir bucles de larga duración
Cancelar operaciones en segundo plano
Coordinar el cierre de varias tareas
Forma parte del espacio de nombres System.Threading y funciona junto con CancellationTokenSource, que crea y señala el token.
# CancellationToken

### Notes:
04_Async_02_Ejemplo07cancellation
https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/events/
https://learn.microsoft.com/en-us/search/?terms=CancellationToken

You have to pass the CancellationToken to the Task, which will periodically monitors the token to see whether cancellation is requested.

In this case, the operation will end when cancellation is requested and the Task will have a RanToCompletion state. If you want to be acknowledged that your task has been cancelled, you have to use ThrowIfCancellationRequested to throw an OperationCanceledException exception.
Una cosa que debemos tener en cuenta es que una vez que se cancela una tarea, la propiedad IsCancellationRequested nos devolverá siempre true, ya que es una propiedad de sólo lectura, por lo que para reutilizar CancellationToken, deberíamos volver a crear un objeto de tipo CancellationTokenSource para luego asignarle a CancellationToken un nuevo token.Es decir, cada tarea que deseemos procesar y en su caso cancelar, debe llevar su propio token.

<!-- Slide number: 33 -->

# Escenarios comunes donde aplica la programación asíncrona.
Procesos que involucran I/O
Tareas que involucran alto procesamiento de recursos

![](GoogleShape385p45.jpg)

### Notes:
If you have any I/O-bound needs (such as requesting data from a network, accessing a database, or reading and writing to a file system), you'll want to utilize asynchronous programming. You could also have CPU-bound code, such as performing an expensive calculation, which is also a good scenario for writing async code.
The core of async programming is the Task and Task<T> objects, which model asynchronous operations. They are supported by the async and await keywords. The model is fairly simple in most cases:
For I/O-bound code, you await an operation that returns a Task or Task<T> inside of an async method.
For CPU-bound code, you await an operation that is started on a background thread with the Task.Run method.
The await keyword is where the magic happens. It yields control to the caller of the method that performed await, and it ultimately allows a UI to be responsive or a service to be elastic. While there are ways to approach async code other than async and await, this article focuses on the language-level constructs.
Will your code be "waiting" for something, such as data from a database?
If your answer is "yes", then your work is I/O-bound.
Will your code be performing an expensive computation?
If you answered "yes", then your work is CPU-bound.

<!-- Slide number: 34 -->

# Notas importantes sobre el uso de async/await
Todo método que se defina con async debe tener un await en su cuerpo.
async void se usa solo en los eventos (ej: button click)
Se debe escribir código que espere por tareas de forma no bloqueante.

| Usar: | En vez de: | Cuando se quiera: |
| --- | --- | --- |
| await | Task.Wait o Task.Result | Devolver el resultado de una background task |
| await Task.WhenAny | Task.WhenAny | Esperar que se complete alguna tarea en una lista |
| await Task.WhenAll | Task.WaitAll | Esperar que todas las tareas de una lista se completen |
| await Task.Delay | Thread.Sleep | Esperar durante un intervalo de tiempo |

### Notes:
async methods need to have an await keyword in their body or they will never yield!
This is important to keep in mind. If await is not used in the body of an async method, the C# compiler generates a warning, but the code compiles and runs as if it were a normal method. This is incredibly inefficient, as the state machine generated by the C# compiler for the async method is not accomplishing anything.
async void should only be used for event handlers.
async void is the only way to allow asynchronous event handlers to work because events do not have return types (thus cannot make use of Task and Task<T>). Any other use of async void does not follow the TAP model and can be challenging to use, such as:
Exceptions thrown in an async void method can't be caught outside of that method.
async void methods are difficult to test.
async void methods can cause bad side effects if the caller isn't expecting them to be async.
Tread carefully when using async lambdas in LINQ expressions
Lambda expressions in LINQ use deferred execution, meaning code could end up executing at a time when you're not expecting it to. The introduction of blocking tasks into this can easily result in a deadlock if not written correctly. Additionally, the nesting of asynchronous code like this can also make it more difficult to reason about the execution of the code. Async and LINQ are powerful but should be used together as carefully and clearly as possible.
Write code that awaits Tasks in a non-blocking manner
Blocking the current thread as a means to wait for a Task to complete can result in deadlocks and blocked context threads and can require more complex error-handling. The following table provides guidance on how to deal with waiting for tasks in a non-blocking way:

<!-- Slide number: 35 -->

# Notas importantes sobre el uso de async/await
Añadir Async al final del nombre de un método async
Usar .ConfigureAwait(false/true) en los casos necesarios.

![](GoogleShape403p47.jpg)

### Notes:
Add "Async" as the suffix of every async method name you write.
This is the convention used in .NET to more easily differentiate synchronous and asynchronous methods. Certain methods that aren't explicitly called by your code (such as event handlers or web controller methods) don't necessarily apply. Because they are not explicitly called by your code, being explicit about their naming isn't as important
A common question is, "when should I use the Task.ConfigureAwait(Boolean) method?". The method allows for a Task instance to configure its awaiter. This is an important consideration and setting it incorrectly could potentially have performance implications and even deadlocks. For more information on ConfigureAwait, see the ConfigureAwait FAQ.

<!-- Slide number: 36 -->

# Threads vsTasks

La clase Thread se usa para crear y manipular recursos del Sistema Operativo. Una Task representa una operación asíncrona y forma parte de la librería Task.Parallel que ofrece un conjunto de APIs para ejecutar tareas de forma asíncrona y en paralelo.
Una tarea puede devolver un resultado. No hay un mecanismo directo de devolver un resultado desde un Thread.
Las tareas soportan cancelación con el uso de Cancellation Tokens. Los hilos no.
Una tarea puede tener múltiples procesos sucediendo al mismo tiempo. Los Threads solo pueden tener una tarea ejecutándose a la vez.
Es muy fácil implementar tareas asíncronas usando async/await
Una Task es un concepto de alto nivel, un Thread no.

### Notes:

<!-- Slide number: 37 -->

# Diseño de funciones asincrónicas
Escriba su método sincrónicamente
Reemplace las llamadas a métodos sincrónicos con llamadas a métodos asincrónicos y esperarlas (await).
A excepción del controlador de eventos de nivel superior (top level event handler), actualice el tipo de retorno de su método asíncrono a Task o Task<Result> para que esté disponible.

### Notes: