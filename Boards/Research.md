---

kanban-plugin: board

---

## 📚 Reading

- [ ] 🔴 **Correr los ejemplos de async de la catedra, sobre todo el cliente/servidor async y el Docker** · @{2026-10-16}
	Ingeridos el 13-10 en `Sources/TEORICO/03_PARALELISMO/EJEMPLOS_04_ASYNC_EJS/` y `EJEMPLOS_THREADEXAMPLES/`. `04_Async_03_00Client/Server` es la base para migrar EDISON, y `04_Async_04_Docker` la base del Dockerfile. [[Ejemplos de codigo de asincronia de clase]]
- [ ] 🟡 **Pedir por Teams los codigos de RabbitMQ y RPC del 08-10** · @{2026-10-15}
	El docente dijo *"les mandamos igual los codigos hoy"*. No estan en la boveda. [[Clase 2026-10-08 - Letra del Obligatorio 2 y patron RPC sobre RabbitMQ]]
- [ ] 🟡 **Reintentar el acta de la clase del 29-09** · @{2026-10-20}
	El video 1231410633 esta en el showcase pero sin pista de subtitulos (as of 2026-10-13). [[Programacion de Redes 2026]]
- [ ] 🟡 **Hacer el práctico que está en Aulas / Drive** · @{2026-09-15}
	Mencionado el 01-09. Complementa `Sources/PRACTICOS/Practico 1.md`. [[Tema 1 - Programacion multihilo]]
- [ ] 🟡 **Repasar Tema 1 con las tres guías de threads** · @{2026-09-07}
	Semanas 1-3 del temario vigente. [[Tema 1 - Programacion multihilo]]
- [ ] 🟡 **Correr modificados los ejemplos de sockets ya ingeridos**
	`Sources/TEORICO/SOCKETS_COMUNICACION_EN_RED/EJEMPLOS_03_SOCKET_EJS/`, analizado en [[Ejemplos de codigo de sockets de clase]]. Ver el `Receive` parcial en vivo comparando `03_Sockets_02` contra `03_Sockets_03`. Ahora es repaso para el parcial.
- [ ] 🟢 **Leer el resumen StuDocu como complemento**
	`Sources/TEORICO/_PROGRAMA_Y_RESUMENES/0.RESUMEN-STUDOCU.md`, 103 KB. No es fuente oficial.

## 🔬 Processing

- [ ] 🔴 **Obligatorio 2: subir el zip a Gestión antes de las 21:00** · @{2026-10-29}
	PDF nuevo con el link al repo en la primera pagina, dentro de un zip (la letra se contradice en el formato: item 28). Max 40 MB, minimo 5 y maximo 15 puntos. Antes: Release en `main` y despues no tocar `main`. [[Obligatorio 2 - Upgrade asincrono y Docker de EDISON]]
- [ ] 🔴 **Migrar EDISON de `Thread` a `Task` con `async`/`await`** · @{2026-10-18}
	`Thread` prohibido. Los puntos son: el hilo de `Accept` (`Edison.Server/Program.cs:64`), el hilo por cliente (`:78`), los `Join` (`:102`, `:115`) y `ReservationProcessor` (`:16`). APIs `Async` de `TcpListener`/`Socket`. No hacer async lo que no es E/S. Primero dejar el codigo viejo en un branch congelado. [[Obligatorio 2 - Upgrade asincrono y Docker de EDISON]]
- [ ] 🔴 **Requerimiento 5: cancelar reservas pendientes con `CancellationToken`** · @{2026-10-22}
	Una `Task` por reserva pendiente con su `CancellationTokenSource` y un plazo de aprobacion largo. Un comando nuevo del cliente cancela; el servidor guarda que cliente pidio que, deshace y avisa. Token enlazado al del servidor. [[Clase 2026-09-22 - Parallel, CancellationToken y cuando usar async]]
- [ ] 🔴 **Comprobante en archivo para cada reserva aprobada** · @{2026-10-22}
	Accesible desde el host via volumen, y documentar como. Opcional sugerido en clase: que el cliente lo descargue como una imagen. [[Obligatorio 2 - Upgrade asincrono y Docker de EDISON]]
- [ ] 🔴 **Dockerfile de cliente y servidor sin multi-stage** · @{2026-10-24}
	Release para Linux y `COPY`. Configuracion fuera de `app.config`, por ejemplo con variables de entorno. Imagenes de modelos visibles desde el host. `ReadKey` al final de las apps para `docker attach`. [[Ejemplos de codigo de asincronia de clase]]
