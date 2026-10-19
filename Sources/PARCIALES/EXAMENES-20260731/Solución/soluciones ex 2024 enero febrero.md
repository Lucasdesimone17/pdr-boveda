| Solución | Examen |     | Programación |     | de Redes | 29/01/2024 |
| -------- | ------ | --- | ------------ | --- | -------- | ---------- |
2 horas
| Ejercicio |             | 1 (20 | puntos) |     |     |     |
| --------- | ----------- | ----- | ------- | --- | --- | --- |
|           | Ver Teórico |       |         |     |     |     |
| Ejercicio |             | 2 (35 | puntos) |     |     |     |
Solución:
struct Connection
{
| public | int id | { get;       | set; } |        |        |     |
| ------ | ------ | ------------ | ------ | ------ | ------ | --- |
| public | Thread | clientThread |        | { get; | set; } |     |
| public | Socket | clientSocket |        | { get; | set; } |     |
}
class Server
{
| private | Socket    | serverSocket; |              |               |              |     |
| ------- | --------- | ------------- | ------------ | ------------- | ------------ | --- |
| private | bool      | bListening    |              | = true;       |              |     |
| private | Thread    | listenThread; |              |               |              |     |
| private | ArrayList |               | clients      | = new         | ArrayList(); |     |
| private | int       | lastId        | = 0;         |               |              |     |
| private | Object    | locker        | =            | new Object(); |              |     |
| public  | const     | int           | SEND_MESSAGE |               | = 1;         |     |
public Server()
{
| serverSocket       |     |     | = new                      | Socket(AddressFamily.InterNetwork, |     |     |
| ------------------ | --- | --- | -------------------------- | ---------------------------------- | --- | --- |
| SocketType.Stream, |     |     |                            | ProtocolType.Tcp);                 |     |     |
| listenThread       |     |     | = new Thread(startListen); |                                    |     |     |
listenThread.Start();
}
| private | void | startListen() |     |     |     |     |
| ------- | ---- | ------------- | --- | --- | --- | --- |
{
serverSocket.Listen(10);
| while | (bListening) |     |     |     |     |     |
| ----- | ------------ | --- | --- | --- | --- | --- |
{
try
{
|     | Socket            | acceptedConnection |           |                       | = serverSocket.Accept(); |     |
| --- | ----------------- | ------------------ | --------- | --------------------- | ------------------------ | --- |
|     | Connection        |                    | conn      | = new                 | Connection();            |     |
|     | conn.id           | =                  | lastId++; |                       |                          |     |
|     | conn.clientSocket |                    |           | = acceptedConnection; |                          |     |

| Solución | Examen | Programación |     | de  | Redes 29/01/2024 |     |     |     |
| -------- | ------ | ------------ | --- | --- | ---------------- | --- | --- | --- |
2 horas
|     | conn.clientThread |     | = new | Thread(handleClient); |     |     |     |     |
| --- | ----------------- | --- | ----- | --------------------- | --- | --- | --- | --- |
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
| foreach | (Connection | con | in  | clients) |     |     |     |     |
| ------- | ----------- | --- | --- | -------- | --- | --- | --- | --- |
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
|     | if (iLocalReceived |                | ==  | 0)                               |     |     |     |     |
{
con.clientSocket.Shutdown(SocketShutdown.Both);
con.clientSocket.Close();
|     | throw | new Exception("Error |     |     | processing | data"); |     |     |
| --- | ----- | -------------------- | --- | --- | ---------- | ------- | --- | --- |
}
|     | iReceived | += iLocalReceived; |     |     |     |     |     |     |
| --- | --------- | ------------------ | --- | --- | --- | --- | --- | --- |
}
|     | int command | = Convert.ToInt32(Encoding.UTF8.GetString(header, |     |     |     |     |     |     |
| --- | ----------- | ------------------------------------------------- | --- | --- | --- | --- | --- | --- |
4, 4));

Solución Examen Programación de Redes 29/01/2024
2 horas
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
}
}

| Solución Examen | Programación | de Redes | 29/01/2024 |
| --------------- | ------------ | -------- | ---------- |
2 horas
| Ejercicio 3 (45 | puntos) |     |     |
| --------------- | ------- | --- | --- |
Ver Teórico.