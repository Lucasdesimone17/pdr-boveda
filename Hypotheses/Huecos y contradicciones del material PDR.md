---
date: 2026-08-26
updated: 2026-10-13
type: hypothesis
tags:
  - hypothesis
  - pdr
  - huecos
  - contradicciones
ai-first: true
status: open
confidence: high
---

# Huecos y contradicciones del material PDR

## For future agent
Registro de contradicciones internas, huecos y duplicaciones del material de Programación de Redes. Cada ítem dice qué se contradice y con qué evidencia. Pull esto antes de asumir que algo "no se da" o antes de confiar en un número de tema. Se cierra cada ítem cuando aparece material o pauta que lo resuelva.

**25 ítems as of 2026-09-15.** Los 1-14 salieron de la ingesta de material del 26/08, los 15-20 de las seis grabaciones de clase (9/09), los **21-24 del PDF de la letra del Obligatorio 1** (10/09), y el **25 de los ejemplos de codigo de la catedra** (15/09). Cerrados o resueltos: 11 (parcial), 16 (as of 2026-09-27), 17, 20, 24.

**29 items as of 2026-10-13.** Los 26-27 salieron de las clases del 10 y del 15-09. El **28** salio de la letra del Obligatorio 2 y el **29** de la clase del 08-10. El **15 quedo resuelto** para el Obligatorio 2.

## Contradicciones

### 1. Dos temarios con numeración incompatible (crítico)
El temario vigente de Aulas tiene **6 temas**; el PDF de programa tiene **8 unidades**. La numeración choca: el "Tema 2" es Sockets en el mapa vigente y Threads y Concurrencia en el histórico. Cualquier referencia a "Tema N" es ambigua sin decir qué mapa.
- Evidencia: `Sources/ESTUDIO_PREVIO/MAIN.md` (copia de Aulas) vs `Sources/TEORICO/_PROGRAMA_Y_RESUMENES/Programación de Redes.md`.
- Resuelto operativamente en [[Mapa del curso Programacion de Redes]]: manda Aulas. (status: documentado, no cerrado)

### 2. El corpus previo tiene carpetas duplicadas por tema
`Sources/ESTUDIO_PREVIO/` contiene **dos juegos de carpetas** para los mismos contenidos, con nombres de las dos numeraciones: `TEMA_01_INTRODUCCION` y `TEMA_01_PROGRAMACION_MULTIHILO`, `TEMA_02_SOCKETS` y `TEMA_02_THREADS_Y_CONCURRENCIA`, `TEMA_03_ASINCRONIA_Y_PARALELISMO` y `TEMA_03_PARALELISMO`, `TEMA_04_COLAS_DE_MENSAJES` y `TEMA_04_MESSAGE_ORIENTED_MIDDLEWARES`, `TEMA_05_RPC` y `TEMA_05_RPC_Y_ORBS`, `TEMA_06_APIS_REST`, `TEMA_06_WEB_SERVICES` y `TEMA_07_REST`.
- Riesgo: leer el archivo equivocado y estudiar el mapa viejo.
- Regla de uso: las notas `Literature/` de esta bóveda apuntan al juego vigente. (status: open)

### 3. Fuentes duplicadas bit a bit en TEORICO
`1-Introduccion-2026.md` y `1-Introducción (2026).md` provienen de dos PDF de idéntico tamaño (1584 KB) que difieren solo en la tilde del nombre. Igual pasa con `20250423-Examen...` y `20250423-Examen... (1)`, y con `20250423-Solucion...` y su `(1)`.
- Efecto: infla el conteo de fuentes y duplica resultados de búsqueda. (status: open)

### 4. La lista de desventajas de MOM incluye una ventaja
El teórico de MOM lista "débil acoplamiento" como **desventaja**. Es una ventaja, y así se usa en el resto del curso. Erratum del material; no repetirlo en un parcial. (status: documentado)

