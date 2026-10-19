---
date: 2026-09-17
type: clase
tags:
  - clase
  - pdr
  - tema-3
ai-first: true
confidence: stated
fuente: "Grabacion Vimeo, showcase 12367503, video 17-09-2026, 1h45m, subtitulos autogenerados"
docentes:
  - Enrique Latorres
  - TBD
---

# Clase 2026-09-17 - Task en detalle, continuations, ThreadPool y TPL

## For future agent
Acta de la clase del 17-09-2026 (Vimeo, showcase 12367503, 105 min). Clase entera de **Tema 3: programacion asincronica en C#**, sobre codigo y diapositivas.

**Lo primero y mas importante: `Task` NO va en el Obligatorio 1.** El docente lo aclaro al arrancar y advirtio del riesgo concreto de que la IA lo meta sin que te des cuenta.

Contenido: `Task.Run`, `ContinueWith` y sus opciones, excepciones encapsuladas en `Task`, `WhenAll` / `WhenAny`, por que **nunca `async void`**, por que **nunca `Thread.Sleep`** en codigo asincronico, `ThreadPool` a mano, las **cuatro capas** (Thread / ThreadPool / TPL / async-await), cuando si usar `Thread`, `CancellationToken`, la convencion del sufijo `Async`, y el ejemplo del desayuno con **tiempos medidos: 15 s secuencial contra 6 s asincronico**.

## Aviso critico: Task no va en el Obligatorio 1
> "Tienen claro que lo que empezamos a dar la clase pasada de task, eso no va para el primero. Para el primero no puede task." ([01:00])

Y la advertencia practica, que vale registrar tal cual:

> "Hay gente que va y le dice: 'Che, haceme esto'. Y naturalmente, si no le especificas nada, vas a task. Y yo tengo que sacarle todo el fondo. Es estricto." ([01:00])

O sea: **si le pedis el codigo a una IA sin especificar, te va a devolver `Task`**, y hay que saco todo y volver a `Thread` explicito. Esto refuerza [[Reglas y prohibiciones del curso PDR 2026]].

## Task.Run, ContinueWith y el orden de ejecucion
Primer ejemplo del pizarron:

```csharp
var t1 = Task.Run(() => { Console.WriteLine(2); Task.Delay(300); Console.WriteLine(3); return 10; });
var t2 = t1.ContinueWith(t => t.Result * 2);
var t3 = Task.Run(async () => { Console.WriteLine(5); await Task.Delay(100); Console.WriteLine(6); });
Console.WriteLine(7);
Console.WriteLine(8 + await t2);   // 20
await t3;
Console.WriteLine(9);
```

- `var t1 = Task.Run(...)` **ya crea y dispara la tarea**. El resultado se asigna a un `Task<int>`.
- **`ContinueWith`** = "atame este pedazo de codigo al final de esa tarea": cuando `t1` termine, se dispara `t2`.
- `t2` recibe `t1` como parametro (`t`), le saca `t.Result` (el `int`), lo multiplica y devuelve.
- `t2` **no arranca hasta que `t1` termine**, y **solo si termino bien**. Si `t1` falla o se cancela, `t2` **no arranca**.
- El `Main` crea **las tres tareas practicamente al mismo tiempo** y sigue. Los segmentos entran a la cola y el `ThreadPool` los saca.
- **No se sabe en que orden van a correr.** Los `Delay` de 100 y 300 ms estan puestos justamente para que se note el entrelazado.

> "Esos segmentos no se en que orden se van a ejecutar." ([09:00])

## Excepciones: la gran ventaja de Task sobre Thread
- Con **`Thread`**, las excepciones **quedan encapsuladas en el stack de cada hilo**. Para avisarle al hilo principal hay que **inventar un mecanismo**: una variable compartida, un objeto, algo a mano.
- Con **`Task`**, la excepcion **viaja dentro del `Task`**. Al hacer `await` (o al leer `Result`) **la excepcion se relanza** y se puede atrapar con `try/catch` normal.

```csharp
try {
    var t1 = InnerTaskAsync();   // lanza InvalidOperationException adentro
    // ... el main sigue haciendo otras cosas, sin enterarse ...
    await t1;                    // aca recien se entera: relanza la excepcion
} catch (Exception ex) { /* manejo */ }
```

- Punto importante del ejemplo: **el main no se entera de la excepcion cuando ocurre**, sino **cuando hace el `await`**.
- Las tareas tambien pueden tener su **`try/catch/finally` interno** y hacer **rethrow** para que la atrape el nivel superior.
- El unico caso que se puede atrapar al crear un `Thread` es que **no se pueda crear el hilo**. Una vez corriendo, las excepciones de ese hilo **solo se capturan adentro de ese hilo**.

