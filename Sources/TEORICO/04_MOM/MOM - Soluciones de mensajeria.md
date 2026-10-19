MOM:
Message-oriented
middleware

Sumario

• Definición

• Ventajas

• Desventajas

• Estándares

• RabbitMQ como implementación de MOM

• Ejemplos de aplicación

¿Qué es un middleware?

Definición
• Middleware, también conocido como lógica de intercambio de información

entre aplicaciones (interlogical) o agente intermedio, es un sistema de
software que ofrece servicios y funciones comunes para las aplicaciones.

• En general, el middleware se encarga de las tareas de gestión de datos,
servicios de aplicaciones, mensajería, autenticación y gestión de API.

• Es un software que asiste a una aplicación para interactuar o comunicarse con
otras aplicaciones, o paquetes de programas, redes, hardware o sistemas
operativos.

• Este simplifica el trabajo de los programadores en la compleja tarea de

generar las conexiones y sincronizaciones que son necesarias en los sistemas
distribuidos.

• De esta forma, se provee una solución que mejora la calidad de servicio, así
como la seguridad, el envío de mensajes, la actualización del directorio de
servicio, etc.

Wikipedia

Definición
El origen de la palabra middleware se remonta al año 1968, en donde la
palabra fue usada durante la '1968 NATO Software Engineering Conference'
pág. 14, para referirse a bibliotecas o software intermediario entre la
comunicación de aplicaciones desarrolladas por diferentes proveedores.

Su definición más simple viene del significado de la palabra “Middle·ware”
que sería “Artículo/Producto/Mercancía (del) Medio”. Hace referencia a
Software/Hardware que hace de intermediario entre otros dos o más
componentes de Software/Hardware, para realizar una operación o servicio
de mayor valor o importancia.

Generalmente el Middleware sólo aporta facilitar la conectividad y el
intercambio entre los dos o más componentes. Las razones de la necesidad
pueden ser variadas: tecnológicas, espaciales, lógicas, etc. o combinaciones
de estas.

Qué es un middleware?

Ejemplos interconexión B2B
Enterprise Merge
Diferentes Arquitecturas
Diferentes Tecnologías

Sistemas de acoplamiento
Distribuidos

Sistemas de registro
Centralizados

Qué es un
middleware

API de comunicación.
Administrable,
Confiable,
Accountability.
Escalable
Multi arquitectura/lenguaje

El middleware se sitúa entre la capa de aplicación y la capa de plataforma (el sistema operativo y los servicios de red subyacentes).

Definición
Hay diferentes clasificaciones para los tipos de MIddleware pues las funciones,
tecnologías y abstracciones que soportan pueden ser muy diversas.

Ver: https://dl.acm.org/doi/10.1145/3526211, https://es.wikipedia.org/wiki/Middleware,
https://www.ibm.com/topics/middleware

Muchas definiciones de middleware encontradas en la bibliografía tienen un sesgo
orientado al principal tema de los autores. Por lo que se sugiere en este tema tener la
visión más amplia posible.

Tipos de
middleware

Remote Procedure Call o
RPC-based middleware

Object Request Broker o
ORB-based middleware

Message Oriented Middleware
o MOM-based middleware

Alguna clasificación de Middlewares

•

•

•

•

•

•

•

•

•

•

•

El middleware orientado a mensajes (MOM) Permite intercambiar mensajes entre componentes de aplicaciones que utilizan
distintos protocolos de mensajería para comunicarse. Además de traducir o transformar los mensajes entre aplicaciones, MOM
gestiona el enrutamiento de los mensajes para que siempre lleguen a los componentes adecuados en el orden correcto. Algunos
ejemplos de MOM son las colas de mensajes y los corredores de mensajes.

El middleware de llamada a procedimiento remoto (RPC) permite a una aplicación activar un procedimiento en otra aplicación
o en otro computador.

El middleware de datos o bases de datos simplifica el acceso a las bases de datos y la interacción con ellas. Por ejemplo un
cliente de base de datos.

El middleware de interfaz de programación de aplicaciones (API) proporciona herramientas que los desarrolladores pueden
utilizar para crear, exponer y gestionar API para sus aplicaciones.

