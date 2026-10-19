# TEMA 4 — Message Oriented Middlewares (PDR)

**Cobertura oficial (programa, 8 unidades):** "Message Oriented Middlewares: Definición, características principales, implementación."

**Fuente principal:** 🟢 `TEORICO/04_MOM/MOM - Soluciones de mensajeria.pdf` (diapositiva oficial de clase, título interno "MOM: Message-oriented middleware", 70 diapositivas). Se cita como **[Pág. N]**.

**Reescrito de cero (11/08)** a partir de esta fuente oficial, reemplazando la versión anterior basada en `0.RESUMEN-STUDOCU.pdf`. El material oficial es mucho más extenso que el resumen previo, especialmente en los patrones concretos de RabbitMQ (Work Queue, Publish/Subscribe, Request/Reply asíncrono, garantías de entrega, dead-letter queue, mensajes envenenados). StuDocu no fue necesario para completar ningún hueco en esta reescritura.

**Tags:** 🟢 Oficial (esta diapositiva) · 🔵 Evidencia de evaluación (parciales/exámenes) · 🟠 Complementario (explicación agregada, no oficial).

---

## Sección de recuperación — preguntas antes de leer

1. Si ya viste el patrón Tarea con Evento en Threads o `async`/`await` en Paralelismo: ¿por qué "mandar un mensaje y seguir con lo tuyo" te suena parecido a algo que ya estudiaste?
2. ¿Qué problema tendría un sistema donde el emisor de un mensaje necesita que el receptor esté prendido y disponible en ese instante exacto?
3. Si tenés una cola de trabajos y dos "workers" disponibles, ¿cómo repartirías el trabajo entre ellos de la forma más simple posible?
4. ¿Qué harías si un mensaje llega mal formado y no se puede procesar nunca, en vez de reintentarlo infinitas veces?

---

## 1. Qué es un middleware

🟢 **Definición (cita Wikipedia, tal como la trae la diapositiva)** [Pág. 4]: "Middleware, también conocido como lógica de intercambio de información entre aplicaciones (interlogical) o agente intermedio, es un sistema de software que ofrece servicios y funciones comunes para las aplicaciones." En general se encarga de gestión de datos, servicios de aplicaciones, mensajería, autenticación y gestión de API. "Simplifica el trabajo de los programadores en la compleja tarea de generar las conexiones y sincronizaciones que son necesarias en los sistemas distribuidos."

🟢 **Origen histórico** [Pág. 5]: la palabra se remonta a 1968, usada en la *1968 NATO Software Engineering Conference* para referirse a bibliotecas o software intermediario entre la comunicación de aplicaciones de diferentes proveedores. Etimológicamente, "Middle·ware" = "artículo/producto del medio": software/hardware que intermedia entre otros dos o más componentes para lograr una operación de mayor valor. Las razones de esa necesidad pueden ser tecnológicas, espaciales, lógicas, o combinaciones.

🟢 **Ubicación arquitectónica** [Pág. 7]: el middleware se sitúa entre la capa de aplicación y la capa de plataforma (sistema operativo y servicios de red subyacentes). Expone una API de comunicación y debe ser administrable, confiable, con accountability, escalable y multi arquitectura/lenguaje.

🟠 **Complementario (analogía oficial de la propia diapositiva, Pág. 3):** el middleware es como un acueducto entre una fuente de agua (un sistema) y el Imperio Romano (otro sistema) — no genera el agua ni la consume, solo la transporta de forma confiable entre los dos extremos.

### Autoevaluación rápida
- ¿Por qué la fuente dice que las definiciones de middleware "tienen un sesgo orientado al tema principal de los autores" y sugiere tener la visión más amplia posible?

---

## 2. Tipos de middleware

🟢 La diapositiva presenta un árbol de clasificación con dos ramas principales — **Integración** (Orientado a procesos, Orientado a objetos, MOM, Orientado a componente, Agente) y **Aplicación** (DAM, Escritorio, Web-Based, Tiempo Real, Específico) [Pág. 8] — y luego destaca tres tipos centrales para la materia [Pág. 9]:

