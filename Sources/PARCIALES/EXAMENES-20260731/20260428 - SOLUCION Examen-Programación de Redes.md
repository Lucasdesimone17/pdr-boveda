Fecha:28/04/2026

Solucion Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 1 de 5

Puntaje Máximo:     100 Puntos

Solución

Ejercicio 1 (40 puntos)

Responda 5 de las siguientes 7 preguntas. Respuestas breves, claras y con ejemplos cuando aplique.

1.  Explique la importancia de operaciones idempotentes en un sistema que se comunique a través

de colas de mensajes utilizando un sistema como RabbitMQ.

Solución: RabbitMQ garantiza entrega "at least once", por lo que un mismo mensaje puede llegar más de
una vez (reintentos por fallos del consumer, ack perdidos, reconexiones). Si la operación es idempotente,
procesar el mismo mensaje N veces produce el mismo efecto que procesarlo una sola vez, evitando
duplicados (ej. cobros repetidos, registros duplicados).

2.  Explique qué problema soluciona Docker y cómo se diferencia con una máquina virtual.

Solución: Docker resuelve el problema de "funciona en mi máquina": empaqueta la aplicación junto con
sus dependencias y conﬁguración en una imagen, garantizando que se ejecute igual en cualquier
entorno. Diferencia con VM: una VM virtualiza hardware completo y corre un sistema operativo completo
sobre un hipervisor (pesada, lenta de iniciar, mucho consumo de recursos). Docker virtualiza a nivel de
SO compartiendo el kernel del host (ligero, inicia en segundos, menor consumo)

3.  De un ejemplo de una operación I/O-bound y una CPU-bound. ¿Cómo se relacionan con el

concepto de Thread Pool?

Solución: I/O-bound: leer un archivo, consultar una base de datos, hacer una request HTTP (el hilo
espera que termine una operación externa). CPU-bound: comprimir un video, calcular un hash, procesar
una imagen (el hilo está usando la CPU activamente). Relación con Thread Pool: el pool tiene una
cantidad limitada de hilos que se comparten entre muchas tareas. En operaciones I/O-bound, mientras
una tarea está esperando una respuesta externa, el hilo puede liberarse y ser reutilizado por otra tarea
(usando async/await), aprovechando los mismos hilos para atender muchas más operaciones
concurrentes.

4.  ¿Cuál es la diferencia entre proceso e hilo? Explique una ventaja de utilizar un hilo por cliente en

un servidor hecho con Sockets en vez de utilizar un proceso por cliente.

Solución: Un proceso es una instancia en ejecución de un programa con su propio espacio de memoria,
mientras que un hilo es una unidad de ejecución dentro de un proceso, que comparte la memoria con los
demás hilos del mismo proceso. Ventaja del hilo por cliente: los hilos son mucho más livianos que los
procesos (menor uso de RAM, creación más rápida, cambios de contexto más baratos), lo que permite

Fecha:28/04/2026

Solucion Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 2 de 5

Puntaje Máximo:     100 Puntos

escalar a más clientes simultáneos. Además, comparten memoria, facilitando el acceso a  datos
compartidos.

5.  Explique la diferencia entre short-polling y long-polling.

Solución: Short-polling: el cliente consulta cada X segundos al servidor preguntando si hay novedades; el
servidor responde de inmediato (con datos o vacío). Genera mucho tráﬁco y la latencia depende del
intervalo. Long-polling: el cliente hace una request y el servidor mantiene la conexión abierta hasta que
tenga novedades (o hasta un tiempo máximo). Cuando responde, el cliente abre una nueva conexión.
Menos requests y menor latencia, pero consume conexiones del servidor más duraderas.

6.  Explique las diferencias entre Websockets y una API HTTP REST.

Solución: REST sigue el modelo request/response: el cliente inicia cada interacción, la conexión es de
corta duración y la comunicación es unidireccional por request. Es stateless. WebSockets establece una
conexión persistente full-duplex sobre TCP (tras un handshake HTTP inicial), permitiendo que tanto
cliente como servidor envíen mensajes en cualquier momento sin nuevas requests. Ideal para chat,
notiﬁcaciones en tiempo real o edición colaborativa. REST es más simple, cacheable y se beneﬁcia de la
infraestructura HTTP existente

