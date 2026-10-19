# TEMA 3 — Asincronía y Paralelismo (PDR)

**Cobertura vigente de Aulas (semanas 7 a 8):** evolución desde Threads hacia `Task` y funciones asíncronas con `async`/`await` en C#.

**Fuentes principales:** 🟢 `TEORICO/03_PARALELISMO/Asincronía y paralelismo.pdf` (39 diapositivas) y 🟢 `TEORICO/03_PARALELISMO/04 Asincronía y paralelismo 2025.pptx` (37 diapositivas, integrada el 17/08/2026). La presentación confirma el contenido del PDF y agrega ejemplos explícitos de `Parallel.Invoke` y mejores prácticas de cancelación.

**Evidencia de evaluación (🔵):** `Task`/asincronía/paralelismo aparece en 14 de las 16 letras históricas de parcial revisadas (2019-2025, clasificación por presencia temática). Se repiten especialmente: diferencia `Task` vs `Thread`, reescritura con `async/await`, `WhenAll`, cancelación y distinción I/O-bound vs CPU-bound.

**Reescrito de cero (11/08)** a partir de esta fuente oficial, reemplazando la versión anterior basada en `0.RESUMEN-STUDOCU.pdf`. StuDocu ya no es la base del tema — solo se usaría puntualmente si hiciera falta completar un hueco, marcado como 🟠, y en esta reescritura no hizo falta.

**Nota histórica:** "precedencia explícita" y "colecciones autosincronizables" pertenecen al programa anterior de ocho unidades. No son huecos del Tema 3 vigente de Aulas, centrado en `Task` y `async`/`await`.

**Tags usados:** 🟢 Oficial (esta diapositiva) · 🔵 Evidencia de evaluación (parciales/exámenes) · 🟠 Complementario (explicación agregada, no oficial).

---

## Sección de recuperación — preguntas antes de leer

Contestá esto de memoria (o intentando razonarlo) antes de leer el resto del resumen. Sirve para anclar mejor lo nuevo:

1. ¿Qué diferencia intuitiva harías entre "dos tareas corriendo a la vez" y "dos tareas que parecen correr a la vez pero se van turnando"?
2. Si ya viste el Tema 2 (Threads): ¿por qué crear un `Thread` por cada tarea chica podría ser un desperdicio?
3. ¿Qué esperás que haga la palabra `await` en una línea de código?
4. ¿Se te ocurre por qué "cancelar" una tarea que ya está corriendo en otro hilo no es tan simple como "matarla" de golpe?

---

## 1. Fundamentos del paralelismo

🟢 **Qué es:** "el término paralelismo se refiere a técnicas para hacer que los programas sean más rápidos al realizar MÚLTIPLES TAREAS al MISMO TIEMPO" [Pág. 3].

🟢 **Por qué importa:** ejecutar tareas de a una agota el beneficio de tener varios núcleos de CPU disponibles. El paralelismo aprovecha esos núcleos.

### Multithreading y su costo

🟢 `System.Threading.Thread`: "Multithreading se refiere a la ejecución concurrente/paralela de uno o más hilos o instrucciones" [Pág. 5]. Pero la diapositiva es explícita: **"es muy costosa computacionalmente"** [Pág. 5] crear y destruir hilos manualmente para cada tarea.

### Thread pooling

🟢 **Qué es:** "el proceso de crear una colección de threads en una aplicación y reutilizarlos según convenga" [Pág. 4]. Existen varias formas históricas de aprovechar un pool antes de llegar a `Task`:

- `ThreadPool.QueueUserWorkItem`
- Asynchronous delegate
- Background worker (ya visto en Tema 2 como patrón)
- **Task Parallel Library (TPL)** [Pág. 6] — la que se usa en la práctica moderna.

🟠 **Complementario:** el pool funciona como una fila de tareas nuevas que se van encolando (`Task Queue`), y los hilos libres del pool van tomando la siguiente tarea disponible apenas se desocupan — evita el costo de crear/destruir un hilo del sistema operativo por cada trabajo chico [Pág. 4, diagrama].

