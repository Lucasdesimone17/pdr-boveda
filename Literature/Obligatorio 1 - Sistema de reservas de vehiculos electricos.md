---
date: 2026-09-09
updated: 2026-09-10
type: literature
tags:
  - pdr
  - evaluacion
  - obligatorio
  - tema-1
  - tema-2
ai-first: true
confidence: stated
status: en curso
entrega: 2026-09-28
puntaje_max: 25
puntaje_min: 5
sistema: EDISON
github_org: ArqSis-PDR-2026-2
fuente: "Letra oficial en PDF: `Sources/OBLIGATORIO/Obligatorio 1 Agosto 2026.pdf.md` (original en `PDR/PDR/OBLIGATORIO/CONSIGNA/`). Ampliada con las aclaraciones orales de las clases del 20-08, 27-08, 01-09, 03-09 y 08-09-2026 (grabaciones Vimeo, showcase 12367503)"
---

# Obligatorio 1 - Sistema de reservas de vehiculos electricos

## For future agent
Consigna del Obligatorio 1 de Programacion de Redes 2026. El sistema se llama **EDISON**. **Es la referencia operativa del trabajo hasta el 2026-09-28.**

**Cambio de estatus el 2026-09-10:** esta nota ya **no** se apoya en la transcripcion de las grabaciones. Se ingirio el **PDF oficial de la letra** y la nota quedo reescrita contra el. Convencion que se usa de aca en adelante:

- **(letra)** = esta textual en el PDF. Confianza `stated`, manda siempre.
- **(clase DD-MM)** = aclaracion oral del docente, no esta en el PDF. Confianza `stated` sobre lo dicho, pero es interpretacion de la letra, no la letra.

Lo que aparecio nuevo al leer el PDF esta consolidado en la seccion "Que cambio al leer el PDF". Las reglas duras del curso viven aparte en [[Reglas y prohibiciones del curso PDR 2026]].

## Datos formales (letra)

| Item | Valor |
|---|---|
| Sistema | **EDISON** |
| Dictados | Matutino y Nocturno |
| Entrega | **28/09/2026 hasta las 21:00** en `gestion.ort.edu.uy` |
| Si no se entrega por gestion | **puntaje final 0** |
| Tamano maximo | **40 MB** |
| Formato del archivo | La letra se contradice: caratula dice "zip, rar o pdf", `ENTREGA` dice "un archivo PDF", `RECORDATORIO` dice "zip o rar con el pdf adentro". Ver la seccion "Contradiccion de formato de entrega" |
| Puntaje | minimo **5**, maximo **25** |
| Grupos | hasta **3 personas del mismo dictado** |
| Codigo | **un unico repositorio privado** dentro de la organizacion `ArqSis-PDR-2026-2` |
| Defensa | **obligatoria y eliminatoria** |
| Contacto por problemas | `adjuntos_ei@ort.edu.uy`, **antes de las 20:00 del dia de entrega** |

Pasos que la letra marca como IMPORTANTE, en orden: **1) inscribirse · 2) formar grupo de hasta 3 del mismo dictado · 3) subir a Gestion antes de la hora**.

## Objetivos declarados (letra)
Poner en practica lo de la primera parte del curso:
- Manejo de **paralelismo, concurrencia y sincronizacion de Threads**.
- Manejo de **Sockets**.
- Manejo de **Streams**.
- **Implementacion y diseno de un protocolo propietario.**

## Escenario: EDISON (letra)
Una afamada empresa internacional de vehiculos electricos llega a Uruguay con precios muy competitivos (en algunos modelos por debajo de los de Estados Unidos). En las primeras 24 horas se concretan cientos de ventas y se llega a miles de compromisos durante el primer fin de semana. Las reservas se tomaron por una **landing page provisoria** donde el cliente unicamente podia **senar una unidad** para recibirla meses despues. El magnate dueno de la compania se entera de que la filial uruguaya aun no tiene sistema permanente de ventas, reservas y catalogo. El estudiante es contratado como Ingeniero de Sistemas para desarrollarlo.

Se pide **un sistema con dos aplicaciones: servidor y cliente**. Reglas del dominio, textuales:
- **El catalogo de modelos es permanente**; lo que varia es el **stock de unidades disponibles** de cada modelo.
- Las reservas, **cuya confirmacion esta sujeta a un proceso de aprobacion**, **afectan al stock una vez confirmadas**.

