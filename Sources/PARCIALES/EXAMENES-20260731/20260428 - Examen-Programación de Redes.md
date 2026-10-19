Fecha: 28/04/2026

Evaluación: Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 1 de 2

Puntaje Máximo:     100 Puntos

Ejercicio 1 (40 puntos)

Responda 5 de las siguientes 7 preguntas. Respuestas breves, claras y con ejemplos cuando aplique.

1.  Explique la importancia de operaciones idempotentes en un sistema que se comunique a través

de colas de mensajes utilizando un sistema como RabbitMQ.

2.  Explique qué problema soluciona Docker y cómo se diferencia con una máquina virtual.
3.  De un ejemplo de una operación I/O-bound y una CPU-bound. ¿Cómo se relacionan con el

concepto de Thread Pool?

4.  ¿Cuál es la diferencia entre proceso e hilo? Explique una ventaja de utilizar un hilo por cliente en

un servidor hecho con Sockets en vez de utilizar un proceso por cliente.

5.  Explique la diferencia entre short-polling y long-polling.
6.  Explique las diferencias entre Websockets y una API HTTP REST.
7.  Explique la importancia del archivo .proto en gRPC.

Ejercicio 2 (35 puntos)

Diseñe dos ejemplos independientes de sistemas que se comuniquen a través de colas de mensajes
utilizando RabbitMQ:

●  Ejemplo A: patrón Publish/Subscribe (pub/sub).
●  Ejemplo B: patrón Work Queue.

Para cada ejemplo incluya: breve descripción del caso de uso y un diagrama (productores,
consumidores, exchanges y colas).

Adicionalmente, incorpore una Dead Letter Queue en uno de los dos ejemplos donde considere que
aplica, justiﬁcando su elección.

Fecha: 28/04/2026

Evaluación: Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 2 de 2

Puntaje Máximo:     100 Puntos

Ejercicio 3 (25 puntos)

Dado el siguiente código de un servidor TCP que atiende múltiples clientes utilizando Socket:

Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
ProtocolType.Tcp);
listener.Bind(new IPEndPoint(IPAddress.Any, 5000));
listener.Listen(10);

int clientesConectados = 0;

while (true)
{
    Socket clientSocket = listener.Accept();
    clientesConectados++;
    Console.WriteLine($"Cliente conectado. Total: {clientesConectados}");

    new Thread(() =>
    {
        byte[] buffer = new byte[1024];
        int bytesRecibidos = clientSocket.Receive(buffer);
        string mensaje = Encoding.UTF8.GetString(buffer, 0, bytesRecibidos);

        byte[] respuesta = Encoding.UTF8.GetBytes($"ECHO: {mensaje}");
        clientSocket.Send(respuesta);

        clientSocket.Close();
        clientesConectados--;
    }).Start();
}

A. Modiﬁque el código para que utilice Task y el modelo asincrónico async/await en lugar de Thread.

B. El código tiene un problema de condición de carrera. Indíquelo y proponga una solución.

