# TEMA 1 — Programación Multihilo (PDR)

**Cobertura vigente de Aulas (semanas 1 a 3):** Threads C#, problemas de concurrencia, exclusión mutua, semáforos, monitores, `lock`, `ManualResetEvent` y `AutoResetEvent`.

**Fuentes:**
- Oficial: `../../TEORICO/02_THREADS_Y_CONCURRENCIA/2-Threads.pdf` — diapositivas de clase, con citas directas de *Sistemas Operativos Modernos* (Tanenbaum) y *A fondo C#*.
- Oficiales: `../../TEORICO/02_THREADS_Y_CONCURRENCIA/THREADS/Guia threads v3.pdf`, `Ejercicios de Threads - v1.pdf` y `Guia Thread Sleep y Join v1.pdf` — incorporadas el 17/08/2026.
- Oficial: `../../PRACTICOS/Practico 1.pdf` — 14 ejercicios de Threads y sincronización.
- Complementaria (no oficial): `../../TEORICO/_PROGRAMA_Y_RESUMENES/0.RESUMEN-STUDOCU.pdf` — ya no es la base; se reemplaza por la fuente oficial de arriba.

**[Complementario — Claude]** marca las explicaciones que agrego yo. **[Interpretación]** marca conexiones que hago yo entre conceptos, no dichas explícitamente así en la fuente.

---

## 1. Base de sistemas operativos: proceso e hilo (Tanenbaum)

