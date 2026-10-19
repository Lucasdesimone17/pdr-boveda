| Fecha:      | 16/07/2024 |        |     |     |     |     | Duración: |     | 2 horas      |     |
| ----------- | ---------- | ------ | --- | --- | --- | --- | --------- | --- | ------------ | --- |
| Evaluación: |            | Examen |     |     |     |     | Uso       | de  | Calculadora: | NO  |
Página 1 de 2
| Materia:  | Programación |       |             | de redes |     |               | Uso     | de  | Material: | NO         |
| --------- | ------------ | ----- | ----------- | -------- | --- | ------------- | ------- | --- | --------- | ---------- |
| Turno:    | Nocturno     |       |             |          |     |               | Puntaje |     | Máximo:   | 100 Puntos |
| Ejercicio | 1 (10        | ptos) |             |          |     |               |         |     |           |            |
| A.        | Explique     | las   | desventajas |          | que | tiene Sockets | sobre   |     | API REST  |            |
B. Explique las ventajas que tiene MOM sobre API REST. Incluya un ejemplo donde sea mejor usar
|           | MOM        | que     | API REST |     |           |         |              |     |     |     |
| --------- | ---------- | ------- | -------- | --- | --------- | ------- | ------------ | --- | --- | --- |
| Ejercicio | 2 (        | 15 pts) |          |     |           |         |              |     |     |     |
| Para cada | afirmación |         | indique  |     | Verdadero | o Falso | y justifique |     |     |     |
A. La funcionalidad de streaming de gRPC sirve principalmente para streaming de vídeo
B. La principal ventaja de usar la clase Task sobre Thread es que evita condiciones de carrera y
|     | posibles | deadlocks |     |     |     |     |     |     |     |     |
| --- | -------- | --------- | --- | --- | --- | --- | --- | --- | --- | --- |
C. Utilizando RabbitMQ: si ocurre un error al procesar el mensaje en el consumidor, el productor del
|           | mensaje | no    | se suele | enterar. |     |     |     |     |     |     |
| --------- | ------- | ----- | -------- | -------- | --- | --- | --- | --- | --- | --- |
| Ejercicio | 3 (35   | ptos) |          |          |     |     |     |     |     |     |
Implemente un servidor que pueda manejar varias conexiones en simultáneo y que además cumpla los
| siguientes | requisitos: |            |     |       |     |          |          |     |            |     |
| ---------- | ----------- | ---------- | --- | ----- | --- | -------- | -------- | --- | ---------- | --- |
| 1.         | Aceptar     | conexiones |     | hasta | un  | máximo N | (elegido | por | el alumno) |     |
2. Cada cliente va a tener asignada la fecha y hora en que se conectó.
3. Cuando un cliente se conecta el servidor le envía un mensaje con la fecha y hora en que se
conectó.
| 4.  | Se debe | verificar |     | desconexiones |     |     |     |     |     |     |
| --- | ------- | --------- | --- | ------------- | --- | --- | --- | --- | --- | --- |
Nota: Se puede utilizar tanto TcpClient/TcpListener como Sockets y se puede utilizar tanto Threads
como Tasks
| El siguiente                       | código         |     | les puede     |     | ayudar:                 |     |            |     |             |     |
| ---------------------------------- | -------------- | --- | ------------- | --- | ----------------------- | --- | ---------- | --- | ----------- | --- |
| DateTime                           | dt             | =   | DateTime.Now; |     |                         |     |            |     |             |     |
| string                             | datetimeString |     |               | =   | dt.ToString("yyyy-MM-dd |     |            |     | HH:mm:ss"); |     |
| Console.WriteLine(datetimeString); |                |     |               |     |                         | //  | 2024-07-01 |     | 15:00:00    |     |

| Fecha:      | 16/07/2024 |        |     |     |     |     |     | Duración: | 2 horas      |     |
| ----------- | ---------- | ------ | --- | --- | --- | --- | --- | --------- | ------------ | --- |
| Evaluación: |            | Examen |     |     |     |     |     | Uso de    | Calculadora: | NO  |
Página 2 de 2
| Materia:  |          | Programación |       | de redes |     |     |     | Uso de  | Material: | NO         |
| --------- | -------- | ------------ | ----- | -------- | --- | --- | --- | ------- | --------- | ---------- |
| Turno:    | Nocturno |              |       |          |     |     |     | Puntaje | Máximo:   | 100 Puntos |
| Ejercicio |          | 4 (40        | ptos) |          |     |     |     |         |           |            |
Usted es contratado por una empresa de venta de ropa que ha empezado a crecer abriendo varias
| sucursales |     | en el | interior | del país. |     |     |     |     |     |     |
| ---------- | --- | ----- | -------- | --------- | --- | --- | --- | --- | --- | --- |
1. Se necesita un canal de comunicación para enviar información entre la casa matriz y las
sucursales. Tanto las sucursales deben poder consultar información a la casa matriz, como la
|     | casa | matriz | necesita | consultar |     | información |     | a las sucursales. |     |     |
| --- | ---- | ------ | -------- | --------- | --- | ----------- | --- | ----------------- | --- | --- |
2. En algunas ciudades y pueblos más chicos no hay sucursales pero sí un representante externo
que se encarga de las ventas. Necesita poder acceder al sistema para saber el valor de la ropa y
|     | poder | efectuar |     | las compras | al  | por mayor. |     |     |     |     |
| --- | ----- | -------- | --- | ----------- | --- | ---------- | --- | --- | --- | --- |
3. La casa matriz quiere enviar todos los datos de ventas y stock al servidor de estadísticas. Este
servidor recibe los datos, los almacena y hace varios cálculos para mostrar estadísticas que se
|     | espera | estén | actualizadas |     | cada | 10  | minutos. |     |     |     |
| --- | ------ | ----- | ------------ | --- | ---- | --- | -------- | --- | --- | --- |
Para cumplir estos requerimientos se requiere utilizar 3 de las siguientes tecnologías sockets, MOM,
| gRPC,      | API        | REST.       |             |         |                 |            |     |                |     |     |
| ---------- | ---------- | ----------- | ----------- | ------- | --------------- | ---------- | --- | -------------- | --- | --- |
| Quedando   |            | bajo        | su criterio | cómo    | y cuáles        | utilizar.  |     |                |     |     |
| Justifique |            | su elección |             | y para  | las tecnologías |            | que | elija indique: |     |     |
|            | ● gprc:    | Especificar |             | archivo | .proto          |            |     |                |     |     |
|            | ● Sockets: |             | Diagrama    | del     | protocolo       | a utilizar |     |                |     |     |
● MOM: Especificar colas, tipo de exchanges y los mensajes que se envían (ej. contenido del
|     | JSON | o   | similar) |     |     |     |     |     |     |     |
| --- | ---- | --- | -------- | --- | --- | --- | --- | --- | --- | --- |
● API REST: Defina los endpoints (especificar datos a enviar y posibles respuestas con sus status
code)
Diagrame la solución mostrando los canales de comunicación y los componentes en ambos extremos
de la comunicación.
Para cada comunicación indique quién es el cliente/servidor o productor/consumidor (según
corresponda)
Pueden hacer suposiciones de que datos se precisan enviar y recibir.
| Pueden | usar | más | de una | tecnología |     | para un | mismo | componente. |     |     |
| ------ | ---- | --- | ------ | ---------- | --- | ------- | ----- | ----------- | --- | --- |