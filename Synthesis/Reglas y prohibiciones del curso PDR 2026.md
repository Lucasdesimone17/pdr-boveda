---
date: 2026-09-09
updated: 2026-10-13
type: synthesis
tags:
  - pdr
  - evaluacion
  - reglas
ai-first: true
confidence: stated
fuente: "Letra oficial en PDF: `Sources/OBLIGATORIO/Obligatorio 1 Agosto 2026.pdf.md`. Mas las clases del 18-08, 20-08, 27-08, 01-09, 03-09 y 08-09-2026 (grabaciones Vimeo, showcase 12367503)"
---

# Reglas y prohibiciones del curso PDR 2026

## For future agent
Lista consolidada de **todo lo que es obligatorio, prohibido o eliminatorio** en Programacion de Redes 2026. Es la nota que hay que leer antes de escribir codigo o de entregar. Cada item dice de donde sale. Vigencia: **as of 2026-09-10**.

**Actualizado el 2026-09-10 con el PDF de la letra del Obligatorio 1.** Ahora las reglas llevan procedencia explicita: **(letra)** si esta textual en el PDF - manda siempre - y **(clase DD-MM)** si es una aclaracion oral. Lo mas caro que agrego el PDF: **el repositorio va en una organizacion de GitHub especifica y privado**, y **la defensa es eliminatoria**. Desglose completo en [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]].

## Reglas del Obligatorio 2 (vigentes as of 2026-10-13)

**La regla de `Thread` se invierte.** En el Obligatorio 1 `Task` estaba prohibido; en el 2 **la prohibida es la clase `Thread`**. Desglose en [[Obligatorio 2 - Upgrade asincrono y Docker de EDISON]].

| Regla | Detalle | Fuente |
|---|---|---|
| **Sin la clase `Thread`** | Todo pasa a `Task` con `async`/`await` | letra |
| **Sin `BinaryFormatter`** | Usar streams | letra |
| **Sin parsers de serializacion existentes** | *"como puede ser Json"* | letra |
| **Docker sin multi-stage** | Release para Linux y `COPY` a la imagen; el multi-stage queda para el Obligatorio 3 | letra + clase 08-10 |
| **Configuracion apta para contenedores** | Reemplazar `app.config` por algo mas adecuado | letra |
| **Imagenes y comprobantes visibles desde el host** | Documentar como | letra |
| **Base de datos en Docker** | Todo menos las imagenes; MySQL o la que se prefiera; en clase recomendo Oracle | letra + clase 08-10 |
| **Cancelacion con `CancellationToken`** | Reservas pendientes, con aviso al cliente | letra + clase 08-10 |
| **Mismo repo, Release, `main` congelada** | Link al repo en la primera pagina del PDF | letra |
| **Defensa** | Junto con la tercera entrega | letra + clase 08-10 |
| `ReadKey` al final de las apps | Para ver el mensaje en un contenedor con `docker attach` | clase 08-10 |
| No hacer todo `async` | Solo E/S y tareas pesadas | clase 22-09 |

## Prohibiciones duras del Obligatorio 1

| Regla | Detalle | Fuente |
|---|---|---|
| **Usar `Thread`, no `Task`** | "Deberan utilizar explicitamente la clase `Thread` y no `Task`". El docente remarco "importante". `Task` y asincronia recien entran en el Obligatorio 2 | letra + 27-08 |
| **Prohibido `binary formatter`** | No se puede usar para el pasaje de informacion. **Se espera que se utilicen streams** | letra |
| **Prohibido usar parsers existentes** | "En esta entrega NO se permitira que se utilicen parsers existentes (como puede ser JSON) para serializar objetos". El protocolo se implementa a mano | letra |
| **Entrega por gestion, si o si** | "Si no se realiza la entrega por Gestion, el puntaje final sera 0", aunque el codigo este en GitHub | letra |
| **Solo cuentan gestion y el repo de la organizacion** | "Solo se aceptara lo entregado en gestion y el codigo en el repositorio de la organizacion de GitHub" | letra |
| **`main` congelada despues del hito** | La rama `main`/`master` no debe ser afectada luego del hito de entrega. Los docentes bajan el repo en esa ventana | letra + 27-08 |
| **Compilados en el repo** | Las aplicaciones **compiladas en modo Release**, con todo lo necesario para ejecutarlas y **ejecutables para Windows** (`.exe`, `.dll`). Requiere editar el `.gitignore` | letra + 27-08 |
| **La defensa es obligatoria y eliminatoria** | **La no presentacion implica la perdida de la totalidad de los puntos del Obligatorio** | letra |

