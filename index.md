---
date: 2026-09-10
updated: 2026-10-13
type: index
tags:
  - index
ai-first: true
---

# Index

## For future agent
Catalogo de la boveda de Programacion de Redes (Universidad ORT, C# / .NET). Leer esto antes de buscar: dice que hay y donde, con una linea por nota.

Contenido as of **2026-10-13**:
- En `Sources/`: **261 archivos crudos**, de los que 147 son markdown y 63 codigo `.cs`.
- **12 actas de clase** en `Clases/`, del 18-08 al 08-10, menos la del 29-09, que no tiene subtitulos.
- 14 literature notes y 7 sintesis.
- 2 notas de metodologia, 1 registro de huecos (29 items), 10 notas de proyecto y 7 personas.

**Reorganizado el 2026-10-13**: el punto de entrada pasa al **Obligatorio 2** y todo lo del Obligatorio 1 queda agrupado como cerrado.

## Punto de entrada: Obligatorio 2 (entrega 2026-10-29)
- [[Obligatorio 2 - Upgrade asincrono y Docker de EDISON]] - **la consigna vigente**, contra la letra oficial y lo dicho en clase el 08-10. Pide:
  - `async`/`await` con `Thread` prohibido;
  - Docker sin multi-stage;
  - base de datos en Docker;
  - cancelacion de reservas con `CancellationToken` y comprobante.
  - 5 a 15 puntos. Se defiende junto con el tercero.
- [[Reglas y prohibiciones del curso PDR 2026]] - todo lo obligatorio o prohibido, con una seccion nueva del Obligatorio 2. La regla de `Thread` se invierte.
- [[Programacion de Redes 2026]] - proyecto del cursado: calendario, registro de clases, pendientes del Obligatorio 2, actividad reciente.
- [[Ejemplos de codigo de asincronia de clase]] - analisis de `04_Async_ejs`, `ThreadExamples` y los ejemplos de streams. Incluye el **cliente/servidor async** y el **Dockerfile** de la catedra, la base para migrar EDISON.
- [[Mapa del curso Programacion de Redes]] - los dos temarios reconciliados, los 6 temas vigentes, calendario y puntajes.
- [[Home]] - dashboard de la boveda.

## Clases del semestre 2026
Actas de las grabaciones del showcase de Vimeo **12367503**, con subtitulos autogenerados que se extraen con Claude in Chrome. Una nota por clase, en `Clases/`.
- [[Clase 2026-08-18 - Presentacion del curso e introduccion a sistemas distribuidos]] - presentacion, mecanica de los tres obligatorios, canales, modelos de capas, monolito a microservicios.
- [[Clase 2026-08-20 - Modelos de arquitectura, procesos e hilos]] - cierre de arquitectura, multiprogramacion, proceso vs hilo, mapa de memoria del proceso, cambio de contexto, primer `Thread`.
- [[Clase 2026-08-27 - Letra del Obligatorio 1 y practica con Thread]] - **lectura completa de la letra del 1**, criterios de correccion, estados, prioridades, `Join`, el bug del `for`.
- [[Clase 2026-09-01 - Exclusion mutua en CSharp y arranque de sockets]] - `Monitor`, `lock`, `Wait`/`Pulse`, `Mutex`, `Semaphore`; JVM vs .NET; TCP vs UDP.
- [[Clase 2026-09-03 - Minimizar bloqueos y sockets en detalle]] - la demo medida de 40 s vs 4,3 s, y la API de sockets completa.
- [[Clase 2026-09-08 - Protocolo propietario, framing y Receive parcial]] - organizacion de la solucion, IO bound vs CPU bound, largo fijo, encodings, **`Receive` parcial**.
- [[Clase 2026-09-10 - Maquina de estados del protocolo, Common y app.config]] - el servidor como **maquina de estados** espejo de los menus, `Common`, `app.config` + `ConfigurationManager`, `HostToNetworkOrder`, apagado prolijo.
- [[Clase 2026-09-15 - Transferencia de archivos por chunks e introduccion a asincronia]] - **chunks de 32 KB**, `FileStreamHelper`, `Stream`, **dos bugs plantados a proposito**, imagenes y persistencia, arranque de Tema 3.
- [[Clase 2026-09-17 - Task en detalle, continuations, ThreadPool y TPL]] - `ContinueWith`, excepciones en `Task`, nunca `async void` ni `Thread.Sleep`, `CancellationToken`, el desayuno de 15 s vs 6 s.
- [[Clase 2026-09-22 - Parallel, CancellationToken y cuando usar async]] - asincronia no es paralelismo, `Parallel.For`/`ForEach`/`Invoke`, PLINQ, `CancellationToken` con buenas practicas, **tokens enlazados**, cuando no usar `async`. Grabacion cortada en el minuto ~52.
- [[Clase 2026-09-24 - Middleware orientado a mensajes, RabbitMQ y AMQP]] - MOM, RabbitMQ y AMQP, mas **la logistica de la defensa del 1**.
- 2026-09-29 - **sin acta**: el video no tiene subtitulos (as of 2026-10-13).
- [[Clase 2026-10-08 - Letra del Obligatorio 2 y patron RPC sobre RabbitMQ]] - **lectura comentada de la letra del 2**, colas durables en RabbitMQ nuevo, direct y topic, y **request-reply con correlation id** y `TaskCompletionSource`.
- [[Clase 2026-10-13 - Usuarios de RabbitMQ y Docker para el Obligatorio 2]] - RabbitMQ con **usuarios de permisos minimos**, teoria de Docker y la **receta sin multi-stage** del Obligatorio 2: cliente tambien en contenedor, variables de entorno, `-it` y release en el repo.
- [[Clase 2026-10-15 - RPC, gRPC y Protocol Buffers]] - **arranque del Tema 5**: RPC generico, stubs y skeletons, MOM vs RPC, gRPC sobre HTTP/2, Protobuf, cuatro tipos de servicio y la plantilla gRPC de .NET.

## Literature - temas vigentes
Cada nota de tema tiene un bloque `## Dicho en clase` con lo que agrego el docente, por fecha.
- [[Tema 1 - Programacion multihilo]] - semanas 1-3. Threads, lock, Monitor, Mutex, Semaphore, ResetEvents. 94% de frecuencia.
- [[Tema 2 - Sockets y protocolos]] - semanas 4-6. Socket, TcpListener/TcpClient, framing, servidor multi-cliente. 88%.
- [[Tema 3 - Asincronia y paralelismo]] - semanas 7-8. Task, async/await, Parallel, PLINQ, cancelacion. 88%. Lo dicho en clase el 22-09 y el 08-10.
- [[Tema 4 - Colas de mensajes y MOM]] - semanas 9-11. AMQP, RabbitMQ, exchanges, ack, garantias de entrega. 81%. Lo dicho en clase el 24-09 y el 08-10.
- [[Tema 5 - RPC y gRPC]] - semanas 12-14. RPC, ORBs, gRPC, HTTP/2, Protobuf, los 4 tipos de servicio. 81%.
- [[Tema 6 - APIs REST y WebSockets]] - semanas 15-16. REST, las 9 restricciones, Web API, capas, DI, WebSockets. 69%.

## Literature - evaluacion
- [[Obligatorio 2 - Upgrade asincrono y Docker de EDISON]] - **vigente**, ver punto de entrada.
- [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]] - consigna del Obligatorio 1 **EDISON**, contra el PDF oficial: CR1-CR10, SR1-SR4, trama del protocolo, reglas del repositorio, defensa eliminatoria. **Cerrado**, pero es la base funcional del 2.

