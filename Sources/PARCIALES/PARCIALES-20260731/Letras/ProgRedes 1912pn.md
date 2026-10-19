Evaluación  Parcial
Materia
Carrera
Condiciones

Programación de Redes
Ingeniería de Sistemas, ID
1 a 40 Puntos, 2 horas desde que se lee la letra, sin material

Grupo

Nocturno

Fecha

Dic-2019

Ejercicio 1 (15 puntos)

Se deberá realizar la siguiente implementación utilizando TCP Listener / Client con asincronía utilizando
Async / Await:

Implemente  un  servidor  que  permita  como  máximo  cien  clientes  conectados.  Cuando  un  cliente  se
conecta este envía una prioridad (Del uno al diez, siendo uno la más baja y diez la más alta). Luego de
conectado  el  cliente  puede  enviar  hasta  veinte  mensajes  o  elegir  desconectarse  antes,  si  envía  veinte
mensajes el servidor deberá enviarle un mensaje al cliente (mensaje definido por usted) y desconectar al
mismo.

Si ya existen cien clientes conectados y se quiere conectar otro ahí deberán estudiar dos casos:

1.  Si  el  nuevo  cliente  envía  una  prioridad  menor  o  igual  a  la  menor  de  los  clientes  que  ya  estén

conectados el mismo será desconectado sin poder enviar mensajes.

2.  Si el nuevo cliente envía una prioridad mayor a la menor de los clientes que ya estén conectados
se deberá desconectar el cliente con la prioridad menor, en caso de que exista más de un cliente
que cumpla esta condición se deberá desconectar al que lleva más tiempo conectado al servidor.

Nota:  Debe  de  tener  en  cuenta  que  se  pide  explícitamente  que  el  ejercicio  se  desarrolle  con  clases
específicas para manejar el paralelismo o asincronía además del manejo de los datos a través de la capa
de  red.  En  caso  de  responder  al  mismo  con  otra  variante  el  puntaje  del  ejercicio  se  verá  afectado
negativamente.

Ejercicio 2 (5 puntos)

El diario “El Detallista” quiere crear un lector de noticias. Las noticias pueden ser creadas, modificadas,
leídas o borradas. Las noticias cuentan con un título, texto, foto, fecha de publicación y categoría. Las
categorías sólo cuentan con nombre y una descripción (pueden ser creadas y modificadas). Las personas
que leen las noticias son usuarios del sistema que se suscriben a las diferentes categorías de noticias.

Deberá ser posible acceder a cada usuario, noticia y categoría.

Es deseable también que se puedan buscar las noticias por fecha, categoría y / o título.

Teniendo en cuenta el escenario anterior diseñe los endpoints para una API REST que modele el problema.

Ejercicio 3 (5 puntos)

Detalle  las  principales  diferencias  entre  paralelismo  y  programación  asincrónica  desde  el  marco  del
framework .NET.

¿Qué desventajas proporciona usar threads contra usar tasks?

Ejercicio 4 (10 puntos)

Elija el tema que le haya tocado presentar en clase y responda las preguntas al respecto

Azure Functions

Explique brevemente que tipo de arquitectura es la que utiliza Azure Functions.

Detalle que es el concepto de trigger en esta plataforma y de un par de ejemplos concretos.

Azure Logic Apps

Detalle en sus palabras que són las Logic Apps ¿Por qué utilizaría esta tecnología?

Azure Storage

Cuáles son las principales diferencias entre Azure Table Storage y Azure Cosmos DB.

Describa brevemente que es blob storage y detalle los diferentes tipos de bloque que existen.

Blazor

Detalle brevemente el concepto de web assembly.

Detalle brevemente las dos grandes formas de trabajo en la que es posible realizar proyectos con Blazor.

GraphQL

¿En qué se diferencia GraphQL de las APIs Rest?

¿Puede explicar los conceptos de Schema, Query y Mutation?

GRPC

¿Qué son los archivos proto y qué tipos pueden contener los mismos?

¿Cuándo y por qué utilizaría GRPC sobre otra tecnología como pueden ser APIs Rest?

Microservicios

¿Por qué surge el concepto de Microservicios? Detalle también puntos a favor y puntos en contra.

¿Por qué es posible asociar los microservicios con una tecnología como Docker?

Rabbit MQ

Detalle el concepto de Broker y porque es importante para Rabbit MQ.

¿Cuáles son los tipos de Exchange que tiene Rabbit y en qué se diferencian?

