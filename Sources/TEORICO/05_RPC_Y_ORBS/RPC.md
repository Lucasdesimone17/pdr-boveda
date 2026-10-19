RPC & ORB & GRPC

Objetivos de RPC
● Interacción entre procesos que residen en diferentes contextos.
● Contexto:
○ Espacios de memoria dentro de una misma máquina.
○ Entre máquinas distintas.
○ Entre procesos distintos que corren una misma máquina.
● La arquitectura consiste básicamente en un cliente que realiza peticiones
a otro programa (el servidor) que le da respuesta.
● Concepto de IPC (Inter Process Communication)

SOLUCIONES
● En un principio, las implementaciones usaban mecanismos de
conectividad de bajo nivel (Sockets, Named Pipes [ Permite a dos
procesos en una misma máquina o de diferentes máquinas conectadas
compartir mensajes entre procesos], Shared Memory [Utilizando RAM
compartida entre procesos]) sumados a protocolos propietarios que
permitían llevar a cabo esta tarea.
● Desarrollar a este nivel de protocolo las aplicaciones se hacen muy
difíciles de mantener.
● Nuevas funcionalidades implican modificar el protocolo.
● Este mecanismo perdió popularidad y se buscaron alternativas.

EVOLUCIÓN
● Se diseñaron plataformas genéricas con protocolos de más alto nivel
enfocados específicamente a la solución del problema anterior.
○ Courier (Xerox).
○ ONC RPC (Sun Microsystems).
○ MSRPC (Microsoft).
● Si bien eran plataformas bastante rígidas, resolvían el problema de forma
adecuada.
● Era de lenguajes Procedurales: RPC
● Era de lenguajes Orientados a Objetos: ORBs

EVOLUCIÓN. RPC. Remote Procedure Call
● Introducido por primera vez por Birrell y Nelson en 1984, y sentaron las bases para el desarrollo en
sistemas distribuidos que se usa a día de hoy
● Orientado a la programación Procedural.
● Es una tecnología para comunicar procesos permitiendo ejecutar una subrutina o procedimiento
que se encuentra en otra dirección de memoria habitualmente ubicada en otra máquina, como si
se encontrara localmente.
● El protocolo que se utiliza para esta llamada es un gran avance sobre los sockets de Internet
usados hasta el momento. De esta manera, el programador no tenía que estar pendiente de las
comunicaciones, estando estas encapsuladas dentro de las RPC.
● Un proceso servidor define en su interfaz de servicio los procedimientos disponibles para ser
llamados remotamente.

EVOLUCIÓN. RPC. Remote Procedure Call
● Se trata de un mecanismo de ejecución sincrónico donde el cliente
hace la llamada y se suspende a sí mismo hasta que reciba la
respuesta.
● El motor del RPC recolecta todos los datos de la invocación, forma
un mensaje y lo envía al servidor.
● El servidor desempaqueta los datos, ejecuta la función y retorna el
resultado al cliente.

Cliente Servidor
RPC
Petición junto con parámetros y stub
Desempaquetamiento
de la petición
Permanece
en espera
Ejecución
Envío de la respuesta
Empaquetamiento
Recibe
respuesta,
continua la
ejecución

ORBs
● Las tecnologías RPC debieron adecuarse al nuevo paradigma de programación orientada a
objetos dando lugar a los ORBs (Object Request Broker).
○ CORBA.
Definido por Object Management Group (OMG) que permite que diversos componentes de software escritos en
múltiples lenguajes de programación y que corren en diferentes computadoras, puedan trabajar juntos; (Wikipedia)
○ RMI.
Mecanismo ofrecido por Java para invocar un método de manera remota
○ Remoting.
Tecnología propietaria de Microsoft que permite crear aplicaciones distribuidas. Una de las principales
características es la capacidad para poder trabajar desde una máquina con los objetos en memoria de la máquina
Remota
● Orientado a arquitecturas de objetos distribuidos.
● El middleware basado en Object Request Broker (ORB) permite distribuir y compartir los objetos
de la aplicación entre redes heterogéneas.

Modelo de Capas

RETOS
Mientras que RPC soluciona muchos problemas y proporciona las herramientas de desarrollo y los entornos de
tiempo de ejecución, también plantea algunos retos:
● ¿De qué modo encuentra el cliente al servidor?
● ¿Cómo se organizan y arrancan canales de comunicación entre el servidor y clientes?
● ¿Qué pasa cuando varios clientes quieren acceder a la misma función?
● ¿Cómo se definen y pasan los parámetros entre el cliente y el servidor?
● ¿Cómo se maneja la representación de la información entre sistemas heterogéneos?
● ¿Cómo se manejan las excepciones?
● ORB: ¿Cómo hacer para consumir las funcionalidades de los objetos remotos de forma
transparente?

