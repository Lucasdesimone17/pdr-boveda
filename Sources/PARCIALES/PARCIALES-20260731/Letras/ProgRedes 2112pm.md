Evaluación

Parcial

Grupo

Matutino

Fecha

Dic-2021

Materia
Carrera
Condiciones

Programación de Redes
Ingeniería de Sistemas, ID
1 a 20 Puntos, 2 horas desde que se lee la letra, sin material

Ejercicio 1 (10 puntos)

Implemente un servidor que pueda manejar varias conexiones en simultáneo y que además cumpla los
siguientes requisitos:

1)   Aceptar conexiones hasta un máximo N (elegido por el alumno)

2)   La aplicación cliente envía el nombre de un artículo, el cual debe ser guardado en una lista en

memoria.

3)   Si el artículo está repetido, esa lista en memoria debe incrementar el valor cantidad de ese

elemento

El artículo cuenta con las siguientes propiedades:

●  Nombre
●  Fecha de vencimiento
●  Descripción
●  cantidad

Se sugiere implementar un protocolo sencillo para la parte de recepción de mensajes.

NOTA: Se puede usar tanto TCPListener / TCPClient como sockets, pero se debe implementar la solución
usando TASKS o THREADS.

Ejercicio 2 (3 puntos)

Explique que es un mecanismo de comunicación basado en RPC, y detalle los diferentes modos de
funcionamiento de gRPC vistos en el curso.

Ejercicio 3 (4 puntos)

Tomando como base las tecnologías vistas en el curso, y teniendo en cuenta las siguientes premisas:

•  Se necesita construir una app que reciba datos desde diferentes clientes
•  Existe  un  canal  de  comunicación  primario  que  recibe  información  que  deba  ser  procesada  en

tiempo real

•  Existe  también  un  canal  secundario,  que  recibe  información  no  crítica  y  se  puede  procesar  de

forma asincrónica.

•  Tanto el canal primario como el secundario deben ser capaces de atender a varios clientes a la

vez.

Discuta cuáles son las mejores opciones para cada uno de los canales de comunicación y justifique su
respuesta.

Ejercicio 4 (3 puntos)

●  Defina y describa las diferencias entre un proceso, una Task y un Thread
●  Defina el concepto de deadlock
●  Describa tres métodos que solucionen el problema de la mutua exclusión.