- **RPC-based middleware** (Remote Procedure Call).
- **ORB-based middleware** (Object Request Broker).
- **MOM-based middleware** (Message Oriented Middleware) — el tema de esta unidad.

🟢 **Otras clasificaciones mencionadas** [Pág. 10]: middleware de datos/bases de datos (simplifica acceso a BD), middleware de API (crear/exponer/gestionar APIs), middleware transaccional (apoyo a transacciones de datos), middleware de flujo de datos asíncrono (replica un flujo en un almacén intermedio — buffer, productor/consumidor), middleware de dispositivos (IoT), middleware de portal (integra capacidades en una app compuesta), middleware de robótica (integra hardware/firmware/software de múltiples fabricantes), entre muchos otros.

🟢 **Definición oficial de MOM dentro de esta clasificación** [Pág. 10]: "El middleware orientado a mensajes (MOM) permite intercambiar mensajes entre componentes de aplicaciones que utilizan distintos protocolos de mensajería para comunicarse. Además de traducir o transformar los mensajes entre aplicaciones, MOM gestiona el enrutamiento de los mensajes para que siempre lleguen a los componentes adecuados en el orden correcto. Algunos ejemplos de MOM son las colas de mensajes y los corredores de mensajes."

### Autoevaluación rápida
- Nombrá 3 tipos de middleware distintos a RPC, ORB y MOM que trae la diapositiva.

---

## 3. MOM en detalle: definición, elementos y arquitecturas

🟢 **Cambio de capa** [Pág. 11]: con Sockets se trabajaba en la capa de Transporte; con MOM se envía y recibe en la capa de **Aplicación** — el cliente se comunica con un servidor de mensajería (Messaging Provider), no directamente con el otro extremo.

🟢 **Elementos básicos de un sistema MOM** [Pág. 12]: clientes, mensajes, y el proveedor MOM (que incluye una API y herramientas administrativas).

🟢 **Arquitecturas del proveedor MOM** [Pág. 12]: puede usar un **servidor de mensajes centralizado**, o puede **distribuir** las funciones de enrutamiento y entrega a cada máquina cliente. Algunos productos combinan ambos enfoques.

🟢 **Flujo básico** [Pág. 14]: el cliente hace una llamada a la API para enviar un mensaje a un destino administrado por el proveedor; esa llamada invoca los servicios del proveedor para enrutar y entregar el mensaje; una vez enviado, el cliente **continúa con su trabajo**, confiando en que el proveedor retiene el mensaje hasta que un cliente receptor lo recupera. El modelo basado en mensajes, junto con la mediación del proveedor, "hace posible crear un sistema de componentes débilmente acoplados."

🟠 **Nota de la propia diapositiva sobre esta idea** [Pág. 14]: "La idea es mandar un mensaje (como WhatsApp, e-mail, etc.) y luego continuamos con lo nuestro. La entrega del mensaje ya no es nuestra responsabilidad." También aparece la sigla **SOP: State-Oriented Programming** junto a esta idea, sin desarrollo adicional en la fuente. **Pendiente de verificar:** qué significa exactamente "SOP" en este contexto — la diapositiva la menciona una sola vez, sin definirla.

🟢 **Otras propiedades de MOM** [Pág. 15]: provee nombrado jerárquico, seguridad y aislamiento. No se habla directamente con el extremo de la aplicación, por lo que el otro extremo puede estar ocupado o apagado sin afectar al que produce el mensaje. El consumidor decide en qué momento extraer los mensajes. El servidor guarda los mensajes.

### Modelos de cola (N a 1, 1 a N, N a N)

🟢 [Pág. 16]: los sistemas MOM basados en colas admiten distintos modelos de participantes, con clientes y tipos de mensaje potencialmente distintos en cada extremo:

| Modelo | Descripción |
|---|---|
| **N a 1** | Varios productores envían a una cola, un único consumidor la procesa |
| **1 a N** | Un único productor envía a una cola, varios consumidores la procesan |
| **N a N** | Varios productores y varios consumidores comparten la misma cola |

