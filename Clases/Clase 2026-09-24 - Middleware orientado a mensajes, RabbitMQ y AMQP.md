---
date: 2026-09-24
type: class-note
tags:
  - pdr
  - clase
  - middleware
  - rabbitmq
ai-first: true
confidence: high
duracion: "1h 44min"
fuente: "Grabacion del showcase de Vimeo 12367503, video 1230000324, subtitulos autogenerados es-x-autogen"
---

# Clase 2026-09-24 - Middleware orientado a mensajes, RabbitMQ y AMQP

## For future agent
Acta de la clase del **2026-09-24**, la **ultima antes de la defensa** del Obligatorio 1. Dos bloques: teoria de middleware orientado a mensajes (MOM) con RabbitMQ y AMQP, que es materia nueva y apunta al Obligatorio 3; y en el medio, **la logistica de la defensa**, que es lo urgente. Escrita desde la transcripcion automatica: los nombres propios y alguna cifra dicha al pasar no son confiables y van como TBD.

## Lo urgente: la defensa

Dicho alrededor del minuto 58 al 61 de la clase.

- **Cronograma.** Entrega el **lunes**. El **martes** hay clase normal. La **defensa es el jueves**, y puede extenderse hasta el **martes siguiente** si no da el tiempo. Con el calendario de entrega del 28-09: entrega lunes 28-09, clase martes 29-09, defensa jueves 01-10, continuacion martes 06-10.
- **Planilla por Teams** para anotarse en el horario que cada grupo quiera. **Tiene que ir todo el grupo.**
- Es **en un laboratorio**, en las maquinas de ahi. Dos docentes toman en paralelo, cada uno a un grupo distinto.
- **La wifi tiene microsegmentacion: dos maquinas conectadas por wifi NO se pueden comunicar entre si.** Textual: *"si se conectan dos por wifi, de hecho, no se pueden conectar"*. Hay que usar las maquinas del laboratorio, que si se comunican entre si.
- **Puede hacer falta desactivar el firewall.**
- **Hay que probar con el Release de GitHub**, no con la compilacion local. Textual: *"la idea es que prueben el release, que se bajen el release de GitHub y que usen el release que ustedes van a publicar en GitHub"*.
- Las IP se cambian **por el archivo de configuracion**, sin recompilar.
- Recomendacion explicita: **probarlo antes en las maquinas del laboratorio**. El docente conto que cuando el cursaba la materia probaba todo en su propia maquina, levantando varias instancias, y eso no cubre el caso real.
- **Como es la defensa.** Se muestra la aplicacion corriendo en dos maquinas, los docentes siguen una **planilla de testing** (*"hace cliente uno, cliente dos, hace tal cosa; cliente dos, que vea que hizo el cliente uno"*), se verifica que **ninguna aplicacion se cierre ni explote**, y ademas hay **preguntas de codigo**. Las dos cosas, no una.

## Middleware orientado a mensajes (MOM)

**Que es un middleware.** Software que conecta dos aplicaciones o servicios y resuelve la logica de intercambio de informacion. El termino aparece en **1968**, en una conferencia de NATO Software Engineering, para bibliotecas o software intermediario reutilizable entre proyectos.

**Para que se usa.** Interconectar sistemas de empresas que se fusionan sin tocar ninguno de los dos (*"si te pones a toquetear los sistemas, casi seguro que armas un lio"*); conectar arquitecturas o tecnologias distintas; integrar una web con una base de datos.

**Tres tipos principales:**

| Tipo | Que hace | Sincronia |
|---|---|---|
| **MOM** (message-oriented middleware) | Intercambia mensajes entre sistemas | Asincronica |
| **RPC** (remote procedure call) | Invoca un procedimiento en la otra aplicacion | Sincronica |
| **API** | Da herramientas al desarrollador para crear, eliminar, gestionar | - |

Tambien se menciono **ORB** (object request broker), de la epoca de la programacion orientada a objetos distribuida: *"estuvo muy de moda hace 20 años"*, hoy desplazado por los otros modelos.

