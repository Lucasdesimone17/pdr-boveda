# TEMA 2 — Comunicación basada en Sockets (PDR)

**Cobertura vigente de Aulas (semanas 4 a 6):** `Socket`, arquitectura cliente-servidor, protocolo de comunicación propio, `TcpListener`/`TcpClient` y mecanismos de exclusión mutua. Las fuentes locales agregan `NetworkStream` y recepción/envío completo de mensajes.

**Fuentes:** 🟢
- `TEORICO/SOCKETS_COMUNICACION_EN_RED/5-Sockets.pdf` (diapositiva oficial, 21 páginas) → citada como **[Slide N]**.
- `TEORICO/SOCKETS_COMUNICACION_EN_RED/SOCKETS/Guia Sockets.pdf` → **[Guía Sockets, pág. N]**.
- `TEORICO/SOCKETS_COMUNICACION_EN_RED/SOCKETS/Guía Protocolo de Comunicación - Básico.pdf` → **[Guía Protocolo, pág. N]**.
- `TEORICO/SOCKETS_COMUNICACION_EN_RED/SOCKETS/Guía TcpListener y TcpClient.pdf` → **[Guía TcpListener/TcpClient, pág. N]**.

**Procesado inicialmente el 11/08 y remapeado el 18/08.** Aulas confirmó que Sockets es el Tema 2 vigente. El antiguo bloque Web Services quedó archivado como referencia del programa de ocho unidades.

**Tags:** 🟢 Oficial · 🔵 Evidencia de evaluación · 🟠 Complementario.

---

## Sección de recuperación — preguntas antes de leer

1. Dos programas corriendo en máquinas distintas, ¿cómo hacen para "encontrarse" en la red?
2. Si ya conocés HTTP: ¿en qué capa del modelo de capas creés que vive un Socket?
3. ¿Por qué separar "el largo de un mensaje" del "mensaje en sí" podría ser útil al enviar datos por la red?
4. ¿Qué problema te imaginás que aparece si dos programas mandan/reciben datos "de a poco" en vez de todo junto?

---

## 1. El problema y la solución: qué es un Socket

🟢 **Problema:** "¿Cómo se comunican dos procesos que están en máquinas distintas?" [Slide 2].

🟢 **Definición:** "Son mecanismos de comunicación entre procesos que permiten que un programa 'hable' con otro programa incluso estando en distintas máquinas. Comunicarse = enviar y recibir datos. Es un canal de comunicación punto a punto" [Slide 4].

🟢 **Ubicación en la pila de capas** [Slide 3]: el socket vive en la capa de Aplicación, apoyado sobre TCP/UDP (Transporte), IP (Red) y Ethernet/UTP (Enlace/Física) — es el punto donde el desarrollador se conecta con el stack de red que gestiona el sistema operativo.

🟢 **Multiplataforma, pero con implementación distinta por SO** [Slide 5]: funcionan para cualquier protocolo de red, no solo TCP/IP. Windows usa la librería `Winsock32`; Unix usa la librería `socket`. La clase `Socket` de .NET es la misma en ambos, pero por debajo usa la implementación del sistema operativo correspondiente.

🟠 **Complementario (analogía de la materia — teléfono/correo):** un socket es como un número de teléfono con interno: la IP identifica el edificio (la máquina), el puerto identifica el interno (el proceso dentro de esa máquina) al que realmente querés hablar.

### Autoevaluación rápida
- ¿Por qué la fuente dice que un socket es multiplataforma y multilenguaje, pero al mismo tiempo su implementación depende del SO?

---

## 2. Endpoint: IP + Puerto

🟢 [Slide 6]: para identificar una máquina remota se usa su **dirección IP**; para identificar un proceso dentro de esa máquina se usa un **puerto**. **Endpoint = dirección IP + número de puerto**.

🟢 **Información necesaria para comunicarse con Sockets** [Slide 7]: IP remota, puerto remoto, IP local, puerto local, y protocolo de red (TCP o UDP).