### Autoevaluación rápida
- ¿Por qué el modelo de proveedor MOM "centralizado" y el "distribuido" resuelven el mismo problema de formas distintas?
- ¿Qué significa que el consumidor "decida en qué momento extraer los mensajes"?

---

## 4. Ventajas y desventajas de MOM

🟢 **Ventajas** [Pág. 17]: asincronía, transformación de mensajes "on the fly", ruteo inteligente.

### Asincronía en profundidad

🟢 [Pág. 18]: en los sistemas asincrónicos, las colas de mensajes proporcionan **almacenamiento temporal** cuando el programa de destino está ocupado o no está conectado. La mayoría de los sistemas MOM asíncronos ofrecen además **almacenamiento persistente** para respaldar la cola. Esto significa que remitente y receptor no necesitan conectarse a la red al mismo tiempo (entrega asíncrona), se resuelven problemas de conectividad intermitente, y si la aplicación receptora falla, los remitentes pueden seguir enviando sin verse afectados — los mensajes se acumulan en la cola para su procesamiento posterior.

### Ruteo en profundidad

🟢 [Pág. 19]: muchas implementaciones dependen de un sistema de cola de mensajes. Algunas dejan la lógica de enrutamiento en la propia capa de mensajería; otras dependen de las aplicaciones cliente para dar esa información; otras combinan ambos paradigmas. Algunas implementaciones usan paradigmas de **difusión** o **multidifusión**.

### Transformación en profundidad

🟢 [Pág. 20]: un sistema MOM puede transformar los mensajes y enrutarlos para que coincidan con los requisitos del remitente o el destinatario. Junto con enrutamiento y difusión/multidifusión, una aplicación puede enviar un mensaje en su formato nativo, y dos o más aplicaciones pueden recibir cada una una copia en su propio formato nativo. Muchos sistemas MOM modernos ofrecen herramientas de transformación (o mapeo) que permiten definir reglas mediante una simple operación de arrastrar y soltar en una GUI.

🟢 **Desventajas** [Pág. 21]: componente extra de arquitectura, pseudosincronización o SOP, performance (costo de asincronía y paralelismo), débil acoplamiento.

🟠 **Complementario:** llama la atención que "débil acoplamiento" aparezca listado tanto como ventaja implícita (Pág. 14-15) como bajo el título "Desventajas" (Pág. 21) — la diapositiva no explica por qué lo lista ahí. Una lectura razonable es que el débil acoplamiento, si bien mejora la resiliencia, también puede dificultar razonar sobre el flujo completo del sistema (menos control directo sobre quién procesa qué y cuándo). **Pendiente de verificar** contra la clase.

### Autoevaluación rápida
- Explicá con tus palabras por qué el almacenamiento persistente de la cola es lo que permite que el receptor "falle sin afectar" al remitente.

---

## 5. Ejemplos de aplicación (oficiales)

🟢 [Pág. 23–26]:

| Ejemplo | Flujo |
|---|---|
| **Procesamiento de PDF** | Usuario sube información a la web → se crea un PDF de forma asincrónica (vía cola en RabbitMQ/CloudAMQP) → se envía email al usuario con el PDF |
| **Softonic** | Usuarios suben archivos → se escanean por virus → se distribuyen al resto de servicios |
| **Cambiar tamaño de imágenes** | Distintas partes de la app necesitan distintos tamaños de una foto → al subir una foto se manda a una cola → el servicio de *Image Scaling* la consume a medida que tiene capacidad |
| **ElephantSQL** (hosting de bases de datos) | Necesitan backups diarios de varias BD → se agrega un mensaje por cada base y se consumen según capacidad del servidor de backups → se usan **prioridades** para los backups pedidos explícitamente por usuarios |

### Autoevaluación rápida
- En el ejemplo de ElephantSQL, ¿para qué sirve específicamente el uso de prioridades en los mensajes?

---

## 6. Estándares de MOM

