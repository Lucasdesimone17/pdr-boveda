Escuela de Ingeniería

Universidad ORT Uruguay

Evaluación
Materia
Carrera
Condiciones

Grupo

Examen
Programación de Redes
Ingeniería de Sistemas, ID
1 a 100 Puntos, 2 horas desde que se lee la letra, sin material

No aplica

Fecha

14-Febrero-2022

Ejercicio 1 (40 puntos)

Implemente  un  servidor  que  pueda  mantener  N  conexiones  de  distintos  clientes  (se  deben
verificar  desconexiones).  Dicho  servidor  atenderá  pedidos  entrantes  de  conexión  y,  cuando
establezca una conexión, deberá comportarse de la siguiente manera:

a.  Cuando un cliente se conecta, el servidor le asigna un id único.
b.  Luego  de  esto,  el  servidor  envía  un  mensaje  de  keep  alive  al  cliente  con  un  formato

especificado por el alumno.

c.  Si luego de cierto tiempo el cliente no contesta, el servidor lo desconecta (el tiempo es

a criterio del alumno).

NOTA: Se sugiere construir un protocolo sencillo para soportar la comunicación mencionada

SOLUCION:

  struct Connection {
        public int id { get; set; }
        public Thread clientThread { get; set; }
        public Socket clientSocket { get; set; }
        public DateTime lastKeepAlive { get; set; }
    }

    class Server {
        private Socket serverSocket;
        private bool bListening = true;
        private Thread listenThread;
        private ArrayList clients = new ArrayList();
        private int lastId = 0;
        private Object locker = new Object();

        public const int SEND_MESSAGE = 1;

        public Server() {
            serverSocket = new Socket(AddressFamily.InterNetwork,
SocketType.Stream, ProtocolType.Tcp);
            listenThread = new Thread(startListen);
            listenThread.Start();
            Thread keepalive = new Thread(controlKeepAlive).Start();
        }

        private void controlKeepAlive() {
lock(locker) {

foreach (var element in clients)
{
    if (element.lastUpdated - DateTime.Now > 30)
    {
        // close connection and remove Element
    }
}

}

 }

Escuela de Ingeniería

Universidad ORT Uruguay

        private void startListen() {
            serverSocket.Listen(10);
            while (bListening) {
                try {
                    Socket acceptedConnection = serverSocket.Accept();
                    Connection conn = new Connection();
                    conn.id = lastId++;

conn.lastKeepAlive = DateTime.Now;

                    conn.clientSocket = acceptedConnection;
                    conn.clientThread = new Thread(handleKeepAliveClient);
                    conn.clientThread.Start((Object)conn);
                    clients.Add(conn);
                }
                catch (Exception e) {
                    Console.WriteLine("Error accepting connection..." +
e.Message);
                    bListening = false;
                }
            }
        }

        private void handleKeepAliveClient(Object o) {
            bool finish = false;
            while (!finish) {

sendKeepAlive((Connection)o.clientSocket);
int command = receiveHeader(iLocalId);

            }
        }

        private int receiveHeader(int iLocalId) {
                    byte[] header = new byte[9];
                    int iReceived = 0;
                    while (iReceived < 9) {
                        int iLocalReceived = con.clientSocket.Receive(header,
iReceived, 9 - iReceived, SocketFlags.None);
                        if (iLocalReceived == 0) {
                            con.clientSocket.Shutdown(SocketShutdown.Both);
                            con.clientSocket.Close();
                            throw new Exception("Error processing data");
                        }
                        iReceived += iLocalReceived;
                    }
                    int command = Convert.ToInt32(Encoding.UTF8.GetString(header,
4, 4));
                    switch (command) {
                        case KEEP_ALIVE:
                            //1 - Recibir resto del mensaje
                            //2 – Construye respuesta de KEEP ALIVE
                            //3 – reset time for this thread, locking conn array
                            break;
                    }
        }

        public void sendKeepAlive(Socket s) {

var keepAlive = "KEEP_ALIVE";
var data = Encoding.UTF8.GetBytes(keepAlive);
var dataSent = s.Send(data);

}

    }

Escuela de Ingeniería

Universidad ORT Uruguay

Ejercicio 2 (25 puntos)

•  Defina cada uno de los siguientes conceptos:

1.  Proceso
2.  Thread
3.  Task
4.  Asincronía
5.  Paralelismo

•  Explique la relación que existe entre Asincronía y Paralelismo

SOLUCION

1.  A Continuacion se escriben las respuestas:

a.  Proceso:  Instancia  de  un  programa  en  ejecucion.  Comparte  solamente

procesador con otros procesos.