Canales
● Existen varias implementaciones posibles.
● En general, cada plataforma RPC/ORB cuenta con un canal común de
comunicaciones (ej. Socket) y manejan entre una o múltiples conexiones
las llamadas a procedimientos o métodos.
● Conexiones múltiples
○ Ventaja: la sincronización la provee el propio canal.
○ Desventaja: limita la concurrencia a la cantidad de conexiones posibles. Es decir, limita la
cantidad de puertos que se pueden abrir.

… Canales
● Una única conexión (puede ser Peer to Peer)
○ Ventaja:
No existen problemas de concurrencia a nivel de canal.
○ Desventaja (aplica para cuando no es Peer to Peer):
Sincronización más compleja, ya que se resuelve a nivel de tablas de bloqueos y enriquecimiento
del protocolo.
● Concurrencia
○ Se resuelve mediante el uso de monitores, locks, semáforos.
○ En general, se delega al programador la responsabilidad de resolver los posibles problemas
asociados al procesamiento concurrente.

Ubicación - Cómo encuentro al Servidor
• Existen múltiples enfoques para la localización de plataformas.
• Ruta absoluta (URL):
Se conoce exactamente la ruta de la infraestructura con la que se desea comunicar
(ej.Remoting). (similar al ipendpoint)
• Directorio:
El cliente conoce únicamente la ruta de un directorio común donde se encuentran
publicadas las infraestructuras que están corriendo (ej. RMI y UDDI).
• Descubrimiento automático:
de las infraestructuras vía broadcast pero esta estrategia no es útil en entornos WAN.

Serialización
● Se debe tener en cuenta: datos simples y estructurados, y la
representación interna de las plataformas que interoperan.
● Actualmente existen múltiples estándares para serializar datos entre
plataformas (SOAP, JSON, etc.)

Representaciones internas de datos
● En aquellas infraestructuras diseñadas para interoperar entre plataformas (incluso entre
infraestructuras) es necesario implementar convertidores de datos.
● En estos casos los procesos de serialización, serializan los datos a estructuras canónicas
estándares que luego cada plataforma convierte al tipo de representación interna que
corresponda.
● Su uso es mucho más complejo, ya que deben manejar a mano los detalles de representación,
pero permiten esta operativa (ej. CORBA).
● El Marshalling o Marshaling es el proceso de transformación de la representación de datos a un
formato adecuado, realizado según unas normas específicas y que su objetivo más primordial
es la transferencia de estos a través de la red. El uso principal del marshalling lo veremos en
sistemas distribuidos, ya que en este tipo de sistemas podemos encontrarnos distintos tipos de
hardware, debido a los cuales podemos tener problemas en la interpretación de los datos
recibidos al guardarlos en disco, lo que nos llevaría a un error.
(wikipedia)

Excepciones
● Los RPC/ORB modernos tienen la capacidad de capturar las excepciones
producidas en el destino de la llamada y serializarlas hacia el origen.
● En origen, se detecta el mensaje de excepción, deserializando la misma y
volviéndola a disparar como si se hubiese producido de forma local.

Instanciación de Objetos
● Los ORB consumen sus objetos mediante el concepto de
Stubs y Skeletons.
● Los Stubs son piezas de código, del lado cliente, que emulan al objeto remoto en el medio local
delegado.
● Los Skeletons son las piezas de código, del lado servidor, que capturan las llamadas
provenientes del cliente, ejecutando los métodos sobre los objetos concretos.
● Cada objeto que se desee distribuir debe poseer su propia implementación de Stub y Skeleton.
● En algunas plataformas, por sus características de interoperabilidad, es necesario programar
ambas piezas (Stubs y Skeletons) para convertir tipos y demás tareas personalizadas.
● En CORBA se programan las características del Stub y Skeleton en archivos IDL (archivo de
lenguaje de definición de interfaz) que posteriormente son compilados con el resto del
programa.

Describe una interfaz en un lenguaje
neutral, que permite la comunicación
entre componentes de software
desarrollados en diferentes lenguajes
de programación, como por ejemplo
entre componentes escritos en C++ y
otros escritos en Java.
“permite a los sistemas de
computadoras utilizar lenguajes y
sistemas operativos diferentes. IDL
ofrece un puente entre dos sistemas
diferentes.”(wikipedia)

MOM VS. RPC
Es como contrastar el servicio telefónico con el servicio Postal.
● RPC - Servicio Telefónico.
○ Requiere la presencia de ambos extremos.
○ La interacción es inmediata.
○ Al finalizar la conversación, finaliza el trabajo.
● MOM - Servicio Postal
○ No requiere la presencia de ambos extremos.
○ La interacción es diferida en el tiempo.