🟢 [Pág. 27]: HLA IEEE (High Level Architecture), MQTT (MQ Telemetry Transport), OMG DDS (Object Management Group Data Distribution Services), XMPP (eXtensible Messaging and Presence Protocol), AMQP (Advance Message Queue Protocol).

| Estándar | Características oficiales |
|---|---|
| **HLA IEEE** [Pág. 28] | Define servicios; API en Java o C++; permite modelo Publish-Subscribe; usado en sistemas federados |
| **MQTT** [Pág. 29] | Desarrollado por IBM; basado en topics (topic-based); comunicación basada en retención (retain based); basado en brokers (open source o pagos) |
| **OMG DDS** [Pág. 30] | Publish-Subscribe; API en Java, C++, Ruby, ADA |
| **XMPP** [Pág. 31] | Antiguamente conocido como Jabber; arquitectura cliente-servidor; concebido para mensajería instantánea; lo usan actualmente WhatsApp y el chat de Zoom |
| **AMQP** [Pág. 32] | Application Level Protocol; codificación binaria; basado en TCP; con soporte SSL |

### Autoevaluación rápida
- ¿Qué tienen en común HLA IEEE y OMG DDS en cuanto al modelo de comunicación que soportan?

---

## 7. AMQP en profundidad

🟢 **"Hello, world" example routing** [Pág. 33]: `Publisher` → **Publish** → `Exchange` → **Routes** → `Queue` → **Consumes** → `Consumer`.

🟢 **Estructura del frame** [Pág. 34]:

| Campo | Ubicación | Contenido |
|---|---|---|
| `SIZE` | Frame Header | Tamaño total del frame |
| `DOFF`, `TYPE`, `CHANNEL` | Frame Header | Data offset, tipo de frame, canal de la conexión |
| Extended Header | Extended Header | Contenido variable según el tipo |
| `PAYLOAD` | Frame Body | El contenido real del mensaje |

🟢 **Establecimiento de la conexión (secuencia oficial)** [Pág. 35]: dos `Container` intercambian, en orden: (1) `header AMQP / SASL handshake` — abre la conexión TCP; (2) `open (max frame size, channel max, ...)` — abre la `connection`; (3) `begin (in/out window, handle max, ...)` — abre la `session`; (4) `attach (name, source, target, handle, ...)` — abre el `link`.

### Tipos de Exchange (AMQP 0-9-1)

🟢 [Pág. 37]: son entidades a las que se envían los mensajes; toman un mensaje y lo enrutan a cero o más colas; el algoritmo de enrutamiento depende del tipo de exchange y de reglas llamadas *bindings*. Los brokers de AMQP ofrecen 4 tipos:

| Tipo | Nombre pre-declarado por defecto |
|---|---|
| Direct exchange | `""` (string vacío) y `amq.direct` |
| Fanout exchange | `amq.fanout` |
| Topic exchange | `amq.topic` |
| Headers exchange | `amq.match` (y `amq.headers` en RabbitMQ) |

🟢 **Direct Exchange** [Pág. 38–39]: compara la routing key exacta del mensaje contra la binding key de cada cola — ejemplo oficial: un exchange `images` envía con `routing_key => "images.archive"` a `archiver1` y `archiver2`, `routing_key => "images.crop"` a `cropper`, `routing_key => "images.resize"` a `resizer`. Existe un exchange por defecto llamado `""` (string vacío) al que cualquier cola queda automáticamente vinculada con su propio nombre como binding key.

🟢 **Fanout Exchange** [Pág. 40]: no necesita routing key — enruta a **todas** las colas vinculadas.

🟢 **Headers Exchange** [Pág. 65]: en vez de una routing key, compara los *headers* del mensaje contra las condiciones definidas en cada binding (`x-match: all` exige que coincidan todos los headers listados; `x-match: any` alcanza con que coincida alguno). Ejemplo oficial: Message 1 (`format: pdf, type: report`) matchea el binding `format=pdf, type=report, x-match=all` y va a Queue A; Message 2 y 3 (con `format: pdf` o `type: log`) matchean el binding `x-match=any` de Queue B; ningún mensaje matchea el binding de Queue C (`format=zip, type=report, x-match=all`), así que Queue C no recibe nada.