### IP
🟢 [Slide 11]: comando `ipconfig` (Windows) o `ifconfig` (Mac/Linux) para ver las interfaces de red propias. `127.0.0.1` o `localhost` sirve para conectar procesos que están en la misma máquina. En Mac, `ifconfig | grep inet` o `netstat -an` para ver las IPs de conexiones existentes.

### Puerto
🟢 [Slide 12]: comando `netstat -an` para ver las conexiones y puertos en uso. Los puertos válidos van del 1 al 65535. Del 1 al 1024 están reservados para aplicaciones específicas (ej. 22 = SSH, 80 = HTTP, 443 = HTTPS). Las aplicaciones propias generalmente usan puertos mayores a 1024. **El puerto 0 especifica usar cualquier puerto disponible.**

🟢 **Puerto 0 en la práctica** [Guía Sockets, pág. 3-4]: es muy útil en el **cliente**, porque el sistema asigna dinámicamente un puerto libre — necesario cuando varios clientes corren en la misma máquina/IP, para que cada uno tenga un puerto distinto y el servidor pueda diferenciarlos. **No conviene usarlo en el servidor**: si el puerto se asigna dinámicamente, los clientes no sabrían de antemano a qué puerto conectarse.

### Autoevaluación rápida
- ¿Por qué el puerto 0 tiene sentido en el cliente pero no en el servidor?
- Si el servidor usa `127.0.0.1` como IP local, ¿qué IP tiene que usar el cliente para conectarse (en la misma máquina)?

---

## 3. TCP vs. UDP

🟢 [Slide 8]:

| | Orientado a la conexión | No orientado a la conexión |
|---|---|---|
| Conexión | Entre 2 procesos | Entre múltiples procesos a la vez |
| Confiabilidad | Mensajes confiables | Mensajes no confiables |
| Tipo de socket | Stream sockets | Datagram sockets |
| Protocolo | **TCP** | **UDP** |
| Uso típico | Transmisión de datos | Multimedia, operaciones de red no convencionales |

### Autoevaluación rápida
- ¿Por qué transmitir video en vivo suele preferir UDP y transferir un archivo importante suele preferir TCP?

---

## 4. Clase `Socket` de .NET — Socket TCP

🟢 En .NET, la clase `Socket` permite el manejo a bajo nivel de las conexiones [Slide 9]. Los sockets TCP operan en una de dos modalidades: **aceptar conexiones** o **transmisión de datos**.

### Métodos del lado servidor
🟢 [Slide 9]:

| Método | Qué hace |
|---|---|
| `Bind(IPEndPoint local)` | Enlaza el socket a una dirección y puerto específicos |
| `Listen(int c)` | Pone el socket en modalidad de aceptar conexiones |
| `Accept()` | Deja al socket a la espera de una nueva conexión |

### Métodos del lado cliente e intercambio de datos
🟢 [Slide 10]:

| Método | Qué hace |
|---|---|
| `Bind(IPEndPoint local)` | Enlaza a dirección/puerto local (opcional para el cliente) |
| `Connect(IPEndPoint remote)` | Establece la conexión con el servidor remoto |
| `Send(byte[] buffer)` | Escribe datos sobre la conexión |
| `int Receive(byte[] buffer)` | Lee datos desde la conexión |
| `Shutdown(SocketShutdown.Both)` | Deshabilita el envío y recepción en un Socket |
| `Close()` | Cierre de la conexión y liberación de recursos |

### Ejemplo mínimo (servidor + cliente con un solo cliente)

🟢 Servidor [Guía Sockets, pág. 2]:

```csharp
using System.Net;
using System.Net.Sockets;

Console.WriteLine("Starting Server Application..");
var socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
var localEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000);
socketServer.Bind(localEndpoint);
socketServer.Listen(1); // Nuestro Socket pasa a estar en modo escucha
var socketClient = socketServer.Accept(); // Bloquea el Main hasta que llegue un pedido de conexión
Console.WriteLine("Acepte un nuevo pedido de conexion");
Console.ReadLine();
```

🟢 Cliente [Guía Sockets, pág. 3]:

