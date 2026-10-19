---
date: 2026-08-26
updated: 2026-10-13
type: project
tags:
  - project
  - pdr
status: active
ai-first: true
confidence: high
---

# Programacion de Redes 2026

## For future agent
Nota de proyecto del cursado de Programación de Redes (Universidad ORT, Ingeniería en Sistemas / Licenciatura en Sistemas), semestre que arranca el 18/08/2026. Reúne calendario, puntajes, material cargado y pendientes. Es el lugar donde se registra el avance del semestre; el contenido temático vive en [[Mapa del curso Programacion de Redes]].

## Objetivo de la materia
Del programa: presentar los mecanismos y tecnologías disponibles para **comunicación entre procesos, programación multi-hilos y elementos de seguridad** asociados al desarrollo de aplicaciones interoperables. Lenguaje del curso: **C#** / .NET.

## Calendario y evaluación

| Fecha | Hito | Puntos |
|---|---|---:|
| 2026-08-26 | Lectura del Obligatorio 1 | - |
| 2026-09-28 | Entrega Obligatorio 1 + lectura del Obligatorio 2 | 25 |
| 2026-10-29 | Entrega Obligatorio 2 + lectura del Obligatorio 3 | 15 |
| 2026-11-23 | Entrega Obligatorio 3 | 25 |
| 2026-11-30 | Parcial | 35 |

Total 100 puntos. Las fechas vienen sin año en la fuente y se leen como 2026 (ver ítem 12 de [[Huecos y contradicciones del material PDR]]).

Del ciclo anterior (obligatorios de marzo 2026, referencia de formato): **la defensa es obligatoria y eliminatoria**, no presentarse implica perder todos los puntos del obligatorio; el Obligatorio 1 tenía mínimo 5 de 25 puntos, entrega por `gestion.ort.edu.uy` hasta las 21:00, y defensas coordinadas por el grupo de Teams. Hay pautas explícitas sobre uso de IA generativa: seguir las instrucciones de los docentes, citar la herramienta y el uso, verificar lo generado y ser responsable del resultado.

**Confirmado por la letra oficial (as of 2026-09-10).** El PDF de la letra del Obligatorio 1 está ingerido en `Sources/OBLIGATORIO/Obligatorio 1 Agosto 2026.pdf.md`, y confirma lo que se había reconstruido de las grabaciones: entrega **2026-09-28 21:00**, mínimo 5 y máximo 25 puntos, entrega por gestión (si no, cero), tamaño máximo 40 MB, código en GitHub con **release** y `main` congelada después del hito, defensa presencial en laboratorio con Windows 10/11, plan de test y **nota individual**.

**Lo que el PDF agregó y no estaba en la bóveda**, en orden de urgencia:
1. **El repositorio debe ser único, privado y pertenecer a la organización `https://github.com/orgs/ArqSis-PDR-2026-2`**, con el número de estudiante en el nombre (ej. `m6a-ingenieria-123456-9876543`). Solo se acepta lo entregado en gestión y el código en ese repo.
2. **La defensa es obligatoria y eliminatoria**: no presentarse implica perder la totalidad de los puntos.
3. **Grupos de hasta 3 personas del mismo dictado** - contradice lo que se dijo en clase el 18-08 sobre mezclar turnos (ítem 22 de [[Huecos y contradicciones del material PDR]]).
4. **La documentación debe citar las herramientas de IA usadas y el contexto de uso.**
5. El sistema se llama **EDISON**, y la trama sugerida tiene largos exactos: HEADER 3 / CMD 2 / LARGO 4 / DATOS variable, con relleno de valor 0.

Consigna completa y reconciliada en [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]]; reglas duras en [[Reglas y prohibiciones del curso PDR 2026]]. Sobre las defensas del 2 y del 3 siguen las afirmaciones contradictorias entre clases: ver [[Huecos y contradicciones del material PDR]].

## Estructura del curso
16 semanas, régimen teórico-práctico de 4 horas semanales en dos clases de 2 horas. Seis temas: multihilo (1-3), sockets (4-6), asincronía (7-8), colas de mensajes (9-11), RPC/gRPC (12-14), APIs REST (15-16).