## Literature - material de codigo
- [[Ejemplos de codigo de asincronia de clase]] - `04_Async_ejs.zip`, `ThreadExamples.zip`, `FileStreamTextMenu.cs` y `SimpleTableCRUD.cs`, ingeridos de Teams el 13-10.
- [[Ejemplos de codigo de sockets de clase]] - los **8 proyectos de ejemplo** de `03_Socket_ejs.zip`, las piezas reutilizables (`NetworkHelper`, `Protocol`, `FileStreamHelper`, `SettingsManager`) y los comentarios donde los docentes marcan su propio codigo como incorrecto.

## Literature - complementarios
- [[Complementario - Docker y Cloud]] - contenedores, Dockerfile multi-stage, volumenes, redes, Compose. **Ojo: el Obligatorio 2 prohibe multi-stage.**
- [[Complementario - Introduccion y Streams]] - sistemas distribuidos, modelos de capas, proceso vs hilo, tabla de streams de C#.
- [[Fundamentos previos]] - nivelacion **no oficial** previa a la primera clase.
- [[Referencia - Web Services del programa anterior]] - nota de estado: sin material oficial, no completar por inferencia.

## Synthesis
- [[Reglas y prohibiciones del curso PDR 2026]] - reglas duras de cada obligatorio, item por item, marcando si salen de **la letra** o de una **clase**.
- [[Mapa del curso Programacion de Redes]] - 6 temas de Aulas vs 8 unidades del PDF historico, y que fuente alimenta cada tema.
- [[Concurrencia de Thread a Task]] - el arco completo: hilo, Task, async/await, Parallel, y la regla CPU-bound vs I/O-bound. Base teorica de la migracion del Obligatorio 2.
- [[Elegir tecnologia de comunicacion]] - matriz de decision Sockets / MOM / gRPC / REST / WebSocket y el esqueleto para responder una pregunta de arquitectura.
- [[Como escribir sockets al estilo del curso PDR]] - las 10 reglas de estilo que se deducen de los ejemplos de la catedra.
- [[Patrones de examen en Programacion de Redes]] - frecuencia historica sobre 16 letras, los 5 formatos que se repiten, prioridad de estudio.
- [[Errores caros en Programacion de Redes]] - 23 errores con el por que y la version correcta, incluidos los que los docentes mostraron en clase.

