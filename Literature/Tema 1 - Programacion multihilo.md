---
date: 2026-08-26
type: literature
tags:
  - literature
  - pdr
  - tema-1
  - threads
  - concurrencia
  - csharp
ai-first: true
source: "Sources/TEORICO/02_THREADS_Y_CONCURRENCIA/"
source_extra:
  - "Sources/ESTUDIO_PREVIO/TEMA_01_PROGRAMACION_MULTIHILO/CHEATSHEET.md"
  - "Sources/PRACTICOS/Practico 1.md"
confidence: stated
semanas: "1-3"
frecuencia_parcial: "15/16 (94%)"
---

# Tema 1 - Programacion multihilo

## For future agent
Nota de tema 1 del temario vigente (semanas 1-3): threads en C# y los mecanismos de exclusión mutua y señalización. Es el tema con **mayor frecuencia histórica de parcial: 15 de 16 letras (94%)**. Contiene los hechos que hay que saber de memoria y los patrones de código que se repiten. Fuentes: `2-Threads.pdf` y las tres guías de Threads, más el cheatsheet del corpus previo.

## Source
`Sources/TEORICO/02_THREADS_Y_CONCURRENCIA/` - `2-Threads.md`, `Guia threads v3.md`, `Guia Thread Sleep y Join v1.md`, `Ejercicios de Threads - v1.md`. Práctico: `Sources/PRACTICOS/Practico 1.md`.

## Key Claims

### Concurrencia real vs percibida
- Multinúcleo produce simultaneidad **real**; un núcleo con multihilo produce simultaneidad **percibida** ("pseudo-simultánea").

### Los cuatro problemas
| Problema | En una frase |
|---|---|
| Condición de carrera | El resultado depende del orden impredecible de ejecución |
| Región crítica | Código que toca un recurso compartido y no debe correr en paralelo |
| Exclusión mutua | Garantiza que solo un hilo entre a la región crítica |
| Deadlock | Hilos bloqueados esperándose mutuamente, sin salida |

### Clase `Thread`
- `new Thread()` deja el hilo en estado *unstarted*; `.Start()` pide al SO ejecutarlo; `.Join()` espera de forma bloqueante a que termine; `.Sleep()` pausa N milisegundos.
- **Regla dura**: `Sleep` demora, **no sincroniza**. `Join` espera la terminación real de otro hilo. Usar `Sleep` como sincronización es uno de los errores caros del curso.
- En un `for`, la lambda debe capturar una **copia local** de la variable del ciclo; si no, todos los hilos comparten la misma referencia.

