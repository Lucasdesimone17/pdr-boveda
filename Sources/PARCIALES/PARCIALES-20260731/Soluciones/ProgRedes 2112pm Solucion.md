Evaluación

Parcial

Grupo

Matutino

Fecha

Dic-2021

Materia
Carrera
Condiciones

Programación de Redes
Ingeniería de Sistemas, ID
1 a 20 Puntos, 2 horas desde que se lee la letra, sin material

Ejercicio 1 (10 puntos)

Implemente un servidor que pueda manejar varias conexiones en simultáneo y que además cumpla los
siguientes requisitos:

1)   Aceptar conexiones hasta un máximo N (elegido por el alumno)

2)   La aplicación cliente envía el nombre de un artículo, el cual debe ser guardado en una lista en

memoria.

3)   Si el artículo está repetido, esa lista en memoria debe incrementar el valor cantidad de ese

elemento

El artículo cuenta con las siguientes propiedades:

●  Nombre
●  Fecha de vencimiento
●  Descripción
●  cantidad

Se sugiere implementar un protocolo sencillo para la parte de recepción de mensajes.

NOTA: Se puede usar tanto TCPListener / TCPClient como sockets, pero se debe implementar la solución
usando TASKS o THREADS.

SOLUCION:

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Interfaces;

namespace ServerProgram
{
    class Articulo
    {
        public Articulo(string nombre, DateTime fechaVencimiento, string
descripcion, int cantidad)
        {
            _nombre = nombre;
            _fechaVencimiento = fechaVencimiento;
            _descripcion = descripcion;

            _cantidad = cantidad;
        }

        public string Nombre
        {
            get => _nombre;
            set => _nombre = value;
        }

        public DateTime FechaVencimiento
        {
            get => _fechaVencimiento;
            set => _fechaVencimiento = value;
        }

        public string Descripcion
        {
            get => _descripcion;
            set => _descripcion = value;
        }

        public int Cantidad
        {
            get => _cantidad;
            set => _cantidad = value;
        }

        private string _nombre;
        private DateTime _fechaVencimiento;
        private string _descripcion;
        private int _cantidad;
    }

    class ServerProgram
    {
        //PROTOCOL = LENGTH + ARTICLE
        //ARTICLE = NOMBRE#FECHAVENCIMIENTO#DESCRIPCION#CANTIDAD
        static readonly ISettingsManager SettingsMgr = new SettingsManager();
        private static readonly object Locker = new object();
        private static List<Articulo> listArticulos = new List<Articulo>();

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

                    var article = Encoding.UTF8.GetString(data);
                    Console.WriteLine("Article received: " + article);
                    var elements = article.Split("#");

                    if (elements.Length != 4) {
                        Console.WriteLine("Wrong Article received!");
                    } else {
                        var art = new Articulo(elements[0],
                            DateTime.Parse(elements[1]),
                            elements[2],
                            int.Parse(elements[3]));
                        AgregarArticulo(art);
                    }

                }
            }
            catch (SocketException e)
            {
                Console.WriteLine($"The client connection was interrupted -
Exception {e.Message}");
            }
        }

        private static void AgregarArticulo(Articulo art)
        {
            lock (Locker)
            {
                var result = listArticulos.FindIndex(x => x.Nombre ==
art.Nombre);
                if (result == -1)
                    listArticulos.Add(art);
                else
                    listArticulos[result].Cantidad += art.Cantidad;
            }
        }
    }
}

Ejercicio 2 (3 puntos)

Explique que es un mecanismo de comunicación basado en RPC, y detalle los diferentes modos de
funcionamiento de gRPC vistos en el curso

SOLUCION:

Un mecanismo de comunicacion basado en RPC permite la invocacion de funcionalidad remota de forma
transparente, tal cual como si esta se encontrara de forma local. Involucra tener un motor de RPC que
permita encapsular la peticion, enviarla al server que la procesa y hacer lo propio con la respuesta, para
que la misma sea procesada de forma local

Modos de funcionamiento de GRPC:

1.  Single invocation, se envia una peticion y vuelve una respuesta
2.  Client Side stream, se envia un stream de peticiones, vuelve una unica respuesta.
3.  Server Side stream. Se envia una unica peticion y vuelve un stream de respuestas

4.  Stream bidireccional. Se abre un stream del lado del cliente y se envia un stream, al mismo tiempo

que se recibe un stream como respuesta.

Referencia: https://www.ionos.es/digitalguide/servidores/know-how/que-es-grpc/

Ejercicio 3 (4 puntos)

Tomando como base las tecnologías vistas en el curso, y teniendo en cuenta las siguientes premisas:

•  Se necesita construir una app que reciba datos desde diferentes clientes
•  Existe  un  canal  de  comunicación  primario  que  recibe  información  que  deba  ser  procesada  en

tiempo real

•  Existe  también  un  canal  secundario,  que  recibe  información  no  crítica  y  se  puede  procesar  de

forma asincrónica.

•  Tanto el canal primario como el secundario deben ser capaces de atender a varios clientes a la

vez.

Discuta cuáles son las mejores opciones para cada uno de los canales de comunicación y justifique su
respuesta.

SOLUCION:

Tomando en cuenta el funcionamiento de esta aplicación, se podría exponer dos canales. El primero, el
cual  debe  recibir  información  que  se  debe  procesar  en  tiempo  real,  puede  ser  con  las  siguientes
tecnologías:

•  API Rest (HTTP)
•  Sockets
•  GRPC

Cualquiera de las tres permite tener un canal abierto y recibir información de forma rápida. Con sockets
tenemos la máxima performance a costa de tener que implementar todo el mecanismo de codificación y
decodificación de mensajes, así como también manejar la conexión de forma manual. Si usamos una API
Rest, podemos tener mucha flexibilidad y facilidad de implementación a costa de performance. La opción
más equilibrada parece ser GRPC porque nos da una buena performance con una facilidad relativa de
implementación.

Para el canal secundario, lo más razonable sería hacer un consumidor de una cola de mensajes para que
se vayan procesando los mismos a medida que van llegando y sin necesidad de acoplarse al productor de
los mismos.

Con esta solución, podemos hacer una aplicación que soporte múltiples clientes que se puedan conectar
a este servidor.

Ejercicio 4 (3 puntos)

●  Defina y describa las diferencias entre un proceso, una Task y un Thread
●  Defina el concepto de deadlock
●  Describa tres métodos que solucionen el problema de la mutua exclusión.

SOLUCION

Proceso. Instancia de un programa en ejecución

Thread. Secuencia (función) que ejecuta dentro de un proceso de forma paralela.

Task. Operación que ejecuta de forma desatendida y de manera asincrónica dentro de un proceso.

Deadlock:  bloqueo  indefinido  que  se  da  por  la  sincronización  de  dos  o  más  threads/tasks  intentando
acceder a un mismo recurso.

Metodos vistos en el curso para atacar el problema de mutuoexclusion:

1.  Lock. Mecanismo para poder crear una zona critica de ejecución (solo un thread a la vez puede

ejecutar ese código).

2.  Monitores, igual al lock pero que además suma el envío de señales entre threads.
3.  Semaforos. Mecanismo para sincronizar el acceso de varios hilos a un único recurso compartido

(permite la ejecución de más de un thread a la vez)

4.  Manual y Auto ResetEvents. Permiten enviar señales entre threads para indicar si una condición
cambia o no. El autoreset retorna a su estado de señalización original, mientras que el manual
mantiene el estado hasta que se resetee manualmente.