Aclaraciones orales:
- **(clase 08-09)** Un modelo **si se puede eliminar**, con la restriccion de CR4 (sin reservas activas). Resuelve la tension aparente con "el catalogo es permanente" (item 17 de [[Huecos y contradicciones del material PDR]], cerrado).
- **(clase 27-08)** Hay **dos roles implicitos**: quien vende (publica modelos) y quien compra o reserva. **Como se separan es decision del grupo** - lo mas simple es que el mismo usuario pueda hacer las dos cosas; otra opcion valida es elegir rol al entrar al menu. **Hay que documentar la decision.** La letra no define roles: es un supuesto a declarar en el punto 3 de la documentacion.

## Requerimientos de la aplicacion cliente (letra, CR1-CR10)

| ID | Requerimiento | Detalle textual de la letra |
|---|---|---|
| **CR1** | Autenticacion | Crear cuenta (**nombre de usuario unico** y contrasena), iniciar sesion y cerrar sesion |
| **CR2** | Alta de modelo | Usuario autenticado da de alta modelos para ser reservados: **nombre del modelo (unico)**, descripcion, precio, fecha de entrega prometida, **stock inicial** y **opcionalmente una imagen** representativa |
| **CR3** | Modificacion de modelo | Solo **el usuario que lo creo**, y **mientras sigan habiendo unidades disponibles**: descripcion, precio, fecha de entrega, stock, imagen, etc. **Al actualizar stock, el nuevo valor debe ser mayor a la cantidad de reservas existentes** |
| **CR4** | Baja de modelo | Solo **el usuario que lo creo**, **siempre que no existan reservas activas** asociadas |
| **CR5** | Solicitud de reserva | Usuario autenticado solicita reserva de **una unidad** de cualquier vehiculo disponible. Queda en **pendiente** y pasa a **confirmada o rechazada luego de un tiempo configurable, en segundos**. **Al procesarse, si no existen unidades disponibles, la reserva debe ser rechazada** |
| **CR6** | Cancelacion de reserva | El que reservo puede cancelar **siempre y cuando la reserva no sea rechazada**. Al cancelar, **la unidad queda disponible para otra reserva** |
| **CR7** | Busqueda y filtrado | Visualizar los vehiculos disponibles y filtrar por **al menos 3 criterios**. Ejemplos de la letra: nombre del modelo, cantidad de unidades disponibles, precio maximo, precio minimo, fecha de entrega. **"Criterios a definir por los estudiantes"** |
| **CR8** | Consultar vehiculo | Todos los detalles: descripcion, precio, **unidades disponibles**, **unidades reservadas**, y **opcionalmente descargar la imagen** |
| **CR9** | Consultar estado de reservas | Ver **modelo, fecha de creacion, estado y ultima actualizacion** |
| **CR10** | Historial de actividades | Historial de sus actividades: **inicio de sesion, vehiculos creados / eliminados / modificados, reservas hechas y compras realizadas** |

Aclaraciones orales sobre estos requerimientos:
- **(clase 27-08)** El cliente es **una interfaz liviana de consola**: toma datos, valida formato y los manda; **el procesamiento va en el servidor**. La letra no lo dice: sale de que SR1 pone toda la funcionalidad del lado servidor.
- **(clase 08-09)** En CR8, **"opcionalmente" significa que el usuario puede elegir** si baja la imagen, no que el grupo pueda no implementarlo.
- **(clase 27-08)** CR10 es un log de actividad. El docente adelanto que **en un obligatorio posterior probablemente se mande a otro servidor**.
- La letra menciona **"compras realizadas"** en CR10 sin que exista un requerimiento de compra en CR1-CR9. Es un hueco de la letra: ver "Preguntas abiertas sobre la letra".

## Requerimientos de la aplicacion servidor (letra, SR1-SR4)

| ID | Requerimiento | Detalle textual |
|---|---|---|
| **SR1** | Funcionalidad completa | El servidor **debe contar con todas las funcionalidades necesarias** para que los clientes realicen los requerimientos anteriores |
| **SR2** | Conexiones multiples | Debe aceptar **multiples conexiones simultaneas**. **Las acciones de un cliente no deben afectar el rendimiento o la funcionalidad de los demas** |
| **SR3** | Eliminacion de archivos | Cuando un vehiculo sea eliminado, **las imagenes asociadas deberan eliminarse del servidor** |
| **SR4** | Cierre controlado | **Desde la consola del servidor** debe haber una opcion que permita cerrarlo **de forma controlada, finalizando las conexiones activas con los clientes** |

