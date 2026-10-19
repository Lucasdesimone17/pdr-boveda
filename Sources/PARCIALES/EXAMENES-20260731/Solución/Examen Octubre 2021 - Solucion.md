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

25-Octubre-2021

Ejercicio 1 (35 puntos)

Implemente  un  servidor  que  pueda  mantener  N  conexiones  de  distintos  clientes  (se  deben
verificar  desconexiones).  Dicho  servidor  atenderá  pedidos  entrantes  de  conexión  y,  cuando
establezca una conexión, deberá comportarse de la siguiente manera:

a.  Cuando un cliente se conecta, el servidor le asigna un id único y se lo envía al cliente

conectado.

b.  Luego de esto, el cliente puede enviar una solicitud de envío de mensaje a otro cliente,

enviando el id y el string a enviar.

c.  Cada vez que el mensaje sea entregado a su  destinatario, el servidor debe enviar un

mensaje notificando al emisor de que el mensaje fue entregado correctamente.

NOTA: Se sugiere construir un protocolo sencillo para soportar los dos comandos mencionadas

SOLUCION:

  struct Connection {
        public int id { get; set; }
        public Thread clientThread { get; set; }
        public Socket clientSocket { get; set; }
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

        }

        private void startListen() {
            serverSocket.Listen(10);
            while (bListening) {
                try {
                    Socket acceptedConnection = serverSocket.Accept();
                    Connection conn = new Connection();
                    conn.id = lastId++;
                    conn.clientSocket = acceptedConnection;
                    conn.clientThread = new Thread(handleClient);
                    conn.clientThread.Start((Object)conn.id);

Escuela de Ingeniería

Universidad ORT Uruguay

                    clients.Add(conn);
                }
                catch (Exception e) {
                    Console.WriteLine("Error accepting connection..." +
e.Message);
                    bListening = false;
                }
            }
        }

        private void handleClient(Object o) {
            int iLocalId = (int)o;
            sendMessageToConnection(iLocalId, "Your id is: [" + iLocalId + "]");
            bool finish = false;
            while (!finish) {
                int command = receiveHeader(iLocalId);

            }
        }

        private int receiveHeader(int iLocalId) {
            foreach (Connection con in clients) {
                if (con.id == iLocalId) {
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
                        case SEND_MESSAGE:
                            //1 - Recibir resto del mensaje

                            //2 - Separar id de conexion a enviar y String de
mensaje

                            //3 - usar funcion sendMessageToConnection

//4 – llamar a funcion sendACK, construyendo mensaje
usando fecha y hora, por ejemplo

                            break;
                    }
                }
            }
            throw new Exception("could not find appropiate connection");
        }

        public void sendMessageToConnection(int id, String message) {
            lock (locker) {
                foreach (Connection con in clients) {
                    if (con.id == id) {
                        con.clientSocket.Send(Encoding.ASCII.GetBytes(message));
                    }
                }
                throw new Exception("could not find appropiate connection");

Escuela de Ingeniería

Universidad ORT Uruguay

            }
        }
        public void sendACK(Socket socket, String message) {
            lock (locker) {
socket.Send(Encoding.ASCII.GetBytes(message));

            }
        }
    }

Escuela de Ingeniería

Universidad ORT Uruguay

Ejercicio 2 (30 puntos)

•  Defina cada uno de los siguientes conceptos:

1.  Monitores
2.  AutoResetEvent
3.  ManualResetEvent
4.  Proceso
5.  Thread
6.  Task

•  Explique  en  que  entorno  es  mas  conveniente  utilizar  locks  y  en  cual  es  preferible

utilizar ResetEvents (Auto y Manual)

SOLUCION

•  A continuacion, se responden los 6 primeros puntos:

a.  Monitor: Objeto que permite establecer una zona de mutuoexclusion (al igual
que el lock) y ademas permite el envio de señales entre hilos mediante Wait()
y Pulse() para establecer una sincronizacion mas especifica entre hilos.

b.  AutoResetEvent. Permite bloquear un hilo mediante Wait() hasta que otro hilo
haga  Set().  Cuando  se  desbloquea,  automaticamente  pone  la  variable  de
espera en False, por lo que el siguiente hilo en la cola de espera debe esperar
un nuevo Set() para desbloquear su Wait().

c.  ManualResetEvent. Permite bloquear un hilo mediante Wait() hasta que otro
hilo  haga  Set().  Cuando  se  desbloquea,  la  variable  de  espera  permanece
incambiada. Subsiguientes Waits no bloquearan hasta que se haga un reset()
del ManualResetEvent.

