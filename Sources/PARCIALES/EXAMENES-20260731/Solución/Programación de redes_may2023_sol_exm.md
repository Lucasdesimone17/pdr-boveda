| Ingeniería   | de        | Sistemas.       |              |        |     |
| ------------ | --------- | --------------- | ------------ | ------ | --- |
| Programación |           | de Redes.       |              |        |     |
| Examen       | 17        | Mayo 2023.      |              |        |     |
| 2 horas.     | Igual     | puntaje por     | pregunta.    |        |     |
| 1)           | (10 Pts)  | Dado el         | siguiente    | código |     |
| Con          | 3 salidas | posibles:       |              |        |     |
|              | a)        |                 |              | b)     | c)  |
|              | 0         |                 |              | 2      | 2   |
|              | 1         |                 |              | 3      | 2   |
|              | 2         |                 |              | 0      | 3   |
|              | 3         |                 |              | 4      | 3   |
|              | 4         |                 |              | 1      | 4   |
| Seleccione   | la        | opción correcta |              |        |     |
|              | a)        | Solo a es       | posible      |        |     |
|              | b)        | Solo b es       | posible      |        |     |
|              | c)        | a y b son       | posibles     |        |     |
|              | d)        | Todas son       | posibles     |        |     |
|              | e)        | Ninguna es      | posible      |        |     |
|              |           | Justifique      | su elección. |        |     |

| Ingeniería   |       | de Sistemas. |               |     |     |     |
| ------------ | ----- | ------------ | ------------- | --- | --- | --- |
| Programación |       | de           | Redes.        |     |     |     |
| Examen       | 17    | Mayo         | 2023.         |     |     |     |
| 2 horas.     | Igual | puntaje      | por pregunta. |     |     |     |
2) (20 Pts) Dada una operación SaveData(String text) que simula guardar datos en
|     | una | base de     | datos. Realizar | el código que      | implique   |            |
| --- | --- | ----------- | --------------- | ------------------ | ---------- | ---------- |
|     |     | a) Disparar | dos hilos.      |                    |            |            |
|     |     | b) Arbitrar | el acceso       | al método SaveData | bajo mutua | exclusión. |
3) (40 Pts) Realizar un servidor que mantenga N conexiones de diferentes clientes,
|     | verificando |            | las desconexiones. |        |      |     |
| --- | ----------- | ---------- | ------------------ | ------ | ---- | --- |
|     | Luego       | de atender | una conexión       | deberá | de : |     |
a) Cuando un cliente se conecta, el servidor le asigna un id único y se lo envía
|     |     | al cliente | conectado. |     |     |     |
| --- | --- | ---------- | ---------- | --- | --- | --- |
b) Luego de esto, el cliente puede enviar una solicitud de envío de mensaje a
|     |     | otro | cliente, enviando | el id y el string | a enviar. |     |
| --- | --- | ---- | ----------------- | ----------------- | --------- | --- |
c) Cada vez que el mensaje sea entregado a su destinatario, el servidor debe
enviar un mensaje notificando al emisor de que el mensaje fue entregado
correctamente.

| Ingeniería     | de Sistemas. |               |           |           |            |          |
| -------------- | ------------ | ------------- | --------- | --------- | ---------- | -------- |
| Programación   | de Redes.    |               |           |           |            |          |
| Examen 17      | Mayo 2023.   |               |           |           |            |          |
| 2 horas. Igual | puntaje      | por pregunta. |           |           |            |          |
|                | Especificar  | un protocolo  | que tenga | en cuenta | los puntos | a,b y c. |
R

| Ingeniería     | de Sistemas. |           |
| -------------- | ------------ | --------- |
| Programación   | de Redes.    |           |
| Examen 17      | Mayo 2023.   |           |
| 2 horas. Igual | puntaje por  | pregunta. |

| Ingeniería   |       | de Sistemas. |        |           |     |     |     |     |
| ------------ | ----- | ------------ | ------ | --------- | --- | --- | --- | --- |
| Programación |       | de           | Redes. |           |     |     |     |     |
| Examen       | 17    | Mayo         | 2023.  |           |     |     |     |     |
| 2 horas.     | Igual | puntaje      | por    | pregunta. |     |     |     |     |
4) (15 Pts) Mencione los tipos de Exchanges de RabbitMQ vistos en el curso y en que
|     | se diferencian |     |     |     |     |     |     |     |
| --- | -------------- | --- | --- | --- | --- | --- | --- | --- |
Respuesta:
| Directo | - Routing |     | Key =        | Binding | Key          |              |                |     |
| ------- | --------- | --- | ------------ | ------- | ------------ | ------------ | -------------- | --- |
| Topic   | - Routing | Key | hace         | match   | parcialmente |              | con el binding | key |
| Fanout  | - Propaga |     | los mensajes |         | a todos      | los bindings |                |     |
-----
| Header  | - Permite |         | hacer | coincidir | con | una binding | key con      | atributos |
| ------- | --------- | ------- | ----- | --------- | --- | ----------- | ------------ | --------- |
| Default | - El      | Routing | Key   | coincide  | con | el nombre   | de la Queue. |           |
5) (15 Pts.) Dado el siguiente código que es parte de un Servidor que atiende N
clientes:
while(IsRunning)
{
vartcpClientSocket=awaittcpListener.AcceptTcpClientAsync();
awaitTask.Run(async()=>awaitHandleClient(tcpClientSocket));
}
|     | Comente |     | qué problema |     | presenta | y cómo | lo resolvería. |     |
| --- | ------- | --- | ------------ | --- | -------- | ------ | -------------- | --- |
Respuesta:
El await en la llamada de Task hace que solo se pueda atender de a un cliente a la
|     | vez. | Se debe | eliminar | ese | await |     |     |     |
| --- | ---- | ------- | -------- | --- | ----- | --- | --- | --- |