### 5. El ejemplo `.proto` mezcla proto2 y proto3
Declara `syntax = "proto3"` y después usa `required` y `optional`, que son de proto2 (`required` fue eliminado en proto3). (status: documentado)

## Huecos de material

### 6. Web Services / SOAP / WSDL: sin material (bloqueado)
`TEORICO/06_WEB_SERVICES/` está vacía. El programa histórico pide la unidad; no hay PDF, guía ni diapositiva. Un resumen previo llegó a tener contenido de Sockets reetiquetado como Web Services, y se retiró justamente por engañoso. Detalle en [[Referencia - Web Services del programa anterior]]. (status: open, depende de la docente)

### 7. Arquitecturas Cloud: solo hay Docker
La unidad 8 del programa pide "Definición y conceptos principales. Sistemas operativos Cloud. Implementación de arquitecturas Cloud". El material local es Docker práctico: contenedores, Dockerfile, Compose, redes, volúmenes. IaaS/PaaS/SaaS, modelos de despliegue y decisiones de arquitectura cloud no tienen cobertura. (status: open)

### 8. Seguridad en REST sin desarrollo
El programa histórico incluye "Seguridad" dentro de REST. El material práctico solo menciona TLS y el header `Authorization`; no hay OAuth, JWT ni esquemas de autorización. (status: open)

### 9. HATEOAS enunciado y nunca ejemplificado
Se lista como restricción obligatoria de REST y no aparece en ningún ejemplo de código del material. (status: open)

### 10. CPU-bound vs I/O-bound sin formulación explícita
El parcial lo pregunta, y el material lo deja implícito: no hay una frase que cierre la regla. La formulación que usa esta bóveda está en [[Concurrencia de Thread a Task]], marcada como inferida (confidence: medium). (status: open)

## Cuestiones de vigencia y contexto

### 11. Obligatorios históricos vs ciclo actual
`Sources/OBLIGATORIO/HISTORICO_MAR2026/` trae los tres obligatorios de **marzo 2026** (Obligatorio 1 con entrega 23-04-2026, máximo 25 puntos y mínimo 5, defensa obligatoria y eliminatoria). Son del ciclo anterior: sirven de referencia de formato, **no** de consigna vigente. (status: **resuelto para el Obligatorio 1 el 2026-09-10**)

**Actualización as of 2026-09-10.** La letra vigente del Obligatorio 1 ya está en la bóveda: `Sources/OBLIGATORIO/Obligatorio 1 Agosto 2026.pdf.md`. Confirma que el formato del ciclo anterior se mantuvo casi entero (5/25 puntos, gestión hasta las 21:00, defensa obligatoria y eliminatoria, pautas de IA) y agrega la organización de GitHub `ArqSis-PDR-2026-2`. **Para los Obligatorios 2 y 3 el ítem sigue abierto**: sus letras se publican el 28/09 y el 29/10 y hasta entonces `HISTORICO_MAR2026/` es solo referencia de formato.

### 12. El calendario del semestre no tiene año en el original
`MAIN.md` anota "26 agosto", "28 septiembre", "29 octubre", "23 noviembre", "30 noviembre" sin año. Se interpretan como 2026 porque el semestre arranca el 18/08/2026. Interpretación, no dato. (confidence: high, no stated)

### 13. Procedencia del corpus previo
El material de `ESTUDIO_PREVIO/` fue producido por otra sesión de trabajo con Aulas abierta bajo la cuenta de **Juan Francisco Constantin Bonjour**, y su `ROADMAP.md` nombra un equipo de estudio de tres personas ([[Juan Francisco Constantin Bonjour]], [[Tomas Moreira]], [[Ignacio Adorjan]]). La relación entre ese equipo y el dueño de esta bóveda no consta en ninguna fuente: no asumirla. (status: open)

### 14. Pautas de aprobación sin confirmar
Puntajes conocidos (25/15/25/35), pero el umbral total de aprobación y de exoneración, la modalidad de defensas y los períodos de inscripción no constan. (status: **parcialmente resuelto el 2026-09-09**)

