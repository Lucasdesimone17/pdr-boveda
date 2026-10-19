---
date: 2026-08-26
type: synthesis
tags:
  - synthesis
  - pdr
  - temario
ai-first: true
confidence: high
sources:
  - "Sources/TEORICO/_PROGRAMA_Y_RESUMENES/Programación de Redes.md"
  - "Sources/ESTUDIO_PREVIO/ROADMAP.md"
  - "Sources/ESTUDIO_PREVIO/MAIN.md"
  - "Sources/ESTUDIO_PREVIO/00_INDICE.md"
---

# Mapa del curso Programacion de Redes

## For future agent
Punto de entrada de esta bóveda. Reconcilia los dos temarios que circulan en el material (6 temas vigentes de Aulas vs 8 unidades del PDF de programa), fija el calendario y el esquema de puntos del semestre 2026, y dice qué tema apunta a qué carpeta de fuentes. Leer esto antes de cualquier nota de tema. Vigencia: refleja el material local a 2026-08-26; lo que la docente publique en clase manda por encima.

## Los dos temarios, y cuál manda

Hay **dos mapas del curso** en el material y no coinciden. Esto no es un error de ingesta: son dos versiones del programa.

| | Temario vigente de Aulas | PDF de programa (histórico) |
|---|---|---|
| Unidades | **6 temas** en 16 semanas | 8 unidades |
| Estructura | Multihilo · Sockets · Asincronía · Colas · RPC · REST | Introducción · Threads y Concurrencia · Paralelismo · MOM · RPC y ORBs · **Web Services** · REST · **Arquitecturas Cloud** |
| Fuente | Aulas (copiado en `Sources/ESTUDIO_PREVIO/MAIN.md`) | `Sources/TEORICO/_PROGRAMA_Y_RESUMENES/Programación de Redes.md` |

**Manda el temario de Aulas de 6 temas** (así lo fija el `ROADMAP.md` del corpus previo, as of 2026-08-18). Las dos unidades que el mapa vigente no tiene:
- **Web Services / SOAP-WSDL**: pertenece al programa anterior. Se conserva como referencia, no como hueco.
- **Arquitecturas Cloud**: el material práctico que quedó es Docker; la teoría de "sistemas operativos cloud" no tiene cobertura oficial en las fuentes locales. Ver [[Huecos y contradicciones del material PDR]].

Consecuencia práctica: la numeración de temas **choca entre las dos versiones** (el "Tema 2" es Sockets en el mapa vigente y Threads en el histórico). Cuando una nota o una letra de parcial diga "Tema N", verificar contra qué mapa.

## Temario vigente: 6 temas, 16 semanas

| # | Semanas | Tema | Tecnología (C#) | Fuente principal | Nota |
|---:|---:|---|---|---|---|
| 1 | 1-3 | Programación multihilo | `Thread`, `lock`, `Monitor`, `Mutex`, `Semaphore`, `ManualResetEvent`, `AutoResetEvent` | `TEORICO/02_THREADS_Y_CONCURRENCIA/` + 3 guías | [[Tema 1 - Programacion multihilo]] |
| 2 | 4-6 | Comunicación basada en Sockets | `Socket`, `TcpListener`/`TcpClient`, `NetworkStream`, `UdpClient` | `TEORICO/SOCKETS_COMUNICACION_EN_RED/` + 3 guías | [[Tema 2 - Sockets y protocolos]] |
| 3 | 7-8 | Asincronía y paralelismo | `Task`, `async`/`await`, `Parallel`, PLINQ, `CancellationToken` | `TEORICO/03_PARALELISMO/` | [[Tema 3 - Asincronia y paralelismo]] |
| 4 | 9-11 | Colas de mensajes | AMQP, RabbitMQ | `TEORICO/04_MOM/` | [[Tema 4 - Colas de mensajes y MOM]] |
| 5 | 12-14 | Invocaciones remotas | gRPC, Protocol Buffers, HTTP/2 | `TEORICO/05_RPC_Y_ORBS/` | [[Tema 5 - RPC y gRPC]] |
| 6 | 15-16 | APIs REST | ASP.NET Core Web API, capas, inyección de dependencias | `TEORICO/07_REST/` (+ WebSockets) | [[Tema 6 - APIs REST y WebSockets]] |

Complementarios conservados: [[Complementario - Docker y Cloud]], [[Complementario - Introduccion y Streams]], [[Referencia - Web Services del programa anterior]], [[Fundamentos previos]].

## El hilo conductor

El curso no es una lista de tecnologías sueltas: es **una escalera de mecanismos de comunicación entre procesos**, de lo más crudo a lo más abstracto.

1. Primero se resuelve la ejecución concurrente **dentro** de un proceso (Temas 1 y 3).
2. Después la comunicación **entre** procesos al nivel más bajo disponible: sockets y un protocolo propio, con framing hecho a mano (Tema 2).
3. Después tres formas de no volver a escribir ese protocolo a mano, cada una con un modelo distinto: asíncrona y desacoplada (MOM, Tema 4), síncrona y tipada (gRPC, Tema 5), y sobre HTTP con recursos (REST, Tema 6).

Toda pregunta de arquitectura del parcial se apoya en ese eje: elegir el mecanismo adecuado y justificarlo. Ver [[Elegir tecnologia de comunicacion]] y [[Patrones de examen en Programacion de Redes]].

## Calendario y evaluación del semestre 2026

De `Sources/ESTUDIO_PREVIO/MAIN.md` (fechas sin año en el original; el semestre corre desde el 18/08/2026, así que se leen como 2026):

| Fecha | Hito |
|---|---|
| 2026-08-26 | Lectura del Obligatorio 1 |
| 2026-09-28 | Entrega Obligatorio 1 + lectura del Obligatorio 2 |
| 2026-10-29 | Entrega Obligatorio 2 + lectura del Obligatorio 3 |
| 2026-11-23 | Entrega Obligatorio 3 |
| 2026-11-30 | Parcial |

Puntaje: **Obligatorio 1 = 25 · Obligatorio 2 = 15 · Obligatorio 3 = 25 · Parcial = 35** (total 100).

De los obligatorios históricos de marzo 2026 (`Sources/OBLIGATORIO/HISTORICO_MAR2026/`) se sabe además que la **defensa es obligatoria y eliminatoria**: no presentarse implica perder todos los puntos del obligatorio, y que el mínimo del Obligatorio 1 era 5 de 25 puntos. Son las pautas del ciclo anterior, no del actual.

## Bibliografía del programa
- Albahari, *C# 7.0 in a Nutshell* y *Threading in C#*; Troelsen, *Pro C# 7*; Comer, *Internetworking with TCP/IP Vol. 1*; Kavis, *Architecting the Cloud*.
- Bibliografía de Aulas: Archer *Inside C#*, Albahari *C# 10.0 in a Nutshell*, Makofske/Donahoo/Calvert *TCP/IP Sockets in C#*, Cleary *Concurrency in C# Cookbook*, Sazanavets *Microservices Communication in .NET Using gRPC*.
- Enlaces oficiales que el curso usa: albahari.com/threading, docs de Microsoft para sockets y async, rabbitmq.com/getstarted, grpc.io y Protocol Buffers.

## Connections
- Proyecto: [[Programacion de Redes 2026]].
- Transversales: [[Elegir tecnologia de comunicacion]] · [[Concurrencia de Thread a Task]] · [[Patrones de examen en Programacion de Redes]] · [[Errores caros en Programacion de Redes]].
- Método: [[Plan de estudio y repaso PDR]]. Pendientes: [[Huecos y contradicciones del material PDR]].
