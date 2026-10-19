Evaluación

Parcial

Grupo

Matutino

Fecha

Dic-2019

Materia
Carrera
Condiciones

Programación de Redes
Ingeniería de Sistemas, ID
1 a 40 Puntos, 2 horas desde que se lee la letra, sin material

Ejercicio 1 (15 puntos)

Implemente un servidor que atienda conexiones y que sea capaz de:

1)  Aceptar conexiones hasta un maximo N (elegido por el alumno)
2)  Cada X cantidad de tiempo, se debe desconectar al cliente que menos mensajes envio al

servidor

3)  Cada vez que se recibe un mensaje desde un cliente, se debe mostrar el mismo en pantalla

Se sugiere implementar un protocolo sencillo para la parte de recepcion de mensajes.

NOTA: Se puede usar tanto TCPListener / TCPClient como sockets, pero se debe implementar la solucion
usando TASKS y THREADS.

Ejercicio 2 (8 puntos)

Compare una arquitectura monolitica vs una arquitectura de microservicios desde el punto de vista de la
escalabilidad y la mantenibilidad.

Ejercicio 3 (12 puntos)

Imagine que tiene que construir un componente de una arquitectura que debe recibir muchos mensajes
desde diferentes aplicaciones. Estos mensajes no van a llegar al mismo tiempo y tampoco es critico su
entrega hacia este componente.

A su vez, el componente tiene que entregar, a medida que vaya obteniendo sus resultados a partir de los
mensajes recibidos, los resultados hacia otra aplicación que los necesita tan pronto como se obtengan los
mismos.

Discuta con que tecnologias vistas en el curso es mejor implmentar la recepcion de mensajes y la entrega
de los resultados hacia la aplicación que los espera.

Ejercicio 4 (5 puntos)

•  ¿En qué se diferencia un proceso de un thread? ¿Todo proceso tiene un thread?
•  Nombre tres métodos que solucionen el problema de la mutua exclusión.

