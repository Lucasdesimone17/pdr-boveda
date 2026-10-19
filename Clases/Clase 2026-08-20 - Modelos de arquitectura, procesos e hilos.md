---
date: 2026-08-20
type: clase
tags:
  - clase
  - pdr
  - tema-1
ai-first: true
confidence: stated
fuente: "Grabacion Vimeo, showcase 12367503, video 20-08-2026, 1h33m, subtitulos autogenerados"
docentes:
  - Enrique Latorres
  - Natalia Franco
---

# Clase 2026-08-20 - Modelos de arquitectura, procesos e hilos

## For future agent
Acta de la segunda clase, de la transcripcion automatica de la grabacion en Vimeo (showcase 12367503, video del 20-08-2026). Primera mitad: cierre de modelos de arquitectura, decisiones de reparto de carga y adelantos sobre el Obligatorio 1. Segunda mitad: arranque de [[Tema 1 - Programacion multihilo]] con multiprogramacion, proceso vs hilo, mapa de memoria de un proceso, cambio de contexto y el primer ejemplo de `Thread` en C#. Todo lo que dice esta nota es lo dicho ese dia.

## Definiciones del Obligatorio 1 adelantadas en esta clase
- El cliente del Obligatorio 1 es una **interfaz de texto muy sencilla, con menus**, que toma datos, **valida el formato** para poder convertirlos a binario y transmitirlos. Todo lo demas ocurre en el servidor.
- Regla de sesion: **el cliente establece una conexion y la mantiene.** Dentro de esa conexion hace la autenticacion con usuario y password; a partir de ahi el servidor acepta las otras opciones del menu porque la conexion "paso a otro estado". Hasta que haga logout.
- **No se usan tokens en el Obligatorio 1.** Los tokens vienen despues en el curso.
- Los datos van en **estructuras en memoria**; el motivo es pedagogico. "Si yo les pongo un servidor de base de datos, el servidor de base de datos [...] me maneja todo el tema equivalente a serial, recuperabilidad, todas esas cosas. Yo le pongo las sentencias y no me tengo que preocupar de nada" ([92:04]). Como quieren que el estudiante programe la concurrencia, va en memoria.
- Es **probable que la tercera entrega** ponga alguna cosa en un servidor de base de datos, por pedido de Seguridad. Mencionaron ejemplos con Oracle.

## Repaso: donde poner el procesamiento
- El reparto de carga entre cliente y servidor **depende del escenario**. Con NoSQL, muchas veces el cliente tiene que hacer parsing o formateo para presentar los datos.
- Objetivo transversal: que las aplicaciones tengan **escalado horizontal**. Los motores NoSQL estan disenados para eso (son BASE, no ACID): se clonan maquinas virtuales ya parametrizadas, se disparan y en pocos minutos estan sincronizadas en el clster.
- Delante de un conjunto de servidores suele haber **un broker** que reparte carga con distintas estrategias. "Vamos a hacerlo aca, por ejemplo con MOM" ([12:28]).
- Ejemplo de estacionalidad: en Navidad se levantan mas servidores para que el cliente entre, vea y compre rapido; si no, compra en otro lado.
- Menciono **IaaS** con un ejemplo propio: le dan una maquina virtual Linux, con panel de administracion, DNS propio, scripts que automatizan instalaciones (por ejemplo NGINX), pero si quiere editar los archivos de configuracion del Linux, es un Linux y lo edita. La infraestructura es responsabilidad del proveedor; el password y los controles siguen siendo del usuario.
- Checkpoint de clase: *app de ecommerce con miles de usuarios concurrentes, que modelo elegiria*. Respuesta desarrollada: cualquiera funciona, pero un monolito hace que los usuarios navegando compitan por recursos con los que se autentican. **Segmentar da mejor respuesta al cliente y gasto mas ajustado.**

## Repaso pedido explicitamente por el docente
- De **Infraestructura**: mutua exclusion, monitores, regiones criticas, semaforos.
- De **Base de Datos 2**: equivalente a serial, bloqueos multivaluados, bloqueo de modo multiple. Motivo: eso mismo aplica cuando se trabaja con estructuras de datos complejas en memoria y hay mensajes de multiples clientes pidiendo cambios. "Yo tengo que asegurarme que todos esos cambios de esas estructuras sean consistentes con la realidad" ([28:02]).
- De **Redes**: sockets, TCP/IP, direcciones y puertos. En general se usa TCP/IP pero tambien UDP.

