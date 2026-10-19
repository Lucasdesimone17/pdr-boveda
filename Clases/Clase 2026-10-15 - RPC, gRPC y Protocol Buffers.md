---
date: 2026-10-15
type: class-note
tags:
  - pdr
  - clase
  - rpc
  - grpc
  - protobuf
ai-first: true
confidence: high
duracion: "~1h43"
fuente: "Grabacion del showcase de Vimeo 12367503, video 1236325147, subtitulos autogenerados es-x-autogen"
---

# Clase 2026-10-15 - RPC, gRPC y Protocol Buffers

## For future agent
Acta de la clase del **2026-10-15** (jueves). Arranca el **Tema 5**. Dictan dos docentes: una docente lee las diapositivas y [[Enrique Latorres]] comenta, con historia y ejemplos. Hasta el minuto ~60 ve **RPC generico**: historia, ORB, problemas que resuelve, stubs y skeletons, y MOM vs RPC. Despues ve **gRPC**: HTTP/2, Protocol Buffers, los cuatro tipos de servicio y la plantilla de .NET. **El cliente gRPC queda para la clase siguiente.** Escrita desde transcripcion automatica.

## Recap de Docker (min ~3)
Para el Obligatorio 2: **compilar afuera y copiar a la imagen**. Se puede elegir dos Dockerfiles, con o sin Compose. Si se animan, pueden publicar la imagen en Docker Hub. (stated)

## RPC: idea y origen
- Ya se hizo una **simulacion de RPC con RabbitMQ**, el Fibonacci del 08-10. La idea de RPC es que sea **transparente**: el programador invoca algo que parece una funcion local. (stated)
- El concepto es de los anos 70. Le costo popularizarse porque **cada proveedor tenia su RPC propietario** que solo funcionaba en su sistema. (stated)
- **gRPC** es la implementacion de **Google**: biblioteca **multiplataforma**, con **muy poco overhead** y transmision **binaria**. Se usa mucho **entre microservicios dentro de la empresa**. Ejemplos: Netflix, Amazon, Google. (stated)
- IPC es comunicacion entre procesos **de la misma maquina**. RPC va mas alla: maquinas, espacios de memoria o virtualizaciones distintas. (stated)

## Antes de RPC: pipes y protocolos propietarios
- **Pipes sin nombre**: el `|` de consola. En `cat archivo | grep error | wc`, los tres procesos **corren en paralelo**. Un pipe es un **productor-consumidor**, y para cada proceso es como leer un archivo en forma secuencial. Muy eficiente para grandes volumenes. (stated)
- **Pipes con nombre**: dos procesos piden al sistema operativo un pipe con un nombre comun. Funcionan **solo dentro de la misma maquina**. (stated)
- Los mecanismos de bajo nivel (named pipes, sockets, memoria compartida) con **protocolos propietarios** son dificiles de mantener: cada funcionalidad nueva obliga a **cambiar el protocolo**. (stated)
- Despues vinieron plataformas genericas: ONC RPC de Sun y MS RPC de Microsoft, entre otras. Despues los **ORB**, con la ola de la orientacion a objetos de los 90: **CORBA**, **RMI** y **.NET Remoting**. *"Asi como ahora todos tenemos que programar con inteligencia artificial"*. (stated)

## Que es RPC
- Hace que un procedimiento remoto **parezca una funcion local**: se invoca con parametros y devuelve el resultado. Es un gran avance sobre los sockets porque no es tan bajo nivel. (stated)
- Se escribe **un contrato** con los servicios y las estructuras de los mensajes. *"Todo eso que ustedes hicieron a mano, transpirando, que le mandaban las tres letras de RQ"*: ahora lo hacen un compilador y una biblioteca. Lo dificil es prepararlo la primera vez. (stated)
- **Es sincronico**: el cliente llama y espera la respuesta. Se puede **integrar en un ambiente asincronico** y recibir una promesa. Pensado para cosas rapidas. (stated)
- Sugerencia: mirar el trafico con **Wireshark**, filtrando el puerto, para ver el largo de los mensajes y el overhead. Las cadenas de texto se ven tal cual en el paquete, y sirve para depurar. (stated)
- RPC esta en la **capa de aplicacion**, junto con MOM y ORB. (stated)

