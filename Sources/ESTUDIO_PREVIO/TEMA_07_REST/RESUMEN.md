# TEMA 7 — REST (PDR)

**Cobertura oficial (programa, 8 unidades):** *"REST: Conceptos principales. Definición de arquitectura REST. Seguridad. Manejo de tecnologías."*

**Fuentes principales:** 🟢 `TEORICO/07_REST/REST APIs 2025.pdf` (62 diapositivas) y 🟢 `TEORICO/07_REST/WebSockets.pdf` (9 diapositivas) — ambos oficiales. Reemplazan de cero la versión anterior de este RESUMEN, basada en `0.RESUMEN-STUDOCU.pdf`.

**Evidencia de evaluación (🔵):** ninguna citable todavía. `PARCIALES/ANALISIS_DE_FRECUENCIA.md` sigue sin procesar — mismo pendiente transversal que Temas 3, 4, 5 y Sockets.

**Convención de tags:** 🟢 Oficial · 🔵 Evidencia de evaluación · 🟠 Complementario (agregado por el asistente).

⚠️ **Sobre "Seguridad" del programa oficial:** el programa pide explícitamente "Seguridad" dentro de este tema. **Ninguna de las dos fuentes oficiales disponibles cubre autenticación, autorización, OAuth, JWT, HTTPS/TLS aplicado a REST, ni ningún otro aspecto de seguridad.** No se inventa contenido — queda marcado como pendiente de verificar al final.

---

## Recuperación — preguntas antes de leer

1. ¿Por qué "REST" no es lo mismo que "API"?
2. ¿Qué significa que una API sea "stateless"?
3. ¿En qué se diferencia un WebSocket de una petición HTTP normal?

---

## 1. API y la concepción de REST

🟢 [REST APIs 2025.pdf, Pág. 3-4]

**API (Application Program Interface):** es un **contrato (especificación)** — un conjunto de operaciones o servicios junto con una descripción de lo que hacen. Un desarrollador no debe saber qué hace internamente la API, pero sí saber cómo utilizarla.

**Concepción de REST:** fue inventado por **Roy Fielding**, quien presentó los principios REST en su **tesis de doctorado en el año 2000**. REST es un **estilo de arquitectura o patrón de diseño** para APIs de sistemas distribuidos.

<details class="autoeval">
<summary>Autoevaluación</summary>
¿Por qué un desarrollador "no debe saber qué hace internamente" una API pero sí "cómo usarla"?
</details>

---

## 2. RESTful: propiedades y restricciones

🟢 [Pág. 5]

REST define una serie de **limitaciones (constraints)** para los sistemas distribuidos, y un conjunto de **propiedades** que deben alcanzar: **Simplicidad, Escalabilidad, Modificabilidad, Performance, Portabilidad, Fiabilidad.**

**Recursos** [Pág. 6-7]: la clave de la abstracción de información en REST es el **Recurso** — cualquier información que puede ser llamada (documento, imágenes, servicio, personas, colecciones, etc.), que **contiene estado** (el cual puede cambiar con el tiempo).

A petición, un recurso puede **transferir una representación de su estado** a un cliente; un cliente podrá transferir una propuesta de una nueva representación (estado) de un recurso. De ahí el nombre: **Re**presentational **S**tate **T**ransfer.

**Una arquitectura que presenta todas las restricciones es RESTful** [Pág. 10]. Una arquitectura puede añadir más restricciones o relajar las existentes. *"REST no es un conjunto de axiomas, o incluso una especificación. Es un estilo de arquitectura."*

<details class="autoeval">
<summary>Autoevaluación</summary>
¿Qué diferencia hay entre "el estado de un recurso" y "la representación de ese estado"?
</details>

---

## 3. Las restricciones RESTful (tabla oficial completa)

🟢 [Pág. 8-9]