**Qué es:** el fundamento teórico (no de C#) de por qué existen los hilos, tal como lo explica el sistema operativo por debajo.

**Cómo funciona:**
- **Multiprogramación:** si los procesos gastan, por ejemplo, 80% del tiempo esperando E/S, hacen falta muchos procesos en memoria a la vez para que la CPU no se desperdicie.
- **Proceso:** instancia de un programa en ejecución, con sus propios valores de contador de programa, registros y variables. Puede pasar por los estados: Nuevo → Preparado → Activo → Terminado, con transiciones de Bloqueado y Suspendido.
- **Cambio de contexto:** cuando el SO deja de ejecutar el proceso A, salva su estado, cede el control al núcleo (pasando de modo usuario a supervisor), decide si B está listo, y retoma la ejecución de B (pasando de supervisor a usuario).
- **Hilo:** "una tarea que puede ser ejecutada al mismo tiempo que otra tarea". A diferencia de un proceso, los hilos que pertenecen a un mismo proceso comparten sección de código, sección de datos y otros recursos del SO (archivos abiertos, banderas) — "la idea de hilo surge de la posibilidad de compartir recursos y permitir el acceso concurrente a esos recursos".
- El hilo tiene contador de programa propio, registros con sus variables de trabajo, y una pila con el historial de llamadas pendientes — pero comparte el espacio de direcciones (y las variables globales) con los demás hilos del mismo proceso.

**Por qué se usan hilos (motivos oficiales):**
1. Muchas aplicaciones desarrollan varias actividades a la vez, algunas de las cuales se pueden bloquear (ej. un procesador de texto con hilos separados para teclado/mouse, paginación y respaldo en disco — si fuera un solo hilo, el respaldo bloquearía al teclado).
2. Descomponer una app en hilos secuenciales que corren en cuasi paralelo simplifica el modelo de programación.
3. En sistemas con varias CPUs, los hilos habilitan paralelismo real.

> **[Complementario — Claude]** Esto conecta directo con el Tema 0: acá está la fuente original (Tanenbaum) de la distinción entre concurrencia (cuasi paralelo, un solo núcleo) y paralelismo real (varios núcleos).

---

## 2. Multithreading en la práctica

**Qué es:** dividir el trabajo de una aplicación en varios hilos o unidades de ejecución.

**Por qué importa:**
- Beneficia el diseño: cada hilo suele capturar una única abstracción o idea (favorece mantenimiento).
- Aplica la estrategia "divide y vencerás".
- Mejora la utilización del tiempo de CPU: el procesador da a cada hilo una **rodaja de tiempo (timeslice)**, y el programador no tiene que decidir manualmente cuándo ceder el control.

### Condiciones de carrera y exclusión mutua (Tanenbaum)

- **Condición de carrera:** "situaciones en donde dos o más procesos están leyendo o escribiendo algunos datos compartidos y el resultado final depende de quién se ejecuta y exactamente cuándo lo hace". Depurar estos bugs "no es nada divertido": la mayoría de las ejecuciones de prueba están bien, pero en algún momento poco frecuente ocurre algo inexplicable.
- **Exclusión mutua:** "la clave para evitar problemas... es buscar alguna manera de prohibir que más de un proceso lea y escriba los datos compartidos al mismo tiempo".
- **Región crítica / sección crítica:** la parte del programa donde se accede a la memoria compartida. Si dos procesos nunca están en su región crítica al mismo tiempo, se evitan las carreras.

### Recurso y RSR

- **Recurso:** elemento del hardware con potencialidad de cómputo; esa capacidad bruta se vuelve neta cuando el SO propicia su uso.
- **RSR (recurso serialmente reusable):** un recurso que se puede usar muchas veces, un proceso por vez — el ejemplo oficial es una impresora.

### Programación concurrente

Definición oficial: "el estudio de técnicas de programación que expresan el paralelismo entre tareas, aplicado a la sincronización de procesos".

### Autoevaluación rápida
- ¿Por qué depurar una condición de carrera es más difícil que depurar un bug normal?
- ¿Qué relación hay entre "sección crítica" y "exclusión mutua"?
- Dame un ejemplo de RSR fuera de la impresora.

---

## 3. La clase `Thread` en .NET

**Qué es:** la API de C# para crear y manejar hilos.

### Ciclo de vida y estado

Un hilo pasa por estados: `Unstarted` → (`.Start()`) → `Running` ⇄ `WaitSleepJoin` → `Stopped` / `Abort Requested` → `Aborted`. Se consulta con `thr1.ThreadState`.

**Importante:** llamar a `.Start()` **no ejecuta el hilo en ese instante** — le avisa al framework de .NET que el hilo está pronto, y es el framework (junto con el scheduler del SO) el que decide **cuándo** arrancarlo. Por eso el orden de ejecución entre hilos creados casi simultáneamente **no está garantizado**, y los resultados en consola pueden salir mezclados.

### Tipos de hilo

| Tipo | Comportamiento oficial | Cómo se crea |
|---|---|---|
| **Foreground** | Cuando el hilo principal termina, si hay un hilo foreground hijo corriendo, el proceso **espera** a que todos los foreground terminen antes de cerrar | `new Thread(función)` — es el default |
| **Background** | Cuando el hilo principal termina, si solo quedan hilos background, se **abortan** y el proceso finaliza igual | Los hilos que corren vía `ThreadPool` son background |

### Prioridades (`Thread.Priority`)

`Highest`, `AboveNormal`, `Normal`, `BelowNormal`, `Lowest`. Está basado en .NET pero **es el sistema operativo el que finalmente define la prioridad real** (ej. Intel maneja 0 a 31 niveles). Cambiar prioridades puede traer problemas: si dos hilos de trabajo tienen prioridad muy alta, la interfaz gráfica (que corre en paralelo con prioridad normal) puede volverse lenta. Con la misma prioridad, se aplica asignación tipo round robin.

### Gestión de un hilo

- `Thread.Sleep(ms)`: pausa el hilo actual.
- `t.Suspend()`, `t.Resume()`, `t.Abort()`: están **obsoletos**, no se recomienda usarlos.
- `thread.Join()`: bloquea al hilo que lo llama hasta que el hilo `thread` termine — es la forma correcta de "esperar" a un hilo.

### Pasaje de parámetros

- `thread.Start(object)` acepta un objeto genérico, pero **no es recomendable** porque no verifica el tipo del parámetro en compilación.
- La forma recomendada es con una **expresión lambda**: `new Thread(() => sum(a: 4, b: 8))`.

### El problema clásico del `for` con lambdas

Si se crean hilos dentro de un `for` con una lambda que referencia directamente la variable incrementadora (`i`), **todos los hilos comparten la misma referencia** a esa variable — como no se controla cuándo arranca cada hilo, `i` puede seguir cambiando antes de que un hilo llegue a leerla, produciendo números repetidos y otros faltantes en la salida.

**Solución:** crear una variable **local** dentro del bloque del `for`, copiando el valor de `i` en ese momento (`int n = i;`), y usar esa variable local en la lambda. Cada iteración así tiene su propia copia, inmune a los cambios posteriores de `i`.

### Autoevaluación rápida
- ¿Por qué `.Start()` no garantiza que el hilo arranque inmediatamente?
- ¿Qué pasa con los hilos background cuando termina el hilo principal, a diferencia de los foreground?
- ¿Por qué usar una variable local dentro del `for` soluciona el problema de los hilos que imprimen valores repetidos?

---

## 4. Mecanismos de sincronización

### Monitor

**Qué es:** clase de .NET que permite acceder de forma secuencial a bloques de código mediante bloqueos y señales.

| Método | Qué hace |
|---|---|
| `Monitor.Enter(obj)` | Intenta obtener un bloqueo exclusivo sobre `obj`. Si otro hilo ya lo tiene, el método queda inaccesible hasta que se libere |
| `Monitor.Exit(obj)` | Libera el bloqueo |
| `Monitor.Wait(obj)` | El hilo libera temporalmente el bloqueo y se pone en cola de espera |
| `Monitor.Pulse(obj)` | Notifica a un hilo en espera que la condición podría haberse cumplido (no le entrega el bloqueo automáticamente, pasa a competir por él) |
| `Monitor.PulseAll(obj)` | Igual que `Pulse`, pero para todos los hilos en espera |

**Problema real mostrado en la fuente:** si dentro del bloque protegido se lanza una excepción antes de llegar al `Monitor.Exit`, el segundo hilo queda **bloqueado indefinidamente** (nunca se libera el candado). Por eso la recomendación oficial es encerrar el `Enter` en un `try` y poner el `Exit` dentro de un `finally`.

**Buena práctica:** tener la mínima cantidad de código posible dentro de la zona protegida, para minimizar el tiempo que esperan los demás hilos.

### Lock

**Qué es:** azúcar sintáctica de C# que es una abstracción directa de `Monitor` — el compilador genera exactamente el patrón `Monitor.Enter` + `try/finally` + `Monitor.Exit`.

```csharp
lock (myLock)
{
    // Critical code
}
```
es equivalente a:
```csharp
Monitor.Enter(myLock);
try { /* Critical code */ }
finally { Monitor.Exit(myLock); }
```

**Usos:**
- Implementar el patrón **Singleton** thread-safe (doble verificación dentro del `lock`, para no pagar el costo del bloqueo en cada acceso una vez que la instancia ya existe).
- **Locks anidados:** en general **no es buena práctica**. Si una función toma el lock `a` y adentro toma `b`, y otra función toma `b` y adentro `a`, se puede producir un **deadlock** — cada una espera el recurso que tiene la otra.

### Mutex

**Qué es:** representación de la primitiva del sistema operativo Win32 Mutex.

- Permite manejar bloqueos **a nivel de todo el sistema operativo**, no solo del objeto donde se instancia (a diferencia de Monitor/Lock).
- Permite sincronizar hilos **entre distintas aplicaciones/procesos**, no solo dentro de un mismo proceso.
- Métodos: `mutex.WaitOne()` (equivalente a Enter), `mutex.ReleaseMutex()` (equivalente a Exit).

### Comparativa oficial: Mutex vs. Monitor vs. Lock

- `lock` es equivalente a usar Monitor — es una instrucción del lenguaje; Mutex y Monitor son clases.
- **Monitor es mucho más rápido que Mutex.**
- Mutex es una primitiva de sincronización a nivel de SO; Monitor tiene alcance limitado al objeto donde se instancia.
- **Alcance: Mutex permite sincronizar threads entre aplicaciones distintas.**
- Orden de "peso" oficial: **Mutex > Monitor > Lock**.

### Semaphore

**Qué es:** abstracción para coordinar la ejecución de varios hilos, permitiendo que hasta **N** entren a la vez a la zona protegida (no solo 1).

**Funcionamiento:**
- Mantiene un contador interno.
- Cada solicitud del recurso decrementa el contador.
- Cada liberación lo incrementa.
- Si el contador llega a 0, rechaza los siguientes pedidos hasta que se libere alguno.

**Constructor:** recibe el número de lugares disponibles inicial y el máximo de lugares disponibles.
**Métodos:** `sem.WaitOne()` (equivalente a P(x)), `sem.Release()` (equivalente a V(x)).

Un `Semaphore(1, 1)` se comporta exactamente igual que un `lock`/`Monitor` (un solo hilo a la vez). Permite además sincronización **entre procesos**.

### Otros mecanismos mencionados (para referencia, se profundizan si aparecen en la práctica)

- **`SemaphoreSlim`:** versión más liviana ("lightweight") de `Semaphore`.
- **`AutoResetEvent` y `ManualResetEvent`:** mecanismos de sincronización de hilos, equivalentes en espíritu a `Monitor` + `Pulse`/`PulseAll` — ya introducidos con analogías en el Tema 0.
- **`Barrier`:** también es señalización entre hilos (thread signaling).

### Autoevaluación rápida
- ¿Por qué es peligroso lanzar una excepción dentro de un `Monitor.Enter`/`Exit` sin `try/finally`?
- ¿En qué orden de "peso" ubica la fuente oficial a Mutex, Monitor y Lock? ¿Por qué ese orden tiene sentido dado lo que hace cada uno?
- ¿Qué situación puede generar un deadlock con locks anidados?
- Un `Semaphore(1,1)` ¿en qué se parece y en qué se diferencia de un `lock`?

---

## 5. Patrones para tareas desatendidas

**Qué es:** formas estandarizadas de encapsular "lanzar un hilo, esperar que termine, y reaccionar cuando termina" sin repetir código cada vez.

### Patrón Tarea

Encapsula la creación del hilo (`Execute()` que hace `new Thread(...)` + `Start()`) y la sincronización con él (`WaitForFinish()` que hace `Join()`), dentro de una clase abstracta que subclases concretas completan implementando `Process()`.

### Patrón Tarea con Evento (delegados y eventos)

Extiende el patrón anterior agregando un **callback**: se define un `delegate` con la firma del método a llamar al terminar, la clase expone un `event` de ese tipo, y cuando la tarea termina invoca automáticamente a todos los métodos suscriptos al evento (`OnFinishTask.Invoke(this)`).

- **Delegado:** puntero a función tipado — permite pasar "qué función llamar" como parámetro.
- **Evento:** una señal que un objeto envía cuando ocurre algo; los interesados se suscriben con métodos (`+=`); el remitente no necesita conocer el tipo ni el método específico que se ejecuta. En .NET, los eventos se implementan mediante delegados.

### `BackgroundWorker`

Clase de .NET pensada específicamente para gestionar hilos con esta lógica de "trabajo + finalización": se le asigna una función a `DoWork` (lo que corre en paralelo) y otra a `RunWorkerCompleted` (el callback al terminar), y se dispara con `RunWorkerAsync()` sin bloquear el hilo principal.

> **[Complementario — Claude]** Estos tres (Patrón Tarea, Patrón Tarea con Evento, `BackgroundWorker`) son básicamente tres formas cada vez más "enlatadas" de resolver lo mismo: lanzar trabajo en paralelo y enterarte cuándo termina. Van a volver a aparecer, ya con azúcar sintáctica moderna, cuando se vea `Task`/`async`/`await` en el Tema 3.

### Autoevaluación rápida
- ¿Qué problema resuelve agregarle un evento al Patrón Tarea, comparado con solo tener `WaitForFinish()`?
- ¿Por qué `BackgroundWorker` no bloquea el hilo principal al llamar `RunWorkerAsync()`?

---

## 6. Patrones prácticos de las guías oficiales

### `Thread.Sleep` no sincroniza

`Thread.Sleep(ms)` bloquea **al hilo que ejecuta esa línea** y lo lleva a `WaitSleepJoin` durante un tiempo aproximado. La guía es explícita: no sirve para esperar que otro trabajo termine. Si un programa “funciona” solo porque se agregó un `Sleep`, hay una carrera escondida; debe reemplazarse por `Join`, `Monitor.Wait/Pulse` u otro mecanismo real.

### `Join` expresa precedencia

`otroHilo.Join()` bloquea al hilo actual hasta que `otroHilo` termina. Si primero se arrancan los hilos que imprimen múltiplos de 2 y 3, se hace `Join()` sobre ambos y después se imprimen los múltiplos de 4, las salidas de 2 y 3 pueden mezclarse, pero la de 4 siempre queda al final.

### “Verificar y luego modificar” debe ser atómico

Las guías repiten dos variantes del mismo bug:

1. Varios hilos verifican `if (!finished)` y más de uno imprime antes de que alguno cambie `finished`.
2. Varios hilos hacen `Exists(nombre)` y luego `Add(persona)` sobre una lista; dos pueden comprobar a la vez que el nombre no existe y ambos agregarlo.

La solución es proteger **la decisión completa** con el mismo `lock`: verificar + actuar. Proteger solo el `Add` no alcanza.

```csharp
lock (locker)
{
    if (!personas.Exists(p => p.Name == nueva.Name))
        personas.Add(nueva);
}
```

### No todo problema concurrente se arregla con `lock`

El `for` que crea hilos usando directamente `i` falla por **captura/alcance de variable**, no por falta de exclusión mutua. Un `lock` alrededor de `new Thread(...).Start()` no congela el valor. La solución es copiarlo a una variable local por iteración:

```csharp
for (int i = 0; i < 100; i++)
{
    var x = i;
    new Thread(() => PrintData(x)).Start();
}
```

### Qué practicar del Práctico 1

- Ordenar hilos de dos formas distintas (`Join` y señalización).
- Compartir resultados y mostrarlos recién cuando todos terminen.
- Abrir 10 hilos y liberarlos al escribir `start` (`ManualResetEvent` o monitor).
- Limitar 100 personas con `Semaphore` y cerrar el ingreso por una condición de recaudación.
- Productor/consumidor con `Monitor.Wait/Pulse` y con semáforos.
- Lectores/escritores con monitor.

---

## Resumen ejecutivo del tema

1. Un hilo es una tarea que corre "al mismo tiempo" que otra dentro del mismo proceso, compartiendo memoria, código y recursos del SO — eso habilita velocidad, pero también condiciones de carrera.
2. Multithreading divide el trabajo en unidades de ejecución independientes, mejora diseño y aprovechamiento de CPU vía timeslices administrados por el SO.
3. `.Start()` no ejecuta un hilo de inmediato — solo lo habilita; el orden real de ejecución lo decide el framework y el SO, nunca hay que asumirlo.
4. Los hilos son Foreground (bloquean el cierre del proceso) o Background (se abortan al cerrar); tienen prioridad, aunque el SO decide en última instancia.
5. Todos los mecanismos de sincronización resuelven la misma condición de carrera con distintos alcances y costos: `lock`/`Monitor` (rápido, mismo proceso), `Mutex` (lento, entre procesos), `Semaphore` (como Monitor pero con N lugares en vez de 1). Orden de peso: Mutex > Monitor > Lock.
6. Los patrones Tarea, Tarea con Evento y `BackgroundWorker` encapsulan crear un hilo, esperar que termine, y reaccionar al final — antecesores directos de lo que se va a ver con `Task`/`async`/`await`.
7. `Sleep` solo demora; `Join` sí establece que un trabajo ocurra después de otro.
8. En operaciones compuestas sobre datos compartidos, el `lock` debe abarcar la verificación y la modificación como una única región crítica.