---

## 2. Task Parallel Library (TPL) vs. Thread Pool

🟢 **Qué es la TPL:** "ofrece formas más eficientes de generar y ejecutar Threads: el código se escribe como una secuencia de tareas a pesar de ser asíncrono. Detrás de escena, los objetos de tipo `Task` se encolan en un thread pool... una forma más eficiente de manejar los hilos que la tradicional" [Pág. 7].

### Beneficios de `Task` frente a crear un `Thread` a mano

🟢 [Pág. 8]:
- **Auto-tuning del ThreadPool** (el propio pool decide cuántos hilos mantener activos).
- Más control sobre: **Status**, **Return values**, **Cancellation**, **Exception handling**, **Scheduling**, **Continuation**.

🟢 **Comparación textual Task vs. Thread** [Pág. 38]:

| Aspecto | Thread | Task |
|---|---|---|
| Nivel | Crea y manipula recursos del SO directamente | Operación asíncrona de la TPL, más alto nivel |
| Valor de retorno | No hay mecanismo directo | Puede devolver un resultado |
| Cancelación | No soportada | Soportada con `CancellationToken` |
| Concurrencia interna | Un hilo ejecuta una sola cosa a la vez | Una tarea puede encadenar múltiples procesos (continuations) |
| Facilidad async | — | Muy fácil de combinar con `async`/`await` |

### Autoevaluación rápida
- Según la fuente, ¿en qué se diferencia devolver un resultado desde un `Thread` frente a hacerlo desde una `Task`?

---

## 3. Formas de crear y usar una `Task`

🟢 Formas mostradas en la diapositiva [Pág. 12]:

```csharp
// Crear sin iniciar, después arrancar
Task t1 = new Task(PrintInfo);
t1.Start();

// Crear e iniciar en un solo paso
Task t3 = Task.Run(() => { PrintInfo(); });
```

🟢 `Task.Delay(300)` es "el equivalente asíncrono del `Thread.Sleep`" [Pág. 13].

🟢 **Continuations** — encadenar código para después de que una tarea termine, distinguiendo el motivo por el que terminó [Pág. 14]:

```csharp
Task<int> task = Task.Run(() => { return 10; });

task.ContinueWith((i) => {
    Console.WriteLine("Task Canceled");
}, TaskContinuationOptions.OnlyOnCanceled);

task.ContinueWith((i) => {
    Console.WriteLine("Task Faulted");
}, TaskContinuationOptions.OnlyOnFaulted);

var completedTask = task.ContinueWith((i) => {
    Console.WriteLine("Task Completed");
}, TaskContinuationOptions.OnlyOnRanToCompletion);

completedTask.Wait();
```

### ¿Qué pasa si no se espera la tarea?

🟢 La diapositiva muestra un ejemplo progresivo muy útil para entender el orden de ejecución [Pág. 9–11]:

1. Si se crea una `Task` con `new Task(...)` y se llama `.Start()` sin esperarla, el programa puede imprimir **"Ha terminado" antes que "La tarea interna"**: el hilo principal sigue de largo sin esperar a que la tarea (que hace `Thread.Sleep(1000)`) termine [Pág. 9].
2. Agregar `Console.ReadLine()` al final solo evita que el proceso cierre — no cambia el orden, la tarea interna igual puede imprimir después [Pág. 10].
3. La forma correcta de esperar es `await task;`: ahí sí "La tarea interna" se imprime **antes** de "Ha terminado", porque el método se suspende hasta que la tarea completa [Pág. 11].

🟠 **Complementario:** esto es la demostración práctica de por qué hace falta `await` — sin él, el código sigue de largo sin ninguna garantía de orden entre lo que la tarea hace y lo que sigue del método.

---

## 4. `async` y `await`

🟢 **`async`** [Pág. 15]:
- Se usa en métodos que retornan `void`(\*), `Task` o `Task<Result>`.
- No cambia la firma pública del método (los parámetros y el nombre son iguales).
- Le indica al compilador que trate `await` como palabra reservada dentro de ese método, de forma similar a `unsafe`.

