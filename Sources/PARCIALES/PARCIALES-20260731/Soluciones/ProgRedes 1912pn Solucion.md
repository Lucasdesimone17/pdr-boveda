Evaluación  Parcial
Materia
Carrera
Condiciones  1 a 40 Puntos, 2 horas desde que se lee la letra, sin material

Programación de Redes
Ingeniería de Sistemas, ID

Nocturno

Grupo

Fecha

Dic-2019

Ejercicio 1 (15 puntos)
Se deberá realizar la siguiente implementación utilizando TCP Listener / Client con asincronía
utilizando Async / Await:
Implemente un servidor que permita como máximo cien clientes conectados. Cuando un cliente
se conecta este envía una prioridad (Del uno al diez, siendo uno la más baja y diez la más alta).
Luego de conectado el cliente puede enviar hasta veinte mensajes o elegir desconectarse antes,
si envía veinte mensajes el servidor deberá enviarle un mensaje al cliente (mensaje definido por
usted) y desconectar al mismo.

Si ya existen cien clientes conectados y se quiere conectar otro ahí deberán estudiar dos casos:

1.  Si  el  nuevo  cliente  envía  una  prioridad  menor  o  igual  a  la  menor  de  los  clientes  que  ya  estén

conectados el mismo será desconectado sin poder enviar mensajes.

2.  Si el nuevo cliente envía una prioridad mayor a la menor de los clientes que ya estén conectados
se deberá desconectar el cliente con la prioridad menor, en caso de que exista más de un cliente
que cumpla esta condición se deberá desconectar al que lleva más tiempo conectado al servidor.

Nota: Debe de tener en cuenta que se pide explícitamente que el ejercicio se desarrolle con clases
específicas para manejar el paralelismo o asincronía además del manejo de los datos a través de
la capa de red. En caso de responder al mismo con otra variante el puntaje del ejercicio se verá
afectado negativamente.

SOLUCION

using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Interfaces;

namespace ServerProgram
{
    class ServerProgram
    {
        static readonly ISettingsManager SettingsMgr = new SettingsManager();
        private static int _clientCount = 0;

        private static readonly object Locker = new object();
        private static int[] _priorties = new int[100];