d.  Proceso:  Instancia  de  un  programa  en  ejecucion.  Comparte  solamente

procesador con otros procesos.

e.  Thread:  Funcion  que  ejecuta  de  forma  concurrente  dentro  de  un  proceso.
Puede compartir variables y recursos con otros hilos y con su proceso padre.
f.  Task: Unidad de trabajo para ser ejecutada. A diferencia de la clase Thread es
el  framework  el  encargado  del  manejo  de  hilos  y  lo  hace  a  través  de  una
máquina de estados. Tiene otras ventajas tales como que se puede obtener un
resultado, código simplifcado para leer a través del patrón async / await.
Locs se utilizan cuando queremos estar seguros que solamente un hilo ejecutara por
vez. Manual y AutoResetEvents los usamos para permitir enviar señales entre hilos y
asi  conntinuar  la  ejecucion  de  uno  u  otro  en  base  a  las  señales  enviadas.  El
autoresetevent  funciona  activandose  de  uno  en  uno,  mientras  que  el  manual  es
necesario solicitarle que deje de estar señalizado mediante una operación explicita.

•

Ejercicio 3 (10 puntos)

Explique  que  es  MOM  y  que  es  gRPC.  ¿Cuales  son  las  principales  diferencias  entre  uno  y  el
otro?  Explique  en  detalle.  ¿En  qué  casos  sería  propicio  interconectar  dos  sistemas  mediante
una tecnologia o la otra?

SOLUCION

Escuela de Ingeniería

Universidad ORT Uruguay

MOM provee un mecanismo de interconexion desacoplado y de envio de mensajes por lotes.
Encapsula objetos hasta cierto tamaño y los envia independiente de su formato.

gRPC involucra una tecnologia mutiplataforma que funciona en .NET (entre otros) que permite
exponer  funciones  hacia  otras  maquinas,  para  que  sean  consumidos  como  si  se  encontraran
locales a este equipo.

gRPC provee mayor performance vs mas facilidad de implementacion en MOM. gRPC es una
solucion que permite ser ejecutada, entre otros, en .NET, mientras que MOM depende de la
implementacion  que  estemos  usando,  pero  en  lineas  generales,  hay  implementaciones
multiplataforma de MOM. gRPC conviene usar mas cuando queremos performance (o cuando
queremos que el uso de los objetos sea transparente a la ubicación) y MOM lo usamos cuando
queremos enviar grandes cantidades de información sin que ambos extremos tengan que estar
conectados.

Ejercicio 4 (15 puntos)

class NotThreadSafe {

  static string name = “Pepe”;

    static void Run(string name, int number)

  {

      if (name != “Pepe”)

{

Console.WriteLine($”{name} was here”);

} else {

Console.WriteLine($”{name} was here”);

name = “Julio”;

Console.WriteLine($”{name} is now here”);

}

  }

}

¿Qué  errores  podría  presentar  el  código  visto  arriba  en  caso  de  ejecutarse  en  paralelo?
Explique en sus palabras y reescriba el mismo (manteniendo la funcionalidad) para solucionar
estos problemas.

SOLUCIÓN

Al ejecutarse concurremente el código puede ocurrir que al menos dos hilos entren en paralelo
al  bloque  de  código  de  la  asignacion  de  nombre,  cambie  el  valor  de  name  a  Julio  y  luego  se
imprima mas de una vez el “pepe was here” seguido de “Julio was here”.

Ejercicio 5 (10 puntos)

Un servicio de compra de articulos de computacion quiere que usted diseñe las URLs para una
arquitectura REST. De momento, lo necesario es:

Escuela de Ingeniería

Universidad ORT Uruguay

1.  Operaciones CRUD con usuarios e items
2.  Filtrar a los usuarios por fecha de nacimiento
3.  Filtrar items por tipo y marca
4.

Interesa también obtener un listado de que items adquirio cada usuario o su inversa,
dada un item que usuarios lo adquirieron.

Diseñe sólo las URLs para cubrir estos requerimientos

SOLUCIÓN

Sección usuarios

URL: api.pcitems.com/users

Verbos: POST, GET

URL: api.pcitems.com/users/userName

Verbos: GET, DELETE, PUT

URL: api.pcitems.com/users?birthDate=2001-05-03

Verbo: GET

URL: api.pcitems.com/users/userName/movies

Sección Películas

URL: api.pcitems.com/items

Verbos: POST, GET

URL: api.pcitems.com/items/itemid

Verbos: GET, DELETE, PUT

URL: api.pcitems.com/items?type=motherboard&brand=MSI

Verbo: GET

URL: api.pcitems.com/items/itemid/users