**(clase 27-08 y 03-09)** Sobre SR4: no se puede cortar algo que se esta haciendo. El patron correcto es **bandera + hilos foreground + `Join`**, nunca `Abort`.

## Requerimientos en comun (letra)
La letra los pone **aparte**, aplican a **cliente y servidor**, no solo al servidor:

- **Configuracion.** El sistema debe ser **portable entre maquinas sin necesidad de recompilar**. Los valores especificos de entorno deben estar **configurados externamente** (por ejemplo, mediante archivos de configuracion).
- **Desconexiones.** Las desconexiones de clientes **no deben causar errores** en el servidor ni en otros clientes.
- **Manejo de errores.** Ante un error procesando una solicitud, el servidor **notifica adecuadamente al cliente y continua funcionando normalmente**.

**(clase 03-09 y 08-09)** Lo que va en configuracion, como minimo: **IP, puerto y el tiempo de aprobacion de reservas de CR5**. Hace falta si o si **en la defensa**, porque las IP del laboratorio las asigna DHCP y cambian.

## Datos de prueba (letra, opcional)
"Se puede entregar el sistema con datos precargados para facilitar su validacion."

**(clase 27-08)** El docente recomendo **persistencia simple**: al levantar leer un archivo, al salir escribirlo. Facilita las pruebas y encaja con el `Join` antes de guardar.

## Compilados (letra)
El repositorio **debe incluir las aplicaciones compiladas en modo Release** con **todo lo necesario para ejecutarlas** (archivos de configuracion, etc.), con **ejecutables para Windows** (`.exe`, `.dll`, etc.). Se recomienda **subir todo el proyecto junto con los archivos `.sln`** para facilitar la correccion.

**(clase 27-08)** Consecuencia practica: hay que **modificar el `.gitignore`**, porque por defecto ignora `bin/` y `obj/` y la compilacion en release no subiria.

## Especificacion del protocolo (letra)
- El protocolo de la letra **es una sugerencia**: "los estudiantes pueden (y **deben**) hacer cambios al mismo".
- **Orientado a caracteres.**
- **Implementado sobre TCP/IP.**
- Los valores **deberan ir alineados a la derecha**, y **los bytes de relleno deberan tener el valor 0**.
- Los campos **HEADER, CMD y LARGO tienen largo fijo**. El campo **DATOS tiene largo variable**, segun el valor indicado en LARGO.

### Formato general de la trama (tabla textual de la letra)

| Nombre del campo | HEADER | CMD | LARGO | DATOS |
|---|---|---|---|---|
| **Valores** | `RES`/`REQ` | `0-99` | Entero | Variable |
| **Largo** | **3** | **2** | **4** | **Variable** |

Trama minima entonces: **9 bytes de encabezado** + el payload.

### Como implementarlo (clase 08-09, ver [[Clase 2026-09-08 - Protocolo propietario, framing y Receive parcial]])
- El campo `LARGO` existe porque **TCP entrega un stream sin delimitar mensajes**: sirve para dimensionar el buffer y para saber si falta recibir.
- **El `LARGO` cuenta bytes, no caracteres.** Con UTF-8 la enie ocupa 2 bytes y un emoji 3 o 4. Usar `datos.Length` del array de bytes, nunca `texto.Length`.
- Dos formas validas de conseguir largo fijo: **texto con ceros a la izquierda** (`PadLeft`) o **`BitConverter.GetBytes`** (4 bytes con `int`, 2 con `short`). Ojo: **la letra pide protocolo orientado a caracteres y relleno con valor 0**, lo que empuja hacia la variante textual con `PadLeft`; si se usa `BitConverter`, es un cambio al protocolo sugerido y **hay que documentarlo** (la letra habilita explicitamente los cambios).
- Cliente y servidor **tienen que acordar la misma representacion** de numeros y el mismo encoding.
- **`Receive` puede devolver menos bytes de los pedidos**: llamarlo en un `while` con offset hasta completar. **`Receive` devolviendo 0 significa conexion cerrada.**
- Eficiencia: **no mandar paquetes muy chiquitos**. Acumular todos los campos de una operacion y mandarlos juntos.
- **Sin `Sleep` en el loop de recepcion**: `Receive` ya bloquea.

