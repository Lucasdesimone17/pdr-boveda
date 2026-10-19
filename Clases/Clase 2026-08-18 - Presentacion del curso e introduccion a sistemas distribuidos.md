---
date: 2026-08-18
type: clase
tags:
  - clase
  - pdr
ai-first: true
confidence: stated
fuente: "Grabacion Vimeo, showcase 12367503, video 18-08-2026, 1h47m, subtitulos autogenerados"
docentes:
  - Enrique Latorres
  - Natalia Franco
---

# Clase 2026-08-18 - Presentacion del curso e introduccion a sistemas distribuidos

## For future agent
Acta de la primera clase del semestre, reconstruida de la transcripcion automatica de la grabacion en Vimeo (showcase 12367503, video del 18-08-2026). Cubre presentacion de docentes, como se va a dar la materia, la mecanica de los tres obligatorios, herramientas, canales de comunicacion, y el arranque del teorico de introduccion a sistemas distribuidos. Las fechas y pautas se propagaron a [[Programacion de Redes 2026]] y a `CRITICAL_FACTS.md`. Salvedad: la transcripcion es automatica y tiene errores en nombres propios; lo dudoso va marcado como `TBD`.

## Docentes
- **Enrique Latorres** - profesor del teorico. Ingeniero en computacion / ingeniero en sistemas, master en ingenieria en computacion, doctorado en curso. Da clases en ORT desde hace ~35 anos. Jubilado de la industria; trabajo en cargos de gerencia de centro de computos, infraestructura y telecomunicaciones (Antel, Union Fenosa, Ministerio de Transporte y Obras Publicas). Hoy mantiene redes de sismometros y estaciones climaticas con convenio de Antel; menciono que probablemente viaje a la Antartida en verano por la estacion de la base Artigas. Desarrolla en **Linux**.
- **Natalia Franco** - docente que acompana el teorico. Analista, haciendo la tesis de la licenciatura.
- Ver [[Enrique Latorres]] y [[Natalia Franco]].

## De que va la materia (dicho por el docente)
- Junta lo visto en **Infraestructura** y en **Redes** y lo arma en **arquitecturas distribuidas**: clientes y servidores que se comunican y hacen tareas en forma distribuida.
- El foco son los **mecanismos de comunicacion entre procesos** que estan en maquinas diferentes, mas **programacion concurrente**: cuando un servidor atiende muchos clientes, genera un hilo por cliente que responde sus solicitudes.
- Lo de Infraestructura (monitores, regiones criticas, semaforos, mutua exclusion) se vuelve a usar, pero con la implementacion de C# / .NET. Advertencia repetida en todas las clases:

> "Microsoft lo que tiene es que a las mismas cosas de siempre les pone nombres diferentes." ([05:30])

- Lo de Redes deja de ser teorico: hay que hacer que la aplicacion se conecte con otra en otra maquina, **disenar un protocolo de comunicacion**, convertir datos a bytes y mandarlos por un stream.
- Recorrido anunciado: **sockets, Remote Procedure Call, MOM tipo RabbitMQ, REST API, gRPC (la implementacion de RPC de Google), WebSockets**. Extra posible: **MQTT** para IoT, que "no esta en el programa" pero el docente dijo que va a intentar prepararles algo.
- Programacion asincronica se presenta como cambio de mentalidad, no como detalle: busca **usar los hilos de forma mas eficiente** aprovechando que otras tareas estan esperando.

## Mecanica de los tres obligatorios (segun esta clase)
- **Es un mismo obligatorio que se va ampliando**, no tres trabajos distintos.
  - **Obligatorio 1**: escenario de una empresa donde hay que comprar y vender cosas. Arquitectura cliente-servidor simple, con sockets a bajo nivel.
  - **Obligatorio 2**: mismo esqueleto, se cambian los mecanismos de comunicacion y se pasa a **programacion asincronica**. El docente lo describio como casi mecanico: "vas y cambias la declaracion de las funciones y se hace bastante directo... es muy liviana" ([42:22]).
  - **Obligatorio 3**: el mas pesado. Varios servidores con distintos servicios y distintas formas de comunicacion; un servidor que tarda mucho en obtener algo, con vuelta posterior a buscar el resultado o **callback**. Ademas, "seguramente les pidamos que todo eso lo metan adentro de un **Docker Compose**".
  - Contraste explicito con Diseno 2: aca no se rehace desde cero entre entregas. "Esto hermoso que hiciste, bueno, cambiale alguna cosita, usa otra tecnologia, a entregar" ([42:22]).
- **Grupos: recomendado de tres.** Se puede mezclar gente de otros grupos y turnos; el unico requisito es ir todos juntos a la defensa, con el grupo docente que el equipo elija.
- **Repositorio en GitHub**: se crea un grupo/organizacion donde los estudiantes se registran y suben todo el proyecto de cada unidad.
- Consigna dura sobre IA generativa: los docentes la usan y no la prohiben, pero exigen entender el codigo linea por linea.