🟢 **Topic Exchange** [Pág. 66–69]: usa una estructura de árbol de bindings/topics; enruta a una o varias colas según la coincidencia entre la routing key del mensaje y el patrón (con wildcards) usado para vincular cada cola. Se usa habitualmente para variaciones de publisher/subscriber y para enrutamiento de mensajes de multidifusión. Ejemplos oficiales de uso: distribuir datos por ubicación geográfica (puntos de venta), procesamiento de tareas en segundo plano repartido por tipo de tarea, actualización de precios de acciones, orquestación de servicios en la nube, compilaciones específicas por SO/arquitectura.

🟢 **Atributos de un Exchange** [Pág. 41]: `Name`, `Durability` (el exchange sobrevive a un reinicio del broker), `Auto-delete` (se borra cuando se desvincula su última cola), `Arguments` (opcionales, usados por plugins y funciones específicas del broker).

### Autoevaluación rápida
- ¿Por qué un Fanout Exchange "no necesita" routing key, a diferencia de Direct y Topic?
- En el ejemplo del Headers Exchange, ¿por qué Queue C no recibe ningún mensaje de los tres enviados?

---

## 8. Message Brokers

🟢 Brokers mencionados en la diapositiva [Pág. 42]: RabbitMQ, Kafka, Amazon SQS, Google Cloud Pub/Sub, Mosquitto, ActiveMQ, IBM MQ.

🟠 **Complementario (características generales de cada uno, no desarrolladas en detalle en esta diapositiva salvo RabbitMQ):**

| Broker | Nota |
|---|---|
| RabbitMQ | Implementación de MOM que la diapositiva desarrolla en profundidad (ver sección 9) |
| Kafka | Plataforma de streaming de eventos, log persistente |
| Amazon SQS | Servicio de colas gestionado de AWS |
| Google Cloud Pub/Sub | Servicio de mensajería gestionado de Google Cloud |
| Mosquitto | Broker ligero especializado en MQTT, popular en IoT |
| ActiveMQ | Broker de Apache |
| IBM MQ | Solución corporativa histórica y robusta |

---

## 9. RabbitMQ como implementación de MOM

🟢 **Características generales** [Pág. 44]: implementación de MOM; soporta operaciones sincrónicas y asincrónicas; soporta operaciones débilmente y fuertemente acopladas; transaccionalidad en el send/receive; prioriza las entregas; plazo de entrega basado en características del mensaje; filtrado de la entrega.

🟢 **Datos técnicos** [Pág. 45]: open source; implementado en Erlang; soporta AMQP, MQTT y otros protocolos; soporta plugins (ej. el plugin administrativo); funciona en todos los sistemas operativos.

🟢 **Flujo básico** [Pág. 46]: `Producer` → **Publish** → `Broker (RabbitMQ)` ← **Consume / Subscribe** → `Consumer`.

### Basic message delivery

🟢 [Pág. 47]: el productor (`P`) publica en una cola con nombre (ej. `hello`); el consumidor (`C`) consume de esa misma cola.

### Work Queue (patrón competing consumers)

🟢 [Pág. 48]: características — Work Queue, **Round Robin dispatching** (reparto rotativo), **message acknowledgment** (confirmación de procesamiento), **message durability** (persistencia del mensaje).

🟢 **Fair dispatching** [Pág. 49]: con `prefetch=1`, cada consumidor (`C1`, `C2`) solo recibe un mensaje nuevo de la cola una vez que confirmó el anterior — evita que un consumidor lento acumule mensajes sin procesar mientras otro más rápido queda ocioso.

### Manejo de errores y acknowledge

🟢 [Pág. 50]: es importante confirmarle al MOM (`Ack`) que el mensaje se procesó correctamente, para poder borrarlo de la cola. Si el procesamiento falla (o el mensaje no es confirmado después de un tiempo), se reenvía el mensaje al mismo u otro consumidor.