```csharp
using System.Net;
using System.Net.Sockets;

Console.WriteLine("Starting Client Application..");
var socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
var localEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 15000);
var remoteEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000);
socketClient.Bind(localEndpoint);
socketClient.Connect(remoteEndpoint); // Nos conectamos con el servidor
Console.WriteLine("Connected to server!!!!");
Console.ReadLine();
Console.WriteLine("Will Close Connection...");
socketClient.Shutdown(SocketShutdown.Both); // Indica en qué sentido quiero cerrar la conexión
socketClient.Close();
```

🟢 **Importante sobre el puerto del cliente** [Guía Sockets, pág. 3]: puede ser cualquiera libre en la máquina del cliente, independientemente del puerto del servidor. Si ambas apps corren en la misma máquina, el cliente tiene que usar un puerto distinto al que ya ocupa el servidor.

🟢 **Shutdown antes de Close** [Guía Sockets, pág. 3]: primero se hace `Shutdown` para asegurar que los datos restantes terminen de enviarse; una vez que termina, `Close` cierra y libera todos los recursos asociados.

### Autoevaluación rápida
- ¿Qué le pasa al servidor mientras ejecuta `socketServer.Accept()` y todavía no llegó ningún cliente?

---

## 5. Aceptar varios clientes a la vez

🟢 Con el código mínimo de la sección anterior, una vez que el servidor acepta el primer cliente, **no acepta ninguno más**. Para aceptar varios clientes hace falta [Guía Sockets, pág. 3-4]:

1. Una vez aceptado un cliente, volver a esperar uno nuevo (bucle `while (true)` alrededor del `Accept`).
2. Por cada cliente, crear un **nuevo `Thread`** que se encargue de él — evita que se bloquee todo el servidor si se traba la comunicación con un cliente en particular.

```csharp
socketServer.Listen(100); // Modo escucha
Console.WriteLine("Waiting for clients...");
while (true)
{
    Socket clientSocket = socketServer.Accept(); // Bloqueante: espera nueva conexión
    Console.WriteLine("Client connected");
    new Thread(() => HandleClient(clientSocket)).Start(); // Un hilo nuevo por cliente
}

static void HandleClient(Socket clientSocket)
{
    bool clientIsConnected = true;
    while (clientIsConnected)
    {
        Thread.Sleep(1000); // Comunicación con el cliente
    }
    Console.WriteLine("Client disconnected");
}
```

🟢 **Advertencia oficial sobre `while(true)`** [Guía Sockets, pág. 5]: es mala práctica y debería evitarse. Alternativas: un contador de conexiones máximas (poco útil, porque normalmente no se quiere limitar clientes), o variables booleanas que se cambian a `false` para salir del `while` de forma controlada — con cuidado con operaciones bloqueantes como `.Accept()`.

### `Socket.Listen(int)`: qué es realmente el parámetro

🟢 [Guía Sockets, pág. 5]: el parámetro de `Listen` **no** es cuántos clientes puede aceptar el servidor — es cuántos clientes pueden quedar en **espera (backlog)** sin que el servidor haya hecho `Accept()` todavía. Es una cola: los clientes se guardan ahí hasta que el servidor los procesa con `.Accept()`. Si se supera el backlog antes de que el servidor acepte a los que están en cola, las conexiones nuevas fallan.

### Autoevaluación rápida
- ¿Por qué crear un `Thread` nuevo por cliente evita que un cliente lento bloquee a los demás?
- ¿Qué significa exactamente `socketServer.Listen(3)`?

---

## 6. Enviar y recibir mensajes: de bytes a string

🟢 Los sockets solo envían y reciben **bytes** (0s y 1s) — hay que convertir explícitamente de `string` (u otro tipo) a `byte[]` para enviar, y de `byte[]` a `string` para leer lo recibido. Un encoder común es **UTF8** [Guía Sockets, pág. 6].

🟢 Cliente enviando un mensaje [Guía Sockets, pág. 6]:

```csharp
byte[] data = Encoding.UTF8.GetBytes(message); // Convierte de string a array de bytes
socketClient.Send(data);
```

🟢 Servidor recibiendo [Guía Sockets, pág. 7]:

