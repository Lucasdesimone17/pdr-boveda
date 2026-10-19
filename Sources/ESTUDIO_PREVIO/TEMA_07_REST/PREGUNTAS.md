# Preguntas de autoevaluación — TEMA 7: REST (PDR)

Fuentes: 🟢 `REST APIs 2025.pdf` + `WebSockets.pdf` (oficiales). Intentá responder antes de abrir la pista.

## API y concepción de REST

**1.** Según la fuente, ¿qué debe saber un desarrollador sobre una API y qué NO necesita saber?

<details><summary>Pista</summary>Contrato/especificación vs implementación interna.</details>

**2.** ¿Quién inventó REST, en qué documento y en qué año?

<details><summary>Pista</summary>Un doctorado del año 2000, del mismo autor de la especificación de HTTP 1.1.</details>

## RESTful, recursos y restricciones

**3.** Explicá con tus palabras qué significa "Representational State Transfer" usando el concepto de recurso.

<details><summary>Pista</summary>Un recurso tiene estado; el cliente pide o propone una representación de ese estado.</details>

**4.** Nombrá las 6 propiedades que REST busca alcanzar en un sistema distribuido.

<details><summary>Pista</summary>Simplicidad, escalabilidad, modificabilidad, performance, portabilidad, fiabilidad.</details>

**5.** De las 9 restricciones RESTful, ¿cuál es la única opcional? ¿Qué permite hacer?

<details><summary>Pista</summary>Descargar código ejecutable, como applets o scripts.</details>

**6.** ¿Qué significa que una API sea Stateless? ¿Qué NO puede hacer el servidor?

<details><summary>Pista</summary>Cada request debe traer toda la información necesaria — no hay "memoria" de requests anteriores del lado servidor.</details>

**7.** ¿Cuáles son las 4 sub-restricciones de Uniform Interface?

<details><summary>Pista</summary>Identificación de recursos, mensajes auto-descriptivos, manipular recursos, HATEOAS.</details>

## Falacias y fundamentos

**8.** Elegí 2 de las 8 falacias de la programación en redes y explicá por qué son falsas en la práctica.

<details><summary>Pista</summary>"La red es confiable", "la latencia es cero", "el ancho de banda es infinito", etc. — pensá en qué pasa realmente en Internet.</details>

**9.** ¿En qué 3 estándares (con sus RFC) se apoya RESTful según la fuente?

<details><summary>Pista</summary>Uno para el protocolo de transporte de aplicación, uno para identificar recursos, uno para tipos de contenido.</details>

## Diseño de URI y verbos

**10.** ¿Por qué `api.ort.edu.uy/deleteTeacher` es un mal diseño de URI según la fuente? ¿Cómo se debería expresar esa operación?

<details><summary>Pista</summary>El verbo ya existe como método HTTP — no hace falta repetirlo en el path.</details>

**11.** ¿Por qué `POST` es el único verbo HTTP no idempotente? Dá un ejemplo contrastando con `DELETE`.

<details><summary>Pista</summary>Cada POST crea algo nuevo; ejecutar el mismo DELETE varias veces sobre el mismo recurso deja el sistema en el mismo estado final.</details>

## Diseño de respuestas y versionado

**12.** Según la fuente, ¿por qué una API REST no debería devolver solamente el código de status ante un error?

<details><summary>Pista</summary>Pensá en un error 400 (Bad Request) — ¿qué necesita saber el cliente para corregir su petición?</details>

**13.** Nombrá las 4 estrategias oficiales de versionado de APIs, con un ejemplo de cada una.

<details><summary>Pista</summary>URI Path, Query String, Headers propios, Headers de aceptación (y tipos de contexto).</details>

**14.** ¿Por qué "el versionado de APIs no implica versionado de producto"?

<details><summary>Pista</summary>Una app puede cambiar de UI y venderse como "nueva versión" sin tocar el contrato de la API que consume.</details>

## Streaming, polling y alternativas a REST

**15.** Compará Short Polling y Long Polling: ¿en qué se diferencian respecto al momento en que el servidor responde?

<details><summary>Pista</summary>Uno responde de inmediato siempre (aunque no haya datos); el otro retiene la respuesta hasta tener datos o hasta un timeout.</details>

**16.** ¿Por qué la fuente dice que WebSocket "no sigue REST"?