### Reafirmado el 17-09-2026, con advertencia sobre la IA
`Task` sigue prohibido en el Obligatorio 1: "lo que empezamos a dar la clase pasada de task, eso no va para el primero". El docente agrego el riesgo concreto: **si se le pide el codigo a una IA sin especificar la restriccion, devuelve `Task`**, y hay que sacarlo y volver a `Thread` explicito. Dijo que **es estricto** con eso. Fuente: [[Clase 2026-09-17 - Task en detalle, continuations, ThreadPool y TPL]].

### `Socket`, no `TcpListener`/`TcpClient` (clase 10-09)
`TcpListener` y `TcpClient` son de mas alto nivel y especificos de TCP/IP. **En el Obligatorio 1 hay que usar `Socket`**; en el Obligatorio 2 si se van a usar, junto con asincronia. En la carpeta de codigos de Teams hay ejemplos con `TcpClient`, que **no aplican a esta entrega**.

### Al borrar un modelo hay que borrar su imagen (clase 15-09)
La imagen es parte de los datos del modelo. La letra lo pide y en clase se aviso que **lo van a chequear** en la correccion.

### Persistencia de datos: opcional, no exigida (clase 15-09)
Serializar las estructuras a TXT/JSON/XML y recargarlas al arrancar el servidor **no esta en la letra**. El docente lo recomienda (ahorra recrear datos de prueba) pero **si no llegan, no pasa nada**.

## Reglas de repositorio y entrega (letra)
Todo esto salio del PDF, no se habia dicho en clase. **Es lo que mas riesgo tiene de arruinar una entrega ya hecha.**

1. **Un unico repositorio de GitHub**, dentro de la organizacion **`https://github.com/orgs/ArqSis-PDR-2026-2`**. Los docentes avisan como acceder. Un repo personal fuera de la organizacion **no cuenta**.
2. **El repositorio debe ser privado.**
3. **Todos los integrantes deben pertenecer al repositorio.**
4. **El nombre del repo lleva el numero de estudiante**, y opcionalmente el o los dictados. Ejemplo de la letra: `m6a-ingenieria-123456-9876543`.
5. **Commits representativos** de que parte hizo cada integrante. Con peer programming, usar **PR indicando los colaboradores**.
6. **Release de GitHub al entregar**, con **codigo fuente, compilados y documentacion**.
7. **El link al repositorio va en la primera pagina de la documentacion.**
8. **Grupos de hasta 3 personas del mismo dictado.** Ver la nota de contradiccion mas abajo.
9. Pasos en orden: **inscribirse**, formar el equipo en Gestion por numero de estudiante, y subir antes de las **21:00 del 28/09/2026**. Maximo **40 MB**.
10. Problemas de inscripcion o tecnicos: **`adjuntos_ei@ort.edu.uy`, antes de las 20:00 del dia de entrega**.

## Reglas sobre IA generativa
Dos capas, y hay que cumplir las dos.

**Lo que exige la letra (documental).** Si se uso una herramienta de IAG hay que **citarla**, indicando **a)** la o las herramientas utilizadas y **b)** el contexto de uso (generacion de ideas, redaccion inicial, analisis de datos, correccion de estilo, etc.). Todo contenido producido por IAG **debe ser revisado y verificado**; cualquier error presente **es responsabilidad del estudiante**. La letra agrega que el uso "no sustituye el razonamiento critico ni la elaboracion personal".

**Lo que exige el docente (defensa).** Dicho con el mismo enfasis en la clase 1 (18-08) y en la clase 3 (27-08). **No esta prohibida** - los docentes la usan - pero:

> "Lo que sea que ustedes ponen en el obligatorio tienen que entender, saberlo. Si yo agarro asi y senalo una linea de codigo, ustedes me tienen que decir por que esta eso. Que hace, como lo hace y por que esta ahi." (27-08, [01:31])

Corolario practico: **cada linea del entregable tiene que ser defendible**, y ademas **el uso de IA se declara por escrito**.

## Reglas de diseno de concurrencia (aplican todo el curso)