## Portabilidad de .NET
- El mismo codigo compila y corre en **Windows, Linux y Mac**. Puede haber pequenas diferencias sutiles en manejo de sockets y de threads.
- El sistema operativo del cliente y el del servidor pueden ser distintos, siempre que la comunicacion use el mismo protocolo. Las bibliotecas de gRPC y MOM ya son multiplataforma y garantizan compatibilidad.
- Middlewares mencionados: web services (se va a trabajar **REST**), CORBA (orientado a objetos), MOM, gRPC, TCP/IP.
- Ventaja del middleware: **oculta la complejidad** y da abstracciones de mas alto nivel. Desventaja implicita: se confia en que funciona.

## Sockets a bajo nivel vs gRPC (comparacion hecha en clase)
- Con sockets, el estudiante planifica todo: como agarra los datos, como los pone en bytes, como los mete en el canal, y del otro lado como los parsea. **Hay que cuidar la sincronizacion de la cantidad de bytes**: si mando cuatro bytes, el otro tiene que saber esperar cuatro bytes para decodificar un primer codigo que le dice que viene a continuacion.
- Agregar una funcionalidad nueva a un protocolo propio es **todo un trabajo de vuelta**: hay que meterla en el protocolo.
- Con gRPC, la nueva funcionalidad son pequenas funciones en las interfaces de cliente y servidor, mas un archivo de configuracion (`.proto`) que describe datos y firmas. **El protocolo lo genera el sistema.** "Agregar funcionalidades son, si es medio complicada, algunos pocos minutos. Si es muy sencilla, segundos" ([35:11]).
- Contraste que uso: con sockets "tienen que pensarla y medir, poner una hoja de papel cuadriculado y calcular donde van cada uno de los caracteres y cada uno de los datos".

## Arquitecturas historicas mostradas
- **Hub and spoke**: sistema de comunicacion de aeropuertos con hubs centrales (Denver, Los Angeles) y nodos hoja. Todo pasaba por el hub: **punto unico de coordinacion y unico punto de falla**. Anuncio: van a hacer un ejemplo de esto **con WebSockets**, donde cada nodo es un proceso (algunos hub, otros hoja), incluso multihilo.
- **Enterprise Service Bus**: comunicacion de todos contra todos a traves de un bus central que abre los canales. Mismo problema: si se cae el bus, se cae todo.
- **Comunicacion directa en LAN**: si todas las maquinas estan en la misma red local, todas se comunican con todas sin intermediarios logicos. Repaso de **ARP**: broadcast preguntando quien es tal IP, respuesta con la MAC, y despues envio directo. Pero el medio sigue siendo compartido: si se cae el switch, quedan todas desconectadas aunque logicamente pudieran hablarse. De ahi la necesidad de **redundancia**.
- Menciono que Netflix movio la parte de servidores de base de datos a **pods**, porque escalar horizontalmente con pods resulto mas facil.

## Tema 1: multiprogramacion, procesos e hilos

### Por que multiprogramacion
- Se dice que **los procesos gastan ~80% del tiempo esperando** operaciones de entrada/salida. Para que el desperdicio de CPU sea minimo, deberia haber **al menos 10 procesos en memoria a la vez**.
- Detalle del recorrido de una E/S: la solicitud a disco es en realidad **un mensaje a otro proceso** (el que atiende las solicitudes de disco); hay buffers de memoria, hardware, un canal de comunicacion y un protocolo. Mientras eso tarda, el hilo original espera y se le puede dar el procesador a otro.
- Con 80% de espera y 20% de proceso, en el caso ideal sincronizado alcanzarian cinco procesos para saturar un hilo; en la practica no arrancan sincronizados, y de ahi las curvas de utilizacion segun cantidad de procesos y porcentaje de espera.
- Objetivo economico: si se gastan miles de dolares en hardware en la nube, se quiere **amortizarlo con uso maximo del procesador** y al mismo tiempo atender bien a los procesos. Una maquina de escritorio, en cambio, pasa la mayor parte del tiempo esperando.