### Publish/Subscribe

🟢 [Pág. 51]: flujo `Producer → Exchange → Queue` y luego `Queue → Exchange → Consumer`. En el patrón Publish/Subscribe, cada consumidor puede recibir su propia cola generada automáticamente por el broker (nombres tipo `amq.gen-RQ6...`, `amq.gen-As8...`), de forma que todos reciban una copia del mensaje.

### Request/Reply asíncrono (patrón RPC sobre RabbitMQ)

🟢 [Pág. 52]: patrón general — `Requestor` envía un `Request` por un `Request Channel`; `Replier` procesa y devuelve un `Reply` por un `Reply Channel`. En RabbitMQ concretamente: el cliente envía un mensaje con `reply_to=amq.gen-Xa2...` y `correlation_id=abc` a una `rpc_queue`; el `Server` la procesa y responde a la cola indicada en `reply_to`; el cliente identifica la respuesta correcta comparando el `correlation_id` recibido con el que envió.

### Autoevaluación rápida
- ¿Para qué sirve el `correlation_id` en el patrón Request/Reply si el cliente ya sabe a qué cola le va a llegar la respuesta (`reply_to`)?
- ¿Qué problema resuelve el `prefetch=1` en el fair dispatching?

---

## 10. Garantías de entrega de mensajes

🟢 [Pág. 53]:

| Garantía | Cómo funciona | Trade-off | Casos de uso típicos |
|---|---|---|---|
| **At most once** ("best-effort") | Se envía una única vez, no se espera ack ni se reenvía | Mínimo uso de recursos, pero se pueden perder datos | Telemetría, métricas, datos de eventos que se reemplazan o envían a ritmo alto |
| **At least once** | Se envía y, si no hay ack en cierto tiempo, se reenvía | Cierto overhead de confirmación, pero asegura que no queden mensajes sin procesar | Transacciones, chats, control de IoT |
| **Exactly once (processing)** | Mensajes idempotentes + ack; si llega un duplicado, se detecta (ej. por ID ya guardado en la BD) y no se reprocesa | Requiere diseño idempotente explícito | Casos donde ni perder ni duplicar procesamiento es aceptable |

🟢 **Fórmula oficial** [Pág. 53]: **Exactly Once = At least once delivery + Idempotence.**

### Autoevaluación rápida
- ¿Por qué "exactly once" no es una garantía de *entrega* per se, sino de *procesamiento* (según remarca la propia diapositiva con el subrayado en "processing")?

---

## 11. Manejo de mensajes problemáticos

### Invalid Message Channel

🟢 [Pág. 54–55]: distintas situaciones pueden impedir procesar un mensaje (datos faltantes, errores de estructura, etc.). Enviar esos mensajes a un canal especial (Invalid Message Channel) permite hacer logging, debugging, monitoreo y alertas después — siempre recordando que todo esto ocurre de forma asincrónica.

### Dead-letter queue

🟢 [Pág. 56]: algunos brokers permiten configurar una dead-letter queue. Los mensajes que no se pueden entregar, o que fallan repetidas veces, terminan reenviados (rerouted) ahí. Permite guardar los mensajes fallidos para analizar después qué ocurrió.

### Orden de los mensajes

🟢 [Pág. 57]: muchas veces los brokers **no garantizan** el orden de los mensajes — esto permite mayor throughput (se pueden enviar mensajes sin esperar la confirmación del anterior). Ejemplo con Amazon SQS: las colas "Standard" ofrecen *at-least-once delivery* con *best-effort ordering* (no garantizado); las colas "FIFO" sí garantizan orden estricto y *exactly-once processing*. En RabbitMQ: las colas son "ordered collections of messages" — se encolan y desencolan en orden FIFO, **pero ese orden FIFO no está garantizado para colas de prioridad (priority) ni para colas particionadas (sharded)**.

### Mensaje envenenado (Poison Message)

