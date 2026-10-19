| Evaluación | Examen                     | Grupo |     | Fecha | 13-02-23 |
| ---------- | -------------------------- | ----- | --- | ----- | -------- |
| Materia    | Programación de Redes      |       |     |       |          |
| Carrera    | Ingeniería de Sistemas, ID |       |     |       |          |
Condiciones 1 a 100 Puntos,2 horas desde que se lee la letra,sin material
Ejercicio 1 (30 puntos)
Dado el siguiente código (Asuma que no hay errores de sintaxis y que el código ejecuta correctamente):
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
● Describa o detalle la solución para el problema presentado
Ejercicio 2 (20 puntos)
Explique qué es un mecanismo de comunicación basado en RPC y explique como funciona en particular
gRPC.
● Que son los Protocol Buffers?
● Cuales son las ventajas de utilizar una tecnología de este tipo en un mecanismo de intercambio
de mensajes?
Ejercicio 3 (25 puntos)
Explique en sus palabras :
1. Cuando es necesario utilizar asincronía
2. Explique queesunSemáforoyqueesunaMonitoryenquecasousaríaunouotrodependiendo
de las necesidades del código
3. Es posible implementar una aplicación que sea asincrónica y paralela al mismo tiempo?
Ejercicio 4 (25 puntos)
Tomando como base las tecnologías vistas en el curso, y teniendo en cuenta las siguientes premisas:
1. Se necesita construir una app que reciba datos desde varios clientes a la vez.
2. Estos clientes envían muchos paquetes de tamaño pequeño y el server debe acumular una
cantidad X de los mismos para luego procesarlos a la vez.
3. Cada un intervalo Y de tiempo, el servidordebepublicarestadísticasdecualfueelresultadodel
procesamiento en ese periodo de tiempo, en uncanaldecomunicacionesSEPARADOdelosque
se establecen con los clientes.
Discuta cuáles son las mejores opciones para el caso descrito anteriormente,desdeelpuntodevistade
los canales de comunicación, así como también desde el punto de vista de la arquitectura del servidor.