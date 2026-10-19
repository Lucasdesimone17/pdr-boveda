---
date: 2026-08-26
type: synthesis
tags:
  - synthesis
  - pdr
  - concurrencia
  - threads
  - task
  - csharp
ai-first: true
confidence: high
sources:
  - "[[Tema 1 - Programacion multihilo]]"
  - "[[Tema 3 - Asincronia y paralelismo]]"
  - "[[Complementario - Introduccion y Streams]]"
---

# Concurrencia de Thread a Task

## For future agent
Nota de concepto que une los temas 1 y 3 en un solo arco: por qué existe `Thread`, qué problema deja abierto, por qué aparece `Task`, y cuándo cada uno sigue siendo la respuesta correcta. Resuelve de una vez las tres confusiones que el parcial explota: concurrencia vs paralelismo vs asincronía, `Task` vs `Thread`, y CPU-bound vs I/O-bound. Entre los dos temas suman 94% y 88% de frecuencia histórica.

## Las tres palabras, ordenadas

| Término | Qué afirma | Prueba mental |
|---|---|---|
| **Paralelismo** | Tareas activas **al mismo tiempo** (físico) | Necesita al menos un núcleo por tarea |
| **Concurrencia** | Tareas activas **en el mismo período** (lógico) | Un núcleo alternando ya alcanza |
| **Asincronía** | Forma de concurrencia con futures/callbacks que **evita hilos innecesarios** | El hilo no se queda esperando |

Relación de conjuntos del material: `programas paralelos ⊂ programas concurrentes ⊂ programas`. Traducción: todo programa paralelo es concurrente, no al revés.

## La escalera

### 1. Proceso e hilo
Los hilos de un proceso comparten memoria. Eso los hace baratos comparados con procesos (cambio de contexto rápido, comunicación directa) y **al mismo tiempo** es el origen de todos los problemas: condición de carrera, región crítica, deadlock.

### 2. `Thread`: control explícito, costo explícito
`new Thread(...)`, `.Start()`, `.Join()`. El programador decide todo, y paga todo: crear y destruir hilos es costoso, y no hay valor de retorno ni cancelación.

Herramientas de coordinación, de más barata a más caras: `lock` y `Monitor` (mismo proceso, rápidas), `Mutex` y `Semaphore` del SO (cruzan procesos, lentas). El criterio real de elección es el **alcance**, no la velocidad.

### 3. `Task`: el pool y el valor de retorno
La TPL existe porque crear y destruir hilos a mano es caro. Una `Task` se encola en un `ThreadPool` reutilizable con auto-tuning, devuelve valor, acepta cancelación por `CancellationToken` y admite continuations.

**Una `Task` no es un hilo dedicado.** Confundirlas está en la lista de errores caros.

### 4. `async`/`await`: dejar de ocupar el hilo mientras se espera
El salto conceptual no es "correr más rápido", es **no bloquear**. Un hilo bloqueado esperando red o disco es un hilo desperdiciado. `await` suspende y reanuda el flujo sin ocupar el hilo del pool.

Y de ahí el ejercicio del desayuno: **crear todas las tareas primero y hacer `await` después** corre en paralelo; hacer `await` inmediato una por una es secuencial disfrazado.

### 5. `Parallel` y PLINQ: cuando el trabajo es CPU
`Parallel.For`, `Parallel.ForEach`, `Parallel.Invoke` y `AsParallel()` reparten trabajo **CPU-bound** entre núcleos. Los datos compartidos siguen necesitando protección: `Interlocked.Add` para contadores, `lock` para el resto. La concurrencia no desaparece por usar una API más alta.

## Las cuatro capas, como las presento el docente (17-09-2026)
Cada capa usa la anterior. Esto ordena toda la escalera de arriba:

| Capa | Que aporta |
|---|---|
| `Thread` | el hilo crudo; control total, costo alto |
| `ThreadPool` | reutiliza hilos, calcula cuantos conviene tener, los reparte (cola FIFO) |
| Task Parallel Library | agrupa tareas, las despacha al pool, espera por ellas, maneja excepciones |
| `async`/`await` | corta el programa en segmentos, notacion natural, promesas |

