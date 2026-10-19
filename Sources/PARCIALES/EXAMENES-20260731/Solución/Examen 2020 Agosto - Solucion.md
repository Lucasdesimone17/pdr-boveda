Escuela de Ingeniería

Universidad ORT Uruguay

Evaluación  Examen

Grupo

No aplica

Fecha

21  -  Agosto  -
2020

Materia
Carrera
Condiciones  1 a 100 Puntos, 2 horas desde que se lee la letra, sin material

Programación de Redes
Ingeniería de Sistemas, ID

SOLUCIÓN

NOTA: Las respuestas que se brindan en este documento no tienen la completitud
esperada  en  un  examen.  Son  solamente  con  el  fin  de  orientar  sobre  las  posibles
respuestas a cada pregunta.

Ejercicio 1 (40 puntos)
Implemente cliente y servidor que pueda mantener hasta N conexiones de distintos clientes (se
deben  verificar  desconexiones).  Dicho  servidor  atenderá  pedidos  entrantes  de  conexión  y,
cuando establezca una conexión, deberá comportarse de la siguiente manera:

a.  Cuando un cliente se conecta, el servidor mediante un método que se encuentra en un
dll externa le asigna un string, que representa el string de conexión hacia una MSMQ.
Estre string se envia desde el server al cliente.

b.  Si la cantidad de clientes conectados es N y se quieren conectar otro cliente el mismo

debe ser desconectado.

Adicionalmente, se debe contemplar el siguiente comportamiento en el cliente:

a.  Cuando el cliente recibe el string de conexión desde el server, el cliente debe conectarse
a esa MSMQ y recibir mensajes de la misma. Los mensajes serán strings, los mismos
deberán ser impresos por pantalla.

b.  Si se recibe un mensaje que diga ^^EXIT^^,  el cliente debe dejar de escuchar mensajes

y desconectarse.

NOTA 1: Se sugiere construir un protocolo para llevar a cabo las operaciones.

NOTA  2:  Recordar  que  para  recibir  el  mensaje  correctamente  en  la  MSMQ,  hay  que
configurar un formatter de la siguiente forma:

var
=
XmlMessageFormatter(new [] {typeof(Movie)})}.

messageQueue

new

MessageQueue(queuePath)