7.  Explique la importancia del archivo .proto en gRPC.

Solución: El archivo .proto es el contrato formal entre cliente y servidor en gRPC: deﬁne los servicios, sus
métodos y los mensajes (estructuras de datos) en un lenguaje neutral (Protocol Buffers). A partir de él
se autogenera el código (stubs) tanto del cliente como del servidor en cualquier lenguaje soportado..
Permite serialización binaria eﬁciente y soporta evolución del contrato (agregar campos sin romper
compatibilidad).

Ejercicio 2 (35 puntos)

Diseñe dos ejemplos independientes de sistemas que se comuniquen a través de colas de mensajes
utilizando RabbitMQ:

●  Ejemplo A: patrón Publish/Subscribe (pub/sub).
●  Ejemplo B: patrón Work Queue.

Para cada ejemplo incluya: breve descripción del caso de uso y un diagrama (productores,
consumidores, exchanges y colas).

Adicionalmente, incorpore una Dead Letter Queue en uno de los dos ejemplos donde considere que
aplica, justiﬁcando su elección.

Fecha:28/04/2026

Solucion Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 3 de 5

Puntaje Máximo:     100 Puntos

Solución

Ejemplo A - Pub/Sub: Sistema de notiﬁcaciones de una red social. Cuando un usuario publica un post, el
evento se publica en un exchange tipo fanout. Múltiples consumidores (servicio de notiﬁcaciones push,
servicio de email, servicio de feed) reciben el evento y actúan según su responsabilidad. Cada
consumidor tiene su propia cola enlazada al exchange, por lo que todos reciben una copia del mensaje.
Diagrama: Productor (Servicio de Posts) → Exchange (fanout) → Cola Notif Push → Consumer Push /
Cola Email → Consumer Email / Cola Feed → Consumer Feed.

Ejemplo B - Work Queue: Sistema de procesamiento de imágenes. Cuando un usuario sube una foto, se
encola un trabajo en una cola única. Varios workers consumen de la misma cola y se reparten el trabajo
(cada mensaje lo procesa solo un worker). Permite escalar horizontalmente agregando más workers
según la carga. Diagrama: Productor (API Upload) → Cola "procesar_imagen" → Worker 1 / Worker 2 /
Worker 3.

DLQ: Aplica claramente en el ejemplo B (Work Queue). El procesamiento de imágenes puede fallar
(imagen corrupta, formato no soportado, error transitorio), y luego de N reintentos sin éxito, el mensaje
se enruta a una DLQ para análisis manual o reprocesamiento posterior, evitando que un mensaje
"envenenado" bloquee la cola principal. Se justiﬁca menos en pub/sub si los consumidores son simples
emisores de notiﬁcaciones, aunque también podría aplicarse.

Fecha:28/04/2026

Solucion Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 4 de 5

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

Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
listener.Bind(new IPEndPoint(IPAddress.Any, 5000));
listener.Listen(10);

int clientesConectados = 0;
object lockObj = new object();

Fecha:28/04/2026

Solucion Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 5 de 5

Puntaje Máximo:     100 Puntos

while (true)
{
    Socket clientSocket = await listener.AcceptAsync();

    lock (lockObj)
    {
        clientesConectados++;
    }
    Console.WriteLine($"Cliente conectado. Total: {clientesConectados}");

    _ = Task.Run(async () =>
    {
        byte[] buffer = new byte[1024];
        int bytesRecibidos = await clientSocket.ReceiveAsync(buffer, SocketFlags.None);
        string mensaje = Encoding.UTF8.GetString(buffer, 0, bytesRecibidos);

        byte[] respuesta = Encoding.UTF8.GetBytes($"ECHO: {mensaje}");
        await clientSocket.SendAsync(respuesta, SocketFlags.None);

        clientSocket.Close();

        lock (lockObj)
        {
            clientesConectados--;
        }
    });
}

B. El código tiene un problema de condición de carrera. Indíquelo y proponga una solución.

Solución: La condición de carrera ocurre sobre la variable clientesConectados, modificada
concurrentemente por múltiples hilos (incremento al aceptar y decremento al cerrar). Las operaciones
++ y -- no son atómicas, por lo que pueden perderse actualizaciones. Se soluciona usando un lock
sobre un objeto compartido al modificar la variable, como se muestra en A.