## Problemas que RPC tiene que resolver
| Problema | Como lo resuelve gRPC, segun la clase |
|---|---|
| Como encuentra el cliente al servidor | Se configura al iniciar la comunicacion. **Ruta absoluta** (URL), **directorio** comun, o **descubrimiento** por broadcast o DNS interno |
| Como se abre el canal | **HTTP/2** para establecer la conexion y despues transferencia binaria. Hace falta un servidor web del lado del servidor |
| Varios clientes a la misma funcion | Cada solicitud corre en su hilo. **La exclusion mutua queda a cargo del programador**: monitores, locks, semaforos |
| Pasaje de parametros | Se declaran; el resto queda escondido detras del protocolo |
| Datos entre sistemas heterogeneos | Serializacion **binaria little-endian**; la biblioteca convierte si la maquina es big-endian |
| Excepciones | Se capturan en el servidor, viajan al cliente y **se relanzan como si fueran locales** |

**Puertos.** Hay 65.536 puertos. Para un servidor con cientos de miles de clientes pueden no alcanzar. HTTP 1 resuelve abriendo y cerrando la conexion por pedido, siempre contra el mismo puerto. (stated)

**URL por servicio.** Con HTTP/2, el mismo servidor web puede atender **REST en una ruta, WebSocket en otra y gRPC en otra** (`/grpc`), en la misma direccion. (stated)

**DNS interno.** Una organizacion puede tener un DNS interno que resuelve sus microservicios y que **desde afuera no se ve**. (stated)

**Marshalling.** Transformar los datos de memoria en algo transferible. En la mayoria de los casos es mandar el binario tal como esta. (stated)

## Stubs, skeletons e IDL
- **Stub:** codigo del lado del cliente que **emula al objeto remoto**. **Skeleton:** codigo del lado del servidor que **captura las llamadas** y ejecuta los metodos. Es un patron de interfaz predefinido. (stated)
- En CORBA se escriben en **IDL**. En gRPC el IDL es el **`.proto` de Protobuf**. No hay un compilador sino *"un transformador"* que genera clases en C#, en el namespace que se le indique. (stated)

## MOM vs RPC
**MOM es el correo:** mando una carta, no se cuando llega ni cuando la atienden, y no hace falta estar presentes a la vez. **RPC es instantaneo:** como pedir una pagina web, el servidor tiene que estar ahi. (stated)

| | MOM | RPC |
|---|---|---|
| Modelo | Asincronico, cada lado a su ritmo | Sincronico, concurrentes |
| Contraparte activa | No hace falta | Hace falta |
| Mecanismo | Colas | Llamadas que devuelven |
| Balanceo | Cola FIFO o prioridades | **Monitor transaccional** |
| Filtro de mensajes | Si | No |
| Velocidad | Puede ser lento | Mas eficiente, mas bajo nivel |

**Monitor transaccional y Base de Datos 2.** Enrique conecto el balanceo de RPC con el **bloqueo multiversion** y la **serializabilidad** de BD2. Si un proceso toca varias estructuras de memoria, el entrelazado multihilo tiene que dar **lo mismo que una ejecucion serial**. Si no, pasa lo de las transacciones bancarias que crean o pierden plata. Esas tecnicas sirven para **minimizar bloqueos** en codigo propio. (stated)

**RPC asincronico.** Retomo el `RpcClient` del 08-10: un metodo **no `async`** que arma la `Task` y la devuelve enseguida, y un delegado que la completa al llegar la respuesta. *"Podemos hacer cosas parecidas tambien con el RPC"*. (stated)