🟢 **`await`** [Pág. 15]:
- Marca un punto de posible suspensión/reanudación del método.
- No necesita usar `.Start()` sobre lo que se espera.

🟢 (\*) **"no hacer *async void*"** [Pág. 15] — excepto en el caso puntual de manejadores de eventos de UI [Pág. 36].

### Ejemplo progresivo: desayuno síncrono vs. asíncrono

🟢 La diapositiva usa la metáfora oficial de preparar un desayuno (café, huevos, panceta, tostadas, jugo) para mostrar la evolución [Pág. 16–19]:

1. **Todo síncrono:** cada paso espera al anterior, tarda la suma de todos los tiempos (ej. 30 min. en total) [Pág. 16].
2. **Con `await` secuencial** (`await FryEggsAsync(2)`, después `await FryBaconAsync(3)`, etc., uno detrás de otro): mejora poco, porque se sigue esperando cada tarea antes de arrancar la siguiente [Pág. 17].
3. **Lanzando las tareas primero y recién después haciendo `await`** (`var eggsTask = FryEggsAsync(2); var baconTask = ...; var toastTask = ...;` y luego `await eggsTask`, `await baconTask`, `await toastTask`): los huevos, la panceta y la tostada se cocinan **en simultáneo**, reduciendo el tiempo total real [Pág. 18].
4. **Con `Task.WhenAny` en un bucle:** se reacciona a la tarea que termine primero, sin importar el orden, sacándola de la lista y esperando a la siguiente que complete [Pág. 19].

🟠 **Complementario:** el punto clave del ejemplo es que **dónde se coloca el `await`** cambia todo — si se espera cada tarea apenas se crea, se pierde el paralelismo; si se crean todas las tareas primero y se esperan después, corren a la vez.

### Autoevaluación rápida
- En el ejemplo del desayuno, ¿por qué la versión con `await` secuencial (paso 2) no es mucho más rápida que la versión totalmente síncrona?

---

## 5. `Task.WaitAll` vs. `Task.WhenAll`, y `Task.WhenAny`

🟢 Tabla de reemplazos recomendados [Pág. 36]:

| Usar (no bloqueante) | En vez de (bloqueante) | Cuándo |
|---|---|---|
| `await` | `Task.Wait` o `Task.Result` | Devolver el resultado de una tarea en background |
| `await Task.WhenAny` | `Task.WhenAny` (sin `await`) | Esperar que se complete **alguna** tarea de una lista |
| `await Task.WhenAll` | `Task.WaitAll` | Esperar que **todas** las tareas de una lista se completen |
| `await Task.Delay` | `Thread.Sleep` | Esperar un intervalo de tiempo sin bloquear el hilo |

🟠 **Complementario:** la idea general de la tabla es siempre la misma — cualquier versión bloqueante (`Wait`, `Result`, `Sleep`) congela el hilo que la llama; la versión con `await` libera el hilo mientras se espera.

### Autoevaluación rápida
- ¿Por qué `Task.WhenAny` sin `await` no tiene sentido en código asíncrono?

---

## 6. Ejecutar tareas en paralelo real: `Parallel` y PLINQ

🟢 **Definición de procesamiento en paralelo**, cita textual: *"Doing lots of work by dividing it up among multiple threads that run concurrently. [it] uses multithreading to maximize the use of multiple processor cores"* — Stephen Cleary, *Concurrency in C# Cookbook*, O'Reilly Media [Pág. 29].

### `Parallel.For` / `Parallel.ForEach`

🟢 Equivalente paralelo de un bucle `foreach` tradicional [Pág. 30]:

```csharp
// Sequential version
foreach (var item in sourceCollection)
{
    Process(item);
}

// Parallel equivalent
Parallel.ForEach(sourceCollection, item => Process(item));
```

