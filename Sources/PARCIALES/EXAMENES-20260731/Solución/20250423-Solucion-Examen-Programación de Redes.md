Fecha: 23/04/2025

Solución de Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Nocturno

Puntaje Máximo:     100 Puntos

Página 1 de 9

20250423-Solución-Examen-Programación de Redes

Ejercicio 1 (10 puntos)

Pregunta 1: Explique qué ventajas tengo al utilizar la librería TCPClient/TCPListener en vez de Sockets.

Abstracción y simplicidad: TcpClient y TcpListener proporcionan una capa de abstracción sobre
los sockets, lo que simplifica el código y reduce la necesidad de manejar detalles de bajo nivel como el
establecimiento y cierre de conexiones manualmente.

Manejo automático de conexiones: TcpListener facilita la escucha de conexiones entrantes y
TcpClient proporciona métodos más sencillos para enviar y recibir datos sin necesidad de interactuar
directamente con Socket.

Menos código repetitivo: Al utilizar Sockets, se requiere una mayor cantidad de código para
configurar y administrar conexiones, leer y escribir datos, y manejar errores.
TcpClient/TcpListener encapsulan estas operaciones en métodos más intuitivos.

Manejo de Streams: TcpClient proporciona un NetworkStream, lo que permite trabajar con flujos
(Stream) de datos de manera más sencilla, utilizando métodos estándar de lectura y escritura en lugar
de Send y Receive de Socket.

Más seguridad y robustez: Debido a la abstracción proporcionada, es menos probable cometer errores
relacionados con la configuración de sockets, lo que mejora la estabilidad y mantenibilidad del código.

Pregunta 2: Explique las ventajas y desventajas que tiene gRPC sobre API Rest. Describa brevemente
un ejemplo donde sea mejor utilizar gRPC que API Rest.

Ventajas de gRPC sobre API REST:

1.  Mayor rendimiento: gRPC utiliza el protocolo HTTP/2, que permite la multiplexación de

múltiples llamadas en una única conexión, reduciendo la latencia y mejorando el rendimiento en
comparación con REST, que generalmente usa HTTP/1.1.

2.  Eficiencia en la serialización: gRPC usa Protocol Buffers (protobuf) en lugar de JSON, lo

que reduce significativamente el tamaño de los mensajes transmitidos y mejora la velocidad de
serialización y deserialización.

3.  Comunicación bidireccional y streaming: gRPC soporta streaming de datos tanto del cliente
al servidor como del servidor al cliente, permitiendo una comunicación más eficiente en tiempo
real.

1

Fecha: 23/04/2025

Solución de Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Nocturno

Puntaje Máximo:     100 Puntos

Página 2 de 9

4.  Contratos bien definidos: Al usar archivos .proto, se genera automáticamente código cliente
y servidor, asegurando que ambas partes cumplan con el contrato definido sin ambigüedad.
5.  Mejor soporte para múltiples lenguajes: Gracias a protobuf, gRPC puede generar código

para múltiples lenguajes de programación, facilitando la interoperabilidad en sistemas
heterogéneos.

Desventajas de gRPC sobre API REST:

1.  Menor compatibilidad con navegadores: API REST es más compatible con los navegadores,
mientras que gRPC requiere gRPC-web o un proxy para ser consumido directamente desde
aplicaciones web.

2.  Mayor complejidad: La configuración y el despliegue de un servicio gRPC pueden ser más

complicados debido al uso de HTTP/2, protobuf y la necesidad de generar código
cliente/servidor.

3.  Dificultad en debugging: REST utiliza JSON, que es legible por humanos, mientras que gRPC
usa protobuf, lo que hace más difícil inspeccionar las respuestas sin herramientas específicas.

4.  Menor adopción: Aunque gRPC es popular en entornos de alto rendimiento, muchas

aplicaciones aún dependen de API REST por su simplicidad y compatibilidad con herramientas
existentes.

Ejemplo donde gRPC es mejor que API REST:

Sistema de comunicación entre microservicios de una plataforma de streaming en tiempo real

Supongamos que una plataforma de streaming necesita que los microservicios de autenticación, gestión
de contenido y recomendaciones se comuniquen entre sí con baja latencia y alto rendimiento.

●  Con gRPC, las solicitudes son rápidas y eficientes, utilizando protobuf para minimizar la

carga de datos transmitidos y HTTP/2 para multiplexar las conexiones.

●  El streaming bidireccional permite actualizar recomendaciones en tiempo real sin necesidad

de realizar múltiples solicitudes HTTP.

●  Al generar automáticamente los clientes en diferentes lenguajes, los microservicios pueden estar

escritos en Python, Go y Java sin problemas de compatibilidad.

Si se usara API REST en este escenario, la sobrecarga de JSON, múltiples conexiones HTTP/1.1 y la
falta de streaming bidireccional harían que el sistema fuera menos eficiente.

2

Fecha: 23/04/2025

Solución de Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Nocturno

Puntaje Máximo:     100 Puntos