- [ ] 🔴 **Base de datos en Docker con al menos una entidad** · @{2026-10-24}
	MySQL u Oracle (el docente recomienda Oracle). Todo menos las imagenes. Hoy la persistencia es `StateStore`. [[Obligatorio 2 - Upgrade asincrono y Docker de EDISON]]
- [ ] 🟡 **`docker-compose.yml` con servidor, cliente y base** · @{2026-10-25}
	Opcional en la letra, recomendado en clase: sin Compose, un nombre de contenedor distinto rompe la conexion. [[Clase 2026-10-08 - Letra del Obligatorio 2 y patron RPC sobre RabbitMQ]]
- [ ] 🟡 **Documentacion del Obligatorio 2** · @{2026-10-27}
	Que cambio, por que y que beneficio trae, con comparacion de codigo viejo contra nuevo. Guia de ejecucion con Docker. Cita de herramientas de IA. Base: la documentacion del Obligatorio 1. [[Decision - formato y voz de la documentacion del Obligatorio 1]]
- [ ] 🟡 **Registrar la devolucion de la defensa del Obligatorio 1 y aplicar las mejoras** · @{2026-10-20}
	Es el requerimiento 4 de la letra. La devolucion se anuncio para la clase siguiente al 08-10. [[Obligatorio 2 - Upgrade asincrono y Docker de EDISON]]
- [ ] 🟡 **Levantar RabbitMQ en Docker y probar los 4 tipos de exchange** · @{2026-10-29}
	`docker run -it -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:4-management`. **Declarar las colas `durable: true`**: la version nueva rechaza las no durables (item 29). [[Tema 4 - Colas de mensajes y MOM]]
- [ ] 🔴 **Resolver Práctico 1 (threads)** · @{2026-09-07}
	`Sources/PRACTICOS/Practico 1.md`. Elegir mecanismo según el problema: Join, ManualResetEvent, Semaphore, Monitor. [[Tema 1 - Programacion multihilo]]
- [ ] 🟡 **Implementar un servidor de sockets multi-cliente con framing propio** · @{2026-11-23}
	Largo fijo de 4 bytes + payload, Send/Receive con ciclo por offset, cierre correcto. Es el ejercicio más repetido del parcial. [[Tema 2 - Sockets y protocolos]]
- [ ] 🟡 **Resolver dos letras históricas completas, cronometradas** · @{2026-11-23}
	Sin mirar la solución primero. Método en [[Plan de estudio y repaso PDR]]
- [ ] 🟢 **Escribir un `.proto` propio con unary y streaming** · @{2026-11-16}
	Cuidado: `required` es de proto2. [[Tema 5 - RPC y gRPC]]
- [ ] 🟢 **Confirmar umbral de exoneración**
	El docente dijo que el porcentaje de exoneración es alto, sin dar número. Preguntar por Teams. La defensa del 2 ya esta resuelta: va con la del 3 (item 15). [[Huecos y contradicciones del material PDR]]
- [ ] 🟢 **Santiago verifica su email `slouro03` en GitHub**
	Commitea con `[email]` además de su noreply. Si ese email no está verificado en su cuenta, esos commits no le quedan vinculados y la nota es individual. Sigue valiendo para el Obligatorio 2. [[Santiago Louro]]
- [ ] 🟢 **Recrear `Develop` desde `main`**
	`Develop` no existe en origin (el repo borra ramas al mergear). Necesaria para trabajar el Obligatorio 2 sin tocar `main`. [[Auditoria de cierre del Obligatorio 1 - 2026-09-27]]

## 🗄️ Obligatorio 1 (cerrado)

- [ ] **Tarjetas historicas del Obligatorio 1**
	Entregado el 28-09, Release `v1.1` en `main` `0756597`, defensa el 01-10 o el 06-10. Estas tarjetas quedaron sin tildar en el tablero y no se verifico una por una si se completaron. El estado real esta en [[Estado del Obligatorio 1 - 2026-09-30]]. Se archivan aca el 2026-10-13 para despejar Processing.
- [ ] 🔴 **Implementar el servidor como maquina de estados espejo de los menus** · @{2026-09-24}
	Clase 10-09. [[Clase 2026-09-10 - Maquina de estados del protocolo, Common y app.config]]