### Docentes y canales (as of 2026-09-09)
- **[[Enrique Latorres]]** - profesor del teórico. **[[Natalia Franco]]** - docente que acompaña. **Gabriel** - catedrático.
- **Teams es el canal oficial**: avisos, códigos de ejemplo (zip por tema), planillas de inscripción a defensas y cambios de fecha.
- **Aulas**: las presentaciones, que son "una guía, no está todo dicho en las diapositivas". También hay un práctico ahí y en el Drive.
- **GitHub**: hay un grupo/organización donde los estudiantes se registran y suben el proyecto de cada unidad.
- La clase del 08-09 la dictó **otro docente**, con foco práctico; la transcripción no permite confirmar el nombre.

### Cómo se encadenan los tres obligatorios (dicho por el docente el 18-08)
Es **un mismo sistema que se va ampliando**, no tres trabajos distintos:
1. **Obligatorio 1** - cliente-servidor simple con **sockets a bajo nivel** y `Thread`.
2. **Obligatorio 2** - mismo esqueleto, se cambian los mecanismos de comunicación y se pasa a **programación asincrónica**. El docente lo describió como un cambio casi mecánico. **Confirmado por la letra (as of 2026-10-13):** `async`/`await` sin `Thread`, Docker sin multi-stage, base de datos en Docker, y cancelacion de reservas con comprobante. Ver [[Obligatorio 2 - Upgrade asincrono y Docker de EDISON]].
3. **Obligatorio 3** - el más pesado: **varios servidores** con distintos servicios y distintos mecanismos de comunicación, un servidor lento con recuperación posterior o **callback**, y "seguramente" todo dentro de un **Docker Compose**.

Además: es "casi seguro" que este semestre pidan **una interfaz web**, a pedido de la gente de Seguridad, porque el obligatorio de esta materia va a ser la base de una **electiva de seguridad del semestre siguiente** (hardening de autenticación y comunicaciones). Conviene guardar el repositorio. Por el mismo pedido, es probable que la tercera entrega use **un servidor de base de datos** en vez de solo estructuras en memoria.

### Grabaciones de las clases
Vimeo showcase **12367503**, un video por clase, con subtítulos autogenerados en español (etiquetados por error como italiano). Actas por clase en `Clases/`.

### Registro de clases dadas

| Fecha | Duración | Contenido | Acta |
|---|---|---|---|
| 2026-08-18 | 1h47 | Presentación, mecánica de obligatorios, introducción a sistemas distribuidos, modelos de capas | [[Clase 2026-08-18 - Presentacion del curso e introduccion a sistemas distribuidos]] |
| 2026-08-20 | 1h33 | Cierre de arquitectura, multiprogramación, proceso vs hilo, mapa de memoria, cambio de contexto, primer `Thread` | [[Clase 2026-08-20 - Modelos de arquitectura, procesos e hilos]] |
| 2026-08-27 | 1h59 | **Lectura de la letra del Obligatorio 1**, estados, prioridades, `Join`, parámetros, el bug del `for` | [[Clase 2026-08-27 - Letra del Obligatorio 1 y practica con Thread]] |
| 2026-09-01 | 1h43 | `Monitor`, `lock`, `Wait`/`Pulse`, `Mutex`, `Semaphore`; JVM vs .NET; arranque de sockets | [[Clase 2026-09-01 - Exclusion mutua en CSharp y arranque de sockets]] |
| 2026-09-03 | 51m | Demo medida de minimizar bloqueos (40 s vs 4,3 s); API de sockets completa | [[Clase 2026-09-03 - Minimizar bloqueos y sockets en detalle]] |
| 2026-09-08 | 1h25 | Organización de la solución, IO bound vs CPU bound, framing, encodings, **`Receive` parcial** | [[Clase 2026-09-08 - Protocolo propietario, framing y Receive parcial]] |
| 2026-09-10 | 1h40 | **Máquina de estados** cliente/servidor por seguridad, proyecto `Common`, `app.config` + `ConfigurationManager`, `HostToNetworkOrder`, apagado prolijo | [[Clase 2026-09-10 - Maquina de estados del protocolo, Common y app.config]] |
| 2026-09-15 | 1h42 | Transferencia de archivos en **chunks de 32 KB**, `FileStreamHelper`, `Stream` y cursor, dos bugs plantados, imágenes y persistencia de EDISON; arranque de asincronía | [[Clase 2026-09-15 - Transferencia de archivos por chunks e introduccion a asincronia]] |
| 2026-09-17 | 1h45 | `Task` en detalle: `ContinueWith`, excepciones, `WhenAll`/`WhenAny`, `ThreadPool`, TPL, `CancellationToken`, desayuno 15 s vs 6 s | [[Clase 2026-09-17 - Task en detalle, continuations, ThreadPool y TPL]] |
| 2026-09-22 | ~53m (cortada) | Asincronia vs paralelismo, `Parallel.For`/`ForEach`/`Invoke`, PLINQ, **`CancellationToken`** completo, **tokens enlazados**, cuando usar `async` | [[Clase 2026-09-22 - Parallel, CancellationToken y cuando usar async]] |
| 2026-09-24 | 1h44 | Middleware orientado a mensajes, RabbitMQ y AMQP; logistica de la defensa | [[Clase 2026-09-24 - Middleware orientado a mensajes, RabbitMQ y AMQP]] |
| 2026-09-29 | TBD | **Sin acta**: el video esta en el showcase pero sin subtitulos (as of 2026-10-13) | - |
| 2026-10-08 | 1h41 | **Lectura de la letra del Obligatorio 2**; ejemplos de RabbitMQ (durable, direct, topic) y **patron request-reply con correlation id** | [[Clase 2026-10-08 - Letra del Obligatorio 2 y patron RPC sobre RabbitMQ]] |