| Constraint | Propiedades que aporta |
|---|---|
| Cliente-Servidor | Simplicidad, escalabilidad, evolucionable |
| Stateless | Escalabilidad, fiabilidad, visibilidad |
| Cacheable | Escalabilidad y performance |
| Uniform Interface | Simplicidad, evolucionable, fácil de leer |
| Sistema en capas | Escalabilidad, seguridad, integración con legados |
| Identificación de los recursos | Simplicidad, escalabilidad (requiere Uniform Interface) |
| Manipulación mediante representación | Evolucionable (requiere Uniform Interface) |
| Mensajes auto-descriptivos | Requiere Stateless, Cacheable, Uniform Interface y arquitectura en capas |
| Hipermedia como motor de estados (HATEOAS) | Escalabilidad, fiabilidad, evolucionable, performance (requiere Uniform Interface) |

---

## 4. Restricción Cliente-Servidor

🟢 [Pág. 11]

Arquitectura cliente-servidor que permite al cliente y al servidor **evolucionar independientemente** — principio de **separation of concerns**. Objetivos: simplicidad, escalabilidad y evolucionable.

---

## 5. Restricción Uniform Interface

🟢 [Pág. 12-13]

Define una **interfaz uniforme** entre componentes: implementaciones desacopladas de los servicios que proveen, información transferida de forma estandarizada. Objetivos: alienta implementaciones independientes y evolutivas; simplicidad, evolucionable, fácil de leer.

**4 restricciones de las interfaces RESTful:**

1. Identificación de recursos.
2. Mensajes auto-descriptivos.
3. Manipular recursos (mediante representación).
4. Hipermedia como motor de estados de la aplicación (**HATEOAS**).

---

## 6. Restricción Stateless

🟢 [Pág. 14]

Cada solicitud del cliente al servidor debe contener **toda la información necesaria** para comprender la solicitud, y no puede tomar ventaja de ningún contexto almacenado en el servidor. Objetivos: escalabilidad, fiabilidad, visibilidad.

---

## 7. Restricción Cacheable

🟢 [Pág. 15]

Los clientes pueden cachear las respuestas. Cada response del servidor debe declararse **cacheable o non-cacheable**. Mejora la eficiencia de la red. Objetivos: escalabilidad y performance.

---

## 8. Restricción Layered System (Sistema en capas)

🟢 [Pág. 16]

Cada componente no puede ver más allá de las capas con las que interactúa. Se puede desplegar la API en el server A y almacenar la data en el server B — el cliente no sabe si está conectado al servidor final o a un intermediario en otra capa. Objetivos: escalabilidad, seguridad, integración con servicios legacy.

---

## 9. Restricción Code on demand (opcional)

🟢 [Pág. 17]

**Única restricción opcional.** REST permite extender la funcionalidad descargando código ejecutable (ej. applets o scripts), simplificando los clientes (menos features que programar). Objetivos: extensibilidad, simplicidad.

---

## 10. Las 8 falacias de la programación en redes

🟢 [Pág. 18]

1. La red es confiable.
2. La latencia es cero.
3. Ancho de banda es infinito.
4. La red es segura.
5. La topología no cambia.
6. Hay un único administrador.
7. El costo de transporte es cero.
8. La red es homogénea.

> 🟠 **Complementario.** Estas falacias explican *por qué* REST insiste tanto en Stateless y Cacheable: si asumieras que la red es confiable y de latencia cero, no te importaría el estado ni el cacheo — pero como ninguna de las 8 falacias es cierta en la práctica, esas restricciones son las que hacen que un sistema distribuido real sea escalable y tolerante a fallos.

<details class="autoeval">
<summary>Autoevaluación</summary>
Elegí 3 de las 8 falacias y explicá qué restricción RESTful ayuda a mitigar cada una.
</details>

---

## 11. Conclusión: ¿qué es RESTful? (cita de Fielding) y quién es Roy Fielding

🟢 [Pág. 19-20]

> *"REST is a coordinated set of architectural constraints that attempts to minimize latency and network communication while at the same time maximizing the independence and scalability of component implementations. This is achieved by placing constraints on connector semantics where other styles have focused on component semantics. REST enables the caching and reuse of interactions, dynamic substitutability of components, and processing of actions by intermediaries, thereby meeting the needs of an Internet-scale distributed hypermedia system."*
> — Roy Fielding, *Architectural Styles and the Design of Network-based Software Architectures*

