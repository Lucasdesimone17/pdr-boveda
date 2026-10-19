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

30-Enero-2020

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

NOTA:  Se  sugiere  construir  un  protocolo  sencillo  para  soportar
mencionados

los  tres  comandos

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

Escuela de Ingeniería

Universidad ORT Uruguay

                    conn.clientThread = new Thread(handleClient);
                    conn.clientThread.Start((Object)conn.id);
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

Escuela de Ingeniería

Universidad ORT Uruguay

                }
                throw new Exception("could not find appropiate connection");
            }
        }
        public void sendACK(Socket socket, String message) {
            lock (locker) {
socket.Send(Encoding.ASCII.GetBytes(message));

            }
        }
    }

Ejercicio 2 (30 puntos)

•  Defina cada uno de los siguientes conceptos:

1.  Monitores
2.  AutoResetEvent
3.  ManualResetEvent
4.  Proceso
5.  Thread
6.  Task

•  Explique en que entorno es mas conveniente utilizar Semaforos y en cual es preferible

utilizar locks

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

•  Semaforos se utilizan cuando se requiere que multiples hilos accedan al mismo recurso
compartido, y este tiene una capacidad superior a 1 (ejemplo, una queue). Un lock, en
cambio, nos asegura que solo un hilo ejecutara

Escuela de Ingeniería

Universidad ORT Uruguay

Ejercicio 3 (10 puntos)

Explique que es MOM y que es Remoting. ¿Cuales son las principales diferencias entre uno y el
otro?  Explique  en  detalle.  ¿En  qué  casos  sería  propicio  interconectar  dos  sistemas  mediante
una tecnologia o la otra?

SOLUCION

MOM provee un mecanismo de interconexion desacoplado y de envio de mensajes por lotes.
Encapsula objetos hasta cierto tamaño y los envia independiente de su formato.

Remoting  involucra  una  tecnologia  propietaria  de  .NET  que  permite  exponer  objetos  hacia
otras maquinas, para que sean consumidos como si se encontraran locales a este equipo.

Remoting provee mayor performance vs mas facilidad de implementacion en MOM. Remoting
es una solucion propietaria de .NET, mientras que MOM depende de la implementacion que
estemos  usando,  pero  en  lineas  generales,  hay  implementaciones  multiplataforma  de  MOM.
Remoting conviene usar mas cuando queremos performance (o cuando queremos que el uso
de  los  objetos  sea  transparente  a  la  ubicación)  y  MOM  lo  usamos  cuando  queremos  enviar
grandes cantidades de información sin que ambos extremos tengan que estar conectados.

Ejercicio 4 (15 puntos)

class NotThreadSafe
{
  static int divider=2;

  static void Go(string name, int number)
  {
      if (divider != 0)

Console.WriteLine (number/divider); divider = 0;
Console.WriteLine($”{name} was here”);

{

}

  }
}

¿Qué  errores  podría  presentar  el  código  visto  arriba  en  caso  de  ejecutarse  en  paralelo?
Explique en sus palabras y reescriba el mismo (manteniendo la funcionalidad) para solucionar
estos problemas.

SOLUCIÓN

Al ejecutarse concurremente el código puede ocurrir que al menos dos hilos entren en paralelo
al bloque de código de la división por cero, uno haga la división, cambié el valor de divider a
cero  y  luego  uno  o  más  intenten  hacer  la  división  por  cero,  aquí  deberíamos  poner  un
mecanismo  de  mutua  exclusión  para  evitar  de  que  esta  parte  del  código  sea  accedida
concurrentemente, no así la línea siguiente donde se imprime a modo de log quién pasó por
ahí ya que esto no generaría problemas de concurrencia y deberíamos de saber que el código
envuelto en mecanismo que solucione el problema de mutúa debe ser lo más corto posible.

Escuela de Ingeniería

Universidad ORT Uruguay

Ejercicio 5 (10 puntos)

Un  servicio  de  streaming  quiere  que  usted  diseñe  las  URLs  para  una  arquitectura  REST.  De
momento, lo necesario es:

1.  Operaciones CRUD con usuarios y películas
2.  Filtrar a los usuarios por fecha de nacimiento
3.  Filtrar películas por género, director y / o año de lanzamiento.
4.

Interesa  también  obtener  un  listado  de  que  películas  vio  cada  usuario  o  su  inversa,
dada una película que usuarios la vieron.

Diseñe sólo las URLs para cubrir estos requerimientos

SOLUCIÓN

Sección usuarios

URL: api.streaming.com/users

Verbos: POST, GET

URL: api.streaming.com/users/userName

Verbos: GET, DELETE, PUT

URL: api.streaming.com/users?birthDate=2001-05-03

Verbo: GET

URL: api.straming.com/users/userName/movies

Sección Películas

URL: api.streaming.com/movies

Verbos: POST, GET

URL: api.streaming.com/movies/movieId

Verbos: GET, DELETE, PUT

URL: api.streaming.com/users?genre=Western&year=1960

Verbo: GET

URL: api.straming.com/movies/movieId/users

