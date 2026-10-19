| Ingeniería   | de Sistemas.  |     |     |     |
| ------------ | ------------- | --- | --- | --- |
| Programación | de Redes.     |     |     |     |
| Examen       | Octubre 2023. |     |     |     |
2 horas.
| Ejercicio | 1 (30 pts) |     |     |     |
| --------- | ---------- | --- | --- | --- |
Implemente un servidor que pueda mantener N conexiones de distintos clientes (se deben
| verificar      | desconexiones). |                   |             |           |
| -------------- | --------------- | ----------------- | ----------- | --------- |
| Dicho servidor | atenderá        | pedidos entrantes | de conexión | y, cuando |
establezca una conexión, deberá comportarse de la siguiente manera:
a. Cuando un cliente se conecta, el servidor le asigna un id único y se lo envía al cliente
conectado.
b. Luego de esto, el cliente puede enviar un mensaje al servidor con su id y la ciudad desde
| donde se | está conectando. |     |     |     |
| -------- | ---------------- | --- | --- | --- |
c. Cada vez que el mensaje sea entregado al servidor, este debe enviar un mensaje a todos
los clientes notificando que existe un nuevo cliente y la ciudad de donde se conecta.
NOTA: Se sugiere construir un protocolo sencillo para soportar los dos comandos
mencionados
| Ejercicio | 2 (15 puntos) |     |     |     |
| --------- | ------------- | --- | --- | --- |
Tomando como base las tecnologías vistas en el curso, y teniendo en cuenta las siguientes
premisas:
1. Se necesita construir una app que reciba datos de varios clientes a la vez.
2. Estos clientes envían muchos paquetes de tamaño pequeño y el servidor debe acumular
| una cantidad | X de los | mismos para luego | procesarlos | a la vez. |
| ------------ | -------- | ----------------- | ----------- | --------- |
3. Cada intervalo de Y tiempo, el servidor debe publicar estadísticas de cuál fue el resultado
del procesamiento en ese periodo de tiempo, en un canal de comunicaciones SEPARADO
| de los que | se establecen | con los clientes. |     |     |
| ---------- | ------------- | ----------------- | --- | --- |
Discuta cuáles son las mejores opciones para el caso descrito anteriormente, desde el
punto de vista de los canales de comunicación, así como también desde el punto de vista
| de la arquitectura | del | servidor. |     |     |
| ------------------ | --- | --------- | --- | --- |

| Ingeniería   | de      | Sistemas. |        |     |     |     |     |
| ------------ | ------- | --------- | ------ | --- | --- | --- | --- |
| Programación |         | de        | Redes. |     |     |     |     |
| Examen       | Octubre | 2023.     |        |     |     |     |     |
2 horas.
| Ejercicio | 3   | (15 pts) |     |     |     |     |     |
| --------- | --- | -------- | --- | --- | --- | --- | --- |
Mencione los tipos de Exchanges de RabbitMQ vistos en el curso y en qué se diferencian
| Ejercicio | 4       | (5 pts)  |              |     |                     |         |     |
| --------- | ------- | -------- | ------------ | --- | ------------------- | ------- | --- |
| Compare   | (nombre |          | diferencias) |     | entre Mutex,Monitor | y Lock. |     |
| Ejercicio | 5       | (20 Pts) |              |     |                     |         |     |
Dada una operación SaveData(String text) que simula guardar datos en una base de
| datos.    | Realizar | el código   |     | que implique |                    |            |            |
| --------- | -------- | ----------- | --- | ------------ | ------------------ | ---------- | ---------- |
|           | a)       | Disparar    | dos | hilos.       |                    |            |            |
|           | b)       | Arbitrar    | el  | acceso       | al método SaveData | bajo mutua | exclusión. |
| Ejercicio | 6        | (5 pts)     |     |              |                    |            |            |
| Mencione  | las      | principales |     | diferencias  | entre gRPC         | y MOM      |            |
| Ejercicio | 7        | (10 pts)    |     |              |                    |            |            |
Realice el archivo Proto para transmitir (dar de alta) un objeto persona que tenga como
| atributos | el     | nombre, | la id | y el mail | y teléfono(s).   |              |     |
| --------- | ------ | ------- | ----- | --------- | ---------------- | ------------ | --- |
| Tener en  | cuenta | que     | puede | tener     | más de un número | de teléfono. |     |
También un método capaz de borrar un usuario (recibe una id) y devuelve un string.