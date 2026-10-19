Introducción a Sockets
En esta guía vas a aprender a:
1. Conectar un cliente y un servidor utilizando Sockets
2. Conectar varios clientes con un mismo servidor
3. Enviar un mensaje del cliente hacía el servidor
Conceptos previos
Sockets:
• Son una abstracción que permiten a las aplicaciones comunicarse entre ellas utilizando cualquier protocolo de red
(TCP/IP, UDP, Bluetooth, etc. )
• Son la base para el resto de mecanismos de comunicación a nivel de aplicación (vamos a dar algunos más
adelante)
• Son un mecanismo multiplataforma. Ej. Puedo usarlos para comunicar una aplicación hecha de C# en Windows con
una aplicación de Java en Mac, siempre y cuando utilicen el mismo protocolo.
• Cada socket precisa una IP y un puerto al que nos vamos a conectar y el protocolo de red (TCP, UDP, etc.). Si vamos
a comunicar dos procesos vamos a precisar un Socket en cada extremo.
Nota
Para los ejemplos en esta guía vamos a usar Sockets con el protocolo de red TCP y vamos a usar la clase de .NET Socket
1. Conectar un cliente con un servidor
Para este primer ejemplo vamos a crear dos proyecto de tipo ConsoleApp: Cliente y Servidor
Vamos a conectar un único cliente con un servidor
Servidor
Para el servidor vamos a precisar:
1. Crear un Socket
2. Crear un Endpoint: Un endpoint es formado por una IP y un puerto, lo vamos a usar en el siguiente paso
3. Asociar el socket con el endpoint
4. Hacer que el socket escuche por pedidos de conexión en la IP y puerto del endpoint especi�cado
5. Hacer que el socket acepte la primera conexión que le llegue

El código quedaría de la siguiente manera:
Servidor
using System.Net;
using System.Net.Sockets;
Console.WriteLine("Starting Server Application..");
var socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); ∕∕
var localEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"),20000); ∕∕Puerto va entre 0 y 65535
socketServer.Bind(localEndpoint);
socketServer.Listen(1); ∕∕ Nuestro Socket pasa a estar en modo escucha
var socketClient = socketServer.Accept(); ∕∕
Console.WriteLine("Acepte un nuevo pedido de conexion");
Console.ReadLine();
1 Especi�camos que vamos a usar TCP
2 Para la IP vamos a elegir localhost, también se puede usar la IP de su máquina si la conocen. Para el puerto vamos
a elegir cualquier puerto libre de nuestra máquina (usar comando netstat -an en su consola para ver puertos
actualmente en uso), se recomiendan usar puertos, por ejemplo, mayores a 5000 (los puertos menos a 1024 están
reservados para aplicaciones especi�cas)
3 Bloquea el Main (pasamos a WAIT/SLEEP/JOIN) hasta que llegue un pedido de conexión de un cliente
Hasta ahora tenemos un servidor que va a escuchar en el puerto 20000 por pedidos de conexión de un cliente y cuando
llegue lo va a aceptar. Como todavía no tenemos ningún cliente, el servidor se va a quedar bloqueado esperando aceptar
alguna conexión.
Cliente
Para el cliente vamos a precisar:
1. Crear un Socket
2. Conectarse al servidor
3. Mandar un mensaje al servidor
4. Cerrar el Socket
Importante
Un Socket es una manera de conectar una maquina local con una maquina remota. Hacer el socket.Bind le dice al socket
que IP y puerto usar en la maquina local. Si queremos iniciar una conexión a una maquina remota, tenemos que conocer la
IP y puerto de esa maquina.
Por lo tanto, en el cliente vamos a tener un endpoint local con la ip y puerto del cliente, y otro endpoint remoto con la ip y
puerto del servidor.
El código sería el siguiente:

