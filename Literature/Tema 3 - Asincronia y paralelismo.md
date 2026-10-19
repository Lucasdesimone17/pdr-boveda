---
date: 2026-08-26
type: literature
tags:
  - literature
  - pdr
  - tema-3
  - task
  - async
  - csharp
ai-first: true
source: "Sources/TEORICO/03_PARALELISMO/"
source_extra:
  - "Sources/ESTUDIO_PREVIO/TEMA_03_ASINCRONIA_Y_PARALELISMO/CHEATSHEET.md"
confidence: stated
semanas: "7-8"
frecuencia_parcial: "14/16 (88%)"
---

# Tema 3 - Asincronia y paralelismo

## For future agent
Nota de tema 3 del temario vigente (semanas 7-8): la TPL, `Task`, `async`/`await`, `Parallel`/PLINQ y cancelación cooperativa. Frecuencia histórica de parcial: **14 de 16 letras (88%)**. Lo que se pregunta: `Task` vs `Thread`, por qué el **orden del `await`** cambia el resultado, y CPU-bound vs I/O-bound. Fuentes: `Asincronía y paralelismo.pdf` y la presentación `04 Asincronía y paralelismo 2025.pptx`.

## Source
`Sources/TEORICO/03_PARALELISMO/` - `Asincronía y paralelismo.md`, `04 Asincronía y paralelismo 2025.md`.

## Key Claims

### Por qué existe la TPL
Crear y destruir `Thread` a mano es costoso. Una `Task` se encola en un `ThreadPool` reutilizable con auto-tuning. La `Task` **no es un hilo dedicado**: confundir las dos cosas está en la lista de errores caros.

### Task vs Thread (tabla oficial)
| Aspecto | `Thread` | `Task` |
|---|---|---|
| Nivel | Recursos del SO | Alto nivel (TPL) |
| Retorna valor | No | Sí |
| Cancelación | No | `CancellationToken` |
| Concurrencia interna | Una cosa a la vez | Continuations múltiples |
| `async`/`await` | - | Directo |

### Crear tareas
- `Task t = new Task(PrintInfo); t.Start();` - dos pasos.
- `Task t = Task.Run(() => PrintInfo());` - uno.
- `Task.Delay(300)` es el equivalente asíncrono de `Thread.Sleep`.
- Continuations: `task.ContinueWith(i => {...}, TaskContinuationOptions.OnlyOnFaulted)` (también `OnlyOnCanceled` y `OnlyOnRanToCompletion`).

### Reglas de `async`/`await`
- `async` aplica a métodos que devuelven `void`, `Task` o `Task<T>`; **no cambia la firma**. `async void` solo para manejadores de eventos.
- Todo método `async` **debe** contener un `await`. Convención: sufijo `Async` en el nombre.
- `await` es un punto de suspensión y reanudación; con `await` no se usa `.Start()`.

### Los tres términos que se confunden
| Término | Definición del material |
|---|---|
| Paralelismo | Tareas activas **al mismo tiempo** (físicamente; requiere al menos un núcleo por tarea) |
| Concurrencia | Tareas activas **en el mismo período** (lógicamente; puede ser un núcleo alternando) |
| Asincronía | Forma de concurrencia con futures/callbacks que evita hilos innecesarios (Cleary) |

Relación de conjuntos: `programas paralelos ⊂ programas concurrentes ⊂ programas`.

### El orden del `await` decide el resultado (ejemplo del desayuno)
| Patrón | Resultado |
|---|---|
| Todo síncrono | Suma de todos los tiempos |
| `await` inmediato, uno por uno | Casi no mejora: sigue siendo secuencial |
| Crear todas las tareas primero y `await` después | Corren en paralelo, tiempo real reducido |
| `await Task.WhenAny` en un ciclo | Reacciona a la primera que termina, sin importar el orden |

Este es el ejercicio de lectura de código más repetido del tema.

### Bloqueante a no bloqueante
| Usar | En vez de |
|---|---|
| `await` | `Task.Wait` / `Task.Result` |
| `await Task.WhenAny` | `WhenAny` sin await |
| `await Task.WhenAll` | `Task.WaitAll` |
| `await Task.Delay` | `Thread.Sleep` |

### Parallel y PLINQ
| Herramienta | Sintaxis | Protección de datos compartidos |
|---|---|---|
| `Parallel.For` | `Parallel.For(0, n, i => {...})` | `Interlocked.Add(ref total, size)` |
| `Parallel.ForEach` | `Parallel.ForEach(items, item => {...})` | `lock (obj) { ... }` |
| `Parallel.Invoke` | `Parallel.Invoke(() => A(), () => B())` | Acciones CPU-bound independientes |
| PLINQ | `source.AsParallel().Where(...).Select(...)` | `.WithDegreeOfParallelism(n)`, `.AsOrdered()` |