**Actualización as of 2026-09-09**, de las grabaciones de clase: la modalidad de defensa del Obligatorio 1 sí consta (presencial, laboratorio, Windows 10/11, plan de test, nota individual) y la inscripción se hace por planilla en Teams. El **umbral de exoneración sigue sin confirmar**: el docente dijo el 18-08 que "en general el porcentaje de exoneración es alto" y un estudiante preguntó si se exonera con 86, sin respuesta explícita. Detalle en [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]].

## Ítems abiertos por las clases del semestre en curso

### 15. Contradicción sobre las defensas de los Obligatorios 2 y 3
El **27-08-2026** los docentes dijeron que el Obligatorio 1 tiene defensa y que **el segundo no la tiene** (tiene devolución, no defensa). El **08-09-2026**, otro docente dijo que **el segundo y el tercero también tienen defensa**. Las dos afirmaciones están en las grabaciones. No resuelto: preguntar por Teams antes de planificar. (status: open, confidence: stated en ambos casos)

**Resuelto para el Obligatorio 2 el 2026-10-13.** La letra oficial dice *"La defensa se evaluará junto con la tercera entrega"*, y el docente lo confirmo en clase el 08-10: *"No va a haber una defensa para este obligatorio"*. Tenia razon la version del 27-08 para el 2, y la del 08-09 en que el 3 tiene defensa, que ademas cubre al 2. Fuente: `Sources/OBLIGATORIO/Obligatorio 2 Agosto 2026.pdf.md` y [[Clase 2026-10-08 - Letra del Obligatorio 2 y patron RPC sobre RabbitMQ]]. Ver tambien el item 28. (status: resuelto el 2026-10-13)

### 16. Defensa escrita vs defensa presencial del Obligatorio 1
La letra dice que la defensa es **presencial**, en laboratorio, ejecutando la aplicación en varias máquinas, con plan de test. En la misma clase del 27-08 el docente dijo además: "yo estaba planteando que incluso la defensa sería escrita. Les pasamos unas preguntas y ustedes tienen que responder ahí". No queda claro si es una alternativa, un complemento o una idea descartada. (status: **resuelto el 2026-09-27**)

**Actualización as of 2026-09-10**, con el PDF de la letra a la vista. El texto formal dice dos cosas que acotan el ítem: **"La defensa será presencial salvo en casos en que se habilite la defensa oral sincrónica en forma remota"** y **"El docente es quien definirá y comunicará la modalidad, y mecánica de defensa"**. O sea: la única variante que la letra contempla es **remota pero oral y sincrónica**, no escrita; y la potestad de fijar la modalidad es del docente. La defensa escrita no está prohibida por la letra, pero tampoco prevista: si aparece, sale del docente. Sigue abierto hasta que lo confirmen por Teams.

**Actualización as of 2026-09-27 (fuente: clase).** El acta de la clase del 24-09 confirma la modalidad operativa, sin mencionar la variante escrita: defensa presencial en el laboratorio, el jueves 2026-10-01 (puede continuar el martes 2026-10-06), con todo el grupo presente, anotándose en la planilla de Teams para elegir horario. La wifi del laboratorio tiene microsegmentación (dos máquinas conectadas por wifi no se pueden comunicar entre sí) y puede hacer falta desactivar el firewall. Se prueba con el Release de GitHub bajado en el momento, contra una planilla de testing con dos clientes donde nada se debe cerrar ni explotar, más preguntas de código. Ítem cerrado. Ver [[Clase 2026-09-24 - Middleware orientado a mensajes, RabbitMQ y AMQP]].

### 17. La letra del Obligatorio 1 se contradice sobre eliminar modelos
La letra dice que **el catálogo de modelos es permanente** y por otro lado tiene un requerimiento de **baja de modelo**. Los estudiantes lo preguntaron y el docente lo resolvió el **08-09-2026**: **sí se puede eliminar**, con la restricción de que no haya reservas activas asociadas. (status: **resuelto**)