Cliente
using System.Net;
using System.Net.Sockets;
Console.WriteLine("Starting Client Application..");
var socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
var localEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 15000); ∕∕
var remoteEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000);
socketClient.Bind(localEndpoint);
socketClient.Connect(remoteEndpoint); ∕∕ Nos conectamos con el servidor
Console.WriteLine("Connected to server!!!!");
Console.ReadLine();
Console.WriteLine("Will Close Connection...");
socketClient.Shutdown(SocketShutdown.Both); ∕∕ Indica en que sentido quiero cerrar la conexion
socketClient.Close(); ∕∕
1 El puerto del cliente puede ser cualquiera que este libre en la máquina del cliente, independientemente del puerto
del servidor. En este caso como ambas aplicaciones corren en la misma maquina, si el servidor ya usa el puerto
20000, el cliente tiene que usar otro que no este ocupado.
2 SocketShutdown.Both Indica que no quiero ni recibir ni enviar más datos.
3 Primero hacemos el Shutdown para asegurarnos que los datos restantes se terminan de enviar, una vez que
terminen el Close cierra y libera todos los recursos asociados al Socket.
Nota
Si usan la IP localhost ("127.0.0.1") en el servidor, la IP del cliente también tiene que ser "127.0.0.1". Si usan la IP de la red
local, el cliente también tiene que usar la IP de la red local.
2. Conectar varios clientes a un servidor
Actualmente una vez que el servidor acepta el primer cliente, ya no acepta ninguno más. Ahora vamos a cambiar
nuestro código para que el servidor se puede conectar a varios clientes a la vez.
Cliente
En el código del cliente vamos a agregar unos ConsoleWriteLine y vamos a hacer un pequeño cambio en el puerto del
endpoint local.
Cliente
using System.Net;
using System.Net.Sockets;
Console.WriteLine("Starting Client Application..");
var socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
var localEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0);
var remoteEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000);
socketClient.Bind(localEndpoint);
Console.WriteLine("Starting client...");
Console.WriteLine("Connecting to server...");

