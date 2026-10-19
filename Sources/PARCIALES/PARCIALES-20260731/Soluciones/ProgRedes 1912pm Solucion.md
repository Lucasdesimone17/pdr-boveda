Evaluación

Parcial

Grupo

Matutino

Fecha

Dic-2019

Materia
Carrera
Condiciones

Programación de Redes
Ingeniería de Sistemas, ID
1 a 40 Puntos, 2 horas desde que se lee la letra, sin material

Ejercicio 1 (15 puntos)

Implemente un servidor que atienda conexiones y que sea capaz de:

1)  Aceptar conexiones hasta un maximo N (elegido por el alumno)
2)  Cada X cantidad de tiempo, se debe desconectar al cliente que menos mensajes envio al

servidor

3)  Cada vez que se recibe un mensaje desde un cliente, se debe mostrar el mismo en pantalla

Se sugiere implementar un protocolo sencillo para la parte de recepcion de mensajes.

NOTA: Se puede usar tanto TCPListener / TCPClient como sockets, pero se debe implementar la solucion
usando TASKS y THREADS.

SOLUCION:

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Interfaces;

namespace ServerProgram
{
    class ServerProgram
    {
        static readonly ISettingsManager SettingsMgr = new SettingsManager();
        private static readonly object Locker = new object();
        private static List<Tuple<TcpClient,int>> _arrayClients = new
List<Tuple<TcpClient,int>>();

        static async Task Main()
        {
            Console.WriteLine("Server is starting...");
            var ipEndPoint = new IPEndPoint(

IPAddress.Parse(SettingsMgr.ReadSetting(ServerConfig.ServerIpConfigKey)),

int.Parse(SettingsMgr.ReadSetting(ServerConfig.SeverPortConfigKey)));
            var tcpListener = new TcpListener(ipEndPoint);

            tcpListener.Start(100);
            Console.WriteLine("Server started listening connections on {0}-
{1}",
                SettingsMgr.ReadSetting(ServerConfig.ServerIpConfigKey),
                SettingsMgr.ReadSetting(ServerConfig.SeverPortConfigKey));

            Console.WriteLine("Server will start displaying messages from the
clients");
            var task1 = Task.Run(async () =>
                await DisconnectLeastMessageCount());
            while (true)
            {
                var tcpClientSocket = await
tcpListener.AcceptTcpClientAsync();
                var task2 = Task.Run(async () =>
                    await HandleClient(tcpClientSocket)); // Pedir un "hilo"
del CLR prestado
                AddToConnectedClients(tcpClientSocket);
            }
        }

        private static Task DisconnectLeastMessageCount()
        {

            while (true)
            {
                Thread.Sleep(3000);
                var leastCount = 0;
                var indexToRemove = 0;
                lock (Locker)
                {
                    for (var i = 0; i < _arrayClients.Count; i++)
                    {
                        if (_arrayClients[i].Item2 < leastCount)
                        {
                            indexToRemove = i;
                        }
                    }
                    _arrayClients[indexToRemove].Item1.Close();
                    _arrayClients.RemoveAt(indexToRemove);
                }
            }
        }

        private static async Task HandleClient(TcpClient tcpClientSocket)
        {
            var isClientConnected = true;
            try
            {
                await using var networkStream = tcpClientSocket.GetStream();
                while (isClientConnected)
                {
                    var dataLength = new byte[Protocol.WordLength];
                    int totalReceived = 0;
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
                        var received = await networkStream
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
                    IncrementMessageCount(tcpClientSocket);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine($"The client connection was interrupted -
Exception {e.Message}");
            }
        }

        private static void AddToConnectedClients(TcpClient tcpClientSocket)
        {
            lock (Locker)
            {
                _arrayClients.Add(new Tuple<TcpClient, int>(tcpClientSocket,
0));
            }
        }

        private static void IncrementMessageCount(TcpClient tcpClientSocket)
        {
            lock (Locker)
            {
                for (var i = 0; i < _arrayClients.Count; i++)
                {
                    if (_arrayClients[i].Item1 != tcpClientSocket)

                        continue;
                    var count = (_arrayClients[i].Item2);
                    _arrayClients[i] = new Tuple<TcpClient,
int>(tcpClientSocket, count++);
                    break;
                }
            }
        }
    }
}

Ejercicio 2 (8 puntos)

Compare una arquitectura monolitica vs una arquitectura de microservicios desde el punto de vista de la
escalabilidad y la mantenibilidad.

SOLUCION:

Arquitectura Monolitica:

•  Mas dificil de escalar verticalmente
•  Dificil de mantener una vez que crece de tamaño
•  Tamaño de la aplicación dificulta el arranque inicial
•  CI/CD dificil de mantener sin tener downtime
•  Simple de desarrollar inicialmente
•  Simple de deployar
•  Simple de escalar horizontalmente

Arquitectura de microservicios:

•  Facil de escalar horizontalmente y verticalmente
•  Mas sencilla de mantener en un CI/CD
•  Mayor facilidad de migrar sin downtime de aplicaciones
•  Mayor complejidad inicial para desarrollar
•  Mas sencillo de crear una funcionalidad nueva que afecte solo a un microservicio
•  Mas complejo de agregar funcionalidad tranversal a varios microservicios

Ejercicio 3 (12 puntos)

Imagine que tiene que construir un componente de una arquitectura que debe recibir muchos mensajes
desde diferentes aplicaciones. Estos mensajes no van a llegar al mismo tiempo y tampoco es critico su
entrega hacia este componente.

A su vez, el componente tiene que entregar, a medida que vaya obteniendo sus resultados a partir de los
mensajes recibidos, los resultados hacia otra aplicación que los necesita tan pronto como se obtengan los
mismos.

Discuta con que tecnologias vistas en el curso es mejor implmentar la recepcion de mensajes y la entrega
de los resultados hacia la aplicación que los espera.

SOLUCION:

Para la parte de recepcion de mensajes, es preferible utilizar una tecnologia de recepcion asincronica y
que no dependa de si estan los clientes presentes o no. Por ello es que parece mas conveniente utilizar
Colas de Mensajes en este punto, asi la recepcion es totalmente asincronica entre emisor y receptor.

Para el envio de mensajes, una vez que se obtienen los resultados necesarios, parece razonable utlilizar
alguna tecnologia que sea performante, por lo que las soluciones posibles son:

1.  Sockets
2.  GRPC

Con Sockets, tenemos la maxima performance a costa de tener que implementar todo el protocolo de
envio y recepcion, asi como la decodificacion manual de paquetes.

Con GRPC, tenemos la ventaja de que tenemos la parte de transporte resuelta a costa de un poco de
performance, pero es la opcion mas mantenible de las dos

Ejercicio 4 (5 puntos)

•  ¿En qué se diferencia un proceso de un thread? ¿Todo proceso tiene un thread?
•  Nombre tres métodos que solucionen el problema de la mutua exclusión.

SOLUCION:

Un proceso es una instancia de un ejecutable en memoria. Un thread, en cambio, es un flujo de ejecucion
DENTRO de un proceso. Un  proceso a priori no comparte nada con otro proceso, salvo tiempo de CPU.
Un thread si comparte variables globales con otro thread por ejemplo, asi como tambien puede compartir
codigo con otro.

Soluciones al problema de mutuoexlcusion (mecanismos vistos en el curso):

•  Events (Manual y Auto)
•  Semaforos
•  Monitores
•
Locks