        static async Task Main()
        {
            Console.WriteLine("Server is starting...");
            var ipEndPoint = new IPEndPoint(

IPAddress.Parse(SettingsMgr.ReadSetting(ServerConfig.ServerIpConfigKey)),

int.Parse(SettingsMgr.ReadSetting(ServerConfig.SeverPortConfigKey)));

            var tcpListener = new TcpListener(ipEndPoint);

            tcpListener.Start(100);
            Console.WriteLine("Server started listening connections on {0}-

{1}",SettingsMgr.ReadSetting(ServerConfig.ServerIpConfigKey),SettingsMgr.ReadSetting
(ServerConfig.SeverPortConfigKey));
            Console.WriteLine("Server will start displaying messages from the
clients");

            while (true)
            {
                var tcpClientSocket = await tcpListener.AcceptTcpClientAsync();

                var task = Task.Run(async () => await HandleClient(tcpClientSocket,
getClientCount())); // Pedir un "hilo" del CLR prestado
            }

        }

        private static void incrementClientCount()
        {
            lock (Locker)
                _clientCount++;
        }

        private static void decrementClientCount()
        {
            lock (Locker)

                _clientCount--;
        }

        private static int getClientCount()

        {
            lock (Locker)
                return _clientCount;
        }

        private static async Task HandleClient(TcpClient tcpClientSocket, int id)

        {
            var receivedMessages = 0;

            var isClientConnected = true;
            try
            {
                using (var networkStream = tcpClientSocket.GetStream())

                {
                    var priorityData = new byte[Protocol.WordLength];
                    int totalReceived = 0;
                    while (totalReceived < Protocol.WordLength)

                    {
                        var received = await networkStream
                            .ReadAsync(priorityData, totalReceived,
Protocol.WordLength - totalReceived)

                            .ConfigureAwait(false);
                        if (received == 0)
                        {
                            isClientConnected = false;

                        }
                        totalReceived += received;
                    }

                    var priority = BitConverter.ToInt32(priorityData, 0);
                    var indexToDisconnect =
Array.IndexOf(_priorties,_priorties.Min());
                    if (getClientCount() > 99)

                    {
                        DisconnectClient(indexToDisconnect);
                    }
                    _priorties[indexToDisconnect] = priority;
                    while (isClientConnected)
                    {
                        if (receivedMessages > 19)
                        {
                            var disconnectMessage = Encoding.UTF8.GetBytes("MAX
ALLOWED MESSAGES REACHED");
                            await networkStream.WriteAsync(disconnectMessage, 0,
disconnectMessage.Length);

                            networkStream.Close();
                            isClientConnected = false;

                        }

                        else
                        {
                            var dataLength = new byte[Protocol.WordLength];
                            totalReceived = 0;

                            while (totalReceived < Protocol.WordLength)
                            {

                                var received = await networkStream
                                    .ReadAsync(dataLength, totalReceived,

Protocol.WordLength - totalReceived)
                                    .ConfigureAwait(false);
                                if (received == 0)
                                {

                                    isClientConnected = false;
                                }

                                totalReceived += received;

                            }

                            var length = BitConverter.ToInt32(dataLength, 0);
                            var data = new byte[length];

                            totalReceived = 0;
                            while (totalReceived < length)
                            {
                                int received = await networkStream

                                    .ReadAsync(data, totalReceived, length -
totalReceived)
                                    .ConfigureAwait(false);
                                if (received == 0)

                                {
                                    isClientConnected = false;
                                }

                                totalReceived += received;
                            }

                            var word = Encoding.UTF8.GetString(data);
                            Console.WriteLine("Client says: " + word);
                            receivedMessages++;
                        }
                    }
                }
            }
            catch (SocketException e)
            {

                Console.WriteLine($"The client connection was interrupted -
Exception {e.Message}");
            }
            finally

            {
                decrementClientCount();
            }
        }

        private static void DisconnectClient(int indexToDisconnect)

        {
          //Disconnect client

        }
    }
}

Ejercicio 2 (5 puntos)
El  diario  “El  Detallista”  quiere  crear  un  lector  de  noticias.  Las  noticias  pueden  ser  creadas,
modificadas,  leídas  o  borradas.  Las  noticias  cuentan  con  un  título,  texto,  foto,  fecha  de
publicación y categoría. Las categorías sólo cuentan con nombre y una descripción (pueden ser
creadas  y  modificadas).  Las  personas  que  leen  las  noticias  son  usuarios  del  sistema  que  se
suscriben a las diferentes categorías de noticias.
Deberá ser posible acceder a cada usuario, noticia y categoría.
Es deseable también que se puedan buscar las noticias por fecha, categoría y / o título.
Teniendo en cuenta el escenario anterior diseñe los endpoints para una API REST que modele el
problema.
SOLUCION:
Sección usuarios
Body {

String name,
String address,
Int age

}

URL: api.news.com/users -> Verbos: POST, GET

URL: api. news.com/users/<username> -> Verbos: GET, DELETE, PUT
Sección categorias
Body {

Int category_id,
String name,
String topic

}

URL: api.news.com/users -> Verbos: POST, GET

URL: api. news.com/users/<username> -> Verbos: GET, DELETE, PUT

Sección news
Body {

String título,
String texto,
Base64 foto,
Date publish_date,
Category category

}

URL: api.news.com/news -> Verbos: POST, GET

URL: api. news.com/news/<news_id> -> Verbos: GET, DELETE, PUT

URL: api. news.com/news?date=<date>&category=<category>&title=<title> -> Verbo: GET
NOTA: los parametros date, category y title son opcionales, pero uno de los tres (por lo menos),
debe estar presente.

Ejercicio 3 (5 puntos)
Detalle las principales diferencias entre paralelismo y programación asincrónica desde el marco
del framework .NET.
¿Qué desventajas proporciona usar threads contra usar tasks?
SOLUCION:
El uso de Threads nos obliga a controlar todo el ciclo de vida del mismo, al tiempo que debemos
asegurarnos de hacer un cerrado ordenado del mismo cuando vamos a terminar la aplicación.
Las tasks, por otro lado, utilizan recursos preinicializados, lo cual las hace mas rapidas de iniciar,
mas faciles de programar y mas entendibles a la hora de incorporarlas a una solucion existente
Ejercicio 4 (10 puntos)
Elija el tema que le haya tocado presentar en clase y responda las preguntas al respecto
Azure Functions
Explique brevemente que tipo de arquitectura es la que utiliza Azure Functions.
Detalle que es el concepto de trigger en esta plataforma y de un par de ejemplos concretos.
Azure Logic Apps
Detalle en sus palabras que són las Logic Apps ¿Por qué utilizaría esta tecnología?
Azure Storage
Cuáles son las principales diferencias entre Azure Table Storage y Azure Cosmos DB.
Describa brevemente que es blob storage y detalle los diferentes tipos de bloque que existen.
Blazor
Detalle brevemente el concepto de web assembly.
Detalle brevemente las dos grandes formas de trabajo en la que es posible realizar proyectos con
Blazor.
GraphQL
¿En qué se diferencia GraphQL de las APIs Rest?
¿Puede explicar los conceptos de Schema, Query y Mutation?
GRPC
¿Qué son los archivos proto y qué tipos pueden contener los mismos?
¿Cuándo y por qué utilizaría GRPC sobre otra tecnología como pueden ser APIs Rest?
Microservicios
¿Por qué surge el concepto de Microservicios? Detalle también puntos a favor y puntos en
contra.
¿Por qué es posible asociar los microservicios con una tecnología como Docker?
Rabbit MQ
Detalle el concepto de Broker y porque es importante para Rabbit MQ.
¿Cuáles son los tipos de Exchange que tiene Rabbit y en qué se diferencian?
SOLUCION:
NOTA: se incluyen solo explicaciones de los temas dados en el curso nuevo, ya que salvo GRPC
y RabbitMQ el resto se incluyo solamente en este semestre.

Broker -> Intermediario para el envio de mensajes. Funcion que cumple basicamente el server de
RabbitMQ
Tipos de exchange de RabbitMQ:
Direct Exchange -> Envia directamente a una cola de mensajes especifica, mediante el uso de
una routing key.
Topic Exchange -> Rutea por wildcard, permitiendo enviar a una o mas colas de mensajes al
mismo tiempo.
Fanout Exchange -> Envia a todas las colas de mensajes suscritas a ese exchange al mismo
tiempo.
Headers Exchange -> Similar al Topic Exchange, pero en lugar de usar wildcards para el ruteo,
se usan headers y con combinaciones entre ellos.