socketClient.Connect(remoteEndpoint);
Console.WriteLine("Connected to server!!!!");
Console.WriteLine("Press enter to exit");
Console.ReadLine();
Console.WriteLine("Will Close Connection...");
socketClient.Shutdown(SocketShutdown.Both);
socketClient.Close();
El puerto 0 es un puerto especial. Quiere decir "cualquier puerto que este disponible en la maquina". Por lo tanto, en vez
de nosotros �jarnos que puertos tenemos disponibles, el sistema dinámicamente asigna un puerto disponible.
Esto es muy útil cuando queremos tener varios clientes en una misma máquina (con una misma IP), ya que precisamos
que cada vez que se crea un cliente se le asigne un puerto distinto y así el servidor sabe a cual de los clientes se esta
comunicando.
¿Por qué no usamos puerto 0 en el servidor?
No es tan útil usar el puerto 0 en el servidor porque no sabríamos que puerto se asignaría al momento de ejecutar el
programa. Por lo tanto no sabríamos a que puerto se tienen que conectar los clientes.
Servidor
Ahora vamos a modi�car el servidor para aceptar varios clientes:
1. Una vez que se aceptó un cliente, volver a esperar a uno nuevo.
2. Por cada cliente crear un nuevo thread que se encargue de él. Evita que se bloquee todo el servidor si se bloquea la
comunicación con un cliente en particular.
El nuevo código del servidor quedaría así:
Servidor
using System.Net;
using System.Net.Sockets;
Console.WriteLine("Starting Server Application..");
var socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); ∕∕
var localEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000); ∕∕Puerto va entre 0 y 65535
socketServer.Bind(localEndpoint);
socketServer.Listen(100); ∕∕ Nuestro Socket pasa a estar en modo escucha
Console.WriteLine("Waiting for clients...");
while (true)
{
Socket clientSocket = socketServer.Accept(); ∕∕ El accept es bloqueante, espera hasta que llega
una nueva conexión
Console.WriteLine("Client connected");
new Thread(() => HandleClient(clientSocket)).Start(); ∕∕ Lanzamos un nuevo hilo para manejar al
nuevo cliente
}
static void HandleClient(Socket clientSocket)
{
bool clientIsConnected = true;

while (clientIsConnected)
{
Thread.Sleep(1000); ∕∕ Comunciación con el cliente
}
Console.WriteLine("Client disconnected");
}
La función HandleClient por ahora no hace nada, en la siguiente parte vamos a ver como recibir mensajes de los
clientes.
Cuidado
Este código de ejemplo tiene dos while(true) uno en el main thread y otro en HandleClient. Hacer while(true) es
mala práctica y se tendría que evitar.
Una alternativa es agregar un contador de máximas conexiones y una vez que se llegue al límite salir del while. Sin
embargo, esto no es tan útil porque normalmente no queremos limitar la cantidad de clientes que puede haber
conectados.
La otra opción es utilizar variables booleanas y cuando queramos cerrar el servidor o la conexión con algun cliente en
particular, cambiarlas a false y asegurarse de salir del while. Cuidado con las operaciones bloqueantes como
.Accept().
Socket.Listen
En el servidor tenemos la siguiente línea:
Servidor
7 socketServer.Listen(100); ∕∕ Nuestro Socket pasa a estar en modo escucha
El parámetro de Socket.Listen no es cuantos clientes puede aceptar el servidor. Es cuantos clientes pueden estar en
"espera" sin que el servidor haga socker.Accept(). En resumen, es una cola donde se guardan los clientes hasta que el
servidor puede procesarlos y haga el .Accept().
El siguiente ejemplo tira error (en el cuarto cliente) si intentamos conectar más de 3 clientes antes que se ejecute el
primer Accept. Esto sucede porque al no ejecutar el .Accept los clientes quedan encolados y una vez que se llegue al
limite, no se permiten más conexiones hasta que no se acepten las ya existentes.
Servidor
∕∕ ...
socketServer.Listen(3); ∕∕ Nuestro Socket pasa a estar en modo escucha, con un backlog de 3
Console.WriteLine("Waiting for clients...");
Thread.Sleep(30000) ∕∕ Esperamos 30s antes de aceptar el primer cliente
while (true)
{
Socket clientSocket = socketServer.Accept(); ∕∕ El accept es bloqueante, espera hasta que llega
una nueva conexión
Console.WriteLine("Client connected");

new Thread(() => HandleClient(clientSocket)).Start(); ∕∕ Lanzamos un nuevo hilo para manejar al
nuevo cliente
}
1 Los clientes quedan en al cola de espera(backlog) a ser aceptados, al aceptar un cliente sale de la cola y queda un
lugar disponible para que un nuevo cliente espere a ser aceptado
Documentación de Socket.Listen
Enviar mensajes del cliente al servidor
Por ahora nuestros clientes se conectan al servidor pero no realizan nada más. Vamos a agregar la posibilidad a los
clientes que envíen mensajes al servidor.
A tener en cuenta:
• Los datos que queremos enviar probablemente sean strings, objetos, ints etc.
• Los Sockets solo pueden enviar 0s y 1s (bytes)
• Hay que convertir de, por ejemplo, string a bytes para poder enviar información.
• El que recibe los datos (ej. servidor) tiene que convertir de bytes a string
• Hay distintos encoders que convierten de de string a bytes y viceversa. Uno muy común es UTF8.
Cliente
Primero vamos a hacer que el cliente lea un mensaje del usuario y lo envíe al servidor.
Hay que agregar las siguientes líneas:
Cliente