b.  Thread:  Funcion  que  ejecuta  de  forma  concurrente  dentro  de  un  proceso.
Puede compartir variables y recursos con otros hilos y con su proceso padre.
c.  Task: Unidad de trabajo para ser ejecutada. A diferencia de la clase Thread es
el  framework  el  encargado  del  manejo  de  hilos  y  lo  hace  a  través  de  una
máquina de estados. Tiene otras ventajas tales como que se puede obtener un
resultado, código simplifcado para leer a través del patrón async / await.
d.  La programación asíncrona nos da la capacidad de “diferir” la ejecución de una
función  a  la  espera  de  que  se  complete  una  operación,  normalmente  de  I/O
(red,  disco  duro,  …),  y  así  evitar  bloquear  la  ejecución  hasta  que  se  haya
completado la tarea en cuestión.

e.  El paralelismo es una forma de computación en la cual varios cálculos pueden
realizarse  simultáneamente,1  basado  en  el  principio  de  dividir  los  problemas
grandes  para  obtener  varios  problemas  pequeños,  que  son  posteriormente
solucionados en paralelo. Hay varios tipos diferentes de paralelismo: nivel de
bit, nivel de instrucción, de datos y de tarea.

2.  Asíncrono permite pasar la ejecución de una función a otro hilo o thread, paralelismo
permite  ejecutar  o  correr  multiples  funciones  al  mismo  tiempo.  Cuando  se  usa  una
función  asíncrona  en  algún  punto  se  espera  la  respuesta  de  esa  función  para  hacer
algo, solo que no esperamos para ejecutar otras cosas, mientras que en paralelismo lo
que se hace es tener funciones que trabajan independientes y una no necesita de otra
para funcionar.

Ejercicio 3 (10 puntos)

Explique  que  es  gRPC  y  cuando  lo  usaría  en  un  entorno  de  aplicaciones  distribuidas.  ¿Qué
diferencia guarda con una comunicación basada en sockets y que ventajas y desventajas tiene
frente a esta?

SOLUCION

Escuela de Ingeniería

Universidad ORT Uruguay

gRPC involucra una tecnologia mutiplataforma que funciona en .NET (entre otros) que permite
exponer  funciones  hacia  otras  maquinas,  para  que  sean  consumidos  como  si  se  encontraran
locales a este equipo.

Sockets  provee  mayor  performance  vs  mas  facilidad  de  implementacion  en  gRPC.  Ambas
soluciones son sumamente orientadas a la perfomance, gRPC permite desarrollar en un nivel
de  abstraccion  mas  alto,  representar  objetos  de  forma  casi  directa  y  funcionar  de  4  formas
diferentes:  invocacion  unica-respuesta  unica,  invocacion  unica-respuesta  stream,  invocacion
stream-respuesta unica, y invocacion stream-respuesta stream. Sockets en cambio es un canal
abierto  que  permite  enviar  cualquier  cosa  por  el  canal  y  recibir  cualquier  tipo  de  elemento,
siempre y cuando sepamos cual es el formato de los mensajes que van por el canal. En gRPC, el
contrato que especifica cual es la data que viaja ya esta especificada en el archivo de Protocol
Buffer.

Ejercicio 4 (15 puntos)

class NotThreadSafe {

    static string varToCheck = “hola”;

    static string name = “pepe”;

static void Run() {

      if (varToCheck == “pepe”)

{

varToCheck = name;

Console.WriteLine($”{varToCheck} is  the value”);

} else {

Console.WriteLine($”{varToCheck} is the first value”);

varToCheck += name;

Console.WriteLine($”{varToCheck} is the second value”);

}

  }

}

¿Qué  errores  podría  presentar  el  código  visto  arriba  en  caso  de  ejecutarse  en  paralelo?
Explique en sus palabras y reescriba el mismo (manteniendo la funcionalidad) para solucionar
estos problemas.

SOLUCIÓN

Al  ejecutarse  concurremente  el  código  puede  ocurrir  que  al  menos  dos  hilos  entren  en  y
ejecuten  la  validacion  de  que  contiene  la  variable  name  al  mismo  tiempo  entonces  puede
darse el caso que dos hilos entren al else y imprima cosas incoherentes en los writelines de esa
porcion de codigo.

Escuela de Ingeniería

Universidad ORT Uruguay

Ejercicio 5 (10 puntos)

Un  servicio  consultas  de  reservas  para  un  consultorio  quiere  que  usted  diseñe  las  URLs  para
una arquitectura REST. De momento, lo necesario es:

1.  Operaciones CRUD con usuarios y reservas
2.  Filtrar a los usuarios por fecha de reserva
3.  Listado de reservas de un usuario.

Diseñe sólo las URLs para cubrir estos requerimientos

SOLUCIÓN

Sección Usuarios

URL: api.reservas.com/users

Verbos: POST, GET, PUT, DELETE

URL: api.reservas.com/users/{id}

Verbos: GET

URL: api.reservas.com/users/{id}/bookings

Verbos: GET

URL: api.reservas.com/users?user_id&bookingDate=2001-05-03

Sección Reservas

URL: api.reservas.com/bookings

Verbos: POST, GET

URL: api.reservas.com/bookings/{booking_id}

Verbos: GET, DELETE, PUT

URL: api.reservas.com/bookings/booking_id

