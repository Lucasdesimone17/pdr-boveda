# Cheat Sheet — TEMA 2: Threads y Concurrencia (PDR)

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