**Roy Fielding:** uno de los principales autores de la especificación de **HTTP 1.0, HTTP 1.1 y URI**. Creador del concepto RESTful en su tesis de doctorado. Co-fundador de **The Apache Software Foundation**, co-autor del Apache Web Server original, autor de `libwww-perl`.

**Estándares en los que se apoya RESTful** [Pág. 21]: **HTTP 1.1 (RFC 2616)**, **URI (RFC 3986)**, **MIME (RFCs 2045/2046)**.

---

## 12. Diseño: diseño primero e historia de la computación distribuida

🟢 [Pág. 22-23]

**Diseño primero, por qué importa:**

- **Fixes:** no se pueden hacer después de publicar.
- **Ad-hoc Endpoints:** es muy fácil terminar con demasiados endpoints sin un diseño previo.
- **Requerimientos:** diseñar primero ayuda a entender mejor el problema.
- **Madurez:** si el diseño es bueno, la API puede madurar.

**Línea de tiempo oficial de APIs distribuidas** (conecta con Tema 5 — RPC y ORBs):

`RPC, Messaging, Queuing (1970s, "Dawn of Distributed Computing")` → `COM/DCOM, CORBA, Java RMI (1990s, "Object Oriented APIs")` → `XMLHTTP, REST, SOAP (2000s)` → `GraphQL (2010s)` → `gRPC (2020s)` — juntas conforman las **"Web-Based APIs"**.

> 🟠 **Complementario.** Esta línea de tiempo es la misma que aparece en `RPC.pdf` (Tema 5) — confirma que REST y gRPC son contemporáneos como "Web-Based APIs" que evolucionaron después de RPC/ORB clásico.

---

## 13. Cómo funciona HTTP: request/response y stateless

🟢 [Pág. 24]

El flujo básico es: cliente envía un **Request** (ej. `POST`, con `Content-Length` y body) → servidor responde con un **Response** (ej. `201`, con `Content-Type` y body). El servidor está marcado explícitamente como **Stateless** en el diagrama oficial.

### Petición (Request) HTTP [Pág. 25-27]

Estructura: `verb` + `headers` + `content` (body).

| Verbo | Acción sobre el servidor |
|---|---|
| `GET` | Request Resource |
| `POST` | Create Resource |
| `PUT` | Update Resource |
| `PATCH` | Update Partial Resource |
| `DELETE` | Delete Resource |

**Headers del request** (metadata sobre la petición): `Content-Type` (formato del contenido), `Content-Length` (tamaño), `Authorization` (quién hace la llamada), `Accept` (qué tipos puede aceptar), `Cookies` (datos de sesión enviados), y más.

**Content del request:** HTML, CSS, JavaScript, XML, JSON — no es válido con todos los verbos; incluye información para completar la petición; binarios y blobs son comunes (ej. `.jpg`).

### Respuesta (Response) HTTP [Pág. 28-30]

Estructura: `status code` + `headers` + `content`.

| Rango | Significado |
|---|---|
| 100-199 | Informational |
| 200-299 | Success |
| 300-399 | Redirection |
| 400-499 | Client Errors |
| 500-599 | Server Errors |

**Headers del response:** `Content-Type`, `Content-Length`, `Expires` (cuándo considerar obsoleta la respuesta), `Cookies`, y más. **Content del response:** HTML, CSS, JavaScript, XML, JSON, binarios/blobs — muchas APIs tienen sus propios tipos.

<details class="autoeval">
<summary>Autoevaluación</summary>
¿En qué franja de status code cae un error de "recurso no encontrado"? ¿Y un error del servidor?
</details>

---

## 14. Diseño de la URI, verbos y recursos

🟢 [Pág. 31-38]

**Partes de una REST API** [Pág. 31-32]: el Request tiene `VERB` + `URI (Query String)` + `Headers` + `Request Body`; el Response tiene `Status Code` + `Headers` + `Response Body`.

**Uniform Resource Identifier (URI)** [Pág. 33-35]: son una **ruta a recursos**.