### 18. Nombre del docente de la clase del 08-09-2026
Esa clase la dictó un docente distinto a [[Enrique Latorres]] y [[Natalia Franco]], con enfoque práctico. En la transcripción automática se oye "Iván", pero el pasaje está degradado y en la misma bóveda aparece un "Iván Mojardín" mencionado como docente de **Infraestructura en la Nube 1**, que puede o no ser la misma persona. **No atribuir por nombre** hasta confirmarlo. (status: open, confidence: speculation)

### 19. Las presentaciones de Aulas están desactualizadas
El docente dijo el 18-08 que el calendario que estaba publicado era **del semestre pasado** ("esto no está actualizado porque es del semestre pasado, pero más o menos las fechas son parecidas") y que hay docentes listados que ya no corrigen. Consecuencia práctica: **las fechas mandan desde la letra publicada y desde Teams**, no desde las diapositivas. (status: open)

### 20. `Thread.Sleep` prohibido en producción pero exigido en el obligatorio final
El docente dijo el 01-09 que `Thread.Sleep` "en un código real, en producción, no deberíamos usarlo nunca", y en la misma frase que **en el obligatorio final va a pedir explícitamente** simular operaciones costosas con `Thread.Sleep`. No es una contradicción real, es una excepción declarada; queda anotada para que no se lea como inconsistencia. (status: cerrado, aclarado)

## Ítems abiertos por la letra del Obligatorio 1 (PDF, ingerido el 2026-09-10)

### 21. La letra se contradice a sí misma sobre el formato del archivo de entrega
Tres secciones del mismo PDF piden cosas distintas:

| Sección | Texto |
|---|---|
| Carátula (`CONDICIONES`) | "max. 40Mb en formato **zip, rar o pdf**" |
| `ENTREGA`, página 6 | "Se debe entregar por Gestión **un archivo PDF** que contenga la documentación" |
| `RECORDATORIO`, página 9 | "Cada equipo debe entregar **un único archivo en formato zip o rar** (los documentos de texto deben ser pdf, y deben ir dentro del zip o rar)" |

El `RECORDATORIO` es texto genérico de la Facultad (aparece igual en otras materias); `ENTREGA` es la sección específica de esta letra. **Mitigación: subir un zip que contenga el PDF**, que cumple las tres redacciones a la vez. Preguntar igual por Teams. Fuente: `Sources/OBLIGATORIO/Obligatorio 1 Agosto 2026.pdf.md`. (status: open, confidence: stated)

### 22. Grupos "del mismo dictado" (letra) vs "se pueden mezclar turnos" (clase 18-08)
La letra dice, en la sección IMPORTANTE: **"Formar grupos de hasta 3 personas del mismo dictado"**. En la clase del **18-08** el docente dijo que **se pueden mezclar turnos y grupos docentes**, con el único requisito de ir todos juntos a la defensa con el grupo docente elegido. Las dos afirmaciones son de fuentes fuertes y se contradicen. El PDF es la fuente formal y es posterior, así que la regla operativa es **mismo dictado**; pero la clase habilitó lo contrario y puede haber grupos ya armados así. **Preguntar por Teams antes de cerrar el grupo.** (status: open, confidence: stated en ambos casos)

### 23. CR10 pide historial de "compras realizadas" pero no hay requerimiento de compra
El requerimiento **CR10** de la letra lista, entre las actividades a historiar, **"reservas hechas y compras realizadas"**. Ninguno de los requerimientos CR1-CR9 define una operación de compra: el flujo del sistema termina en **reserva confirmada** tras el proceso de aprobación. Dos lecturas posibles: es un residuo de otra versión de la letra, o "compra" se usa como sinónimo de "reserva confirmada". **Hay que declararlo como supuesto** en el punto 3 de la documentación (supuestos de la letra), y conviene preguntar por Teams. (status: open, confidence: stated)