🟢 [Pág. 58]: es un mensaje que falla repetidas veces y nunca se logra procesar — puede fallar siempre contra el mismo consumidor, o pasar por varios consumidores distintos y fallar en todos. En RabbitMQ (y otras soluciones) se puede configurar un **delivery-limit**: si la cantidad de intentos de procesar el mensaje supera ese límite, se lo envía a la dead-letter queue (o equivalente).

🟠 **Complementario:** esto corresponde directamente al punto del temario de Aulas de 6 bloques que menciona explícitamente "dead-letter queues y mensajes envenenados" dentro del bloque de Mensajería.

### Autoevaluación rápida
- ¿Por qué "no garantizar el orden" permite mayor throughput?
- ¿Qué diferencia hay entre reenviar un mensaje que falló una vez y un mensaje "envenenado"?

---

## 12. RabbitMQ Streams

🟢 [Pág. 64]: un tipo de cola más nuevo dentro de RabbitMQ. Casos de uso: fanouts grandes, mensajes que **no se eliminan nunca**, time-travel/replay. El consumidor elige desde qué momento (o mensaje) empezar a consumir. Usa un **append-only log**, de forma similar a lo que hacen otros brokers como Kafka.

---

## 13. Cómo correr RabbitMQ y su interfaz administrativa

🟢 **Opciones para usar RabbitMQ** [Pág. 59]:

| Opción | Detalle |
|---|---|
| Instalación local | Más sencilla; instaladores oficiales para Windows y Mac (Homebrew) |
| Docker (**recomendado** según la fuente) | `docker run -it -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:4-management` |
| CloudAMQP (servicio web) | No requiere correr RabbitMQ localmente; plan gratuito "Little Lemur" sin tarjeta de crédito; "RabbitMQ as a Service" |

🟢 **Interfaz web administrativa** [Pág. 60]: plugin para administrar la instancia desde el navegador — permite visualizar actividad y estado del servicio. Hace falta instalarlo si se usa la versión local y no viene ya incluido. Usuario y contraseña por defecto: `guest`/`guest`. Se accede en `http://localhost:15672`. También existe una CLI. **Importante:** el usuario `guest` solo funciona conectándose desde `localhost`.

---

## Checkpoint oficial de la fuente

🟢 Preguntas de repaso que trae la propia diapositiva [Pág. 61]:

1. ¿Qué significa que la comunicación a través de MOM sea **asincrónica**?
2. ¿Qué desventajas puede tener el uso de MOM?
3. ¿Cuál es la diferencia entre el modelo publish/subscribe y competing consumers (Work Queues)?
4. ¿Qué tipo de exchange podemos utilizar para enviar todos los mensajes a todos los consumidores?
5. MOM se usa para comunicar _________ (procesos/hilos).

🟠 **Complementario (respuestas razonadas, no explícitas en la fuente):**
1. Que el remitente envía el mensaje y sigue con su trabajo sin esperar que el receptor lo procese en el momento — el proveedor MOM retiene el mensaje hasta que el receptor lo recupera.
2. Componente extra de arquitectura para mantener, cierta pseudosincronización o complejidad adicional (SOP), impacto en performance por la naturaleza asíncrona/paralela, y el propio débil acoplamiento como potencial desventaja de control.
3. En publish/subscribe, **cada** consumidor suscrito recibe su propia copia del mensaje (vía colas separadas generadas por el exchange); en competing consumers (work queue), los consumidores **compiten** por los mensajes de una única cola compartida, y cada mensaje lo procesa solo uno de ellos.
4. Un **Fanout Exchange**, porque ignora la routing key y envía a todas las colas vinculadas.
5. **Procesos** — MOM es un mecanismo de comunicación entre procesos (posiblemente en máquinas distintas), no entre hilos dentro de un mismo proceso.

---

## Resumen ejecutivo del tema