> "Yo voy a agarrar asi y voy a decir: 'Explicame esto. Por que funciona esto?'" ([34:15])

- Los docentes **dan codigos de ejemplo**, de bajo nivel y de demostracion. El estudiante los integra, hace clases razonables y reutiliza (clases comunes entre cliente y servidor para los mecanismos de comunicacion). No piden una estructura supersofisticada.
- El docente comento que **el porcentaje de exoneracion suele ser alto** si se lleva la materia al dia. Un estudiante pregunto si se exonera con 86; no hubo respuesta explicita. Umbral: **TBD**.

## Canales, herramientas y material
- **Teams es el mecanismo de comunicacion oficial.** Grupo general para avisos y para publicar los codigos; chats con ambos docentes para dudas (poner a los dos, responde el primero que puede); tambien se usa para **inscribirse a las defensas** segun horarios y fechas disponibles. Responsabilidad del estudiante revisarlo periodicamente, puede haber cambios de fecha.
- **Aulas**: las presentaciones del teorico. Aviso explicito: "son mas bien una guia... no esta todo dicho en las diapositivas" ([63:15]). Hay que tomar apuntes de lo que se dice en clase.
- Herramientas: el docente prefiere **JetBrains Rider**; tambien sirve Visual Studio. Licencias estudiantiles con el **correo institucional**. **Postman** para armar clientes REST sin programarlos.
- **Toda la programacion es de consola.** No se usa interfaz grafica: las bibliotecas graficas de Windows no corren en el Linux del docente. Van a tener varias terminales abiertas haciendo de servidor y clientes.
- Aviso: "casi seguro" pidan **una interfaz web** este semestre. Motivo: pedido de **la gente de Seguridad**, que quiere que el obligatorio de esta materia sirva de base para una **electiva de seguridad del semestre siguiente** (hardening de autenticacion y de comunicaciones). Recomendacion: guardar el repositorio para reusarlo alli.
- Tambien por pedido de Seguridad, es probable que en alguna entrega haya que usar **un servidor de base de datos** en vez de solo estructuras en memoria. El motivo de usar estructuras en memoria es pedagogico: si se usa un motor, el motor resuelve la concurrencia y el estudiante no la programa.

## Teorico: introduccion a sistemas distribuidos
Objetivos declarados: conocer la evolucion historica de los sistemas distribuidos, los modelos de arquitectura (dos, tres, multicapa) e identificar los bloques fundamentales cliente / servidor / middleware.

### Cliente, servidor y middleware son roles relativos
- Un cliente es el que tiene al usuario; se conecta a un servidor que, a su vez, es cliente de otro servidor. **Todo es relativo al rol en cada interaccion.**
- **Middleware**: aplicacion que esta en el medio de otras dos. Visto desde la funcion, el middleware es servidor de los dos que estan a los costados; pero entre esos dos hay a su vez relacion cliente-servidor por el tipo de mensaje que se manda y el resultado que se devuelve.
- El middleware **no tiene que ser una aplicacion separada**: puede ser simplemente la biblioteca que uso para conectarme.

### Linea historica (relato del docente)
- **1960s - mainframes**: muchas terminales tontas, poca CPU para los estandares actuales pero mucha capacidad de entrada/salida, canal de E/S muy optimizado, aplicaciones simples en modo texto. Lo importante era atender a muchos clientes a la vez. IBM apostaba a que todos tuvieran una terminal tonta en casa y contrataran servicios de sus mainframes. Anecdota larga sobre el nacimiento de la IBM PC: el Z80 de Zilog era el lider en procesadores chicos, CPM portado al 8088/8086, y la jugada de licenciar el sistema operativo en vez de venderlo. Todavia quedan mainframes en UTU y Antel.
- Anecdota tecnica util: en MVS existia el **reader**, una cola de mensajes de tamano limitado usada tanto para comunicacion entre procesos como para mensajes entre usuarios. Un volcado de datos demasiado grande lleno el reader y **colgo toda la comunicacion entre procesos del mainframe**. Caso real de saturacion de una cola compartida; ata con [[Tema 4 - Colas de mensajes y MOM]].
- **1980s-1990s**: tarjetas Ethernet y LAN, primeras redes inalambricas (al principio mas transferencia de datos por radio que LAN), enlaces de radio entre edificios, WAN, era web. El docente fue director de la Camara Uruguaya del Software e hizo campana para promover Internet en Uruguay.
- **Hoy**: infraestructura distribuida real. Ejemplo propio: red de sismometros sobre VPN interna en LTE de Antel (canal privado, no celular), servidores en la nube de Presidencia / AGESIC, **Red UI** como intranet del Estado, y la base Artigas en la Antartida con **dos medios de comunicacion: Intelsat (LTE) y Starlink (wifi)**.
- Moraleja practica: entender lag y tiempos de respuesta cambia el diseno. Conto un caso donde **un solo parametro de retardo mal configurado dejo a una organizacion entera sin comunicacion**.

