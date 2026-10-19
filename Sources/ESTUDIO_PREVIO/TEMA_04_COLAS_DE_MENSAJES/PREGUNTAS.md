# Preguntas de autoevaluación — TEMA 4: Colas de Mensajes (PDR)

Fuente: 🟢 `MOM - Soluciones de mensajeria.pdf` (oficial). Intentá responder antes de abrir la pista.

## Middleware en general

**1.** ¿Dónde se ubica un middleware dentro de la arquitectura de un sistema (entre qué capas), y qué características debería tener según la fuente?

<details><summary>Pista</summary>Pensá en capa de Aplicación arriba y capa de Plataforma (SO + red) abajo — el middleware queda en el medio.</details>

**2.** Nombrá los tres tipos centrales de middleware que distingue la materia, y decí cuál es síncrono y cuál asíncrono.

<details><summary>Pista</summary>RPC, ORB y MOM. Dos de los tres comparten el mismo modelo de comunicación.</details>

## MOM — definición y elementos

**3.** ¿Por qué se dice que con MOM "ya no trabajamos en la capa de Transporte, sino en la de Aplicación"?

<details><summary>Pista</summary>Con Sockets programabas directamente con TCP/UDP; con MOM sos cliente de un servidor de mensajería.</details>

**4.** Nombrá los 3 elementos básicos de todo sistema MOM.

<details><summary>Pista</summary>Quién manda, qué manda, y quién lo administra en el medio.</details>

**5.** Un sistema MOM puede usar arquitectura centralizada o distribuida para enrutar y entregar mensajes. Explicá la diferencia.

<details><summary>Pista</summary>¿Hay un único servidor de mensajes, o cada máquina cliente participa del enrutamiento?</details>

## Ventajas y desventajas

**6.** Explicá con tus palabras por qué la asincronía de MOM permite que "si la aplicación receptora falla, el remitente no se ve afectado".

<details><summary>Pista</summary>Pensá en el almacenamiento persistente de la cola de mensajes.</details>

**7.** La fuente lista "débil acoplamiento" tanto como algo positivo (en la sección de definición) como bajo el título "Desventajas del uso de MOM". ¿Se te ocurre una explicación razonable de por qué el débil acoplamiento también podría verse como una desventaja?

<details><summary>Pista</summary>Menos control directo sobre el flujo completo del sistema puede complicar razonar sobre qué está pasando en cada momento — no es una respuesta que dé explícitamente la fuente.</details>

## Estándares y AMQP

**8.** ¿Qué diferencia hay entre un Direct Exchange y un Fanout Exchange en cuanto a la necesidad de una routing key?

<details><summary>Pista</summary>Uno compara la routing key contra la binding key de cada cola; el otro ignora completamente ese dato.</details>

**9.** En el ejemplo oficial del Headers Exchange, un mensaje con `format: zip, type: log` no coincide con ninguno de los tres bindings mostrados. ¿Qué binding necesitaría existir para que ese mensaje llegara a alguna cola?

<details><summary>Pista</summary>Necesitarías un binding con `x-match: any` (o `all`) que incluya `format=zip` o `type=log` entre sus condiciones.</details>

## Patrones de RabbitMQ

**10.** Explicá la diferencia entre el patrón Work Queue (competing consumers) y Publish/Subscribe en RabbitMQ.

<details><summary>Pista</summary>En uno, los mensajes se reparten entre consumidores que compiten por una misma cola; en el otro, cada consumidor recibe su propia copia.</details>

**11.** En el patrón Request/Reply asíncrono de RabbitMQ, ¿para qué sirven `reply_to` y `correlation_id` respectivamente?

<details><summary>Pista</summary>Uno le dice al server a qué cola mandar la respuesta; el otro le permite al cliente identificar cuál de sus pedidos corresponde a la respuesta que llegó.</details>

## Garantías de entrega y manejo de errores

**12.** Completá y explicá la fórmula oficial: Exactly Once = ____ + ____.

<details><summary>Pista</summary>Una de las tres garantías de entrega, combinada con una propiedad que evita procesar el mismo mensaje dos veces.</details>

**13.** ¿Cuál es la diferencia entre enviar un mensaje a la Invalid Message Channel y enviarlo a una Dead-letter queue?

<details><summary>Pista</summary>Pensá en el motivo del fallo: ¿el mensaje está mal formado desde el principio, o falló repetidas veces al intentar procesarlo?</details>

---

## Respuestas completas

<details>
<summary>Ver todas las respuestas</summary>

**1.** El middleware se ubica entre la capa de aplicación (donde corren los programas: Application 1, Application 2, componentes) y la capa de plataforma (el sistema operativo y los servicios de red subyacentes). Según la fuente, debería exponer una API de comunicación, ser administrable, confiable, con accountability, escalable, y soportar múltiples arquitecturas y lenguajes.

**2.** Los tres tipos centrales son RPC-based middleware, ORB-based middleware y MOM-based middleware. RPC y ORB son síncronos (el cliente espera la respuesta antes de continuar); MOM es asíncrono (el cliente envía el mensaje y sigue con su trabajo sin esperar).

