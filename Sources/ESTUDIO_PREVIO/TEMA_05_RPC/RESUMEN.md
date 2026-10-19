# TEMA 5 — Invocaciones Remotas (RPC/gRPC) (PDR)

**Cobertura vigente de Aulas (semanas 12 a 14):** invocaciones remotas con gRPC, Protocol Buffers y cliente/servidor en C#. RPC y ORBs se mantienen como contexto histórico del material oficial.

**Fuente principal:** 🟢 `TEORICO/05_RPC_Y_ORBS/RPC.pdf` ("RPC & ORB & GRPC", 49 diapositivas) — único material oficial disponible para este tema. Reemplaza de cero la versión anterior de este RESUMEN, que estaba basada en `0.RESUMEN-STUDOCU.pdf` (no oficial).

**Evidencia de evaluación (🔵):** RPC/gRPC aparece en 13 de las 16 letras históricas revisadas. Se repiten contratos `.proto`, tipos de streaming y elección justificada dentro de arquitecturas distribuidas.

**Convención de tags:** 🟢 Oficial (`RPC.pdf`) · 🔵 Evidencia de evaluación (parcial/examen) · 🟠 Complementario (agregado por el asistente para reforzar comprensión, no viene de la fuente).

---

## Recuperación — preguntas antes de leer

Respondé mentalmente antes de avanzar (no hace falta escribir la respuesta):

1. ¿Por qué "llamar a una función remota como si fuera local" es más difícil de lo que suena?
2. ¿Qué diferencia esperás entre un mecanismo pensado para lenguajes procedurales y uno pensado para lenguajes orientados a objetos?
3. ¿Por qué gRPC usa HTTP/2 en lugar de HTTP/1.1?

---

## 1. Por qué existe RPC

**Qué es:** RPC (*Remote Procedure Call*) es una tecnología para comunicar procesos que residen en **contextos distintos**, permitiendo ejecutar una subrutina o procedimiento ubicado en otra dirección de memoria —habitualmente en otra máquina— **como si se encontrara localmente** [Pág. 2, 5].

**Contextos posibles** [Pág. 2]:
- Espacios de memoria dentro de una misma máquina.
- Entre máquinas distintas.
- Entre procesos distintos que corren en la misma máquina.

La arquitectura básica es cliente-servidor: un cliente hace peticiones a un servidor, que responde. Esto es el concepto de **IPC (Inter Process Communication)** [Pág. 2].

**Por qué importa:** antes de RPC, las implementaciones distribuidas usaban mecanismos de conectividad de bajo nivel —**Sockets**, **Named Pipes** (comunicación entre procesos de una misma máquina o de máquinas distintas conectadas) y **Shared Memory** (RAM compartida entre procesos)— sumados a **protocolos propietarios** [Pág. 3]. Desarrollar a ese nivel hacía las aplicaciones muy difíciles de mantener: cualquier funcionalidad nueva implicaba modificar el protocolo. Por eso ese mecanismo perdió popularidad y se buscaron alternativas [Pág. 3].

<details class="autoeval">
<summary>Autoevaluación</summary>
¿Qué problema concreto de mantenibilidad genera programar directo sobre Sockets con un protocolo propietario?
</details>

---

## 2. Evolución: de RPC (procedural) a ORB (orientado a objetos)

🟢 [Pág. 4]

Se diseñaron plataformas genéricas con protocolos de más alto nivel para resolver el problema anterior:

| Plataforma | Empresa |
|---|---|
| Courier | Xerox |
| ONC RPC | Sun Microsystems |
| MSRPC | Microsoft |

Eran plataformas rígidas, pero resolvían el problema de forma adecuada. La fuente marca dos eras:

- **Era de lenguajes Procedurales → RPC.**
- **Era de lenguajes Orientados a Objetos → ORBs.**

Las tecnologías RPC debieron adecuarse al paradigma orientado a objetos, dando lugar a los **ORBs (Object Request Broker)** [Pág. 8]:

| ORB | Descripción oficial |
|---|---|
| **CORBA** | Definido por Object Management Group (OMG); permite que componentes escritos en múltiples lenguajes, corriendo en distintas computadoras, trabajen juntos (cita Wikipedia) |
| **RMI** | Mecanismo ofrecido por Java para invocar un método de manera remota |
| **Remoting** | Tecnología propietaria de Microsoft para aplicaciones distribuidas; permite trabajar desde una máquina con objetos en memoria de la máquina remota |

