| Ingeniería | de Sistemas. | Programación |         | de Redes. |
| ---------- | ------------ | ------------ | ------- | --------- |
| Examen     | Julio 2023.  | 2 horas.     |         |           |
| 1)         | (5 pts) Dado | el siguiente | método: |           |
Se disparan dos hilos que deben acceder al método imprimir(String texto_a_imprimir). Dicho
método (imprimir) solamente muestra en pantalla lo que recibe, pero debe mostrarlo en
| pantalla | bajo mutua | exclusión. |     |     |
| -------- | ---------- | ---------- | --- | --- |
WorkerThreadMethod1 es un método que imprime (enviando a imprimir) “Hello world from
Worker 1” y WorkerThreadMethod2 imprime “Hello world from Worker 2”.
Se pide, programar los métodos WorkerThreadMethod1, WorkerThreadMethod2 e imprimir,
| utilizando | monitores | para garantizar | la  | mutua exclusión. |
| ---------- | --------- | --------------- | --- | ---------------- |
Solución:

| Ingeniería | de Sistemas. | Programación | de Redes. |     |
| ---------- | ------------ | ------------ | --------- | --- |
| Examen     | Julio 2023.  | 2 horas.     |           |     |
2) (5 pts) Para el anterior ejercicio, se descubre que el método imprimir ha
comenzado a fallar. Lo cual genera que uno de los threads no libere el monitor. Por
|     | lo que es necesario | gestionar dicha | situación ante | una excepción. |
| --- | ------------------- | --------------- | -------------- | -------------- |
Se pide, evitando la utilización de monitores, gestionar los hilos de forma que en
caso de que falle el imprimir, no se quede un hilo bloqueando el recurso.
Solución:

| Ingeniería | de Sistemas. | Programación | de Redes. |     |
| ---------- | ------------ | ------------ | --------- | --- |
| Examen     | Julio 2023.  | 2 horas.     |           |     |
3) (15 puntos)
Tomando como base las tecnologías vistas en el curso, y teniendo en cuenta las siguientes
premisas:
1. Se necesita construir una app que reciba datos de varios clientes a la vez.
2. Estos clientes envían muchos paquetes de tamaño pequeño y el servidor debe acumular
| una cantidad | X de los | mismos para luego | procesarlos | a la vez. |
| ------------ | -------- | ----------------- | ----------- | --------- |
3. Cada intervalo de Y tiempo, el servidor debe publicar estadísticas de cuál fue el resultado
del procesamiento en ese periodo de tiempo, en un canal de comunicaciones SEPARADO
| de los que | se establecen | con los clientes. |     |     |
| ---------- | ------------- | ----------------- | --- | --- |
Discuta cuáles son las mejores opciones para el caso descrito anteriormente, desde el
punto de vista de los canales de comunicación, así como también desde el punto de vista
| de la arquitectura | del | servidor. |     |     |
| ------------------ | --- | --------- | --- | --- |
Solución:
Para la comunicación con los clientes, conviene establecer una conexión que permita hacer
streaming de datos, ya que nos van a enviar permanentemente data. Debido a esto, un
canal basado en GRPC o sockets parece ser la mejor opción, para poder mantener la
conexión establecida. Además, se debe implementar un server que tenga un hilo o Task
para atender a cada cliente. Una vez que en ese hilo o task se acumulan X paquetes, se
pueden lanzar X hilos para procesar los mismos (teniendo en cuenta de no sobrepasar el
| número | máximo de hilos | para una app). |     |     |
| ------ | --------------- | -------------- | --- | --- |
Respecto al canal donde se publican las estadísticas, podría perfectamente implementarse
una comunicación basada en colas de mensaje, para poder enviar de forma asincrónica y
desacoplada estos mensajes sin interrumpir el procesamiento de cada cliente.

| Ingeniería | de Sistemas.  | Programación     |             | de Redes. |
| ---------- | ------------- | ---------------- | ----------- | --------- |
| Examen     | Julio 2023.   | 2 horas.         |             |           |
| 4) (10     | pts) Explique | en sus palabras: |             |           |
| 1. ¿Cuándo | es necesario  | utilizar         | asincronía? |           |
2. Es posible implementar una aplicación que sea asincrónica y paralela al mismo tiempo?
Solución:
1. Cuando nuestra aplicación va a ejecutar operaciones que pueden demorar en el tiempo y
| que van | a resolver | en un entorno | externo a | la misma. |
| ------- | ---------- | ------------- | --------- | --------- |
2. Si, ya que podemos querer procesar muchas cosas en paralelo, y cada uno de esos
procesamientos puede implicar la resolución de operaciones por fuera del scope de nuestra
app (lo cual nos puede obligar a esperar estos resultados), por lo que es perfectamente
| posible | combinar ambos | conceptos |     |     |
| ------- | -------------- | --------- | --- | --- |