## Nunca async void
> "El void en general es peligroso porque si no tenemos un resultado no podemos verificar la terminacion, tenemos riesgo de condicion de carrera." ([19:00])

- `async void` son las rutinas **"fire and forget"** (disparar y olvidar).
- Problema: **no se puede saber si termino, con que resultado, ni si tuvo excepcion**.
- Regla: **siempre devolver un `Task`** (o `Task<T>`) y que alguien lo evalue.
- Unica excepcion mencionada: **eventos de interfaz grafica en Windows**. Para aplicaciones normales, no.

## Muchas tareas: listas, WhenAll y WhenAny
```csharp
var tasks = new List<Task> { Tarea1Async(msg1), Tarea2Async(msg2) };
try {
    await Task.WhenAll(tasks);
} catch {
    foreach (var ex in tasks.Where(t => t.IsFaulted).SelectMany(t => t.Exception.InnerExceptions))
        Procesar(ex);
}
```

- Se crean las tareas **invocandolas** (sin `Run`) y se meten en un conjunto.
- **`await Task.WhenAll(...)`** espera a que **todas** terminen. Caso de uso: partes de una misma cosa que hay que juntar al final.
- Se pueden **disparar todas, hacer otra cosa en el medio**, y recien despues esperar.
- Cuando varias tareas fallan, hay que **recorrer las excepciones agregadas** y procesarlas una por una (identificar que tarea fallo permite deshacer o recuperar).
- **`WhenAny`** en un `while`: cada vez que **una** termina, se reporta y se **quita de la lista**; el loop sigue mientras quede alguna. Sirve para ir informando el progreso.

## ContinueWith con opciones
Se le puede decir **bajo que condicion** continuar:

| Opcion | Cuando dispara |
|---|---|
| `OnlyOnCanceled` | la tarea anterior **se cancelo** (recibio una excepcion de cancelacion via `CancellationToken`) |
| `OnlyOnFaulted` | la tarea anterior **fallo** con excepcion |
| `OnlyOnRanToCompletion` | la tarea anterior **se completo bien** |

- Se pueden encadenar varios `ContinueWith` con distintas opciones sobre la misma tarea, y asi manejar cada terminacion por separado.
- Regla general repetida: **si la tarea previa falla, las continuations normales no se ejecutan.**

## Continuations implicitas: como se corta el codigo en segmentos
- Cada tramo entre dos puntos de transicion asincronica es un **segmento**, y **cada segmento es un `ContinueWith` implicito del anterior**.
- Ejemplo: un metodo con un `await` en el medio son **tres segmentos** (antes, el await, despues).

> "En el fondo, mi programa se transforma en un monton de segmentitos con continuation with implicito." ([24:00])

- Los segmentos **respetan el orden del codigo**, pero **entre distintas tareas el despachador puede intercalar**: al terminar un segmento puede darle el hilo a otra tarea que estaba esperando.
- Sobre **prioridades** en la cola: el docente dijo que **no sabe bien como lo maneja .NET**, que supone algo tipo **FIFO con manejo especial**, y que no lo estudio a fondo.

## Nunca Thread.Sleep en codigo asincronico
> "En general, no usen thread sleep. Ahora usaria el task delay." ([21:00])

- **`Thread.Sleep` bloquea el hilo**, y ese es **un hilo menos** para todo el sistema asincronico.
- Un segundo de `Sleep` es **una enormidad**: "pasan millones de cosas en un segundo".
- Lo correcto es **`await Task.Delay(...)`**, que **no bloquea** y ademas **crea un punto de segmento**.
- **Trampa importante**: `Task.Delay(300)` **sin `await` no espera nada**. Dispara la tarea y el codigo sigue de largo.

> "Para que esos delay realmente funcionen como delay, tengo que ponerle explicitamente el await." ([81:00])

## ThreadPool a mano (ejemplo ilustrativo, no se usa)
Ejemplo que baja varias paginas web:

```csharp
var counter = new CountdownEvent(urls.Count);
foreach (var url in urls)
    ThreadPool.QueueUserWorkItem(FetchWebPage, new WorkState(url, counter));
counter.Wait();      // el main espera a que todos terminen
```

- **`QueueUserWorkItem`** es el metodo para **meter una tarea en el `ThreadPool`**.
- **`CountdownEvent`** es el contador que lleva la cuenta de los hilos que van terminando. El `Main` espera a que llegue a cero.
- `WorkState` es una clase **solo para pasar parametros** (la URL y el evento), porque a mano hay que inventar el mecanismo de aviso.
- El trabajo usa **`HttpClient`** y cuenta los bytes de la pagina. Una URL del ejemplo no existe, para ver la captura de excepcion.
- **Las excepciones se manejan adentro de cada hilo**: el hilo principal **ni se entera** de que paso, solo ve que termino.
- **Veredicto del docente: "No lo vamos a usar. Es un ejemplo."** Sirve para entender lo que TPL y `async/await` hacen por encima.
- El docente pidio **correrlo para ver como funciona**.

