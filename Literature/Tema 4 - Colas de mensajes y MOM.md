---
date: 2026-08-26
type: literature
tags:
  - literature
  - pdr
  - tema-4
  - mom
  - rabbitmq
  - amqp
ai-first: true
source: "Sources/TEORICO/04_MOM/MOM - Soluciones de mensajeria.md"
source_extra:
  - "Sources/ESTUDIO_PREVIO/TEMA_04_COLAS_DE_MENSAJES/CHEATSHEET.md"
confidence: stated
semanas: "9-11"
frecuencia_parcial: "13/16 (81%)"
---

# Tema 4 - Colas de mensajes y MOM

## For future agent
Nota de tema 4 del temario vigente (semanas 9-11): middleware orientado a mensajes, AMQP y RabbitMQ. Frecuencia histórica de parcial: **13 de 16 letras (81%)**. Lo evaluable: diseñar exchanges, colas y routing keys, y justificar arquitectónicamente por qué una cola en vez de una llamada directa. Fuente: `MOM - Soluciones de mensajeria.pdf`.

## Source
`Sources/TEORICO/04_MOM/MOM - Soluciones de mensajeria.md`.

## Key Claims

### Dónde encaja el middleware
- Vive **entre la capa de Aplicación y la capa de Plataforma** (SO + red).
- Tres familias: **RPC-based** (síncrono, procedural), **ORB-based** (síncrono, orientado a objetos), **MOM-based** (asíncrono, orientado a mensajes).
- MOM trabaja en la capa de **Aplicación**, no de Transporte. Elementos básicos: clientes, mensajes y proveedor MOM (API + herramientas de administración). Arquitectura del proveedor: centralizada, distribuida o combinada. Modelos de cola: N a 1, 1 a N, N a N.

### Ventajas y desventajas
| Ventajas | Desventajas |
|---|---|
| Asincronía | Un componente más en la arquitectura |
| Ruteo inteligente | Pseudosincronización cuando se necesita respuesta |
| Transformación al vuelo | Costo de performance por el salto intermedio |
| Acoplamiento débil | |

### Estándares
| Estándar | Clave |
|---|---|
| HLA IEEE | Java/C++, publish-subscribe, sistemas federados |
| MQTT | IBM, topic-based, retain, brokers ligeros |
| OMG DDS | Publish-subscribe, varios lenguajes |
| XMPP | Ex-Jabber, cliente-servidor, chat de WhatsApp/Zoom |
| **AMQP** | Nivel de aplicación, binario, sobre TCP, con SSL |

### AMQP
- Frame: `SIZE | DOFF | TYPE | CHANNEL` (header), header extendido variable, y `PAYLOAD`.
- Secuencia de conexión: handshake AMQP/SASL → `open` (connection) → `begin` (session) → `attach` (link).

### Los cuatro tipos de exchange (esto se pregunta)
| Tipo | Nombre default | Ruteo |
|---|---|---|
| Direct | `""` y `amq.direct` | Routing key exactamente igual a la binding key |
| Fanout | `amq.fanout` | Sin routing key: a todas las colas ligadas |
| Topic | `amq.topic` | Patrón con wildcards `*` y `#` |
| Headers | `amq.match` (`amq.headers` en RabbitMQ) | Coincidencia de headers con `x-match: all`/`any` |

Atributos de un exchange: `Name`, `Durability`, `Auto-delete`, `Arguments`.

### Brokers
RabbitMQ (Erlang, AMQP + MQTT, plugins, todos los SO), Kafka (streaming, log persistente), Amazon SQS, Google Cloud Pub/Sub, Mosquitto (MQTT, IoT), ActiveMQ, IBM MQ.

### Patrones de RabbitMQ
| Patrón | Flujo |
|---|---|
| Basic delivery | Productor → cola → consumidor |
| Work queue (competing consumers) | Round robin, ack, durabilidad, `prefetch=1` para reparto justo |
| Publish/Subscribe | Productor → exchange → una cola por consumidor (`amq.gen-*`) |
| Request/Reply asíncrono | Cliente manda `reply_to` + `correlation_id` a `rpc_queue`; el servidor responde a `reply_to`; el cliente correlaciona por `correlation_id` |