El middleware Object Request Broker (ORB) actúa como intermediario entre la solicitud de un objeto o componente de la
aplicación.

El middleware transaccional ofrece servicios de apoyo a la ejecución de transacciones de datos.

El middleware de flujo de datos asíncrono replica un flujo de datos en un almacén intermedio, lo que permite compartir datos
entre varias aplicaciones. buffer, productor/consumidor, etc.

El middleware de dispositivos proporciona un conjunto específico de capacidades de integración y conectividad para Internet
de las Cosas.

El middleware de portal proporciona herramientas y recursos para integrar capacidades en una aplicación única y compuesta.

El middleware de robótica simplifica el proceso de integración de hardware, firmware y software robótico de múltiples
fabricantes y ubicaciones.

Entre muchos otros.

MOM – Message Oriented
Middleware

• Los sistemas basados en MOM permiten la comunicación a través

del intercambio asincrónico de mensajes

Ya no trabajamos  en la capa de Transporte, ahora enviamos y recibimos en la capa de Aplicación. Somos clientes de un servidor de mensajería.

Middleware orientado a mensajes (MOM)
•

El middleware orientado a mensajes (MOM) permite intercambiar mensajes entre componentes de
aplicaciones que utilizan distintos protocolos de mensajería para comunicarse.

•

•

•

•

•

•

Además de traducir o transformar los mensajes entre aplicaciones, MOM gestiona el enrutamiento de
los mensajes para que siempre lleguen a los componentes adecuados en el orden correcto.

Algunos ejemplos de MOM son las colas de mensajes y los corredores de mensajes.

Los sistemas basados en MOM permiten la comunicación a través del intercambio asincrónico de
mensajes.

Los elementos básicos de un sistema MOM son los clientes, los mensajes y el proveedor MOM, que
incluye una API y herramientas administrativas.

El proveedor de MOM usa diferentes arquitecturas para enrutar y entregar mensajes:

•
•

puede usar un servidor de mensajes centralizado o
puede distribuir funciones de enrutamiento y entrega a cada máquina cliente.

Algunos productos MOM combinan estos dos enfoques.

MOM -
Definición

Los elementos básicos de un sistema MOM son los
clientes, los mensajes y el proveedor MOM, que incluye
una API y herramientas administrativas.

El proveedor de MOM usa diferentes arquitecturas para
enrutar y entregar mensajes: puede usar un servidor de
mensajes centralizado o puede distribuir funciones de
enrutamiento y entrega a cada máquina cliente.

Algunos productos MOM combinan estos dos enfoques.

• Usando un sistema MOM, un cliente realiza una llamada a la API para

enviar un mensaje a un destino administrado por el proveedor.

• La llamada invoca los servicios del proveedor para enrutar y entregar el

mensaje.

• Una vez que ha enviado el mensaje, el cliente puede continuar con su

trabajo, confiando en que el proveedor retiene el mensaje hasta que un
cliente receptor lo recupera.

• El modelo basado en mensajes, junto con la mediación del proveedor,
hace posible crear un sistema de componentes débilmente acoplados.

MOM -
Definición

La idea es mandar un mensaje (Como Whatsapp, e-mail, etc.) y luego continuamos con lo nuestro. La entrega del mensaje ya no es nuestra responsabilidad. SOP: State-Oriented Programming

⚫ MOM provee nombrado jerárquico, seguridad y aislamiento.

⚫ No se habla directamente con el extremo de la aplicación por lo tanto
el otro extremo puede estar ocupado o apagado sin afectar al que
produce el mensaje.

⚫ El consumidor decide en qué momento va extraer los mensajes.

MOM -
Definición

⚫ El servidor guarda los mensajes

MOM - Definición

Modelo  N a 1

COLA

Modelo  1 a N

COLA

Modelo  N a N

COLA

Los clientes y el tipo de mensajes podría ser diferente

Asincronía

Ventajas del
uso de MOM

Transformación de mensajes on the fly

Ruteo inteligente

MOM -
Asincronía

