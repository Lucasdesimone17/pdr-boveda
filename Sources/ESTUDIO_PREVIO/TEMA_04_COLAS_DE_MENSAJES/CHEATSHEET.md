# Cheat Sheet — TEMA 4: Colas de Mensajes (PDR)

Fuente: 🟢 `MOM - Soluciones de mensajeria.pdf` (oficial). Sin prosa, solo tablas/sintaxis de referencia rápida.

## Middleware — ubicación y tipos centrales

| Concepto | Detalle |
|---|---|
| Ubicación | Entre capa de Aplicación y capa de Plataforma (SO + red) |
| RPC-based | Síncrono, llama a un procedimiento remoto |
| ORB-based | Síncrono, orientado a objetos |
| MOM-based | Asíncrono, orientado a mensajes |

## MOM — capa y elementos

| Elemento | Detalle |
|---|---|
| Capa de trabajo | Aplicación (no Transporte) |
| Elementos básicos | Clientes, mensajes, proveedor MOM (API + herramientas admin) |
| Arquitectura del proveedor | Centralizada, distribuida, o combinación |
| Modelos de cola | N a 1, 1 a N, N a N |

## Ventajas / Desventajas de MOM

| Ventajas | Desventajas |
|---|---|
| Asincronía | Componente extra de arquitectura |
| Ruteo inteligente | Pseudosincronización / SOP |
| Transformación on the fly | Performance (costo asincronía/paralelismo) |
| | Débil acoplamiento (así lo lista la fuente) |

## Estándares de MOM

| Estándar | Clave |
|---|---|
| HLA IEEE | Java/C++, Publish-Subscribe, sistemas federados |
| MQTT | IBM, topic-based, retain-based, brokers |
| OMG DDS | Publish-Subscribe, Java/C++/Ruby/ADA |
| XMPP | Ex-Jabber, cliente-servidor, WhatsApp/Zoom chat |
| AMQP | Application-level, binario, TCP, SSL |

## AMQP — frame y conexión

```
Frame Header: SIZE | DOFF | TYPE | CHANNEL
Extended Header: (variable)
Frame Body: PAYLOAD
```

Secuencia de conexión: `header AMQP/SASL handshake` → `open (connection)` → `begin (session)` → `attach (link)`.

## AMQP — tipos de Exchange

| Tipo | Nombre default | Routing |
|---|---|---|
| Direct | `""` y `amq.direct` | Routing key exacta == binding key |
| Fanout | `amq.fanout` | Sin routing key, a todas las colas |
| Topic | `amq.topic` | Patrón con wildcards (`*`, `#`) |
| Headers | `amq.match` (`amq.headers` en RabbitMQ) | Coincidencia de headers, `x-match: all`/`any` |

Atributos de un Exchange: `Name`, `Durability`, `Auto-delete`, `Arguments`.

## Message Brokers

| Broker | Nota |
|---|---|
| RabbitMQ | Erlang, AMQP+MQTT, plugins, todos los SO |
| Kafka | Streaming, log persistente |
| Amazon SQS | Colas gestionadas AWS |
| Google Cloud Pub/Sub | Mensajería gestionada GCP |
| Mosquitto | Ligero, MQTT, IoT |
| ActiveMQ | Apache |
| IBM MQ | Corporativo histórico |

## RabbitMQ — patrones

| Patrón | Flujo |
|---|---|
| Basic delivery | `P → Queue → C` |
| Work Queue (competing consumers) | Round robin, ack, durability, `prefetch=1` para fair dispatching |
| Publish/Subscribe | `Producer → Exchange → Queue` (una cola por consumidor, `amq.gen-*`) |
| Request/Reply async | Cliente manda `reply_to` + `correlation_id` a `rpc_queue`; server responde a `reply_to`; cliente matchea por `correlation_id` |

## RabbitMQ — instalación

| Opción | Comando/nota |
|---|---|
| Local | Instaladores oficiales Windows/Mac |
| Docker (recomendado) | `docker run -it -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:4-management` |
| CloudAMQP | Plan gratuito "Little Lemur", sin tarjeta |
| Admin web | `http://localhost:15672`, user/pass `guest`/`guest` — solo funciona desde localhost |

## Garantías de entrega

| Garantía | Mecanismo | Fórmula/uso |
|---|---|---|
| At most once | Sin ack, sin reenvío | Telemetría, métricas |
| At least once | Reenvía si no hay ack | Transacciones, chats, IoT |
| Exactly once (processing) | Idempotencia + ack | **= At least once + Idempotence** |

## Manejo de errores

| Mecanismo | Para qué |
|---|---|
| Invalid Message Channel | Mensajes mal formados → logging/debugging/alertas |
| Dead-letter queue | Mensajes fallidos repetidamente → análisis posterior |
| `delivery-limit` | Define cuándo un mensaje se considera "envenenado" y va a dead-letter |
| Orden no garantizado | Mayor throughput; en RabbitMQ FIFO salvo colas de prioridad/sharded |

## RabbitMQ Streams

| Característica | Detalle |
|---|---|
| Tipo | Nuevo tipo de Queue |
| Casos de uso | Fanouts grandes, mensajes que nunca se eliminan, time-travel/replay |
| Mecanismo | Append-only log (similar a Kafka) |
| Consumidor | Elige desde qué mensaje/momento empezar a consumir |

## Preguntas típicas de parcial

🔵 **Frecuencia histórica:** MOM/RabbitMQ aparece en 13 de 16 letras revisadas. Se repiten diseño de exchanges/colas/routing, acknowledgements y justificación arquitectónica.
