REST APIS
Programación de Redes – Universidad ORT

• API
• RESTful
• HTTP
TEMARIO

• Application Program Interface
• Es un contrato(especificación)
| • Una  | API  |     | es  | un  | conjunto  |     |     | de  |
| ------ | ---- | --- | --- | --- | --------- | --- | --- | --- |
API
| operaciones  |     |     |     | o  servicios  |     |     | junto  |     |
| ------------ | --- | --- | --- | ------------- | --- | --- | ------ | --- |
con una descripción de lo que
hacen.
| • Un      | desarrollador  |       |       |     |               | no  | debe  |     |
| --------- | -------------- | ----- | ----- | --- | ------------- | --- | ----- | --- |
| saber     |                | que   | hace  |     | internamente  |     |       |     |
| la  API,  |                | pero  |       | si  | saber         |     | como  |     |
utilizarla.

| • REST     | fue  inventado  |           | por  | Roy  |
| ---------- | --------------- | --------- | ---- | ---- |
| Fielding,  |                 | presentó  |      | los  |
principios REST en su tesis en
el año 2000.
CONCEPCIÓN
| • REST        | es  | un  | estilo  | de  |
| ------------- | --- | --- | ------- | --- |
| arquitectura  |     | o   | patrón  | de  |
diseño para APIs para sistemas
distribuidos.

RESTFUL
REST define una serie de limitaciones para los sistemas distribuidos, y
un conjunto de propiedades que deben alcanzar los mismos:
• Simplicidad
• Escalabilidad
• Modificabilidad
• Performance
• Portabilidad
• Fiabilidad

RESTFUL - Recursos
| • Clave  | de  de  | la  información  | (datos)  |     | de  abstracción:  |     |
| -------- | ------- | ---------------- | -------- | --- | ----------------- | --- |
Recursos
| • Es  | cualquier  | información  | que  | puede  | ser  | llamada:  |
| ----- | ---------- | ------------ | ---- | ------ | ---- | --------- |
documento, imágenes, servicio, personas, colecciones, etc.
• Contienen estado (el cual puede cambiar con el tiempo)

RESTFUL - Recursos
• A petición, un recurso puede transferir una
representación de su estado(transfer state) a un cliente
• Un cliente podrá transferir una propuesta de una nueva
representación (estado) de un recurso.
Representational State Transfer

RESTFUL - Restricciones
Constraint Propiedades
Clientes-Servidor Separación de los asuntos: simplicidad, escalabilidad y evolucionable.
Stateless Escalabilidad, la fiabilidad, visibilidad
Cacheable Escalabilidad y performance
Uniform Interface Simplicidad, evolucionable y fácil de leer.
Sistema en capas Escalabilidad, la seguridad e integración con legados
Identificación de los recursos Simplicidad, escalabilidad. Requiere uniform interface

RESTFUL - Restricciones
Constraint Propiedades
Manipulación mediante Evolucionable.
representación Requiere uniform interface
Requiere las condiciones de
Mensajes auto descriptivos stateless, cacheable, uniform interface y
arquitectura en capas.
Escalabilidad, fiabilidad, evolucionable,
Hipermedia como motor de
performance.
estados de la aplicación
Requiere uniform interface.

RESTFUL
• Una arquitectura que presenta todos estas restricciones
es RESTful.
• Una arquitectura podrán añadir más restricciones o
relajar las restricciones existentes.
• REST no es un conjunto de axiomas, o incluso una
especificación. Es un estilo de arquitectura.

RESTFUL - Restricción Cliente-Servidor
● Arquitectura cliente-servidor
● Permite al cliente y al servidor evolucionar independientemente
● Principio de separation of concerns
● Objetivos:
○ Simplicidad, escalabilidad y evolucionable

RESTFUL - Restricción Uniform Interface
● Definir una interfaz uniforme entre componentes
● Implementaciones desacopladas de los servicios que proveen.
● Información transferida de una forma estandarizada
● Objetivos:
○ Alienta las implementaciones independientes y evolutivas
○ Simplicidad, evolucionable y fácil de leer.