## Documentacion (letra)
Se espera que incluya, en este orden:
1. **Alcance** de la aplicacion.
2. **Errores conocidos** (si los hay).
3. **Supuestos de la letra** que hayan realizado.
4. **Descripcion de la arquitectura.**
5. **Diseno detallado de cada uno de sus componentes.**
6. **Diseno del protocolo de comunicacion.**

Ademas, textual de la letra:
- Es **"muy importante (tanto como la aplicacion)"** que la documentacion explique **las principales decisiones tomadas**: por que se tomaron las decisiones a nivel de protocolo, **como se manejan los errores**, **cuales son los mecanismos de concurrencia utilizados**, etc.
- Se debe documentar el **funcionamiento** de la aplicacion. **No hace falta manual de usuario**, pero si las aclaraciones necesarias para poder probarla, usarla y comprender su estructura.
- **No se espera ningun formato particular.** El orden, tecnicas y nomenclaturas los deciden los alumnos; **se evalua la correcta seleccion y uso** de los mismos.
- Se espera el **adecuado uso de diagramas** que complementen las explicaciones.
- **El link al repositorio debe estar en la primera pagina de la documentacion.**

**(clase 27-08)** Los diagramas **no al nivel de Diseno 2**. Y conviene documentar los **formatos de fecha y numericos aceptados**, porque los docentes prueban la aplicacion a ciegas.

**Citacion de IA generativa (letra, seccion de condiciones).** Si se uso una herramienta de IAG hay que citarla, indicando:
1. **la/las herramientas utilizadas**, y
2. **el contexto de uso**: generacion de ideas, redaccion inicial, analisis de datos, correccion de estilo, etc.

Todo contenido producido por IAG **debe ser revisado y verificado**; cualquier error presente **es responsabilidad del estudiante**. El uso "no sustituye el razonamiento critico ni la elaboracion personal".

## Codigo y repositorio (letra)
La gestion del codigo se hace con **UN UNICO repositorio de GitHub**:

1. **El repositorio debe pertenecer a la organizacion `https://github.com/orgs/ArqSis-PDR-2026-2`.**
   - Los docentes avisan como acceder a la organizacion.
   - Con el acceso, hay que **crear el repositorio e invitar a los integrantes**.
   - **Incluir el numero de estudiante en el nombre del repositorio**, y opcionalmente el o los dictados. Ejemplo de la letra: `m6a-ingenieria-123456-9876543`.
2. **El repositorio debe ser privado.**
3. **Todos los integrantes deben pertenecer al repositorio.**
4. **Commits representativos** de que parte desarrollo cada integrante. Si trabajaron varios en los mismos commits (peer programming), usar **PR e indicar los colaboradores**.
5. **El link al repositorio va en la primera pagina de la documentacion.**
6. Al entregar hay que hacer un **Release de GitHub**, y **la rama `main` (o `master`) no debe ser afectada luego del hito de la entrega**. El release **debe incluir codigo fuente, compilados y documentacion**.

Textual de la seccion ENTREGA: **"Solo se aceptara lo entregado en gestion y el codigo en el repositorio de la organizacion de GitHub"**.

**(clase 27-08)** Dejar el repo quieto unos dias despues del 28/09, porque los docentes lo bajan en esa ventana. Alternativa sugerida por el docente: usar `main` solo para releases. El `README` **no es obligatorio**.

## Evaluacion (letra)
Criterios de correccion, textuales:

- **"Deberan utilizar explicitamente la clase `Thread` y no `Task`"** para el desarrollo del obligatorio.
- **No esta permitido `binary formatter`** para el pasaje de informacion: **se espera que se utilicen streams**.
- **"En esta entrega NO se permitira que se utilicen parsers existentes (como puede ser JSON) para serializar objetos."**
- En la defensa **se correra un plan de testing**, evaluando **cuantos puntos pasa el sistema**.
- **Criterios de diseno.**
- **Porcentaje de cumplimiento con los requerimientos presentados.**
- **Funcionamiento de la aplicacion.**
- **Cumplimiento de buenas practicas de los temas trabajados en clase** (multi-threading, sockets, etc.).
- **Calidad de diseno e implementacion.**
- **Completitud y calidad de la documentacion.**