**3.** Porque con Sockets el desarrollador trabaja directamente con los protocolos de la capa de Transporte (TCP/UDP), gestionando conexiones, puertos y bytes crudos. Con MOM, en cambio, el cliente ya no habla ese lenguaje de bajo nivel: envía y recibe mensajes a través de una API de la capa de Aplicación, siendo "cliente de un servidor de mensajería" que se encarga de todo el trabajo de transporte por debajo.

**4.** Los 3 elementos básicos son: los clientes (quienes envían y reciben mensajes), los mensajes (la información intercambiada), y el proveedor MOM (que incluye una API para que los clientes interactúen, y herramientas administrativas para gestionar el sistema).

**5.** En la arquitectura centralizada, existe un único servidor de mensajes que concentra todo el enrutamiento y la entrega. En la arquitectura distribuida, esas funciones de enrutamiento y entrega se reparten entre cada máquina cliente, sin depender de un único punto central. Algunos productos MOM combinan ambos enfoques según el caso.

**6.** La mayoría de los sistemas MOM asíncronos ofrecen almacenamiento persistente para respaldar la cola de mensajes. Esto significa que, aunque la aplicación receptora se caiga o esté apagada, los mensajes que el remitente sigue enviando no se pierden: simplemente se acumulan en la cola persistente, esperando a ser procesados cuando el receptor vuelva a estar disponible. El remitente nunca necesita saber ni preocuparse por el estado del receptor en cada instante.

**7.** Aunque la fuente no lo explica directamente, una lectura razonable es que el débil acoplamiento, si bien mejora la resiliencia y la independencia entre componentes, también reduce el control y la visibilidad directa que tiene el desarrollador sobre el flujo completo de una operación: como emisor y receptor nunca interactúan directamente, se vuelve más difícil razonar de punta a punta sobre qué está pasando con un mensaje en cada momento, depurar problemas, o garantizar cierto orden estricto de ejecución entre distintos componentes del sistema.

**8.** Un Direct Exchange necesita que la routing key del mensaje coincida exactamente con la binding key configurada en cada cola para decidir a dónde enviarlo — es una comparación de igualdad. Un Fanout Exchange, en cambio, ignora completamente la routing key (no la necesita): simplemente envía una copia del mensaje a todas las colas que estén vinculadas a él, sin ningún criterio de selección.

**9.** Necesitaría existir un binding cuyas condiciones incluyan `format=zip` o `type=log` (con `x-match: any`, que exige coincidir con al menos una condición), o un binding que exija exactamente `format=zip` y `type=log` juntos (con `x-match: all`). En el ejemplo oficial, el único binding que menciona `format=zip` es el de Queue C, pero exige `x-match: all` con `format=zip`, `type=report` y `z-match=all` simultáneamente — como el mensaje tiene `type: log` y no `type: report`, no coincide con ese binding tampoco, por eso Queue C no recibe nada.

**10.** En Work Queue (competing consumers), todos los consumidores están vinculados a la **misma** cola y compiten por sus mensajes: cada mensaje individual lo procesa solo uno de los consumidores disponibles (reparto tipo round robin, con fair dispatching usando `prefetch`). En Publish/Subscribe, cada consumidor recibe su **propia** cola (generada automáticamente por el broker, con nombres tipo `amq.gen-...`), de forma que un mismo mensaje publicado le llega a **todos** los consumidores suscritos, cada uno con su copia independiente.

**11.** `reply_to` le indica al servidor a qué cola específica tiene que enviar la respuesta una vez que procese el pedido — es la "dirección de vuelta" del cliente. `correlation_id` es un identificador único que el cliente genera para cada pedido; cuando llega una respuesta a la cola de `reply_to`, el cliente compara el `correlation_id` de esa respuesta con los IDs de sus pedidos pendientes para saber exactamente a cuál de ellos corresponde — esto es necesario porque un mismo cliente puede tener varios pedidos en simultáneo esperando respuesta por la misma cola.

**12.** La fórmula oficial es: **Exactly Once (processing) = At least once delivery + Idempotence.** At least once garantiza que el mensaje se reenvía hasta que se confirma su recepción (pudiendo llegar duplicado en el proceso), y la idempotencia agrega la capacidad de detectar si un mensaje ya fue procesado antes (por ejemplo, revisando si su ID ya existe en la base de datos) para no volver a procesarlo — la combinación de ambas cosas logra que el efecto neto sea "exactamente una vez", aunque el mensaje técnicamente pueda haber llegado más de una vez a nivel de transporte.

**13.** La Invalid Message Channel recibe mensajes que **no se pueden procesar desde el principio** por algún problema inherente a su contenido (datos faltantes, estructura mal formada, etc.) — es un problema de validez del mensaje en sí. La Dead-letter queue, en cambio, recibe mensajes que **fallan repetidamente** al intentar procesarlos (superando un `delivery-limit` configurado) o que no se pueden entregar — el mensaje puede estar perfectamente bien formado, pero algo en el proceso de entrega o procesamiento sigue fallando. Ambos mecanismos sirven para el mismo propósito general (permitir logging, debugging y análisis posterior sin bloquear el resto del sistema), pero atienden a motivos de fallo distintos.

</details>
