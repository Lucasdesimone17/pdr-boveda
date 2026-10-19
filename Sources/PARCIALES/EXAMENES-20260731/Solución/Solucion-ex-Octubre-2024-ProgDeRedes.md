| Fecha:   | 09/10/2024 |           |     |     |     |     | Duración: | 2 horas      |     |
| -------- | ---------- | --------- | --- | --- | --- | --- | --------- | ------------ | --- |
| Solución |            | de Examen |     |     |     |     | Uso de    | Calculadora: | NO  |
Página 1 de 5
| Materia:  | Programación |          |         | de  | redes |               | Uso de  | Material: | NO         |
| --------- | ------------ | -------- | ------- | --- | ----- | ------------- | ------- | --------- | ---------- |
| Turno:    | Nocturno     |          |         |     |       |               | Puntaje | Máximo:   | 100 Puntos |
| Ejercicio |              | 1 (10    | puntos) |     |       |               |         |           |            |
| Pregunta  | 1:           | Ventajas | de      | API | REST  | sobre Sockets |         |           |            |
● Simplicidad y estandarización: API REST usa HTTP, un protocolo bien conocido y ampliamente
soportado en diversas plataformas, lo que facilita la integración entre sistemas heterogéneos.
|     | Sockets, |     | en cambio, | requieren |     | un manejo | más manual | y bajo | nivel. |
| --- | -------- | --- | ---------- | --------- | --- | --------- | ---------- | ------ | ------ |
● Interoperabilidad: REST es fácilmente compatible con múltiples lenguajes y tecnologías,
mientras que Sockets suelen estar más ligados a una implementación específica.
● Escalabilidad: REST está bien diseñado para sistemas escalables en la nube, ya que permite
caché y balanceo de carga. Los Sockets, al mantener conexiones persistentes, requieren más
|     | recursos |     | y complican |     | la escalabilidad. |     |     |     |     |
| --- | -------- | --- | ----------- | --- | ----------------- | --- | --- | --- | --- |
● Facilidad de depuración y testing: Las APIs REST pueden ser probadas y depuradas con
herramientas comunes como Postman o curl. El debug de Sockets suele ser más complejo
|          | debido | a        | su bajo | nivel | de abstracción. |      |     |     |     |
| -------- | ------ | -------- | ------- | ----- | --------------- | ---- | --- | --- | --- |
| Pregunta | 2:     | Ventajas | de      | MOM   | sobre           | gRPC |     |     |     |
● Desacoplamiento y asincronía: Middleware de Colas de Mensajes (MOM) desacopla el productor
y el consumidor, permitiendo que los mensajes se procesan en tiempos distintos, sin necesidad
|     | de  | mantener | una | conexión |     | persistente | entre ambos. |     |     |
| --- | --- | -------- | --- | -------- | --- | ----------- | ------------ | --- | --- |
● Fiabilidad y persistencia: En un MOM como RabbitMQ, los mensajes se pueden almacenar en
colas y no se perderán incluso si el consumidor no está disponible en ese momento, a diferencia
de gRPC, que requiere que ambos extremos estén activos durante la transmisión.
● Escalabilidad: MOM puede manejar grandes volúmenes de mensajes asíncronamente y distribuir
|     | la carga |     | entre múltiples |     | consumidores. |     |     |     |     |
| --- | -------- | --- | --------------- | --- | ------------- | --- | --- | --- | --- |
Ejemplo: En un sistema de procesamiento de pedidos, si el procesamiento del pedido lleva tiempo, es
mejor usar un MOM para enviar el pedido a una cola y procesarlo más tarde. gRPC es más adecuado
cuando se necesita respuesta inmediata, como en una consulta a un servicio remoto.

| Fecha:   | 09/10/2024 |           |     |     |     |     | Duración: |     | 2 horas      |     |
| -------- | ---------- | --------- | --- | --- | --- | --- | --------- | --- | ------------ | --- |
| Solución |            | de Examen |     |     |     |     | Uso       | de  | Calculadora: | NO  |
Página 2 de 5
| Materia:  | Programación |       |         | de redes |     |     | Uso     | de  | Material: | NO         |
| --------- | ------------ | ----- | ------- | -------- | --- | --- | ------- | --- | --------- | ---------- |
| Turno:    | Nocturno     |       |         |          |     |     | Puntaje |     | Máximo:   | 100 Puntos |
| Ejercicio |              | 2 (15 | puntos) |          |     |     |         |     |           |            |
Afirmación 1: "Mutex es más potente que Monitores pues me permite sincronizar procesos a nivel del
SO"
● Verdadero. Un Mutex puede sincronizar entre procesos diferentes, mientras que un Monitor solo
|     | sincroniza |     | hilos | dentro del | mismo | proceso. |     |     |     |     |
| --- | ---------- | --- | ----- | ---------- | ----- | -------- | --- | --- | --- | --- |
Afirmación 2: "El manejo de hilos (Threads) en .NET es más eficiente que el uso de Task en situaciones
de alta carga de trabajo debido a la menor sobrecarga que generan"
● Falso. El manejo de Tasks es generalmente más eficiente que Threads en escenarios de alta
concurrencia.Tasks utilizan un pool de hilos y son más ligeros, mientras que los Threads crean
|     | nuevos | hilos | para | cada tarea, | lo  | que genera | más | sobrecarga. |     |     |
| --- | ------ | ----- | ---- | ----------- | --- | ---------- | --- | ----------- | --- | --- |
Afirmación 3: "Cuando un mensaje se envía a una cola de RabbitMQ, el mensaje se pierde si no hay
| consumidores |     | disponibles |     | para procesarlo" |     |     |     |     |     |     |
| ------------ | --- | ----------- | --- | ---------------- | --- | --- | --- | --- | --- | --- |
● Falso. En RabbitMQ, los mensajes pueden ser persistidos en la cola hasta que un consumidor
esté disponible, siempre que se haya configurado correctamente la persistencia en la cola.
| Ejercicio |             | 3 (35         | puntos) |     |     |     |     |     |     |     |
| --------- | ----------- | ------------- | ------- | --- | --- | --- | --- | --- | --- | --- |
| Esquema   | de          | solución:     |         |     |     |     |     |     |     |     |
|           | 1. Servidor | multicliente: |         |     |     |     |     |     |     |     |
○ El servidor debe usar TcpListener o Socket para escuchar conexiones entrantes en un
puerto. Cuando se recibe una nueva conexión, se asigna un hilo o tarea (Thread/Task)
|     |     | para | manejar | esa conexión |     | de manera |     | individual. |     |     |
| --- | --- | ---- | ------- | ------------ | --- | --------- | --- | ----------- | --- | --- |
○ El servidor limita el número máximo de conexiones a N (definido por el alumno).
|     | 2. Asignación |     | de  | ID autoincremental: |     |     |     |     |     |     |
| --- | ------------- | --- | --- | ------------------- | --- | --- | --- | --- | --- | --- |
○ Utiliza una variable global compartida para manejar el ID autoincremental. Cada vez que
|     |          | un  | cliente | se conecta, | se  | le asigna | un  | nuevo | ID único. |     |
| --- | -------- | --- | ------- | ----------- | --- | --------- | --- | ----- | --------- | --- |
|     | 3. Envío | del | ID al   | cliente:    |     |           |     |       |           |     |
○ Después de aceptar la conexión, el servidor envía un mensaje al cliente con su ID
asignado.
|     | 4. Manejo | de  | desconexiones: |     |     |     |     |     |     |     |
| --- | --------- | --- | -------------- | --- | --- | --- | --- | --- | --- | --- |
○ El servidor debe detectar cuando un cliente se desconecta y liberar los recursos
asociados. Esto puede lograrse monitorizando la transmisión de datos o utilizando
|     |     | excepciones |     | que se | generen | al  | fallar la | conexión. |     |     |
| --- | --- | ----------- | --- | ------ | ------- | --- | --------- | --------- | --- | --- |

| Fecha:   |     | 09/10/2024 |        |     |     |     | Duración: | 2 horas      |     |
| -------- | --- | ---------- | ------ | --- | --- | --- | --------- | ------------ | --- |
| Solución |     | de         | Examen |     |     |     | Uso de    | Calculadora: | NO  |
Página 3 de 5
| Materia:    |     | Programación |          |        | de redes |                            | Uso de  | Material: | NO         |
| ----------- | --- | ------------ | -------- | ------ | -------- | -------------------------- | ------- | --------- | ---------- |
| Turno:      |     | Nocturno     |          |        |          |                            | Puntaje | Máximo:   | 100 Puntos |
| Ejemplo     |     | de código    |          | básico | (C#):    |                            |         |           |            |
| TcpListener |     |              | listener |        | = new    | TcpListener(IPAddress.Any, |         |           | 8080);     |
listener.Start();
| bool |     | salir | =   | false; |     |     |     |     |     |
| ---- | --- | ----- | --- | ------ | --- | --- | --- | --- | --- |
| int  | i   | = 1;  |     |        |     |     |     |     |     |
while (!salir)
{
|     | TcpClient   |          |     | client | = await              | listener.AcceptTcpClientAsync(); |       |     |     |
| --- | ----------- | -------- | --- | ------ | -------------------- | -------------------------------- | ----- | --- | --- |
|     | int         | clientId |     | =      | i;                   |                                  |       |     |     |
|     | Task.Run(() |          |     | =>     | HandleClient(client, |                                  | id)); |     |     |
i++;
}
static async Task HandleClient(TcpClient client, int clientId) {
|     |     | NetworkStream |         |                              | stream                          | = client.GetStream();                  |           |                  |               |
| --- | --- | ------------- | ------- | ---------------------------- | ------------------------------- | -------------------------------------- | --------- | ---------------- | ------------- |
|     |     | byte[]        |         | message                      | = Encoding.UTF8.GetBytes($"Your |                                        |           |                  | ID is {id}"); |
|     |     | byte[]        |         | msglength                    | =                               | BitConverter.GetBytes(message.Length); |           |                  |               |
|     |     | await         |         | stream.WriteAsync(msglength, |                                 |                                        |           | 0, 4);           |               |
|     |     | await         |         | stream.WriteAsync(message,   |                                 |                                        | 0,        | message.Length); |               |
|     |     | //            | Manejar |                              | desconexión                     | y liberar                              | recursos. |                  |               |
});
}

| Fecha:   | 09/10/2024 |        |     |     |     |     | Duración: | 2 horas      |     |
| -------- | ---------- | ------ | --- | --- | --- | --- | --------- | ------------ | --- |
| Solución | de         | Examen |     |     |     |     | Uso de    | Calculadora: | NO  |
Página 4 de 5
| Materia:  | Programación |           | de  | redes |     |     | Uso de  | Material: | NO         |
| --------- | ------------ | --------- | --- | ----- | --- | --- | ------- | --------- | ---------- |
| Turno:    | Nocturno     |           |     |       |     |     | Puntaje | Máximo:   | 100 Puntos |
| Ejercicio | 4 (40        | puntos)   |     |       |     |     |         |           |            |
| Solución  | sugerida:    |           |     |       |     |     |         |           |            |
| 1.        | Tecnologías  | elegidas: |     |       |     |     |         |           |            |
○ Sockets: Para la comunicación bidireccional entre la casa matriz y las sucursales, ya que
|     |     | permite | consultas | rápidas | y   | de bajo | nivel. |     |     |
| --- | --- | ------- | --------- | ------- | --- | ------- | ------ | --- | --- |
○ MOM (RabbitMQ): Para enviar datos de ventas y stock de manera asíncrona al servidor
de estadísticas. MOM garantiza que los datos se envíen de manera confiable sin pérdida.
○ API REST: Para que los representantes externos puedan acceder al sistema desde
ubicaciones remotas para consultar precios y realizar compras al por mayor. Es sencillo y
|     |                | es soportado | por     | muchos      | clientes. |     |     |     |     |
| --- | -------------- | ------------ | ------- | ----------- | --------- | --- | --- | --- | --- |
| 2.  | Implementación |              | de cada | tecnología: |           |     |     |     |     |
gRPC: Si se elige, se debe definir el archivo .proto con los servicios necesarios, por ejemplo, para
| enviar  | datos de           | ventas. |                  |               |     |     |         |              |     |
| ------- | ------------------ | ------- | ---------------- | ------------- | --- | --- | ------- | ------------ | --- |
| service | VentasService      |         |                  | {             |     |     |         |              |     |
| rpc     | ObtenerVentas      |         | (ConsultaVentas) |               |     |     | returns | (Ventas);    |     |
| rpc     | EnviarEstadisticas |         |                  | (Estadistica) |     |     | returns | (Respuesta); |     |
}
○
○ Sockets: Se usa un protocolo basado en TCP, donde las sucursales envían solicitudes y
|     |     | la casa | matriz | responde. | Diagrama |     | del protocolo: |     |     |
| --- | --- | ------- | ------ | --------- | -------- | --- | -------------- | --- | --- |
■ Solicitud (consulta de stock): { "accion": "consulta_stock",
|     |     |                 | "producto_id": |     |                     | 1 } |     |     |     |
| --- | --- | --------------- | -------------- | --- | ------------------- | --- | --- | --- | --- |
|     |     | ■               | Respuesta:     |     |                     |     |     |     |     |
|     |     |                 |                | {   | "stock_disponible": |     |     | 150 | }   |
|     | ○   | MOM (RabbitMQ): |                |     |                     |     |     |     |     |
■ Exchange: Tipo direct para enviar los mensajes de ventas y stock.
|     |     | ■   | Colas: | Una cola | para | cada | tipo de dato: | ventas | y stock. |
| --- | --- | --- | ------ | -------- | ---- | ---- | ------------- | ------ | -------- |
|     |     | ■   | Modelo | N a 1:   |      |      |               |        |          |
○

| Fecha:   | 09/10/2024 |        |     |     | Duración: | 2 horas         |     |     |
| -------- | ---------- | ------ | --- | --- | --------- | --------------- | --- | --- |
| Solución | de         | Examen |     |     | Uso       | de Calculadora: |     | NO  |
Página 5 de 5
| Materia: | Programación |     | de redes |     | Uso     | de Material: |     | NO         |
| -------- | ------------ | --- | -------- | --- | ------- | ------------ | --- | ---------- |
| Turno:   | Nocturno     |     |          |     | Puntaje | Máximo:      |     | 100 Puntos |
○ Todas las sucursales y los representantes externos envían datos de ventas y stock al
|             |        |          |                     | middleware  | y este luego | al servidor | administrativo. |     |
| ----------- | ------ | -------- | ------------------- | ----------- | ------------ | ----------- | --------------- | --- |
|             | ○      | Son dos  | modelos             | N a 1 , uno | para ventas  | y otro para | stock.          |     |
| Formato     | de los | mensajes | (JSON):             |             |              |             |                 |     |
| { "ventas": |        | 100,     | "stock_disponible": |             | 500          | }           |                 |     |
■
○ API REST:
■ GET /productos: Devuelve la lista de empanadas, sus distintos sabores y sus
|     |     |     | precios al | por mayor. |     |     |     |     |
| --- | --- | --- | ---------- | ---------- | --- | --- | --- | --- |
■ POST /compras: Envía la compra de empanadas por parte de los representantes
externos.
|     |     | ■   | Respuesta: |        |                     |        |     |     |
| --- | --- | --- | ---------- | ------ | ------------------- | ------ | --- | --- |
|     |     |     | 1. 200     | OK: La | solicitud ha tenido | éxito. |     |     |
2. 400 Bad Request: La solicitud no se pudo entender debido a una sintaxis
incorrecta.
3. 404 Not Found: El recurso solicitado no se ha encontrado en el servidor.
4. 500 Internal Server Error: Ha ocurrido un error inesperado en el servidor
|     |          |       | al        | procesar la | solicitud. |     |     |     |
| --- | -------- | ----- | --------- | ----------- | ---------- | --- | --- | --- |
| 3.  | Diagrama | de la | solución: |             |            |     |     |     |
○ Casa matriz: Actúa como servidor de consultas de stock y servidor de estadísticas.
○ Sucursales: Clientes que consultan stock y envían datos de ventas a través de Sockets o
MOM.
○ Representantes externos: Clientes que interactúan con la API REST para realizar
compras.
| 4.  | Componentes |          | del sistema:  |          |              |                |         |          |
| --- | ----------- | -------- | ------------- | -------- | ------------ | -------------- | ------- | -------- |
|     | ○           | Clientes | y servidores: |          |              |                |         |          |
|     |             | ■        | Casa matriz:  | Servidor | en el caso   | de API REST    | y       | Sockets. |
|     |             | ■        | Sucursales:   | Clientes | al consultar | stock y enviar | ventas. |          |
■ Representantes externos: Clientes al interactuar con la API REST.
|     | ○   | Productores | y consumidores: |           |             |           |     |     |
| --- | --- | ----------- | --------------- | --------- | ----------- | --------- | --- | --- |
|     |     | ■           | Casa matriz:    | Productor | de mensajes | de stock. |     |     |
■ Servidor de estadísticas: Consumidor de datos de ventas y stock.