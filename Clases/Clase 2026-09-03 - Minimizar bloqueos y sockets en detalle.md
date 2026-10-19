---
date: 2026-09-03
type: clase
tags:
  - clase
  - pdr
  - tema-1
  - tema-2
ai-first: true
confidence: stated
fuente: "Grabacion Vimeo, showcase 12367503, video 03-09-2026, 51m, subtitulos autogenerados"
docentes:
  - Enrique Latorres
  - Natalia Franco
---

# Clase 2026-09-03 - Minimizar bloqueos y sockets en detalle

## For future agent
Acta de la clase del 03-09-2026 (Vimeo, showcase 12367503). Es la clase mas corta (51 minutos) y tiene dos partes muy concretas: **una demo medida de por que hay que minimizar los bloqueos** (40 segundos vs 4 segundos con el mismo problema), y **la anatomia de un cliente y un servidor de sockets en C#** paso por paso. La demo del "acomodador de boletos" es el mejor argumento del curso sobre diseno de concurrencia y conviene tenerla presente para el Obligatorio 1.

## Parte 1: la demo del acomodador - minimizar bloqueos

### Version lenta (la ingenua)
Productor-consumidor en C# con semaforos. Parametros de la corrida:
- Buffer circular de **3 lugares**.
- **5 productores** que producen 20 items cada uno = 100.
- **10 consumidores** que consumen 10 items cada uno = 100.
- **Insertar o sacar del buffer cuesta 200 ms simulados** (piensen en generar un PDF, o manipular una estructura compleja).

Estructura: la clase `BufferCircular` tiene tres semaforos - `consumerLock` (inicializado en 0, maximo = capacidad), `producerLock` (inicializado en la capacidad, minimo 0) y **uno de exclusion mutua con maximo 1**.

El defecto: **el mismo semaforo de exclusion mutua cubre tanto poner como sacar, y cubre tambien los 200 ms de trabajo.** Consecuencia:

> "Eso quiere decir que a la vez solo uno y un solo hilo, ya sea productor o consumidor, esta poniendo o sacando del buffer." ([05:12])

Analogia que uso: el docente esta en la puerta con dos filas, productores y consumidores; deja pasar de a uno, ese hace todo su trabajo adentro, sale, y recien entra el siguiente.

**Tiempo medido: ~40 segundos.**

### Version rapida (el acomodador / la boletera)
Pregunta que planteo a la clase: como dejar que **todos** entren a la vez, cada uno sabiendo que lugar le toca, sin tropezarse. Pista que dio: **el numerito que te dan en un cine o un teatro**.

Solucion: **un acomodador que reparte numeros de asiento**. Dar un numero es muchisimo mas rapido que poner o sacar el artefacto de cada lugar.
- **Dos "boleteras"**, cada una un buffer productor-consumidor **de numeros**: una con los numeros de los lugares libres (para productores) y una vacia (para consumidores).
- Llega un productor: se le da un numero de lugar libre, y entra a producir **en su lugar**, en paralelo con todos los demas.
- Cuando el productor sale, **devuelve el numero**, y ese numero se apila en la cola de consumidores. **El orden es el de salida de los productores**, que es exactamente el orden en que los consumidores deben consumir.
- El `Mutex` queda **solo para las variables de la boletera** (contador, puntero de entrada, puntero de salida), que es una operacion trivial. **Los 200 ms quedan fuera del bloqueo.**

**Tiempo medido: ~4,3 segundos.** Mismo problema, mismas condiciones, **relacion 1 a 10**.

> "Piensen cuando ponen algo en produccion, si no piensan bien los algoritmos y los bloqueos que van a hacer, pueden hacer cosas que sean insoportablemente lentas." ([21:43])