## Las cuatro capas
De abajo hacia arriba, cada una usa la anterior:

| Capa | Que aporta |
|---|---|
| **`Thread`** | el hilo crudo; control total, costo alto |
| **`ThreadPool`** | reutiliza hilos, calcula cuantos conviene tener, los reparte |
| **Task Parallel Library** | agrupa tareas, las despacha al pool, espera por ellas, maneja excepciones |
| **`async` / `await`** | corta el programa en segmentos, notacion natural, promesas |

### Por que el ThreadPool es mejor que crear hilos
- Crear un hilo cuesta: **ubicar memoria, un stack y un mini Process Control Block**. El **cambio de contexto** guarda y restaura estado entre memoria y procesador.
- **No se sabe cuantos hilos necesita una aplicacion** ni cual es la **cantidad optima**: depende de los nucleos de la CPU, del tipo de tarea, del rendimiento, y de todo lo demas que corre en la maquina.
- El `ThreadPool` **calcula y escala** eso solo, y **reutiliza** el hilo en vez de destruirlo: cuando una tarea termina, el hilo **vuelve al pool** y agarra la siguiente de la cola (**FIFO**).
- Objetivo del pool: que **los hilos esten ocupados al 100%** y **no paren nunca**. Si la cola se vacia, ahi si los apaga.

## Cuando SI usar Thread
Solo en estos casos:
- Hace falta que el hilo sea **foreground**.
- Tarea **intensiva en procesador**, que necesita el hilo **mucho tiempo de forma continua**.
- Hace falta **darle prioridad**.
- **Tiempo real**: leer un sensor, algo que requiere continuidad garantizada.

> "Para temas de sistemas en general, de servicios, preferible que hagan programacion asincrona." ([54:00])

Nota de tipos: **los hilos de `Thread` son foreground por defecto; los de `Task` son background.** De ahi que el `Main` tenga que esperar.

## Como esperar bien en el Main
Tres versiones del mismo ejemplo:

1. **Mal**: se dispara la tarea (que tarda 1 s) y el `Main` termina. Nunca se ve el mensaje de la tarea, porque el hilo de background **se corta**.
2. **Peor todavia, aunque "funcione"**: se agrega un **`Console.ReadLine()`**. Parece arreglarlo, pero **no esta esperando la tarea**, esta esperando que el usuario apriete enter. Funciona solo porque **los usuarios son lentos**. El docente desafio a probar apretar enter lo bastante rapido para que no aparezca el mensaje.
3. **Bien**: **`await`** de la tarea. Ahi si el orden tiene sentido.

Ejercicio sugerido: poner codigo **entre** el `Start` y el `await` para ver que el `Main` avanza mientras la tarea corre.

## Convencion: sufijo Async
- **`async` no cambia la firma del metodo**: le indica al compilador que trate `await` como palabra reservada (sin `async`, `await` seria un identificador cualquiera).
- Convencion: **agregarle `Async` al final del nombre** a todo metodo asincronico.

> "Tambien les va a ayudar a ustedes despues en el obligatorio a decir: 'Okey, este metodo, ya veo el nombre y ya se que es async'." ([90:00])

- **No todo tiene version asincronica**: `Console.WriteLine` y similares **no la tienen** y no tendria sentido, porque son muy cortos. Lo que si suele tener version asincronica es la **entrada/salida** (archivos, red): mandan el pedido y devuelven un `Task`.
- `Task.Run(...)` con un lambda es otra forma de crear una tarea; lo mas prolijo es **declarar el metodo `async`** y simplemente invocarlo. Y **no mezclar** los dos estilos.
- Tambien hay **esperas sincronicas** (`Task.Wait()`, leer `Result`) para usar desde codigo que **no** esta compilado como asincronico: bloquean el hilo, a la vieja usanza. Se usan solo cuando hay una tarea CPU-intensiva sincronica que necesita sincronizarse con el mundo asincronico.

## CancellationToken
Uno de los temas que se van a ver en detalle, y el que resuelve el problema de cierre que quedo abierto el 10-09:

- Es una clase que **manda un mensaje** a las tareas para avisarles que tienen que terminar.
- **Lo mas valioso**: metodos que con `Thread` son **bloqueantes** dejan de serlo con `Task`. Ejemplo dado: **`AcceptAsync`** del `TcpListener`, que espera una conexion nueva.
- Con el token, ese `await` **recibe el mensaje y genera una excepcion**, sale del bloqueo, se atrapa la excepcion y se hace un **cierre prolijo y ordenado**.

