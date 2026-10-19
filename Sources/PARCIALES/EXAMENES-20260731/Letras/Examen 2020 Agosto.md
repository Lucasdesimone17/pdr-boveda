Escuela de Ingeniería

Universidad ORT Uruguay

Evaluación
Materia
Carrera
Condiciones

Grupo

Examen
Programación de Redes
Ingeniería de Sistemas, ID
1 a 100 Puntos, 2 horas desde que se lee la letra, sin material

No aplica

Fecha

21 - Agosto - 2020

Ejercicio 1 (40 puntos)

Implemente cliente y servidor que pueda mantener hasta N conexiones de distintos clientes (se
deben  verificar  desconexiones).  Dicho  servidor  atenderá  pedidos  entrantes  de  conexión  y,
cuando establezca una conexión, deberá comportarse de la siguiente manera:

a.  Cuando un cliente se conecta, el servidor mediante un método que se encuentra en un
dll externa le asigna un string, que representa el string de conexión hacia una MSMQ.
Estre string se envia desde el server al cliente.

b.  Si la cantidad de clientes conectados es N y se quiere conectar otro cliente el mismo

debe ser desconectado.

Adicionalmente, se debe contemplar el siguiente comportamiento en el cliente:

a.  Cuando el cliente recibe el string de conexión desde el server, el cliente debe conectarse
a esa MSMQ y recibir mensajes de la misma. Los mensajes serán strings, los mismos
deberán ser impresos por pantalla.

b.  Si se recibe un mensaje que diga ^^EXIT^^, el cliente debe dejar de escuchar mensajes

y desconectarse.

NOTA 1: Se sugiere construir un protocolo para llevar a cabo las operaciones.
NOTA 2: Recordar que para recibir el mensaje correctamente en la MSMQ, hay que configurar
un formatter de la siguiente forma:

var messageQueue = new MessageQueue(queuePath)

{Formatter = new XmlMessageFormatter(new [] {typeof(XXXX)})};

Ejercicio 2 (20 puntos)

¿Cuál es la diferencia entre paralelismo y asincronía? ¿Son conceptos excluyentes?

Ejercicio 3 (10 puntos)

¿Cuáles son los estados que pueden tener un Thread? ¿Existe relación entre ellos? Justifique

Ejercicio 4 (15 puntos)

¿Qué mecanismos de mutúa exclusión conoce? ¿Cuál es la diferencia entre ellos?

Ejercicio 5 (15 puntos)

¿Tiene sentido implementar en una API REST una estructura de herencia de objetos?

Digamos si yo tengo la clase animal, la cuál es abstracta, pero tenemos las clases perro y gato
que heredan de animal y represento esto mediante las siguientes rutas:

•  api.zoo.com/animals
•  api.zoo.com/animals/dogs
•  api.zoo.com/animals/cats

