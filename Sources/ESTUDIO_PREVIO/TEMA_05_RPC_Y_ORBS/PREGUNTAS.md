# Preguntas de autoevaluación — TEMA 5: RPC y ORBs (PDR)

Fuente: 🟢 `RPC.pdf` (oficial). Intentá responder antes de abrir la pista.

## RPC — fundamentos

**1.** ¿Qué problema de los mecanismos de bajo nivel (Sockets, Named Pipes, Shared Memory con protocolos propietarios) vino a resolver RPC?

<details><summary>Pista</summary>Pensá en qué pasaba cada vez que había que agregar una funcionalidad nueva a ese tipo de protocolo.</details>

**2.** ¿Por qué se dice que RPC es un mecanismo sincrónico? ¿Qué hace el cliente mientras espera la respuesta?

<details><summary>Pista</summary>El cliente "se suspende a sí mismo" — no sigue haciendo otra cosa en paralelo.</details>

## Evolución: RPC → ORB

**3.** Nombrá las tres plataformas RPC de la "era procedural" mencionadas en la fuente y su empresa/organización asociada.

<details><summary>Pista</summary>Una es de Xerox, otra de Sun Microsystems, otra de Microsoft.</details>

**4.** CORBA, RMI y Remoting son los tres ejemplos oficiales de ORB. ¿Qué tienen en común que los diferencia de RPC clásico?

<details><summary>Pista</summary>Pensá en el paradigma de programación al que se adaptaron.</details>

## Retos y canales

**5.** De los 7 retos que plantea RPC/ORB según la fuente, elegí dos y explicá con tus palabras qué problema describen.

<details><summary>Pista</summary>Localización del servidor, canales, concurrencia, parámetros, heterogeneidad, excepciones, transparencia de objetos remotos.</details>

**6.** ¿Cuál es la ventaja de usar una conexión única (Peer to Peer) frente a conexiones múltiples, en términos de concurrencia?

<details><summary>Pista</summary>Con una sola conexión no hay que coordinar múltiples canales simultáneos entre sí.</details>

## Ubicación, serialización y excepciones

**7.** Nombrá los tres enfoques oficiales para que un cliente encuentre a un servidor, con un ejemplo de cada uno.

<details><summary>Pista</summary>Ruta absoluta (tipo IPEndPoint), directorio, y una estrategia que no sirve en WAN.</details>

**8.** Según la cita oficial de Marshalling, ¿cuál es su objetivo más primordial, y por qué se necesita especialmente en sistemas distribuidos con hardware heterogéneo?

<details><summary>Pista</summary>Pensá en qué pasaría si dos máquinas con arquitecturas distintas interpretaran los mismos bytes de forma diferente.</details>

**9.** ¿Cómo manejan las excepciones remotas los RPC/ORB modernos, según la fuente?

<details><summary>Pista</summary>Hay un proceso de ida (capturar + serializar) y uno de vuelta (deserializar + re-disparar).</details>

## Stubs, Skeletons e IDL

**10.** Explicá la diferencia de responsabilidad entre un Stub y un Skeleton.

<details><summary>Pista</summary>Uno vive del lado cliente y "emula"; el otro vive del lado servidor y "ejecuta sobre el objeto real".</details>

**11.** ¿Qué rol cumple un archivo IDL, y por qué es clave para la interoperabilidad entre lenguajes distintos (ej. C++ y Java)?

<details><summary>Pista</summary>Es un "lenguaje neutral" que actúa de puente — de ahí se generan Stub y Skeleton en cada lenguaje.</details>

## MOM vs RPC

**12.** Usando la metáfora oficial (teléfono vs correo postal), explicá por qué RPC necesita que ambos extremos estén activos simultáneamente y MOM no.

<details><summary>Pista</summary>En una llamada telefónica necesitás que el otro atienda en el momento; con una carta, no.</details>

## gRPC — historia y protocolo

**13.** ¿Qué empresa creó gRPC, en qué año, y qué protocolo de transporte usa?

<details><summary>Pista</summary>2015, empresa que también inventó Protocol Buffers.</details>

**14.** Nombrá al menos 3 de las 4 mejoras de HTTP/2 sobre HTTP/1.1 que menciona la fuente.

<details><summary>Pista</summary>Multiplexed streams, server push, formato binario, compresión de headers.</details>

**15.** Según la cita oficial, ¿por qué los mensajes JSON de REST terminan usando más ancho de banda que los de gRPC?

<details><summary>Pista</summary>Un formato es texto legible, el otro es binario no legible por humanos.</details>