**Dato relevante para el curso:** el docente dijo que **lo que se hizo hasta ahora con sockets tambien es un middleware**, el de la biblioteca. La diferencia es que un MOM de verdad trae administracion, resiliencia y diagnostico que seria muy dificil programar a mano.

**Ventajas.** Asincronia (mando el mensaje y sigo con otra cosa), transformacion de mensajes al vuelo entre representaciones distintas, enrutado inteligente por tipo de mensaje, debil acoplamiento, medicion y diagnostico.

**Desventajas, y el docente aclaro que son discutibles.** Agrega un componente mas a la arquitectura, es un **punto mas de falla**, y es **ajeno**: si el proveedor falla, dependes de un tercero. La contracara es que no lo programas vos.

**Ejemplo de transformacion de datos.** Las cadenas en C y C++ se codifican con un caracter nulo al final; en Pascal y Ada llevan un numero al principio con la cantidad de bytes. El MOM transforma entre representaciones.

## AMQP, exchanges y colas

El protocolo del curso es **AMQP**. Tambien se nombro **MQTT**, muy usado en IoT, telemetria, datos meteorologicos y ambientales, con **Mosquitto** como servidor clasico; y **HLA**, orientado a objetos, usado en simulacion y control aereo.

**Arquitectura basica:** productor, **exchange** (el enrutador), colas, consumidores. El exchange no se programa, se configura con reglas, y segun atributos del mensaje decide en que cola lo pone.

**Tipos de exchange:**

| Tipo | Comportamiento | Analogia |
|---|---|---|
| **direct** | Enruta directo a una cola por la *binding key* | - |
| **topic** | Arbol jerarquico, el consumidor se suscribe a una rama | Suscribirse a un canal de YouTube |
| **fanout** | A todas las colas | broadcast |
| **headers** | Por atributos del header, con match de tipo *all* o *any* | - |

El ejemplo de **topic** fue un arbol de paises: mundo, luego Estados Unidos, Europa, Asia, Latinoamerica, y dentro de Europa España, Italia, Alemania. Uno se suscribe al nivel del arbol que le interesa.

**AMQP sobre TCP con canales virtuales.** Se establece **una** conexion TCP con el servidor y dentro de ella se abren **multiples canales bidireccionales**. Eso permite que distintas tareas usen distintas colas y exchanges por la misma conexion.

**Sobre la trama AMQP:** tiene tipo de mensaje, el canal al que va dirigido, parametros opcionales y el mensaje. El docente aclaro que *"pocas veces nos va a interesar esto"*, salvo para diagnosticar.

## Patrones de mensajeria

- **Work queue / competing consumers.** El primer consumidor que queda libre agarra el mensaje. Sin planificacion: uno puede recibir muchos mensajes cortos y otro pocos largos.
- **Fair dispatching.** Se reparte de a uno entre todos los consumidores, equitativo.
- **Publish-subscribe.** El publicador manda al exchange y este reparte a las colas segun el mecanismo de seleccion; los consumidores se suscriben.
- **Request-reply.** Estilo RPC. El cliente crea una **cola temporal** (nombre autogenerado, del estilo `amq.gen-` mas un chorizo de letras y numeros), la indica en el atributo `reply to` del mensaje, y el servidor responde por ahi. Al desconectarse, la cola desaparece.
- **Correlation id.** Necesario porque **un cliente puede tener multiples hilos haciendo multiples solicitudes**: hay que saber a que tarea corresponde cada respuesta, y pueden llegar en orden distinto al que se enviaron. El docente insistio en este punto: *"son multiples codigos corriendo en multiples lugares, mandandose multiples mensajes"*.

## Garantias de entrega