Los ORB están orientados a arquitecturas de **objetos distribuidos**. El middleware basado en Object Request Broker permite distribuir y compartir los objetos de la aplicación entre redes heterogéneas [Pág. 8].

> 🟠 **Complementario.** RMI ya se procesó en detalle como Tema 3 de esta materia ("Java RMI y Serialización"); acá aparece solo como ejemplo de ORB dentro del panorama general de RPC/ORB, sin repetir ese desarrollo.

<details class="autoeval">
<summary>Autoevaluación</summary>
¿Qué tienen en común CORBA, RMI y Remoting como para agruparse bajo el nombre "ORB"?
</details>

---

## 3. RPC en detalle: origen y mecanismo síncrono

🟢 [Pág. 5-7]

**Origen:** introducido por primera vez por **Birrell y Nelson en 1984**, sentando las bases del desarrollo en sistemas distribuidos que se usa hasta hoy. Está orientado a la **programación Procedural** [Pág. 5].

El protocolo de RPC fue un gran avance sobre los sockets de Internet usados hasta el momento: el programador ya no tenía que estar pendiente de las comunicaciones, quedando estas **encapsuladas** dentro de las RPC [Pág. 5]. Un proceso servidor define en su interfaz de servicio los procedimientos disponibles para ser llamados remotamente.

**Mecanismo síncrono** [Pág. 6]: el cliente hace la llamada y se **suspende a sí mismo** hasta recibir la respuesta. El motor del RPC recolecta todos los datos de la invocación, forma un mensaje y lo envía al servidor; el servidor desempaqueta los datos, ejecuta la función y retorna el resultado al cliente.

**Secuencia oficial** [Pág. 7]:

1. Cliente: petición junto con parámetros y stub → envío al servidor.
2. Cliente permanece en espera.
3. Servidor: desempaquetamiento de la petición.
4. Servidor: ejecución.
5. Servidor: empaquetamiento y envío de la respuesta.
6. Cliente: recibe respuesta, continúa la ejecución.

<details class="autoeval">
<summary>Autoevaluación</summary>
¿Por qué se dice que RPC es un mecanismo sincrónico? ¿Qué le pasa al cliente mientras espera?
</details>

---

## 4. Ubicación en el modelo de capas

🟢 [Pág. 9]

La diapositiva ubica **RPC** (junto con MOM, ORB, Named Pipes, DCE, SUN, Peer-to-Peer) en la capa **Application** del modelo de capas de red, por encima de Presentation (NDR, XDR), Session (NetBIOS, Sockets, TLI), Transport (TCP/SPX/CPI-C), Network, Logical Link, Media Access Control y Physical.

> 🟠 **Complementario.** Esto confirma que, a diferencia de Sockets (que trabaja a nivel de Transporte/Sesión), RPC/ORB/MOM son mecanismos de **capa de Aplicación** — el mismo punto que ya se remarcó al procesar Tema 4 (MOM).

---

## 5. Retos de RPC/ORB

🟢 [Pág. 10]

RPC resuelve muchos problemas y da herramientas de desarrollo y entornos de ejecución, pero plantea retos:

1. ¿De qué modo encuentra el cliente al servidor?
2. ¿Cómo se organizan y arrancan los canales de comunicación entre servidor y clientes?
3. ¿Qué pasa cuando varios clientes quieren acceder a la misma función?
4. ¿Cómo se definen y pasan los parámetros entre cliente y servidor?
5. ¿Cómo se maneja la representación de la información entre sistemas heterogéneos?
6. ¿Cómo se manejan las excepciones?
7. (Específico de ORB) ¿Cómo consumir las funcionalidades de los objetos remotos de forma transparente?

Las secciones siguientes del PDF (Canales, Ubicación, Serialización, Excepciones, Stubs/Skeletons) responden, en ese orden, a cada uno de estos retos.

<details class="autoeval">
<summary>Autoevaluación</summary>
Emparejá cada reto de la lista con la sección del PDF que lo resuelve.
</details>

---

## 6. Canales de comunicación

🟢 [Pág. 11-12]

Cada plataforma RPC/ORB cuenta con un canal común de comunicaciones (ej. Socket) y maneja entre una o múltiples conexiones las llamadas a procedimientos o métodos.

