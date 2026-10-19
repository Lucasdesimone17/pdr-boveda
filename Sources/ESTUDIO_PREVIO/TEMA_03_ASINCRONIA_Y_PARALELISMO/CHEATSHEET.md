# Cheat Sheet — TEMA 3: Asincronía y Paralelismo (PDR)

Fuente: 🟢 `Asincronía y paralelismo.pdf` (oficial). Sin prosa, solo tablas/sintaxis de referencia rápida.

## Motivo de existir de la TPL

| Problema | Solución TPL |
|---|---|
| Crear/destruir `Thread` a mano es costoso computacionalmente | `Task` se encola en un `ThreadPool` reutilizable con auto-tuning |

## Crear una Task

| Sintaxis | Nota |
|---|---|
| `Task t1 = new Task(PrintInfo); t1.Start();` | Crear y arrancar en 2 pasos |
| `Task t3 = Task.Run(() => { PrintInfo(); });` | Crear e iniciar en 1 paso |
| `Task.Delay(300)` | Equivalente asíncrono de `Thread.Sleep` |

## Continuations

```csharp
task.ContinueWith(i => {...}, TaskContinuationOptions.OnlyOnCanceled);
task.ContinueWith(i => {...}, TaskContinuationOptions.OnlyOnFaulted);
task.ContinueWith(i => {...}, TaskContinuationOptions.OnlyOnRanToCompletion);
```

## Task vs Thread (tabla oficial)

| Aspecto | Thread | Task |
|---|---|---|
| Nivel | Recursos del SO | Alto nivel (TPL) |
| Retorna valor | No | Sí |
| Cancelación | No | `CancellationToken` |
| Concurrencia interna | 1 cosa a la vez | Continuations múltiples |
| async/await | — | Muy fácil |

## async / await

| Elemento | Regla |
|---|---|
| `async` | Métodos que retornan `void`(\*), `Task`, `Task<Result>`. No cambia la firma. |
| `await` | Punto de suspensión/reanudación. No usa `.Start()`. |
| (\*) `async void` | Solo en manejadores de eventos (ej. click de botón) |
| Regla dura | Todo método `async` **debe** tener un `await` en el cuerpo |
| Convención | Sufijo `Async` en el nombre del método |

## Orden del `await` cambia el resultado (ejemplo del desayuno)

| Patrón | Resultado |
|---|---|
| Todo síncrono | Suma de todos los tiempos |
| `await` inmediato, uno por uno | Casi no mejora — sigue siendo secuencial |
| Crear todas las tareas primero, `await` después | Corren en paralelo — tiempo real reducido |
| `await Task.WhenAny` en loop | Reacciona a la que termine primero, sin importar orden |

## Bloqueante → No bloqueante (tabla oficial, Pág. 36)

| Usar | En vez de | Para qué |
|---|---|---|
| `await` | `Task.Wait` / `Task.Result` | Resultado de una tarea en background |
| `await Task.WhenAny` | `Task.WhenAny` sin await | Esperar la primera tarea que termine |
| `await Task.WhenAll` | `Task.WaitAll` | Esperar todas las tareas |
| `await Task.Delay` | `Thread.Sleep` | Esperar un intervalo sin bloquear |

## Parallel / PLINQ

| Herramienta | Sintaxis | Protección de datos compartidos |
|---|---|---|
| `Parallel.For` | `Parallel.For(0, n, i => {...})` | `Interlocked.Add(ref total, size)` |
| `Parallel.ForEach` | `Parallel.ForEach(items, item => {...})` | `lock (obj) { ... }` |
| `Parallel.Invoke` | `Parallel.Invoke(() => A(), () => B())` | Acciones CPU-bound independientes |
| PLINQ | `source.AsParallel().Where(...).Select(...)` | `.WithDegreeOfParallelism(n)`, `.AsOrdered()` |

## Concurrencia vs Paralelismo vs Asincronía

| Término | Definición oficial (síntesis Pág. 24) |
|---|---|
| Paralelismo | Tareas activas **al mismo tiempo** (físicamente, necesita ≥1 núcleo por tarea) |
| Concurrencia | Tareas activas **en el mismo período** (lógicamente, puede ser 1 núcleo alternando) |
| Asincronía | Forma de concurrencia con futures/callbacks, evita hilos innecesarios (Stephen Cleary) |

Relación de conjuntos (Pág. 25): `Parallel programs ⊂ Concurrent programs ⊂ Programs`.

## CancellationToken

```csharp
var source = new CancellationTokenSource();
var token = source.Token;
var task = Task.Run(() => DoSomething(token), token);
source.Cancel(); // Solo pide, no fuerza

// Dentro de DoSomething:
if (token.IsCancellationRequested) { return; }
// Alternativa que lanza OperationCanceledException:
token.ThrowIfCancellationRequested();
```

Cancelación **cooperativa**: si el método nunca chequea `IsCancellationRequested`, `Cancel()` no tiene efecto.
Pasá el token también a APIs cancelables: `await Task.Delay(1000, token)`.

## Diseño de una función asíncrona (3 pasos oficiales)

1. Escribir el método síncrono primero.
2. Reemplazar llamadas síncronas por sus versiones async + `await`.
3. Cambiar el tipo de retorno a `Task`/`Task<Result>` (excepto en el top-level event handler).

## Preguntas típicas de parcial

🔵 **Frecuencia histórica:** Task/asincronía/paralelismo aparece en 14 de 16 letras de parcial revisadas. Se repiten `Task` vs `Thread`, `async/await`, `WhenAll`, cancelación y CPU-bound vs I/O-bound.
