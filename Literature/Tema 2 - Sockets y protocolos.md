---
date: 2026-08-26
type: literature
tags:
  - literature
  - pdr
  - tema-2
  - sockets
  - tcp
  - csharp
ai-first: true
source: "Sources/TEORICO/SOCKETS_COMUNICACION_EN_RED/"
source_extra:
  - "Sources/ESTUDIO_PREVIO/TEMA_02_SOCKETS/CHEATSHEET.md"
confidence: stated
semanas: "4-6"
frecuencia_parcial: "14/16 (88%)"
---

# Tema 2 - Sockets y protocolos

## For future agent
Nota de tema 2 del temario vigente (semanas 4-6): sockets como base de todos los demás mecanismos de comunicación del curso. Frecuencia histórica de parcial: **14 de 16 letras (88%)**. Lo evaluable es siempre lo mismo: diseñar un protocolo propio con framing, garantizar envío y recepción completos, y atender múltiples clientes. Fuentes: `5-Sockets.pdf` y las tres guías de sockets.

## Source
`Sources/TEORICO/SOCKETS_COMUNICACION_EN_RED/` - `5-Sockets.md`, `Guia Sockets.md`, `Guía Protocolo de Comunicación - Básico.md`, `Guía TcpListener y TcpClient.md`.

## Key Claims

### Endpoint y puertos
- **Endpoint = IP + puerto**. La IP identifica la máquina, el puerto identifica el proceso dentro de la máquina.
- Puerto `0` significa "cualquier puerto disponible": útil en el cliente, mala idea en el servidor. Rango válido 1-65535; 1-1024 reservados (22 SSH, 80 HTTP, 443 HTTPS).
- Diagnóstico: `ipconfig` / `ifconfig` para interfaces, `netstat -an` para conexiones y puertos en uso.

### TCP vs UDP
| | TCP | UDP |
|---|---|---|
| Conexión | Orientada a conexión | Sin conexión |
| Confiabilidad | Confiable | No confiable |
| Destinatarios | 1 a 1 | 1 a muchos |
| Tipo de socket | Stream socket | Datagram socket |
| Uso típico | Transmisión de datos | Multimedia, operaciones no convencionales |

### Ciclo del servidor y del cliente
- Servidor: `new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)` → `Bind(endpointLocal)` → `Listen(backlog)` → `Accept()` (bloqueante).
- Cliente: crear socket → `Bind` local (puerto 0) → `Connect(endpointRemoto)` → `Send` / `Receive` → `Shutdown(SocketShutdown.Both)` → `Close()`.
- `Listen(n)`: **n es el tamaño del backlog** (clientes en espera de ser aceptados), no el máximo total de clientes. Confundir esas dos cosas es error de examen.

### Multi-cliente
```csharp
while (true)
{
    Socket clientSocket = socketServer.Accept();
    new Thread(() => HandleClient(clientSocket)).Start(); // un hilo por cliente
}
```
El material marca `while(true)` sin control como mala práctica, aunque lo usa por simplicidad.

### El punto central: Send y Receive no garantizan el mensaje completo
Esto es lo que el curso realmente evalúa. Hay que envolver ambos en un ciclo por offset:

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

### Protocolo propio: framing largo fijo + parte variable
```
[ Largo del mensaje (4 bytes, fijo) ][ Mensaje (N bytes, variable) ]
```
Envío: `Send(BitConverter.GetBytes(data.Length))` y después `Send(data)`. Recepción: leer 4 bytes, `BitConverter.ToInt32(largoData, 0)`, y leer exactamente ese largo.

Conversiones: `Encoding.UTF8.GetBytes` / `GetString` para texto, `BitConverter.GetBytes` / `ToInt32` para el entero del largo.

### Detección de desconexión
- `Receive` devuelve `0`: el otro lado cerró la conexión.
- `Receive` lanza `SocketException`: el cliente se cerró antes de enviar nada. Hay que envolver en `try`/`catch`.

### Socket vs los wrappers de alto nivel
| | `Socket` | `TcpListener` | `TcpClient` | `NetworkStream` |
|---|---|---|---|---|
| Rol | Escucha, establece y usa | Solo escucha | Establece y usa conexión | Envía y recibe |
| Nivel | Bajo | Alto | Alto | Alto |
| Protocolo | Cualquiera | Solo TCP | Solo TCP | - |
| Envío completo | Requiere ciclo manual | - | - | `Write` ya lo garantiza |
| Recepción completa | Requiere ciclo manual | - | - | `Read` **igual requiere ciclo** |
| Cierre | `Shutdown` + `Close` | - | - | Solo `Close` |