## gRPC
- Creado por Google en **2015**, **codigo abierto**, sobre **HTTP/2**. (stated)
- **HTTP/2 vs HTTP/1:**
  - **Multiplexado**: se establece la conexion y se piden varias cosas sin cortarla.
  - **Sitios virtuales**: segun la URL, el servidor tiene distintas *"personalidades"*.
  - **Binario**, aunque sigue aceptando texto. Es un superconjunto de HTTP/1.
  - **Headers en un solo bloque** al establecer la conexion. (stated)
- Soporta **TLS**. Uno de los ejemplos del docente usa canal encriptado. (stated)
- Enrique va a pasar **servidores HTTP hechos a nivel de socket** para ver el protocolo por dentro, con los chunks. *"Esta fuera del curso, pero creo que es importante que lo vean"*. (stated)

**Por que gRPC.** Convertir a texto (JSON, XML) **carga el procesador** y agranda los paquetes: lo que entra en 4 bytes ocupa 8 o mas como texto. gRPC da mensajes mas chicos y rapidos de procesar. (stated)

**REST hacia afuera, gRPC hacia adentro.** Hay menos gente con experiencia en gRPC y REST es mas intuitivo y facil de inspeccionar. Entonces las organizaciones publican **REST API para el publico** y usan **gRPC internamente**. Netflix usa gRPC para su comunicacion interna. (stated)

## Protocol Buffers
- Mecanismo para serializar datos estructurados, parecido a XML pero mas rapido y sencillo. Compararlo con JSON *"es como comparar un lenguaje de programacion con un notepad"*: Protobuf trae compiladores que generan el codigo de transferencia. (stated)
- **Desventaja:** no es *self-describing*. Hay un formato ASCII para depurar. (stated)
- **El mismo `.proto` en cliente y servidor.** *"Si cambian una cosa, tiene que cambiar"* del otro lado. Cada lado compila su parte. (stated)

**Ejemplo `Persona`.** Nombre e id obligatorios, email opcional, un enum embebido `PhoneType` (mobile, home, work) con **home por defecto**, y hasta cuatro telefonos. La docente dijo que los numeros 1, 2 y 3 de los campos *"lo unico que estan marcando es un orden, nada mas"*. **Ver item 30 de [[Huecos y contradicciones del material PDR]]**: en Protobuf esos numeros son identificadores de campo.

**Ejemplo `ChatMessage`.** Campos `from` y el mensaje, los dos cadenas, sin validacion. (stated)

**`service` vs `message`.** `message` declara estructuras; `service` declara los metodos remotos. Ejemplo: `ChatService` con un metodo `Chat` que recibe un `ChatMessage` y devuelve otro. (stated)

## gRPC vs REST, segun la diapositiva
| | gRPC | REST |
|---|---|---|
| Transporte | HTTP/2 | Ver salvedad |
| Formato | Protobuf, binario | JSON, texto |
| Contrato | `.proto`, obligatorio y estricto | OpenAPI, opcional y flexible |
| Generacion de codigo | Integrada | De terceros: Swagger, Postman |
| Seguridad | TLS/SSL, por ir dentro de HTTP | Igual |
| Streaming | **Bidireccional** | Solo request del cliente |
| Navegador | Limitado, requiere gRPC-Web | Completo |

**Salvedad:** la transcripcion dice *"REST usa HTTP/3"*. El material del curso compara contra **HTTP/1.1**. Probable error de lectura o de transcripcion: ver item 31 de [[Huecos y contradicciones del material PDR]].

## Los cuatro tipos de servicio
1. **Unario (sincronico):** una peticion y una respuesta. El mas usado y para lo que se penso RPC. (stated)
2. **Server streaming:** una peticion y un stream de respuestas. Para algo muy grande que llega en partes, o para **avisos de progreso** (10 %, 20 %, 30 %) sin hacer polling. (stated)
3. **Client streaming:** un stream de peticiones y una respuesta. Ejemplo: mandar todos los asientos contables y recibir un resumen. (stated)
4. **Bidireccional:** stream en los dos sentidos, para un dialogo. El mas complejo. (stated)