using System.Net;
using System.Net.Sockets;
using System.Text;
Console.WriteLine("Starting Client Application..");
var socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
var localEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0);
var remoteEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000);
socketClient.Bind(localEndpoint);
Console.WriteLine("Starting client...");
Console.WriteLine("Connecting to server...");
socketClient.Connect(remoteEndpoint);
Console.WriteLine("Connected to server!!!!");
Console.WriteLine("Type a message and press enter to send it");
while (true) {
string message = Console.ReadLine();
byte[] data = Encoding.UTF8.GetBytes(message); ∕∕ Convierte de string a una array de bytes
socketClient.Send(data);
}
Console.WriteLine("Will Close Connection...");
socketClient.Shutdown(SocketShutdown.Both);
socketClient.Close();
Si ejecutamos el programa, vemos que nos permite escribir mensajes del lado del cliente pero todavía el servidor no los
recibe ni procesa.
Servidor
Ahora en el servidor tenemos que esperar por los datos que nos envían los clientes, convertirlos de bytes a string y
mostrarlos en pantalla.
Vamos cambiar la función ClientHandler:
Servidor
static void HandleClient(Socket clientSocket)
{
bool clientIsConnected = true;
while (clientIsConnected)
{
byte[] data = new byte[256]; ∕∕ Array de bytes donde vamos a guardar los datos recibidos
clientSocket.Receive(data); ∕∕ Se bloquea hasta recibir datos del cliente
string message = $"Client says {Encoding.UTF8.GetString(data)}"; ∕∕ Convertimos de bytes a
string
Console.WriteLine(message);
}
Console.WriteLine("Client disconnected");
}
Ahora si probamos nuestra aplicación vemos que podemos enviar datos de varios clientes a la vez y el servidor los va a
mostrar por pantalla.
Mejoras

Ya tenemos una comunicación entre varios clientes y un servidor básica.
Hay algunos puntos que pueden mejorar:
• Si el cliente escribe "exit", cerrar el cliente
• Si el cliente se cerró, terminar la función de ClientHandler
• Ahora si el cliente se conecta pero se cierra antes de enviar datos al servidor, el .Receive(data) tira una
SocketException, evitar que rompa el programa.
Recomendamos intentar solucionar estos problemas antes de seguir con la solución.
Tip: socket.Receive(data) devuelve un int que representa la cantidad de bytes recibidos. Si la cantidad es 0, quiere
decir que la conexión fue cerrada. Esto no funciona si el cliente nunca envió datos antes, ver lo explicado arriba sobre la
excepción.
Solución:
Primero vamos a hacer que se cierre el cliente cuando se escribe "exit".
Modi�camos el �nal de nuestro proyecto Cliente:
Cliente
∕∕∕ ...
bool clientRunning = true;
while (clientRunning) {
string message = Console.ReadLine();
if (message.Equals("exit"))
{
clientRunning = false;
}
else
{
byte[] data = Encoding.UTF8.GetBytes(message); ∕∕ Convierte de string a una array de bytes
socketClient.Send(data);
}
}
Console.WriteLine("Will Close Connection...");
socketClient.Shutdown(SocketShutdown.Both);
socketClient.Close();
Si lo probamos, vemos que el cliente funciona pero el servidor anda mal.
Vamos agregar dos mejoras al servidor:
• try∕catch para detecta SocketException
• Veri�car si la data recibida es de largo 0, en tal caso, terminar el hilo con ese cliente.
Primero el try/catch:
Servidor

| static void | HandleClient(Socket | clientSocket) |
| ----------- | ------------------- | ------------- |
{
| bool  | clientIsConnected = | true; |
| ----- | ------------------- | ----- |
| while | (clientIsConnected) |       |
{
byte[] data = new byte[256]; ∕∕ Array de bytes donde vamos a guardar los datos recibidos
try {
clientSocket.Receive(data);
string message = $"Client says {Encoding.UTF8.GetString(data)}"; ∕∕ Convertimos de bytes
a string
Console.WriteLine(message);
}
| catch | (SocketException | e)  |
| ----- | ---------------- | --- |
{
|     | clientIsConnected | = false; |
| --- | ----------------- | -------- |
}
}
Console.WriteLine("Client disconnected");
}
Después chequeamos si el largo de los datos recibidos es 0:
Servidor
| static void | HandleClient(Socket | clientSocket) |
| ----------- | ------------------- | ------------- |
{
| bool  | clientIsConnected = | true; |
| ----- | ------------------- | ----- |
| while | (clientIsConnected) |       |
{
byte[] data = new byte[256]; ∕∕ Array de bytes donde vamos a guardar los datos recibidos
try
{
|     | int received = clientSocket.Receive(data); |     |
| --- | ------------------------------------------ | --- |
|     | if (received == 0)                         |     |
{
|     | clientIsConnected | = false; |
| --- | ----------------- | -------- |
}
else
{
string message = $"Client says {Encoding.UTF8.GetString(data)}"; ∕∕ Convertimos de
bytes a string
Console.WriteLine(message);
}
}
| catch | (SocketException | e)  |
| ----- | ---------------- | --- |
{
|     | clientIsConnected | = false; |
| --- | ----------------- | -------- |
}
}
Console.WriteLine("Client disconnected");
}
Si probamos el código ahora, vemos como podemos enviar datos al servidor de varios clientes y además podemos
cerrar los clientes sin problemas.