1. **Nunca mas programar con un solo hilo.** "Siempre que tengamos la oportunidad de hacerlo en multihilo, tenemos que hacerlo en multihilo" (20-08). Excepcion: tareas con relaciones de precedencia fuertes, que no se pueden paralelizar.
2. **No usar `Abort`.** Matar un hilo deja **datos inconsistentes en memoria**. El patron correcto: bandera que los hilos revisan, y **final ordenado**. Mas adelante, `CancellationToken` (27-08).
3. **Todos los hilos son foreground.** El diseno tiene que ser tal que **el servidor no pueda terminar hasta que todos sus hijos terminen**; con persistencia, `Join` explicito antes de guardar (27-08). Background solo para servicios que **no modifican datos** (por ejemplo un hilo de logs).
4. **`Monitor.Enter` siempre con `try` / `finally` que haga el `Exit`.** Sin `finally`, una excepcion deja el bloqueo tomado para siempre. Y el `try` va **donde ocurre la excepcion**: el `try/catch` de un nivel superior **no captura** lo que pasa en otro hilo (01-09).
5. **`Monitor.Wait` siempre dentro de un `while`, nunca un `if`.** Al despertar hay que reevaluar la condicion (01-09).
6. **Minimizar el codigo dentro del lock.** Solo lo minimo necesario para la consistencia. La demo del 03-09 midio **40 s contra 4,3 s** en el mismo problema solo por sacar el trabajo pesado de la zona de exclusion.
7. **Tomar los locks siempre en el mismo orden** para no crear deadlocks. Sugerencia concreta: orden alfabetico (01-09).
8. **Preferir estructuras concurrentes de la biblioteca.** `ConcurrentDictionary` ya implementa lectores-escritores; es mucho mejor que un `Dictionary` con un `lock` global. "No las tienen que implementar, si tienen que entender como funcionan" (18-08, repetido 01-09 y 03-09).
9. **A cada hilo, una copia de sus parametros.** En .NET hasta un entero es un objeto y se pasa por referencia. Dentro de un `for`, declarar **una variable nueva por iteracion** (27-08, repasado 01-09).
10. **`Thread.Sleep` no va en produccion.** Solo para pruebas, para simular trabajo, o para bajarle el ritmo a un polling y que no sea espera activa. Y aumentar la probabilidad de un orden **no lo garantiza** (01-09, 03-09).
11. **No poner `Sleep` en el loop de recepcion.** `Receive` ya es bloqueante y espera; esperar de mas es mala practica y no aporta nada (08-09).

## Reglas de protocolo y sockets

0. **La trama sugerida por la letra tiene largos exactos** (letra): `HEADER` = `RES`/`REQ`, **3 bytes**; `CMD` = `0-99`, **2 bytes**; `LARGO` = entero, **4 bytes**; `DATOS` = variable. Los valores van **alineados a la derecha** y **los bytes de relleno deben tener el valor 0**. El protocolo es **orientado a caracteres, sobre TCP/IP**, y la letra dice que los estudiantes **pueden y deben cambiarlo** - pero todo cambio se documenta.
1. **El campo `Largo` cuenta bytes, no caracteres.** En UTF-8 la enie ocupa 2 bytes y un emoji 3 o 4. Usar el `Length` del array de bytes, nunca el del string. Sumar "+1" a ojo **no es solucion** (08-09).
2. **El `Largo` tiene tamano fijo.** Con ceros a la izquierda (`PadLeft`) o con `BitConverter.GetBytes` (4 bytes desde `int`, 2 desde `short`). Si el largo fuera variable, haria falta otro largo para el largo (08-09).
3. **Cliente y servidor tienen que acordar la misma representacion.** Encoding y representacion binaria de numeros son ambiguos; si cada extremo elige distinto, no funciona (08-09).
4. **`Receive` puede devolver menos bytes de los pedidos.** Hay que llamarlo en un `while` con **offset** hasta completar el mensaje. **`Receive` devolviendo 0 significa conexion cerrada** (08-09).
5. **`Shutdown` antes de `Close`.** Buena practica explicita (03-09).
6. **Puertos a partir de 1024.** Hasta 1024 estan reservados. El **cliente puede usar puerto 0** (le pide uno libre al sistema); **el servidor no puede** (03-09).
7. **El backlog de `Listen` funciona distinto en Windows y en Linux.** Linux lo maneja a nivel de socket en forma automatica. "Este mismo codigo funciona diferente en Windows que en Linux" (03-09).
8. **La misma linea de creacion de socket en cliente y servidor**, para garantizar el mismo protocolo (03-09).
9. **La IP y el puerto no se cablean**: van en archivo de configuracion o variables de entorno. Es requerimiento de la letra y **hace falta en la defensa**, porque las IP del laboratorio son DHCP y cambian (03-09, 08-09).
10. **No mandar paquetes muy chiquitos.** Es ineficiente en ancho de banda; conviene acumular los campos de una operacion y mandarlos juntos (01-09).

## Reglas de forma y entorno

