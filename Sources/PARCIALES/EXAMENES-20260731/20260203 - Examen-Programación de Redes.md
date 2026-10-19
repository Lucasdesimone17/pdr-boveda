Fecha: 03/02/2026

Evaluación: Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 1 de 4

Puntaje Máximo:     100 Puntos

Ejercicio 1 (40 puntos)

Responda 6 de las siguientes 8 preguntas. Respuestas breves, claras y con ejemplos cuando aplique.

1.  Explique concurrencia vs paralelismo e indique un ejemplo de cada uno en un servidor que

atiende múltiples clientes.

2.  Deﬁna I/O-bound y CPU-bound. ¿Qué enfoque recomienda en .NET para cada caso

(Task/async/await vs paralelismo)?

3.  Explique qué es idempotencia en APIs y por qué es importante en presencia de reintentos y

timeouts. Dé un ejemplo con un endpoint REST.

4.  Diferencie Polling (short/long), Server-Sent Events (SSE) y WebSockets. Incluya un

mini-diagrama de ﬂujo y un caso de uso para cada uno.

5.  En un sistema con MOM (RabbitMQ): ¿qué estrategias recomienda ante fallos de consumo?

6.  Compare gRPC vs REST en términos de performance, contratos, compatibilidad (navegadores), y

evolución del servicio.

7.  Deﬁna condición de carrera y mencione dos formas de mitigarla en .NET cuando varios Tasks

actualizan un recurso compartido.

8.  Explique el concepto de Thread Pool y cómo se relaciona con el uso de Task (beneﬁcios y

riesgos si se abusa de Thread/Task).

Fecha: 03/02/2026

Evaluación: Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 2 de 4

Puntaje Máximo:     100 Puntos

Ejercicio 2 (15 puntos)

Una plataforma registra los siguientes eventos de red (simplificados):

Hora

Origen (IP:Puerto)

Destino (IP:Puerto)

Proto

Acción

10:12:01

198.51.100.21:53110

203.0.113.10:443

HTTPS

POST /jobs (202 Accepted)

10:12:01

203.0.113.10:443

198.51.100.21:53110

HTTPS

10:12:05

198.51.100.21:53111

203.0.113.10:443

HTTPS

10:12:12

198.51.100.21:53112

203.0.113.10:443

HTTPS

Response 202 (Location:
/jobs/8841)

GET /jobs/8841 (200 OK,
status=RUNNING)

GET /jobs/8841 (200 OK,
status=DONE)

10:12:12

198.51.100.21:53112

203.0.113.10:443

HTTPS

GET /jobs/8841/result (200 OK)

1.  Realice un diagrama de secuencia que muestre la comunicación.
2.   Identifique el patrón utilizado (pista: procesamiento asíncrono + consulta de estado) y explique

beneficios y desventajas.

3.   Proponga una alternativa para evitar el polling (puede ser SSE, WebSockets o callback

webhook) y explique brevemente cómo quedaría el flujo.

Fecha: 03/02/2026

Evaluación: Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 3 de 4

Puntaje Máximo:     100 Puntos

Ejercicio 3 (45 puntos)

Se necesita implementar un servidor TCP para un sistema de “turnos rápidos” de un local de atención al
público.

Requerimientos funcionales

●  El servidor acepta múltiples clientes concurrentes hasta un máximo N (deﬁnido por el alumno).

●  Cada cliente, al conectarse, recibe un mensaje de bienvenida con un ClientId autoincremental.

●  El protocolo es texto.

Comandos del cliente

●  PING → el servidor responde PONG

●  TAKE → el servidor asigna un número de turno incremental y responde TICKET <numero>

●  STATUS <numero> → responde STATUS <numero> <state> donde <state> puede ser PENDING,

CALLED, UNKNOWN

●  QUIT → el servidor responde BYE y cierra la conexión

Eventos del servidor

●  Si un cliente se desconecta inesperadamente, el servidor debe detectarlo (explicar cómo).

●  El servidor debe registrar en consola: conexión, comandos recibidos, tickets emitidos y

desconexiones.

Fecha: 03/02/2026

Evaluación: Examen

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de Redes

Uso de Material:     NO

Página 4 de 4

Puntaje Máximo:     100 Puntos

Se pide:

1 -  (40 pts) Pseudocódigo (estilo C#) de un servidor concurrente usando TcpListener +
Tasks/async-await (o Threads, pero se valora el enfoque asincrónico). Debe verse:

●  Bucle accept de clientes

●  Manejo concurrente por cliente

●  Asignación segura de ClientId y TicketNumber (sin condiciones de carrera)

●  Manejo de desconexión/errores

2 -  (5 pts) Explique 2 decisiones técnicas:

●  Cómo evita condiciones de carrera
●  Cómo limita N conexiones y qué hace cuando se supera (rechazar, encolar, etc.)

