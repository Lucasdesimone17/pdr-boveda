---
date: 2026-08-26
type: synthesis
tags:
  - synthesis
  - pdr
  - errores
  - examen
ai-first: true
confidence: high
sources:
  - "Sources/ESTUDIO_PREVIO/REPASO_FINAL/README.md"
  - "[[Tema 1 - Programacion multihilo]]"
  - "[[Tema 2 - Sockets y protocolos]]"
  - "[[Tema 3 - Asincronia y paralelismo]]"
---

# Errores caros en Programacion de Redes

## For future agent
Lista de los errores que el material marca como caros, cada uno con **por qué** está mal y **cuál es la versión correcta**. Son errores de examen y de obligatorio: la mayoría compila y hasta parece funcionar, y ahí está el problema. Leer antes de entregar código o de responder una pregunta de arquitectura.

**Actualizado el 2026-09-09** con lo dicho en las seis primeras clases del semestre (grabaciones Vimeo, showcase 12367503): se agregaron los ítems 2b a 2e y el 8b, y se anotaron confirmaciones de clase en los ítems 2, 3, 8 y 9. Las reglas duras del curso están consolidadas en [[Reglas y prohibiciones del curso PDR 2026]].

## Concurrencia

### 1. Usar `Sleep` como sincronización
`Thread.Sleep(500)` para "darle tiempo" al otro hilo no sincroniza nada: funciona en la máquina de desarrollo y falla bajo carga. Lo correcto es `Join()` para esperar terminación, o `Monitor.Wait`/`Pulse` o un `ManualResetEvent` para esperar una condición.

### 2. Poner el `lock` alrededor de una parte de la operación crítica
```csharp
// MAL: entre el Exists y el Add otro hilo puede insertar el mismo id
if (!lista.Exists(x => x.Id == nuevo.Id))
    lock (locker) { lista.Add(nuevo); }

// BIEN: check y act dentro del mismo lock
lock (locker)
{
    if (!lista.Exists(x => x.Id == nuevo.Id))
        lista.Add(nuevo);
}
```
La regla: **la región crítica es todo el check-and-act**, no solo la escritura.

**Contrapeso agregado en clase el 01-09 y demostrado el 03-09-2026**: la regla de arriba no autoriza a agrandar el lock. La consigna del docente es **minimizar el código dentro del lock**, y lo midió: el mismo productor-consumidor tardó **40 s** con el trabajo pesado dentro de la zona de exclusión y **4,3 s** dejando dentro solo la actualización de punteros y contadores. Las dos reglas juntas: **todo el check-and-act adentro, y nada más que eso**. Detalle en [[Clase 2026-09-03 - Minimizar bloqueos y sockets en detalle]].

### 2b. `Monitor.Enter` sin `try` / `finally`, y capturar la excepción en el hilo equivocado
Error señalado el **01-09-2026**. Dos partes:
```csharp
// MAL: si salta una excepción, el Exit nunca corre y el bloqueo queda tomado para siempre
Monitor.Enter(lockObject);
HacerAlgoQuePuedeFallar();
Monitor.Exit(lockObject);

// BIEN
Monitor.Enter(lockObject);
try { HacerAlgoQuePuedeFallar(); }
finally { Monitor.Exit(lockObject); }
```
La segunda parte es más sutil: **un `try`/`catch` puesto en el `main` o en el worker no captura la excepción que ocurre dentro del método que corre en otro hilo.** El `try` va donde ocurre la excepción, no en un nivel superior. `lock` ya trae `try`/`finally` adentro, pero **se come la excepción**: si hace falta enterarse, usar `Monitor` con `catch` propio.

### 2c. Matar hilos con `Abort`
Señalado el 27-08 y repetido el 01-09-2026. Si el hilo estaba en medio de una operación sobre datos compartidos, **queda inconsistencia en memoria y no se sabe qué dato quedó mal**. El patrón que pide el curso: el hilo revisa de tanto en tanto una **bandera**, el hilo que lee la consola la levanta al recibir el comando de salida, y cada hilo hace un **final ordenado**. Más adelante, la versión canónica es `CancellationToken`. Cierre del servidor: hilos **foreground** más `Join`, para poder persistir después de que todos terminaron.

### 2d. Tomar los locks en distinto orden en distintos caminos
Señalado el 01-09-2026. Si un camino toma `lock A` y luego `lock B`, y otro toma `B` y luego `A`, **hay deadlock**. La regla que dieron: **tomar siempre los locks en el mismo orden**; sugerencia concreta, orden alfabético.

### 2e. Usar `Monitor.Wait` dentro de un `if`
Señalado el 01-09-2026. `Wait` va **siempre dentro de un `while`** que reevalúa la condición, porque al despertar la condición puede haber cambiado otra vez. Con un `if` el hilo sigue de largo con la condición falsa.

### 3. Capturar la variable del `for` en la lambda
```csharp
// MAL: todos los hilos ven la misma i
for (int i = 0; i < n; i++)
    new Thread(() => Trabajar(i)).Start();

// BIEN: copia local
for (int i = 0; i < n; i++)
{
    int copia = i;
    new Thread(() => Trabajar(copia)).Start();
}
```

