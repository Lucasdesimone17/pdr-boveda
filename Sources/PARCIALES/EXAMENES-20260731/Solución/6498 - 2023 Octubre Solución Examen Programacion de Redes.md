| Ingeniería     | de Sistemas. |     |     |     |
| -------------- | ------------ | --- | --- | --- |
| Programación   | de Redes.    |     |     |     |
| Examen Octubre | 2023.        |     |     |     |
2 horas.
| Ejercicio 1 | (30 pts) |     |     |     |
| ----------- | -------- | --- | --- | --- |
Implemente un servidor que pueda mantener N conexiones de distintos clientes (se deben
| verificar desconexiones). |          |                   |             |           |
| ------------------------- | -------- | ----------------- | ----------- | --------- |
| Dicho servidor            | atenderá | pedidos entrantes | de conexión | y, cuando |
establezca una conexión, deberá comportarse de la siguiente manera:
a. Cuando un cliente se conecta, el servidor le asigna un id único y se lo envía al cliente
conectado.
b. Luego de esto, el cliente puede enviar un mensaje al servidor con su id y la ciudad desde
| donde se está | conectando. |     |     |     |
| ------------- | ----------- | --- | --- | --- |
c. Cada vez que el mensaje sea entregado al servidor, este debe enviar un mensaje a todos
los clientes notificando que existe un nuevo cliente y la ciudad de donde se conecta.
NOTA: Se sugiere construir un protocolo sencillo para soportar los dos comandos
mencionados
Solución:
struct Connection
{
| public int    | id { get; set; | }      |        |     |
| ------------- | -------------- | ------ | ------ | --- |
| public Thread | clientThread   | { get; | set; } |     |
| public Socket | clientSocket   | { get; | set; } |     |
}
class Server
{
| private Socket    | serverSocket;    |                    |      |     |
| ----------------- | ---------------- | ------------------ | ---- | --- |
| private bool      | bListening       | = true;            |      |     |
| private Thread    | listenThread;    |                    |      |     |
| private ArrayList | clients          | = new ArrayList(); |      |     |
| private int       | lastId = 0;      |                    |      |     |
| private Object    | locker           | = new Object();    |      |     |
| public const      | int SEND_MESSAGE |                    | = 1; |     |
| public Server()   |                  |                    |      |     |
{
| serverSocket       | = new | Socket(AddressFamily.InterNetwork, |     |     |
| ------------------ | ----- | ---------------------------------- | --- | --- |
| SocketType.Stream, |       | ProtocolType.Tcp);                 |     |     |
| listenThread       | = new | Thread(startListen);               |     |     |
listenThread.Start();
}
| private void | startListen() |     |     |     |
| ------------ | ------------- | --- | --- | --- |

| Ingeniería   | de Sistemas. |        |     |     |     |     |
| ------------ | ------------ | ------ | --- | --- | --- | --- |
| Programación | de           | Redes. |     |     |     |     |
| Examen       | Octubre      | 2023.  |     |     |     |     |
2 horas.
{
serverSocket.Listen(10);
| while | (bListening) |     |     |     |     |     |
| ----- | ------------ | --- | --- | --- | --- | --- |
{
try
{
|     | Socket acceptedConnection |             |                             | = serverSocket.Accept(); |     |     |
| --- | ------------------------- | ----------- | --------------------------- | ------------------------ | --- | --- |
|     | Connection                | conn        | = new Connection();         |                          |     |     |
|     | conn.id                   | = lastId++; |                             |                          |     |     |
|     | conn.clientSocket         |             | = acceptedConnection;       |                          |     |     |
|     | conn.clientThread         |             | = new Thread(handleClient); |                          |     |     |
conn.clientThread.Start((Object)conn.id);
|     | Escuela | de Ingeniería | Universidad | ORT | Uruguay |     |
| --- | ------- | ------------- | ----------- | --- | ------- | --- |
clients.Add(conn);
}
|     | catch (Exception | e)  |     |     |     |     |
| --- | ---------------- | --- | --- | --- | --- | --- |
{
|     | Console.WriteLine("Error |     | accepting | connection..." | +   |     |
| --- | ------------------------ | --- | --------- | -------------- | --- | --- |
e.Message);
|     | bListening | = false; |     |     |     |     |
| --- | ---------- | -------- | --- | --- | --- | --- |
}
}
}
| private | void handleClient(Object |     | o)  |     |     |     |
| ------- | ------------------------ | --- | --- | --- | --- | --- |
{
| int                               | iLocalId = | (int)o; |     |          |                   |         |
| --------------------------------- | ---------- | ------- | --- | -------- | ----------------- | ------- |
| sendMessageToConnection(iLocalId, |            |         |     | "Your id | is: [" + iLocalId | + "]"); |
| bool                              | finish =   | false;  |     |          |                   |         |
| while                             | (!finish)  |         |     |          |                   |         |
{
|     | int command | = receiveHeader(iLocalId); |     |     |     |     |
| --- | ----------- | -------------------------- | --- | --- | --- | --- |
}
}
| private | int receiveHeader(int |     | iLocalId) |     |     |     |
| ------- | --------------------- | --- | --------- | --- | --- | --- |
{
| foreach | (Connection | con | in clients) |     |     |     |
| ------- | ----------- | --- | ----------- | --- | --- | --- |
{
|     | if (con.id == | iLocalId) |     |     |     |     |
| --- | ------------- | --------- | --- | --- | --- | --- |
{
|     | byte[] header    | = new | byte[9]; |     |     |     |
| --- | ---------------- | ----- | -------- | --- | --- | --- |
|     | int iReceived    | = 0;  |          |     |     |     |
|     | while (iReceived | <     | 9)       |     |     |     |
{
|     | int iLocalReceived |                | = con.clientSocket.Receive(header, |     |     |     |
| --- | ------------------ | -------------- | ---------------------------------- | --- | --- | --- |
|     | iReceived,         | 9 - iReceived, | SocketFlags.None);                 |     |     |     |

| Ingeniería     | de Sistemas. |           |     |     |     |     |     |     |
| -------------- | ------------ | --------- | --- | --- | --- | --- | --- | --- |
| Programación   |              | de Redes. |     |     |     |     |     |     |
| Examen Octubre |              | 2023.     |     |     |     |     |     |     |
2 horas.
|     | if (iLocalReceived |     |     | == 0) |     |     |     |     |
| --- | ------------------ | --- | --- | ----- | --- | --- | --- | --- |
{
con.clientSocket.Shutdown(SocketShutdown.Both);
con.clientSocket.Close();
|     | throw | new | Exception("Error |     |     | processing | data"); |     |
| --- | ----- | --- | ---------------- | --- | --- | ---------- | ------- | --- |
}
|     | iReceived |     | += iLocalReceived; |     |     |     |     |     |
| --- | --------- | --- | ------------------ | --- | --- | --- | --- | --- |
}
|     | int command |           | = Convert.ToInt32(Encoding.UTF8.GetString(header, |     |     |     |     |     |
| --- | ----------- | --------- | ------------------------------------------------- | --- | --- | --- | --- | --- |
|     | 4, 4));     |           |                                                   |     |     |     |     |     |
|     | switch      | (command) |                                                   |     |     |     |     |     |
{
case SEND_MESSAGE:
|     | //1 | - Recibir | resto | del mensaje    |     |          |          |     |
| --- | --- | --------- | ----- | -------------- | --- | -------- | -------- | --- |
|     | //2 | - Separar |       | id de conexion |     | a enviar | y String | de  |
mensaje
|     | //3    | - usar   | funcion | sendMessageToConnection |             |              |     |         |
| --- | ------ | -------- | ------- | ----------------------- | ----------- | ------------ | --- | ------- |
|     | //4    | – llamar | a       | funcion                 | sendACK,    | construyendo |     | mensaje |
|     | usando |          | fecha   | y hora,                 | por ejemplo |              |     |         |
break;
}
}
}
| throw | new | Exception("could |     | not | find appropiate |     | connection"); |     |
| ----- | --- | ---------------- | --- | --- | --------------- | --- | ------------- | --- |
}
| public void | sendMessageToConnection(int |     |     |     |     | id, String | message) |     |
| ----------- | --------------------------- | --- | --- | --- | --- | ---------- | -------- | --- |
{
lock (locker)
{
| foreach |     | (Connection |     | con in clients) |     |     |     |     |
| ------- | --- | ----------- | --- | --------------- | --- | --- | --- | --- |
{
|     | if (con.id | ==  | id) |     |     |     |     |     |
| --- | ---------- | --- | --- | --- | --- | --- | --- | --- |
{
con.clientSocket.Send(Encoding.ASCII.GetBytes(message));
}
}
| throw   | new | Exception("could |     | not         | find | appropiate  | connection"); |     |
| ------- | --- | ---------------- | --- | ----------- | ---- | ----------- | ------------- | --- |
| Escuela |     | de Ingeniería    |     | Universidad |      | ORT Uruguay |               |     |
}
}
| public void | sendACK(Socket |     |     | socket, | String | message) |     |     |
| ----------- | -------------- | --- | --- | ------- | ------ | -------- | --- | --- |
{
lock (locker)
{
socket.Send(Encoding.ASCII.GetBytes(message));

| Ingeniería   | de Sistemas. |        |     |     |
| ------------ | ------------ | ------ | --- | --- |
| Programación | de           | Redes. |     |     |
| Examen       | Octubre      | 2023.  |     |     |
2 horas.
}
}
}
| Ejercicio | 2 (25 puntos)   |          |            |     |
| --------- | --------------- | -------- | ---------- | --- |
| Explique  | en sus palabras | :        |            |     |
| 1. Cuando | es necesario    | utilizar | asincronía |     |
2. Explique que es un Semáforo y que es una Monitor y en qué caso usaría uno u otro
| dependiendo | de  | las necesidades | del código. |     |
| ----------- | --- | --------------- | ----------- | --- |
3. Es posible implementar una aplicación que sea asincrónica y paralela al mismo tiempo?
Solución:
1. Cuando nuestra aplicación va a ejecutar operaciones que pueden demorar en el tiempo y
| que van | a resolver | en un entorno | externo a | la misma. |
| ------- | ---------- | ------------- | --------- | --------- |
2. Ambos son mecanismo para mutuexcluir el acceso a ciertas partes del código. En caso
de solamente permitir que un hilo por vez acceda uso monitor pero si necesito que mas de
un proceso ejecute la porcion de codigo en simultaneo utilizo semáforos indicando el nro
| maximo | de procesos | concurrentes | permitidos. |     |
| ------ | ----------- | ------------ | ----------- | --- |
3. Si, ya que podemos querer procesar muchas cosas en paralelo, y cada uno de esos
procesamientos puede implicar la resolución de operaciones por fuera del scope de nuestra
app (lo cual nos puede obligar a esperar estos resultados), por lo que es perfectamente
| posible | combinar ambos | conceptos |     |     |
| ------- | -------------- | --------- | --- | --- |