**(clase 27-08)** Regla de oro sobre IA generativa, complementaria a la citacion que pide la letra: se permite usarla, pero **hay que poder explicar cualquier linea**. "Si yo agarro asi y senalo una linea de codigo, ustedes me tienen que decir por que esta eso."

## Defensa (letra)
- **La defensa es obligatoria y eliminatoria.** **La no presentacion implica la perdida de la totalidad de los puntos del Obligatorio.**
- **El docente define y comunica la modalidad y mecanica** de la defensa.
- **La defensa sera presencial**, salvo en casos en que se habilite la **defensa oral sincronica en forma remota**.
- Requiere **ejecutar la aplicacion en varias maquinas del laboratorio**.
- Las maquinas del laboratorio usan **Windows 10 u 11**: probar antes y **tener los compilados en la entrega**.
- Se evalua el **conocimiento general de todos los integrantes** sobre la solucion: **todos deben conocer toda la solucion**.
- **La nota del obligatorio es individual** y varia segun el resultado de la defensa y el trabajo de cada integrante.
- Cada integrante debe haber participado en **la codificacion de al menos alguna parte significativa**.
- **Fecha y lugar se coordinan con el docente por el grupo de Teams.**

**(clase 08-09)** Hay que **configurar la IP del servidor en el cliente en el momento**, por DHCP. Puede haber que cambiar algo de codigo en vivo, pero el foco es correr el plan de test.

## Que cambio al leer el PDF
Comparacion contra la version de esta nota del 2026-09-09, que se habia reconstruido solo de las grabaciones. Nada de lo anterior resulto **falso**; lo que hubo fue **material que la clase no cubrio** y **dos precisiones de estructura**.

### Nuevo, no estaba en la boveda
| Que | Por que importa |
|---|---|
| **El sistema se llama EDISON** | Nombre util para el repo, los namespaces y la caratula |
| **El repo va en la organizacion `ArqSis-PDR-2026-2`, privado, uno solo** | **Es la novedad mas cara.** La boveda decia solo "repositorio de GitHub". Un repo personal fuera de la organizacion no cuenta como entregado |
| **El nombre del repo lleva el numero de estudiante** (`m6a-ingenieria-123456-9876543`) | Convencion obligatoria de nombre |
| **"Solo se aceptara lo entregado en gestion y el codigo en el repositorio de la organizacion"** | Cierra la puerta a entregar codigo por otra via |
| **Largos exactos de la trama: 3 / 2 / 4 / variable** | La clase dio la estructura, no los tamanos. `HEADER` es `RES`/`REQ` (3), `CMD` es `0-99` (2), `LARGO` es entero de 4 |
| **Los bytes de relleno tienen que valer 0** | La boveda decia "relleno por delante" sin el valor |
| **Grupos de hasta 3 del MISMO DICTADO** | Contradice lo dicho en clase el 18-08 sobre mezclar turnos. Ver [[Huecos y contradicciones del material PDR]] |
| **La defensa es obligatoria y eliminatoria**; no presentarse = perder todos los puntos | La boveda lo tenia solo del obligatorio historico de marzo 2026, no de la letra vigente |
| **Hay que citar herramientas de IA y el contexto de uso en la documentacion** | Requisito de documentacion, no solo la regla oral de "explicar cada linea" |
| **Inscribirse es el paso 1** y el equipo se arma en Gestion por numero de estudiante | La boveda solo tenia la planilla de Teams, que es para las defensas |
| **`adjuntos_ei@ort.edu.uy` antes de las 20:00 del dia de entrega** ante problemas tecnicos | Plan B con hora limite propia |
| **CR7 cierra con "criterios a definir por los estudiantes"** | Los 3 criterios los elige el grupo, no hay lista obligatoria |

### Correcciones de estructura
- **El servidor tiene 4 requerimientos, no 8.** `Configuracion`, `Desconexiones` y `Manejo de errores` son **requerimientos en comun** de cliente y servidor, no requerimientos del servidor. `Datos de prueba` y `Compilados` son secciones aparte. La version anterior de esta nota los numeraba del 1 al 8 como si fueran todos del servidor.
- **`Compilados` aplica al repositorio**, no a la entrega de gestion.