**Confirmado con demo en clase el 27-08-2026 y repasado el 01-09.** El mismo problema aparece pasando el índice por `Start(i)`, sin lambda: en .NET **hasta un entero es un objeto y se pasa por referencia**, y el hilo del `for` lo sigue incrementando mientras el despachador arranca los hilos. Síntomas observados: **números repetidos, números faltantes, y el 100 apareciendo aunque el `for` nunca debería generarlo**. La razón de que la copia local funcione: mientras exista una referencia al objeto, **el garbage collector no lo borra**, aunque el bloque constructivo ya haya terminado. Cita del docente: "Nunca más tenés que pensar así. Nunca más en el resto de la carrera, porque si no, no te va a ir muy bien."

### 4. Confundir `Task` con un hilo dedicado
Una `Task` se encola en el `ThreadPool`; no garantiza un hilo propio ni ejecución inmediata. De ahí que bloquear tareas agote el pool.

### 5. Bloquear con `.Result` o `.Wait()` dentro de un flujo async
Anula el beneficio de la asincronía y puede provocar deadlock. La sustitución es directa: `await` en lugar de `.Result`/`.Wait()`, `await Task.WhenAll` en lugar de `Task.WaitAll`, `await Task.Delay` en lugar de `Thread.Sleep`.

### 6. `await` inmediato cuando se quería paralelo
Hacer `await` de cada tarea justo después de crearla es código secuencial con sintaxis asíncrona. Si las tareas son independientes: crearlas todas primero, y después `await Task.WhenAll(...)`.

**Medido en clase el 17-09-2026** con el ejemplo del desayuno, mismas esperas en las tres versiones: la versión sincrónica tardó **15 s**; la versión con `async`/`await` pero con el `await` pegado a cada invocación tardó **lo mismo**; la versión que dispara las tres tareas y recién después hace los `await` tardó **6 s**. Cita del docente: "sigue siendo secuencial de alguna forma, aunque se use await". Regla: **disparar primero, esperar después**. Detalle en [[Clase 2026-09-17 - Task en detalle, continuations, ThreadPool y TPL]].

### 6b. Declarar un método asincrónico como `async void`
Marcado explícitamente como peligroso en clase el **17-09-2026**. Un `async void` es "fire and forget": **no se puede verificar si terminó, con qué resultado, ni si tuvo excepción**, y habilita condiciones de carrera. Regla: **devolver siempre `Task` o `Task<T>`** y que alguien lo evalúe. Única excepción admitida: manejadores de eventos de interfaz gráfica en Windows.

### 6c. `Task.Delay(...)` sin `await`
`Task.Delay(300)` sin `await` **no espera nada**: crea la tarea y el código sigue de largo. Aparece como bug real en los propios ejemplos de la cátedra, corregido en vivo el 17-09-2026. Va siempre `await Task.Delay(300)`.

### 6d. `Thread.Sleep` dentro de código asincrónico
Distinto del error 1 (usar `Sleep` como sincronización): acá el problema es que `Thread.Sleep` **bloquea un hilo del `ThreadPool`**, o sea le saca un hilo a todo el sistema asincrónico. "Un segundo es una enormidad dentro de un sistema." Sustituto directo: `await Task.Delay(...)`, que además crea un punto de segmento.

### 7. Olvidar chequear el `CancellationToken`
`source.Cancel()` solo **pide** la cancelación. Si el método nunca consulta `IsCancellationRequested` ni llama a `ThrowIfCancellationRequested()`, la cancelación no existe. Y el token hay que pasarlo también a las APIs cancelables (`await Task.Delay(1000, token)`).

## Sockets

### 8. Suponer que un `Receive` devuelve un mensaje completo
Es el error más caro del curso porque es intermitente: funciona con mensajes chicos y se rompe con los grandes. `Send` y `Receive` pueden transferir menos bytes de los pedidos. Hay que envolver ambos en un ciclo por offset, y usar framing explícito (4 bytes de largo + payload) para saber cuánto leer.

**Confirmado en clase el 08-09-2026** con demo: `Receive` es bloqueante y espera **hasta recibir al menos un byte**, así que en operación normal **nunca devuelve 0**; devolver 0 es la señal de conexión cerrada. Dos precisiones que agregaron: **no poner `Sleep` dentro del loop** (`Receive` ya espera; esperar de más es mala práctica que no aporta nada) y **usar `while`, no `if`**, porque puede llegar en más de dos tandas. Con TCP y la configuración por defecto, `Send` en general sí manda todo; el patrón por offset es **obligatorio para `Receive`** y opcional para `Send`.

### 8b. Mandar como largo la cantidad de caracteres en vez de la cantidad de bytes
Error nuevo, con demo en vivo el **08-09-2026**. Se manda `texto.Length` como campo `Largo` y funciona con texto ASCII, pero **la eñe en UTF-8 ocupa 2 bytes** y un emoji 3 o 4: el mensaje llega cortado. El intento de arreglarlo sumando `+1` **también falla** con el siguiente carácter especial. Versión correcta: convertir primero a bytes y mandar el `Length` del array de bytes.

