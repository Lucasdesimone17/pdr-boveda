| Parcial | Programación |           | de    | Redes | Nocturno | 26/06/2023 |     |     |
| ------- | ------------ | --------- | ----- | ----- | -------- | ---------- | --- | --- |
| Todas   | las          | preguntas | valen | lo    | mismo.   |            |     |     |
2 horas.
|     | 1) Comenta | la  | veracidad | de  | las siguientes | frases |     | justifique: |
| --- | ---------- | --- | --------- | --- | -------------- | ------ | --- | ----------- |
a) Un Lock con manejo de excepciones puede ser considerado un Monitor.
|     |     | b) No | es posible | configurar |     | prioridades | a   | los Hilos. |
| --- | --- | ----- | ---------- | ---------- | --- | ----------- | --- | ---------- |
c) Si quiero manejar bloqueos a nivel de Sistema Operativo, debo manejar
Mutex.
|     | 2) Diferencie |              | una Task    | de un   | Thread.     |                |     |     |
| --- | ------------- | ------------ | ----------- | ------- | ----------- | -------------- | --- | --- |
|     | 3) Se         | tiene un     | objeto      | Persona | con         | los atributos: |     |     |
|     |               | a) nombre    | (string)    |         |             |                |     |     |
|     |               | b) teléfonos | (una        | lista   | de strings) |                |     |     |
|     |               | c) edad      | (un entero) |         |             |                |     |     |
|     |               | d) apellido  | (string)    |         |             |                |     |     |
e) un método dameDireccion que recibe un apellido y devuelve un String.
|     | Se       | pide, |         |       |            |               |     |          |
| --- | -------- | ----- | ------- | ----- | ---------- | ------------- | --- | -------- |
|     | Realizar | el    | archivo | proto | del objeto | anteriormente |     | nombrado |
4) Dado el siguiente código que es parte de un Servidor que atiende N clientes:
while(IsRunning)
{
vartcpClientSocket=awaittcpListener.AcceptTcpClientAsync();
awaitTask.Run(async()=>awaitHandleClient(tcpClientSocket));
}
| Comente | qué | problema | presenta |     | y cómo | lo resolvería. |     |     |
| ------- | --- | -------- | -------- | --- | ------ | -------------- | --- | --- |
5) Mencione los tipos de Exchanges de RabbitMQ vistos en el curso y en qué se
diferencian.

| Parcial | Programación  | de    | Redes Nocturno | 26/06/2023 |     |
| ------- | ------------- | ----- | -------------- | ---------- | --- |
| Todas   | las preguntas | valen | lo mismo.      |            |     |
2 horas.
6) Se tiene un sistema de gestión de una cadena de pizzerías. Existen los siguientes
servicios:
- Servidor Contable: el cual despacha las facturas emitidas por cada sucursal.
- Servidor Geomática (GeoServidor): El cual describe a cada repartidor el camino más
| corto      | al destino      | de la | entrega.         |             |           |
| ---------- | --------------- | ----- | ---------------- | ----------- | --------- |
| - Servidor | administrativo. |       | El cual registra | los pedidos | en su BD. |
- Servidor Central. Donde se conectan todas las sucursales para el ingreso de
pedidos.
Realizar un esquema de los mismos, indicando qué tecnologías de las vistas en el curso
aplicaría para cada una de las comunicaciones entre los mismos. Justifique.
7)
En una de las pizzerías, los mozos cuentan con tablets que se conectan al servidor central.
Escriba el código necesario para que el Servidor Central acepte varias conexiones de las
| tablets utilizando | Socket. |     |     |     |     |
| ------------------ | ------- | --- | --- | --- | --- |
8)
Para la misma pizzería escriba el código necesario para que la tablet se conecte al Servidor
| Central    | utilizando   | Socket.   |             |     |     |
| ---------- | ------------ | --------- | ----------- | --- | --- |
| 9)Explique | en sus       | palabras: |             |     |     |
| Cuando     | es necesario | utilizar  | asincronía. |     |     |
Explique que es un semáforo y en qué caso lo utilizaría sobre otros mecanismos de mutua
exclusión.
10) Mencione las ventajas de utilizar TCPListener/TCPClient versus utilizar Socket.