| Estrategia | Ventaja | Desventaja |
|---|---|---|
| **Conexiones múltiples** | La sincronización la provee el propio canal | Limita la concurrencia a la cantidad de conexiones (puertos) posibles |
| **Conexión única** (puede ser Peer to Peer) | No existen problemas de concurrencia a nivel de canal | (si no es Peer to Peer) Sincronización más compleja: se resuelve a nivel de tablas de bloqueos y enriquecimiento del protocolo |

**Concurrencia** [Pág. 12]: se resuelve mediante **monitores, locks, semáforos**. En general, se delega al programador la responsabilidad de resolver los problemas asociados al procesamiento concurrente.

> 🟠 **Complementario.** Esto conecta directo con Tema 2 (Threads y Concurrencia): los mismos mecanismos de exclusión mutua vistos ahí (Monitor, lock, Semaphore) son los que un framework de RPC/ORB necesita internamente para manejar múltiples llamadas simultáneas sobre un canal compartido.

<details class="autoeval">
<summary>Autoevaluación</summary>
¿Por qué una conexión única Peer to Peer no tiene problemas de concurrencia a nivel de canal?
</details>

---

## 7. Ubicación — cómo el cliente encuentra al servidor

🟢 [Pág. 13]

| Enfoque | Descripción | Ejemplo oficial |
|---|---|---|
| **Ruta absoluta (URL)** | Se conoce exactamente la ruta de la infraestructura a comunicar (similar a un IPEndPoint) | Remoting |
| **Directorio** | El cliente solo conoce la ruta de un directorio común donde están publicadas las infraestructuras corriendo | RMI, UDDI |
| **Descubrimiento automático** | Vía broadcast; no es útil en entornos WAN | — |

---

## 8. Serialización, representaciones internas y Marshalling

🟢 [Pág. 14-15]

**Serialización:** hay que tener en cuenta datos simples y estructurados, y la representación interna de las plataformas que interoperan. Existen múltiples estándares para serializar datos entre plataformas (SOAP, JSON, etc.) [Pág. 14].

**Representaciones internas de datos** [Pág. 15]: en infraestructuras diseñadas para interoperar entre plataformas es necesario implementar convertidores de datos. Los procesos de serialización convierten los datos a **estructuras canónicas estándar**, que luego cada plataforma convierte a su representación interna. Es más complejo de usar porque hay que manejar a mano los detalles de representación, pero permite esta operativa (ej. CORBA).

**Marshalling** (cita Wikipedia, Pág. 15): *"proceso de transformación de la representación de datos a un formato adecuado, realizado según unas normas específicas y que su objetivo más primordial es la transferencia de estos a través de la red."* Su uso principal es en sistemas distribuidos: distintos tipos de hardware pueden generar problemas de interpretación de los datos recibidos al guardarlos en disco, lo que llevaría a un error.

<details class="autoeval">
<summary>Autoevaluación</summary>
¿Qué diferencia hay entre "serializar" y el concepto más específico de "Marshalling" según la cita oficial?
</details>

---

## 9. Manejo de excepciones remotas

🟢 [Pág. 16]

Los RPC/ORB modernos capturan las excepciones producidas en el destino de la llamada y las **serializan hacia el origen**. En origen, se detecta el mensaje de excepción, se **deserializa** y se vuelve a disparar **como si se hubiese producido de forma local**.

---

## 10. Stubs, Skeletons e IDL

🟢 [Pág. 17-19]

**Instanciación de objetos** [Pág. 17]: los ORB consumen sus objetos mediante el concepto de **Stubs** y **Skeletons**.

- **Stub:** pieza de código del lado **cliente** que emula al objeto remoto en el medio local delegado.
- **Skeleton:** pieza de código del lado **servidor** que captura las llamadas provenientes del cliente y ejecuta los métodos sobre los objetos concretos.

Cada objeto que se desee distribuir debe tener su propia implementación de Stub y Skeleton. En algunas plataformas, por sus características de interoperabilidad, es necesario programar ambas piezas para convertir tipos y otras tareas personalizadas. **En CORBA**, las características del Stub y Skeleton se programan en archivos **IDL**, que luego se compilan con el resto del programa.

**Flujo numerado del diagrama oficial** [Pág. 17-18]: `client functions → (1) client stub → (2) sockets → (3) red → (4) sockets → (5) server stub (skeleton) → (6) server functions` y el camino inverso `(7)-(10)` para la respuesta.