<details><summary>Pista</summary>Pensá en la restricción Stateless: ¿una conexión WebSocket mantiene o no estado entre mensajes?</details>

**17.** ¿Qué es un Webhook y qué dos consideraciones importantes menciona la fuente al implementarlo?

<details><summary>Pista</summary>"HTTP Callback"; manejo de fallas y seguridad (confiar en quién llama al URL).</details>

## WebSockets

**18.** Según el RFC 6455 citado en la fuente, ¿quién inicia siempre la conexión WebSocket? ¿Puede el servidor enviar mensajes sin que el cliente pida algo primero?

<details><summary>Pista</summary>El handshake inicial siempre lo dispara uno de los dos lados — después la comunicación es libre en ambos sentidos.</details>

**19.** Completá la tabla comparativa: WebSocket es full duplex y REST es ___ duplex. ¿Qué significa esa diferencia?

<details><summary>Pista</summary>Semi duplex — en REST la comunicación va en un sentido por vez (request, después response); en WebSocket ambos lados pueden enviar en simultáneo.</details>

**20.** Nombrá los 4 eventos del ciclo de vida de un WebSocket según la fuente, en orden.

<details><summary>Pista</summary>Inicio, mensajes, fallo, cierre.</details>

---

## Respuestas completas

<details>
<summary>Ver todas las respuestas</summary>

**1.** Un desarrollador debe saber **cómo utilizar** la API: qué operaciones ofrece y qué hace cada una (el contrato/especificación). No necesita saber **qué hace internamente** la API — su implementación interna queda oculta, solo importa el comportamiento que expone hacia afuera.

**2.** REST fue inventado por **Roy Fielding**, quien presentó los principios REST en su **tesis de doctorado**, en el **año 2000**.

**3.** Un recurso (cualquier información llamable: documento, imagen, persona, colección, etc.) tiene un **estado** que puede cambiar con el tiempo. Cuando un cliente pide ese recurso, el servidor le **transfiere una representación** de ese estado (por ejemplo, un JSON). Cuando el cliente quiere modificarlo, le propone al servidor una **nueva representación** del estado del recurso. Ese intercambio de representaciones del estado, a través de la red, es literalmente lo que significa "Representational State Transfer".

**4.** Simplicidad, Escalabilidad, Modificabilidad, Performance, Portabilidad, Fiabilidad.

**5.** La única restricción opcional es **Code on demand**. Permite extender la funcionalidad de un cliente descargando código ejecutable (por ejemplo applets o scripts) desde el servidor, lo que simplifica el cliente porque no necesita tener programadas de antemano todas las features.

**6.** Que sea Stateless significa que **cada solicitud del cliente al servidor debe contener toda la información necesaria** para que el servidor la entienda y la procese. El servidor **no puede** apoyarse en ningún contexto guardado de solicitudes anteriores de ese mismo cliente — no hay "sesión" recordada del lado servidor entre una petición y la siguiente.

**7.** Identificación de recursos, mensajes auto-descriptivos, manipulación de recursos mediante representación, e hipermedia como motor de estados de la aplicación (HATEOAS).

**8.** Cualquier combinación razonada es válida. Ejemplos: "La red es confiable" es falsa porque los paquetes se pueden perder, las conexiones se pueden cortar, los servidores pueden caerse — cualquier sistema distribuido real tiene que asumir que las comunicaciones pueden fallar. "La latencia es cero" es falsa porque toda transmisión de datos por una red física (cables, fibra, radio) tarda un tiempo no nulo, que además varía según la distancia, la congestión y la cantidad de saltos intermedios — por eso hay que diseñar pensando en la latencia, no ignorarla.

**9.** HTTP 1.1 (RFC 2616), URI (RFC 3986) y MIME (RFCs 2045/2046).

**10.** Porque mete un **verbo** (`delete`) directamente en la URI, cuando ese verbo ya se expresa de forma explícita y separada a través del método HTTP (`DELETE`). Repetirlo en la URI es redundante y rompe la convención de que las URIs deberían ser sustantivos (recursos), no acciones. La forma correcta sería usar el método HTTP `DELETE` sobre una URI como `api.ort.edu.uy/teachers/{id}`.

