# Cheat Sheet — TEMA 1: Programación Multihilo (PDR)

## Concurrencia
- Multinúcleo → simultaneidad **real**.
- Un núcleo + multihilo → simultaneidad **percibida** ("pseudo-simultánea").

## Problemas de concurrencia

| Problema | En una frase |
|---|---|
| Condición de carrera | Resultado depende del orden impredecible de ejecución |
| Región crítica | Código que accede a recurso compartido, no debe correr en paralelo |
| Exclusión mutua | Garantiza que solo 1 hilo entra a la región crítica |
| Deadlock | Hilos bloqueados esperándose mutuamente, sin salida |

## Clase Thread — métodos clave

| Método | Función |
|---|---|
| `new Thread()` | Instancia (estado unstarted) |
| `.Start()` | Pide al SO ejecutar (pasa a Running) |
| `.Join()` | Espera bloqueante a que termine |
| `.Sleep()` | Pausa por N milisegundos |

⚠️ En bucles `for`, usar variable local dentro de la lambda (si no, todos los hilos comparten la referencia de `i`).

**Regla:** `Sleep` demora; **no sincroniza**. `Join` espera la terminación real de otro hilo.

## Patrones de código que se repiten

```csharp
// Arrancar dos trabajos y esperar ambos
t1.Start();
t2.Start();
t1.Join();
t2.Join();
MostrarResultado();
```

```csharp
// Verificar + modificar debe ser una única región crítica
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

## Mecanismos de sincronización

| Mecanismo | Alcance | Rendimiento | N hilos permitidos |
|---|---|---|---|
| `lock` (C#) | Mismo proceso | Rápido | 1 |
| `Monitor` (.NET) | Mismo proceso | Rápido | 1 (+ señalización con Wait/Pulse) |
| `Mutex` (SO) | Mismo o distintos procesos | Lento | 1 |
| Semáforo (SO) | Mismo o distintos procesos | Lento | N (configurable; N=1 = Mutex) |

## Monitor — métodos

- `Enter(obj)` → tomar la llave
- `Exit(obj)` → soltar la llave (¡siempre en `finally`!)
- `Wait(obj)` → soltar temporalmente y esperar
- `Pulse(obj)` / `PulseAll(obj)` → avisar a 1 / a todos los que esperan

## Práctico 1 — mecanismo probable

| Tipo de problema | Herramienta |
|---|---|
| Esperar que termine otro hilo | `Join` |
| Liberar varios hilos con una señal | `ManualResetEvent` o `Monitor.PulseAll` |
| Máximo N accesos simultáneos | `Semaphore` |
| Productor/consumidor | `Monitor.Wait/Pulse` o semáforos |
| Verificar y modificar una lista | `lock` sobre ambas operaciones |