### De monolito a microservicios
- Ejemplo canonico usado en clase: **Mercado Libre**. Monolitico = login, pagos, seleccion de productos, carrito, todo en el mismo codigo.
- Observacion clave: **las distintas funciones tienen distintas necesidades de CPU, memoria y conectividad**. Login es instantaneo y barato; navegar y comparar productos consume mucho mas. Entonces conviene partirlo y dimensionar cada parte a su necesidad, cada una con su escalabilidad particular.
- Se pueden usar **tecnologias distintas por servicio** (algo mejor en C#, otra cosa mejor en JavaScript).
- Flujo de autenticacion descrito: dialogo corto, intercambio de certificados, genera un token, y **manda un redirect** a otra URL que atiende otra maquina.
- **Escalabilidad horizontal**: multiples servidores atendiendo, repartiendo clientes. "Esas cosas las vamos a hacer nosotros tambien, repartir las solicitudes entre multiples servidores" ([90:02]).
- Los sistemas nuevos **son necesariamente distribuidos**: los componentes dialogan para dar un unico servicio al usuario.
- Motivo economico: en la nube se paga por recurso, entonces se quiere gastar la cantidad justa. De ahi los disparadores que levantan y apagan servidores segun la carga.

### Modelos de capas

| Modelo | Complejidad | Escalabilidad | Uso tipico |
|---|---|---|---|
| Dos capas (cliente-servidor) | Baja | Limitada | Apps simples, intraoficina, sistemas basicos (a veces todo en una maquina) |
| Tres capas | Media | Media (el servidor de aplicaciones puede manejar varias BD) | Aplicaciones web sencillas, sistemas empresariales |
| N capas / microservicios | Alta | Alta, horizontal | Sistemas empresariales y cloud complejos, grandes volumenes |

- **Dos capas**: interfaz de usuario liviana + red + servidor (que suele manejar la base de datos). El cliente valida formato y transmite; el procesamiento va en el servidor. Es lo que se pide en el Obligatorio 1.
- **Tres capas**: la razon de fondo es **seguridad**. "Lo que tenga el usuario cerca es lo mas hackeable" ([94:05]). Si la logica de negocio esta lejos, por la red pasan solo datos crudos, los mismos que el usuario pone en la consola.
- Advertencia de seguridad concreta y repetida en dos clases: si la aplicacion cliente se conecta directo a la base de datos con **un unico usuario** y la autorizacion se verifica solo en la aplicacion, un atacante puede inspeccionar el proceso (incluso con cheaters de juegos), sacar usuario y password y conectarse directo al motor. La contramedida es la de Base de Datos 2: **usuarios, roles y permisos dentro del motor**, y restricciones del sistema puestas en la propia base de datos. Ademas, la ultima capa de datos tiene que poder identificar quien hizo cada cambio.
- **N capas**: microservicios; cumulo de servicios independientes que se conectan entre si de forma transparente, mas los que generan estadisticas y logs.
- Ventaja operativa del escalado horizontal que subrayo: **actualizar en produccion sin apagar nada**. Se levanta el servicio nuevo en paralelo, se derivan algunos clientes por DNS, se valida, y se van bajando los viejos. Tambien: decirle a un servidor "no aceptas mas clientes", esperar que se vacie, apagarlo y reemplazarlo.

> "En realidad, estoy haciendo testing gratis. [...] En realidad, sos un conejillo de indias." ([101:17])

- Contrapeso explicito: para una aplicacion de una pequena empresa **no tiene sentido** agregar esa complejidad.
- La escalabilidad tambien da **redundancia, control de riesgos y distribucion en multiples regiones**; incluso repartir entre varios proveedores de nube. El docente lo llamo indicador de madurez de la gestion de infraestructura: poder demostrar que en el peor caso se levantan todos los servicios en dos horas.

## Avisos y consignas de esta clase
- Armar grupos de estudio y definir el grupo del obligatorio (de tres).
- Jugar con los codigos de ejemplo, experimentar, plantear escenarios propios.
- Llevar el obligatorio al dia: si se lleva al dia, las dudas se resuelven en la clase siguiente.
- Se abre el grupo de Teams ese mismo dia.
- La proxima clase arranca **mutua exclusion en C#**: equivalentes de regiones criticas, regiones criticas condicionales, monitores, semaforos y **lectores-escritores**. Aclaro que en general se usan bibliotecas: por ejemplo un `ConcurrentDictionary` ya trae los bloqueos de lector-escritor adentro. "No las tienen que implementar, si tienen que entender como funcionan" ([45:30]).
- Tambien anuncio **productor-consumidor implementado probablemente con sockets**.

## Connections
- Tema teorico: [[Complementario - Introduccion y Streams]] · [[Mapa del curso Programacion de Redes]]
- Proyecto: [[Programacion de Redes 2026]]
- Siguiente clase: [[Clase 2026-08-20 - Modelos de arquitectura, procesos e hilos]]
