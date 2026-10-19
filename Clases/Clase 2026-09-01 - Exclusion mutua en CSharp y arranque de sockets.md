---
date: 2026-09-01
type: clase
tags:
  - clase
  - pdr
  - tema-1
  - tema-2
ai-first: true
confidence: stated
fuente: "Grabacion Vimeo, showcase 12367503, video 01-09-2026, 1h43m, subtitulos autogenerados"
docentes:
  - Enrique Latorres
  - Natalia Franco
---

# Clase 2026-09-01 - Exclusion mutua en CSharp y arranque de sockets

## For future agent
Acta de la clase del 01-09-2026 (Vimeo, showcase 12367503). Nucleo de la clase: **todos los mecanismos de exclusion mutua de C#** (`Monitor`, `lock`, `Monitor.Wait`/`Pulse`, `Mutex`, `Semaphore`), con su jerarquia de niveles de abstraccion y los errores clasicos. Ademas: una explicacion larga de **Java Virtual Machine vs .NET** que el docente considera importante, y el arranque de [[Tema 2 - Sockets y protocolos]]. Al final hay una consigna concreta sobre que se puede ir haciendo del Obligatorio 1.

## Repaso del checkpoint (preguntas que hizo el docente)
Vale como banco de preguntas de defensa:
- **En que se diferencian los procesos de los hilos.** Respuesta aceptada: el proceso es una instancia de un programa; los hilos son varias lineas de ejecucion en paralelo dentro de ese proceso. Analogia que reforzo: la relacion proceso-programa es analoga a la relacion hilo-proceso. Un proceso tiene siempre al menos un hilo, el `main`.
- **Para que sirve el `Start` en `Thread`.** Para comenzar el hilo: primero se prepara y se le asigna la funcion delegada, y `Start` avisa al despachador.
- **Diferencia entre foreground y background.** Foreground tiene un **`Join` implicito**; background se asume descartable y se mata cuando termina el `main`, con el riesgo de dejar los hilos en estado inconsistente.
- **Como se pasan parametros a una funcion que se ejecuta en un hilo aparte.** Con `lambda`, o con la version parametrizada.
- **Cuando usar `Thread.Sleep`.** "En un codigo real, en produccion, no deberiamos usarlo nunca" ([16:27]). Excepciones: simular trabajo costoso (y en el obligatorio final **el docente va a pedir explicitamente** simular ciertas cosas con `Thread.Sleep`), y bajarle el ritmo a un loop de polling para que no sea **espera activa**.

Tambien repitio el error del `for` con el indice compartido de la clase anterior y su solucion (variable nueva dentro del bloque constructivo). Ver [[Clase 2026-08-27 - Letra del Obligatorio 1 y practica con Thread]].

Adelanto: el docente esta preparando **un patron propio para generar hilos y tareas**, una clase abstracta a la que se le agrega el codigo a ejecutar, con herramientas para controlar y seguir el avance de tareas costosas.

## Mecanismos de exclusion mutua en C#

### El problema, tal como se planteo
Existe un metodo que actualiza una estructura (aca simulada como "base de datos en memoria") que **no puede ser ejecutado por dos o mas hilos al mismo tiempo**, pero varios hilos lo llaman. Demo: dos workers, uno escribe 100 `X` y el otro 100 `0` en un loop. Sin exclusion, los `X` y los `0` **se entrelazan**; con exclusion, primero salen todos los de uno y despues todos los del otro.

### `Monitor.Enter` / `Monitor.Exit`
- **Aviso de nomenclatura**: lo que Microsoft llama `Monitor` **es en realidad una region critica**, no un monitor en el sentido de Infraestructura. Un monitor clasico era un objeto cuyos metodos ya tenian exclusion mutua (el `synchronized` de Java); el `Monitor` de Microsoft define **un punto de entrada y un punto de salida**.
- Hay que elegir **un objeto** para bloquear. Tiene que ser **un objeto complejo**: enteros y tipos simples no funcionan y Microsoft no lo recomienda. Todos los objetos heredan de un objeto primario que **ya tiene un semaforo interno**, y es ese semaforo el que se usa.
- **Siempre hay que usar el mismo objeto.** Practicas: instanciar uno especifico para el caso, usar `this` si es la clase que tiene los datos, o **usar el objeto que contiene los datos a proteger** (asi se parece mas a una region critica clasica, donde la variable protegida estaba en la declaracion).

