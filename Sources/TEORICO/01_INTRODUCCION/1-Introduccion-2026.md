PROGRAMACIÓN DE REDES
Introducción — 2026
Universidad ORT Uruguay

CONOCIÉNDONOS
Sobre ustedes
Nombre Materias
1 2
¿Qué otras materias estás cursando este
semestre?
Expectativas Trabajo
3 4
¿Escuchaste algo de la materia? ¿Trabajás? ¿De qué?
¿Qué te imaginás o esperás? ¿Está relacionado con programación?

Equipo Docente
N6A — ID N6A — AN
Andres Soria Enrique Latorres
andres.soria@fi365.ort.edu.uy enrique.latorres@fi365.ort.edu.uy
Gabriel Bentos Pereira Santiago Pucciarelli
gabriel.bentos@fi365.ort.edu.uy sp245481@fi365.ort.edu.uy
M6A — ID M6A — AN
Karen Hirshfield Iván Monjardín
karen.hirshfield@fi365.ort.edu.uy im239850@fi365.ort.edu.uy
Renzo Chiarino German Martinez
rc232953@fi365.ort.edu.uy gm284226@fi365.ort.edu.uy
Los obligatorios se pueden hacer con alumnos de otras clases

Evaluaciones
| Obligatorios |     |     |     |     | Un proyecto en grupo, dividido en 3 entregas |     |     |     |     |     |     |     |     |
| ------------ | --- | --- | --- | --- | -------------------------------------------- | --- | --- | --- | --- | --- | --- | --- | --- |
65 puntos
Parcial
35
puntos
| Obl. 1 |     | 25 pts |     | Obl. 2 | 15 pts | Obl. 3 |     | 25 pts |     |     |     |     |     |
| ------ | --- | ------ | --- | ------ | ------ | ------ | --- | ------ | --- | --- | --- | --- | --- |
Semana 16  •  29/6
| Entrega: 23/04 |     |     |     | Entrega: 26/05 |     | Entrega: 22/06 |     |     |     |     |     |     |     |
| -------------- | --- | --- | --- | -------------- | --- | -------------- | --- | --- | --- | --- | --- | --- | --- |
| Semana 6       |     |     |     | Semana 11      |     | Semana 15      |     |     |     |     |     |     |     |
Grupos de máximo 3 personas
Calendario — 16 semanas + turismo
| 1   | 2   | 3   | T   | 4   | 5 Ob1 | 7   | 8   | 9   | 10 Ob2 | 12  | 13  | 14 Ob3 | Pa. |
| --- | --- | --- | --- | --- | ----- | --- | --- | --- | ------ | --- | --- | ------ | --- |
Exoneración: 86 puntos o más Derecho a examen: 70 puntos o más

Temario del Curso
Sujeto a cambios
1 2 3 T 4 5 6 7 8 9 10 11 12 13 14 15 16
09/03 16/03 23/03 30/03 06/04 13/04 20/04 27/04 04/05 11/05 18/05 25/05 01/06 08/06 15/06 22/06 29/06
Threads + Locks S. T Sockets Docker Async MOM gRPC REST + WS Dudas Parcial
Detalle semanal
1 09/03 Introducción y Threads 10 11/05 MOM
2 16/03 Threads y Mutua Exclusión
11 18/05 MOM
3 23/03 Threads + Sockets
12 25/05 gRPC
4 30/03 Semana de Turismo
13 01/06 API REST, WebSocket
5 06/04 Sockets
14 08/06 Dudas Obligatorio 3
6 13/04 Sockets
15 15/06 Dudas parcial + Defensas Obl. 3
7 20/04 Docker
16 22/06 Parcial
8 27/04 Async/Await
9 04/05 Async/Await

Métodos de Contacto
Grupo de Teams de la clase Chat de Teams
G C
Canal principal de comunicación Para dudas del obligatorio
Novedades y anuncios del curso Dudas específicas del obligatorio
que no se puedan responder
por el grupo general
Preguntas generales sobre la materia
Crear un chat con todos los
Coordinar defensas de obligatorios
integrantes del grupo + ambos
docentes del curso
Información sobre clases y horarios
Respuesta más rápida y directa
Por favor crear el chat con todos los integrantes + ambos docentes para respuestas más rápidas