RESTFUL - Restricción Uniform Interface
● Continuación:
● 4 restricciones de las interfaces RESTful:
1. Identificación de recursos
2. Mensajes auto-descriptivos
3. Manipular recursos
4. Hipermedia como motor de estados de la aplicación (HATEOAS)

RESTFUL - Restricción Stateless
● Cada solicitud del cliente al servidor debe contener toda la
información necesaria para comprender la solicitud y no pueden
tomar ventaja de cualquier contexto almacenado en el servidor.
● Objetivos:
○ Escalabilidad, fiabilidad, visibilidad

RESTFUL - Restricción Cacheable
● Los clientes pueden cachear las respuestas
● Cada response del server deve ser declarada cacheable o
non-cacheable
● Mejora la eficiencia de la red
● Objetivos:
○ Escalabilidad y performance

RESTFUL - Restricción Layered System
● Cada componente no puede ver más allá de las capas con las que
interactúa.
● Se puede desplegar la API en el server A y almacenar la data en el
server B
● El cliente no sabe si está conectado al servidor final o a un
intermediario en otra capa
● Objetivos:
○ Escalabilidad, la seguridad e integración con servicio legacy

RESTFUL - Restricción Code on demand
(opcional)
● OPCIONAL
● REST permite extender la funcionalidad descargando código
ejecutable (ej. applets o scripts)
● Simplifica los clientes (menos features que programar)
● Objetivos:
○ Extensibilidad, simplicidad

1. La red es confiable
2. La latencia es cero
Las 8 falacias
3. Ancho de banda es infinito
de la
4. La red es segura
programación
5. Topología no cambia
en redes
6. Hay un único administrador
7. Coste de transporte es cero
8. La red es homogénea

Conclusión - ¿Qué es RESTFUL?
• “REST is a coordinated set of architectural constraints that attempts
to minimize latency and network communication while at the same
time maximizing the independence and scalability of component
implementations. This is achieved by placing constraints on connector
semantics where other styles have focused on component semantics.
REST enables the caching and reuse of interactions, dynamic
substitutability of components, and processing of actions by
intermediaries, thereby meeting the needs of an Internet-scale
distributed hypermedia system.” - Roy Fielding
• Architectural Styles and the Design of Network-based Software
Architectures

Roy Fielding
• Roy es uno de los principales autores de la especificación de HTTP
1.0, HTTP 1.1, y URI.
• Creador del concepto RESTFull en su tesis de doctorado.
• Co-fundador de The Apache Software Foundation, co-autor del
original Apache Web Server, autor de libwww-perl

• HTTP 1.1 (RFC 2616)
¿En qué
estándares se
• URI (RFC 3986)
apoya
RESTFUL?
• MIME(RFCs 2045/2046)

DISEÑO – DISEÑO PRIMERO
• Fixes (no se pueden hacer después de publicar)
• AD-HOC Endpoints (es muy fácil tener demasiados endpoints)
• Requerimientos (ayuda a entender el problema mejor)
• Madurez (si el diseño es bueno, la API puede madurar)

INTRODUCCIÓN - HISTORIA DE LA COMPUTACIÓN
DISTRIBUIDA

INTRODUCCIÓN - ¿CÓMO FUNCIONA HTTP?

INTRODUCCIÓN - PETICIÓN (REQUEST) HTTP

INTRODUCCIÓN - PETICIÓN (REQUEST) HTTP

INTRODUCCIÓN - PETICIÓN (REQUEST) HTTP

INTRODUCCIÓN - RESPUESTA (RESPONSE) HTTP

INTRODUCCIÓN - RESPUESTA (RESPONSE) HTTP

INTRODUCCIÓN - RESPUESTA (RESPONSE) HTTP

DISEÑO - PARTES DE REST API (REQUEST)

DISEÑO - PARTES DE REST API (RESPONSE)

DISEÑO - UNIFORM RESOURCE IDENTIFIER (URI)
• Son una ruta a recursos
• api.ort.edu.uy/students
• Query path se utiliza para recursos únicos (no tiene porque ser
PKs)
• api.ort.edu.uy/teachers/pepeBotella
• Query parameters (con un query string) se utiliza para filtrado
• api.ort.edu.uy/students?lastName=Botella&active=true