- [ ] 🔴 **Mover IP y puerto a `app.config` con `ConfigurationManager` en `Common`** · @{2026-09-24}
	Clase 10-09. En el Obligatorio 2 esto se reemplaza por configuracion apta para contenedores. [[Clase 2026-09-10 - Maquina de estados del protocolo, Common y app.config]]
- [ ] 🔴 **Arreglar los dos bugs plantados del ejemplo de transferencia de archivos** · @{2026-09-25}
	Clase 15-09. [[Errores caros en Programacion de Redes]]
- [ ] 🔴 **Nombre propio por modelo para las imagenes, y borrarlas al borrar el modelo** · @{2026-09-25}
	Clase 15-09. [[Clase 2026-09-15 - Transferencia de archivos por chunks e introduccion a asincronia]]
- [ ] 🟡 **Probar que no se pierden bytes con una secuencia mixta** · @{2026-09-26}
	Clase 10-09. [[Como escribir sockets al estilo del curso PDR]]
- [ ] 🔴 **Subir el zip a Gestión antes de las 21:00** · @{2026-09-28}
	[[Auditoria de cierre del Obligatorio 1 - 2026-09-27]]
- [ ] 🔴 **Inscribirse en la evaluación por Gestión y armar el equipo ahí** · @{2026-09-12}
	[[Obligatorio 1 - Sistema de reservas de vehiculos electricos]]
- [ ] 🔴 **Armar el grupo (hasta 3, del mismo dictado)** · @{2026-09-12}
	Resuelto segun [[Programacion de Redes 2026]] (2026-09-27).
- [ ] 🔴 **Pedir acceso a la organización de GitHub `ArqSis-PDR-2026-2` y crear el repo privado** · @{2026-09-12}
	Resuelto segun [[Programacion de Redes 2026]] (2026-09-27).
- [ ] 🟡 **Confirmar por Teams el formato del archivo de entrega** · @{2026-09-19}
	Mitigacion aplicada: zip con el PDF. [[Huecos y contradicciones del material PDR]]
- [ ] 🔴 **Esqueleto del cliente: menú de consola completo** · @{2026-09-14}
	[[Clase 2026-09-01 - Exclusion mutua en CSharp y arranque de sockets]]
- [ ] 🔴 **Codificar el protocolo propietario del servidor** · @{2026-09-18}
	[[Clase 2026-09-08 - Protocolo propietario, framing y Receive parcial]]
- [ ] 🟡 **Lógica y acceso a datos del servidor en memoria, con concurrencia** · @{2026-09-21}
	[[Reglas y prohibiciones del curso PDR 2026]]
- [ ] 🟡 **Cierre controlado del servidor desde la consola** · @{2026-09-23}
	En el Obligatorio 2 se puede ignorar. [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]]
- [ ] 🟡 **Configuración externa de IP y puerto, sin recompilar** · @{2026-09-23}
	[[Clase 2026-09-08 - Protocolo propietario, framing y Receive parcial]]
- [ ] 🔴 **Corregir la documentación (fila 99, 8.2 paso 3, reserva sin unidades, errores conocidos, LEEME.txt)** · @{2026-09-28}
	[[Auditoria de cierre del Obligatorio 1 - 2026-09-27]]
- [ ] 🔴 **Redactar el punto 9 (uso de IA) en la documentación y regenerar el PDF y el zip** · @{2026-09-28}
	Segun [[Estado del Obligatorio 1 - 2026-09-30]], el PDF de 19 paginas tiene el punto 9 completo.
- [ ] 🟡 **Declarar en supuestos qué significa "compras realizadas" en CR10** · @{2026-09-26}
	[[Huecos y contradicciones del material PDR]]
- [ ] 🟡 **Ensayar el Release en dos PC cableadas del laboratorio, con `0.0.0.0` en el server** · @{2026-09-29}
	[[Auditoria de cierre del Obligatorio 1 - 2026-09-27]]
- [ ] 🟡 **Preparar la defensa: poder explicar cada línea** · @{2026-09-28}
	[[Estudio para la defensa del Obligatorio 1]]
- [ ] 🔴 **Confirmar que los tres integrantes figuran Inscriptos en Gestión** · @{2026-09-28}
	[[Auditoria de cierre del Obligatorio 1 - 2026-09-27]]
- [ ] 🔴 **Crear GitHub Release sobre `main` con el PDF final** · @{2026-09-28}
	Hecho segun [[Estado del Obligatorio 1 - 2026-09-30]]: `v1.1` sobre `0756597`.