Código �nal
El código �nal para el cliente y servidor serían:
Cliente
using System.Net;
using System.Net.Sockets;
using System.Text;
Console.WriteLine("Starting Client Application..");
var socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
var localEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 0);
var remoteEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000);
socketClient.Bind(localEndpoint);
Console.WriteLine("Starting client...");
Console.WriteLine("Connecting to server...");
socketClient.Connect(remoteEndpoint);
Console.WriteLine("Connected to server!!!!");
Console.WriteLine("Type a message and press enter to send it");
bool clientRunning = true;
while (clientRunning) {
string message = Console.ReadLine();
if (message.Equals("exit"))
{
clientRunning = false;
}
else {
byte[] data = Encoding.UTF8.GetBytes(message); ∕∕ Convierte de string a una array de bytes
socketClient.Send(data);
}
}
Console.WriteLine("Will Close Connection...");
socketClient.Shutdown(SocketShutdown.Both);
socketClient.Close();
Servidor
using System.Net;
using System.Net.Sockets;
using System.Text;
Console.WriteLine("Starting Server Application..");
var socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); ∕∕
var localEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 20000); ∕∕Puerto va entre 0 y 65535
socketServer.Bind(localEndpoint);
socketServer.Listen(3); ∕∕ Nuestro Socket pasa a estar en modo escucha
Console.WriteLine("Waiting for clients...");
while (true)
{
Socket clientSocket = socketServer.Accept(); ∕∕ El accept es bloqueante, espera hasta que llega
una nueva conexión
Console.WriteLine("Client connected");
new Thread(() => HandleClient(clientSocket)).Start(); ∕∕ Lanzamos un nuevo hilo para manejar al
nuevo cliente*∕

}
| static void | HandleClient(Socket | clientSocket) |
| ----------- | ------------------- | ------------- |
{
| bool  | clientIsConnected = | true; |
| ----- | ------------------- | ----- |
| while | (clientIsConnected) |       |
{
byte[] data = new byte[256]; ∕∕ Array de bytes donde vamos a guardar los datos recibidos
try
{
|     | int received = clientSocket.Receive(data); |     |
| --- | ------------------------------------------ | --- |
|     | if (received == 0)                         |     |
{
|     | clientIsConnected | = false; |
| --- | ----------------- | -------- |
}
else
{
string message = $"Client says {Encoding.UTF8.GetString(data)}"; ∕∕ Convertimos de
bytes a string
Console.WriteLine(message);
}
}
| catch | (SocketException | e)  |
| ----- | ---------------- | --- |
{
|     | clientIsConnected | = false; |
| --- | ----------------- | -------- |
}
}
Console.WriteLine("Client disconnected");
}
En esta guía aprendiste:
• Utilizar Sockets
• Conectar un cliente con un servidor
• Conectar varios clientes a un servidor
• Enviar mensajes desde clientes al servidor
En las siguiente guía vamos a ver una manera más prolija y extensible de como enviar datos de�niendo nuestro propio
protocolo.