## Methodology
- [[Plan de estudio y repaso PDR]] - orden de repaso, que usar de cada carpeta, como practicar con las letras historicas, ritmo del semestre.
- [[Estudio para la defensa del Obligatorio 1]] - preparacion de la defensa del 01-10: preguntas probables y explicaciones. Sirve de molde para la defensa conjunta del 2 y el 3.

## Hypotheses
- [[Huecos y contradicciones del material PDR]] - **29 items** en cuatro grupos:
  - temarios incompatibles, duplicados, erratas y huecos de material;
  - contradicciones que aparecieron en las clases;
  - las de las letras de los obligatorios;
  - la **28**, defensa y formato del 2, y la **29**, RabbitMQ nuevo que rechaza colas no durables.
  - La **15**, defensas del 2 y el 3, quedo **resuelta** el 13-10.

## Projects - Obligatorio 1 (cerrado)
Historico del primer obligatorio, en orden de vigencia. El estado final es el del 30-09.
- [[Estado del Obligatorio 1 - 2026-09-30]] - **estado final**: entregado, Release `v1.1` en `main` `0756597`, PDF de 19 paginas con el punto 9, prueba en dos PC. Resultado de la defensa: TBD.
- [[Auditoria de cierre del Obligatorio 1 - 2026-09-27]] - auditoria final con verificacion adversarial: codigo, binarios y prueba real con dos clientes.
- [[Estado del Obligatorio 1 - 2026-09-27]] · [[Estado del Obligatorio 1 - 2026-09-24]] · [[Estado del Obligatorio 1 - 2026-09-22]] · [[Estado del Obligatorio 1 - 2026-09-21]] - estados intermedios, **reemplazados** por el del 30-09.
- [[Decisiones de la capa de protocolo - Obligatorio 1]] - por que el protocolo quedo como quedo. Vigente si el Obligatorio 2 mantiene el protocolo.
- [[Decision - formato y voz de la documentacion del Obligatorio 1]] - formato y voz de DA2, solo lo que pide la letra, 20-30 paginas. **Aplica tambien al Obligatorio 2.**
- [[Decision - sin comentarios en el codigo del Obligatorio 1]] - el codigo va sin comentarios. **Aplica tambien al Obligatorio 2.**
- `Projects/herramientas/espia_visual.py` - herramienta auxiliar de la sesion del 30-09.