### Confirmado por el PDF (era reconstruccion, ahora es `stated`)
Fecha y hora de entrega, 40 MB, 5/25 puntos, grupos de hasta 3, entrega por gestion bajo pena de cero, CR1 a CR10 completos, SR1 a SR4, protocolo orientado a caracteres sobre TCP/IP con campos de largo fijo y datos variable, los 6 puntos de documentacion, los criterios de correccion (incluido `Thread` y no `Task`, sin `binary formatter`, sin parsers JSON), release de GitHub con `main` congelada, y la defensa presencial en laboratorio con Windows 10/11, plan de test y nota individual.

### Sigue siendo solo oral (no esta en el PDF)
El cliente como interfaz liviana, la separacion de roles a criterio del grupo, que se pueda eliminar un modelo, que "opcionalmente descargar la imagen" es eleccion del usuario, la persistencia simple recomendada, el plan de arranque, los diagramas "no al nivel de Diseno 2", editar el `.gitignore`, el `README` no obligatorio, y que CR10 se mande a otro servidor en un obligatorio posterior. Todo eso vale como interpretacion del docente, no como texto de la letra.

## Contradiccion de formato de entrega
La letra dice tres cosas distintas sobre que archivo subir a Gestion:

| Seccion del PDF | Que dice |
|---|---|
| Caratula (`CONDICIONES`) | "max. 40Mb en **formato zip, rar o pdf**" |
| `ENTREGA` (pagina 6) | "Se debe entregar por Gestion **un archivo PDF** que contenga la documentacion y en la primera pagina el link al repositorio" |
| `RECORDATORIO` (pagina 9) | "Cada equipo debe entregar **un unico archivo en formato zip o rar** (los documentos de texto deben ser pdf, y deben ir dentro del zip o rar)" |

El `RECORDATORIO` es texto generico de la Facultad, igual en todas las materias; `ENTREGA` es la seccion especifica de esta letra. **Opcion segura: un zip que contenga el PDF de la documentacion**, que satisface las tres redacciones a la vez. Igual conviene **preguntar por Teams**. Registrado como item 21 en [[Huecos y contradicciones del material PDR]].

## Preguntas abiertas sobre la letra
- **CR10 nombra "compras realizadas"** en el historial, pero **no hay ningun requerimiento de compra** en CR1-CR9: el flujo termina en reserva confirmada. O es un residuo de otra version de la letra, o "compra" es sinonimo de "reserva confirmada". Supuesto a declarar en el punto 3 de la documentacion, y candidato a preguntar por Teams.
- **Formato del archivo de entrega**: ver arriba.
- **Modalidad exacta de la defensa**: la letra dice presencial salvo habilitacion de defensa oral sincronica remota, y que **el docente define la modalidad**. El 27-08 el docente menciono ademas la idea de una **defensa escrita**. Item 16 de [[Huecos y contradicciones del material PDR]].
- **Umbral de exoneracion**: sin confirmar, no esta en la letra.

## Como arrancar (plan que dieron los docentes, clase 27-08 y 03-09)
1. Esqueleto del **cliente**: menu de consola completo, que datos pide y que muestra.
2. **Mockup de la clase** con la que interactua el menu.
3. **Codificacion del protocolo** en el servidor: al llegar un paquete, en funcion del **estado del hilo que atiende al cliente** y del mensaje, aceptarlo o no y despachar la operacion. **Se puede programar sin tener la comunicacion andando.**
4. **Logica y acceso a datos** del servidor, con la estructura en memoria y su concurrencia.
5. Recien despues, cablear la comunicacion por sockets.
6. Para probar: en **Rider**, `Run` -> `Edit configurations` -> habilitar **varias instancias**; en Visual Studio, **multiple startup projects**.

## Recomendaciones de arquitectura (clases 27-08, 01-09, 03-09, 08-09)
- Solucion .NET con **dos proyectos ejecutables** (cliente y servidor) y los proyectos auxiliares que hagan falta (dominio, helpers -> compilan a **DLL**).
- **Clases comunes entre cliente y servidor** para los mecanismos de comunicacion. Es lo que pidieron los docentes desde la primera clase, y encaja con que ambos extremos deben acordar encoding y representacion.
- Division por capas como en Diseno 1: en el servidor, control / sockets / logica / acceso a datos; en el cliente, CLI + sockets.
- Estructuras de datos concurrentes: preferir las que traen los bloqueos adentro, por ejemplo **`ConcurrentDictionary`**, antes que un `Dictionary` con un `lock` de arriba a abajo. Aplica directo a SR2: las acciones de un cliente no deben afectar a los demas.
- **Minimizar el codigo dentro de los locks.** La demo del 03-09 midio **40 s contra 4,3 s** en el mismo problema solo por esto.
- **Cierre ordenado (SR4)**: nada de `Abort`. Un hilo lee la consola y levanta una bandera; los hilos la revisan y terminan solos. Los hilos son **foreground**, y el servidor hace `Join` para poder persistir despues de que todos terminaron.
- Testing: el que importa aca es **de integracion** (peticion que sale del cliente, llega y vuelve), mas el test de usuario.

