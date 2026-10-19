# PDR para mañana — 18/08/2026

## Objetivo

Llegar a la clase con Threads fresco. El horario personal registrado es martes de 10:05 a 12:00. El contenido exacto de la clase todavía depende de lo que publique o indique el docente; por la secuencia actual de Aulas, Threads es la preparación más segura.

## Ocho ideas que tenés que poder explicar

1. `Start()` habilita un hilo para ejecutarse, pero no garantiza cuándo ni en qué orden corre.
2. `Join()` espera que otro hilo termine y sí establece una precedencia.
3. `Sleep()` solo demora al hilo actual; no sincroniza ni garantiza orden.
4. Una carrera ocurre cuando el resultado depende del intercalado impredecible entre hilos.
5. `lock` debe cubrir la operación lógica completa: leer, decidir y modificar.
6. Bloquear `if (!finished)` y después modificar fuera del `lock` sigue siendo incorrecto.
7. Capturar la variable de un `for` en una lambda se corrige creando una copia local por iteración; agregar un `lock` no arregla el alcance.
8. Los hilos foreground mantienen vivo el proceso; los background no.

## Tres patrones mínimos

### Esperar terminación

```csharp
Thread worker = new Thread(Trabajar);
worker.Start();
worker.Join();
Console.WriteLine("El worker terminó");
```

### Check + modificación atómicos

```csharp
lock (gate)
{
    if (!finished)
    {
        finished = true;
        ProcesarUnaSolaVez();
    }
}
```

### Captura correcta en un `for`

```csharp
for (int i = 0; i < 5; i++)
{
    int copia = i;
    new Thread(() => Console.WriteLine(copia)).Start();
}
```

## Repaso de 45 minutos

- **10 min:** leer [`TEMA_01_PROGRAMACION_MULTIHILO/CHEATSHEET.md`](TEMA_01_PROGRAMACION_MULTIHILO/CHEATSHEET.md).
- **10 min:** responder sin mirar las preguntas 1–14 de [`TEMA_01_PROGRAMACION_MULTIHILO/PREGUNTAS.md`](TEMA_01_PROGRAMACION_MULTIHILO/PREGUNTAS.md).
- **15 min:** resolver dos ejercicios de [`../PRACTICOS/Practico 1.pdf`](../PRACTICOS/Practico%201.pdf): uno de orden/`Join` y uno de exclusión mutua.
- **5 min:** explicar en voz alta `Sleep` vs `Join` y carrera vs deadlock.
- **5 min:** anotar dudas concretas para preguntar en clase.

## Después de la clase

Registrar qué dio realmente la profesora, qué archivo subió y cualquier diferencia con este material. Eso permite actualizar PDR sin mezclar contenido histórico con el curso 2026.