- **Todo es aplicacion de consola.** No hay interfaz grafica: las bibliotecas graficas de Windows no corren en el Linux del docente. Puede pedirse una interfaz **web** este semestre (18-08).
- **Teams es el canal oficial.** Ahi van los avisos, los codigos, las planillas de inscripcion a defensas y los cambios de fecha. Es responsabilidad del estudiante revisarlo (18-08).
- **Las diapositivas de Aulas son una guia, no el contenido.** "No esta todo dicho en las diapositivas": hay que tomar apuntes de clase (18-08).
- **La defensa es en laboratorio, con Windows 10/11, en varias maquinas.** Probar antes en ese entorno (letra, 27-08, 08-09). La letra agrega que **es presencial salvo que se habilite defensa oral sincronica remota**, y que **el docente define y comunica la modalidad**.
- **Grupos de hasta 3 personas del mismo dictado** (letra). **Contradiccion abierta:** el 18-08 en clase se dijo que **se pueden mezclar turnos y grupos docentes**, con el requisito de ir todos juntos a la defensa con el grupo docente elegido. El PDF es posterior y es la fuente formal, asi que **la regla operativa es "mismo dictado"**; si el grupo ya se armo mezclando turnos, **preguntar por Teams antes del 28/09**. Item 22 de [[Huecos y contradicciones del material PDR]].
- **La nota del obligatorio es individual** y varia segun la defensa y el trabajo de cada integrante (letra). Cada integrante debe haber participado en la codificacion de **al menos alguna parte significativa**, y **todos deben conocer toda la solucion** (letra, 27-08).
- **Llevar el obligatorio al dia.** Argumento del docente: las dudas se resuelven en la clase siguiente y sale mucho mas barato (18-08).

## Herramientas que recomendaron
- **JetBrains Rider** (preferencia del docente) o Visual Studio. Licencias estudiantiles con **correo institucional**.
- **Postman** para clientes REST sin programarlos.
- **Sysinternals / Process Explorer** de Microsoft, para ver y suspender procesos e hilos: "un task manager pero con esteroides" (27-08).
- `ipconfig` / `ifconfig` y **`netstat`** para IP y puertos ocupados (03-09).
- Para correr varias instancias: **Rider** `Run` -> `Edit configurations` -> varias instancias; **Visual Studio** -> multiple startup projects (03-09).

## Pendiente de confirmar
- **Umbral de exoneracion.** El docente dijo que el porcentaje de exoneracion suele ser alto, pero **no confirmo el numero**. Un estudiante pregunto si se exonera con 86; sin respuesta explicita. **No esta en la letra.**
- **Defensas de los Obligatorios 2 y 3.** El 27-08 se dijo que el segundo no tiene defensa (solo devolucion); el 08-09 se dijo que el segundo y el tercero **si** tienen. Ver [[Huecos y contradicciones del material PDR]].
- **Modalidad exacta de la defensa del Obligatorio 1.** La letra dice **presencial salvo habilitacion de defensa oral sincronica remota**, y que **el docente define la modalidad**. El 27-08 el docente menciono ademas una **defensa escrita**. La letra no contempla esa variante: si aparece, sale del docente, no de la letra. **Resuelto el 2026-09-24 (fuente: clase).** El docente confirmo la modalidad operativa, sin mencionar la variante escrita: defensa presencial en el laboratorio, el jueves 2026-10-01 (puede continuar el martes 2026-10-06), con todo el grupo presente, anotandose en la planilla de Teams para elegir horario. La wifi del laboratorio tiene microsegmentacion (dos maquinas conectadas por wifi no se pueden comunicar entre si) y puede hacer falta desactivar el firewall. Se prueba con el Release de GitHub bajado en el momento, contra una planilla de testing con dos clientes donde nada se puede cerrar ni explotar, mas preguntas de codigo. Ver [[Clase 2026-09-24 - Middleware orientado a mensajes, RabbitMQ y AMQP]].
- **Formato del archivo de entrega.** La letra dice tres cosas distintas (zip/rar/pdf en la caratula, un PDF en `ENTREGA`, zip o rar con el pdf adentro en el `RECORDATORIO`). Opcion segura: **un zip que contenga el PDF**. Item 21 de [[Huecos y contradicciones del material PDR]].
- **Grupos de distinto dictado.** La letra pide mismo dictado, la clase del 18-08 permitia mezclar. Item 22.

## Connections
- Letra oficial en crudo: `Sources/OBLIGATORIO/Obligatorio 1 Agosto 2026.pdf.md`
- Evaluacion: [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]]
- Errores concretos con su version correcta: [[Errores caros en Programacion de Redes]]
- Clases fuente: [[Clase 2026-08-18 - Presentacion del curso e introduccion a sistemas distribuidos]] · [[Clase 2026-08-20 - Modelos de arquitectura, procesos e hilos]] · [[Clase 2026-08-27 - Letra del Obligatorio 1 y practica con Thread]] · [[Clase 2026-09-01 - Exclusion mutua en CSharp y arranque de sockets]] · [[Clase 2026-09-03 - Minimizar bloqueos y sockets en detalle]] · [[Clase 2026-09-08 - Protocolo propietario, framing y Receive parcial]]
- Proyecto: [[Programacion de Redes 2026]]