### Proceso
- **Un proceso es una instancia de un programa en ejecucion.** Incluye contador de programa, registros y variables.
- Analogia usada: el programa es como un programa de teatro (dice que va a pasar y en que secuencia); al ponerlo en memoria con un procesador asignado, se transforma en proceso.
- Historia del hardware: un solo nucleo ejecutando cuatro procesos secuencialmente; despues varios procesadores para procesos **independientes**; y cuando se les ocurrio que varios procesadores trabajaran juntos sobre datos comunes, **"ahi se les pudrio todo"** y aparecieron las condiciones de carrera ([52:37]).
- Distincion: **paralelismo** = varios procesadores ejecutando de verdad al mismo tiempo. **Concurrencia** = un procesador repartiendose entre varios. Los sistemas modernos suman las dos cosas (8 nucleos, 100 procesos). **Los problemas de mutua exclusion y condicion de carrera son los mismos en ambos casos.**
- Estados de un proceso: los diagramas cambian por sistema (.NET, Linux/Unix, Windows) y los nombres cambian, pero el concepto es el mismo: corriendo, bloqueado al pedir E/S, listo cuando le responden, y en algun momento el despachador le da el procesador.

### Hilo
- **Un hilo es una tarea que se puede ejecutar al mismo tiempo que otra.** Mas precisamente: se le da a un procesador una secuencia de codigo para ejecutar.
- Todo proceso tiene al menos un hilo, el **main**. Despues se crean otros para aprovechar procesadores libres.
- Ventaja: comparten el mismo espacio de direcciones, entonces comparten datos y recursos.
- Ejemplo de por que conviene: entrenar redes neuronales exige muchisimas aproximaciones sucesivas de los parametros de la matriz; con ocho procesadores el tiempo baja a la octava parte.
- Regla que dio el docente, dicha dos veces:

> "Siempre que tengamos la oportunidad de hacerlo en multihilo, tenemos que hacerlo en multihilo." ([59:47])
> "Entonces, nunca mas programen con un solo hilo." (clase, [72:16])

- Contrapeso: hay tareas con **relaciones de precedencia muy fuertes** que no hay otra forma que hacerlas lineales. Hay que entender cuando es cada caso.
- Relacion mental util que aporto una estudiante y el docente confirmo: **varios hilos en paralelo dentro de un proceso, y varios procesos en paralelo dentro de un entorno.**

### Mapa de memoria de un proceso (repaso de arquitectura que ya no se da en la licenciatura)
El docente reconstruyo desde assembler: la CPU tiene un **contador de programa** que apunta a una direccion de la RAM; la RAM es solo una tabla de numeros; cada numero es una operacion (sumar, restar, leer otra posicion, saltar). El compilador traduce el codigo fuente a ese lenguaje de maquina y el ejecutable es exactamente esos numeros. El icono del escritorio es un archivo con una linea de comando que le dice al sistema operativo que cargue el ejecutable en memoria y salte a una etiqueta de inicio.

Disposicion del espacio virtual del proceso, de abajo hacia arriba:

| Zona | Contenido | Compartido entre hilos |
|---|---|---|
| Codigo | Instrucciones compiladas, empezando por el `main` y siguiendo con las funciones | **Si** |
| Variables estaticas | Variables globales, alcance global al programa | **Si** |
| Heap | Objetos creados con `new`; crece hacia arriba | **Si** (visible o no segun alcance, pero esta ahi) |
| Espacio libre | | - |
| Stack / pila | Direcciones de retorno y variables locales; crece desde el final de la memoria hacia abajo | **No: uno privado por hilo** |

- Como funciona una llamada a funcion: se guarda en el stack la direccion de retorno (la siguiente a la del llamado), se llama, la funcion hace `push` de sus variables locales, al terminar hace `pop` y esas variables desaparecen, se toma la direccion de retorno, se carga en el contador de programa y se vuelve. Eso explica el **alcance de las variables locales**.
- **Punto central**: con multiples hilos, ese stack se tiene que repetir por cada hilo. Codigo, estaticas y heap compartidos; **stack privado**.

> "Okey, eso tenganlo presente porque afecta mucho a distintas cosas de como usan la memoria los multiples hilos." ([70:07])