MOM VS. RPC
| Características | Mensajes y Colas | Llamadas a RPC     |
| --------------- | ---------------- | ------------------ |
| Metáfora        | Oficina postal   | Llamada telefónica |
Relación de tiempo entre el  Asincrónica, cliente y  Sincrónica, cliente y
cliente y el servidor  servidor pueden trabajar a  servidor deben ejecutarse
|     | distintas horas y distintas  | de manera concurrente. Los  |
| --- | ---------------------------- | --------------------------- |
|     | velocidades.                 | servidores deben seguir el  |
ritmo del cliente.
Secuencia entre el cliente y  Sin secuencia fija Primero, los servidores
| el servidor.  |     | tienen que estar activos  |
| ------------- | --- | ------------------------- |
antes de los clientes
pueden comunicarse.
| Estilo | Encolamiento | Devolución de llamadas.  |
| ------ | ------------ | ------------------------ |

MOM VS. RPC
| Características           | Mensajes y Colas | Llamadas a RPC |
| ------------------------- | ---------------- | -------------- |
| Es necesario que la otra  | No               | Si             |
contraparte esté activa
Equilibrio de carga.  Puede usarse una sola cola FIFO  Necesito un monitor transaccional.
o basada en prioridades.
Soporte transaccional.  La cola de mensajes puede  No, necesita un RPC transaccional
participar en el commit de
sincronización.
| Filtro de mensajes  | Si                           | No    |
| ------------------- | ---------------------------- | ----- |
| Desempeño           | Lento, se necesita un salto  | Veloz |
intermedio.
Procesamiento asincrónico  o Si, se necesita cola y triggers  Limitado, se necesita thread y
código para manejar los hilos

GRPC
● Implementación de RPC hecha por Google (2015)
● Proyecto de Código Abierto
● Utiliza HTTP/2 para el transporte
○ https://http2.akamai.com/demo

Http 2 vs Http 1
● MULTIPLEXED STREAMS
¿Recuerda las congestiones causadas por muchas conexiones cuando se trata de descargar al mismo tiempo texto, fuentes e imágenes desde
el servidor a su navegador? La multiplicación elimina este problema al proveer esos recursos en partes más pequeñas, pasando todos ellos a
través de una misma conexión. Es decir, enviamos muchas peticiones y respuestas diferentes a través de una misma conexión.
● SERVER PUSH
HTTP/2 permite enviar recursos a la cache del navegador sin que este los solicite reutilizando la conexión con el cliente, enviándole recursos
que probablemente vaya a necesitar. Es decir cuando suple una página HTML puede enviar los archivos de CSS, imágenes o fuentes antes de
que el navegador haya interpretado el contenido HTML y los solicite,
● FORMATO BINARIO EN LUGAR DE TEXTO
Los protocolos binarios son más eficientes para interpretar, más compactos al ser transportados, y lo más importante, son mucho menos
propenso a errores comparado con los protocolos basados en texto como HTTP/1.X, porque estos tienen a menudo un sin número de
problemas con los que lidiar como los espacios en blanco, la capitalización, los finales de línea y muchos más.
● COMPRESION DE HEADERS
En HTTP/1.X, cada solicitud enviada tiene una pequeña pieza de información
adicional que son los encabezados HTTP o HEADERS, que describen cómo se
comporta un navegador o un servidor. Cuando se establece una conexión HTTP/2,
todas las cabeceras se empaquetan en un solo bloque comprimido para ser
enviados como una unidad. Se envían y una vez finalizada la transmisión, se
decodifica el bloque de cabeceras del otro lado.

GRPC
● Estructura de Datos con Protocolo liviano
| ○ Protocol  |           |       |              |           | Buffers  |
| ----------- | --------- | ----- | ------------ | --------- | -------- |
| ● Soporta   |           |       |              |           | TLS      |
| ● IDL       | estricto  | para  | representar  | los       | datos    |
| ● Soporta   |           |       |              | Streaming |          |
“El gRPC usa como IDL de mensajes el Protobuf (Protocol buffer) de formato binario no legible por humanos,
desarrollado por Google para permitir la serialización y deserialización de datos estructurados. El desarrollador
decide como estructurar los datos definiéndolo en archivos .proto. Después el fichero deberá ser compilado vía
protoc. Dando como resultado un archivo con una clase por cada mensaje que hayamos definido.
En REST el IDL (...) Los mensajes JSON se intercambian en un formato de texto legible por el usuario, lo que hace
que de base el tamaño del mensaje sea mayor, usando más ancho de banda.”