•En los sistemas asincrónicos, las colas de
mensajes proporcionan almacenamiento
temporal cuando el programa de destino
está ocupado o no está conectado.
• Además, la mayoría de los sistemas MOM
asíncronos brindan almacenamiento
persistente para respaldar la cola de
mensajes.
• Esto significa que el remitente y el
receptor no necesitan conectarse a la red al
mismo tiempo (entrega asíncrona) y se
resuelven los problemas de conectividad
intermitente.
•También significa que si la aplicación del
receptor falla por algún motivo, los
remitentes pueden continuar sin verse
afectados, ya que los mensajes que envían
simplemente se acumularán en la cola de
mensajes para su posterior procesamiento
cuando se reinicie el receptor.

MOM - Ruteo

• Muchas implementaciones de middleware
orientadas a mensajes dependen de un
sistema de cola de mensajes.

• Algunas implementaciones permiten que la
propia capa de mensajería proporciona la
lógica de enrutamiento, mientras que otras
dependen de las aplicaciones cliente para
proporcionar información de enrutamiento o
permiten una combinación de ambos
paradigmas

• Algunas implementaciones hacen uso de
paradigmas de distribución de difusión o
multidifusión.

MOM -
Transformación

• Un sistema MOM puede transformar los mensajes y
enrutarlos para que coincidan con los requisitos del
remitente o del destinatario.

•  Junto con las funciones de enrutamiento y

difusión/multidifusión, una aplicación puede enviar un
mensaje en su propio formato nativo, y dos o más
aplicaciones pueden recibir cada una una copia del
mensaje en su propio formato nativo.

• Muchos sistemas MOM modernos proporcionan

herramientas sofisticadas de transformación (o mapeo)
de mensajes que permiten a los programadores
especificar reglas de transformación aplicables a una
simple operación de arrastrar y soltar de GUI.

Desventajas
del uso de
MOM

Componente extra de arquitetura

PseudoSincronización o SOP

Performance - Asincronía y paralelismo

Débil acoplamiento

Ejemplos

1. Usuarios suben información a la web
2. Se crea un PDF asincronicamente
3. Se envía email a usuarios con PDF

Ejemplo
Procesamiento
de PDF

1. Usuarios suben archivos al sistema
2. Se escanea los archivos por virus
3. Se distribuyen al resto de servicios

Ejemplo
Softonic

Ejemplo
Cambiar tamaño
imagenes

1. Distintas partes de la aplicación precisan distintos tamaños de

fotos

2. Cuando se sube una foto se manda a una cola
3. El servicio de Image Scaling va consumiendo la cola a medida

que tiene capacidad

Ejemplo
ElephantSQL

• Servicio de hosting de bases de datos.
• Requieren realizar backups diariamente de varias bases de

datos

• Se agrega un mensaje por cada base de datos y se consumen a

medida que el servidor de backups puede procesarlos

• Se utiliza prioridades para priorizar las backups

específicamente pedidos por los usuarios

MOM -
Estándares

HLA IEEE (High Level Architecture)

MQTT (MQ Telemetry Transport)

OMG DDS (Object Management Group Data
Distribution Services)

XMPP (eXtensible Messaging and Presence
Protocol)

AMQP (Advance Message Queue Protocol)

MOM
Estándares -
HLA IEEE

Define servicios

API: Java o C++

Permite modelo Publish-Subscribe

Sistemas federados

MOM
Estándares -
MQ
Telemetry
Transport
(MQTT)

Desarrollada por IBM

Topic-based

Retain based communication

Basada en Brokers (open source o pagos)

MOM
Estándares -
Object
Management
Group Data
Distribution
Service

Publish subscribe

API: Java, C++, Ruby, ADA

MOM
Estándares -
XMPP

Antiguamente -> Jabber

Arquitectura cliente - servidor

Concebido para mensajería instantánea

Actualmente lo usan:

• Whatsapp
• Zoom (chat)

MOM
Estándares -
AMQP

Application Level Protocol

Binary encoding

TCP Based

SSL support

MOM – AMQP Standard

MOM – AMQP Standard

MOM – AMQP
Standard
Establecimiento
de la conexión

AMQP - Tipos de enrutamiento

AMQP (versión 0-9-1): Exchange y sus tipos