### Cuando SI usar `Thread`, textual del docente
Solo cuatro casos: hilo **foreground**; tarea **intensiva en procesador** que necesita el hilo mucho tiempo de forma continua; hace falta **darle prioridad**; **tiempo real** (leer un sensor). Para servicios y sistemas en general, programacion asincronica.

> "En un programa profesional, rara vez se usaria threads." (15-09)

### Dos diferencias que rompen codigo
- **`Thread` es foreground por defecto; `Task` es background.** Si el `Main` termina, las tareas mueren a medio camino. Hay que **esperarlas con `await`** - un `Console.ReadLine()` **no es esperar**, funciona solo porque el usuario es lento.
- **Las excepciones de `Thread` quedan en el stack de su hilo**: para avisarle al hilo principal hay que inventar una variable compartida. **`Task` las encapsula** y las **relanza en el `await`**, asi que se atrapan con `try/catch` normal.

### El modelo mental de los segmentos
El compilador corta el codigo en **segmentos** delimitados por los puntos de transicion asincronica (`await`, invocacion de tarea, `Task.Delay`), y cada segmento es un **`ContinueWith` implicito** del anterior. Todo eso se compila a una **maquina de estados** en el lenguaje interno de .NET. Consecuencia practica: el codigo parece sincronico, pero **entre tareas distintas no hay garantia de orden**; dentro de un segmento si.

Encuadre historico que dio el docente: **la programacion asincronica es planificacion no apropiativa metida adentro de un sistema apropiativo**. Los hilos del pool **no mueren ni se bloquean**, asi que se ahorra el costo de crear hilos (stack + PCB) y de cambio de contexto.

### `CancellationToken`: lo que resuelve
Con `Thread`, un hilo trancado en `Accept` **solo sale si alguien se conecta**. Con asincronia, `AcceptAsync` es awaitable y el token le **genera una excepcion** para sacarlo del bloqueo y hacer un cierre prolijo. Ese es el salto real del Obligatorio 1 al 2.

## La regla que decide

| Tipo de trabajo | Qué usar | Por qué |
|---|---|---|
| CPU-bound (calcular, comprimir, procesar imágenes) | `Task.Run`, `Parallel`, PLINQ | El trabajo necesita núcleos; sumar hilos ayuda hasta el límite físico |
| I/O-bound (red, disco, base de datos) | API async nativa con `await` | El trabajo lo hace otro; un hilo esperando no aporta nada |
| Coordinación entre hilos del mismo proceso | `lock` / `Monitor` | Rápidas, en proceso |
| Coordinación entre procesos | `Mutex` / `Semaphore` del SO | Es lo único que cruza el límite de proceso |
| Esperar terminación de un hilo clásico | `Join` | `Sleep` **no** sincroniza |

La formulación CPU-bound vs I/O-bound no aparece cerrada en una frase del material; está inferida del contenido de los temas 1 y 3 (confidence: medium), pero es exactamente lo que se pregunta.

## Dónde se cruza con el resto del curso

- Un servidor de sockets clásico usa **un hilo por cliente** ([[Tema 2 - Sockets y protocolos]]); la versión asíncrona usa `AcceptAsync`/`ReadAsync` y no gasta un hilo por conexión ociosa. Ese es el argumento de escalabilidad completo.
- Los controllers de Web API son `async Task<ActionResult<T>>` porque su trabajo es I/O ([[Tema 6 - APIs REST y WebSockets]]).
- Un consumidor de RabbitMQ procesa mensajes en paralelo y necesita idempotencia, no solo locks ([[Tema 4 - Colas de mensajes y MOM]]).

## Connections
- Temas fuente: [[Tema 1 - Programacion multihilo]] · [[Tema 3 - Asincronia y paralelismo]].
- Nivelación: [[Fundamentos previos]] · [[Complementario - Introduccion y Streams]].
- Trampas de código: [[Errores caros en Programacion de Redes]]. Mapa: [[Mapa del curso Programacion de Redes]].