Netflix hacía uso de REST API, pero para 2015 se identificaron una
| serie  de  | puntos  débiles  | que  | hacían  que  | se  dificultara  | el  |
| ---------- | ---------------- | ---- | ------------ | ---------------- | --- |
descubrimiento, comprensión y auditoría de las APIs disponibles
en el ecosistema. Con este conocimiento Netflix hizo esfuerzos
para construir su propia RPC, para después optar por usar gRPC.
El impacto de este cambio se tradujo en una bajada de latencia en
| el  servicio;  | además  | de  una  | disminución  | de  tiempo  | de  |
| -------------- | ------- | -------- | ------------ | ----------- | --- |
comercialización de clientes.
Hoy en día, en Netflix, gran parte de la comunicación interna de
servicio a servicio se ejecuta en gRPC.

PROTOBUF, PROTOS Y DEMÁS
Protocol buffers son un mecanismo automatizado, eficiente, flexible para serializar data estructurada, parecido a
XML pero más pequeño, rápido y sencillo.
Protocol Buffers (abreviado como Protobuf) es un formato de intercambio de datos desarrollado originalmente
para uso interno en Google y lanzado al gran público por la empresa en 2008 como proyecto de código abierto (en
parte, con licencia Apache 2.0). Este formato binario permite a las aplicaciones almacenar e intercambiar datos
estructurados fácilmente, incluso si los programas están compilados en diferentes lenguajes. Entre los lenguajes
de programación compatibles, se incluyen los siguientes:
● C#
● C++
● Go
● Objective-C
● Java
● Python
● Ruby

PROTOCOL
BUFFERS
• Archivo .proto (definicion)
• Archivo .protoc (compilado)
• Serializacion -> formato binario liviano
• Ventaja : liviano y eficiente para enviar data
• Desventaja: no es self describing (wire format)
• Se puede usar un formato ASCII para debugging

GRPC vs Rest

syntax = “proto3”;
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

OTRO EJEMPLO DE UN ARCHIVO PROTO
syntax = “proto3”;
message ChatMessage {
string from = 1;
string message = 2;
}

SERVICIOS
Para comunicar una aplicación Servidor y Cliente es necesario definir un servicio de
intercambio de mensajes, aquí es donde entra gRPC, un servicio utiliza los mensajes definidos
en un archivo proto.
Service ChatService {
rpc chat (stream ChatMessage) returns (stream ChatMessage);
}

TIPOS DE SERVICIOS GRPC
Servicio Unario: Llamada simple, el cliente hace una petición y el servidor
responde.
Servidor Streaming: el cliente hace una petición, el servidor responde con un
stream de respuestas.
Cliente Streaming: el cliente hace un stream de peticiones y el servidor devuelve
una sola respuesta. Normalmente la respuesta es enviada luego de que todos las
peticiones son enviadas, pero no siempre es el caso.
Stream Bidireccinal: el caso más complejo, peticiones y respuestas bidireccionales.
El flujo de la aplicación se decide enteramente en la aplicación.

TIPOS DE SERVICIOS

TIPOS DE SERVICIOS

TIPOS DE SERVICIOS

TIPOS DE SERVICIOS

TIPOS DE SERVICIOS

Ventajas y desventajas.
Ventajas
● Performance!
● Resuelve el “viejo” problema de RPC de no usar HTTP
● Acoplamiento bajo entre cliente y servidor
● Multiplataforma y multilenguaje real!
Desventajas
● Definición estricta
● No tan difundido en la comunidad

Webgrafia
● https://somostechies.com/que-es-http2/#:~:text=HTTP%2F2%20es%20un%20protocol
o,transporte%20dentro%20del%20protocolo%20HTTP).
● https://www2.deloitte.com/es/es/blog/todo-tecnologia/2021/grpc-vs-rest-api.html
● https://www.ionos.es/digitalguide/paginas-web/desarrollo-web/protocol-buffers/
● https://grpc.io/
● https://www.xenonstack.com/insights/what-is-grpc
● https://developers.google.com/protocol-buffers
● https://www.yonego.com/nl/blogs/why-milliseconds-matter/
● https://medium.com/cuemby-techblog/introducci%C3%B3n-a-grpc-6cc246f08ac3
● https://developers.google.com/protocol-buffers/docs/overview

Notas luego de haber creado un proyecto GRPC.
Paquete que permite exponer servidor
grpc.

Notas luego de haber creado un proyecto GRPC.
● Define en applicationUrl, la direccion por donde expone la aplicación servidor.
○ Si hacemos netstat -a , podemos encontrar el puerto escuchando

Notas luego de haber creado un proyecto GRPC.
● Proto3, implica la versión del proto
● la definición del namespace, aplica más que nada a C#
● service, implica el servicio a exponer, con sus parámetros.

notar la complejidad desde donde hereda, … greeterbase