**IDL (Interface Definition Language)** [Pág. 19]: describe una interfaz en un **lenguaje neutral**, que permite la comunicación entre componentes de software desarrollados en diferentes lenguajes (ej. C++ y Java). Cita Wikipedia: *"permite a los sistemas de computadoras utilizar lenguajes y sistemas operativos diferentes. IDL ofrece un puente entre dos sistemas diferentes."* El ejemplo gráfico muestra una interfaz bancaria (`interface bank; debit_acct(acctID, amt, newbal)`) que pasa por un **IDL Compiler** y genera Stub + RPC Run Time tanto del lado cliente como del lado servidor.

<details class="autoeval">
<summary>Autoevaluación</summary>
¿Por qué el Stub "emula" al objeto remoto en vez de ejecutar la lógica real?
</details>

---

## 11. MOM vs. RPC

🟢 [Pág. 20-23] — retoma y profundiza la comparación ya vista en Tema 4.

**Metáfora oficial:** es como contrastar el **servicio telefónico** (RPC) con el **servicio Postal** (MOM) [Pág. 20].

| | RPC — Servicio Telefónico | MOM — Servicio Postal |
|---|---|---|
| Presencia de ambos extremos | Requiere que estén ambos presentes | No requiere presencia simultánea |
| Interacción | Inmediata | Diferida en el tiempo |
| Al finalizar | Termina el trabajo al cortar | — |

**Tabla comparativa completa** [Pág. 22-23]:

| Característica | Mensajes y Colas (MOM) | Llamadas a RPC |
|---|---|---|
| Metáfora | Oficina postal | Llamada telefónica |
| Relación de tiempo cliente-servidor | Asincrónica, distintas horas/velocidades | Sincrónica, deben ejecutarse concurrentemente |
| Secuencia | Sin secuencia fija | Servidores deben estar activos antes que los clientes se comuniquen |
| Estilo | Encolamiento | Devolución de llamadas |
| ¿Necesita que la contraparte esté activa? | No | Sí |
| Equilibrio de carga | Cola FIFO o por prioridades | Necesita un monitor transaccional |
| Soporte transaccional | La cola puede participar en el commit | No; necesita un RPC transaccional |
| Filtro de mensajes | Sí | No |
| Desempeño | Lento (salto intermedio) | Veloz |
| Procesamiento asincrónico | Sí, con cola y triggers | Limitado — necesita threads y código propio |

<details class="autoeval">
<summary>Autoevaluación</summary>
Según la tabla, ¿por qué MOM es "más lento" que RPC pero a la vez soporta mejor el desacople temporal?
</details>

---

## 12. gRPC: historia, HTTP/2 y Protocol Buffers

🟢 [Pág. 24-30, 33-34]

**Qué es:** implementación de RPC hecha por **Google (2015)**, proyecto de código abierto, que utiliza **HTTP/2** para el transporte [Pág. 25].

**Línea de tiempo de las APIs distribuidas** (diapositiva oficial, Pág. 25): RPC/Messaging/Queuing (1970s) → COM/DCOM/CORBA/Java RMI (1990s) → XMLHTTP/REST/SOAP (2000s) → GraphQL (2010s) → **gRPC** (2020s).

### HTTP/2 vs HTTP/1 [Pág. 26]

| Característica | Descripción oficial |
|---|---|
| **Multiplexed Streams** | Muchas peticiones y respuestas distintas viajan por una misma conexión, evitando la congestión de múltiples conexiones simultáneas |
| **Server Push** | El servidor envía recursos a la caché del navegador sin que este los pida, anticipándose a lo que va a necesitar |
| **Formato binario en lugar de texto** | Más eficiente de interpretar, más compacto, menos propenso a errores que texto plano (espacios, mayúsculas, fin de línea) |
| **Compresión de headers** | Las cabeceras se empaquetan en un bloque comprimido único, se envían y se decodifican del otro lado |

### gRPC: características técnicas [Pág. 27]

- Estructura de datos con protocolo liviano: **Protocol Buffers**.
- Soporta **TLS**.
- **IDL estricto** para representar los datos.
- Soporta **Streaming**.

> Cita oficial: *"El gRPC usa como IDL de mensajes el Protobuf (Protocol buffer) de formato binario no legible por humanos... El desarrollador decide cómo estructurar los datos definiéndolo en archivos `.proto`. Después el fichero deberá ser compilado vía `protoc`... En REST los mensajes JSON se intercambian en un formato de texto legible por el usuario, lo que hace que de base el tamaño del mensaje sea mayor, usando más ancho de banda."*

### Protocol Buffers [Pág. 29-30]