```csharp
byte[] data = new byte[256]; // Buffer donde se guardan los datos recibidos
clientSocket.Receive(data); // Se bloquea hasta recibir datos del cliente
string message = $"Client says {Encoding.UTF8.GetString(data)}";
```

### Manejo de desconexión y errores

🟢 [Guía Sockets, pág. 8-9]: `socket.Receive(data)` devuelve un `int` con la cantidad de bytes recibidos. Si ese valor es **0**, la conexión fue cerrada por el otro lado. Si el cliente se desconecta antes de enviar ningún dato, `Receive` puede tirar directamente una `SocketException` — hay que envolver la recepción en `try/catch` para no romper el programa.

```csharp
static void HandleClient(Socket clientSocket)
{
    bool clientIsConnected = true;
    while (clientIsConnected)
    {
        byte[] data = new byte[256];
        try
        {
            int received = clientSocket.Receive(data);
            if (received == 0) { clientIsConnected = false; }
            else
            {
                string message = $"Client says {Encoding.UTF8.GetString(data)}";
                Console.WriteLine(message);
            }
        }
        catch (SocketException e) { clientIsConnected = false; }
    }
    Console.WriteLine("Client disconnected");
}
```

### Autoevaluación rápida
- ¿Qué significa que `Receive` devuelva 0 bytes recibidos?
- ¿Por qué hace falta un `try/catch` alrededor de `Receive` aunque ya se chequee el valor devuelto?

---

## 7. El problema del buffer fijo y el protocolo propio

🟢 **El problema** [Guía Protocolo, pág. 1-2]: si el buffer de recepción tiene un tamaño fijo (ej. 8 bytes) y el mensaje enviado es más largo, el `Receive` solo llena el buffer hasta su capacidad y corta ahí — el resto del mensaje se recibe recién en la **siguiente iteración** del `while`. Con la prueba mostrada: enviar `"12345678"` (8 bytes) funciona bien, pero enviar `"123456789"` (9 bytes) con un buffer de 8 hace que el servidor reciba primero `"12345678"` y después, en otra vuelta del bucle, `"9"` — el mensaje queda partido en dos.

