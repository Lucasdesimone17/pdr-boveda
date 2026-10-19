# Cheat Sheet — TEMA 2: Comunicación basada en Sockets (PDR)

Fuente: 🟢 `5-Sockets.pdf` + 3 guías oficiales de `SOCKETS/`. Sin prosa, solo tablas/sintaxis de referencia rápida.

## Endpoint

| Concepto | Definición |
|---|---|
| Endpoint | IP + puerto |
| IP remota/local | Identifican la máquina |
| Puerto remoto/local | Identifican el proceso dentro de la máquina |
| Puerto 0 | "Cualquier puerto disponible" — útil en cliente, mala idea en servidor |
| Puertos 1-1024 | Reservados (22 SSH, 80 HTTP, 443 HTTPS) |
| Puertos válidos | 1 a 65535 |
| Comandos | `ipconfig` (Windows) / `ifconfig` (Mac/Linux) — interfaces; `netstat -an` — conexiones/puertos en uso |

## TCP vs UDP

| | TCP | UDP |
|---|---|---|
| Conexión | Orientada a conexión | No orientada a conexión |
| Confiabilidad | Confiable | No confiable |
| Destinatarios | 1 a 1 | 1 a muchos |
| Tipo de socket | Stream socket | Datagram socket |
| Uso | Transmisión de datos | Multimedia / operaciones no convencionales |

## Socket (bajo nivel) — Servidor

```csharp
var socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
var localEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000);
socketServer.Bind(localEndpoint);
socketServer.Listen(100);          // backlog: clientes en espera, NO límite total
var clientSocket = socketServer.Accept(); // bloqueante
```

## Socket (bajo nivel) — Cliente

```csharp
var socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
var localEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0); // 0 = puerto libre
var remoteEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000);
socketClient.Bind(localEndpoint);
socketClient.Connect(remoteEndpoint);
socketClient.Send(data);
socketClient.Receive(buffer);
socketClient.Shutdown(SocketShutdown.Both); // antes de Close
socketClient.Close();
```

## Aceptar múltiples clientes

```csharp
while (true)
{
    Socket clientSocket = socketServer.Accept();
    new Thread(() => HandleClient(clientSocket)).Start(); // 1 hilo por cliente
}
```

⚠️ `while(true)` sin control = mala práctica oficial. `Listen(n)` = tamaño del backlog, no cantidad total de clientes.

## Bytes ↔ String

| Dirección | Código |
|---|---|
| string → bytes | `Encoding.UTF8.GetBytes(mensaje)` |
| bytes → string | `Encoding.UTF8.GetString(data)` |
| int → 4 bytes | `BitConverter.GetBytes(largo)` |
| 4 bytes → int | `BitConverter.ToInt32(dataLength, 0)` |

## Detección de desconexión

| Señal | Significado |
|---|---|
| `Receive` devuelve 0 | Conexión cerrada por el otro lado |
| `Receive` tira `SocketException` | Cliente se cerró antes de enviar nada — usar try/catch |

## Protocolo propio (largo fijo + variable)

```
[ Largo del mensaje (4 bytes, fijo) ][ Mensaje (N bytes, variable) ]
```

```csharp
// Enviar
byte[] data = Encoding.UTF8.GetBytes(message);
socketClient.Send(BitConverter.GetBytes(data.Length)); // parte fija
socketClient.Send(data);                                 // parte variable

// Recibir
byte[] largoData = new byte[4];
clientSocket.Receive(largoData);
int largo = BitConverter.ToInt32(largoData, 0);
byte[] data = new byte[largo];
clientSocket.Receive(data);
```

## Send/Receive no garantizan todo de una vez

```csharp
public void Send(byte[] data)
{
    int offset = 0, size = data.Length;
    while (offset < size)
    {
        int sent = _socket.Send(data, offset, size - offset, SocketFlags.None);
        if (sent == 0) throw new SocketException();
        offset += sent;
    }
}

public byte[] Receive(int length)
{
    byte[] response = new byte[length];
    int offset = 0;
    while (offset < length)
    {
        int received = _socket.Receive(response, offset, length - offset, SocketFlags.None);
        if (received == 0) throw new SocketException();
        offset += received;
    }
    return response;
}
```

## Socket vs TcpListener/TcpClient/NetworkStream

| | Socket | TcpListener | TcpClient | NetworkStream |
|---|---|---|---|---|
| Rol | Escucha + establece + usa | Solo escucha | Establece/usa conexión | Envío/recepción |
| Nivel | Bajo | Alto (wrapper) | Alto (wrapper) | Alto |
| Protocolo | Cualquiera (TCP/UDP/...) | Solo TCP | Solo TCP | — |
| Envío garantizado | Requiere `while` manual | — | — | `Write` ya garantiza envío completo |
| Recepción garantizada | Requiere `while` manual | — | — | `Read` requiere `while` igual que Socket |
| Cierre | `Shutdown` + `Close` | — | — | Solo `Close` |

## Comparación de métodos (tabla oficial)

| Socket | TcpListener | TcpClient | NetworkStream |
|---|---|---|---|
| `.Bind(local)` | `TcpListener(local)` | `.Connect(remote)` | `.Write(buf, off, size)` |
| `.Listen(backlog)` | `.Start()` | `.GetStream()` | `.Read(buf, off, size)` |
| `.Accept()` | `.Stop()` | `.Close()` | |
| `.Connect(remote)` | `.AcceptTcpClient()` | | |
| `.Send()`/`.Receive()` | | | |

## TcpListener — servidor mínimo

```csharp
var localEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 40000);
TcpListener tcpListener = new TcpListener(localEndpoint);
tcpListener.Start();
while (true)
{
    TcpClient tcpClient = tcpListener.AcceptTcpClient();
    new Thread(() => handleTcpConnection(tcpClient)).Start();
}
```

## UdpClient

| Método | Qué hace |
|---|---|
| `UdpClient(IPEndPoint)` | Crea cliente, enlaza extremo local |
| `Send(byte[], int, IPEndPoint)` | Envía datagrama al remoto |
| `Receive(IPEndPoint) : byte[]` | Espera datagrama del remoto |

## Métodos asincrónicos

| Sync | Async |
|---|---|
| `Accept() : Socket` | `AcceptAsync() : Task<Socket>` |
| Aplica a Sockets, TcpListener, TcpClient, disco, etc. | Devuelven `Task`/`Task<T>`, se usan con `await` |

## Ventajas / Desventajas de Sockets

| Ventajas | Desventajas |
|---|---|
| Eficiente, confiable | Solo capa de transporte |
| Control total del canal | Codificación/decodificación manual |
| Multiplataforma/multilenguaje | Difícil agregar nuevas operaciones |

## Preguntas típicas de parcial

🔵 **Frecuencia histórica:** Sockets aparece en 14 de 16 letras revisadas. Practicar framing, protocolo propio, recepción/envío completo y servidor multi-cliente.