> "Cuando ustedes hagan su ejemplo con threads, eso ustedes no lo pueden hacer. Si tienen un hilo que esta atrapado adentro de un accept, la unica forma de sacarlo es que alguien trate de hacer la conexion." ([72:00])

Eso es exactamente la limitacion que tiene el Obligatorio 1 y que el 2 resuelve.

## El ejemplo del desayuno: la leccion central
Tres versiones del mismo programa, con las **mismas esperas**, medidas en vivo:

| Version | Como esta escrito | Tiempo |
|---|---|---|
| **1. Sincronica** | un paso tras otro: cafe, huevos, panceta, tostadas, manteca, mermelada, jugo | 30 min "reales" / **15 s medidos** |
| **2. Con `async`/`await` mal usados** | `await FryEggsAsync(); await FryBaconAsync(); await ToastBreadAsync();` | **lo mismo que la sincronica** |
| **3. Bien** | se disparan las tres tareas, se guardan, y **recien despues** se hacen los `await` | **6 s medidos** |

**Por que la version 2 no mejora nada**: el `await` va **pegado a la invocacion**, asi que se espera antes de arrancar la siguiente. Sigue siendo secuencial aunque diga `async`.

> "Aca se esta usando el await, pero al mismo tiempo estoy llamando a la funcion, pero al mismo tiempo me estoy quedando a que la funcion termine. Entonces, sigue siendo secuencial de alguna forma, aunque se use await." ([94:00])

**Regla practica**: **disparar primero, esperar despues.** Guardar los `Task` y hacer los `await` (o un `WhenAll`) cuando realmente se necesitan los resultados.

Matices que salieron:
- Algunas cosas **si tienen que ser secuenciales**: no se le pone manteca y mermelada a un pan que todavia no esta tostado. Esa parte queda `await` adentro de su propio metodo.
- El total tarda **lo que tarda la tarea mas lenta**, no la suma.
- Version mas prolija: meter todas las tareas en una lista y hacer **un solo `WhenAll`** en vez de esperas separadas. "Queda mas lindo de leer, pero es la misma cosa."
- El hilo principal podria ademas **hacer una de las tareas el mismo** en vez de tirar un tercer hilo.

## Anecdota: el 42
El valor de retorno de los ejemplos es **42**, por la novela *The Hitchhiker's Guide to the Galaxy* (la respuesta de la supercomputadora a la pregunta por el sentido del universo). El docente agrego que **si le pedis un numero aleatorio a la mayoria de las IA, te dan 42**, y que hay estudios sobre eso: "es el menos aleatorio de todos".

## Lambdas y metodos delegados (adelanto)
- Las **lambdas** se van a usar mucho cuando se vean **funciones delegadas**.
- Patron: se le dice a una clase "cuando pase tal cosa, llama aca". Bibliotecas de comunicacion remota **disparan automaticamente** el metodo delegado al recibir un mensaje (a diferencia de TCP/IP crudo, donde hay que sentarse a esperar).
- Recomendacion de estilo: para logica no trivial, **hacer el metodo explicito con un nombre significativo** en vez de un lambda inline, que queda dificil de asociar con lo que hace.

## Sumario de lo que viene en Tema 3
Enunciado por el docente:
- TPL y `ThreadPool` (visto).
- Distintas formas de definir tareas.
- Fundamentos de asincronia (repaso).
- **Sincronizacion de tareas**: `WaitAll`, `WhenAll`.
- **`CancellationToken`** ("otro elemento superimportante").
- Consideraciones al usar `async`/`await`.
- Mecanismos para ejecutar tareas en paralelo.
- Comparacion `Task` contra `Thread`.
- Detalles de metodos asincronicos.

## Tarea y avisos de cierre
- **Entrega del Obligatorio 1: el lunes 28-09.** "Diez dias." Sin apuro por entregar ese mismo dia.
- **No entregar "cosas fantasma"** (proyectos vacios o que no corren). En el obligatorio no se permite.
- Los **ejemplos de `ContinueWith`** los manda **por Teams** (un zip) y **tambien estan en el grupo**. Parte esta en el libro.
- Pedido explicito: **copiar los codigos, modificarlos, romper las tareas a proposito, poner mensajes** y ver que pasa.

> "Y ahi rapidamente se van a acostumbrar y van a hacer cosas multihilo mucho mas facil." ([104:00])

- Se paso lista.

## Enlaces
- [[Tema 3 - Asincronia y paralelismo]] - la teoria de este tema
- [[Clase 2026-09-15 - Transferencia de archivos por chunks e introduccion a asincronia]]
- [[Concurrencia de Thread a Task]]
- [[Reglas y prohibiciones del curso PDR 2026]]
- [[Errores caros en Programacion de Redes]]
- [[Programacion de Redes 2026]]
