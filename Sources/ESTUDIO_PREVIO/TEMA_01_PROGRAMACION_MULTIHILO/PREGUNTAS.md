# Preguntas de autoevaluación — TEMA 1: Programación Multihilo (PDR)

1. ¿Qué diferencia hay entre concurrencia "real" y "pseudo-simultánea"? ¿De qué depende cuál se da?
2. Definí con tus palabras: condición de carrera, región crítica, exclusión mutua y deadlock.
3. Dá un ejemplo (distinto al del auto en un cruce) de una situación cotidiana que se parezca a un deadlock.
4. ¿Qué hace `thread.Join()` y en qué se diferencia de `thread.Sleep()`?
5. ¿Por qué usar una lambda con la variable de un bucle `for` sin variable local puede causar errores al crear hilos?
6. ¿Por qué se recomienda llamar `Monitor.Exit` dentro de un bloque `finally`?
7. Completá la comparación: ¿en qué se parecen y en qué se diferencian `lock` y `Monitor`? ¿Y `Mutex` y un Semáforo?
8. Si necesitás sincronizar el acceso a un archivo entre dos programas distintos corriendo en la misma máquina, ¿usarías `lock` o `Mutex`? Justificá.
9. Si necesitás que como máximo 3 hilos accedan a la vez a una conexión (y no una relación estricta de 1 a 1), ¿qué mecanismo usarías?
10. ¿Por qué `Thread.Sleep(3000)` no garantiza que una escritura de archivo haya terminado? ¿Qué usarías si conocés el hilo que la realiza?
11. Dos hilos ejecutan `if (!lista.Exists(...)) lista.Add(...)`. ¿Por qué proteger solamente el `Add` sigue siendo incorrecto?
12. ¿Por qué un `lock` alrededor de la creación de hilos dentro de un `for` no soluciona la captura de `i`?
13. En el Práctico 1, ¿qué mecanismo usarías para mantener 10 hilos esperando hasta que el usuario escriba `start`? Justificá.
14. Diseñá el orden mínimo de llamadas para arrancar dos hilos, esperar que ambos terminen y recién entonces mostrar un resultado combinado.

---

## Espacio para tus notas / respuestas

-
-
-