Ese último detalle es el que se pregunta: subir de nivel con `NetworkStream` resuelve el envío, **no** la recepción.

- `UdpClient`: `Send(byte[], int, IPEndPoint)` y `Receive(ref IPEndPoint)`.
- Versiones asíncronas: `AcceptAsync()` devuelve `Task<Socket>` y se usa con `await` (enlaza con [[Tema 3 - Asincronia y paralelismo]]).

### Ventajas y desventajas
| Ventajas | Desventajas |
|---|---|
| Eficiente y confiable | Solo llega a la capa de transporte |
| Control total del canal | Codificación y decodificación manuales |
| Multiplataforma y multilenguaje | Difícil agregar operaciones nuevas |

Esa última columna es exactamente la motivación de los temas 4, 5 y 6.

## Methodology
Teórico oficial más tres guías: una de sockets crudos, una de diseño de protocolo básico y una de los wrappers `TcpListener`/`TcpClient`. La progresión va de bajo nivel a wrapper, mostrando qué problema resuelve cada capa.

## Critique
- El material usa `while(true)` y lo desaconseja en la misma página, sin mostrar la alternativa (token de cancelación, señal de apagado). El cierre ordenado del servidor queda como ejercicio.
- No trata timeouts de socket ni `KeepAlive`, que en un servidor real son obligatorios.
- Presenta un hilo por cliente sin discutir el límite de escalabilidad, que es justo lo que motiva `async`/`await` en el tema siguiente.

## Lo que agregaron los docentes en clase (as of 2026-09-09)
Del dictado real del semestre (clases del 01-09, 03-09 y 08-09-2026). El tema está a mitad de camino: se dio hasta la conexión y el framing; falta el servidor multi-cliente completo y los wrappers.

- **La capa de socket no es específica de TCP/IP.** Es multiprotocolo; IP se volvió el estándar mundial, pero hubo protocolos populares que quedaron afuera, y Microsoft mantiene protocolos propios en sus redes. La clase de .NET se apoya en **WinSock** en Windows y en **sockets** en Linux/Unix, muy parecidas porque respetan el mismo estándar.
- **Endpoint = IP + puerto.** Hacen falta los **dos** endpoints (local y remoto) **más el protocolo**. En .NET la clase `IPEndPoint` junta IP y puerto y se le pasa a la biblioteca; la IP hay que pasarla por `IPAddress.Parse`.
- **La línea de creación del socket es idéntica en cliente y servidor.** Eso es lo que garantiza que usen el mismo protocolo.
- **El backlog de `Listen` es un buffer de conexiones pendientes**, y el número **funciona distinto en Windows y en Linux**: Linux lo maneja a nivel de socket en forma automática. Cita: "este mismo código funciona diferente en Windows que en Linux". Si el backlog es chico, en un pico los clientes reciben error de conexión; si es enorme, ocupa memoria al gusto.
- **`Accept` devuelve otro socket, ya conectado**, y ese es el que se le pasa al hilo que atiende a ese cliente.
- **El cliente puede usar puerto 0** (le pide uno libre al sistema); **el servidor no puede**, necesita un puerto conocido. En el curso se usan **puertos a partir de 1024**; en los ejemplos el servidor usa 20000.
- **Buena práctica explícita: `Shutdown` antes de `Close`**, no solo `Close`.
- Utilitarios: `ipconfig` / `ifconfig` para la IP, y **`netstat`** para ver puertos ocupados (si se elige uno ocupado, falla el `Bind`).
- **Correr varias instancias en el IDE**: en Rider, `Run` → `Edit configurations` → habilitar varias instancias; en Visual Studio, *multiple startup projects*. Es lo que permite probar un servidor y tres clientes.
- **Ventajas y desventajas de sockets crudos, tal como las listó la clase**: eficiente, confiable, control total, multiprotocolo; contra eso, **solo resuelve la capa de transporte** y hay que programar toda la codificación y decodificación.

### Framing: por qué existe el campo Largo (clase del 08-09)
Esta parte responde justo lo que esta nota marcaba como hueco.