DISEÑO - UNIFORM RESOURCE IDENTIFIER (URI)
• No deberíamos usar verbos en la en la URI misma, para eso
tenemos el verbo explícitamente por separado.
• api.ort.edu.uy/saveStudent
• api.ort.edu.uy/getStudent
• api.ort.edu.uy/deleteTeacher

DISEÑO - UNIFORM RESOURCE IDENTIFIER (URI)
• Deberíamos de preferir sustantivos y que la acción esté dada por
el verbo.
• api.ort.edu.uy/students
• api.ort.edu.uy/teachers
• api.ort.edu.uy/courses

DISEÑO - RECURSOS
•Los recursos no son sólo las
| entidades  |            | que        | tenemos  |      | en  la  |
| ---------- | ---------- | ---------- | -------- | ---- | ------- |
| base       | de         | datos,     | sino     | que  | son     |
| esas       | entidades  |            |          | con  | un      |
| contexto   |            | asociado.  |          | Eso  | es  lo  |
que exponemos

DISEÑO - VERBOS
• GET (obtener un recurso)
• POST (crear un nuevo recurso)
• PUT (actualizar un recurso existente)
• PATCH (actualizar parcialmente un recurso existente)
• DELETE (borrar un recurso existente)

DISEÑO – VERBOS Y RECURSOS
| RECURSO | GET | POST | PUT / PATCH | DELETE |
| ------- | --- | ---- | ----------- | ------ |
/students Obtener lista de  Crear nuevo  Actualizar varios  -
|                 | estudiantes     | estudiante | estudiantes      |                  |
| --------------- | --------------- | ---------- | ---------------- | ---------------- |
| /students/99851 | Obtener al      | -          | Actualizar los   | Borrar al        |
|                 | estudiante con  |            | datos del        | estudiante 99851 |
|                 | ID 99851        |            | estudiante 99851 |                  |

DISEÑO - IDEMPOTENCIA
• GET, PUT, PATCH y DELETE son idempotentes
• POST es el único verbo no idempotentes
El post produce un resultado distinto cada vez que se ejecuta (se
crea una nueva entidad), un delete puede borrar una entidad pero si
luego de borrarla se ejecuta n veces se obtiene el mismo resultado.

DISEÑO – DISEÑANDO RESULTADOS
• No se deben exponer detalles de la implementación (como puede ser el
lenguaje)
• Hay que ser consistente
• Si pensamos que estamos haciendo algo no tan correcto o cuestionable,
nuevamente, seamos consistentes con lo que venimos haciendo.

DISEÑO – DISEÑANDO RESULTADOS
{
Tomemos en cuenta
"totalResults": 5014,
• Paginado "nextPageUrl": "/teachers?page=3",
"results": [{
• Hypermedia "id": 13854654156,
"name": “Pepe Botella",
• Consistencia (si, de nuevo!)
"since": 2022,
...
}]
}

ESCENARIOS COMPLEJOS - ASOCIACIONES
• Debemos utilizar navegación por URIs
api.ort.edu.uy/teachers/pepeBotella/courses
api.ort.edu.uy/courses/networkProgramming/teachers
• Los datos que retornemos en casos donde hay asociaciones deberían ser iguales
api.ort.edu.uy/teachers/pepeBotella/courses
api.ort.edu.uy/courses

ESCENARIOS COMPLEJOS – MANEJO DE ERRORES
• Una API REST no sólo debería retornar el código de Status como respuesta.
• Un código de un recurso no encontrado si es bastante auto explicatorio.
• Una petición incorrecta generalmente (más bien siempre) requiere más datos,
pero esto depende de la situación y la consistencia.

VERSIONADO DE APIS
Versionado de APIs? ¿Por qué, cómo, cuando?

VERSIONADO DE APIS
Una vez publicada una API, queda ahí, es un contrato, algo escrito en
una piedra.

VERSIONADO DE APIS
En realidad…

VERSIONADO DE APIS
Los requerimientos cambian