### Cancelación cooperativa
```csharp
var source = new CancellationTokenSource();
var token = source.Token;
var task = Task.Run(() => DoSomething(token), token);
source.Cancel(); // solo pide, no fuerza

// dentro de DoSomething:
if (token.IsCancellationRequested) { return; }
// o bien:
token.ThrowIfCancellationRequested();
```
Si el método nunca chequea el token, `Cancel()` no tiene efecto. Pasar el token también a las APIs cancelables: `await Task.Delay(1000, token)`.

### Diseñar una función asíncrona (3 pasos del material)
1. Escribir el método síncrono.
2. Reemplazar las llamadas síncronas por sus versiones async con `await`.
3. Cambiar el tipo de retorno a `Task`/`Task<T>`, salvo en el manejador de evento de más arriba.

## Methodology
Teórico oficial con el ejemplo canónico del desayuno para mostrar el efecto del orden del `await`, más tablas de equivalencias bloqueante/no bloqueante.

## Critique
- El material no cierra la regla **CPU-bound vs I/O-bound** con una frase única, aunque el parcial la pregunta: `Task.Run` sirve para CPU-bound, mientras que para I/O-bound conviene la API async nativa sin ocupar un hilo del pool (inferido del contenido, no citado literal - confidence: medium).
- `async void` se menciona como excepción para eventos sin explicar el problema real: las excepciones no se pueden capturar desde el llamador.
- No trata `ConfigureAwait` ni contextos de sincronización.

## Dicho en clase

### 2026-09-22 ([[Clase 2026-09-22 - Parallel, CancellationToken y cuando usar async]])
- La asincronia **no implica paralelismo**: da la percepcion de simultaneidad. Es una abstraccion sobre hilos que no se bloquean en E/S. **El `await` bloquea la tarea, no el hilo.** (stated)
- `Parallel.For` va **de inicio a fin menos uno** (`toExclusive`). `Interlocked.Add` es la exclusion mutua para acumular. `Parallel` es **de mas bajo nivel que `Task`** y sirve para procesos cortos. (stated)
- `CancellationToken`:
  - **Un token cancelado no se reutiliza.**
  - Un source da muchos tokens.
  - Las APIs async de sockets aceptan el token y **salen por excepcion** si estaban bloqueadas.
  - `catch (OperationCanceledException)`.
  - No asumir que la tarea termina al instante.
  - Cierre del servidor: cancelar a todos y `Task.WhenAll`, y recien despues persistir. (stated)
- **Tokens enlazados** (`CreateLinkedTokenSource`): una tarea hija se cancela si cancela el servidor **o** si el cliente cancela su pedido. Cancelar la hija no cancela al padre. Este caso **no esta en las diapositivas**. (stated)
- **No hacer todo `async` por reflejo:** solo E/S y tareas pesadas. (stated)

### 2026-10-08 ([[Clase 2026-10-08 - Letra del Obligatorio 2 y patron RPC sobre RabbitMQ]])
- **Truco de `TaskCompletionSource`:** un metodo que devuelve una `Task` armada a mano **no se declara `async`**. Se crea el `TaskCompletionSource` con `RunContinuationsAsynchronously`, se devuelve `tcs.Task` y se completa cuando llega la respuesta. *"Si ponen async eso les va a fallar"*. (stated)
- `ConcurrentDictionary` alcanza mientras se toque un solo diccionario. Si una operacion toca varios que deben quedar consistentes, hace falta otro bloqueo. (stated)
- Este tema es el nucleo del [[Obligatorio 2 - Upgrade asincrono y Docker de EDISON]]: migrar de `Thread` a `Task` y cancelar reservas con `CancellationToken`.

## Connections
- Reemplaza el modelo de [[Tema 1 - Programacion multihilo]]; el arco completo en [[Concurrencia de Thread a Task]].
- Aplica directamente a [[Tema 2 - Sockets y protocolos]] (`AcceptAsync`, `ReadAsync`).
- Formato de examen: [[Patrones de examen en Programacion de Redes]] · [[Errores caros en Programacion de Redes]].
- Acta de clase donde se dicto `Task`, `ContinueWith` y TPL: [[Clase 2026-09-17 - Task en detalle, continuations, ThreadPool y TPL]].