🟢 Ejemplo con `Parallel.For` sobre un array de archivos, usando `Interlocked.Add` para acumular un total de forma segura entre hilos [Pág. 30]. Ejemplo con `Parallel.ForEach` sobre una lista de sitios, usando `lock` para proteger el agregado a una lista compartida (`pingReplies`) [Pág. 31].

### `Parallel.Invoke`

🟢 La presentación 2025 agrega `Parallel.Invoke`, útil cuando ya se conocen varias acciones independientes y se quieren ejecutar simultáneamente sin modelarlas como un bucle:

```csharp
Parallel.Invoke(
    () => DoTask("Tarea A", 1000),
    () => DoTask("Tarea B", 1500),
    () => DoTask("Tarea C", 500)
);
```

`Parallel.Invoke` es para trabajo **CPU-bound y sincrónico** que puede repartirse entre hilos. No reemplaza `Task.WhenAll` en operaciones de E/S asíncronas.

### PLINQ (`AsParallel()`)

🟢 Método de extensión LINQ que paraleliza automáticamente una consulta [Pág. 32]:

```csharp
var evenNums = from num in source.AsParallel()
               where num % 2 == 0
               select num;
```

🟢 `WithDegreeOfParallelism(n)` limita la cantidad de hilos usados; `AsOrdered()` preserva el orden original de la colección, a costa de algo de paralelismo [Pág. 33].

### Autoevaluación rápida
- ¿Para qué sirve `Interlocked.Add` en el ejemplo de `Parallel.For`, y qué pasaría si se usara una suma común (`totalSize += size`) sin protección?

---

## 7. Concurrencia vs. Paralelismo vs. Asincronía

🟢 El PDF dedica varias diapositivas a citar autores para distinguir estos tres términos [Pág. 20–29]:

> "Concurrency is the composition of independently executing processes, while parallelism is the simultaneous execution of (possibly related) computations. Concurrency is about dealing with lots of things at once. Parallelism is about doing lots of things at once."
> — Rob Pike (co-creador de Go), conferencia *"Concurrency is not parallelism"*, 2013 [Pág. 21].

> "Concurrency is about structure, parallelism is about execution. Concurrency provides a way to structure a solution to solve a problem that may (but not necessarily) be parallelizable."
> — Rob Pike, misma conferencia [Pág. 22].

> "Concurrency is when the execution of two or more pieces of code act as if they run at the same time. Parallelism is when they do run at the same time."
> — *The Pragmatic Programmer*, Capítulo 6: Concurrency [Pág. 23].

> "Asynchronous programming: a form of concurrency that uses futures or callbacks to avoid unnecessary threads. A future (or promise) is a type that represents some operation that will complete in the future [...] it doesn't block the original thread."
> — Stephen Cleary, *Concurrency in C# Cookbook*, O'Reilly Media [Pág. 26].

🟢 **Síntesis de la propia diapositiva** [Pág. 24]:

| Concepto | Definición oficial |
|---|---|
| **Paralelismo** | Tareas que se ejecutan al mismo tiempo — están activas simultáneamente (necesita más de un núcleo real) |
| **Concurrencia** | Tareas que se ejecutan en el mismo período de tiempo — están "lógicamente" activas al mismo tiempo (puede ser un solo núcleo alternando) |

🟢 La diapositiva 25 grafica esto: todo programa concurrente puede ser paralelo o no (`Concurrent, non-parallel Execution` vs. `Concurrent, parallel Execution`); los programas paralelos son un subconjunto de los concurrentes, que a su vez son un subconjunto de "Programs" en general [Pág. 25].

### Ejemplos oficiales dados en la diapositiva [Pág. 27]

| # | Ejemplo | Categoría según la fuente |
|---|---|---|
| 1 | Servidor web que atiende múltiples solicitudes, se comunica con BD y API de terceros | Concurrencia |
| 2 | App de escritorio que descarga un archivo mientras el usuario sigue usando la interfaz | Asincronía |
| 3 | Entrenar un modelo de machine learning | Paralelismo |
| 4 | App de edición de fotos que aplica filtros a varias imágenes | Paralelismo |
| 5 | App de mensajería que revisa mensajes, actualiza estado de contactos y sincroniza archivos al mismo tiempo | Concurrencia |
| 6 | Multiplicar matrices | Paralelismo |