**Cobertura verificada al 2026-09-21**: el showcase tenía **9 videos** y había **9 actas**, una por video, del 18-08 al 17-09. **Actualizado al 2026-09-27**: el showcase de Vimeo **12367503** ahora tiene **11 videos** y pide **contraseña**; en `Clases/` hay **10 actas**, del 18-08 al 24-09, y **falta la del 22-09** (publicada en el showcase pero todavía sin ingestar). Las clases son **martes y jueves**. El unico martes/jueves del periodo sin clase es el **2026-08-25**, que **tampoco tiene video** en el showcase; lo mas probable es que no haya habido clase por ser feriado nacional (Declaratoria de la Independencia), pero **eso no esta confirmado por el material** - si aparece material de ese dia, falta el acta.

**Actualizado al 2026-10-13**: el showcase tiene **13 videos**, del 18-08 al 08-10. Ya **no pide contraseña** desde Chrome. Hay **12 actas**: se ingestaron la del 22-09 y la del 08-10. **Falta la del 29-09**, que no tiene subtitulos: reintentar mas adelante. No hay video del 01-10 ni del 06-10, dias de defensa del Obligatorio 1.

## Material cargado en la bóveda

Convertido de PDF/PPTX/DOCX a markdown con markitdown el 2026-08-26 (**84 archivos, 0 fallos**), en `Sources/`:

| Carpeta | Contenido |
|---|---|
| `Sources/TEORICO/` | Teóricos oficiales por unidad + guías de threads y sockets + programa + resumen StuDocu |
| `Sources/PARCIALES/` | 21 letras de parcial y examen con 21 soluciones (2019-2026) |
| `Sources/PRACTICOS/` | Práctico 1 |
| `Sources/OBLIGATORIO/` | **`Obligatorio 1 Agosto 2026.pdf.md` - la letra vigente** (ingerida el 10/09; original en `PDR/PDR/OBLIGATORIO/CONSIGNA/`) |
| `Sources/OBLIGATORIO/HISTORICO_MAR2026/` | Los tres obligatorios de marzo 2026, solo referencia de formato |
| `Sources/ESTUDIO_PREVIO/` | 61 notas de estudio ya elaboradas: resúmenes, cheatsheets y preguntas por tema, análisis de frecuencia de parciales, repaso final |

Notas propias: 10 en `Literature/`, 4 en `Synthesis/`, 1 en `Methodology/`, 1 en `Hypotheses/`.

## Key Decisions
- 2026-09-27 - La documentacion de los obligatorios sigue el formato y la voz impersonal de la entrega de DA2 del usuario, incluye solo lo que pide la letra y apunta a 20-30 paginas. Ver [[Decision - formato y voz de la documentacion del Obligatorio 1]].
- 2026-08-26 - **El temario vigente de Aulas (6 temas) manda** sobre el PDF histórico de 8 unidades. Web Services y Arquitecturas Cloud se conservan como referencia, no como huecos del curso vigente.
- 2026-08-26 - El corpus de estudio previo se copia dentro de la bóveda como fuente cruda (`Sources/ESTUDIO_PREVIO/`) para que la bóveda sea autocontenida; no se edita.
- 2026-08-26 - Nombres de archivo nuevos en ASCII sin acentos, por la mezcla NFC/NFD del material original.