### Corolarios que dio
- **Preferir estructuras que ya traen la concurrencia adentro.** `ConcurrentDictionary` esta disenado para multiples hilos e internamente usa **lectores-escritores**: si solo se lee y no hay escritor adentro, **deja entrar a todos**. Eso es mucho mejor que tomar un `Dictionary` comun y ponerle un `lock` de arriba a abajo - que es exactamente el error de la version lenta.
- **Los `Sleep` son solo para pruebas o para simular trabajo.** Y aumentar la probabilidad de un orden **no lo garantiza**: "Nadie garante eso. 100 % no" ([63:38]).
- Historia de produccion, del Ministerio de Transporte: se licito un sistema de expedientes que quedo **terriblemente lento**. El docente retraso los ultimos pagos y pelearon casi dos anos; **se resolvio recien cuando Intel saco una nueva generacion de procesadores** y cambiaron el servidor. Moraleja: "cuando uno hace un software, la idea es que tenga claro que haya algun procesador en el universo que pueda correrlo" ([23:46]).
- Objetivo del curso, dicho como sintesis: separar procesos para que corran en maquinas diferentes, repartirlos, **usar todos los nucleos** de cada procesador, y aun asi cumplir los requisitos funcionales.

## Parte 2: sockets en C#

### Repaso
- Se comunican a nivel de **capa de aplicacion**; abajo esta el stack de comunicacion.
- **TCP**: orientado a conexion, lleva la cuenta de paquetes, **ACK** integrado, retransmite lo que no llego. **UDP**: se manda y listo, sin handshake ni ACK integrado; sirve para streaming.
- **Endpoint** = IP + puerto. Para comunicarse hacen falta **los dos endpoints** (remoto y local) **y el protocolo**. En .NET hay una clase `IPEndPoint` que junta las dos cosas y se le pasa a la biblioteca.

### Las dos modalidades del socket
1. **Aceptar conexiones** (lado servidor).
2. **Transmitir datos.**

La clase `Socket` de .NET es **de bajo nivel**; mas adelante en el curso se van a usar abstracciones un poco mas altas.

### API, lado servidor

| Llamada | Que hace |
|---|---|
| `Bind` | Conecta el socket a una direccion y un puerto. Aca puede fallar si el puerto ya esta usado por otro proceso |
| `Listen(backlog)` | Pone el socket en modo servidor, abierto a solicitudes de conexion |
| `Accept` | Deja el socket esperando una nueva conexion; devuelve **otro socket, ya conectado** |

- El socket que devuelve `Accept` es el que **se le pasa al hilo que va a atender a ese cliente**.
- **El numero de `Listen` es el backlog**, el tamano de un buffer de conexiones pendientes: cuantas conexiones se pueden guardar mientras todavia no fueron aceptadas.
  - Si el backlog es chico y hay un pico, los clientes reciben **error de conexion**.
  - Si es enorme (por ejemplo 1000), **ocupa espacio** al gusto.
  - **Detalle no obvio, dicho explicitamente**: "Ese numero funciona en Windows, en Linux, no. Linux lo maneja de otra forma, lo maneja a nivel de socket mismo, en forma automatica. Este mismo codigo funciona diferente en Windows que en Linux" ([46:29]).

### API, lado cliente

| Llamada | Que hace |
|---|---|
| `Bind` | Igual que el servidor: fija el endpoint local |
| `Connect` | Intenta conectarse al endpoint remoto |

- **El cliente puede usar puerto 0**: le pide al socket que busque un puerto libre disponible. **El servidor no puede**: necesita un puerto conocido, si no el cliente no sabe a donde mandar.
- Se le puede fijar un puerto al cliente si hay restricciones (por ejemplo, que sea el unico puerto que pasa el firewall).

### Intercambio de datos y cierre

| Llamada | Que hace |
|---|---|
| `Send` | Escribe los datos y los manda. Recibe un **array de bytes** |
| `Receive` | Lee los datos en un buffer de bytes |
| `Shutdown` | Cierra el envio y/o la recepcion |
| `Close` | Cierra la conexion y libera |

- **Todo se convierte a bytes.** "Byte es la estructura mas basica de la memoria. Entonces, lo que sea que haya, hay que convertirlo a una estructura de ese estilo para transmitir" ([31:08]).
- **Buena practica explicita**: no hacer solo `Close`, hacer **`Shutdown` y despues `Close`**.