### 24. La bóveda había asignado mal la estructura de requerimientos del servidor
No es una contradicción del material sino un error propio, corregido el 2026-09-10 y anotado para no repetirlo. La versión de [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]] reconstruida de las grabaciones listaba **ocho requerimientos de servidor**. El PDF tiene **cuatro** (SR1-SR4); `Configuración`, `Desconexiones` y `Manejo de errores` son **requerimientos en común** de cliente y servidor, y `Datos de prueba` y `Compilados` son secciones aparte. Lección: la transcripción de una lectura en voz alta **aplana la jerarquía de secciones** del documento leído. (status: cerrado)

## Sobre la conversión a markdown
Convertido con markitdown el 2026-08-26: **91 archivos de 91, sin fallos** (84 de PDR). Se perdieron las imágenes y diagramas de los PDF y PPTX, y en algunos PDF viejos apareció el aviso `Could not get FontBBox`, sin pérdida de texto detectada. Para diagramas, la fuente sigue siendo el original en `PDR/PDR/TEORICO/`.

## Connections
- [[Mapa del curso Programacion de Redes]] · [[Programacion de Redes 2026]] · [[Referencia - Web Services del programa anterior]] · [[Complementario - Docker y Cloud]] · [[Concurrencia de Thread a Task]]
- Ítems 21-24: [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]] · [[Reglas y prohibiciones del curso PDR 2026]] · fuente cruda en `Sources/OBLIGATORIO/Obligatorio 1 Agosto 2026.pdf.md`

### 25. Los ejemplos de codigo se contradicen en el orden de bytes
`03_Sockets_03` (cliente y servidor) usa `IPAddress.HostToNetworkOrder` y `NetworkToHostOrder` para los enteros de la cabecera, o sea **big-endian / orden de red**. Los otros seis ejemplos usan `BitConverter.GetBytes` y `BitConverter.ToInt32` directo, que respeta el endianness de la maquina (little-endian en x86).

Ambos funcionan mientras cliente y servidor corran en la misma arquitectura, que es el caso del laboratorio. Pero **no son intercambiables**, y el material no dice cual es el esperado ni menciona la diferencia.

Evidencia: `Sources/TEORICO/SOCKETS_COMUNICACION_EN_RED/EJEMPLOS_03_SOCKET_EJS/03_Sockets_03_Client/03_Sockets_03_Client.cs` contra `.../03_Sockets_TCP_01_Client/03_Sockets_TCP_01_Client.cs`.

**Estado:** abierto. **Mitigacion:** elegir uno, aplicarlo en `Common` para los dos lados, y **documentarlo como decision de diseno del protocolo**, que es lo que la letra pide igual. Preguntar en clase cual esperan en la correccion. Detalle en [[Ejemplos de codigo de sockets de clase]].

### 26. En clase se dijo que la red usa little endian
El 10-09, al explicar `IPAddress.HostToNetworkOrder`, el docente dijo que **la red y TCP/IP codifican en little endian** y que `.NET` lleva la representacion nativa a little endian. La convencion real de TCP/IP es la inversa: **network byte order es big endian**, y `HostToNetworkOrder` en x86 justamente **invierte** de little a big. En la misma explicacion el docente tambien dijo "normalmente en la red, TCP/IP y la gran mayor parte de los protocolos lo mandan en protocolo big endian", asi que **se contradijo dentro de la misma clase** y la transcripcion automatica no permite saber que quedo en el pizarron.

Para el codigo **no cambia nada**: la regla operativa sigue siendo **usar siempre `HostToNetworkOrder` / `NetworkToHostOrder` de los dos lados y no asumir el orden nativo**, que es la mitigacion que ya estaba en el item 25. Fuente: [[Clase 2026-09-10 - Maquina de estados del protocolo, Common y app.config]] ([41:00]-[43:00]). (status: cerrado a efectos practicos, anotado para no citar la afirmacion)