## Pendientes

### Obligatorio 2, entrega 2026-10-29 21:00 (as of 2026-10-13)
Consigna en [[Obligatorio 2 - Upgrade asincrono y Docker de EDISON]]. Las mismas tareas son tarjetas en `Boards/Research.md`.
- **Migrar todo de `Thread` a `Task` con `async`/`await`.** Hay cinco puntos en el codigo: el hilo de `Accept`, el hilo por cliente, los `Join` de `Edison.Server/Program.cs` y `ReservationProcessor`.
- **Requerimiento 5:**
  - cancelar reservas pendientes con `CancellationToken`;
  - un comando nuevo en el protocolo;
  - avisar al cliente;
  - un comprobante en archivo para las aprobadas;
  - un plazo de aprobacion mas largo.
- **Dockerizar cliente y servidor sin multi-stage:**
  - release para Linux con `COPY`;
  - configuracion fuera de `app.config`;
  - imagenes y comprobantes visibles desde el host;
  - `ReadKey` al final de las aplicaciones.
- **Base de datos en Docker** (MySQL u Oracle) con al menos alguna entidad. Las imagenes no van a la base.
- **`docker-compose.yml`**, opcional en la letra pero recomendado en clase.
- **Documentacion nueva:** que cambio, por que y que beneficio trae, mas la guia de Docker. Conviene un branch con el codigo viejo congelado.
- **Aplicar la devolucion de la defensa del Obligatorio 1**, que todavia no esta registrada en la boveda.
- Pedir por Teams los codigos de RabbitMQ y RPC del 08-10.

### Obligatorio 1: cerrado
Entregado el 28-09. Release `v1.1` sobre `main` `0756597`, PDF de 19 paginas con el punto 9 completo; ver [[Estado del Obligatorio 1 - 2026-09-30]]. Defensa el 01-10 o el 06-10: **el resultado no esta registrado** (as of 2026-10-13). Las tareas que siguen son **historicas**: quedan como registro del cierre.

**Actualizado al 2026-09-27**, tras el cierre y la auditoría del Obligatorio 1. Detalle completo en [[Auditoria de cierre del Obligatorio 1 - 2026-09-27]] y [[Estado del Obligatorio 1 - 2026-09-27]]; son las mismas tareas que las tarjetas nuevas de `Boards/Research.md`.

### Bloqueantes para la entrega del 28-09
- **Redactar el punto 9 (uso de IA) en la documentación y regenerar el PDF y el zip.** El punto 9 sigue con `[COMPLETAR]` en `DOCUMENTACION.md` línea 424.
- **Confirmar que los tres integrantes figuran Inscriptos en Gestión**, antes de subir el zip.
- **Subir el zip a Gestión antes de las 21:00 del 28-09.**
- **Crear el GitHub Release sobre `main` `8e80066`** con el PDF final, recién cuando esté listo; después no tocar `main`.
- **Corregir la documentación en la misma regeneración**: fila 99 del catálogo, 8.2 paso 3, reserva sin unidades, punto 2 (errores conocidos) y `LEEME.txt`.

### Para la defensa del 01-10
- **Anotarse en la planilla de defensa de Teams.** Ninguna nota del 27-09 registra que se haya hecho.
- **Ensayar el Release en dos PC cableadas del laboratorio** (clase del 29-09), con `0.0.0.0` en `Edison.Server.dll.config`.
- **Decidir si se aplica el parche candidato de defensa** (4 archivos, 14 líneas más y 1 cambiada), antes del Release.

### Logística y bóveda
- **Ingestar la clase del 22-09**, publicada en el showcase de Vimeo pero sin acta todavía.
- **Santiago verifica su email `slouro03` en GitHub**, para que sus commits queden vinculados a su cuenta.
- **Recrear `Develop` desde `main` después del Release.**

### Pendientes generales del curso (sin cambios, ver `Boards/Research.md`)
- Hacer el práctico de Aulas / Drive; bajar y correr modificados los zip de ejemplos de Teams (threads y sockets).
- Declarar como supuesto qué se entiende por **"compras realizadas"** en CR10 (ítem 23 de [[Huecos y contradicciones del material PDR]]).
- Confirmar umbral de exoneración y la modalidad/defensa de los Obligatorios 2 y 3.
- Resolver los huecos sin material oficial: Web Services, teoría cloud, seguridad en REST. Ver [[Huecos y contradicciones del material PDR]].