{Formatter

=

new

SOLUCION:

1)  Server:

    class Program {
        static void Main(string[] args) {
            MTServer mts = new MTServer(15000);

Escuela de Ingeniería

Universidad ORT Uruguay

            mts.Start();
        }
    }
    public class Client {
        public bool KeepProcessing { get; set; }
        public Socket Socket { get; set; }
        public int id { get; set; }
    }

    public class MTServer {
        public int Port { get; set; }
        public Socket ServerSocket { get; set; }
        private List<Client> Clients = new List<Client>();
        private bool runServer = true;
        private int max_clients = 10;
        private int TIME_INTERVAL = 150;
        private Thread threadAccept;
        private Thread threadCheck;

        private static readonly Object locker = new object();

        public MTServer(int port) {
            Port = port;
            ServerSocket = new Socket(AddressFamily.InterNetwork,
SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start() {
            threadAccept = new Thread(new ThreadStart(StartServer));
            threadAccept.Start();
        }

        private void BindSocketAndListen() {
            ServerSocket.Bind(new IPEndPoint(IPAddress.Any, Port));
            ServerSocket.Listen(100);
        }

        private void StartServer() {

            BindSocketAndListen();
            int idLocal = 0;
            while (runServer) {
                Socket clientSocket = ServerSocket.Accept();
                if (Clients.Count == max_clients) {
                    //REJECT CONNECTION

continue;

                }
                Client cl = new Client();
                cl.KeepProcessing = true;
                cl.Socket = clientSocket;
                cl.id = idLocal++;
                lock(locker) {
                    Clients.Add(cl); //add to clients collection
                }
                ParameterizedThreadStart pts = new
ParameterizedThreadStart(ProcessClientThreadProc);
                Thread thr = new Thread(pts);
                thr.Start(cl); //start client processing thread
            }
        }
        private void ProcessClientThreadProc(object obj) {
            Client clnt = (Client)obj;

Escuela de Ingeniería

Universidad ORT Uruguay

            Socket clientSocket = clnt.Socket;
            byte[] buff = new byte[8]; //buffer to receive data from client
socket
            int bread = 0;
            byte[] buffToPrint;
            String dataToPrint;
            String msmqConnString = ExternalData.getMSMQConnectionString();

     String msgToSend = createMessageWithProtocol(msmqConnString);

            while (clnt.KeepProcessing) {

//do stuff

            }
            clientSocket.Close();
            Clients.Remove(clnt);
        }
}

2)  Client:

La parte mas relevante del cliente es la funcion que se queda recibiendo de una MSMQ.

        void receiveFromQueue(string queuePath) {
            bool bNotFinish = true;

            using (var messageQueue = new MessageQueue(queuePath) {Formatter =
new XmlMessageFormatter(new [] {typeof(Movie)})})
            {
                while (bNotFinish) {

                    Message message = messageQueue.receive();
                    if (messageIsNotExit(message)){
                        Console.WriteLine(message.Body);
                    } else {

                        socket.shutdown();
                        socket.close();
                        bNotFinish = false;
                    }

                }
            }
        }

El resto de la solucion no se incluye ya que puede ser tomada de otros examenes anteriores.

Ejercicio 2 (20 puntos)
¿Cuál es la diferencia entre paralelismo y asincronía? ¿Son conceptos excluyentes?
SOLUCION:
Asincronia: la asincronía se usa para presentar la impresión de tareas concurrentes o paralelas,
pero efectivamente, una llamada a un método asíncrono se usa normalmente para un proceso
que necesita trabajar fuera de la aplicación actual y no lo hacemos. No quiero esperar y bloquear
nuestra aplicación esperando la respuesta.
Paralelismo: El paralelismo involucra ejecutar varias tareas al mismo tiempo. Dicho de
otra forma, el paralelismo involucra hacer muchas cosas a la vez.
Asincronia no es excluyente de paralelismo, ya que muchas veces, ejecutamos tareas de
forma  paralela  pero  las  mismas  no  dependen  una  de  otras,  por  lo  que  podemos  evitar
sincronizar las mismas.

Escuela de Ingeniería

Universidad ORT Uruguay

Ejercicio 3 (10 puntos)
¿Cuáles son los estados que pueden tener un Thread?

SOLUCION: Ver el siguiente diagrama, cada nodo representa un estado:

Ejercicio 4 (15 puntos)
¿Qué mecanismos de mutúa exclusión conoce? ¿Cuál es la diferencia entre ellos?

SOLUCION:

1.  Monitor: Objeto que permite establecer una zona de mutuoexclusion (al igual que el
lock) y ademas permite el envio de señales entre hilos mediante Wait() y Pulse() para
establecer una sincronizacion mas especifica entre hilos.

2.  AutoResetEvent.  Permite  bloquear  un  hilo  mediante  Wait()  hasta  que  otro  hilo  haga
Set(). Cuando se desbloquea, automaticamente pone la variable de espera en False, por
lo  que  el  siguiente  hilo  en  la  cola  de  espera  debe  esperar  un  nuevo  Set()  para
desbloquear su Wait().

3.  ManualResetEvent. Permite bloquear un hilo mediante Wait() hasta que otro hilo haga
incambiada.
Set().  Cuando  se  desbloquea,
Subsiguientes Waits no bloquearan hasta que se haga un reset() del ManualResetEvent.
4.  Lock: Primitiva del framework que permite establecer una zona de mutuoexclusion para

la  variable  de  espera  permanece

sincronizar el acceso a una zona critica de codigo en una funcion.

5.  Semaforo: Objeto que permite sincronizar el acceso concurrente a un recurso o grupo
de recursos de forma simultanea. Permite llevar una cuenta de cuantos recursos estan
accediendo  simultaneamente  a  este  recurso  y  establecer  un  limite  maximo  de  los
procesos que pueden acceder simultaneamente al recurso mencionado.

Escuela de Ingeniería

Universidad ORT Uruguay

Ejercicio 5 (15 puntos)
¿Tiene sentido implementar en una API REST una estructura de herencia de objetos?
Digamos si yo tengo la clase animal, la cuál es abstracta, pero tenemos las clases perro y
gato que heredan de animal y represento esto mediante las siguientes rutas
api.zoo.com/animals
api.zoo.com/animals/dogs
api.zoo.com/animals/cats

SOLUCION: Si bien la idea puede ser considerada original por parte del alumno, es de
notar que REST no especifica ninguna forma clara de representar abstracciones basadas
en OOP, por lo que mas alla de si tiene sentido o no, la idea no es del todo correcta desde
un punto de vista tecnico.
Dicho esto, se puede representar como describe la API planteada, pero tambien puede ser
representada mediante tipificacion de sustantivos, mediante params. Por ejemplo:
api.zoo.com/animals?type=dog
api.zoo.com/ animals?type=cat