## Protocol Buffers y servicios gRPC

**16.** ¿Cuál es la principal desventaja de Protobuf frente a JSON según la tabla oficial, y qué solución menciona la fuente para mitigarla en debugging?

<details><summary>Pista</summary>"No es self-describing (wire format)" — hay un formato alternativo para poder leerlo igual.</details>

**17.** Describí los 4 tipos de servicio gRPC y dónde usarías cada uno con un ejemplo propio.

<details><summary>Pista</summary>Unario, Server Streaming, Client Streaming, Bidireccional — pensá en un chat, una descarga de datos en vivo, una subida de archivos y una videollamada.</details>

**18.** Según el ejemplo de Netflix, ¿qué dos beneficios concretos obtuvo la empresa al migrar de su propia RPC a gRPC?

<details><summary>Pista</summary>Uno tiene que ver con la velocidad del servicio, el otro con el tiempo que tardan los clientes en salir al mercado.</details>

---

## Respuestas completas

<details>
<summary>Ver todas las respuestas</summary>

**1.** RPC resolvió el problema de mantenibilidad de programar aplicaciones distribuidas directamente sobre mecanismos de bajo nivel (Sockets, Named Pipes, Shared Memory) con protocolos propietarios: cualquier funcionalidad nueva obligaba a modificar el protocolo a mano, lo que hacía esas aplicaciones muy difíciles de mantener. RPC encapsuló toda esa complejidad de comunicación, permitiendo llamar a una función remota "como si fuera local" sin que el programador tuviera que estar pendiente de las comunicaciones.

**2.** Es sincrónico porque, apenas el cliente hace la llamada RPC, se suspende a sí mismo y queda en espera hasta recibir la respuesta del servidor — no continúa ejecutando otro código mientras tanto. El motor de RPC recolecta los datos de la invocación, arma el mensaje, lo envía, y el cliente permanece bloqueado hasta que el servidor desempaqueta, ejecuta y devuelve el resultado.

**3.** Courier (Xerox), ONC RPC (Sun Microsystems) y MSRPC (Microsoft). Las tres eran plataformas genéricas con protocolos de más alto nivel, bastante rígidas pero que resolvían el problema de forma adecuada, típicas de la era de lenguajes procedurales.

**4.** Los tres surgieron para adecuar las tecnologías RPC al nuevo paradigma de programación orientada a objetos — están orientados a arquitecturas de objetos distribuidos, en lugar de simples procedimientos/funciones. El middleware ORB permite distribuir y compartir objetos de la aplicación entre redes heterogéneas, algo que el RPC procedural clásico no contemplaba.

**5.** Ejemplos posibles: (a) "¿De qué modo encuentra el cliente al servidor?" — el cliente necesita algún mecanismo (URL, directorio, broadcast) para saber a dónde dirigir sus llamadas remotas, ya que el servidor no está en la misma máquina/proceso. (b) "¿Cómo se manejan las excepciones?" — si algo falla del lado del servidor, ese error tiene que poder viajar de vuelta al cliente y comportarse como si hubiera ocurrido localmente, en vez de perderse o romper la comunicación. (Cualquier combinación de los 7 retos listados en la fuente es válida si se explica correctamente.)

**6.** Con una conexión única (especialmente si es Peer to Peer), no existen problemas de concurrencia a nivel de canal porque no hay múltiples conexiones compitiendo entre sí por los mismos recursos de red — hay un solo canal a coordinar. La contracara es que, si no es Peer to Peer, la sincronización se vuelve más compleja porque hay que resolverla a nivel de tablas de bloqueos y enriqueciendo el protocolo mismo, en vez de dejar que el propio canal la resuelva "gratis" como pasa con conexiones múltiples.

**7.** (a) Ruta absoluta (URL): se conoce exactamente la ruta de la infraestructura a comunicar, similar a un IPEndPoint — ejemplo oficial: Remoting. (b) Directorio: el cliente solo conoce la ruta de un directorio común donde están publicadas las infraestructuras corriendo — ejemplos oficiales: RMI y UDDI. (c) Descubrimiento automático vía broadcast — la fuente aclara explícitamente que esta estrategia no es útil en entornos WAN (solo sirve en redes locales).

**8.** Según la cita oficial, el objetivo más primordial del Marshalling es la transferencia de los datos a través de la red, transformando su representación a un formato adecuado según normas específicas. Es especialmente necesario en sistemas distribuidos porque distintos tipos de hardware pueden representar internamente los mismos datos de formas diferentes; si esos datos se transfieren o se guardan en disco sin convertirlos a un formato común, la interpretación puede fallar y generar errores al reconstruir la información del otro lado.