### Resueltos desde la última revisión
- ~~Armar el grupo (hasta 3, del mismo dictado)~~ - los tres integrantes figuran como admin del repo (verificado 2026-09-27).
- ~~Pedir acceso a la organización de GitHub `ArqSis-PDR-2026-2` y crear el repo privado~~ - repo privado, `main` en `8e80066` (verificado 2026-09-27).
- ~~Confirmar el formato del archivo de entrega~~ - se aplicó la mitigación por defecto: zip con el PDF adentro (`EDISON - Obligatorio 1 - [repo-del-grupo].zip`).
- ~~Arrancar el Obligatorio 1 con el plan de los docentes~~ - código completo: los diez requerimientos de cliente y los cuatro de servidor implementados (verificado 2026-09-27).

## Contexto del corpus heredado
El material de `Sources/ESTUDIO_PREVIO/` fue elaborado por otra sesión de trabajo, con la cuenta de Aulas de [[Juan Francisco Constantin Bonjour]], y su roadmap declara un equipo de estudio con [[Tomas Moreira]] e [[Ignacio Adorjan]] (as of 2026-08-18). La relación de ese equipo con el dueño de esta bóveda no consta en el material: no asumirla.

## Recent Activity
- 2026-10-13 - Ingesta de las clases del **22-09** y del **08-10**, de la **letra oficial del Obligatorio 2** y de los ejemplos de codigo de Teams. Los ejemplos son `04_Async_ejs`, `ThreadExamples` y dos `.cs` de streams.
  - Notas nuevas: [[Obligatorio 2 - Upgrade asincrono y Docker de EDISON]], las dos actas y [[Ejemplos de codigo de asincronia de clase]].
  - Novedades del Obligatorio 2:
    - entrega 29-10, 5 a 15 puntos;
    - **sin defensa propia, se defiende con el tercero**;
    - `Thread` prohibido;
    - Docker **sin multi-stage**;
    - base de datos en Docker;
    - cancelacion de reservas con `CancellationToken` y comprobante.
  - La clase del 29-09 no tiene subtitulos y queda sin acta.