Ingeniería de Sistemas.
Programación de Redes.
Examen Octubre 2023.
2 horas.
Ejercicio 3 (15 pts)
Mencione los tipos de Exchanges de RabbitMQ vistos en el curso y en qué se diferencian
Respuesta:
Directo - Routing Key = Binding Key
Topic - Routing Key hace match parcialmente con el binding key
Fanout - Propaga los mensajes a todos los bindings
-----
Header - Permite hacer coincidir con una binding key con atributos
Default - El Routing Key coincide con el nombre de la Queue.
Ejercicio 4 (5 pts)
Compare (nombre diferencias) entre Mutex,Monitor y Lock.
Solución:
● lock es equivalente a utilizar monitores
○ Lock es una instrucción, mutex y monitores hacen referencia a clases.
● Monitores es mucho más rápido que Mutex
● Mutex es una primitiva de sincronización. Permite manejar bloqueos a nivel de
Sistema Operativo (a diferencia de monitores, donde el alcance del bloqueo
depende del objeto donde es instanciado).
● Alcance, mutex me permite sincronizar threads entre aplicaciones.
● Mutex > Monitor > Lock

| Ingeniería   | de Sistemas.  |        |     |     |     |
| ------------ | ------------- | ------ | --- | --- | --- |
| Programación | de            | Redes. |     |     |     |
| Examen       | Octubre 2023. |        |     |     |     |
2 horas.
| Ejercicio | 5 (20 Pts) |     |     |     |     |
| --------- | ---------- | --- | --- | --- | --- |
Dada una operación SaveData(String text) que simula guardar datos en una base de
| datos. | Realizar el código | que implique |                    |            |            |
| ------ | ------------------ | ------------ | ------------------ | ---------- | ---------- |
|        | a) Disparar        | dos hilos.   |                    |            |            |
|        | b) Arbitrar        | el acceso    | al método SaveData | bajo mutua | exclusión. |
Solución:
| Ejercicio | 6 (5 pts)       |             |            |       |     |
| --------- | --------------- | ----------- | ---------- | ----- | --- |
| Mencione  | las principales | diferencias | entre GRpc | y MOM |     |
Solución:

