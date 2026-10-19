| Examen | Programación | de Redes | 29/01/2024 |     |     |
| ------ | ------------ | -------- | ---------- | --- | --- |
2 horas
| Ejercicio | 1 (20         | puntos)        |           |             |            |
| --------- | ------------- | -------------- | --------- | ----------- | ---------- |
| Responda  | brevemente    | las siguientes | preguntas |             |            |
| A.        | Explique que  | es un deadlock | y de      | un ejemplo  |            |
| B.        | ¿Qué ventajas | provee usar    | Async     | Task frente | a Threads? |
| C.        | ¿Qué es un    | middleware?    |           |             |            |
D. ¿En qué contexto se utiliza mutex frente al resto de mecanismos de mutua exclusión
|           | vistos en clase? |         |     |     |     |
| --------- | ---------------- | ------- | --- | --- | --- |
| Ejercicio | 2 (35            | puntos) |     |     |     |
Implemente un servidor que pueda manejar varias conexiones en simultáneo y que además
| cumpla | los siguientes     | requisitos: |           |            |                |
| ------ | ------------------ | ----------- | --------- | ---------- | -------------- |
| 1.     | Aceptar conexiones | hasta       | un máximo | N (elegido | por el alumno) |
2. Cada cliente va a tener asignado una ID de tipo entero autoincremental, diferente
|     | para cada cliente |     |     |     |     |
| --- | ----------------- | --- | --- | --- | --- |
3. Cuando un cliente se conecta el servidor le envía un mensaje con su ID
| 4.  | Se debe verificar | desconexiones |     |     |     |
| --- | ----------------- | ------------- | --- | --- | --- |
Nota: Se puede utilizar tanto TcpClient/TcpListener como Sockets y se puede utilizar tanto
| Threads   | como Tasks |         |     |     |     |
| --------- | ---------- | ------- | --- | --- | --- |
| Ejercicio | 3 (45      | puntos) |     |     |     |
Una empresa de trenes le pidió ayuda para diseñar su nuevo sistema. Existen los siguientes
requerimientos:
1. La empresa TrenesUy administra todas las estaciones del país. Se necesita un canal
de comunicación para enviar información entre la estación central y las estaciones
del país. Tanto las estaciones secundarias deben poder consultar información a la
estación central, como la central consultar información a las secundarias.
2. Existen empresas externas que se encargan de la venta de pasajes. Quieren poder
acceder al sistema para saber el valor de los viajes y poder efectuar las compras.
3. TrenesUy quiere enviar la cantidad de pasajeros que entraron a la estación y el
tiempo de retraso de los trenes al servidor de estadísticas. Este servidor recibe los
datos, los almacena y hace varios cálculos para mostrar estadísticas que se espera
|     | estén actualizadas | cada | 10 minutos. |     |     |
| --- | ------------------ | ---- | ----------- | --- | --- |

| Examen | Programación |     | de Redes |     | 29/01/2024 |     |
| ------ | ------------ | --- | -------- | --- | ---------- | --- |
2 horas
Para cumplir estos requerimientos se requiere utilizar 3 de las siguientes tecnologías
| sockets,   | MOM,     | gRPC,       | API REST. |                 |            |                    |
| ---------- | -------- | ----------- | --------- | --------------- | ---------- | ------------------ |
| Quedando   | bajo     | su criterio | cómo      | y cuáles        | utilizar.  |                    |
| Justifique | su       | elección    | y para    | las tecnologías |            | que elija indique: |
| ●          | gprc:    | Especificar | archivo   | .proto          |            |                    |
| ●          | Sockets: | Diagrama    | del       | protocolo       | a utilizar |                    |
● MOM: Diagrama de los mensajes que se envían (ej. contenido del JSON o similar)
| ●   | API REST: | Defina | los endpoints |     |     |     |
| --- | --------- | ------ | ------------- | --- | --- | --- |
Diagrame la solución mostrando los canales de comunicación y los componentes en ambos
| extremos | de  | la comunicación. |     |     |     |     |
| -------- | --- | ---------------- | --- | --- | --- | --- |