## Mapa requerimiento a mecanismo de clase
Puente entre la letra y lo que se dio en el teorico. Util para la defensa: cada requerimiento tiene un mecanismo que hay que poder justificar.

| Requerimiento de la letra | Mecanismo visto en clase | Donde se dio |
|---|---|---|
| SR2 conexiones multiples | `Thread` por cliente aceptado, hilos foreground | [[Clase 2026-08-20 - Modelos de arquitectura, procesos e hilos]] · [[Clase 2026-08-27 - Letra del Obligatorio 1 y practica con Thread]] |
| SR2 sin afectar a los demas | `ConcurrentDictionary`, minimizar codigo en el lock, orden fijo de locks | [[Clase 2026-09-01 - Exclusion mutua en CSharp y arranque de sockets]] · [[Clase 2026-09-03 - Minimizar bloqueos y sockets en detalle]] |
| CR3 stock > reservas, CR5 rechazo por falta de stock, CR6 devolver unidad | Seccion critica sobre el stock: `Monitor` / `lock`, `Wait` dentro de un `while` | [[Clase 2026-09-01 - Exclusion mutua en CSharp y arranque de sockets]] |
| CR5 tiempo configurable de aprobacion | Hilo temporizador y `Thread.Sleep` solo para simular espera, mas configuracion externa | [[Clase 2026-09-01 - Exclusion mutua en CSharp y arranque de sockets]] |
| Protocolo HEADER/CMD/LARGO/DATOS | Framing por largo fijo, `PadLeft` o `BitConverter`, `Receive` parcial en `while` con offset | [[Clase 2026-09-08 - Protocolo propietario, framing y Receive parcial]] |
| CR2/CR8 imagen, SR3 borrado de archivos | Streams y envio por bloques, largo en bytes y no en caracteres | [[Clase 2026-09-08 - Protocolo propietario, framing y Receive parcial]] |
| SR4 cierre controlado | Bandera compartida + hilos foreground + `Join`; prohibido `Abort` | [[Clase 2026-08-27 - Letra del Obligatorio 1 y practica con Thread]] |
| Configuracion externa | IP y puerto fuera del codigo; puertos a partir de 1024; el servidor no puede usar puerto 0 | [[Clase 2026-09-03 - Minimizar bloqueos y sockets en detalle]] |
| Desconexiones sin errores | `Receive` == 0 es conexion cerrada; `Shutdown` antes de `Close` | [[Clase 2026-09-03 - Minimizar bloqueos y sockets en detalle]] · [[Clase 2026-09-08 - Protocolo propietario, framing y Receive parcial]] |

## Connections
- Fuente cruda: `Sources/OBLIGATORIO/Obligatorio 1 Agosto 2026.pdf.md` · original en `PDR/PDR/OBLIGATORIO/CONSIGNA/Obligatorio 1 Agosto 2026.pdf`
- Clases donde se explico: [[Clase 2026-08-27 - Letra del Obligatorio 1 y practica con Thread]] · [[Clase 2026-09-01 - Exclusion mutua en CSharp y arranque de sockets]] · [[Clase 2026-09-03 - Minimizar bloqueos y sockets en detalle]] · [[Clase 2026-09-08 - Protocolo propietario, framing y Receive parcial]]
- Temas: [[Tema 1 - Programacion multihilo]] · [[Tema 2 - Sockets y protocolos]]
- Reglas: [[Reglas y prohibiciones del curso PDR 2026]] · [[Errores caros en Programacion de Redes]]
- Huecos abiertos: [[Huecos y contradicciones del material PDR]]
- Proyecto: [[Programacion de Redes 2026]]
- Referencia de formato del ciclo anterior: `Sources/OBLIGATORIO/HISTORICO_MAR2026/`
