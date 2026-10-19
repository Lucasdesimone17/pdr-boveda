PROGRAMACIÓN DE REDES
INTRODUCCIÓN
2025

Universidad ORT

BIENVENIDOS!

● Docentes de la Materia:

● Gerardo Nuñez

- gerardo.nunez@fi365.ort.edu.uy

● Enrique Latorres

- enrique.latorres@fi365.ort.edu.uy

● Andres Soria

- andres.soria@fi365.ort.edu.uy

● Iván Monjardin

-

im239850@fi365.ort.edu.uy

● Gabriel Bentos Pereira

- gabriel.bentos@fi365.ort.edu.uy

● Guillermo Echichure

- ge151381@fi365.ort.edu.uy

● Gustavo García

- gustavo.garcia@fi365.ort.edu.uy
-

EVALUACIONES

• Métodos de contacto:

Teams, mail de los docentes, Aulas

• Obligatorios:

• Obligatorio 1 (25 puntos) *

● Entrega 28/04
• Obligatorio 2 (15 puntos)
● Entrega 27/05

• Obligatorio 3 (25 puntos) *

● Entrega 23/06
• Grupos de máximo 3 personas
• Todas las entregas son por Gestión
• * Defensas a coordinar por Teams
• Parcial (35 puntos)
• Fecha 30/06

PAUTAS DEL
CURSO

•Las diapositivas son una guía, no es el teórico.

• El obligatorio es la suma de los ejercicios
prácticos, fundamental ir al día con los
prácticos.

• En las clases, se prioriza resolver problemas
conceptuales, no de código.

TEMARIO

•Introducción

•Puesta a punto.

• Contexto de ejecución.

• Procesos y threads.

• Streaming

• Patrones, Tarea, Colas, Delegados.

•Multi-Threading.

• Tasks

• Programación Asincrónica

•Sockets

TEMARIO

• RPC

•  gRPC

• MOM

• RabbitMQ

• RestFul + Microservicios

• .NET Web API

HERRAMIENTAS
DE
DESARROLLO

• Visual Studio

• Visual Studio Code

• JetBrains Rider

• .NET (última version, para este curso .NET 8)

HERRAMIENTAS
DE
DESARROLLO

• A partir de .NET 5, el término .NET
Core dejó de utilizarse oficialmente, y
la plataforma pasó a llamarse
simplemente .NET.

• .NET 5 y posteriores (incluyendo .NET
6, .NET 7 y .NET 8) son
multiplataforma, soportando Windows,
Linux, y macOS de manera nativa.

• Un poco de historia

• Modelos

• Sistemas Distribuidos

• Sistemas Cliente/Servidor

• Modelo de Capas.

• Según la escala.

INTRODUCCIÓN

• Bloques.

• Cliente

• Servidor

• Middleware

• Requerimientos de software.

 Un poco de Historia

Evolución

 UN POCO DE
HISTORIA

•La era de los Mainframes.

• Primeras computadoras,  eran del
tamaño de una habitación.

• Aplicaciones monolíticas.

• Soluciones propietarias.

UN POCO DE
HISTORIA

•  La era del PC.

• Computadoras de tamaño más reducido.
• Más baratas.

• Las redes LAN Ethernet.

• Estandarización de los protocolos de red.
• Sistemas heterogéneos.
• Una empresa con varias computadoras y

software de distintos proveedores.

•  Aplicaciones distribuidas.

•  Cloud computing

 MODELOS

•Sistemas Distribuidos

•Es la forma genérica de definición.

•Son entidades lógicas autónomas que
trabajan juntas en una red para
cumplir una tarea.

•Se puede considerar como una tecnología particular dentro
de los sistemas distribuidos.

•Servidor – brinda servicio / Cliente – consume el servicio

•Relación 1 a muchos.

•Protocolos asimétricos.

• Independencia de la ubicación: Cliente y Servidor pueden
residir en un mismo host.

 MODELOS

 MODELOS

•Modelo de 2 Capas

•La lógica de la aplicación puede estar en el Cliente, en el
Servidor o en ambos.

•Es sencilla su implementación y desarrollo.

•Ideal para sistemas pequeños, menores a 10 usuarios.

•No escala bien.

 Modelos

● Modelo de 2 capas

GUI

Red

Servidor
Servidor

DB

Primera capa

Segunda capa

 MODELOS

• Modelo de 3 Capas

• La lógica se concentra en el servidor de

aplicaciones.
• Cliente liviano.

• Independencia entre lógica y datos.

• Es más complejo el desarrollo inicial.

• Requiere herramientas de desarrollo.

• Escala bien, es sencillo agregar nuevos

módulos.

 Modelos

●Modelo de 3 capas

GUI

Red

Servidor
de
Aplicaciones

Servidor

DB

Primera capa

Segunda capa

Tercera capa

 MODELOS

•Modelo de N capas.

•Se puede ver como una generalización del modelo de
3 capas, cuando la capa lógica, está conformada por
“n” componentes lógicos.

•Cada componente realiza una función específica del
negocio.

•Los componentes interactúan entre ellos.

•Permite el desarrollo gradual, en pasos pequeños.

•Rehúso de componentes.

•Requiere frameworks complejos de desarrollo.

Modelos de N capas

 Modelos

Revisar
credito

Revisar
stock

Enviar
aviso

Solicitud
De
servicio

Hacer factura

Internet

Administración

Venta

Cliente

 MODELOS

• Según la escala.

• Sistemas básico.

• Para un 1 cliente.

• Aplicación Servidor, Middleware y Cliente corren en una

misma máquina.

• Este tipo de aplicaciones se podría diseñar en forma

monolítica en donde su desarrollo es mucho más rápido
pero no escala.

• No requiere herramientas complejas de desarrollo