| Ingeniería | de    | Sistemas. |     | Programación |     | de Redes. |     |     |
| ---------- | ----- | --------- | --- | ------------ | --- | --------- | --- | --- |
| Examen     | Julio | 2023.     | 2   | horas.       |     |           |     |     |
5) (10 Pts) Un servicio de streaming quiere que usted diseñe las URLs para una
| arquitectura   |           | REST.        |         |          |               |           |                 |     |
| -------------- | --------- | ------------ | ------- | -------- | ------------- | --------- | --------------- | --- |
| De momento,    |           | lo necesario |         | es:      |               |           |                 |     |
| 1. Operaciones |           | CRUD         | con     | usuarios | y             | películas |                 |     |
| 2. Filtrar     | a los     | usuarios     | por     | fecha    | de nacimiento |           |                 |     |
| 3. Filtrar     | películas | por          | género, | director |               | y / o año | de lanzamiento. |     |
4. Interesa también obtener un listado de qué películas vio cada usuario o su inversa,
| para una | película | qué      | usuarios | la     | vieron. |                |     |     |
| -------- | -------- | -------- | -------- | ------ | ------- | -------------- | --- | --- |
| Diseñe   | sólo     | las URLs | para     | cubrir | estos   | requerimientos |     |     |
Solución:
| Sección | usuarios |     |     |     |     |     |     |     |
| ------- | -------- | --- | --- | --- | --- | --- | --- | --- |
URL: api.streaming.com/users
| Verbos: | POST, | GET |     |     |     |     |     |     |
| ------- | ----- | --- | --- | --- | --- | --- | --- | --- |
URL: api.streaming.com/users/userName
| Verbos: | GET, | DELETE, | PUT |     |     |     |     |     |
| ------- | ---- | ------- | --- | --- | --- | --- | --- | --- |
URL: api.streaming.com/users?birthDate=2001-05-03
Verbo: GET
URL: api.straming.com/users/userName/movies
| Sección | Películas |     |     |     |     |     |     |     |
| ------- | --------- | --- | --- | --- | --- | --- | --- | --- |
URL: api.streaming.com/movies
| Verbos: | POST, | GET |     |     |     |     |     |     |
| ------- | ----- | --- | --- | --- | --- | --- | --- | --- |
URL: api.streaming.com/movies/movieId
| Verbos: | GET, | DELETE, | PUT |     |     |     |     |     |
| ------- | ---- | ------- | --- | --- | --- | --- | --- | --- |
URL: api.streaming.com/users?genre=Western&year=1960
Verbo: GET
URL: api.straming.com/movies/movieId/users
6) (10 pts) Mencione los tipos de Exchanges de RabbitMQ vistos en el curso y en que se
diferencian
Respuesta:
| Directo | - Routing | Key | = Binding | Key                |       |              |                |     |
| ------- | --------- | --- | --------- | ------------------ | ----- | ------------ | -------------- | --- |
| Topic - | Routing   | Key | hace      | match parcialmente |       |              | con el binding | key |
| Fanout  | - Propaga | los | mensajes  | a                  | todos | los bindings |                |     |
-----
| Header | - Permite | hacer | coincidir |     | con una | binding | key con | atributos |
| ------ | --------- | ----- | --------- | --- | ------- | ------- | ------- | --------- |

Ingeniería de Sistemas. Programación de Redes.
Examen Julio 2023. 2 horas.
Default - El Routing Key coincide con el nombre de la Queue.
7) (5 pts) Compare (nombre diferencias) entre Mutex,Monitor y Lock.
Solucion:
● lock es equivalente a utilizar monitores
○ Lock es una instrucción, mutex y monitores hacen referencia a clases.
● Monitores es mucho más rápido que Mutex
● Mutex es una primitiva de sincronización. Permite manejar bloqueos a nivel de
Sistema Operativo (a diferencia de monitores, donde el alcance del bloqueo
depende del objeto donde es instanciado).
● Alcance, mutex me permite sincronizar threads entre aplicaciones.
● Mutex > Monitor > Lock