1. Un middleware es software que media entre dos o más componentes para facilitar su conectividad e intercambio, ubicado entre la capa de aplicación y la capa de plataforma (SO + red).
2. MOM es uno de los tres tipos centrales de middleware junto a RPC y ORB: mientras estos son síncronos, MOM permite comunicación asíncrona en la capa de Aplicación, con clientes, mensajes y un proveedor MOM (API + herramientas administrativas) que enruta y entrega mensajes, de forma centralizada o distribuida.
3. Las colas MOM soportan modelos N a 1, 1 a N y N a N; sus ventajas principales son asincronía (almacenamiento temporal/persistente, tolerancia a desconexión), ruteo inteligente y transformación de mensajes "on the fly"; sus desventajas incluyen un componente extra de arquitectura y cierto costo de performance.
4. AMQP es el protocolo estándar detrás de RabbitMQ (binario, sobre TCP, con SSL), con 4 tipos de Exchange — Direct (routing key exacta), Fanout (sin routing key, a todas las colas), Topic (patrones con wildcards) y Headers (coincidencia de headers con `x-match: all`/`any`).
5. RabbitMQ implementa varios patrones concretos: Work Queue (competing consumers, con fair dispatching vía `prefetch`), Publish/Subscribe (colas generadas por consumidor), y Request/Reply asíncrono (RPC vía `reply_to` + `correlation_id`).
6. Las garantías de entrega van de menor a mayor costo: At most once (best-effort, puede perder datos), At least once (reintenta hasta confirmar), y Exactly once processing (= At least once + idempotencia).
7. Para mensajes problemáticos existen mecanismos específicos: Invalid Message Channel (logging/debugging), Dead-letter queue (mensajes fallidos para análisis posterior) y `delivery-limit` para detectar mensajes envenenados (poison messages) que nunca se logran procesar.
8. El orden de los mensajes no siempre está garantizado (a cambio de mayor throughput) — en RabbitMQ el orden FIFO se respeta salvo en colas de prioridad o particionadas (sharded).

---

## Repaso espaciado

### Día 3
1. Nombrá los 3 elementos básicos de un sistema MOM.
2. ¿Cuál es la diferencia entre proveedor MOM centralizado y distribuido?
3. Completá: Exactly Once = ____ + ____.
4. ¿Qué tipo de Exchange usarías para un broadcast a todos los consumidores sin importar el contenido del mensaje?
5. Nombrá los 5 estándares de MOM mencionados en la diapositiva.

### Día 7
1. Explicá de memoria el patrón Request/Reply asíncrono de RabbitMQ, incluyendo `reply_to` y `correlation_id`.
2. Diseñá un escenario propio (no de la diapositiva) donde usarías `at most once` delivery, y otro donde usarías `exactly once`.
3. ¿Qué diferencia hay entre un mensaje que va a la Invalid Message Channel y uno que se considera "envenenado" (poison message)? ¿En qué momento uno se convierte en el otro?
4. Repasá sin mirar el resumen: ¿qué información va en el frame header de AMQP y qué en el frame body?

---

## Pendiente de confirmar

- La sigla **SOP (State-Oriented Programming)** aparece mencionada una sola vez en la diapositiva [Pág. 14], junto a la idea de "mandar un mensaje y seguir con lo nuestro", sin ninguna definición ni desarrollo posterior. No hay material oficial adicional que la explique.
- "Débil acoplamiento" aparece listado tanto implícitamente como ventaja (Pág. 14-15) como explícitamente bajo "Desventajas del uso de MOM" (Pág. 21), sin que la fuente aclare la aparente contradicción — se dejó registrada tal cual aparece en el material, con una lectura razonada marcada como 🟠.
- Evidencia de parcial (🔵): `PARCIALES/ANALISIS_DE_FRECUENCIA.md` sigue desactualizado — no refleja los ~30 exámenes/parciales ya descargados en `PARCIALES/`. Este tema no cita evidencia de evaluación hasta que se procese ese análisis.
- No se profundizó en las referencias externas que trae la propia diapositiva (documentación de Oracle sobre ORB, tutoriales oficiales de RabbitMQ) [Pág. 62] — el resumen se basa en el contenido tal como aparece en las diapositivas de clase, no en esos enlaces externos.