🟠 **Complementario:** la diapositiva lista los 6 ejemplos juntos bajo el título "Concurrencia / Paralelismo / Asincronía" sin etiquetar explícitamente cada uno con su categoría — la columna de la derecha en la tabla de arriba es una lectura razonable según las definiciones de la misma diapositiva, no una etiqueta textual de la fuente. **Pendiente de confirmar** contra la clase si el docente los categorizó de otra forma.

### Autoevaluación rápida
- Con tus palabras (no citando a Rob Pike de memoria): ¿por qué puede haber concurrencia sin paralelismo, pero no paralelismo sin concurrencia?

---

## 8. `CancellationToken`

🟢 **Qué es:** mecanismo que "indica que una operación debe ser cancelada" [Pág. 34].

🟢 Uso mostrado en la fuente [Pág. 34]:

```csharp
static void Main()
{
    // Create CancellationTokenSource
    var source = new CancellationTokenSource();
    var token = source.Token;

    var task = Task.Run(() => DoSomething(token), token);

    Thread.Sleep(500);
    source.Cancel(); // Afecta a los CancellationTokens creados desde source
    Thread.Sleep(500);
}

static void DoSomething(CancellationToken token)
{
    for (int i = 0; i < 100; i++)
    {
        Thread.Sleep(100);
        if (token.IsCancellationRequested)
        {
            Console.WriteLine("Method1 canceled");
            return;
        }
        Console.WriteLine($"Method1 running... {i}");
    }
}
```

🟠 **Complementario:** el token no cancela nada por sí solo — es la propia tarea la que, dentro de su lógica, tiene que consultar `IsCancellationRequested` periódicamente y decidir cortar. Si el método nunca revisa el token, `source.Cancel()` no tiene ningún efecto sobre él (cancelación **cooperativa**, no forzada).

🟢 **Buenas prácticas de la presentación 2025:** pasar el token a métodos largos o asíncronos; comprobarlo dentro de bucles; preferir `ThrowIfCancellationRequested()` cuando corresponda; capturar `OperationCanceledException` para tratar la cancelación esperada; no asumir que `Cancel()` detiene la tarea instantáneamente; y pasar el token a operaciones cancelables como `Task.Delay(1000, token)`.

---

## 9. Escenarios de uso y buenas prácticas

🟢 **Escenarios comunes donde aplica la programación asíncrona** [Pág. 35]:
- Procesos que involucran E/S (I/O).
- Tareas que involucran alto procesamiento de recursos.

🟢 **Notas importantes sobre `async`/`await`** [Pág. 36–37]:
- Todo método definido con `async` **debe** tener al menos un `await` en su cuerpo.
- `async void` se usa **solo** en manejadores de eventos (ej. click de un botón).
- Hay que escribir código que espere tareas de forma no bloqueante (ver tabla de la sección 5).
- Convención de nombres en .NET: agregar el sufijo `Async` al nombre de un método asíncrono.
- Cuidado con usar `.ConfigureAwait(false/true)` sin entender el motivo.

🟢 **Diseño de funciones asíncronas** — pasos recomendados por la fuente [Pág. 39]:
1. Escribir el método de forma sincrónica primero.
2. Reemplazar las llamadas a métodos sincrónicos por llamadas a sus equivalentes asincrónicos, esperándolas con `await`.
3. Salvo en el manejador de eventos de nivel superior (top level event handler), actualizar el tipo de retorno del método a `Task` o `Task<Result>` para que esté disponible para quien lo llame.

### Autoevaluación rápida
- ¿Por qué la regla dice "todo método `async` debe tener un `await`"? ¿Qué sentido tendría un método `async` sin ningún `await` adentro?

---

## Resumen ejecutivo del tema