### Cambio de contexto
- **Contexto** = el conjunto de registros del procesador que dicen que instruccion se ejecuta, cual es el stack pointer y a que pedazo de memoria apunta ese hilo.
- Al pedir E/S, el sistema operativo guarda ese estado en el **process control block**, busca otro hilo o proceso en condiciones de recibir el procesador, restaura sus registros y "el procesador ahora cambio de personalidad".
- **El cambio de contexto tiene un costo que no es menor** (sacar todo del procesador, guardarlo, rescatarlo, volverlo a meter). Referencia de magnitud dada: el disco es como 1000 veces mas lento que la CPU.
- **Esa es la razon por la que despues se estudia programacion asincronica**: es un ambiente que trata de minimizar los cambios de contexto. Ata con [[Tema 3 - Asincronia y paralelismo]] y [[Concurrencia de Thread a Task]].

### Condicion de carrera
- Ocurre cuando dos o mas procesos leen o escriben datos compartidos.
- **Ocurre tanto con paralelismo como con concurrencia**, incluso con un solo hilo: un hilo leyo el dato, se bloqueo por disco, otro leyo y modifico el mismo dato, y al volver el primero hace su suma y **pierde la modificacion del segundo**.
- Los deadlocks se describieron como el peor caso operativo: sistema entero bloqueado, y reiniciar deja **datos inconsistentes sin saber cual es el inconsistente**.
- Para evitarlas: **exclusion mutua**. Se van a ver los mecanismos de C#: lectores-escritores, productor-consumidor, semaforos, regiones criticas, monitores.
- Repaso de definicion pedido en clase: **RSR = recurso serialmente reutilizable**; ejemplo, una impresora.

### Primer ejemplo de codigo: `Thread` y funcion delegada
Namespace: `System.Threading`.

Patron mostrado, llamado **patron worker**: se declara un metodo (el worker), se lo asocia a un `Thread` como **funcion delegada**, y se dispara.

```csharp
// patron worker, primer ejemplo de la clase
static void WorkerThreadMethod()
{
    Console.WriteLine("worker thread ejecutando");
}

static void Main()
{
    Console.WriteLine("main creating worker thread");
    ThreadStart worker = new ThreadStart(WorkerThreadMethod);
    Thread t = new Thread(worker);
    t.Start();                       // avisa al despachador, no arranca necesariamente ya
    Console.WriteLine("main has requested the start of worker thread");
    Console.ReadKey();               // para que no se cierre la consola
}
```

Puntos que subrayo el docente:
- `t.Start()` **no arranca el hilo**: lo pone en el despachador de tareas. "Che, despachador de tareas, en algun momento largalo" ([87:45]). En maquinas modernas potentes es practicamente instantaneo, por eso casi no se nota; en maquinas cargadas o lentas si se nota la diferencia.
- Por eso **el orden de los mensajes en la consola es incierto** y puede cambiar entre ejecuciones.
- `ThreadStart` es el tipo que envuelve al metodo para poder asignarlo al `Thread`. El uso de **funciones delegadas** va a ser constante en el curso: mas adelante, cuando llegue un mensaje, se disparara un metodo delegado que atiende el servicio.

### Hilos foreground vs background
- **Foreground** (el default al crear un hilo): cuando el `main` termina, la aplicacion **espera** a que terminen. Equivale a un **`Join` implicito** al final del `main`.
- **Background**: cuando el `main` termina, **abortan**.
- Alternativa explicita al `ReadKey`: `t.Join()`.

## Avisos y consignas de esta clase
- La proxima clase: muchos ejemplos creando hilos, `Join`, y **`lock`, `Mutex`, semaforo, monitores**.
- Van a mandar algunos codigos ese mismo dia por Teams.
- Cosa a observar al correr los ejemplos: **el entrelazado de los hilos cambia entre ejecuciones**; y como afectan variables globales vs locales.

## Connections
- Tema: [[Tema 1 - Programacion multihilo]] · [[Concurrencia de Thread a Task]]
- Complemento teorico: [[Complementario - Introduccion y Streams]]
- Anterior: [[Clase 2026-08-18 - Presentacion del curso e introduccion a sistemas distribuidos]]
- Siguiente: [[Clase 2026-08-27 - Letra del Obligatorio 1 y practica con Thread]]
