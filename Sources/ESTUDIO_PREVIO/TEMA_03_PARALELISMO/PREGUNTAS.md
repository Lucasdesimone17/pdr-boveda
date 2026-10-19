# Preguntas de autoevaluación — TEMA 3: Paralelismo (PDR)

Fuente: 🟢 `Asincronía y paralelismo.pdf` (oficial). Intentá responder antes de abrir la pista.

## Fundamentos y TPL

**1.** ¿Por qué la diapositiva dice que crear hilos manualmente con `System.Threading.Thread` "es muy costosa computacionalmente"? ¿Qué problema resuelve el Thread Pool?

<details><summary>Pista</summary>Pensá en el costo de crear/destruir recursos del sistema operativo por cada tarea chica, versus reutilizar un conjunto ya creado.</details>

**2.** Nombrá al menos 4 de los 6 beneficios que la fuente atribuye a `Task` frente a manejar hilos "a mano".

<details><summary>Pista</summary>Están agrupados como "Más control:" en la diapositiva de "Tasks".</details>

## Task vs Thread

**3.** Según la tabla oficial de comparación, ¿qué puede hacer una `Task` que un `Thread` no puede hacer directamente?

<details><summary>Pista</summary>Hay tres diferencias clave: valor de retorno, cancelación y continuations.</details>

## async / await

**4.** ¿Qué regla obligatoria establece la fuente sobre todo método declarado `async`?

<details><summary>Pista</summary>Es una condición sobre lo que tiene que haber, sí o sí, dentro del cuerpo del método.</details>

**5.** En el ejemplo del desayuno (Pág. 16 a 19), explicá por qué la versión con `await` colocado inmediatamente después de cada llamada (`var eggs = await FryEggsAsync(2);` y luego `var bacon = await FryBaconAsync(3);`, etc.) no logra el mismo paralelismo que crear todas las tareas primero y esperarlas después.

<details><summary>Pista</summary>Fijate en qué momento arranca cada tarea nueva en cada versión: ¿arranca mientras la anterior sigue corriendo, o solo después de que termina?</details>

**6.** ¿Cuándo se permite usar `async void` según la fuente, y por qué en el resto de los casos se recomienda `Task`/`Task<Result>`?

<details><summary>Pista</summary>Pensá en un botón de una interfaz gráfica: ¿quién más podría necesitar "esperar" ese método?</details>

## Esperar múltiples tareas

**7.** Completá la correspondencia según la tabla oficial (Pág. 36): `await Task.WhenAll` reemplaza a ____; `await Task.WhenAny` reemplaza a ____ (sin await); `await Task.Delay` reemplaza a ____.

<details><summary>Pista</summary>Las tres respuestas son formas bloqueantes de hacer lo mismo: `Task.WaitAll`, `Task.WhenAny` sin await, y `Thread.Sleep`.</details>

## Paralelismo real (Parallel / PLINQ)

**8.** En el ejemplo de `Parallel.For` que suma el tamaño de varios archivos, ¿para qué se usa `Interlocked.Add(ref totalSize, size)` en vez de simplemente `totalSize += size`?

<details><summary>Pista</summary>Varios hilos están escribiendo la misma variable `totalSize` al mismo tiempo — es el mismo problema del Tema 2 (condición de carrera), aplicado acá.</details>

**9.** ¿En qué escenario usarías `Parallel.ForEach`/PLINQ y en cuál usarías `async`/`await`, según los ejemplos y definiciones de la fuente (procesos de CPU vs. procesos de E/S)?

<details><summary>Pista</summary>Repasá la sección "Escenarios comunes donde aplica la programación asíncrona" (Pág. 35).</details>

## Concurrencia vs. Paralelismo vs. Asincronía

**10.** Citá (con tus palabras, no memorizado) la distinción de Rob Pike entre concurrencia y paralelismo, y dala vuelta con un ejemplo propio (no de la diapositiva).

<details><summary>Pista</summary>"Concurrency is about dealing with lots of things at once. Parallelism is about doing lots of things at once." Pensá en algo cotidiano, no de programación.</details>

**11.** De los 6 ejemplos que trae la diapositiva 27 (servidor web, descarga de archivo en background, entrenar un modelo de ML, editar fotos en batch, app de mensajería, multiplicar matrices), elegí dos y explicá por qué uno es más "concurrencia" y el otro más "paralelismo" según las definiciones de la Pág. 24.

<details><summary>Pista</summary>Preguntate: ¿esas tareas están *físicamente* activas a la vez (necesitan varios núcleos), o solo *lógicamente* activas dentro del mismo período de tiempo?</details>

## CancellationToken

**12.** ¿Por qué se dice que la cancelación con `CancellationToken` es "cooperativa"? ¿Qué pasa si el método que corre en la `Task` nunca revisa `token.IsCancellationRequested`?

<details><summary>Pista</summary>`source.Cancel()` no "mata" nada por sí solo — solo cambia un valor que alguien tiene que estar mirando.</details>

---

## Respuestas completas

<details>
<summary>Ver todas las respuestas</summary>

**1.** Crear y destruir un `Thread` implica pedirle recursos al sistema operativo cada vez (asignar pila, registrar el hilo en el scheduler, etc.), lo cual tiene un costo real medible. El Thread Pool resuelve esto manteniendo un conjunto de hilos ya creados: las tareas nuevas se encolan (`Task Queue`) y un hilo libre del pool toma la siguiente apenas se desocupa, evitando el costo de crear un hilo del SO por cada trabajo chico.