- [ ] 🟡 **Anotarse en la planilla de defensa de Teams**
	[[Auditoria de cierre del Obligatorio 1 - 2026-09-27]]
- [ ] 🟡 **Preguntar en clase qué orden de bytes esperan en la corrección**
	[[Huecos y contradicciones del material PDR]]
- [ ] 🟡 **Decidir si se aplica el parche candidato de defensa**
	[[Auditoria de cierre del Obligatorio 1 - 2026-09-27]]

## 🧬 Synthesized

- [x] ~~**Ingerir la letra del Obligatorio 2 y las clases del 22-09 y 08-10**~~ ✅ 2026-10-13
	Letra en `Sources/OBLIGATORIO/Obligatorio 2 Agosto 2026.pdf.md`. Contradicciones nuevas en los items 28 y 29. [[Obligatorio 2 - Upgrade asincrono y Docker de EDISON]]
- [x] ~~**Ingerir los ejemplos de async, ThreadExamples y streams de Teams**~~ ✅ 2026-10-13
	[[Ejemplos de codigo de asincronia de clase]]
- [x] ~~**Ingerir el PDF oficial de la letra del Obligatorio 1 y reconciliarlo con las clases**~~ ✅ 2026-09-10
	La consigna dejó de ser reconstrucción de transcripciones. Apareció la organización de GitHub obligatoria, la defensa eliminatoria y 4 ítems nuevos de contradicción. [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]]
- [x] ~~**Ingestar las seis grabaciones de clase del showcase de Vimeo**~~ ✅ 2026-09-09
	18/08 al 08/09. Seis actas en `Clases/`. [[Programacion de Redes 2026]]
- [x] ~~**Desglosar la letra del Obligatorio 1 y consolidar las reglas del curso**~~ ✅ 2026-09-09
	[[Obligatorio 1 - Sistema de reservas de vehiculos electricos]] · [[Reglas y prohibiciones del curso PDR 2026]]
- [x] ~~**Mapa del curso y reconciliación de los dos temarios**~~ ✅ 2026-08-26
	[[Mapa del curso Programacion de Redes]]
- [x] ~~**Matriz de decisión de tecnología de comunicación**~~ ✅ 2026-08-26
	[[Elegir tecnologia de comunicacion]]
- [x] ~~**Arco de concurrencia: de Thread a Task**~~ ✅ 2026-08-26
	[[Concurrencia de Thread a Task]]
- [x] ~~**Patrones de examen y frecuencia histórica**~~ ✅ 2026-08-26
	[[Patrones de examen en Programacion de Redes]]
- [x] ~~**Catálogo de errores caros**~~ ✅ 2026-08-26
	[[Errores caros en Programacion de Redes]]

## ✅ Done

- [x] ~~**Ingestar la clase del 22-09**~~ ✅ 2026-10-13
	[[Clase 2026-09-22 - Parallel, CancellationToken y cuando usar async]]
- [x] ~~**Bajar el zip de threads de Teams**~~ ✅ 2026-10-13
	`ThreadExamples.zip` y `04_Async_ejs.zip` ingeridos en `Sources/TEORICO/03_PARALELISMO/`.
- [x] ~~**Revisar que no haya quedado `Task` en el codigo del Obligatorio 1**~~ ✅ 2026-09-27
	Verificado en la auditoría del 27-09: el código usa `Thread` explícito, sin `Task`. [[Auditoria de cierre del Obligatorio 1 - 2026-09-27]]
- [x] ~~**Implementar el loop de `Receive` hasta completar el mensaje**~~ ✅ 2026-09-27
	Verificado en la auditoría del 27-09: `NetworkHelper.Receive` loopea hasta completar el mensaje. [[Auditoria de cierre del Obligatorio 1 - 2026-09-27]]
- [x] ~~**Convertir 84 fuentes de PDF/PPTX/DOCX a markdown**~~ ✅ 2026-08-26
	markitdown, 84 de 84 sin fallos
- [x] ~~**Incorporar el corpus de estudio heredado como fuente**~~ ✅ 2026-08-26
	61 notas en `Sources/ESTUDIO_PREVIO/`
- [x] ~~**Literature note por tema vigente y complementario**~~ ✅ 2026-08-26
	10 notas
- [x] ~~**Registrar huecos y contradicciones del material**~~ ✅ 2026-08-26
	[[Huecos y contradicciones del material PDR]]

%% kanban:settings
```
{"kanban-plugin":"board","list-collapse":[false,false,true,false,false]}
```
%%