| Ingeniería   | de Sistemas.  |        |     |     |
| ------------ | ------------- | ------ | --- | --- |
| Programación | de            | Redes. |     |     |
| Examen       | Octubre 2023. |        |     |     |
2 horas.
| Ejercicio | 7 (10 pts) |     |     |     |
| --------- | ---------- | --- | --- | --- |
Realice el archivo Proto para transmitir (dar de alta) un objeto persona que tenga como
| atributos | el nombre, | la id y el mail | y teléfono(s).   |              |
| --------- | ---------- | --------------- | ---------------- | ------------ |
| Tener en  | cuenta que | puede tener     | más de un número | de teléfono. |
También un método capaz de borrar un usuario (recibe una id) y devuelve un string.
Solución:
| service            | Admin {    |                         |     |     |
| ------------------ | ---------- | ----------------------- | --- | --- |
| // Functions       | availables |                         |     |     |
| rpc Alta(UserDTO)  |            | returns (MessageReply); |     |     |
| rpc DeleteUser(Id) |            | returns (MessageReply); |     |     |
}
| message  | UserDTO{ |         |     |     |
| -------- | -------- | ------- | --- | --- |
| int32    | id = 1;  |         |     |     |
| string   | name     | = 2;    |     |     |
| string   | email    | = 3;    |     |     |
| repeated | string   | phone = | 4;  |     |
}
| message | Id { |     |     |     |
| ------- | ---- | --- | --- | --- |

Ingeniería de Sistemas.
Programación de Redes.
Examen Octubre 2023.
2 horas.
int32 id = 1;
}
message MessageReply {
string message = 1;
}