- **Query path** se usa para recursos únicos (no tiene por qué ser una PK): `api.ort.edu.uy/teachers/pepeBotella`.
- **Query parameters** (con query string) se usan para filtrado: `api.ort.edu.uy/students?lastName=Botella&active=true`.
- **No deberíamos usar verbos en la URI** — para eso está el verbo HTTP explícito, por separado. Mal: `api.ort.edu.uy/saveStudent`, `getStudent`, `deleteTeacher`. Bien: preferir **sustantivos**, con la acción dada por el verbo HTTP: `api.ort.edu.uy/students`, `/teachers`, `/courses`.

**Diseño de recursos** [Pág. 36]: los recursos no son solo las entidades de la base de datos, sino esas entidades **con un contexto asociado** — eso es lo que se expone (ej. People, Invoices, Payments, Products, con su Context de Site/Category/Region/Location).

**Verbos** [Pág. 37]: `GET` (obtener), `POST` (crear), `PUT` (actualizar completo), `PATCH` (actualizar parcial), `DELETE` (borrar).

**Verbos + recursos, tabla oficial** [Pág. 38]:

| Recurso | GET | POST | PUT/PATCH | DELETE |
|---|---|---|---|---|
| `/students` | Obtener lista de estudiantes | Crear nuevo estudiante | Actualizar varios estudiantes | - |
| `/students/99851` | Obtener al estudiante 99851 | - | Actualizar los datos del estudiante 99851 | Borrar al estudiante 99851 |

<details class="autoeval">
<summary>Autoevaluación</summary>
¿Por qué `api.ort.edu.uy/getStudent` se considera mal diseño según esta fuente?
</details>

---

## 15. Idempotencia

🟢 [Pág. 39]

**`GET`, `PUT`, `PATCH` y `DELETE` son idempotentes.** **`POST` es el único verbo no idempotente.**

`POST` produce un resultado distinto cada vez que se ejecuta (se crea una nueva entidad). Un `DELETE` puede borrar una entidad, pero si luego de borrarla se ejecuta *n* veces, se obtiene el mismo resultado (la entidad sigue sin existir).

---

## 16. Diseñando resultados, asociaciones y manejo de errores

🟢 [Pág. 40-43]

**Diseñando resultados** [Pág. 40-41]: no se deben exponer detalles de implementación (ej. el lenguaje usado); hay que ser **consistente**; si algo se hace de forma cuestionable, al menos ser consistente con esa decisión. Tomar en cuenta: **paginado**, **hypermedia**, consistencia.

Ejemplo oficial de respuesta paginada:

```json
{
  "totalResults": 5014,
  "nextPageUrl": "/teachers?page=3",
  "results": [{
    "id": 13854654156,
    "name": "Pepe Botella",
    "since": 2022
  }]
}
```

**Asociaciones (escenarios complejos)** [Pág. 42]: usar **navegación por URIs** — `api.ort.edu.uy/teachers/pepeBotella/courses`, `api.ort.edu.uy/courses/networkProgramming/teachers`. Los datos retornados en casos con asociaciones deberían ser consistentes entre sí: `/teachers/pepeBotella/courses` debería devolver el mismo formato que `/courses`.

**Manejo de errores** [Pág. 43]: una API REST **no debería devolver solo el código de status** como respuesta. Un código de "recurso no encontrado" ya es bastante auto-explicativo; una petición incorrecta generalmente requiere más datos en el body (depende de la situación y la consistencia del diseño).

---

## 17. Versionado de APIs

🟢 [Pág. 44-54]

**Por qué:** una vez publicada, una API es **un contrato** — pero *"nothing is written in stone"*: los requerimientos cambian, y hay que dar soporte tanto a clientes viejos como nuevos. El versionado de APIs **no implica** versionado de producto: una app puede tener una UI nueva vendida como "nueva versión" y seguir usando las mismas APIs.

**4 estrategias oficiales:**

| Estrategia | Ejemplo |
|---|---|
| **URI Path** | `api.ort.edu.uy/v1/students` vs `api.ort.edu.uy/v2/students` |
| **Query String** | `api.ort.edu.uy/students?version=1.0` vs `?version=2.0` |
| **Headers** | Header propio `Api-Version: 1.0` vs `Api-Version: 2.0` |
| **Headers de aceptación** | `Accept: application/json ; version=1.0` vs `version=2.0` |
| **Tipos de contexto** | `Accept: application/version.model.v1+json` vs `v2+json` |