• Son entidades  a las que se envían los mensajes.
• Toman un mensaje y lo enrutan a cero o más colas.
• El algoritmo de enrutamiento utilizado depende del tipo de
intercambio y las reglas llamadas enlaces.
•  Los brokers de AMQP ofrecen cuatro tipos de intercambio:

Tipo

Direct exchange

Fanout exchange

Topic exchange

Headers exchange

Default pre-declared names

(Empty string) and amq.direct

amq.fanout

amq.topic

amq.match (and amq.headers in RabbitMQ)

AMQP: Direct
Exchange

AMQP: Direct
Exchange

Existe un Exchange por
defecto llamado “ ”

AMQP

No es necesario una routing key

AMQP –
Atributos
del
Exchange

Name

Durability (exchanges survive broker
restart)

Auto-delete (exchange is deleted
when last queue is unbound from it)

Arguments (optional, used by
plugins and broker-specific features)

Message Brokers

Una implementación de MOM

• Implementación de MOM
• Operaciones sincrónicas y asincrónicas.
• Operaciones débilmente acopladas y fuertemente acopladas.
• Transaccionalidad en el send/receive.
• Prioriza las entregas.
• Plazo de entrega basada en características del mensaje.
• Filtrado de la entrega.

RabbitMQ

• Open Source
• Implementado en Erlang
• Soporta AMQP, MQTT y otros protocolos
• Soporta Plugins (ej. plugin administrativo)
• Funciona en todos los sistemas operativos
• https://www.rabbitmq.com/download.html

RabbitMQ

RABBITMQ:
BASIC
MESSAGE
DELIVERY

• Work Queue
• Round Robin dispatching
• Message acknowledgment
• Message durability

RABBITMQ:
WORK
QUEUE
(patrón
competing
consumers)

• Fair dispatching

RABBITMQ:
WORK
QUEUE
(patrón
competing
consumers)

Manejo de errores y acknowledge

• Es importante confirmarle al MOM que el mensaje se procesó correctamente, para poder borrarlo

de la cola

• Si falla (o el mensaje no es confirmado después de un tiempo), se reenvía el mensaje al mismo u

otro consumidor

• Producer -> Exchange -> Queue

• Queue-> Exchange -> Consumer

RABBITMQ:
PUBLISH/
SUBSCRIBE

Request/Reply  Async

En RabbitMQ:

Message Delivery Guarantees

• At most once delivery (“best-effort):

Se envía el mensaje una única vez, no se espera un ack y tampoco se reenvía. Minimiza
utilización de recursos, se pueden perder datos.

Util para: Telemetria, metricas, datos de eventos, donde los datos son  reemplazados y/o
enviados a un ritmo alto.

• At least once delivery: Se envía el mensaje y si no hay un ack después de cierto tiempo se reenvía
el mensaje. Hay cierto overhead de confirmar que se recibir los mensajes, asegura que no queden
mensajes sin procesar.

Útil para: Transacciones, chats, control de IoT.

Si se agrega Idempotencia a los mensajes se logra exact once processing

• Exactly once processing: Los mensajes son idempotentes, existe ack y si viene un mensaje

duplicado se detecta y no se procesa de nuevo. Por ejemplo, cuando se va a guardar los datos del
mensaje en la DB, se detecta la ID duplicada y no se sigue procesando el mensaje.

Exactly Once = At least once delivery + Idempotence

Invalid Message Channel

Distintas situaciones pueden impedir el procesamiento de un mensaje.
- datos faltantes
- errores en la estructura
- etc

Enviando el mensaje a un canal especial, permite un posterior logging, debugging, monitoreo, alertas,
etc.

Recordar asincronismo!

Invalid Message Channel

Distintas situaciones pueden impedir el procesamiento de un mensaje.
- datos faltantes
- errores en la estructura
- etc

Enviando el mensaje a un canal especial, permite un posterior logging, debugging, monitoreo, alertas,
etc.

Recordar asincronismo!

Dead-letter queue

• Algunos brokers soportan configurar una

dead-letter queue

• Los mensajes que no se pueden enviar o

fallan repetida veces, pueden terminar en
la dead-letter

• Permite guardar los mensajes fallidos

para luego analizar qué ocurrió