### Puesta en marcha
- Docker (lo recomendado): `docker run -it -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:4-management`.
- Admin web en `http://localhost:15672`, usuario y contraseña `guest`/`guest`, **solo desde localhost**.
- Alternativas: instaladores oficiales, o CloudAMQP (plan gratuito "Little Lemur", sin tarjeta).

### Garantías de entrega
| Garantía | Mecanismo | Cuándo |
|---|---|---|
| At most once | Sin ack ni reenvío | Telemetría, métricas |
| At least once | Reenvía si no hay ack | Transacciones, chats, IoT |
| Exactly once (processing) | **At least once + idempotencia** | Cuando el efecto no puede duplicarse |

La fórmula "exactly once = at least once + idempotencia" es respuesta de examen.

### Errores y orden
- **Invalid Message Channel** para mensajes mal formados (logging, alertas); **dead-letter queue** para mensajes que fallan repetidamente; `delivery-limit` define cuándo un mensaje se considera envenenado.
- El orden **no está garantizado** en general (a cambio de throughput). En RabbitMQ es FIFO salvo colas de prioridad o sharded.

### RabbitMQ Streams
Nuevo tipo de cola, append-only log estilo Kafka: sirve para fanouts grandes, mensajes que no se eliminan y replay/time-travel; el consumidor elige desde qué punto empezar.

## Methodology
Teórico único y extenso que va de la teoría de middleware a la práctica concreta de RabbitMQ, con los patrones oficiales del tutorial de rabbitmq.com.

## Critique
- La lista oficial de desventajas incluye "débil acoplamiento", que en realidad es una ventaja. Erratum del material; conviene no repetirlo tal cual en un parcial.
- No cubre clustering ni alta disponibilidad del broker: el punto único de falla que MOM introduce queda sin tratar.
- La comparación con RPC llega recién en el tema siguiente, aunque conceptualmente pertenece acá.

## Dicho en clase

### 2026-09-24 ([[Clase 2026-09-24 - Middleware orientado a mensajes, RabbitMQ y AMQP]])
- Teoria completa de MOM, AMQP, exchanges, patrones, garantias de entrega y dead letter queue. Ver el acta.

### 2026-10-08 ([[Clase 2026-10-08 - Letra del Obligatorio 2 y patron RPC sobre RabbitMQ]])
- **La ultima imagen de RabbitMQ en Docker Hub rechaza colas no durables** cuando son no exclusivas y sin autodelete: esa combinacion esta deprecada. Hay que declararlas `durable: true`. Por eso fallaron los ejemplos del 24-09. (stated)
- Pasos fijos: `ConnectionFactory`, conexion, canal. `BasicConsumeAsync` es lo que **pone a escuchar**. (stated)
- Ejemplos mostrados:
  - hello send/receive con ack automatico;
  - `direct_logs` con la severidad como routing key y un bind por severidad;
  - topics con la raiz `app.`. (stated)
- **Request-reply con correlation id**: cola temporal en `ReplyTo`, GUID como correlation id y un diccionario de promesas en el cliente. **Varios servidores en la misma cola no exclusiva** dan escalabilidad horizontal. **Ack manual despues de responder.** Prefetch de 10 mensajes concurrentes. (stated)
- El patron sirve en canales bidireccionales con callback (MOM, WebSocket). **No sirve en TCP pelado ni en HTTP**, que son pedido-respuesta. (stated)

## Connections
- Contrapunto síncrono: [[Tema 5 - RPC y gRPC]] (ahí está la tabla MOM vs RPC, "postal vs teléfono").
- Alternativa de bajo nivel: [[Tema 2 - Sockets y protocolos]]. Criterio de elección: [[Elegir tecnologia de comunicacion]].
- Docker para levantar el broker: [[Complementario - Docker y Cloud]].
- Formato de examen: [[Patrones de examen en Programacion de Redes]].