**2.** Según la Pág. 8: auto-tuning del ThreadPool, control de Status, Return values, Cancellation, Exception handling, Scheduling y Continuation (basta con nombrar 4 de estas 6).

**3.** Una `Task` puede devolver un valor directamente (`Task<T>`), soporta cancelación cooperativa vía `CancellationToken`, y permite encadenar continuations (código que corre automáticamente después según cómo terminó la tarea: `OnlyOnCanceled`, `OnlyOnFaulted`, `OnlyOnRanToCompletion`). Un `Thread` crudo no tiene ninguno de estos tres mecanismos de forma nativa.

**4.** Todo método definido con `async` debe tener al menos un `await` en su cuerpo. Si no lo tiene, no hay ningún punto de suspensión real y el `async` pierde su sentido (el compilador suele advertir sobre esto).

**5.** Cuando se hace `var eggs = await FryEggsAsync(2);` inmediatamente, el método se suspende ahí mismo hasta que esa tarea puntual termine, y recién después arranca `FryBaconAsync(3)`. Es decir, las tareas siguen ejecutándose una detrás de la otra, aunque cada una sea "asíncrona" individualmente. En cambio, si se guardan las tres tareas en variables (`var eggsTask = ...; var baconTask = ...; var toastTask = ...;`) sin `await` todavía, las tres arrancan casi al mismo tiempo, y recién después se hace `await` sobre cada una — ahí sí corren en paralelo real y el tiempo total se reduce.

**6.** `async void` se permite únicamente en manejadores de eventos (por ejemplo, el evento de click de un botón), porque los eventos en .NET tienen firma `void` por diseño y no hay forma de que el framework les pida devolver una `Task`. En el resto de los casos se recomienda `Task`/`Task<Result>` porque eso permite que quien llama al método pueda hacer `await` sobre él, encadenar continuations, capturar excepciones correctamente y saber cuándo terminó — nada de eso es posible con `async void`.

**7.** `await Task.WhenAll` reemplaza a `Task.WaitAll`; `await Task.WhenAny` reemplaza a `Task.WhenAny` usado sin `await`; `await Task.Delay` reemplaza a `Thread.Sleep`.

**8.** Varios hilos ejecutando el cuerpo de `Parallel.For` acceden a la misma variable `totalSize` al mismo tiempo. Si se usara `totalSize += size` sin protección, dos hilos podrían leer el mismo valor viejo de `totalSize`, sumarle su `size` por separado, y el último en escribir pisaría el resultado del otro — perdiendo una de las dos sumas (condición de carrera, mismo problema que exclusión mutua del Tema 2). `Interlocked.Add` hace la operación de sumar y guardar de forma atómica, sin que otro hilo pueda intercalarse en el medio.

**9.** `Parallel.ForEach`/PLINQ conviene para trabajo intensivo de CPU que se puede dividir en partes independientes (ej. procesar muchas imágenes, hacer cálculos matemáticos sobre una colección grande) — ahí el paralelismo real usando varios núcleos acelera el trabajo. `async`/`await` conviene para trabajo de E/S (llamadas de red, acceso a disco, consultas a bases de datos) — en ese caso el cuello de botella no es la CPU sino la espera de una respuesta externa, y usar `Parallel.For` no acelera nada porque el hilo igual queda esperando una operación externa; lo que se gana con `async`/`await` es no bloquear el hilo mientras se espera esa respuesta.

**10.** Rob Pike distingue: la concurrencia es sobre cómo se **estructura** un problema en piezas que se pueden manejar de forma independiente (pueden estar "lógicamente" activas al mismo tiempo, alternándose en un solo núcleo); el paralelismo es sobre la **ejecución simultánea real** de esas piezas (activas físicamente al mismo tiempo, en núcleos distintos). Un ejemplo cotidiano: una persona que cocina mientras atiende el teléfono y revisa el horno cada tanto es "concurrente" (va turnándose entre tareas); dos cocineros distintos preparando cada uno un plato distinto al mismo tiempo en la misma cocina son "paralelos" (ambos activos de verdad, simultáneamente).

**11.** Ejemplo de concurrencia: la app de mensajería que revisa mensajes nuevos, actualiza estado de contactos y sincroniza archivos "al mismo tiempo" — estas tareas están lógicamente activas en el mismo período, pero no necesariamente requieren múltiples núcleos ejecutándose en el mismo instante exacto; muchas veces se resuelven alternando o esperando E/S. Ejemplo de paralelismo: multiplicar matrices — es un trabajo intensivo de CPU que se puede dividir en partes independientes (por ejemplo, cada hilo calcula una fila del resultado) y que se beneficia directamente de tener varios núcleos trabajando literalmente al mismo tiempo.

**12.** Es "cooperativa" porque `CancellationTokenSource.Cancel()` no interrumpe la ejecución de la tarea a la fuerza — solo cambia el estado interno del token a "cancelación solicitada". Es responsabilidad del propio código que corre dentro de la tarea consultar periódicamente `token.IsCancellationRequested` y decidir cortar su propia ejecución (como en el ejemplo `DoSomething`, que chequea el token dentro de su bucle `for`). Si el método nunca hace esa consulta, la tarea sigue corriendo hasta el final sin importar que se haya llamado a `Cancel()` — la cancelación queda sin efecto.

</details>
