Fecha: 24/10/2025

Evaluación: Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 1 de 2

Puntaje Máximo:     100 Puntos

Ejercicio 1 (40 puntos)

Responda brevemente 6 de las siguientes 8 preguntas:

1.  Explique qué problema soluciona Docker.
2.  Explique las ventajas que tiene gRPC sobre una API REST con HTTP.
3.  Explique las ventajas de utilizar Task en lugar de Thread en .NET. Describa cómo alguna de estas

ventajas se relaciona con el concepto de Thread Pool.

4.  Explique las diferencias entre programa, proceso e hilo.
5.  Deﬁna qué es una condición de carrera.
6.  Un sistema utiliza colas de mensajes para procesar los resultados de encuestas de forma

asincrónica. ¿Qué recomienda para el manejo de errores en caso de que falle el procesamiento
de un mensaje?

7.  Explica la diferencia entre operaciones de entrada/salida (I/O bound) y operaciones CPU-bound.

Incluya cómo se relacionan con el concepto de concurrencia o el de paralelismo.
 Explique las ventajas de utilizar una comunicación a través de colas de mensaje.

8.

Ejercicio 2 (60 puntos)

Usted ha sido contratado por una aerolínea que desea modernizar su sistema de reservas de vuelos. El
objetivo es desarrollar un sistema de comunicación distribuido que cumpla con los siguientes
requerimientos:

Servidor Principal:
 Gestiona las consultas y reservas de vuelos. Los clientes y agencias de viajes deben poder consultar
disponibilidad y realizar reservas en línea. La conﬁrmación de las reservas se procesa en tiempo real
para evitar la sobreventa de asientos.

Servidor de Notiﬁcaciones:
 Recibe avisos del servidor principal cuando un vuelo sufre un cambio (retraso, cancelación,
modiﬁcación de puerta de embarque). A partir de esta información, se encarga de notiﬁcar a los clientes
y agencias de viajes afectadas.

Servidor de Análisis:
 Recibe periódicamente información sobre reservas y ocupación de vuelos. Consolida los datos
históricos y genera reportes para la gestión de la empresa.

Fecha: 24/10/2025

Evaluación: Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 2 de 2

Puntaje Máximo:     100 Puntos

Para implementar este sistema, deberá utilizar 3 de las siguientes tecnologías: Sockets, MOM, gRPC, API
REST. Queda bajo su criterio cómo y cuáles tecnologías utilizar. Justiﬁque su elección y para cada
tecnología seleccionada, realice lo siguiente:

●  gRPC: Deﬁna el archivo .proto que describe los servicios y mensajes.
●  Sockets: Proporcione un diagrama del protocolo de comunicación e incluya ejemplos de

mensajes y respuestas.

●  MOM: Especiﬁque el uso de colas, tipo de exchanges y los mensajes que se envían (ej. contenido

y formato JSON o XML).

●  API REST: Deﬁna los endpoints necesarios para el sistema, incluyendo los datos que se enviarán,

las posibles respuestas y sus status codes.

Además, realice lo siguiente:

●  Diagrame la solución mostrando los canales de comunicación y los componentes involucrados

●

en cada extremo.
Indique en cada canal quién actúa como cliente/servidor o productor/consumidor (según
corresponda).

●  Puede realizar suposiciones sobre los datos que se necesitan enviar y recibir.
●  Puede utilizar más de una tecnología para un mismo componente, si lo considera apropiado.

