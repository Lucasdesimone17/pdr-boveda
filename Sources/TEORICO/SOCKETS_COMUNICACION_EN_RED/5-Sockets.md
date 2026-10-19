Sockets

Problema

Proceso

???

Red

Proceso

¿Cómo se comunican dos procesos que están en máquinas distintas?

Solución

Deﬁnición de Socket

Son mecanismos de comunicación entre procesos que permiten
que un programa “hable” con otro programa incluso estando en
distintas máquinas.

Comunicarse = enviar y recibir datos

Es un canal de comunicación punto a punto.

Sockets en Sistemas Operativos

● Funcionan para cualquier protocolo de red, no solo TCP/IP
● Su implementación depende del Sistema Operativo, si bien son

similares difieren en algunos aspectos

● Ej:

○ Windows usa la librería Winsock32
○ Unix usa la librería socket

Clase de .NET

Socket

Implementación del SO

WinSocket32

Socket

socket

Windows

Unix-like

Endpoint

1. ¿Cómo identificar una
máquina remota?

Con su dirección IP

2. ¿Cómo identificar un proceso

dentro de la máquina?

Con un puerto

Dirección IP + número de puerto = Endpoint

¿Qué información preciso para comunicarme
usando Sockets?

1.
2.
3.
4.
5.

IP remota
Puerto remoto
IP local
Puerto local
Protocolo de red (Ejemplo TCP o UDP)

Tipos de Sockets - TCP vs UDP

● Orientados a la conexión

Permite conexión entre 2 procesos. Mensajes confiables.

Se usan stream sockets (protocolo TCP). Útiles para
transmisión de datos.

● No orientada a la conexión

Permite conexión entre múltiples procesos a la vez. Mensajes
no confiables.

Se utiliza datagram sockets (protocolo UDP). Útiles para
transmisión de información multimedia u operaciones de red no
convencionales.

Socket TCP

● Los Sockets TCP operan en una de dos modalidades

Aceptar conexiones.

Transmisión de datos.

● En .NET la clase Socket permite el manejo a bajo nivel de las conexiones sockets.

● Métodos para establecer la conexión

Servidor

- Bind(IPEndPoint local) – enlaza al socket a una dirección y puerto específicos.
- Listen(int c) – establece el socket en modalidad de aceptar conexiones.
- Accept() – deja al socket a la espera de una nueva conexión.

Socket TCP

Cliente

- Bind(IPEndPoint local) – enlaza al socket a una dirección y puerto

específicos. (Opcional para el cliente)

- Connect(IPEndPoint remote) – establece la conexión con el servidor

remoto.

Intercambio de datos

- Send(byte[] buffer) – escribe datos sobre la conexión.
-

int Receive(byte[] buffer) – lee datos desde la conexión.

Finalización de una conexión

- Shutdown(SocketShutdow.Both) – Deshabilita el envío y recepción en un

Socket.

- Close() - Cierre de la conexión y liberación de los recursos

IP

● Comando ipconﬁg  en Windows o ifconﬁg en Mac/Linux para ver mis

interfaces de red

● 127.0.0.1 o localhost: Sirve para conectar procesos que esten en la misma

maquina

● En Mac: Para encontrar su ip ﬁltrando usando grep, ejemplo:

○ ifconﬁg | grep inet
○ También pueden usar netstat -an para ver la ip que usan las conexiones ya existentes

Puerto

● Comando netstat -an  para ver información de las conexiones en mi

máquina, permite ver los puertos en uso

● Los puertos del 1 al 65535 son válidos
● Del 1 al 1024 están reservados para ciertas aplicaciones

○ Ej. 22 para SSH, 80 para http, 443 para https

● Generalmente nuestras aplicaciones usan puertos mayores a 1024
● El puerto 0 especiﬁca usar cualquier número de puerto disponible

Ejecutar varios proyectos a la vez (Visual Studio)

Recordar
correr el Server
y despues el
Cliente

Rider:

https://codeopinion.com/launching-mult
iple-projects-in-jetbrains-rider/

Sockets Ventajas y Desventajas

Ventajas

Desventajas

● Tecnología muy eficiente y
confiable para efectuar
comunicaciones entre procesos
● Permite llevar a cabo diversas

operaciones de red

● Nos da más control sobre el
canal de comunicación

● Multiplataforma y multilenguaje

● Solo resuelve la capa de

transporte de datos

● Tenemos que programar la

codificación y
decodificación de datos
● Dificultad para agregar
nuevas operaciones

Librerías de alto nivel para Sockets

Utilizan
Sockets por
debajo

● TcpListener
● TcpClient
● UdpClient

Fuente: TCP/IP Sockets in C# Practical Guide for Programmers

TCP

● TcpListener

○ Librería de alto nivel que permite administrar las conexiones TCP entrantes.
○ Resuelve los problemas de conectividad de bajo nivel.

● TcpClient

○ Librería de alto nivel que permite establecer una conexión TCP entre dos

IPEndPoint.

● NetworkStream

○ Clase que relaciona un Stream con un socket para facilitar la lectura y escritura

desde una una conexión socket establecida.

Diferencias entre Sockets y TCPClient/TCPListener

• Socket
•
•
•

.Bind(IPEndPoint local)

.Listen(int backlog)

.Accept() : Socket

•

•
•

•

•

.Connect(IpEndPoint remote)

.Shutdown()
.Close()

.Send(byte[]  buffer,  int  offset,  int  size,
SocketFlags)

.Receive(byte[]  buffer,
size, SocketFlags)

int  offset,

int

• TcpListener

• TcpListener(IPEndPoint local)

• .Start()

• .Stop()

• .AcceptTcpClient() : TcpClient

• TcpClient

• .Connect(IPEndPoint remote)

• .GetStream() : NetworkStream

• .Close()

• NetworkStream

• .Write(byte[] buffer, int offset, int size)

• .Read(byte[] buffer, int offset, int size)

Métodos asincrónicos

● Varias operaciones en .NET cuentan con su versión “Async”

● Devuelven Task o Task<...> lo cual permite realizar await

● Ej:  Accept() : Socket => AcceptAsync() : Task<Socket>

● Aplica para todos los métodos de I/O (Sockets, TcpListener,

TcpClient, escritura/lectura a disco etc. )

UdpClient

● UdpClient

○ Librería de alto nivel que permite efectuar el intercambio de datagramas

entre dos IPEndPoint.

● Métodos

○ UdpClient(IPEndPoint) – crea el cliente enlazando con el extremo local

especificado.

○ Send(byte[], int, IPEndPoint) – efectúa el envío del datagrama al extremo

remoto especificado.

○ Receive(IPEndPoint) : byte[] – queda a la espera de datagramas

provenientes del extremo remoto especificado.

Checkpoint!

● ¿Que hace el socket.Listen(int)?

● ¿Puedo tener un servidor usando Socket y un cliente usando TcpClient?

● ¿En qué caso uso Socket y en qué caso uso TcpClient?

● ¿Que limitaciones tiene usar la ip 127.0.0.1?

● ¿Por qué no enviamos los archivos de la misma manera que mandamos el resto

de datos?