• Frameworks que permiten rápidamente desarrollar apps

de este estilo

• Ej: PHP + Laravel/Codeigniter

 MODELOS

• Según la escala.

• Sistemas empresarial.

• Para 100 clientes.

• Aplicación Servidor corre en máquinas dedicadas para
este fin, el acceso humano a ellas es únicamente para su
administración.

• Aplicación Cliente corren en máquinas con poder de

procesamiento menor , manejadas directamente por los
clientes.

• Por lo general, cuando hay más de un servidor, entre

ellos no hay interacción.

 MODELOS

• Según la escala.

• Sistemas masivos.

• Para miles de clientes.

• Aplicación Servidor corren en máquinas dedicadas.

• Arquitecturas distribuidas, tendencia a dividir

responsabilidades en módulos específicos (o conjunto de
servicios)

• Las aplicaciones servidoras son híbridas, actúan como
servidoras para los clientes y como cliente para otros
servidores.

• Diseño escalable.

• Los clientes, corren en máquinas manejadas directamente

por el usuario.

 MODELOS

• Según la escala.

• Sistemas masivos en la nube

• Para miles de clientes.

• Aplicación Servidor:

• Entorno: corre en infraestructura de la nube bajo el
modelo de de Infrastructure as a Service (IaaS).

• Recursos: como ser (máquinas virtuales, almacenamiento,

redes) son proporcionados y gestionados por el
proveedor de la nube.

• Arquitecturas distribuidas, tendencia a dividir

responsabilidades en módulos específicos (o conjunto de
servicios).

 MODELOS

• Según la escala.

• Sistemas masivos en la nube

• Para miles de clientes.

• Aplicación Servidor:

•

•

Escalabilidad: Los recursos pueden escalarse
dinámicamente según la demanda, optimizando el uso y
los costos.

Interacción: Las aplicaciones servidoras actúan como
servidoras para los clientes y como clientes para otros
servicios en la nube, aprovechando servicios adicionales
(bases de datos, colas de mensajes, almacenamiento de
objetos) proporcionados por el proveedor de la nube.

• Según la escala.

• Sistemas masivos en la nube

• Para miles de clientes.

• Aplicación Servidor:

• Gestión: La gestión de la infraestructura (redes,

hardware, y en algunos casos, el sistema operativo) es
responsabilidad del proveedor de la nube, permitiendo
a los desarrolladores enfocarse en la lógica de la
aplicación y sus servicios.

 MODELOS

 MODELOS

• Según la escala.

• Sistemas masivos en la nube

• Para miles de clientes.

• Aplicación Cliente:

•

•

Entorno: Corre en máquinas manejadas directamente por
los usuarios.

Interacción: Los clientes se comunican con las aplicaciones
servidoras en la nube a través de APIs y protocolos
estándar de Internet.

• Capacidad de Procesamiento: Las máquinas cliente
pueden tener menor poder de procesamiento
comparado con los servidores, ya que la carga de
trabajo más pesada se maneja en la nube.

 Bloques

Cliente

Red
o
Middlware

Servidor

 Descripción de Bloques

Red o Middleware
 Es todo lo que existe entre medio de las entidades lógicas que

forman el sistema distribuido. Puede ser un stack de
protocolos de red o un framework que haga transparente al
programador los detalles de la red.

WEB Services         CORBA          REST

gRPC (.NET)            RMI (Java)

RPC     NFS    MOM

TCP/IP   HTTP

DESCRIPCIÓN
DE BLOQUES

• Servidor

• Está formado por:

• Software que implementa los servicios.

• Hardware adecuado para la solución

• Ej:

• Motor de DB.

• PHP.

• Web, JBoss.

•Sistema operativo.

• Stack de protocolos de red.

• Parte servidora del middlware.

• Corba.

• MOM

• RPC.

• API socket TCP/IP

DESCRIPCIÓN
DE BLOQUES

• Cliente

• Está formado por:

• Software que implementa la aplicación cliente.

•Ej:  Browser.

•Aplicación propietaria GUI

•Cliente DB.

•Stack de protocolos de red.

•Parte cliente del middlware API.

•Corba.

•MOM

•RPC.

•API socket TCP/IP

REQUERIMIENTOS

• El software que implemente a un Servidor o Cliente

tiene  los siguientes requerimientos:

• Multi-thread.

•Para atender a varios clientes al mismo tiempo.

• Mecanismo de concurrencia:

•Sincronizar los datos comunes.

• Comunicación entre procesos.

•Permite la división en varios procesos, para escalar o para
robustez.

• Soporte de comunicación de red.

•Api de protocolos de red.

Mecanismos de seguridad

• Servicios de autenticación y autorización, para que el
servidor pueda asegurarse de la identidad de los
clientes y proteger la información.

REQUERIMIENTOS

Servicios orientados a objetos.

• Interacción con la parte del middlware que provee la

publicación de objetos, o webservices.

•Spoke - Hub:

• Conocido como ”Sistema San Francisco”.  Hub
centralizado que distribuye las peticiones
dentro del sistema

ARQUITECTURAS
DISTRIBUIDAS

•ESB:

• Enterprise Service Bus.
Variante de cliente –
servidor .

ARQUITECTURAS
DISTRIBUIDAS

•Point to point:

ARQUITECTURAS
DISTRIBUIDAS

 GOOGLE

•Al comienzo.

•1 Sun Ultra 200 Mhz, 256 MB
ram

•2 x 300 Mhz Dual Pentium II

•F50 IBM RS/6000

•Disk expansions.

 GOOGLE

• Actualidad.

•No hay datos oficiales, pero
se estima:

•+ 450.000 servidores, 16 GB
RAM, Pcs class.

•Cada dato está en por lo
menos 3 lugares diferentes.

Netflix Playback Architecture

Netflix Backend Architecture