🟢 **Solución — protocolo propio de largo fijo + variable** [Guía Protocolo, pág. 2-3]: todo mensaje se manda en dos partes:
1. **Largo del mensaje** — tamaño **fijo** (4 bytes, porque un `int` en C# ocupa 32 bits = 4 bytes).
2. **Mensaje** — tamaño **variable**, tantos bytes como indique la primera parte.

El receptor primero lee siempre 4 bytes (el largo), los convierte a `int`, y con ese número sabe exactamente cuántos bytes leer a continuación para el mensaje completo.

```csharp
// Codificar el largo como 4 bytes fijos
string mensaje = "Bienvenido";
int largo = mensaje.Length; // 10
byte[] largoEnBytes = BitConverter.GetBytes(largo);
// largoEnBytes.Length siempre es 4, sin importar el valor de "largo"
```

🟢 **Aplicado a cliente/servidor** [Guía Protocolo, pág. 4]:

```csharp
// Cliente: envía largo (parte fija) + mensaje (parte variable)
byte[] data = Encoding.UTF8.GetBytes(message);
int dataLength = data.Length;
socketClient.Send(BitConverter.GetBytes(dataLength));
socketClient.Send(data);

// Servidor: recibe largo (4 bytes) y luego exactamente esa cantidad de bytes del mensaje
byte[] largoData = new byte[4];
clientSocket.Receive(largoData);
int largo = BitConverter.ToInt32(largoData);
byte[] data = new byte[largo];
int received = clientSocket.Receive(data);
```

🟠 **Complementario (analogía de la materia — correo):** es como escribir en el sobre "adentro hay 3 hojas" antes de abrirlo — el que recibe sabe de antemano cuánto tiene que leer, en vez de adivinar cuándo termina el mensaje.

### Autoevaluación rápida
- ¿Por qué el largo del mensaje se codifica siempre en exactamente 4 bytes, sin importar si el número es 5 o 50000?
- ¿Qué problema exacto resuelve mandar primero el largo y después el mensaje?

---

## 8. `Send`/`Receive` no garantizan enviar/recibir todo de una vez

🟢 Cita textual de la documentación oficial de .NET, citada en la guía [Guía Protocolo, pág. 5]:

> "Send may complete successfully even if it sends less than the number of bytes you request. It is your application's responsibility to keep track of the number of bytes sent and to retry the operation until the application sends the requested number of bytes."

🟢 Lo mismo aplica a `Receive`. Ejemplo oficial: si hay 100 bytes para enviar, un `Send` puede mandar solo 40 (quedan 60), el siguiente `Send` manda 30 más (quedan 30), y un tercer `Send` termina de mandar los últimos 30. **Es responsabilidad de la aplicación reintentar hasta completar el envío/recepción total** [Guía Protocolo, pág. 5]. Esto es menos frecuente con datos chicos en la misma máquina, pero se vuelve real con datos grandes (ej. archivos).

🟢 **Firma completa usada para resolverlo** [Guía Protocolo, pág. 5]:

```csharp
int Send(byte[] buffer, int offset, int size, SocketFlags socketFlags)
```
- `buffer`: array con los datos a enviar.
- `offset`: posición desde donde seguir enviando (avanza en cada iteración).
- `size`: cantidad máxima de bytes a mandar en esa llamada.
- `socketFlags`: configuraciones adicionales (se usa `SocketFlags.None`).
- Retorno: cantidad de bytes efectivamente enviados en esa llamada.

### Patrón con `while` para garantizar el envío/recepción completos

🟢 Clase auxiliar oficial `NetworkDataHelper` (proyecto `Common`, compartido entre cliente y servidor) [Guía Protocolo, pág. 7]:

```csharp
public class NetworkDataHelper
{
    private readonly Socket _socket;
    public NetworkDataHelper(Socket socket) { _socket = socket; }

    public void Send(byte[] data)
    {
        int offset = 0;
        int size = data.Length;
        while (offset < size) // Sigue enviando hasta llegar al final
        {
            int sent = _socket.Send(data, offset, size - offset, SocketFlags.None);
            if (sent == 0) { throw new SocketException(); }
            offset += sent; // La siguiente iteración empieza donde terminó la anterior
        }
    }

    public byte[] Receive(int length)
    {
        byte[] response = new byte[length];
        int offset = 0;
        while (offset < length) // Sigue recibiendo hasta completar la array
        {
            int received = _socket.Receive(response, offset, length - offset, SocketFlags.None);
            if (received == 0) { throw new SocketException(); }
            offset += received;
        }
        return response;
    }
}
```

🟠 **Complementario:** es el mismo patrón de "recepción/envío completo de mensajes" que menciona el temario de Aulas para el bloque de Sockets — resolver esto con un `while` que avanza el `offset` es, en esencia, la respuesta técnica esperada a ese punto del temario.

### Autoevaluación rápida
- En el ejemplo de "100 bytes en 3 envíos parciales (40+30+30)", explicá qué valor tiene `offset` al empezar cada iteración del `while`.
- ¿Por qué la clase `NetworkDataHelper` lanza una `SocketException` cuando `sent == 0` o `received == 0`?

---

## 9. `TcpListener` / `TcpClient` / `NetworkStream`

🟢 **Por qué existen:** `Socket` es una abstracción de muy bajo nivel — mucha configuración manual (`AddressFamily`, `Bind`, protocolo, etc.). .NET ofrece **wrappers de más alto nivel** para cuando solo se necesita TCP: `TcpListener`, `TcpClient` y `UdpClient` [Slide 15-16; Guía TcpListener/TcpClient, pág. 1].

🟢 **Relación con Socket** [Slide 16]: por debajo, `TcpListener`, `TcpClient` y `UdpClient` usan la clase `Socket`, que a su vez usa la implementación `WinSock 2.0` del sistema operativo. Son "wrappers": se puede acceder al `Socket` interno con `tcpListener.AcceptSocket()` o `tcpClient.Client()`, aunque la idea es usar directamente el `TcpClient` y su `NetworkStream` [Guía TcpListener/TcpClient, pág. 3].

🟢 **Propósito diferenciado** [Slide 17; Guía TcpListener/TcpClient, pág. 3]:

| Clase | Para qué sirve |
|---|---|
| `TcpListener` | Administra conexiones TCP **entrantes** — solo sirve para **escuchar** conexiones, resuelve la conectividad de bajo nivel |
| `TcpClient` | Establece una conexión TCP entre dos `IPEndPoint` — se usa tanto para **conectarse** como, del lado servidor, para **trabajar** con la conexión ya aceptada |
| `NetworkStream` | Relaciona un `Stream` con un socket, para facilitar lectura/escritura sobre una conexión ya establecida |

🟢 **Diferencia clave con `Socket`** [Guía TcpListener/TcpClient, pág. 3]: los Sockets se usan tanto para escuchar como para establecer y usar conexiones (todo con la misma clase); `TcpListener` **solo escucha**, y `TcpClient` se usa para establecer/usar la conexión — son dos clases con roles separados.

### Comparación de métodos (tabla oficial)

🟢 [Slide 18]:

| Socket | TcpListener | TcpClient | NetworkStream |
|---|---|---|---|
| `.Bind(IPEndPoint local)` | `TcpListener(IPEndPoint local)` | `.Connect(IPEndPoint remote)` | `.Write(byte[] buffer, int offset, int size)` |
| `.Listen(int backlog)` | `.Start()` | `.GetStream() : NetworkStream` | `.Read(byte[] buffer, int offset, int size)` |
| `.Accept() : Socket` | `.Stop()` | `.Close()` | |
| `.Connect(IpEndPoint remote)` | `.AcceptTcpClient() : TcpClient` | | |
| `.Shutdown()` / `.Close()` | | | |
| `.Send(...)` / `.Receive(...)` | | | |

### Ejemplo mínimo: servidor con `TcpListener`

🟢 [Guía TcpListener/TcpClient, pág. 2]:

```csharp
using System.Net;
using System.Net.Sockets;

Console.WriteLine("Server is starting...");
var localEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 40000);
TcpListener tcpListener = new TcpListener(localEndpoint);
tcpListener.Start();

while (true)
{
    TcpClient tcpClient = tcpListener.AcceptTcpClient(); // Bloqueante
    var thread = new Thread(() => handleTcpConnection(tcpClient));
    thread.Start();
}
```

🟢 **Ventaja frente a `Socket`:** no hace falta especificar protocolo, `AddressFamily`, ni hacer `Bind` manual — `TcpListener` ya asume TCP [Guía TcpListener/TcpClient, pág. 2].

### Protocolo propio con `NetworkStream`

🟢 El mismo protocolo de largo fijo (4 bytes) + mensaje variable se implementa igual, pero usando `NetworkStream.Read`/`Write` en vez de `Socket.Send`/`Receive` [Guía TcpListener/TcpClient, pág. 3-4]:

```csharp
static void handleTcpConnection(TcpClient client)
{
    NetworkStream networkStream = client.GetStream();
    while (true)
    {
        byte[] dataLength = new byte[Protocol.WordLength];
        int totalReceived = 0;
        while (totalReceived < Protocol.WordLength)
        {
            int received = networkStream.Read(dataLength, totalReceived, Protocol.WordLength - totalReceived);
            if (received == 0) { networkStream.Close(); return; }
            totalReceived += received;
        }
        var length = BitConverter.ToInt32(dataLength, 0);
        // ... recepción del mensaje con la misma lógica de while, usando "length"
    }
}
```

🟢 **Diferencia importante en el envío** [Guía TcpListener/TcpClient, pág. 6]: con `NetworkStream.Write` **no** hace falta el `while` manual para garantizar el envío completo — el propio `NetworkStream` se encarga de eso internamente. Con la **recepción** (`Read`) sí sigue haciendo falta el `while`, igual que con `Socket.Receive`.

🟢 **Cierre de conexión** [Guía TcpListener/TcpClient, pág. 5]: alcanza con `networkStream.Close()`. No hace falta el `Shutdown` previo que sí se recomienda con `Socket`.

### Conclusiones oficiales de la guía

🟢 [Guía TcpListener/TcpClient, pág. 7]:
- Tanto `Socket` como `TcpListener`/`TcpClient` sirven para enviar bytes por la red.
- Los "Tcp wrappers" separan `TcpListener` (solo escucha) de `TcpClient` (establece y administra la conexión).
- `TcpListener`/`TcpClient` requieren menos configuración que `Socket`.
- Los Tcp wrappers usan `NetworkStream` para enviar/recibir; los Sockets usan directamente el objeto `Socket`.
- Con `NetworkStream`, el envío no necesita `while` manual, pero la recepción sí.

### Autoevaluación rápida
- ¿Por qué `TcpListener` "solo sirve para escuchar" y no para enviar/recibir datos una vez establecida la conexión?
- ¿Qué gana en simplicidad el código de la Guía TcpListener/TcpClient frente al código equivalente con `Socket` puro?

---

## 10. `UdpClient` y métodos asincrónicos

🟢 **UdpClient** [Slide 20]: librería de alto nivel para intercambiar **datagramas** entre dos `IPEndPoint`.

| Método | Qué hace |
|---|---|
| `UdpClient(IPEndPoint)` | Crea el cliente enlazando con el extremo local especificado |
| `Send(byte[], int, IPEndPoint)` | Envía el datagrama al extremo remoto especificado |
| `Receive(IPEndPoint) : byte[]` | Espera datagramas del extremo remoto especificado |

🟢 **Métodos asincrónicos** [Slide 19]: varias operaciones de .NET tienen su versión "Async", que devuelve `Task`/`Task<...>` y permite usar `await` (tema desarrollado en profundidad en Tema 3 — Paralelismo). Ejemplo: `Accept() : Socket` → `AcceptAsync() : Task<Socket>`. Aplica a todos los métodos de E/S: Sockets, `TcpListener`, `TcpClient`, lectura/escritura a disco, etc.

### Autoevaluación rápida
- ¿Por qué UDP usa el término "datagrama" en vez de "conexión" como TCP?

---

## 11. Ventajas y desventajas de Sockets

🟢 [Slide 14]:

| Ventajas | Desventajas |
|---|---|
| Tecnología eficiente y confiable para comunicación entre procesos | Solo resuelve la capa de transporte de datos |
| Permite diversas operaciones de red | Hay que programar la codificación/decodificación de datos manualmente |
| Más control sobre el canal de comunicación | Dificultad para agregar nuevas operaciones |
| Multiplataforma y multilenguaje | |

🟠 **Complementario:** esto explica por qué el resto de la materia (RPC/gRPC, MOM/RabbitMQ, REST) existe — son formas de resolver "las desventajas" de trabajar con Sockets puros, agregando capas de abstracción sobre este mecanismo base.

---

## Checkpoint oficial de la fuente

🟢 Preguntas de repaso que trae la propia diapositiva [Slide 21]:

1. ¿Qué hace `socket.Listen(int)`?
2. ¿Puedo tener un servidor usando `Socket` y un cliente usando `TcpClient`?
3. ¿En qué caso uso `Socket` y en qué caso uso `TcpClient`?
4. ¿Qué limitaciones tiene usar la IP `127.0.0.1`?
5. ¿Por qué no enviamos los archivos de la misma manera que mandamos el resto de datos?

🟠 **Complementario (respuestas razonadas, no explícitas en la fuente):**
1. Es el tamaño del backlog (cola de clientes en espera de `Accept`), no un límite total de clientes.
2. Sí, técnicamente son compatibles porque `TcpClient` es un wrapper de `Socket` sobre el mismo protocolo TCP — mientras ambos lados respeten TCP, pueden combinarse.
3. `Socket` cuando se necesita control de bajo nivel o soporte de protocolos distintos a TCP (ej. UDP); `TcpClient` cuando alcanza con TCP y se prioriza código más simple.
4. `127.0.0.1` solo permite comunicar procesos dentro de la misma máquina — no sirve para comunicarse con otra computadora en la red.
5. Un archivo puede ser mucho más grande que cualquier buffer razonable y su tamaño no se conoce siempre de antemano de la misma forma que un mensaje corto — típicamente requiere enviar por partes (chunks) y un protocolo que indique el tamaño total o marque el fin de la transmisión. **Pendiente de verificar:** el manejo específico de archivos con Sockets no está desarrollado en el material disponible.

---

## Resumen ejecutivo del tema

1. Un socket es un canal de comunicación punto a punto identificado por un **endpoint** (IP + puerto); para hablar con un proceso remoto hacen falta IP/puerto local y remoto, más el protocolo (TCP o UDP).
2. TCP es orientado a la conexión y confiable (stream sockets); UDP no está orientado a la conexión y no es confiable, pero soporta múltiples destinatarios (datagram sockets).
3. La clase `Socket` de .NET es de bajo nivel: el servidor hace `Bind` → `Listen` → `Accept`; el cliente hace `Connect`; ambos usan `Send`/`Receive` con bytes.
4. Aceptar varios clientes requiere un `while(true)` alrededor del `Accept` y lanzar un `Thread` nuevo por cliente — con las salvedades oficiales sobre evitar `while(true)` sin control.
5. Los sockets solo transportan bytes: hay que codificar/decodificar explícitamente (ej. `Encoding.UTF8`), y un buffer de tamaño fijo puede partir un mensaje largo en varias lecturas.
6. La solución es un **protocolo propio**: mandar primero el largo del mensaje en un tamaño fijo (4 bytes, un `int`), y después el mensaje de largo variable — el receptor sabe así cuánto leer.
7. `Send`/`Receive` no garantizan mover todos los bytes en una sola llamada — hace falta un `while` que reintente con `offset` hasta completar, patrón encapsulado en la clase auxiliar `NetworkDataHelper`.
8. `TcpListener`/`TcpClient`/`NetworkStream` son wrappers de más alto nivel sobre `Socket`, exclusivos para TCP: `TcpListener` solo escucha, `TcpClient` establece y administra la conexión, y `NetworkStream` simplifica el envío/recepción (el envío ya no necesita `while` manual, la recepción sí).
9. `UdpClient` es el wrapper equivalente para UDP; casi todos los métodos de E/S de sockets tienen su versión `Async` que devuelve `Task` para usar con `await`.

---

## Repaso espaciado

### Día 3
1. Definí "endpoint" con tus palabras y dá un ejemplo concreto (IP + puerto).
2. ¿Qué diferencia hay entre `Socket.Listen(3)` y "el servidor solo acepta 3 clientes en total"?
3. ¿Por qué hace falta mandar el largo del mensaje antes que el mensaje mismo?
4. Nombrá 2 diferencias entre `Socket` y `TcpListener`/`TcpClient`.

### Día 7
1. Escribí de memoria (sin mirar el resumen) el esqueleto mínimo de un servidor y un cliente usando `Socket` puro.
2. Explicá el problema del buffer de tamaño fijo con un ejemplo propio, distinto al de "12345678"/"123456789" de la guía.
3. ¿Por qué el envío con `NetworkStream.Write` no necesita `while` pero `Socket.Send` sí lo necesita para garantizar que se manden todos los bytes?
4. Repasá las 5 preguntas del "Checkpoint" oficial (Slide 21) sin mirar las respuestas razonadas.

---

## Pendiente de confirmar

- El manejo específico de envío de **archivos** con Sockets (mencionado en el checkpoint oficial, pregunta 5) no está desarrollado en ningún material disponible — solo se plantea la pregunta, no la solución.
- La relación con Web Services ya no es un pendiente operativo: el temario vigente ubica Sockets explícitamente como Tema 2 y APIs REST como Tema 6.
- 🔵 Evidencia de parcial: Sockets aparece en 14 de 16 letras históricas revisadas, con protocolos propios y servidores multi-cliente como formatos recurrentes.
- No se leyó en profundidad la documentación externa de Microsoft enlazada dentro de las guías (ej. documentación de `Socket.Listen`, `Socket.Send`) — se tomó el contenido tal como está citado en los PDF oficiales de la materia.