Página 3 de 9

Ejercicio 2 (15 puntos)

Afirmación 1: Los Semáforos son una herramienta más ﬂexible que los Monitores para la
sincronización de hilos en sistemas concurrentes.

●  Respuesta: Verdadero ✅
●  Explicación:
●  Los semáforos son una herramienta más flexible que los monitores en la sincronización de

hilos porque permiten controlar el acceso a múltiples recursos y pueden ser utilizados tanto
para sincronización como para control de concurrencia.

Afirmación 2: En .NET, el uso de Task es más adecuado que Thread para operaciones que involucran
esperas de I/O.

Respuesta: Verdadero ✅

Explicación:

En .NET, el uso de Task es más adecuado que Thread para operaciones que involucran esperas de
I/O porque Task está diseñado para manejar operaciones asíncronas y concurrentes de manera más
eficiente.

Razones por las que Task es mejor que Thread en operaciones de I/O:

1.  Aprovechamiento del modelo asincrónico (Async/Await)

○  Task se integra con el modelo asíncrono de .NET, lo que permite que el hilo actual no
quede bloqueado mientras espera que la operación de I/O (por ejemplo, acceso a una
base de datos o lectura de un archivo) se complete.

○  En cambio, Thread trabaja de forma sincrónica, bloqueando el hilo hasta que la

operación termine, lo que es ineficiente en operaciones de I/O.

2.  **Uso eficiente del Thread Pool

○  Task utiliza el ThreadPool, permitiendo que los hilos sean reutilizados en lugar de crear

nuevos hilos constantemente, lo que reduce la sobrecarga.

○  Thread, por otro lado, crea un hilo dedicado para cada tarea, lo que consume más

recursos y puede llevar a problemas de escalabilidad si hay muchas operaciones de I/O
simultáneas.
3.  Evita bloqueos innecesarios

3

Fecha: 23/04/2025

Solución de Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Nocturno

Puntaje Máximo:     100 Puntos

Página 4 de 9

○  Si se usa Thread, el hilo permanece ocupado hasta que la operación termine, lo que

puede llevar a un mal uso de los recursos.

4.  Mejor escalabilidad

○  Task permite ejecutar muchas operaciones asíncronas sin necesidad de un número

excesivo de hilos físicos, lo que mejora la escalabilidad de la aplicación.

○  Thread no es escalable cuando hay muchas operaciones de I/O, ya que cada Thread

ocupa memoria y consume recursos del sistema.
5.  Integración con el patrón de programación asíncrona en .NET

○  Task es la base del patrón async/await, lo que permite escribir código más claro y

fácil de mantener.

○  Thread no se integra de manera natural con async/await, lo que puede complicar la

implementación de operaciones asíncronas.

4

Fecha: 23/04/2025

Solución de Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Nocturno

Puntaje Máximo:     100 Puntos

Página 5 de 9

Afirmación 3: En un sistema basado en colas de mensajes, los mensajes siempre se procesan en el
mismo orden en que fueron enviados.

Respuesta: Falso ❌

Explicación:

En un sistema basado en colas de mensajes (Message Queue Systems), los mensajes no siempre
se procesan en el mismo orden en que fueron enviados. Existen varios factores que pueden afectar
el orden de procesamiento:

1.  Procesamiento en paralelo:

○  Muchas colas de mensajes permiten múltiples consumidores (consumidores

concurrentes) procesando mensajes en paralelo.

○  Si varios consumidores están leyendo de la misma cola, el orden de llegada no garantiza

el orden de procesamiento, ya que cada mensaje será procesado por el primer
consumidor disponible.

2.  Redirección de mensajes a múltiples colas (Fan-out):

○  Algunos sistemas de mensajería, como RabbitMQ (con Exchanges) o Kafka (con
Particiones), pueden distribuir los mensajes entre varias colas o particiones, lo que
afecta el orden de procesamiento.

3.  Prioridad de mensajes:

○  Algunos sistemas de colas permiten establecer prioridades.
○  Si hay mensajes de alta prioridad, estos pueden ser procesados antes que otros

mensajes enviados anteriormente.

4.  Retries y reenvíos de mensajes:

○  Si un mensaje falla en su procesamiento y debe ser reintentado, puede procesarse

después de otros mensajes que llegaron más tarde.

○  Esto es común en sistemas con Dead Letter Queues (DLQ).

5.  Diferentes configuraciones de enrutamiento:

○  Sistemas como RabbitMQ permiten configurar tipos de exchanges que pueden afectar

el orden de los mensajes cuando se redirigen a diferentes colas.

5

Fecha: 23/04/2025

Solución de Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Nocturno

Puntaje Máximo:     100 Puntos

Página 6 de 9

Ejercicio 3 (35 puntos)

Esquema de solución:

1.  Servidor multicliente:

○  El servidor debe usar TcpListener o Socket para escuchar conexiones entrantes en un
puerto. Cuando se recibe una nueva conexión, se asigna un hilo o tarea (Thread/Task)
para manejar esa conexión de manera individual.