| Ingeniería | de    | Sistemas. |          | Programación | de  | Redes. |     |
| ---------- | ----- | --------- | -------- | ------------ | --- | ------ | --- |
| Examen     | Julio | 2023.     | 2 horas. |              |     |        |     |
8) (25 pts) Implemente un servidor que pueda mantener N conexiones de distintos clientes
(se deben
| verificar      | desconexiones). |          |         |           |     |             |           |
| -------------- | --------------- | -------- | ------- | --------- | --- | ----------- | --------- |
| Dicho servidor |                 | atenderá | pedidos | entrantes |     | de conexión | y, cuando |
establezca una conexión, deberá comportarse de la siguiente manera:
a. Cuando un cliente se conecta, el servidor le asigna un id único y se lo envía al cliente
conectado.
b. Luego de esto, el cliente puede enviar una solicitud de envío de mensaje a otro cliente,
| enviando | el id | y el string | a   | enviar. |     |     |     |
| -------- | ----- | ----------- | --- | ------- | --- | --- | --- |
c. Cada vez que el mensaje sea entregado a su destinatario, el servidor debe enviar un
mensaje notificando al emisor de que el mensaje fue entregado correctamente.
NOTA: Se sugiere construir un protocolo sencillo para soportar los dos comandos
mencionadas
Solución:
struct Connection
{
| public | int id | { get; set;  | }   |        |        |     |     |
| ------ | ------ | ------------ | --- | ------ | ------ | --- | --- |
| public | Thread | clientThread |     | { get; | set; } |     |     |
| public | Socket | clientSocket |     | { get; | set; } |     |     |
}
class Server
{
| private | Socket    | serverSocket;    |       |                    |      |     |     |
| ------- | --------- | ---------------- | ----- | ------------------ | ---- | --- | --- |
| private | bool      | bListening       | =     | true;              |      |     |     |
| private | Thread    | listenThread;    |       |                    |      |     |     |
| private | ArrayList | clients          |       | = new ArrayList(); |      |     |     |
| private | int       | lastId =         | 0;    |                    |      |     |     |
| private | Object    | locker           | = new | Object();          |      |     |     |
| public  | const     | int SEND_MESSAGE |       |                    | = 1; |     |     |
| public  | Server()  |                  |       |                    |      |     |     |
{
| serverSocket       |     | =     | new Socket(AddressFamily.InterNetwork, |     |     |     |     |
| ------------------ | --- | ----- | -------------------------------------- | --- | --- | --- | --- |
| SocketType.Stream, |     |       | ProtocolType.Tcp);                     |     |     |     |     |
| listenThread       |     | = new | Thread(startListen);                   |     |     |     |     |
listenThread.Start();
}

| Ingeniería | de Sistemas.       | Programación |     |     | de Redes. |     |     |     |
| ---------- | ------------------ | ------------ | --- | --- | --------- | --- | --- | --- |
| Examen     | Julio 2023.        | 2 horas.     |     |     |           |     |     |     |
| private    | void startListen() |              |     |     |           |     |     |     |
{
serverSocket.Listen(10);
| while | (bListening) |     |     |     |     |     |     |     |
| ----- | ------------ | --- | --- | --- | --- | --- | --- | --- |
{
try
{
|     | Socket acceptedConnection |             |                       |                       | = serverSocket.Accept(); |     |     |     |
| --- | ------------------------- | ----------- | --------------------- | --------------------- | ------------------------ | --- | --- | --- |
|     | Connection                | conn        | = new                 | Connection();         |                          |     |     |     |
|     | conn.id                   | = lastId++; |                       |                       |                          |     |     |     |
|     | conn.clientSocket         |             | = acceptedConnection; |                       |                          |     |     |     |
|     | conn.clientThread         |             | = new                 | Thread(handleClient); |                          |     |     |     |
conn.clientThread.Start((Object)conn.id);
|     | Escuela | de Ingeniería | Universidad |     | ORT | Uruguay |     |     |
| --- | ------- | ------------- | ----------- | --- | --- | ------- | --- | --- |
clients.Add(conn);
}
|     | catch (Exception | e)  |     |     |     |     |     |     |
| --- | ---------------- | --- | --- | --- | --- | --- | --- | --- |
{
|     | Console.WriteLine("Error |     |     | accepting | connection..." |     | +   |     |
| --- | ------------------------ | --- | --- | --------- | -------------- | --- | --- | --- |
e.Message);
|     | bListening | = false; |     |     |     |     |     |     |
| --- | ---------- | -------- | --- | --- | --- | --- | --- | --- |
}
}
}
| private | void handleClient(Object |     |     | o)  |     |     |     |     |
| ------- | ------------------------ | --- | --- | --- | --- | --- | --- | --- |
{
| int                               | iLocalId = | (int)o; |     |     |          |          |          |         |
| --------------------------------- | ---------- | ------- | --- | --- | -------- | -------- | -------- | ------- |
| sendMessageToConnection(iLocalId, |            |         |     |     | "Your id | is: [" + | iLocalId | + "]"); |
| bool                              | finish =   | false;  |     |     |          |          |          |         |
| while                             | (!finish)  |         |     |     |          |          |          |         |
{
|     | int command | = receiveHeader(iLocalId); |     |     |     |     |     |     |
| --- | ----------- | -------------------------- | --- | --- | --- | --- | --- | --- |
}
}
| private | int receiveHeader(int |     | iLocalId) |     |     |     |     |     |
| ------- | --------------------- | --- | --------- | --- | --- | --- | --- | --- |
{
| foreach | (Connection | con | in clients) |     |     |     |     |     |
| ------- | ----------- | --- | ----------- | --- | --- | --- | --- | --- |
{
|     | if (con.id == | iLocalId) |     |     |     |     |     |     |
| --- | ------------- | --------- | --- | --- | --- | --- | --- | --- |
{
|     | byte[] header    | = new | byte[9]; |     |     |     |     |     |
| --- | ---------------- | ----- | -------- | --- | --- | --- | --- | --- |
|     | int iReceived    | = 0;  |          |     |     |     |     |     |
|     | while (iReceived | <     | 9)       |     |     |     |     |     |
{
|     | int iLocalReceived |                | =   | con.clientSocket.Receive(header, |     |     |     |     |
| --- | ------------------ | -------------- | --- | -------------------------------- | --- | --- | --- | --- |
|     | iReceived,         | 9 - iReceived, |     | SocketFlags.None);               |     |     |     |     |

Ingeniería de Sistemas. Programación de Redes.
Examen Julio 2023. 2 horas.
if (iLocalReceived == 0)
{
con.clientSocket.Shutdown(SocketShutdown.Both);
con.clientSocket.Close();
throw new Exception("Error processing data");
}
iReceived += iLocalReceived;
}
int command = Convert.ToInt32(Encoding.UTF8.GetString(header,
4, 4));
switch (command)
{
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
public void sendMessageToConnection(int id, String message)
{
lock (locker)
{
foreach (Connection con in clients)
{
if (con.id == id)
{
con.clientSocket.Send(Encoding.ASCII.GetBytes(message));
}
}
throw new Exception("could not find appropiate connection");
Escuela de Ingeniería Universidad ORT Uruguay
}
}
public void sendACK(Socket socket, String message)
{
lock (locker)
{
socket.Send(Encoding.ASCII.GetBytes(message));
}

| Ingeniería | de Sistemas. | Programación |     | de Redes. |     |
| ---------- | ------------ | ------------ | --- | --------- | --- |
| Examen     | Julio 2023.  | 2 horas.     |     |           |     |
}
}
9) (10 pts) Realice el archivo Proto para transmitir (dar de alta) un objeto persona que
| tenga como | atributos  | el nombre,  | la id y | el mail y teléfono(s). |              |
| ---------- | ---------- | ----------- | ------- | ---------------------- | ------------ |
| Tener en   | cuenta que | puede tener | más     | de un número           | de teléfono. |
También un método capaz de borrar un usuario (recibe una id) y devuelve un string.
Solución:
| service            | Admin {    |                         |     |     |     |
| ------------------ | ---------- | ----------------------- | --- | --- | --- |
| // Functions       | availables |                         |     |     |     |
| rpc Alta(UserDTO)  |            | returns (MessageReply); |     |     |     |
| rpc DeleteUser(Id) |            | returns (MessageReply); |     |     |     |
}
| message | UserDTO{        |         |     |     |     |
| ------- | --------------- | ------- | --- | --- | --- |
|         | int32 id = 1;   |         |     |     |     |
|         | string name     | = 2;    |     |     |     |
|         | string email    | = 3;    |     |     |     |
|         | repeated string | phone = | 4;  |     |     |
}
| message | Id {          |     |     |     |     |
| ------- | ------------- | --- | --- | --- | --- |
|         | int32 id = 1; |     |     |     |     |
}
| message | MessageReply   | {    |     |     |     |
| ------- | -------------- | ---- | --- | --- | --- |
|         | string message | = 1; |     |     |     |
}
| 10) (5 pts) | Diferencias | entre GRpc | y MOM |     |     |
| ----------- | ----------- | ---------- | ----- | --- | --- |

| Ingeniería | de Sistemas. | Programación | de Redes. |
| ---------- | ------------ | ------------ | --------- |
| Examen     | Julio 2023.  | 2 horas.     |           |
Solución: