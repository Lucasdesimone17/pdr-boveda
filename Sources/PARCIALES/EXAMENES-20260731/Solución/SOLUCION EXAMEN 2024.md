Fecha: 16/07/2024 Duración: 2 horas
Evaluación: Solución a Examen Uso de Calculadora: NO
Página 1 de 3
Materia: Programación de redes Uso de Material: NO
Turno: Nocturno Puntaje Máximo: 100 Puntos
Ejercicio 1 ( 10 pts)
A. Explique las desventajas que tiene Sockets sobre API REST
Es más compleja de implementar, esta menos estandarizada, es menos segura, menos
escalable, menos compatible y más difícil de monitorear y testear
B. Explique las ventajas que tiene MOM sobre API REST. Incluya un ejemplo donde sea
mejor usar MOM que API REST
Aplicaciones que no necesitan sincronía para funcionar. Por ejemplo el servidor de
respaldos que se enciende en horas de la madrugada.
Ejercicio 2 ( 15 pts)
Para cada afirmación indique Verdadero o Falso y justifique
A. La funcionalidad de streaming de gRPC sirve principalmente para streaming de vídeo
False. gRPC streaming permite mandar cualquier tipo de datos que se puede definir
usando el lenguaje de protocol buffer (enteros, texto, listas, booleanos etc). Está pensado
para enviar varios datos sin tener que esperar una respuesta del cliente/servidor cada vez.
gRPC usa HTTP 2.0 que en la mayoría de los casos utiliza TCP, lo cual no es del todo
ideal si queres ver un vídeo en vivo donde te puede no importar perder algún paquete. Si
bien los protocol buffer soportan mensajes con campos de tipo byte (que se podrían usar
para vídeo), no es su principal objetivo ni uso.
B. La principal ventaja de usar la clase Task sobre Thread es que evita condiciones de carrera
y posibles deadlocks
Falso. Utilizar task no nos soluciona los problemas de condiciones de carrera o deadlocks.
Las tasks se ejecutan en la Threadpool que es un conjunto de hilos y pueden existir casos
donde dos tareas (o hilos de la threadpool) lean y modifiquen los mismos datos.
Algunas ventajas de Task sobre Thread es la facilidad de uso, la posibilidad de devolver
valores, poder ejecutar funciones concurrentemente (aprovechando la CPU) sin necesidad
de crear y administrar los hilos (lo cual puede ser costoso)
C. Utilizando RabbitMQ: si ocurre un error al procesar el mensaje en el consumidor, el
productor del mensaje no se suele enterar.
Verdadero. En general, el productor envía el mensaje a RabbitMQ el cual se encarga del
enrutamiento del mensaje a los consumidores. Cada consumidor lo va a procesar cuando
pueda, pudiendo el productor no estar prendido cuando esto suceda. Al ser una
comunicación asincrónica y desacoplada, el consumidor no tiene manera de avisarle al
productor que el mensaje falló.

| Fecha: 16/07/2024 |          |          |     | Duración: | 2 horas      |     |
| ----------------- | -------- | -------- | --- | --------- | ------------ | --- |
| Evaluación:       | Solución | a Examen |     | Uso de    | Calculadora: | NO  |
Página 2 de 3
| Materia:        | Programación | de redes |     | Uso de  | Material: | NO         |
| --------------- | ------------ | -------- | --- | ------- | --------- | ---------- |
| Turno: Nocturno |              |          |     | Puntaje | Máximo:   | 100 Puntos |
| Ejercicio       | 3 (35 ptos)  |          |     |         |           |            |
Solución
| Console.WriteLine("Iniciando |     | servidor..."); |     |     |     |     |
| ---------------------------- | --- | -------------- | --- | --- | --- | --- |
IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
| TcpListener | server | = new TcpListener(endPoint); |     |     |     |     |
| ----------- | ------ | ---------------------------- | --- | --- | --- | --- |
server.Start();
| int n =                   | 10;                       |                           |     |     |     |     |
| ------------------------- | ------------------------- | ------------------------- | --- | --- | --- | --- |
| int acceptedClients       |                           | = 0;                      |     |     |     |     |
| while(acceptedClients     |                           | < n){                     |     |     |     |     |
| TcpClient                 | client =                  | server.AcceptTcpClient(); |     |     |     |     |
| Console.WriteLine("Client |                           | accepted");               |     |     |     |     |
| Task.Run(()               | => HandleClient(client)); |                           |     |     |     |     |
acceptedClients++;
}
| async | Task HandleClient(TcpClient |     | client){ |     |     |     |
| ----- | --------------------------- | --- | -------- | --- | --- | --- |
try{
string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
byte[] messageInBytes = Encoding.UTF8.GetBytes(currentDateTime);
|     | NetworkStream | stream = | client.GetStream(); |     |     |     |
| --- | ------------- | -------- | ------------------- | --- | --- | --- |
await stream.WriteAsync(messageInBytes, 0, messageInBytes.Length);
}
| catch(Exception |     | e){ |     |     |     |     |
| --------------- | --- | --- | --- | --- | --- | --- |
Console.WriteLine(e.Message);
}
finally{
client.Close();
}
}

| Fecha:      | 16/07/2024 |          |     | Duración: | 2 horas      |     |
| ----------- | ---------- | -------- | --- | --------- | ------------ | --- |
| Evaluación: | Solución   | a Examen |     | Uso de    | Calculadora: | NO  |
Página 3 de 3
| Materia:  | Programación | de redes |     | Uso de  | Material: | NO         |
| --------- | ------------ | -------- | --- | ------- | --------- | ---------- |
| Turno:    | Nocturno     |          |     | Puntaje | Máximo:   | 100 Puntos |
| Ejercicio | 4 (40        | ptos)    |     |         |           |            |
Solución
1. gRPC: Bueno para uso interno, fácil de usar (comparado a Sockets), permite hacer y
responder peticiones. Elegir algún ejemplo de información que se puede pedir, desde la
|     | casa matriz | a la sucursal, | o viceversa | y hacer | el archivo | .proto. |
| --- | ----------- | -------------- | ----------- | ------- | ---------- | ------- |
2. API REST: Facilita la integración con los sistemas de los representantes externos. Hacer
|     | Endpoints | GET /clothes | y POST /purchases |     |     |     |
| --- | --------- | ------------ | ----------------- | --- | --- | --- |
MOM: Los datos solo se tiene que enviar en un sentido sin necesidad de respuesta. Hacer un
JSON que incluya los datos de la venta, el stock y algún identificador.