### Error clasico: excepcion sin `finally`
- Si salta una excepcion entre el `Enter` y el `Exit`, **el `Exit` no se ejecuta y el bloqueo queda tomado para siempre**.
- Peor: **no se puede capturar desde afuera**. El `try/catch` del `main` o del worker **no captura** lo que pasa dentro del metodo que corre en otro hilo. "Como esto esta corriendo en otro hilo, no lo va a capturar" ([31:05]).
- Por lo tanto: `Monitor.Enter` **siempre** con `try` / `finally` que haga el `Exit`, y el `try` **puesto donde ocurre la excepcion**, no en un nivel superior.

```csharp
Monitor.Enter(lockObject);
try
{
    // seccion critica
}
finally
{
    Monitor.Exit(lockObject);   // se libera aunque haya excepcion
}
```

### Regla de diseno: minimizar el bloqueo
> "En general, como medida, traten de minimizar la cantidad de codigo que esta dentro del lock. Restrinjan el bloqueo de exclusion a lo minimo necesario para garantizar la consistencia de los datos." ([32:07])

Motivo: mientras un hilo esta dentro, **todos los demas quedan bloqueados**. Si el bloqueo es minimo, los otros adelantan trabajo. Y a veces conviene **rediseniar** para minimizarlo (ver la clase del [[Clase 2026-09-03 - Minimizar bloqueos y sockets en detalle|03-09]], donde la diferencia medida fue 40 s vs 4 s).

### `lock`
- `lock` **se parece mas a una region critica** porque trae el bloque constructivo incorporado, y hace la programacion mucho mas facil.
- **Es equivalente a `Monitor.Enter` + `try/finally` + `Monitor.Exit`.** Si se inspecciona el codigo intermedio de .NET, se ve exactamente eso generado a bajo nivel.
- Toma tambien un objeto de bloqueo, y vale la misma recomendacion: usar el objeto cuyos datos se quieren proteger.

```csharp
lock (myLock)
{
    // codigo critico; si hay excepcion, el lock se libera solo
}
```

- **Cuando usar `Monitor` en vez de `lock`**: cuando se quiere control fino - ver con detalle que pasa ante una excepcion, tener condiciones de salida complicadas, poner un `catch` con un mensaje propio. Con `lock`, si hay una excepcion adentro y no se captura, "me la come el lock" y hay que enterarse por otro mecanismo.
- Tambien se mostro `lock(this)` a nivel de metodo, para que **todo el metodo** tenga un solo hilo. Eso si se parece a un monitor clasico.
- Idea general que dejo: en .NET hay **distintos niveles de abstraccion** para lo mismo; se elige segun el control que se necesite. Ejemplo que dio: para un servidor web hay una clase que se instancia y ya funciona, y hay soluciones de mas bajo nivel para controlar que se hace ante cada verbo HTTP.

### `Monitor.Wait` / `Monitor.Pulse` - bloqueo condicional
- El problema: `lock` y `Monitor` solo dan exclusion mutua de **un** hilo. A veces se quiere **patio de capacidad 2 o 3**, o cualquier condicion de acceso. En Infraestructura eso eran las **regiones criticas condicionales**, donde la condicion iba junto con el bloqueo. **C# no tiene lock condicional**, entonces hay que hacerlo a mano.
- Mecanica: el hilo entra al `lock`, evalua la condicion en un `while`; si no se cumple hace `Monitor.Wait`, que **lo bloquea y al mismo tiempo libera el lock** para que otros hilos puedan entrar. Otro hilo, al salir, hace `Monitor.Pulse` y lo desbloquea; el hilo despertado **vuelve a evaluar la condicion**.
- **El `Wait` va siempre dentro de un `while`, nunca en un `if`.** Motivo: al despertarse hay que volver a comprobar la condicion.

```csharp
// patio de capacidad N, esquema de la clase
lock (lockObject)
{
    while (runningThreads >= maxThreads)
        Monitor.Wait(lockObject);   // se bloquea Y libera el lock
    runningThreads++;
}
try
{
    // zona con capacidad limitada
}
finally
{
    lock (lockObject)
    {
        runningThreads--;
        Monitor.Pulse(lockObject);  // despierta a uno de los que esperan
    }
}
```

- Ejemplo previo mas simple mostrado: **ping-pong**, dos hilos que se autobloquean y se liberan mutuamente para alternar.
- El docente reconocio que el ejemplo del ping-pong "es medio infame, porque es dificil de entender lo que hace" y paso al del patio.
- Tambien dejo **un productor-consumidor** con multiples productores y consumidores, parametrizado por cantidad de buffers, productores, consumidores e items. **Consigna practica**: que la cantidad total producida y la total consumida coincidan, si no el programa no termina. Ejemplo: 5 productores x 20 items = 100, y 10 consumidores x 10 items = 100.