- Abajo, TCP entrega **un stream**: conceptualmente una lista potencialmente infinita de bytes, **sin delimitación de mensajes**. Mandar largo y datos juntos o separados da casi lo mismo en la red.
- El problema real es dimensionar el buffer de recepción: **muy chico** corta el mensaje, **muy grande** desperdicia memoria en cada iteración. El `Largo` resuelve las dos cosas y además dice **si falta recibir**.
- **El `Largo` tiene tamaño fijo**, porque si fuera variable haría falta otro largo para el largo. Dos técnicas válidas:
  - **Texto con ceros a la izquierda** (`PadLeft`) y después a bytes con un encoding.
  - **`BitConverter.GetBytes`**, que garantiza tamaño por sobrecarga: **4 bytes desde `int`, 2 desde `short`, 8 desde `long`**. Observación del docente: `int` da hasta ~2 mil millones y permite negativos, un disparate para un largo; con `short` alcanza.
- **Cliente y servidor tienen que acordar la misma representación**, porque la de números es ambigua (complemento a uno, a dos, endianness) y la de texto también.
- **El error del encoding, con demo en vivo**: ASCII usa **1 byte por carácter siempre**; **UTF-8 usa 1 a 4** (la eñe son **2 bytes**, un emoji 3 o 4). Al mandar como `Largo` la cantidad de **caracteres** en vez de **bytes**, se pierde el último byte. Sumarle `+1` a ojo **no es solución**. La regla:

> "Siempre que mando un dato, el largo no es el largo de la string, el largo es el largo de la cantidad de bytes que voy a enviar."

### `Receive` parcial: el patrón obligatorio (clase del 08-09)
- **`Send` con TCP y la configuración por defecto en general manda todo. `Receive` NO garantiza llenar el buffer.**
- `Receive` es **bloqueante**: espera **hasta recibir al menos un byte**, entonces en operación normal **nunca devuelve 0**; **`Receive` == 0 significa que la conexión se cerró**.
- Por eso la responsabilidad de armar el mensaje completo **es del programador**: `while` sobre `Receive(buffer, offset, size - recibido, SocketFlags.None)` acumulando el offset. Ejemplo de clase: se esperan 11 bytes, llegan 5, se vuelve a llamar con offset 5 y size 6, y así.
- **No poner `Sleep` dentro de ese loop**: `Receive` ya espera, y esperar de más es mala práctica que no aporta nada. Y **`while`, no `if`**: puede llegar en más de dos tandas.
- El socket es **bidireccional**, con dos canales; se puede mandar y recibir a la vez y no se recibe lo propio. El orden y las retransmisiones los resuelve TCP; si falla de verdad, **el socket tira excepción**, no llega un truncado silencioso.
- `SocketFlags` es un vestigio de que `Socket` es viejo y se usó con muchos protocolos; hay una flag de **broadcast**, que soporta UDP y no TCP.

### Eficiencia y configuración
- **No mandar paquetes muy chiquitos**: es ineficiente en ancho de banda. Conviene acumular todos los campos de una operación y mandarlos juntos.
- **La IP y el puerto no se cablean**: van en archivo de configuración o variables de entorno. Es requerimiento de la letra y hace falta en la defensa, porque las IP del laboratorio son DHCP y cambian.
- **El cierre ordenado del servidor**, que esta nota marcaba como ejercicio pendiente, **es requerimiento explícito del Obligatorio 1**: un comando en la consola del servidor que finalice las conexiones activas avisando a los clientes. El patrón que pidieron: bandera revisada por los hilos, hilos foreground, `Join`, nunca `Abort`.

## Connections
- Actas de clase donde se dictó: [[Clase 2026-09-01 - Exclusion mutua en CSharp y arranque de sockets]] · [[Clase 2026-09-03 - Minimizar bloqueos y sockets en detalle]] · [[Clase 2026-09-08 - Protocolo propietario, framing y Receive parcial]]
- Aplicación directa: [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]] · [[Reglas y prohibiciones del curso PDR 2026]]
- Necesita [[Tema 1 - Programacion multihilo]] (un hilo por cliente, exclusión mutua sobre estructuras compartidas).
- Se supera con `async` en [[Tema 3 - Asincronia y paralelismo]].
- Es la línea base contra la que se comparan [[Tema 4 - Colas de mensajes y MOM]], [[Tema 5 - RPC y gRPC]] y [[Tema 6 - APIs REST y WebSockets]]. Criterio de elección: [[Elegir tecnologia de comunicacion]].
- Formato de examen: [[Patrones de examen en Programacion de Redes]] · [[Errores caros en Programacion de Redes]].
- Reglas de estilo deducidas del codigo de catedra: [[Como escribir sockets al estilo del curso PDR]].
