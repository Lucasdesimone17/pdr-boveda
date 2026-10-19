Fecha: 23/04/2025

Evaluación: Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Turno: Nocturno

Puntaje Máximo:     100 Puntos

Página 1 de 2

Ejercicio 1 (10 puntos)

Responda brevemente las siguientes preguntas:

1.  Explique qué ventajas tengo al utilizar la librería TCPClient/TCPListener en vez de Sockets.
2.  Explique las ventajas y desventajas que tiene gRPC sobre API Rest. Describa brevemente un

ejemplo donde sea mejor utilizar gRPC que API Rest.

Ejercicio 2 (15 puntos)

Para cada aﬁrmación indique si es Verdadero o Falso y justiﬁque su respuesta:

1.  Los Semáforos son una herramienta más ﬂexible que los Monitores para la sincronización de

hilos en sistemas concurrentes.

2.  En .NET, el uso de Task es más adecuado que Thread para operaciones que involucran esperas

de I/O.

3.  En un sistema basado en colas de mensajes, los mensajes siempre se procesan en el mismo

orden en que fueron enviados.

Ejercicio 3 (35 puntos)

Implemente un servidor que pueda manejar varias conexiones en simultáneo y que además cumpla los
siguientes requisitos:

1.  Aceptar conexiones hasta un máximo N (elegido por el alumno).
2.  Cada cliente va a tener asignado una ID de tipo entero autoincremental, diferente para cada

cliente

3.  Cuando un cliente se conecta el servidor le envía un mensaje con su ID.
4.  Se debe veriﬁcar desconexiones.

Nota: Se puede utilizar tanto TcpClient/TcpListener como Sockets y se puede utilizar tanto Threads
como Tasks.

1

Fecha: 23/04/2025

Evaluación: Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Turno: Nocturno

Puntaje Máximo:     100 Puntos

Página 2 de 2

Ejercicio 4 (40 puntos)

Usted ha sido contratado por una empresa de venta de congelados que ha empezado a crecer abriendo
varias sucursales en el interior del país. El objetivo es desarrollar un sistema de comunicación
distribuido que cumpla con los siguientes requerimientos:

1.  Comunicación entre casa matriz y sucursales:

Las sucursales deben poder consultar información en la casa matriz, y la casa matriz necesita
consultar información en las sucursales.

2.  Representantes externos:

En algunas ciudades pequeñas, no hay sucursales, pero sí representantes externos que se
encargan de las ventas. Estos representantes deben poder acceder al sistema para consultar el
valor de los congelados y realizar compras al por mayor.

3.  Estadísticas de ventas y stock:

La casa matriz desea enviar periódicamente (cada 10 minutos) todos los datos de ventas y stock
al servidor de estadísticas. Este servidor almacena los datos y realiza cálculos para mostrar
estadísticas en tiempo real.

Para implementar este sistema, deberá utilizar 3 de las siguientes tecnologías: Sockets, MOM, gRPC,
API REST. Queda bajo su criterio cómo y cuáles tecnologías utilizar. Justiﬁque su elección y para cada
tecnología seleccionada, realice lo siguiente:

●  gRPC: Deﬁna el archivo .proto que describe los servicios y mensajes.
●  Sockets: Proporcione un diagrama del protocolo de comunicación (mensajes, respuestas).
●  MOM: Especiﬁque el uso de colas, tipo de exchanges y los mensajes que se envían (ej. formato

JSON o XML).

●  API REST: Deﬁna los endpoints necesarios para el sistema, incluyendo los datos que se enviarán,

las posibles respuestas y sus status codes.

Además, realice lo siguiente:

1.  Diagrame la solución mostrando los canales de comunicación y los componentes involucrados

en cada extremo.

2.  Indique en cada canal quién actúa como cliente/servidor o productor/consumidor (según

corresponda).

3.  Puede realizar suposiciones sobre los datos que se necesitan enviar y recibir.
4.  Puede utilizar más de una tecnología para un mismo componente, si lo considera apropiado.

2