## People
- [[Enrique Latorres]] - profesor del teorico 2026: que valora al corregir, estilo de clase, trayectoria.
- [[Natalia Franco]] - docente que acompana el teorico 2026.
- [[Paula Poli]] · [[Santiago Louro]] - companeros de grupo: numero de estudiante, GitHub y aportes segun git (as of 2026-09-27).
- [[Juan Francisco Constantin Bonjour]] · [[Tomas Moreira]] · [[Ignacio Adorjan]] - nombrados en el corpus de estudio heredado. Relacion con el cursado propio: TBD.

## Sources (crudo inmutable, no editar)
Lista completa en [[Catalogo de fuentes]], regenerado el 2026-10-13.
- `Sources/OBLIGATORIO/Obligatorio 2 Agosto 2026.pdf.md` - **la letra vigente del Obligatorio 2**, convertida del PDF el 2026-10-13. Original en `PDR/PDR/OBLIGATORIO/CONSIGNA/`.
- `Sources/OBLIGATORIO/Obligatorio 1 Agosto 2026.pdf.md` - la letra del Obligatorio 1, convertida el 2026-09-10.
- `Sources/TEORICO/` - teoricos por unidad, mas el codigo de ejemplo de la catedra:
  - `SOCKETS_COMUNICACION_EN_RED/EJEMPLOS_03_SOCKET_EJS/`
  - `03_PARALELISMO/EJEMPLOS_04_ASYNC_EJS/` y `03_PARALELISMO/EJEMPLOS_THREADEXAMPLES/`, desde el 13-10
  - `01_INTRODUCCION/EJEMPLOS_STREAM/`, desde el 13-10
  - Los zip se copiaron **sin `bin/` ni `obj/`**.
- `Sources/PARCIALES/` - `PARCIALES-20260731/` y `EXAMENES-20260731/`, con letras y soluciones.
- `Sources/PRACTICOS/Practico 1.md` · `Sources/OBLIGATORIO/HISTORICO_MAR2026/` (3 obligatorios de marzo 2026, solo referencia de formato) · `Sources/Tabla_comparativa_capas_Google_Docs.md`.
- `Sources/ESTUDIO_PREVIO/` - 61 notas heredadas. Ojo: tiene **carpetas duplicadas** por las dos numeraciones de temario (item 2 de [[Huecos y contradicciones del material PDR]]).
- `Sources/CLASES_2026/` - vacia. Las transcripciones crudas no se guardan: la fuente de cada acta es el video de Vimeo.

Los originales (PDF, PPTX, DOCX) viven fuera de la boveda, en la carpeta hermana `PDR/PDR/`.

## Dev Logs, Daily y Logs
- [[2026-09-21 - Obligatorio 1 EDISON]] · [[2026-09-27 - Obligatorio 1 EDISON]] · [[2026-09-28 - Obligatorio 1 EDISON]] - logs de las sesiones de trabajo del Obligatorio 1.
- [[Daily/2026-08-26]] · [[Daily/2026-09-09]] · [[Daily/2026-09-10]] · [[Daily/2026-09-27]] · [[Daily/2026-10-13]] - notas diarias.
- `Logs/YYYY-MM-DD.md` - registro de operaciones por dia, del 2026-08-26 al 2026-10-13. `log.md` es solo un puntero.
- [[Reading Queue/_Queue]] - bandeja de lectura pendiente.

## Estructura
- `Clases/` - un acta por clase, `Clase YYYY-MM-DD - titulo.md`.
- `Boards/Research.md` - tablero unico:
  - columnas Reading, Processing, Synthesized y Done;
  - desde el 13-10, una columna plegada **Obligatorio 1 (cerrado)**.
- `Templates/`, `Bases/`, `Reading Queue/`, `_trash/`.
- `_CLAUDE.md` - reglas de operacion. `CRITICAL_FACTS.md` - hechos vigentes.