<details class="autoeval">
<summary>Autoevaluación</summary>
¿Por qué el versionado de APIs "no implica" versionado de producto?
</details>

---

## 18. Streaming en REST/HTTP: alternativas al request/response clásico

🟢 [Pág. 55-62]

**Pregunta que plantea la fuente:** *"¿Existe streaming en REST/HTTP, o siempre es request/response?"*

| Alternativa | Descripción oficial |
|---|---|
| **Short Polling** | El cliente realiza peticiones frecuentes y el servidor responde enseguida |
| **Long Polling** | El cliente espera una respuesta; el servidor responde cuando hay datos o tras un tiempo límite |
| **SSE (Server-Sent Events)** | El servidor envía datos de forma continua al cliente por una conexión HTTP abierta |
| **WebSocket** | Comunicación bidireccional entre cliente y servidor (**no sigue REST**) |
| **Webhook** | El servidor llama al cliente (o a otro servidor) cuando ocurre un evento — "HTTP Callback" |

**Long-running tasks / polling design** [Pág. 57-59]: cuando un productor MOM necesita la respuesta de un consumidor, una estrategia oficial es: `POST` inicial → `HTTP 202 / Location` → el cliente hace `GET` repetido a una **Status API** hasta `HTTP 200`, y luego a una **Resource API** para obtener el resultado final.

**Server-Sent Events** [Pág. 60]: similar al server-side streaming de gRPC (Tema 5); el servidor puede enviar varios mensajes al cliente; es **unidireccional**; soportado nativamente por los navegadores (a diferencia de gRPC).

**Webhook** [Pág. 62]: es un callback HTTP — permite que un servidor le diga a otro a dónde mandar la respuesta cuando la tenga. No es tecnología nueva (usa URLs HTTP existentes), pero hay un "estándar webhook" para estandarizar la comunicación. Evita la necesidad de *polling* **entre servidores**. Consideraciones importantes: manejo de fallas (qué pasa si nunca llaman al URL) y seguridad (poder confiar en que quien llama al URL es quien debía llamarlo). También se usa para integraciones (Teams, Discord, Slack) — ej. un servidor manda un `POST` a Slack cuando falla la app.

<details class="autoeval">
<summary>Autoevaluación</summary>
¿Por qué SSE es "similar" al server streaming de gRPC pero con una ventaja concreta sobre navegadores?
</details>

---

## 19. WebSockets

🟢 [`WebSockets.pdf`, oficial, 9 diapositivas] — material aparte, oficial de Aulas, no numerado como bloque independiente del programa.

**Qué es** [Pág. 2]: protocolo (**RFC 6455**) que da un mecanismo para aplicaciones basadas en navegador que necesitan **comunicación bidireccional** con servidores, sin depender de abrir múltiples conexiones HTTP.

**Claves:** el cliente inicia la conexión; comunicación bidireccional; el servidor puede enviar mensajes **sin** que medie un request del cliente.

**Pros y contras** [Pág. 3]:

| Pros | Contras |
|---|---|
| Comunicación bidireccional en tiempo real | Implementación compleja (múltiples conexiones, eventos, reconexiones) |
| Soporte nativo por los navegadores | Mayor consumo de recursos |
| Menor sobrecarga de red que HTTP | Dificultad para escalado horizontal |
| Multiplataforma y multilenguaje | |

**WebSocket vs HTTP (tabla oficial)** [Pág. 4]:

| | API REST | WebSockets |
|---|---|---|
| Protocolo | HTTP/HTTPS | WS/WSS |
| Duplex | Semi duplex | Full duplex |
| Tiempo de conexión | Corto | Indefinido |
| Latencia | Alta | Baja |
| Velocidad | Lenta | Rápido |
| Usos | Carga inicial, CRUD, subida/descarga de archivos | Apps en tiempo real, chats, juegos |
| Modelo de comunicación | Basado en REST | Conexión persistente (eventos/mensajes) |

**Casos de uso oficiales** [Pág. 5]: WhatsApp (mensajes, audios), Uber (ubicación en tiempo real, mensajería), Figma/Google Docs (colaboración en vivo), GitHub (notificaciones, mensajes, estado de issues y PRs).