```csharp
byte[] datos = Encoding.UTF8.GetBytes(texto);
byte[] largo = BitConverter.GetBytes(datos.Length);   // datos.Length, NO texto.Length
```

### 9. Confundir el backlog de `Listen(n)` con el máximo de clientes
`n` es la cola de conexiones pendientes de `Accept`, no el total de clientes que el servidor puede atender.

**Agregado en clase el 03-09-2026**: además, **ese número se comporta distinto según el sistema operativo**. Linux lo maneja a nivel de socket en forma automática. Cita del docente: "este mismo código funciona diferente en Windows que en Linux". No razonar el backlog como si fuera portable.

### 10. Cerrar mal la conexión
En sockets crudos, `Shutdown(SocketShutdown.Both)` antes de `Close()`. Con `NetworkStream`, `Close()` alcanza. Y hay que manejar los dos casos de desconexión: `Receive` devuelve 0 (cierre ordenado) y `Receive` lanza `SocketException` (cierre abrupto).

### 10b. Escribir el archivo recibido con `Append` sin tratar especial el primer bloque
**Bug plantado a propósito** en el ejemplo 5 de la cátedra, mostrado el **15-09-2026**. El `FileStreamHelper.Write` abre el archivo con `FileMode.Append` si ya existe. Funciona la primera vez; cuando se manda **el mismo archivo por segunda vez**, se **concatena**: el resultado pesa el doble y contiene dos copias, y el visor de imágenes muestra solo la primera, así que el bug es invisible salvo por el tamaño. Cita del docente: "esta es una buena forma de enviar información oculta". Versión correcta: **el primer bloque se trata aparte**, llevando el cursor a cero y truncando si el archivo ya existía; del segundo en adelante `Append` está bien.

### 10c. Dejar el archivo a medio escribir cuando se corta la transferencia
Segundo bug plantado del ejemplo 5, con demo el **15-09-2026**: se cortó la conexión a mitad de una transferencia y el archivo quedó parcial en el servidor. El visor lee el encabezado, entiende las dimensiones y **rellena el resto de gris**, así que parece un archivo válido. El ejemplo tampoco atrapa la excepción. Lo correcto: **atrapar la excepción, detectar que era una transferencia incompleta y borrar el archivo**, avisando al usuario que reintente.

### 10d. Mandar el path del archivo en vez del nombre
El path del cliente **no sirve** en el servidor: el archivo se guarda en otro lugar y la máquina destino puede ser de **otro sistema operativo**, donde el path se representa distinto. Se manda solo `FileInfo.Name`. Y en EDISON conviene ni usar ese nombre: **un nombre propio por modelo**, porque los nombres de archivo del cliente **colisionan** (imágenes bajadas de internet que se llaman todas igual).

### 10e. Borrar un modelo y dejar su imagen en disco
La letra lo pide y en clase (**15-09-2026**) se avisó que **lo van a chequear**: la imagen es parte de los datos del modelo, así que al eliminar el modelo hay que **eliminar el archivo**. Si no, queda una referencia perdida y basura en el directorio.

## MOM

### 11. Diseñar con colas sin ack ni reintentos
Una respuesta de arquitectura que dice "uso RabbitMQ" sin especificar acknowledgements, qué pasa con el mensaje fallido (dead-letter, `delivery-limit`) y cómo se rutea (exchange y routing key), está incompleta.

### 12. Prometer "exactly once" sin idempotencia
La fórmula del material es explícita: **exactly once = at least once + idempotencia**. Sin operación idempotente, no hay garantía.

## REST y gRPC

### 13. Describir REST sin recursos, verbos ni códigos concretos
Decir "hago una API REST" no es una respuesta. La respuesta es `GET /students/{id}` → 200, `POST /students` → 201 + Location, `DELETE` → 204, y por qué POST no es idempotente.

### 14. Verbos en la URI
`/getStudent` está mal; `/students/{id}` con `GET` está bien. Sustantivos en el path, filtros en query params.

### 15. Usar `required` en `proto3`
El ejemplo del material lo hace, pero `required` es de proto2 y fue eliminado en proto3. Ver la crítica en [[Tema 5 - RPC y gRPC]].

## Docker

### 16. Dejar herramientas de compilación en la imagen final
Un Dockerfile de una sola etapa que copia el SDK completo produce una imagen enorme sin motivo. Multi-stage: compilar con `sdk`, ejecutar con `runtime`.

### 17. Suponer que el puerto está publicado
Docker **no** expone ningún puerto por defecto. `EXPOSE` en el Dockerfile solo documenta; publicar es `-p host:contenedor` o `ports:` en Compose.

## Connections
- Temas: [[Tema 1 - Programacion multihilo]] · [[Tema 2 - Sockets y protocolos]] · [[Tema 3 - Asincronia y paralelismo]] · [[Tema 4 - Colas de mensajes y MOM]] · [[Tema 5 - RPC y gRPC]] · [[Tema 6 - APIs REST y WebSockets]] · [[Complementario - Docker y Cloud]].
- Contexto de evaluación: [[Patrones de examen en Programacion de Redes]] · [[Plan de estudio y repaso PDR]].