Mecanismo automatizado, eficiente y flexible para serializar datos estructurados — parecido a XML pero más pequeño, rápido y sencillo. Desarrollado por Google, lanzado como código abierto en 2008 (parte con licencia Apache 2.0). Compatible con C#, C++, Go, Objective-C, Java, Python, Ruby, entre otros.

| | Protobuf | JSON |
|---|---|---|
| Desarrollador | Google | Douglas Crockford |
| Formato binario | Sí | No |
| Estandarización | No | Sí |
| Legible por humanos | En parte | Sí |
| Comunidad/documentación | Pequeña, manuales extensibles | Enorme, documentación oficial |

- Archivo `.proto` (definición) → archivo compilado vía `protoc`.
- Ventaja: liviano y eficiente para enviar datos.
- Desventaja: **no es self-describing** (wire format) — se puede usar un formato ASCII para debugging.

**Ejemplo oficial de archivo `.proto`** [Pág. 33]:

```proto
syntax = "proto3";

message Person {
  required string name = 1;
  required int32 id = 2;
  optional string email = 3;

  enum PhoneType {
    MOBILE = 0;
    HOME = 1;
    WORK = 2;
  }

  message PhoneNumber {
    required string number = 1;
    optional PhoneType type = 2 [default = HOME];
  }

  repeated PhoneNumber phone = 4;
}
```

### gRPC vs REST [Pág. 32]

| Característica | gRPC | REST |
|---|---|---|
| Protocolo | HTTP/2 (rápido) | HTTP/1.1 (lento) |
| Carga útil | Protobuf (binario) | JSON (texto plano) |
| Contrato API | Estricto, requerido (`.proto`) | Flexible, opcional (OpenAPI) |
| Generación de código | Integrada | Terceros (Swagger) |
| Seguridad | TLS/SSL | TLS/SSL |
| Streaming | Bidireccional | Cliente → Servidor (solo request) |
| Soporte de navegador | Limitado (requiere gRPC-Web) | Sí |

> 🟠 **Complementario.** Este cuadro conecta directo con Tema 6 (APIs REST), que sigue a RPC/gRPC en el temario vigente.

**Caso de uso citado — Netflix** [Pág. 28]: Netflix usaba REST API, pero para 2015 identificó puntos débiles que dificultaban el descubrimiento, comprensión y auditoría de las APIs de su ecosistema. Construyó su propia RPC y luego migró a gRPC, logrando bajar la latencia del servicio y el tiempo de comercialización de clientes. Hoy gran parte de la comunicación interna servicio-a-servicio de Netflix corre en gRPC. (Otras empresas mencionadas en la diapositiva de logos: IBM, Google, Cisco, Dropbox, Spotify.)

<details class="autoeval">
<summary>Autoevaluación</summary>
¿Por qué Protobuf "no ser self-describing" se lista como desventaja frente a JSON?
</details>

---

## 13. Servicios y tipos de streaming en gRPC

🟢 [Pág. 35-42]

**Definición de servicio:** para comunicar una aplicación Servidor y Cliente es necesario definir un servicio de intercambio de mensajes; un servicio utiliza los mensajes definidos en un archivo `.proto` [Pág. 35]:

```proto
syntax = "proto3";

message ChatMessage {
  string from = 1;
  string message = 2;
}

Service ChatService {
  rpc chat (stream ChatMessage) returns (stream ChatMessage);
}
```

**Los 4 tipos de servicio gRPC** [Pág. 37-38]:

| Tipo | Descripción oficial |
|---|---|
| **Unario (Unary RPC)** | Llamada simple: el cliente hace una petición y el servidor responde |
| **Servidor Streaming** | El cliente hace una petición, el servidor responde con un stream de respuestas |
| **Cliente Streaming** | El cliente hace un stream de peticiones, el servidor devuelve una sola respuesta (normalmente luego de recibir todas las peticiones, aunque no siempre) |
| **Stream Bidireccional** | El caso más complejo: peticiones y respuestas bidireccionales, con el flujo decidido enteramente por la aplicación |

**Polling vs Streaming** [Pág. 39-42]: la fuente contrasta gráficamente el diseño tradicional de *polling* (`Client: "¿Hay datos nuevos?"` repetido, `Server: "No, todavía no"`) contra el diseño de *streaming* (`Client: "¿Puedo suscribirme?"` → conexión establecida → el servidor empuja datos nuevos sin que el cliente vuelva a preguntar). También ilustra client-side streaming (el cliente publica datos de a uno hasta avisar que terminó) y bidirectional streaming (ambos lados envían datos sobre el mismo stream abierto).