**9.** Los RPC/ORB modernos tienen la capacidad de capturar las excepciones que se producen en el destino de la llamada (el servidor) y serializarlas para enviarlas de vuelta hacia el origen (el cliente). Del lado del origen, se detecta el mensaje de excepción recibido, se deserializa, y se vuelve a disparar esa excepción como si se hubiese producido de forma local — de modo que el código del cliente puede capturarla con un manejo de excepciones normal, sin enterarse de que en realidad ocurrió en otra máquina.

**10.** El Stub es una pieza de código que vive del lado del cliente y "emula" al objeto remoto en el medio local — el cliente le habla al Stub como si fuera el objeto real, y el Stub se encarga de empaquetar la llamada y mandarla por la red. El Skeleton vive del lado del servidor: captura las llamadas que llegan desde el cliente y las ejecuta sobre el objeto concreto real, devolviendo después el resultado. Cada objeto que se quiera distribuir necesita su propio par Stub/Skeleton.

**11.** Un archivo IDL (Interface Definition Language) describe una interfaz en un lenguaje neutral, independiente de cualquier lenguaje de programación específico. Esto es clave para la interoperabilidad porque permite que componentes escritos en lenguajes distintos (por ejemplo, uno en C++ y otro en Java) puedan comunicarse: el archivo IDL se pasa por un compilador IDL, que genera automáticamente el Stub y el código de RPC Run Time correspondientes a cada lenguaje/plataforma involucrada, actuando como puente entre los dos sistemas.

**12.** RPC funciona como una llamada telefónica: para que la comunicación ocurra, ambos extremos (quien llama y quien atiende) tienen que estar presentes y disponibles al mismo tiempo, de forma sincrónica — si el servidor no está activo, la llamada simplemente no puede completarse. MOM, en cambio, funciona como el servicio postal: el remitente puede escribir y enviar una carta (mensaje) sin que el destinatario esté presente en ese momento; la carta queda esperando (en la cola) hasta que el destinatario la lea cuando pueda, de forma asincrónica y diferida en el tiempo.

**13.** gRPC fue creado por Google en 2015, como proyecto de código abierto, y utiliza HTTP/2 como protocolo de transporte.

**14.** Cualquier 3 de estas 4: Multiplexed Streams (múltiples peticiones/respuestas por una sola conexión, evitando congestión), Server Push (el servidor envía recursos anticipadamente sin que el cliente los pida), Formato binario en lugar de texto (más eficiente, compacto y menos propenso a errores que texto plano), Compresión de headers (las cabeceras se empaquetan en un bloque comprimido único en vez de repetirse sin comprimir en cada request).

**15.** Porque REST intercambia mensajes JSON en un formato de texto legible por humanos, lo que hace que el tamaño base del mensaje sea mayor y por lo tanto use más ancho de banda; gRPC, en cambio, usa Protobuf como IDL de mensajes, un formato binario no legible por humanos pero mucho más compacto y eficiente para serializar/deserializar los mismos datos estructurados.

**16.** La principal desventaja de Protobuf frente a JSON, según la tabla oficial, es que "no es self-describing" (formato wire) — a diferencia de JSON, que es legible directamente por una persona, un mensaje Protobuf crudo no se puede interpretar a simple vista sin conocer previamente su esquema `.proto`. La fuente menciona que, para mitigar esto en tareas de debugging, se puede usar un formato ASCII alternativo que sí permite inspeccionar los datos de forma legible.

**17.** Servicio Unario: el cliente hace una única petición y el servidor responde una única vez — ejemplo propio: consultar el saldo de una cuenta. Server Streaming: el cliente hace una petición y el servidor devuelve un stream de respuestas — ejemplo propio: pedir la cotización de una acción y recibir actualizaciones continuas de precio. Client Streaming: el cliente envía un stream de peticiones y el servidor responde una sola vez al final — ejemplo propio: subir un archivo grande en fragmentos y recibir una confirmación única de "archivo recibido". Stream Bidireccional: ambos lados envían y reciben datos en el mismo stream abierto, con el flujo decidido por la aplicación — ejemplo propio: una videollamada o un chat en tiempo real.

**18.** Según la fuente, el impacto de migrar de su propia implementación de RPC a gRPC se tradujo en una bajada de la latencia del servicio, además de una disminución del tiempo de comercialización ("time to market") de clientes. Hoy en día, gran parte de la comunicación interna de servicio a servicio dentro de Netflix se ejecuta en gRPC.

</details>