**Contra el polling.** Preguntar *"¿tenes algo?"* veinte veces es incomodo. Es mejor que el cliente se **suscriba** con server streaming. (stated)

**Ventajas:** performance, bajo acoplamiento, multiplataforma y multilenguaje, streaming. *"Tenganlo en cuenta"*. **Desventajas:** contrato estricto que, una vez compilado, *"no acepta modificaciones"*: en JSON se puede agregar un atributo y el otro lo ignora. Ademas, mucha gente sigue atada a REST. (stated)

## Demo: servidor gRPC en .NET (min ~93-102)
- Plantilla **gRPC Service** de .NET: arma el servidor web, la carpeta `Protos` y el `greet.proto` con el servicio **Greeter** (*"Hello, Enrique"*). Hay que agregar las bibliotecas. (stated)
- `Program.cs`: el builder, el agregado de gRPC y el mapeo del servicio. Para sumar REST u otros servicios se agregan en el mismo builder. Con un `MapGet` en la raiz, al entrar con un navegador avisa que solo atiende gRPC. Se puede montar en `/grpc`. (stated)
- **`Properties/launchSettings.json`** trae puertos por defecto. Enrique fijo **5000 para HTTP** y **no uso HTTPS**, porque haria falta crear claves. (stated)
- **`syntax = "proto3"`**: *"lo prolijo es que pongan el protocolo"*. (stated)
- **`option csharp_namespace`** tiene que coincidir con el namespace del codigo. Del lado del cliente va **el namespace del cliente**. (stated)
- **En el `.csproj`:** la entrada del `.proto` dice **`GrpcServices="Server"`** en el servidor y **`"Client"`** en el cliente. La plantilla la pone sola en el servidor. El nombre del `.proto` y su carpeta pueden cambiar. (stated)
- Del `.proto` se genera un `.cs` (transcripto "grit.cs") que conecta los mensajes con el codigo. (stated)
- Para agregar funciones: mas `service` y `message` en el `.proto`, de los dos lados, y la implementacion del calculo. *"Es muy sencillito"*. (stated)
- **El cliente queda para la proxima.** (stated)

## Salvedades de la transcripcion
- Correcciones probables:
  - gRPC (transcripto "XRP")
  - multi-stage ("multisales")
  - Dockerfile ("dover file")
  - "Gopher, que es de Xenos": no queda claro que plataforma nombro; TBD
  - **RMI** ("RMI", sin cambio) y **.NET Remoting** ("RemoteING")
  - `greet.proto` ("grit")
  - Greeter service ("Twitter Service")
- La transcripcion dice *"en 1984 se empezaron a ver las bases del desarrollo de sistemas distribuidos"*. Ano sin verificar.
- Hay un **pasaje de lista** en el minuto ~66. No se registran los nombres.
- Al cierre se escucha *"Viernes"* y *"exitos en el parcial"*. El parcial es de otra materia de los estudiantes, no de esta. El dia de la proxima clase de PDR no consta.

## Connections
- [[Tema 5 - RPC y gRPC]] - la teoria de esta clase
- [[Tema 4 - Colas de mensajes y MOM]] - el contrapunto, y el RPC sobre RabbitMQ del 08-10
- [[Tema 2 - Sockets y protocolos]] - lo que el contrato reemplaza
- [[Tema 6 - APIs REST y WebSockets]] - REST hacia afuera
- [[Clase 2026-10-13 - Usuarios de RabbitMQ y Docker para el Obligatorio 2]] - la anterior
- [[Clase 2026-10-08 - Letra del Obligatorio 2 y patron RPC sobre RabbitMQ]] - el `RpcClient`
- [[Programacion de Redes 2026]] · [[Enrique Latorres]]