Herramientas de Desarrollo
IDEs
| JetBrains Rider | Visual Studio | Visual Studio Code |
| --------------- | ------------- | ------------------ |
Multiplataforma Buena opción para Windows Con extensions o usando la
CLI
★
Recomendado
Otras Herramientas
| .NET 8 / 10 Core | Postman               | Docker              |
| ---------------- | --------------------- | ------------------- |
| Última versión.  | O similar (ej: Bruno) | Contenedores        |
| Multiplataforma  | Tercer obligatorio    | Segundo obligatorio |

Pautas del Curso
Material del curso
1
Las diapositivas son una guía, no son el teórico completo.
Obligatorios y prácticos
2
El obligatorio es la suma de los ejercicios prácticos. Es fundamental ir al día con el obligatorio y los
prácticos.
Enfoque de las clases
3
En las clases se prioriza resolver problemas conceptuales.

SECCIÓN 01
Introducción
Objetivos
1 Conocer la evolución histórica de los sistemas distribuidos
2 Entender los modelos de arquitectura (2, 3 y N capas)
3 Identificar los bloques fundamentales: cliente, servidor y middleware
4 Comprender los requerimientos de software distribuido

Un poco de Historia
Evolución

La Era de los Mainframes
Primeras computadoras del tamaño de una
habitación
Terminales "tontas" conectadas al mainframe
central
Soluciones 100% propietarias (IBM, etc.)
Sin interoperabilidad entre fabricantes
Todo el procesamiento centralizado

Un Poco de Historia
La era del PC
Computadoras de tamaño más reducido
Aparece el concepto de computadora personal
Las redes LAN Ethernet
Estandarización de los protocolos de red
Aparecen aplicaciones como email
Aplicaciones distribuidas
Cloud computing

Sistemas Distribuidos
Definición
Es la forma genérica de definición.
Son entidades lógicas autónomas que trabajan juntas en
una red para cumplir una tarea.
Las partes del sistema pueden estar en diferentes máquinas, pero
colaboran como si fueran una sola entidad.

SECCIÓN 02
Modelos de Arquitectura
Objetivos
1 Explicar la diferencia entre arquitecturas de 2, 3 y N capas
2 Identificar ventajas y desventajas de cada modelo
3 Reconocer cuándo aplicar cada modelo en escenarios reales

Modelo Cliente Servidor
Se puede considerar como una tecnología
particular dentro de los sistemas
distribuidos
Servidor – brinda servicio / Cliente –
consume el servicio
Relación 1 a muchos
Protocolos asimétricos
Independencia de la ubicación: Cliente y
Servidor pueden residir en un mismo host

Modelo de 2 Capas
Lógica puede estar en cliente, servidor o ambos
El servidor gestiona los datos (BD)
SSeerrvviiddoorr
GUI
Red
DB
Simple de implementar para apps pequeñas
No escala bien para sistemas grandes
Primera Segunda
capa capa
Simple y rápido No escalable

Modelo de 3 Capas
Separa claramente la presentación de la lógica
Modelo de 3 capas
●
La lógica de negocio es independiente de la UI
Servidor
de
Cada capa puede evolucionar por separado
GUI Servidor
Red
Aplicacio
nes
Mejor escalabilidad que 2 capas
DB
Estándar en desarrollo web moderno
Primera
Segunda Tercera
capa
capa capa
Escalable y flexible Más complejo

Modelo de N Capas
Generalización del modelo de 3 capas, donde la capa lógica está conformada por "n" componentes lógicos independientes.
Cada componente realiza una función específica Rehúso de componentes
1 4
del negocio
Los componentes interactúan entre ellos Requiere frameworks complejos de desarrollo
2 5
Permite el desarrollo gradual, en pasos pequeños
3
← Capa Lógica (N componentes) →
Componente Componente
Presentación Componente C Datos
A B

Modelos de N Capas — Diagrama
Revisar
Revisar stock
credito
Enviar
aviso
Solicitud
De Hacer factura
servicio
Cliente
Internet
Venta
Administración

Comparación de Modelos
|               | 2 Capas      | 3 Capas  |                        | N Capas   |
| ------------- | ------------ | -------- | ---------------------- | --------- |
| Complejidad   | Baja         | Media    |                        | Alta      |
| Escalabilidad | Limitada     | Buena    |                        | Excelente |
| Mantenimiento | Difícil      | Moderado |                        | Fácil     |
| Caso de uso   | Apps simples | Web apps | Sistemas empresariales |           |
N Capas agrega capas adicionales (caching, messaging, etc.) para alta escalabilidad y sistemas masivos en la nube