1. La TPL (`Task`) resuelve el costo de crear hilos manualmente reutilizando un `ThreadPool` con auto-tuning, y agrega control sobre estado, valor de retorno, cancelación, excepciones y continuations que un `Thread` crudo no tiene.
2. `async` marca el método y habilita `await` dentro de él; `await` es un punto de suspensión/reanudación que no bloquea el hilo — el orden real de ejecución depende de **dónde** se coloca el `await` respecto de cuándo se crea la tarea (ejemplo del desayuno).
3. Para esperar varias tareas: `await Task.WhenAll` (todas) o `await Task.WhenAny` (la primera que termine) reemplazan a las versiones bloqueantes `Task.WaitAll` y `Task.Wait`/`Task.Result`.
4. Paralelismo real de CPU: `Parallel.For`/`ForEach` y PLINQ (`AsParallel()`) dividen trabajo entre hilos que corren simultáneamente; conviene protegerlos con `Interlocked` o `lock` si acumulan resultados compartidos.
5. Concurrencia ≠ Paralelismo ≠ Asincronía: concurrencia es sobre **estructura** (tareas lógicamente activas a la vez), paralelismo es sobre **ejecución** (tareas físicamente activas a la vez, necesita múltiples núcleos), y asincronía es una forma de concurrencia basada en futures/callbacks que evita hilos innecesarios.
6. `CancellationToken` permite pedir la cancelación de una tarea de forma cooperativa: la tarea tiene que sondear `IsCancellationRequested` por su cuenta.
7. Reglas de diseño: todo `async` necesita un `await`; `async void` solo para manejadores de eventos; devolver `Task`/`Task<Result>` en vez de `void` en el resto de los casos.

---

## Repaso espaciado

### Día 3 (autoevaluación sin mirar el resumen)
1. Nombrá 3 de los 6 beneficios de usar `Task` en vez de `Thread` (según la fuente: status, return values, cancellation, exception handling, scheduling, continuation).
2. En el ejemplo del desayuno, ¿qué diferencia el paso 2 (await secuencial) del paso 3 (crear todas las tareas primero)?
3. Completá la tabla: `await Task.WhenAll` reemplaza a ____; `await Task.Delay` reemplaza a ____.
4. Citá (con tus palabras) la diferencia entre concurrencia y paralelismo según Rob Pike.
5. ¿Qué significa que la cancelación con `CancellationToken` sea "cooperativa"?

### Día 7 (repaso final antes de mezclar con otros temas)
1. Explicá con un diagrama mental (o dibujado) el flujo completo del ejemplo `Task`/`await` de las diapositivas 9 a 11: qué imprime primero en cada versión y por qué.
2. Diseñá de cero (con los 3 pasos oficiales) una función asíncrona que descargue un archivo y lo guarde en disco.
3. ¿Cuándo usarías `Parallel.For` y cuándo `async`/`await` para el mismo volumen de trabajo? Dá un ejemplo de cada escenario.
4. Repasá los 6 ejemplos de la sección 7 sin mirar la tabla y clasificalos vos mismo en concurrencia/paralelismo/asincronía.

---

## Pendiente de confirmar

- "Precedencia explícita" y "colecciones autosincronizables" se conservan como referencia del mapa anterior, no como pendientes del temario vigente.
- La categorización de los 6 ejemplos de la sección 7 en concurrencia/paralelismo/asincronía es una lectura razonada de las definiciones de la propia diapositiva, no una etiqueta textual de la fuente — está marcada como 🟠 complementario, no como equivalencia oficial.
- 🔵 Evidencia de parciales: Task/asincronía/paralelismo aparece en 14 de 16 letras históricas revisadas. Ver `../../PARCIALES/ANALISIS_DE_FRECUENCIA.md`.
- No hay pptx adicional de "Latorres" todavía integrado a `03_PARALELISMO/` (el `ROADMAP.md` lo menciona como pendiente de mover desde el inbox) — si aparece, revisar si agrega contenido nuevo sobre precedencia explícita o colecciones autosincronizables.