### `Mutex`
- **Semaforo binario**: pasa / no pasa. Se usa `WaitOne` y `ReleaseMutex`, equivalentes a `wait`/`signal`, o `P`/`V`.
- Distincion importante que hizo:
  - **Mecanismos del sistema operativo**: `Mutex` viene de la biblioteca del **kernel Win32**, son primitivas del sistema. Por eso se pueden **compartir entre aplicaciones de tecnologias distintas** (semaforo con nombre), y dos procesos separados pueden sincronizarse.
  - **Mecanismos de .NET**: solo accesibles dentro de .NET y en general dentro de la misma aplicacion.
- **Los del sistema operativo son mas lentos**, porque implican interaccion con el nucleo.

### Jerarquia de abstraccion (tabla resumen de la clase)

| Mecanismo | Nivel | Notas |
|---|---|---|
| `Mutex` | Mas bajo (sistema operativo) | Mas lento; compartible entre procesos y tecnologias |
| `Monitor` (`Enter`/`Exit`, `Wait`/`Pulse`) | Intermedio | Control fino; obliga a `try`/`finally`; permite bloqueo condicional |
| `lock` | Mas alto | Azucar sobre `Monitor` con `try`/`finally` incluido |

### `Semaphore` (semaforo generico)
- Tiene contador, `WaitOne` y `Release`. Se inicializa con el valor inicial y el **valor maximo**.
- Ejemplo: semaforo inicializado en 3 con maximo 3, y 10 hilos; entre el `WaitOne` y el `Release` solo pueden haber **tres hilos**. Es el patio de capacidad 3, resuelto sin `Wait`/`Pulse`.
- Aviso: en el ejemplo faltaba el `finally` con el `Release` para el caso de excepcion. **Hay que ponerlo.**

### Preguntas de repaso que hizo (utiles para el parcial)
- **`lock` garantiza el orden de ejecucion?** **No.** El primero que llega, entra. Igual que los semaforos.
- **Diferencia entre `lock` y `Monitor.Enter`/`Exit`?** El `lock` **ya trae el `try`/`finally` adentro**.
- **Para que sirven `Wait` y `Pulse`?** Para **bloqueo condicional**: limitar capacidad, semaforos genericos, reparto de recursos. Siempre combinados con un `while`.

### Deadlock por orden de bloqueos
- Si un camino toma `lock A` y despues `lock B`, y otro toma `lock B` y despues `lock A`, **hay deadlock**.
- Regla dada: **tomar siempre los locks en el mismo orden**. Sugerencia concreta: **orden alfabetico**.

### Adelantos
- Mas adelante: **`SemaphoreSlim`**, barreras, y disparo de eventos como mecanismo de sincronizacion. Estan ligados a programacion asincronica.
- Preferencia declarada del docente sobre estructuras: usar las que ya traen los bloqueos adentro, por ejemplo **`ConcurrentDictionary`**, que internamente implementa **lectores-escritores**. "No las tienen que implementar, si tienen que entender como funcionan."

## Digresion importante: Java Virtual Machine vs .NET
El docente le dio bastante tiempo. Vale como pregunta de defensa.

- **Java**: el compilador traduce el codigo a **bytecode**, que es el lenguaje de maquina de una **CPU virtual**. La JVM tiene adentro una CPU virtual y una especie de sistema operativo virtual: su propio despachador de tareas, garbage collection, etc.
  - Consecuencia: **esa CPU virtual la simula la CPU real**, entonces cada instruccion virtual cuesta muchas instrucciones fisicas. **No es supereficiente.**
  - Ventaja historica enorme: la JVM era **codigo abierto**, cualquiera podia implementarla. Por eso Java corria en celulares del siglo pasado, cafeteras, dispositivos con capacidad minima. Anecdota: un ex alumno (Fernando Sarberro, hoy de Batovi Games) hizo un motor de ajedrez de tesis y lo porto a Java para celulares de la epoca.
- **.NET**: se hace **al reves**. El codigo C# se traduce a un lenguaje intermedio, que es el assembler de una **CPU teorica que no existe**; y despues **hay un compilador por plataforma** (Linux, Windows, Mac) que baja ese lenguaje intermedio al procesador fisico.
  - Consecuencia: **corre a la velocidad maxima del procesador real**. Mas rapido y eficiente que Java.
  - Costo: **solo existe para los ambientes populares**. Hay .NET de Microsoft para Linux, Windows y Mac, mas **Mono** de la comunidad para Linux y Windows. Para procesadores chicos, IoT, hardware exotico, **hay Java pero no hay .NET**.