VERSIONADO DE APIS
Tenemos que darle soporte a viejos y nuevos clientes

VERSIONADO DE APIS
El versionado de APIs no implica versionado de
| productos,  |     | una     | aplicación  | puede  | tener  | una    |
| ----------- | --- | ------- | ----------- | ------ | ------ | ------ |
| nueva       | UI  | siendo  | vendida     | como   | una    | nueva  |
versión del producto y utilizar las mismas APIs.

VERSIONADO DE APIS - ESTRATEGIAS
• URIs
• Headers
• Headers de aceptación
• Tipos de contexto

VERSIONADO DE APIS – VERSIONANDO POR URIS
• URI Path
• api.ort.edu.uy/v1/students
• api.ort.edu.uy/v2/students
• Query String
• api.ort.edu.uy/students?version=1.0
• api.ort.edu.uy/students?version=2.0

VERSIONADO DE APIS – HEADERS
GET /api/helloWorld HTTP/1.1
Host: localhost:44236
Content-Type: application/json
Api-Version: 1.0
GET /api/helloWorld HTTP/1.1
Host: localhost:44236
Content-Type: application/json
Api-Version: 2.0

VERSIONADO DE APIS – HEADERS DE ACEPTACIÓN
GET /api/helloWorld HTTP/1.1
Host: localhost:44236
Content-Type: application/json
Accept: application/json ; version=1.0
GET /api/helloWorld HTTP/1.1
Host: localhost:44236
Content-Type: application/json
Accept: application/json ; version=2.0

VERSIONADO DE APIS – TIPOS DE CONTEXTO
GET /api/helloWorld HTTP/1.1
Host: localhost:44236
Content-Type: application/version.model.v1+json/
Accept: application/version.model.v1+json
GET /api/helloWorld HTTP/1.1
Host: localhost:44236
Content-Type: application/version.model.v2+json/
Accept: application/version.model.v2+json

Existe streaming en REST/HTTP?
o siempre es request/response…

Alternativas al modelo Request/Response clásico
Short Polling El cliente realiza peticiones frecuentes y el servidor responde
enseguida.
Long Polling El cliente espera una respuesta; el servidor responde cuando
hay datos o tras un tiempo límite.
SSE (Server-Sent El servidor envía datos de forma continua al cliente a través de
Events)
una conexión HTTP abierta.
WebSocket Comunicación bidireccional entre cliente y servidor (no sigue
REST).
Webhook El servidor llama al cliente (o a otro servidor) cuando ocurre un
evento. "HTTP Callback"

Long-running Task/processes
• Ya vimos cómo MOM se puede usar para procesar eventos de manera
desacoplada entre los productores y los consumidores
• Que pasa si el productor precisa la respuesta del consumidor?

Polling - Short vs long

Polling - Cómo diseñar una API para tareas
Hay varias maneras:
"Long-running"
● REST API Design for
Long-Running Tasks
● Adidas | API
Guidelines
● Long running
operations - Microsoft
Fabric REST APIs

Server-sent events
• Similar al server-side streaming de gRPC
• El servidor puede enviar varios mensajes al cliente
• Unidireccional
• Soportado por los browsers (a diferencia de gRPC)

Websockets
No vamos a entrar en
mayor detalle (2025 S1)
• Comunicación bidireccional
• No sigue REST
• Conexión persistente (stateful)
• Ejemplos: chat, videojuegos, apps para colaborar en vivo etc.

Webhook
• Callback HTTP
• Permite que un servidor le diga a otro a
donde mandar la respuesta (cuando la tenga)
• No es una tecnología nueva. Es una manera de
utilizar los URL HTTP ya existentes
• Sin embargo, hay empresas que crearon un
"estándar webhook" para estandarizar la
comunicación
• Evitar la necesidad de polling entre
servidores
• Consideraciones importantes:
• Manejo de fallas (que pasa si nunca llaman al
URL)
• Seguridad (poder confiar que el servicio que
llama al URL fue el que lo recibió)
• También utilizado para integración con
aplicaciones como Teams, Discord, Slack etc.
E.g. mi servidor web manda un POST request
a Slack cuando falla mi app.