○  El servidor limita el número máximo de conexiones a N (definido por el alumno).

2.  Asignación de ID autoincremental:

○  Utiliza una variable global compartida para manejar el ID autoincremental. Cada vez que

un cliente se conecta, se le asigna un nuevo ID único.

3.  Envío del ID al cliente:

○  Después de aceptar la conexión, el servidor envía un mensaje al cliente con su ID

asignado.
4.  Manejo de desconexiones:

○  El servidor debe detectar cuando un cliente se desconecta y liberar los recursos

asociados. Esto puede lograrse monitorizando la transmisión de datos o utilizando
excepciones que se generen al fallar la conexión.

Ejemplo de código básico (C#):

TcpListener listener = new TcpListener(IPAddress.Any, 8080);

listener.Start();

bool salir = false;

int i = 1;

while (!salir)

{

    TcpClient client = await listener.AcceptTcpClientAsync();

    int clientId = i;

    Task.Run(() => HandleClient(client, id));

    i++;

}

static async Task HandleClient(TcpClient client, int clientId)    {

        NetworkStream stream = client.GetStream();

6

Fecha: 23/04/2025

Solución de Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Nocturno

Puntaje Máximo:     100 Puntos

Página 7 de 9

        byte[] message = Encoding.UTF8.GetBytes($"Your ID is {id}");

        byte[] msglength = BitConverter.GetBytes(message.Length);

        await stream.WriteAsync(msglength, 0, 4);

        await stream.WriteAsync(message, 0, message.Length);

       // Manejar desconexión y liberar recursos.

    });

}

7

Fecha: 23/04/2025

Solución de Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Nocturno

Puntaje Máximo:     100 Puntos

Página 8 de 9

Ejercicio 4 (40 puntos)

Solución sugerida:

1.  Tecnologías elegidas:

○  Sockets: Para la comunicación bidireccional entre la casa matriz y las sucursales, ya que

permite consultas rápidas y de bajo nivel.

○  MOM (RabbitMQ): Para enviar datos de ventas y stock de manera asíncrona al servidor

de estadísticas. MOM garantiza que los datos se envíen de manera confiable sin pérdida.

○  API REST: Para que los representantes externos puedan acceder al sistema desde

ubicaciones remotas para consultar precios y realizar compras al por mayor. Es sencillo y
es soportado por muchos clientes.

2.  Implementación de cada tecnología:

gRPC: Si se elige, se debe definir el archivo .proto con los servicios necesarios, por ejemplo, para
enviar datos de ventas.

service VentasService {

  rpc ObtenerVentas (ConsultaVentas) returns (Ventas);

  rpc EnviarEstadisticas (Estadistica) returns (Respuesta);

}

8

○
○  Sockets: Se usa un protocolo basado en TCP, donde las sucursales envían solicitudes y

la casa matriz responde. Diagrama del protocolo:

■  Solicitud (consulta de stock): { "accion": "consulta_stock",

"producto_id": 1 }

■  Respuesta: { "stock_disponible": 150 }

○  MOM (RabbitMQ):

■  Exchange: Tipo direct para enviar los mensajes de ventas y stock.
■  Colas: Una cola para cada tipo de dato: ventas y stock.
■  Modelo N a 1:

○

Fecha: 23/04/2025

Solución de Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Nocturno

Puntaje Máximo:     100 Puntos

Página 9 de 9

○  Todas las sucursales y los representantes externos envían datos de ventas y stock al

○  Son dos modelos N a 1 , uno para ventas y otro para stock.

middleware y este luego al servidor administrativo.

Formato de los mensajes (JSON):

{ "ventas": 100, "stock_disponible": 500 }

■
○  API REST:

■  GET /productos: Devuelve la lista de congelados, sus distintos sabores y sus

precios al por mayor.

■  POST /compras: Envía la compra de congelados por parte de los representantes

externos.
■  Respuesta:

1.  200 OK: La solicitud ha tenido éxito.
2.  400 Bad Request: La solicitud no se pudo entender debido a una sintaxis

incorrecta.

3.  404 Not Found: El recurso solicitado no se ha encontrado en el servidor.
4.  500 Internal Server Error: Ha ocurrido un error inesperado en el servidor

3.  Diagrama de la solución:

al procesar la solicitud.

○  Casa matriz: Actúa como servidor de consultas de stock y servidor de estadísticas.
○  Sucursales: Clientes que consultan stock y envían datos de ventas a través de Sockets o

MOM.

○  Representantes externos: Clientes que interactúan con la API REST para realizar

compras.
4.  Componentes del sistema:

○  Clientes y servidores:

■  Casa matriz: Servidor en el caso de API REST y Sockets.
■  Sucursales: Clientes al consultar stock y enviar ventas.
■  Representantes externos: Clientes al interactuar con la API REST.

○  Productores y consumidores:

■  Casa matriz: Productor de mensajes de stock.
■  Servidor de estadísticas: Consumidor de datos de ventas y stock.

9