**Eventos del ciclo de vida** [Pág. 6]: `Open` (inicio de conexión) → `Message` (envío de mensajes, bidireccional) → `Error` (fallo en la conexión) → `Close` (cierre de conexión). El diagrama oficial muestra un handshake inicial y luego comunicación bidireccional libre hasta el cierre.

**Ejemplo cliente JavaScript** [Pág. 7]:

```javascript
const uri = "ws://localhost:5000/ws";
const webSocket = new WebSocket(uri);

webSocket.addEventListener("open", () => {
  // Manejar apertura de conexión
});
webSocket.addEventListener("message", (event) => {
  // Manejar mensajes entrantes
});
webSocket.addEventListener("error", (event) => {
  // Manejar errores
});
webSocket.addEventListener("close", (event) => {
  // Manejar cierre de conexión
});

function sendMessage(message) {
  if (webSocket.readyState !== WebSocket.OPEN) console.log("Web socket no está abierto");
  webSocket.send(message);
}
```

**Ejemplo servidor C#/.NET** [Pág. 8-9]: se configura con `WebSocketOptions` (ej. `KeepAliveInterval`) y `app.UseWebSockets(wsOptions)`. Un endpoint verifica `HttpContext.WebSockets.IsWebSocketRequest`, acepta la conexión con `AcceptWebSocketAsync()` y delega a un manejador que corre en paralelo un `ReceiveMessage` (loop con `ReceiveAsync` hasta `CloseStatus.HasValue`) y un `SendMessageAsync` (con `SendAsync` y `WebSocketMessageType.Text`).

> 🟠 **Complementario.** El patrón `ReceiveAsync` en loop hasta detectar el cierre es conceptualmente el mismo patrón visto en Sockets/TcpClient (Tema Sockets): siempre hay que loopear la lectura porque no se garantiza recibir todo de una sola vez.

<details class="autoeval">
<summary>Autoevaluación</summary>
¿Por qué WebSocket es "full duplex" y REST es "semi duplex" según la tabla oficial?
</details>

---

## Pendiente de verificar

- **Seguridad**, pedida explícitamente por el programa oficial de la Unidad 7 ("REST: ... Seguridad. Manejo de tecnologías"), **no está cubierta en ninguna de las dos fuentes disponibles** (`REST APIs 2025.pdf`, `WebSockets.pdf`). No hay autenticación, autorización, OAuth, JWT ni HTTPS/TLS aplicado a REST.
- No hay evidencia de parcial (🔵) citable: `PARCIALES/ANALISIS_DE_FRECUENCIA.md` sigue desactualizado.
- Los links a "REST API Design for Long-Running Tasks", "Adidas API Guidelines" y "Long running operations - Microsoft Fabric REST APIs" [Pág. 59] son referencias externas mencionadas pero no desarrolladas en el cuerpo de la diapositiva.
- El material aclara explícitamente "No vamos a entrar en mayor detalle (2025 S1)" sobre WebSockets dentro de `REST APIs 2025.pdf` [Pág. 61] — el desarrollo completo de WebSockets vino después, en el PDF aparte (`WebSockets.pdf`), que sí se usó acá.

---

## Repaso espaciado

**Día 3:**
1. Nombrá las 6 restricciones RESTful principales y a qué propiedades apunta cada una.
2. Explicá con tus palabras la diferencia entre Query path y Query parameters en una URI.
3. ¿Por qué POST es el único verbo no idempotente?

**Día 7:**
1. Reconstruí de memoria la línea de tiempo de APIs distribuidas (RPC/Messaging → CORBA/RMI → REST/SOAP → GraphQL → gRPC).
2. Compará las 5 estrategias de streaming/alternativas a request-response (short polling, long polling, SSE, WebSocket, Webhook) en una tabla propia.
3. ¿Qué parte del programa oficial de Tema 7 sigue sin cobertura en el material disponible?

---

*Generado a partir de `TEORICO/07_REST/REST APIs 2025.pdf` y `TEORICO/07_REST/WebSockets.pdf` (fuentes oficiales). Reemplaza la versión anterior basada en StuDocu.*