Modelos según la Escala
Sistema Básico Para 1 cliente
Servidor, Middleware y Cliente corren en una misma
Una sola máquina
máquina
Se podría diseñar en forma monolítica — desarrollo
Cliente
mucho más rápido pero no escala
No requiere herramientas complejas de desarrollo
Middleware
Frameworks que permiten rápidamente desarrollar apps
de este estilo
Ejemplo: PHP + Laravel/Codeigniter
Servidor

Modelos según la Escala
Sistema Empresarial Para ~100 clientes
Servidor corre en máquinas dedicadas, acceso solo para
administración
Servidor Clientes
Dedicado (~100)
Cliente corre en máquinas con menor poder de
procesamiento, manejadas por los usuarios
Solo administración Menor procesamiento
Manejadas por usuarios
Generalmente, cuando hay más de un servidor, entre
ellos no hay interacción
Los servidores no interactúan entre sí — solo atienden
clientes directamente.

Modelos según la Escala
Sistema Masivo Para miles de clientes
Servidor: máquinas dedicadas con arquitecturas distribuidas
Tendencia a dividir responsabilidades en módulos específicos (microservicios)
Apps servidoras son híbridas: servidoras para clientes y clientes para otros servidores
Diseño escalable desde su concepción
Clientes: máquinas manejadas directamente por el usuario
💡 Las aplicaciones servidoras actúan como servidoras para clientes y como clientes para otros servidores

Sistemas Masivos en la Nube — Servidor
Nube Para miles de clientes
Entorno (IaaS) Escalabilidad Gestión
Corre en infraestructura de la nube Recursos se escalan dinámicamente Infraestructura (redes, hardware,
bajo el modelo Infrastructure as a según demanda, optimizando uso y SO) es responsabilidad del
Service. Recursos proporcionados costos. Apps actúan como proveedor. Desarrolladores se
por el proveedor. servidoras y clientes de otros enfocan en la lógica de la aplicación.
servicios.

Discusión
Una app web de e-commerce con miles de usuarios concurrentes.
¿Qué modelo de arquitectura elegirías?
A 2 Capas — Es más simple de implementar
B 3 Capas — Separa presentación, lógica y datos
C N Capas — Agrega caching y messaging para escalar
D No importa el modelo, cualquiera funciona

SECCIÓN 03
Bloques Fundamentales
Objetivos de Aprendizaje
1 Describir los componentes de un cliente y un servidor
2 Entender el rol del middleware en sistemas distribuidos
3 Conocer los requerimientos de software distribuido

Cliente y Servidor
Cliente Servidor
Interfaz de usuario (GUI o CLI) Motor de BD, servicios web, etc.
Parte cliente del middleware Parte servidora del middleware
API de red (sockets TCP/IP) API de red (sockets TCP/IP)
Sistema operativo Sistema operativo servidor
Hardware adecuado Hardware de alto rendimiento
Ej: Browser, App Mobile, Desktop

Middleware
Software que conecta componentes distribuidos, ocultando la complejidad de la red
WEB Services CORBA REST
gRPC (.NET) RMI (Java)
RPC NFS MOM
TCP/IP HTTP

SECCIÓN 04
Ejemplos de Arquitecturas

Arquitecturas Distribuidas
Spoke-Hub
Conocido como "Sistema San Francisco"
Todas las comunicaciones pasan por el hub central —
punto único de coordinación.
Hub centralizado que distribuye las peticiones dentro
del sistema.

Arquitecturas Distribuidas
ESB
Enterprise Service Bus
Variante de cliente-servidor.
Conecta múltiples servicios con diferentes
protocolos a través de un bus central de
comunicación.

Arquitecturas Distribuidas
Point to Point
Comunicación Directa
Cada nodo se comunica directamente con otros
nodos sin pasar por un intermediario central.
Más descentralizado pero puede ser más complejo
de gestionar a medida que crece.

Google
Al comienzo
1 Sun Ultra 200 Mhz, 256 MB RAM
2 x 300 Mhz Dual Pentium II
F50 IBM RS/6000
Disk expansions

Google
Actualidad
No hay datos oficiales, pero se estima:
+450.000 servidores, 16 GB RAM, PCs class
Cada dato está en por lo menos 3 lugares
diferentes

Netflix
Playback Architecture

Netflix
Backend Architecture

?
¿Preguntas?