**11.** Porque cada vez que se ejecuta un `POST`, se **crea una nueva entidad** — ejecutarlo dos veces con los mismos datos normalmente genera dos entidades distintas, es decir, un resultado diferente cada vez. En cambio, con `DELETE`, si borrás una entidad y después volvés a ejecutar el mismo `DELETE` sobre ese mismo recurso *n* veces más, el resultado final es siempre el mismo: la entidad sigue sin existir (más allá de que la segunda vez en adelante pueda devolver un 404 en vez de un 200, el *estado del sistema* no vuelve a cambiar).

**12.** Porque un código de status por sí solo no siempre es suficientemente explicativo. La fuente aclara que un código de "recurso no encontrado" (404) sí es bastante auto-explicativo por sí mismo, pero una petición incorrecta (400, Bad Request) generalmente requiere **más datos en el body de la respuesta** para que el cliente entienda exactamente qué estuvo mal en su petición y pueda corregirla — aunque esto depende de la situación concreta y de mantener la consistencia con el resto del diseño de la API.

**13.** (a) **URI Path**: `api.ort.edu.uy/v1/students` vs `api.ort.edu.uy/v2/students`. (b) **Query String**: `api.ort.edu.uy/students?version=1.0` vs `?version=2.0`. (c) **Headers**: un header propio como `Api-Version: 1.0` vs `Api-Version: 2.0`. (d) **Headers de aceptación / Tipos de contexto**: `Accept: application/json; version=1.0` vs `version=2.0`, o directamente `Accept: application/version.model.v1+json` vs `v2+json`.

**14.** Porque son dos conceptos independientes: el versionado de API se refiere al **contrato técnico** que expone el backend (qué endpoints, qué formato de datos), mientras que el versionado de producto es una decisión de **marketing/negocio** sobre cómo se le presenta la aplicación al usuario final. Una aplicación puede lanzar una interfaz visual completamente nueva y venderla como "versión 2.0" del producto, sin que eso obligue a cambiar ni una sola línea de la API REST que consume por debajo — siguen siendo la misma v1 del contrato.

**15.** En Short Polling, el servidor responde **inmediatamente** a cada pedido del cliente, tenga o no datos nuevos para ofrecer — por eso el cliente tiene que preguntar seguido y en general recibe muchas respuestas vacías ("no, todavía no"). En Long Polling, en cambio, el servidor **retiene** la respuesta y no contesta de inmediato: espera hasta tener datos nuevos disponibles, o hasta que se cumpla un tiempo límite (timeout), lo que reduce la cantidad de peticiones "vacías" que hace el cliente.

**16.** Porque WebSocket rompe la restricción **Stateless** de REST: abre una **conexión persistente** entre cliente y servidor que se mantiene con estado a lo largo del tiempo (el servidor "recuerda" esa conexión abierta y puede escribirle al cliente en cualquier momento), en vez del modelo de peticiones independientes y sin estado que caracteriza a REST.

**17.** Un Webhook es un **callback HTTP**: un mecanismo que le permite a un servidor decirle a otro servidor a dónde mandar una respuesta cuando la tenga lista, en vez de que ese otro servidor tenga que estar preguntando ("polling") todo el tiempo si ya está lista. Las dos consideraciones importantes que menciona la fuente son: (a) el **manejo de fallas** — qué pasa si, por algún motivo, nunca se llega a llamar al URL de callback; y (b) la **seguridad** — cómo puede el receptor confiar en que quien está llamando a su URL es realmente el servicio que se supone que debía llamarlo, y no un tercero malicioso.

**18.** Según el RFC 6455 citado, **el cliente es siempre quien inicia la conexión** (el handshake). Una vez establecida esa conexión inicial, sí: el servidor **puede enviar mensajes al cliente sin que medie ningún request previo del cliente** en ese momento puntual — esa es justamente una de las claves que distingue a WebSocket de HTTP tradicional.

**19.** REST es **semi duplex**. La diferencia es que en un modelo semi duplex la comunicación va en un solo sentido por vez: primero el cliente manda el request completo, después el servidor manda el response completo, y recién ahí puede haber una siguiente interacción. En un modelo full duplex (WebSocket), ambos extremos pueden enviar datos **al mismo tiempo**, de forma simultánea e independiente, sobre la misma conexión ya abierta.

**20.** En orden: **Open** (inicio de la conexión, tras el handshake) → **Message** (envío/recepción de mensajes, en cualquiera de los dos sentidos, mientras la conexión está abierta) → **Error** (se dispara si hay un fallo en la conexión) → **Close** (cierre de la conexión, ya sea normal o por error).

</details>