> 🟠 **Complementario.** La comparación Polling vs Streaming es la misma idea de fondo que "pull vs push": preguntar todo el tiempo (ineficiente, hay respuestas vacías) vs suscribirse y que te avisen (eficiente, cero peticiones desperdiciadas). Es la razón práctica por la que gRPC con streaming es más eficiente que hacer polling contra un endpoint REST.

<details class="autoeval">
<summary>Autoevaluación</summary>
¿Qué tipo de servicio gRPC usarías para un chat en tiempo real con mensajes en ambos sentidos?
</details>

---

## 14. Ventajas y desventajas de gRPC

🟢 [Pág. 43]

| Ventajas | Desventajas |
|---|---|
| Performance | Definición estricta |
| Resuelve el "viejo" problema de RPC de no usar HTTP | No tan difundido en la comunidad |
| Acoplamiento bajo entre cliente y servidor | |
| Multiplataforma y multilenguaje real | |

---

## 15. Implementación práctica en C#/.NET

🟢 [Pág. 45-49] — notas oficiales de la fuente tomadas luego de crear un proyecto gRPC real en Visual Studio.

- El paquete que expone el servidor gRPC en .NET es **`Grpc.AspNetCore`** (ejemplo de la fuente: versión 2.40.0).
- `launchSettings.json` define en `applicationUrl` la dirección donde se expone la aplicación servidor (ej. `http://localhost:5172;https://localhost:7172`); con `netstat -a` se puede encontrar el puerto escuchando.
- El archivo `.proto` (ej. `greet.proto`) usa `syntax = "proto3"`, define `option csharp_namespace` (aplica a C#) y el `service` a exponer con sus métodos.
- Al compilar, se genera automáticamente código marcado `// <auto-generated> ... DO NOT EDIT!` a partir del `.proto` — no se edita a mano.
- La clase de servicio del lado servidor hereda de la clase base autogenerada: `public class GreeterService : Greeter.GreeterBase`, y sobreescribe (`override`) los métodos definidos en el `.proto` (ej. `SayHello`).
- La comunicación con un endpoint gRPC debe hacerse a través de un **cliente gRPC** — acceder por navegador directamente a la URL da el mensaje *"Communication with gRPC endpoints must be made through a gRPC client."*

<details class="autoeval">
<summary>Autoevaluación</summary>
¿Por qué el código generado a partir del `.proto` no se debe editar directamente?
</details>

---

## 16. Pendiente de verificar

- **"Activación y ciclo de vida"** pertenece al mapa anterior de ocho unidades. Se conserva como antecedente, pero no es un hueco del Tema 5 vigente, centrado en gRPC, Protocol Buffers y cliente/servidor.
- 🔵 RPC/gRPC aparece en 13 de 16 letras históricas revisadas. Ver `../../PARCIALES/ANALISIS_DE_FRECUENCIA.md`.
- El PDF no distingue formalmente **RPC transaccional** más allá de mencionarlo en la tabla comparativa con MOM (Pág. 23) — no se profundiza en qué significa ni cómo se implementa.
- Las referencias de la webgrafía [Pág. 44] no se desarrollaron en detalle acá (son enlaces externos de contexto, no contenido evaluable per se).

---

## Repaso espaciado

**Día 3 (repasar sin mirar el resumen):**
1. Explicá con tus palabras qué hace un Stub y qué hace un Skeleton.
2. Nombrá los 4 tipos de servicio gRPC y de qué se diferencian entre sí.
3. ¿Por qué RPC es sincrónico y MOM es asincrónico? Usá la metáfora oficial (teléfono vs correo).

**Día 7 (repaso final antes de mezclar con otros temas):**
1. Armá de memoria la línea de tiempo: RPC (1984, Birrell y Nelson) → ORBs (CORBA/RMI/Remoting) → gRPC (Google, 2015).
2. ¿Qué reto de la lista de "Retos" [Pág. 10] resuelve el concepto de IDL?
3. Compará gRPC vs REST en al menos 4 dimensiones de la tabla oficial (protocolo, carga útil, contrato, streaming).
4. ¿Qué conceptos del mapa anterior quedaron fuera del alcance vigente de gRPC?

---

*Generado a partir de `TEORICO/05_RPC_Y_ORBS/RPC.pdf` (fuente oficial). Reemplaza la versión anterior basada en StuDocu.*