### Mecanismos de sincronización
| Mecanismo | Alcance | Rendimiento | Hilos permitidos |
|---|---|---|---|
| `lock` (C#) | Mismo proceso | Rápido | 1 |
| `Monitor` (.NET) | Mismo proceso | Rápido | 1, más señalización `Wait`/`Pulse` |
| `Mutex` (SO) | Mismo o distintos procesos | Lento | 1 |
| `Semaphore` (SO) | Mismo o distintos procesos | Lento | N configurable (N=1 equivale a Mutex) |

- `Monitor`: `Enter(obj)` toma la llave, `Exit(obj)` la suelta (**siempre en `finally`**), `Wait(obj)` la suelta temporalmente y espera, `Pulse`/`PulseAll` avisan a uno o a todos los que esperan.
- Elección de mecanismo según el problema: esperar que termine otro hilo → `Join`; liberar varios hilos con una señal → `ManualResetEvent` o `Monitor.PulseAll`; máximo N accesos simultáneos → `Semaphore`; productor/consumidor → `Monitor.Wait`/`Pulse` o semáforos; verificar y modificar una lista → `lock` cubriendo **ambas** operaciones.

## Patrones de código que deben salir de memoria

```csharp
// Arrancar dos trabajos y esperar ambos
t1.Start();
t2.Start();
t1.Join();
t2.Join();
MostrarResultado();
```

```csharp
// Verificar + modificar es UNA sola región crítica
lock (locker)
{
    if (!lista.Exists(x => x.Id == nuevo.Id))
        lista.Add(nuevo);
}
```

```csharp
// Captura correcta en un for
for (int i = 0; i < n; i++)
{
    int copia = i;
    new Thread(() => Trabajar(copia)).Start();
}
```

## Methodology
El material combina el teórico oficial (`2-Threads.pdf`) con tres guías prácticas de ejercicios y el Práctico 1. La progresión es: crear hilos, ver la carrera, arreglarla con exclusión mutua, y después señalizar.

## Critique
- El error clásico que el material advierte y que igual se comete: poner el `lock` alrededor de **una parte** de la operación crítica en vez de todo el check-and-act. Un `lock` que cubre solo el `Add` no evita nada.
- `Mutex` y `Semaphore` del SO se presentan como "lentos" sin cuantificar; el criterio real de elección es el **alcance** (cruzar procesos o no), no el rendimiento.
- Deadlock se define pero el material no desarrolla estrategias de prevención (orden global de adquisición, timeouts).

## Lo que agregaron los docentes en clase (as of 2026-09-09)
Del dictado real del semestre (clases del 20-08, 27-08, 01-09, 03-09 y 08-09-2026). Confirma, corrige o completa lo de arriba:

- **Nomenclatura**: lo que Microsoft llama `Monitor` **es una región crítica**, no un monitor clásico (el monitor clásico era el objeto con métodos `synchronized`). Aviso repetido en tres clases: "a las mismas cosas de siempre les pone nombres diferentes".
- **Jerarquía de abstracción confirmada y explicada**: `Mutex` (más bajo, primitiva del kernel Win32, **compartible entre procesos y tecnologías**, más lento) → `Monitor` (intermedio, control fino) → `lock` (más alto, azúcar sobre `Monitor` con `try`/`finally` incluido). El criterio de elección que dieron **es el alcance y el control**, no el rendimiento - coincide con la crítica de esta nota. Se puede verificar en el lenguaje intermedio de .NET que `lock` genera `Monitor.Enter`/`Exit` con `try`.
- **Prevención de deadlock, que el material no desarrollaba**: la regla que dieron es **tomar siempre los locks en el mismo orden**, y la sugerencia concreta fue orden alfabético.
- **Bloqueo condicional**: C# **no tiene lock condicional**, entonces las regiones críticas condicionales de Infraestructura se emulan con `Monitor.Wait` / `Monitor.Pulse` dentro de un **`while`** (nunca un `if`). `Wait` bloquea **y libera el lock**, lo que permite que otro hilo entre a la zona de exclusión y haga el `Pulse`.
- **Errores que remarcaron y que conviene tener presentes**:
  - `Monitor.Enter` sin `try`/`finally`: una excepción deja el bloqueo tomado para siempre.
  - **El `try`/`catch` de un nivel superior no captura excepciones de otro hilo.** Hay que capturar donde ocurre.
  - `Abort` deja datos inconsistentes: el patrón correcto es bandera + final ordenado, y más adelante `CancellationToken`.
  - **Parámetros a hilos dentro de un `for`**: en .NET hasta un entero es objeto y se pasa por referencia; hay que declarar **una variable nueva por iteración**. Con la versión ingenua se ven números repetidos y faltantes, e incluso el 100 que no debería generarse.
- **Regla de diseño que el material no enunciaba y en clase es central**: **minimizar el código dentro del lock**. La demo del 03-09 midió **40 s contra 4,3 s** en el mismo productor-consumidor, solo por sacar el trabajo pesado de la zona de exclusión (el patrón del "acomodador de boletos": repartir números rápido y hacer el trabajo lento en paralelo).
- **Preferir estructuras concurrentes de la biblioteca**: `ConcurrentDictionary` ya implementa lectores-escritores internamente. "No las tienen que implementar, sí tienen que entender cómo funcionan."
- **CPU-bound vs I/O-bound sí se formuló explícitamente en clase**, cerrando el ítem 10 de [[Huecos y contradicciones del material PDR]]: los hilos sirven por dos razones distintas - **I/O bound** (mientras un hilo espera bloqueado, otro avanza; si hubiera uno solo se bloquearía todo el proceso) y **CPU bound** (si los datos son independientes, se paraleliza y tarda ~1/N). El ejemplo de CPU bound fue **multiplicación de matrices**, y el de I/O bound fueron **`Accept` y `Receive`** en el servidor de sockets.
- **Foreground / background** con la regla del curso: **todo es foreground**, y el diseño tiene que ser tal que el servidor no pueda terminar hasta que todos sus hijos terminen (`Join` explícito si hay persistencia). Background solo para servicios que **no modifican datos**, por ejemplo un hilo de logs.
- **`Start` no arranca el hilo**: lo encola en el despachador. Por eso el orden de los mensajes es incierto y cambia entre ejecuciones; en máquinas potentes casi no se nota.
- **`Thread.Sleep` no va en producción**, solo para pruebas, simular trabajo o bajarle el ritmo a un polling. Y **no garantiza orden**, solo cambia probabilidades.
- **Prioridades**: Windows usa colas multinivel con realimentación (1-15 aplicaciones, 16-31 tiempo real, 0 el gestor de memoria), sube las tareas interactivas y baja las intensivas en CPU; Linux usa round robin con time slice variable y la propiedad `nice` (-20 a +19). **Modificarlas no se recomienda**, y en máquinas modernas descargadas casi no se nota el efecto.
- **`Suspend`/`Resume` no es bloqueo**: es administrativo, para liberar CPU y memoria para algo más importante. Herramienta recomendada: **Sysinternals / Process Explorer**.
- Detalle práctico: **la consola de .NET lineariza la salida** (tiene una especie de productor-consumidor adentro), así que una cadena de texto no se corta a la mitad aunque escriban varios hilos.
- **Restricción del Obligatorio 1**: hay que usar **`Thread` explícito y no `Task`**. Ver [[Reglas y prohibiciones del curso PDR 2026]].

## Connections
- Actas de clase donde se dictó: [[Clase 2026-08-20 - Modelos de arquitectura, procesos e hilos]] · [[Clase 2026-08-27 - Letra del Obligatorio 1 y practica con Thread]] · [[Clase 2026-09-01 - Exclusion mutua en CSharp y arranque de sockets]] · [[Clase 2026-09-03 - Minimizar bloqueos y sockets en detalle]]
- Evolución natural del tema: [[Tema 3 - Asincronia y paralelismo]], y el hilo completo en [[Concurrencia de Thread a Task]].
- Se aplica de inmediato en [[Tema 2 - Sockets y protocolos]] (un hilo por cliente).
- Frecuencia y formato de examen: [[Patrones de examen en Programacion de Redes]] · [[Errores caros en Programacion de Redes]].
- Nivelación previa: [[Fundamentos previos]]. Mapa: [[Mapa del curso Programacion de Redes]].