- Nota practica: el docente escribe los codigos del curso **en Linux** y aclaro que corren igual en Windows salvo detalles.
- Recordatorio de que la parte de .NET es **codigo abierto** y las fuentes se pueden inspeccionar; de hecho mostro el lenguaje intermedio para probar que `lock` genera `Monitor.Enter`/`Exit` con `try`.

## Arranque de Tema 2: sockets
- Un **socket** es un mecanismo de comunicacion entre procesos que permite **enviar y recibir datos**; es un canal de comunicacion punto a punto. La clase de .NET se apoya en la biblioteca del sistema: **WinSock** en Windows, **sockets** en Linux/Unix; son muy parecidas porque respetan el mismo estandar.
- **La capa de socket no es especifica de TCP/IP**: es multiprotocolo. IP se volvio el estandar mundial, pero hubo protocolos populares que quedaron afuera del mercado, y Microsoft tiene protocolos propios que todavia usa en sus redes.
- **Endpoint** = **direccion IP** (identifica la maquina) + **puerto** (identifica el proceso).
- Para usar sockets hace falta: IP remota, puerto remoto, IP local, puerto local y **el protocolo de red** (TCP o UDP).
- **TCP vs UDP**, como se discutio en clase:

| | TCP | UDP |
|---|---|---|
| Orientado a conexion | Si, hace handshake previo | No |
| Confiabilidad | Confiable, ACK integrado al protocolo, retransmite | Sin garantia de llegada integrada |
| Modelo | "Cano virtual": se establece la conexion y se mandan datos | Se arma un paquete, se tira, y listo |
| Uso tipico | Cuando importa que llegue todo | Streaming, video, Zoom, logs, IoT |

- Frase de cierre: "el TCP en general lo que nos asegura, lo que nosotros queremos es garantizar que todo lo que se manda llega, es mas importante que llegue rapido" ([98:25]).
- **En el curso se usa TCP.** El docente dijo que va a intentar hacer algun ejemplo con UDP.

## Consigna concreta: que se puede ir haciendo del Obligatorio 1
Cierre de la clase, respondiendo a un estudiante. Vale como plan de arranque:
1. **El esqueleto del cliente**: toda la interfaz de menu de consola, que datos pide y que muestra. No hay interfaz grafica.
2. Un **mockup de la clase** con la que interactua el menu.
3. **La parte del servidor**: como se codifica el protocolo, la tabla de la diapositiva. Cuando llega un paquete, en funcion del **estado del hilo que atiende al cliente** y del mensaje recibido, aceptar o no ese mensaje y hacer una cosa u otra. **Eso ya se puede programar sin tener la comunicacion.**
4. La **logica y el acceso a datos** del servidor: las operaciones y la estructura en memoria.
5. Aviso de eficiencia: mandar paquetes muy chiquitos es **ineficiente para el ancho de banda**. Alternativa: **acumular** todos los datos que pide el usuario (por ejemplo todos los campos de un vehiculo nuevo) y mandarlos juntos al servidor.

## Otros avisos
- **Hay un practico en Aulas** (y tambien en el Drive).
- Los codigos se pasan por **Teams**; algunos tambien estan en el Drive, que puede estar menos actualizado.
- Consigna de estudio: "Jueguen un poco con esto, experimenten para entender como es que funcionan, modifiquen los codigos, a ver que pasaria. [...] Ponganle mucho `console.writeline` para ver que cosas se estan ejecutando en paralelo" ([83:39]).
- Recomendo los libros de C# de la biblioteca, y dijo que ChatGPT, Gemini y DeepSeek estan muy bien entrenados en C#.
- Prioridad declarada del curso: **el foco fuerte va a ser programacion asincronica**, porque da codigos mucho mas eficientes para servidores. El objetivo es que el Obligatorio 2 y el 3 esten hechos con asincronia.

## Connections
- Temas: [[Tema 1 - Programacion multihilo]] · [[Tema 2 - Sockets y protocolos]] · [[Concurrencia de Thread a Task]]
- Errores: [[Errores caros en Programacion de Redes]]
- Anterior: [[Clase 2026-08-27 - Letra del Obligatorio 1 y practica con Thread]]
- Siguiente: [[Clase 2026-09-03 - Minimizar bloqueos y sockets en detalle]]