- 2026-09-27 - Auditoría de cierre del Obligatorio 1 (37 agentes: 8 revisores, verificadores adversariales por hallazgo y un crítico de completitud), ver [[Auditoria de cierre del Obligatorio 1 - 2026-09-27]]. Código y binarios OK; prueba real con dos clientes OK (flujo completo, tramas basura, cortes a mitad de trama, carrera por la última unidad, reinicio con estado). Faltan: el punto 9 (uso de IA), el Release de GitHub, la subida a Gestión, la planilla de defensa de Teams y el ensayo en el laboratorio.
- 2026-09-27 - Cierre de la documentacion y arreglos finales, ver [[2026-09-27 - Obligatorio 1 EDISON]] y [[Estado del Obligatorio 1 - 2026-09-27]]. Tres fallas graves arregladas y mergeadas a `main` (`8e80066`, PR #28 y #29): una cabecera con bytes invalidos tiraba el servidor, el precio `38.900` se guardaba como 38,9 y una carpeta de descarga invalida cerraba el cliente. Documentacion reescrita con el formato y la voz de la entrega de DA2 del usuario, siete diagramas UML estilo Astah en PlantUML y 22 paginas. Pendiente: el punto 9 (uso de IA), que redacta el grupo, el Release y la subida a Gestion.
- 2026-09-24 - Ingestada la clase del 24-09, ver [[Clase 2026-09-24 - Middleware orientado a mensajes, RabbitMQ y AMQP]]. Trae **la logistica de la defensa**: entrega lunes 28-09, clase martes 29-09, **defensa jueves 01-10** con posible continuacion el martes 06-10, planilla por Teams, en laboratorio y con todo el grupo presente. **La wifi tiene microsegmentacion y dos maquinas por wifi no se comunican.** Hay que **probar bajando el Release de GitHub**, no la compilacion local. Falta ingestar la clase del **22-09**, que tambien esta publicada.
- 2026-09-24 - Registro del hueco del 22 al 24, ver [[Estado del Obligatorio 1 - 2026-09-24]]. El **PR #25 `feature/pulido-final`** (`3660c71`) cerro el lock global sobre lecturas, borro el codigo sin llamador, habilito los compilados en el `.gitignore` y sumo persistencia con `StateStore` y datos de prueba con `SeedData`. **La documentacion quedo escrita**: `.md`, `.docx`, `.pdf` y diagramas en `Entrega Obligatorio 1/`, fuera del repo. Pendiente critico: los compilados **siguen sin agregarse** al repositorio, y `GetReservations` y `GetActivity` siguen con `Take` fijo sin medir bytes.
- 2026-09-22 - Re-auditoria sobre `Develop` en `a1fdbf3`, ver [[Estado del Obligatorio 1 - 2026-09-22]]. Los PR #21 a #24 cerraron reservas, imagenes, historial, el disparador del proceso de aprobacion, la clave de configuracion de los segundos y la instancia unica del repositorio de reservas. **Cobertura 85-90%.** Quedan abiertos: los compilados Release excluidos por el `.gitignore`, `GetReservations` y `GetActivity` paginando con `Take` fijo sin medir bytes, el lock global sobre lecturas, y **toda la documentacion**.
- 2026-09-21 - Sesion de ingesta y auditoria, ver [[2026-09-21 - Obligatorio 1 EDISON]]. Se completaron las **9 actas de clase**, se audito el codigo contra la letra en [[Estado del Obligatorio 1 - 2026-09-21]], se arreglaron dos bugs de la capa de protocolo y se documentaron las decisiones en [[Decisiones de la capa de protocolo - Obligatorio 1]]. **Al cierre el grupo pusheo los PR #21, #22 y #23**: reservas, historial e imagenes por socket, o sea lo que el informe listaba como faltante. `Develop` quedo en `d679608` y la auditoria hay que rehacerla.
- 2026-09-21 - Ingesta de las **tres grabaciones faltantes** (10/09, 15/09 y 17/09) del showcase de Vimeo. Tres actas nuevas en `Clases/`. Novedades que impactan la entrega: el servidor tiene que ser una **máquina de estados** espejo de los menús; **`app.config` + `ConfigurationManager`** es el mecanismo con el que se configura la IP en la defensa; la transferencia de archivos va en **chunks de 32 KB** con **dos bugs plantados a propósito** en el ejemplo (el `Append` que duplica el archivo y el archivo parcial que hay que borrar); hay que **borrar la imagen al borrar el modelo** y lo van a chequear; el Obligatorio 1 se **reusa en la optativa de Seguridad**; y se reafirmó que **`Task` no va en el Obligatorio 1**, con la advertencia de que la IA lo mete por defecto. Ítems 26-27 en [[Huecos y contradicciones del material PDR]].
- 2026-09-10 - Ingesta del **PDF oficial de la letra del Obligatorio 1**. La consigna dejó de ser una reconstrucción de transcripciones y pasó a estar respaldada por el documento: cada afirmación quedó marcada como `(letra)` o `(clase DD-MM)`. Aparecieron la **organización de GitHub obligatoria**, la **defensa eliminatoria**, la restricción de **mismo dictado**, la citación de IA en la documentación, el nombre **EDISON** y los largos exactos de la trama. Cuatro ítems nuevos en [[Huecos y contradicciones del material PDR]] (21-24).
- 2026-09-09 - Ingesta de las **seis grabaciones de clase** del showcase de Vimeo (18/08 al 08/09). Seis actas nuevas en `Clases/`, la consigna del Obligatorio 1 desglosada, las reglas duras consolidadas, dos notas de personas y `CRITICAL_FACTS.md` actualizado.
- 2026-08-26 - Bóveda inicializada, 84 fuentes convertidas, 16 notas propias escritas, corpus previo incorporado como fuente.

## Connections
- Evaluación en curso: [[Obligatorio 2 - Upgrade asincrono y Docker de EDISON]] · [[Reglas y prohibiciones del curso PDR 2026]]. Cerrado: [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]].
- Docentes: [[Enrique Latorres]] · [[Natalia Franco]].
- Entrada: [[Mapa del curso Programacion de Redes]]. Método: [[Plan de estudio y repaso PDR]].
- Transversales: [[Elegir tecnologia de comunicacion]] · [[Concurrencia de Thread a Task]] · [[Patrones de examen en Programacion de Redes]] · [[Errores caros en Programacion de Redes]].
- Materia hermana en su propia bóveda: Base de Datos 3.