### Utilitarios y convenciones
- Ver la IP: **`ipconfig`** en Windows, **`ifconfig`** en Mac/Linux.
- **`netstat`**: ver los puertos ya usados en la maquina, para no elegir uno ocupado. Si se elige uno ocupado, el `Bind` da error.
- Para probar cliente y servidor en la misma maquina se usa **`localhost` / 127.x**, que no esta expuesto afuera. Se corren dos proyectos por separado que dialogan.
- Cuando haya que probar entre maquinas distintas, hay que elegir **una tarjeta con acceso a red**, accesible desde la otra maquina.
- Rango de puertos: hasta **1024 estan reservados**. **En el curso se usan puertos a partir de 1024.** En los ejemplos el servidor usa el **20000** y el cliente el 1500 (o 0).

### Correr varias instancias en el IDE (necesario para probar el Obligatorio 1)
- **Visual Studio**: propiedades de la solucion, **"Multiple startup projects"**.
- **Rider** (el recomendado): `Run` -> `Edit configurations`, elegir el proyecto y **habilitar varias instancias**. Eso permite abrir varias ejecuciones del mismo proyecto - por ejemplo **un servidor y tres clientes**, para probar que el servidor aguante varios clientes y no se caiga.

### Ventajas y desventajas de trabajar con sockets crudos (lo que dijo la clase)
- **Ventajas**: eficiente y confiable; al ser de bajo nivel **da control total** sobre la comunicacion; es **multiprotocolo** - no solo TCP, y en teoria se podria implementar un protocolo de red propio.
- **Desventajas**: **solo resuelve la capa de transporte**; hay que **programar la codificacion y decodificacion** de todo lo que se transmita - como se serializa y como se deserializa.

### El primer ejemplo de codigo (solo conexion, sin datos)
El ejemplo mostrado crea el socket, hace `Bind`, `Listen` y `Accept` del lado servidor, y `Bind` y `Connect` del lado cliente, imprime que se conecto y despues hace `Shutdown` y `Close`. **No intercambia datos todavia.**

```csharp
// servidor, esqueleto del ejemplo de clase
Socket socketServidor = new Socket(
    AddressFamily.InterNetwork,   // familia de protocolos de Internet
    SocketType.Stream,            // stream
    ProtocolType.Tcp);            // TCP

var localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000);
socketServidor.Bind(localEndPoint);   // une el socket a esa direccion y puerto
socketServidor.Listen(1);             // backlog; en Linux este numero se ignora
Socket socketCliente = socketServidor.Accept();  // devuelve un socket YA conectado
```

```csharp
// cliente, esqueleto del ejemplo de clase
Socket socketCliente = new Socket(
    AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp);            // MISMO protocolo que el servidor

var localEndPoint  = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1500);  // 0 = puerto libre
var remoteEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000);
socketCliente.Bind(localEndPoint);
socketCliente.Connect(remoteEndPoint);
// ...
socketCliente.Shutdown(SocketShutdown.Both);
socketCliente.Close();
```

- Punto que remarco: **la linea de creacion del socket es identica en cliente y servidor**. "Ahi lo que estan garantizando es que estan usando el mismo protocolo cliente y servidor" ([48:32]).
- Sobre la IP cableada: "Despues, nosotros vamos a ver como hacer para tomar un archivo de configuracion o variables de ambiente y con eso, sin tocar el codigo, darle como parametros la direccion" ([41:16]). **Esto es requerimiento del Obligatorio 1** (portabilidad sin recompilar).

## Connections
- Temas: [[Tema 1 - Programacion multihilo]] · [[Tema 2 - Sockets y protocolos]]
- Evaluacion: [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]]
- Errores: [[Errores caros en Programacion de Redes]]
- Anterior: [[Clase 2026-09-01 - Exclusion mutua en CSharp y arranque de sockets]]
- Siguiente: [[Clase 2026-09-08 - Protocolo propietario, framing y Receive parcial]]