Orden de los mensajes

Muchas veces, los brokers no garantizan el orden de los mensajes. Esto permite mayor
throughput (por ejemplo,  enviar mensajes sin esperar que el anterior haya sido confirmado)

Mensaje envenenado
(Poison Messages)

• Mensaje que falla repetidas veces y nunca se logra procesar
• En RabbitMQ (y en otras soluciones) se puede configurar un delivery-limit,  si el número de intentos de

procesar el mensaje supera el límite, se manda el mensaje a la dead letter queue o similar

Opciones
para utilizar
RabbitMQ

1. Instalación local (más sencillo)

• https://www.rabbitmq.com/install-windows.html

• Mac:

https://www.rabbitmq.com/install-homebrew.html

2. Utilizando Docker (recomendado)

• Muy fácil si ya tienen Docker instalado

• docker run -it -d --name rabbitmq -p

5672:5672 -p 15672:15672
rabbitmq:4-management

3. CloudAMQP (servicio web)

• No requiere correr RabbitMQ en su máquina

• Tiene plan gratuito (Little Lemur) no requiere tarjeta de

crédito

• RabbitMQ as a Service

• https://www.cloudamqp.com/

Interfaz web administrativa

Es un plugin que permite administrar la
instancia de RabbitMQ desde el navegador
• Permite visualizar la actividad y estado de

nuestro servicio

• Es necesario instalar un plugin si utilizan la
versión local ( si no lo tienen ya instalado)
• Usuario por defecto: guest Contraseña: guest
• Para entrar: http://localhost:15672
• También pueden utilizar la cli

• Nota: El usuario guest solo funciona con localhost

Checkpoint!

• ¿Que significa que la comunicación a través de MOM sea asincrónica?
• ¿Que desventajas puede tener el uso de MOM?
• ¿Cual es la diferencia entre el modelo publish/subscribe y competing

consumers (Work Queues)

• ¿Que tipo de exchange podemos utilizar para enviar todos los

mensajes a todos los consumidores?

• MOM se usa para comunicar _________ (procesos/hilos)

Referencias

• https://docs.oracle.com/cd/E19340-01/820-6424
/aeraq/index.html#:~:text=Object%20Request%2
0Broker%20or%20ORB,by%20sending%20and%2
0receiving%20messages.

• https://www.rabbitmq.com/tutorials/amqp-conc

epts.html

• https://www.rabbitmq.com/tutorials/tutorial-on

e-dotnet.html

Material complementario

RABBITMQ: Streams

• Un nuevo tipo de Queue
• Casos de uso: Fanouts grandes, los mensajes no se eliminan nunca,
time-travel/replay.
• El consumidor eligen desde que momento (o mensaje) empezar a consumir
• Usa un append-only log
• Similar a lo que hacen algunos otros tipos de brokers (e.g. Kafka)

AMQP:
Header
Exchange

A recibe Mesg.1
B recibe Mesg.2 y Mesg.3
C no recibe nada.

AMQP: Topic
Exchange

Hay una estructura de árbol que
define los bindings/topics en el
sistema.

• Topic Exchange

RABBITMQ:
TOPICS

RABBITMQ: TOPICS
continuación

•Los intercambios de temas enrutan los mensajes a una o varias colas en
función de la coincidencia entre una clave de enrutamiento de mensajes y el
patrón que se usó para vincular una cola a un intercambio

• El tipo de intercambio de temas se usa a menudo para implementar diversas
variaciones de patrones de publisher/subscriber

• Los intercambios de temas se utilizan comúnmente para el enrutamiento de
mensajes de multidifusión

RABBITMQ: TOPICS
continuación

• Ejemplos:

• Distribuir datos relevantes para una ubicación geográfica específica, por ejemplo,

puntos de venta

• Procesamiento de tareas en segundo plano realizado por varios procesos, cada uno

capaz de manejar un conjunto específico de tareas

• Actualización de precios de acciones (y actualizaciones de otro tipos de datos

financieros)

• Orquestación de servicios de diferente índole en la nube
• Compilaciones o paquetes de software específicos de SO/arquitectura distribuida

donde cada constructor puede manejar solo una arquitectura o SO

FIN