| Garantia | Que hace | Cuando se usa |
|---|---|---|
| **at most once** (best effort) | Envia una vez, sin acknowledge ni reenvio | Logs, donde perder uno no importa |
| **at least once** | Reenvia si no llega el acknowledge en cierto tiempo | Chats, control de IoT, transacciones |
| **exactly once** | at least once mas **idempotencia** | Cuando reprocesar rompe datos |

La idempotencia es la clave del **exactly once**: si llegan cinco copias del mismo mensaje, la primera modifica la base de datos y las siguientes no. El docente sugirio ademas **cache del lado del servidor de servicios** para no reprocesar lo mismo.

**Sobre el orden:** RabbitMQ usa **FIFO**, pero no todos los brokers garantizan el orden. Amazon SQS puede ordenar por best effort.

## Dead letter queue

Cola especial donde van los mensajes que **fallaron multiples veces** al procesarse, o que son invalidos por datos faltantes o error de lectura. Se configura un **delivery limit**, un tope de reintentos; superado, el mensaje va al dead letter queue y queda marcado como *poison*.

Aplica tanto con un solo consumidor que falla repetidamente como con varios consumidores que tampoco pueden procesarlo. Un receptor tambien puede rechazarlo explicitamente en vez de hacer acknowledge. Resolverlo es **responsabilidad del administrador**, que analiza por que fallaron.

## RabbitMQ en la practica

- **Se instala con Docker**, con la imagen que trae el **plugin de administracion**.
- **Puerto 5672**: la API, por donde se conecta la aplicacion. El otro puerto es la pagina web del administrador. TBD el numero del puerto web: se dijo al pasar y la transcripcion lo confunde con el 5672.
- Usuario por defecto **guest**, password **guest**, que es el administrador. Sirve para el curso.
- **`guest` solo funciona desde localhost.** Para conectarse desde otra maquina hay que crear otro usuario. Dato importante si se prueba entre dos equipos.
- En los ejemplos del curso, **el primero que se conecta arma el exchange y las colas**. En produccion eso no se hace: las colas ya estan creadas y los usuarios tienen password.
- El panel web muestra mensajes encolados, enviados y recibidos, con graficos en tiempo real y configuracion de usuarios, exchanges, colas y bindings.
- Se menciono **CloudAMQP** como alternativa en la nube para pruebas livianas, sin tarjeta de credito en su momento y con limite de mensajes. TBD si sigue siendo asi.

**Otros brokers nombrados:** Kafka, Apache ActiveMQ, Google Cloud Pub/Sub, Mosquitto, Amazon SQS. Se dijo que **Amazon SQS nacio adaptando RabbitMQ**, que es software libre, aunque a esta altura pueden diferir bastante.

## Avisos y proximos pasos
- **La proxima clase son ejemplos de codigo**: fabrica de conexiones, pedir canales, vincular canales a exchanges y colas, armar mensajes y asignar funciones delegadas para recibirlos.
- Se menciono el **Obligatorio 3** al pasar, en el ejemplo de fanout: un servidor que recibe inscripciones y ventas, y varios servidores de informes que necesitan una copia de todos los mensajes para mantenerse sincronizados.
- El docente ofrecio consultas hasta el domingo por los canales habituales.

## Salvedades de la transcripcion
- Se nombra a **Enrique**, presumiblemente [[Enrique Latorres]], y a un **Guille**, sin confirmar quien es: TBD.
- Cerca del minuto 1:09 la transcripcion incluye lo que parece **una lista de apellidos del pasaje de lista**, mal transcripta. No se registran aca por no atribuir mal.
- El puerto de la pagina web del administrador quedo confundido con el 5672 en la transcripcion: verificar contra la documentacion de RabbitMQ antes de usarlo.

## Connections
- [[Programacion de Redes 2026]] · [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]]
- [[Estado del Obligatorio 1 - 2026-09-24]] - el estado del entregable
- [[Clase 2026-09-17 - Task en detalle, continuations, ThreadPool y TPL]] - la clase anterior registrada
- [[Concurrencia de Thread a Task]] · [[Reglas y prohibiciones del curso PDR 2026]]