### 27. El calculo del maximo de un long dio "ocho zetta bytes"
El 15-09, al justificar por que el tamano del archivo va en `long` y no en `int`, el docente hizo la cuenta en el pizarron y concluyo **"ocho zetta bytes"**. Con 63 bits de valor el maximo es 2^63 bytes, del orden de **8 exabytes** (dos ordenes de magnitud menos que un zettabyte). El argumento que importa es correcto y no se ve afectado: **`int` con signo tope ~2 GB, insuficiente para archivos reales; `long` alcanza de sobra**. Fuente: [[Clase 2026-09-15 - Transferencia de archivos por chunks e introduccion a asincronia]] ([06:00]-[07:00]). (status: cerrado, anotado para no repetir la cifra)

### 28. La letra del Obligatorio 2 se contradice en defensa y en formato de entrega
- **Defensa.** La cabecera repite el texto estandar *"La defensa es obligatoria y eliminatoria"*. La seccion DEFENSA dice *"se evaluará junto con la tercera entrega"*. "Informacion importante" dice *"Defensas: a coordinar"*. El docente lo dijo en clase el 08-10: no hay defensa propia del 2. **Lectura vigente:** no hay defensa en octubre, y lo del 2 se defiende en la defensa del 3, que es eliminatoria.
- **Formato del archivo.** Las condiciones dicen *"zip, rar o pdf"*. ENTREGA pide *"un archivo PDF"*. El RECORDATORIO exige *"un único archivo en formato zip o rar"* con los PDF adentro. Es la misma contradiccion que el item 21; la mitigacion es **zip con el PDF adentro**.

Fuente: `Sources/OBLIGATORIO/Obligatorio 2 Agosto 2026.pdf.md` y [[Obligatorio 2 - Upgrade asincrono y Docker de EDISON]]. (status: open en formato, resuelto en defensa)

### 29. La version nueva de RabbitMQ rompe los ejemplos del curso
El material y los ejemplos declaran colas **no durables**, no exclusivas y sin autodelete. La ultima imagen de RabbitMQ en Docker Hub **ya no acepta esa combinacion**: esta deprecada y lanza excepcion. Por eso fallaron los ejemplos en clase el 24-09. El docente los arreglo poniendo **`durable: true`**. El comando del curso (`rabbitmq:4-management`, ver [[Tema 4 - Colas de mensajes y MOM]]) baja siempre la ultima version 4.x, asi que cualquier ejemplo viejo puede fallar igual. Mitigacion: declarar las colas durables. Fuente: [[Clase 2026-10-08 - Letra del Obligatorio 2 y patron RPC sobre RabbitMQ]]. (status: cerrado, anotado)

### 30. Los numeros de campo del `.proto`, presentados como "solo un orden"
En la clase del 15-10 la docente dijo que los numeros 1, 2 y 3 del ejemplo `Persona` *"lo unico que estan marcando es un orden, nada mas"*. En Protobuf esos numeros son **identificadores de campo**: viajan en el binario y no se pueden cambiar sin romper la compatibilidad. Es conocimiento general, no material del curso (confidence: high). En un parcial conviene decir que **identifican el campo**. Fuente: [[Clase 2026-10-15 - RPC, gRPC y Protocol Buffers]]. (status: abierto)

### 31. "REST usa HTTP/3" en la comparacion gRPC vs REST
En la clase del 15-10 la transcripcion dice *"gRPC usa HTTP/2 y REST usa HTTP/3"*. La tabla del material compara contra **HTTP/1.1** (ver la Critique de [[Tema 5 - RPC y gRPC]]). Probable error de lectura o de transcripcion. Para un parcial, usar la version del material. Fuente: [[Clase 2026-10-15 - RPC, gRPC y Protocol Buffers]]. (status: abierto)
