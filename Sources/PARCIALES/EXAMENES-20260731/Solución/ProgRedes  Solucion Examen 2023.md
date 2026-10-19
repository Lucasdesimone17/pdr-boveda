| Evaluación | Examen                     | Grupo |     | Fecha | 13-02-23 |
| ---------- | -------------------------- | ----- | --- | ----- | -------- |
| Materia    | Programación de Redes      |       |     |       |          |
| Carrera    | Ingeniería de Sistemas, ID |       |     |       |          |
Condiciones 1 a 100 Puntos,2 horas desde que se lee la letra,sin material
Ejercicio 1 (30 puntos)
Dado el siguiente codigo (Asuma que no hay errores de sintaxis y que el codigo ejecuta correctamente):
| class | Program |     |     |     |     |
| ----- | ------- | --- | --- | --- | --- |
{
| static | void | Main(string[] args) |     |     |     |
| ------ | ---- | ------------------- | --- | --- | --- |
{
var factory = new ConnectionFactory() { HostName = "localhost" };
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
{
|     | channel.QueueDeclare(queue: |     | "hello", |     |     |
| --- | --------------------------- | --- | -------- | --- | --- |
durable: false,
|     |     | exclusive: false, |           |     |     |
| --- | --- | ----------------- | --------- | --- | --- |
|     |     | autoDelete:       | false,    |     |     |
|     |     | arguments: null); |           |     |     |
|     | for | (var i = 0; i <   | 100; i++) |     |     |
{
var chan = channel;
new Thread(() => SendMessage(chan,i)).Start();
Console.WriteLine("Mensaje a enviar valor -> ”+ value);
}
}
}
| private | static | void SendMessage(IModel channel, |     |     | int value) |
| ------- | ------ | -------------------------------- | --- | --- | ---------- |
{
|     | const string                              | message =          | "Mensaje:"; |     |                 |
| --- | ----------------------------------------- | ------------------ | ----------- | --- | --------------- |
|     | var body = Encoding.UTF8.GetBytes(message |                    |             |     | + " " + value); |
|     | channel.BasicPublish(exchange:            |                    | "",         |     |                 |
|     | routingKey:                               | "hello",           |             |     |                 |
|     | basicProperties:                          | null, body: body); |             |     |                 |
Console.WriteLine("Mensaje enviado: " + message + " " + value);
}
}
Responda:

● Cual es el output del mismo
● Que problemas presenta el mismo
● Describa o detalle la solucion para el problema presentado
RESPUESTAS
● Eloutputesindeterminado,yaquecuandounhiloestahaciendounalecturadelavariablevalue,
el for puede incrementar este valor y causar que haya una inconsistencia entreloqueseloguea
en el for y en el hilo.
● El problema es el descrito anteriormente, hay dos problemas asociados a la solucion:
○ La lectura de la variable value esta en el contexto del for y al pasarla al thread puede
darse una inconsistencia en su lectura
● Basta con asociar elvalordevalueaunavariablelocalalforyconesoprotegemoselscopedela
misma
Ejercicio 2 (20 puntos)
Explique qué es un mecanismo de comunicación basado en RPC y explique como funciona en particular
gRPC.
● Que son los Protocol Buffers?
● Cuales son las ventajas de utilizar una tecnologia de este tipo en un mecanismo de intercambio
de mensajes?
RESPUESTAS
Los mecanismos basados en RPC apuntan a realizar invocaciones remotas escondiendo toda la
complejidad del intercambio de mensajes utilizandounmiddlewareparaestefin.EnRPC,sebuscapoder
invocar unafuncion(oenelcasodeORB,unmetododeunobjeto)deformatransparente,estoes,dando
la apariencia que se esta ejecutando todo local.
● Protocol Buffers es una tecnologia de abstraccion que permite utilizar un mecanismo de
representacion intermedia de datos, que es independiente del lenguaje de programacion
utilizado
● Su principalventajaesque,alserunaformacanonica,permiteimplementarparsers/decodersen
diferentes lenguajes y lograr independencia del lenguaje quecorreenlamaquinahost.Dichode
otra forma, permite interconectar apps hechas en diferentes lenguajes de programacion que
tengan un runtime de gRPC
Ejercicio 3 (25 puntos)
Explique en sus palabras :
1. Cuando es necesario utilizar asincronia
2. Explique queesunSemaforoyqueesunaMonitoryenquecasousariaunouotrodependiendo
de las necesidades del codigo
3. Es posible implementar una aplicación que sea asincrónica y paralela al mismo tiempo?
RESPUESTAS

1. Cuando nuestra aplicación va a ejecutar operaciones que pueden demorar en el tiempo y que
van a resolver en un entorno externo a la misma..
2. Un Semaforo es un mecanismo de mutuoexclusion que define una estructura que nos permite
llevar la cuenta de cuantos slots tenemos disponibles en un recurso compartido. Nos permite
tener un control de cuantos hilos pueden acceder al mismo en un momento dado. Un Monitor
sirve para crear una zona de mutuoexclusion y asegurarquesolamenteseaccedaalamismade
a un hilo a la vez, pero ademas permite enviar señales entre threads utilizando Wait()yPulse().
De esta forma, podemos tener codigo queespereaqueunhiloenvieunaseñalsinhacerunbusy
wait o similar.
3. Si, ya que podemos querer procesar muchas cosas en paralelo, y cada uno de esos
procesamientos puede implicar la resolución de operaciones por fuera del scope denuestraapp
(lo cual nos puede obligar a esperar estos resultados), por lo que es perfectamente posible
combinar ambos conceptos
Ejercicio 4 (25 puntos)
Tomando como base las tecnologías vistas en el curso, y teniendo en cuenta las siguientes premisas:
1. Se necesita construir una app que reciba datos desde varios clientes a la vez.
2. Estos clientes envían muchos paquetes de tamaño pequeño y el server debe acumular una
cantidad X de los mismos para luego procesarlos a la vez.
3. Cada un intervalo Y de tiempo, el servidordebepublicarestadísticasdecualfueelresultadodel
procesamiento en ese periodo de tiempo, en uncanaldecomunicacionesSEPARADOdelosque
se establecen con los clientes.
Discuta cuáles son las mejores opciones para el caso descrito anteriormente,desdeelpuntodevistade
los canales de comunicación, asi como también desde el punto de vista de la arquitectura del servidor.
RESPUESTAS
Para la comunicación con los clientes, conviene establecerunaconexiónquepermitahacerstreamingde
datos, ya que nos van a enviar permanentemente data. Debido a esto, un canal basado en GRPC o
sockets parece ser la mejor opción, para poder mantener la conexión establecida. Ademas, se debe
implementar un server que tenga un hilo o Task para atender a cada cliente. Una vez que en ese hilo o
task se acumulan X paquetes, se pueden lanzar X hilos paraprocesarlosmismos(teniendoencuentade
no sobrepasar el número máximo de hilos para una app)
Respecto al canal donde se publican las estadísticas, podría perfectamente implementarse una
comunicación basada en colas de mensaje, para poder enviar de forma asincrónicaydesacopladaestos
mensajes sin interrumpir el procesamiento de cada cliente.