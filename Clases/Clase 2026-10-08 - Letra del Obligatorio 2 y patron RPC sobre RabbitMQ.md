---
date: 2026-10-08
type: class-note
tags:
  - pdr
  - clase
  - obligatorio-2
  - rabbitmq
  - rpc
  - docker
ai-first: true
confidence: high
duracion: "~1h41"
fuente: "Grabacion del showcase de Vimeo 12367503, video 1234179474, subtitulos autogenerados es-x-autogen"
---

# Clase 2026-10-08 - Letra del Obligatorio 2 y patron RPC sobre RabbitMQ

## For future agent
Acta de la clase del **2026-10-08** (jueves), dictada por [[Enrique Latorres]]. Tiene dos partes. Hasta el minuto ~23 lee y comenta **la letra del Obligatorio 2**, que entrega el **29-10**. Despues muestra los **ejemplos de codigo de RabbitMQ** que fallaron la clase anterior y el **patron request-reply con correlation id**, presentado como RPC sobre una cola de mensajes. La letra oficial ya esta ingerida y manda sobre esta acta: ver [[Obligatorio 2 - Upgrade asincrono y Docker de EDISON]]. Escrita desde transcripcion automatica, con los terminos tecnicos corregidos y marcados.

**Hay un hueco en la grabacion entre los minutos ~3 y ~8** y no consta que se dijo ahi. La clase del **29-09** esta en el showcase **sin subtitulos** (as of 2026-10-13) y no tiene acta.

## Obligatorio 2: lo dicho en clase
La letra oficial esta en `Sources/OBLIGATORIO/Obligatorio 2 Agosto 2026.pdf.md`. Aca va **solo lo que el docente agrego o remarco de palabra**.

**Fecha y mecanica.**
- Entrega **29 de octubre**, maximo **15 puntos**. Misma mecanica: codigo en el **mismo repositorio** y documento por Gestion, con **el link al repo en la primera pagina**. (stated)
- La documentacion puede partir de la del Obligatorio 1, **modificando lo que haya que modificar**. (stated)
- Al cerrar la lectura se escucha *"el 9 de octubre de 2027 a las 21 horas"*: **error de transcripcion**. La letra dice **29/10/2026 21:00**.
- **No hay defensa propia del Obligatorio 2: se defiende junto con el tercero.** La del ultimo obligatorio *"va a ser bastante mas elaborada"*. (stated, coincide con la letra)
- Grupos de tres. La hoja de requerimientos **se sube a Teams**. (stated)

**Uso de IA.**
- Hay que usar IA generativa, pero tener *"un conocimiento cabal"* de lo que se explica y **poder explicar cada linea**. (stated)
- Para la migracion a asincronico: *"le pueden decir a su amigo imaginario que los ayude, y en general lo hace bastante bien"*. Despues, reflexionar si conviene repensar parte de la logica. (stated)

**Orden sugerido de trabajo** (min ~21-23):
1. Convertir todo a programacion asincronica.
2. Revisar si conviene repensar alguna logica.
3. Agregar el **mensaje de cancelacion** en el cliente y la logica en el servidor.
4. Aplicar los cambios que surgieron en las defensas del Obligatorio 1.

**Docker sin multi-stage.** Hacer el **release para Linux**, y en el Dockerfile **copiarlo con `COPY`** al directorio de la aplicacion. Lo comparo con lo que hicieron en Infraestructura o en Taller de Tecnologias. El **multi-stage build** compila en un contenedor de desarrollo y copia a una imagen que solo tiene el runtime de .NET; es mas ajustado, pero **queda para el proximo obligatorio**. (stated, coincide con la letra)

**Base de datos.**
- MySQL o la que prefieran, **corriendo en Docker**. Hay que migrar el guardado de datos **excepto las imagenes**. (stated)
- **Recomienda Oracle**, porque ya la conocen de Base de Datos 2 y el docente tiene mas ejemplos (*"creo que hay un ejemplo de interfaz de C# con Oracle"*). (stated)
- Como minimo, **alguna de las entidades** tiene que manejarse en la base. (stated)
- El motivo: el proyecto se va a **continuar en la materia de Seguridad**, que tiene la carrera de Ingenieria al semestre siguiente, con los mismos docentes. (stated, coincide con [[Programacion de Redes 2026]])
- Todo apunta a un **Docker Compose** con servidor, cliente y base. Otra opcion es poner la base dentro de un contenedor con la aplicacion. El docente dijo que quiza muestre varias formas. (stated)

**Docker Compose, recomendado.** La letra lo hace opcional, pero el docente **lo sugiere**: un `docker compose up` levanta todo con las redes que hacen falta. Sin Compose, si alguien cambia el nombre de un contenedor, el cliente pierde la referencia al servidor. (stated)

**Mejoras pedidas a partir del Obligatorio 1.**
- Hubo pocos detalles porque *"les fue muy bien"*. (stated)
- **Mejora general: un `ReadLine` o `ReadKey` al final de las aplicaciones.** En un contenedor la aplicacion suele ser lo unico que corre: si sale, no se puede leer la consola. Si termina pidiendo una tecla, se hace `docker attach`, se ve el mensaje y se confirma. Ejemplo: cerrar el servidor hacia que el cliente se cerrara sin mostrar nada. (stated)

**Requerimiento 5: cancelacion y comprobante.**
- **El periodo de aprobacion de una reserva tiene que ser bastante mas largo** que los pocos segundos que usaron. (stated)
- El cliente manda la solicitud y despues puede **arrepentirse y mandar una cancelacion**, que tiene que **llegar a la `Task` del servidor y cancelarla**, por ejemplo antes de que se apruebe. (stated)
- El servidor tiene que **guardar en memoria que cliente solicito que**, para cancelar la tarea correcta. (stated)
- Lo que antes era un `sleep` pasa a ser **un loop con delays** que va contando el tiempo y **evalua el `CancellationToken`**. Si se cancela, termina. (stated)
- Hay que ver **que deshacer** al cancelar, y **avisarle al cliente** que se cancelo, *"tal vez con un mensaje"*. (stated)
- Las reservas aprobadas generan **un archivo como comprobante** que se guarda en disco. **Sugerencia, no esta en la letra:** que el cliente pueda descargar el comprobante, transferido igual que una imagen. (stated)
- Usar la cancelacion de `Task`: *"vimos unos ejemplos"*. Ver [[Clase 2026-09-22 - Parallel, CancellationToken y cuando usar async]].

**Protocolo y documentacion.**
- Se puede **seguir con el mismo protocolo** o cambiarlo. (stated)
- En la documentacion: **que se modifico, por que, y que beneficio trae** respecto del codigo anterior. Le gustaria una **comparacion del codigo viejo con el nuevo**. Sugerencia: **dejar el codigo viejo congelado en un branch** y seguir en `main`. (stated)
- Release en `main`, y `main` no se toca despues del hito. El docente baja el codigo la noche de la entrega, pero *"por las dudas"* preguntar antes de modificar algo despues. (stated)

**Restricciones.** No usar la clase `Thread`. No usar formateador binario: usar `Stream`. **No usar parsers de serializacion existentes.** La transcripcion dice *"Jackson"*; la letra dice **"como puede ser Json"**, asi que fue un error de transcripcion. (stated)

**Proxima clase:** Docker en detalle, con algun ejemplo, y **la devolucion de las defensas** del Obligatorio 1. (stated)

## Ejemplos de RabbitMQ: por que fallaron el 24-09
- La **version nueva de RabbitMQ en Docker Hub** cambio las reglas. Con las caracteristicas del ejemplo, `exclusive: false` y `autoDelete: false`, **la cola tiene que ser `durable: true`**. Al declararla no durable, el servidor lanzaba excepcion. El comando del curso baja siempre la ultima version de la imagen, y por eso aparecio el problema. (stated)
- El error dice que esa combinacion esta **deprecada**. Se puede configurar el servidor para aceptar las colas viejas, pero **lo desaconsejan y la van a quitar en futuras versiones**. (stated)
- **Leccion:** fijar la version de la imagen de RabbitMQ, o declarar las colas durables. La primera parte es inferencia propia (confidence: medium).

## Los ejemplos, en orden
**Pasos que se repiten siempre:** `ConnectionFactory` con usuario y password, despues la conexion, despues el canal. Un factory puede dar **varios canales**, cada uno a colas distintas. (stated)

**1. Hello, send/receive basico.**
- El receptor declara la cola durable, no exclusiva y sin autodelete, para que varios se conecten y la cola sobreviva a una caida del servidor.
- Crea un consumidor y le asigna una **funcion delegada**. En `ea.Body` llega el cuerpo; se pasa a `byte[]` y se decodifica a string.
- Simula trabajo con un **retraso de 1 segundo** por mensaje, y los procesa uno detras de otro.
- `BasicConsumeAsync` (transcripto "Basic In Sum") **es lo que pone a escuchar**. Lo anterior es preparacion. Despues, un `ReadLine` para esperar. El **ack es automatico** en este ejemplo.
- El sender lee de consola, sale con `exit`, y con vacio manda "Hello World". Codifica a bytes y hace `BasicPublishAsync` con el **exchange por defecto** y la cola `hello`.
- Se levantaron **cuatro senders**. (stated)

**2. Direct logs.**
- Exchange de tipo **direct** llamado `direct_logs`, al estilo de los logs de Linux: una linea con la severidad y el mensaje.
- El sender pide la severidad (`info`, `warning`, `error`), pasa a minusculas y **por defecto usa `info`**. En un sistema real deberia rechazar una severidad invalida.
- Publica con la **severidad como routing key**.
- El receptor declara **una cola de nombre aleatorio**, propia, y hace un **bind por cada severidad** que quiere escuchar (`QueueBindAsync`, transcripto "bind sync").
- Demo: un receptor con `info` y `warning`, otro con `warning` y `error`. Un `warning` llega a los dos. (stated)
- Uso real sugerido: **registro remoto de logs**, con distintas personas atendiendo distintos tipos de mensaje.
- Consejo de diseno: pensar estos servicios **como eventos**. Recibir el mensaje dispara una tarea que hace el trabajo y manda el resultado. (stated)

**3. Topics.**
- Supersimplificado: una raiz `app.` y debajo los niveles. Suscribirse a `app` recibe todo.
- Retomo el arbol de paises, regiones y departamentos de la clase anterior.
- Colas autogeneradas y exchange de tipo topic.
- En estos ejemplos de logs **las colas se configuran para que, si no estas conectado, el mensaje se pierda**. (stated)

## Patron request-reply con correlation id (RPC sobre MOM)
**El problema** (min ~54-67). Varias entidades de un lado y varias del otro. Muchos clientes, cada uno con muchas tareas, y cada tarea puede pedir varias cosas a la vez. Las respuestas tienen que volver **a la promesa correcta del solicitante correcto**, aunque lleguen en otro orden o las atiendan servidores distintos. El docente lo planteo como **patron general**: se ve con MOM y despues con **WebSockets**. (stated)

**La solucion:**
1. El invocador llama a `CallAsync` (transcripto "cola sync") como si fuera una funcion local, y **recibe enseguida una promesa** (`Task`).
2. El cliente RPC genera un **GUID como correlation id** y lo asocia a la promesa en un diccionario.
3. El mensaje lleva el **correlation id** y el **`ReplyTo`**, que es la **cola temporal privada** del cliente donde espera las respuestas.
4. El servidor escucha la cola `rpc_queue` (transcripto "RFQ queue"). Su delegado procesa y **responde a la cola de `ReplyTo` con el mismo correlation id**.
5. El consumidor de la cola privada del cliente lee el correlation id, busca la promesa y **le pone el resultado**. Quien hacia `await` se desbloquea.

**Por que importa: escalabilidad horizontal.** Se pueden levantar **muchos servidores escuchando la misma cola**, con la cola **no exclusiva**, y hasta dos mensajes de la misma tarea los pueden atender servidores distintos. Funciona cuando cualquier servidor puede atender cualquier pedido. (stated)

**Cuando no sirve.** Necesita un canal **bidireccional con callback**. TCP pelado y HTTP estan pensados para pedir y esperar la respuesta: el servidor no manda nada que no se haya pedido. Sirve en **WebSocket, MOM** y algunos REST con callback. (stated)

### Servidor
- Conexion, canal y la cola `rpc_queue` declarada **durable**, **no exclusiva** para que haya muchos servidores, y **sin autodelete**. (stated)
- **Hasta 10 mensajes concurrentes** sin esperar el ack. Es un **prefetch**, probablemente `BasicQosAsync` con `prefetchCount: 10`: el nombre del metodo no se escucha, correccion probable.
- El delegado saca el body, que es **un numero mandado como string**. El docente admitio que deberia ser un entero y que el programa *"viene modificado varias veces"*.
- Del mensaje toma el **correlation id** y calcula **Fibonacci recursivo**, que el docente llamo *"horrible"* a proposito: crece del orden de 2^n, asi que no hay que pasarle numeros grandes. Agrega un retardo **fuera** de la funcion recursiva, porque adentro se multiplicaria por la recursion.
- Publica la respuesta en la cola de `ReplyTo` con el **correlation id en las propiedades**.
- **Ack manual, explicito, despues de mandar la respuesta**, y no automatico como en el hello. Significa *"esto ya se proceso, no lo reintentes"*. (stated)

### Cliente (`RpcClient`)
- **`ConcurrentDictionary<string, TaskCompletionSource<string>>`** como *callback mapper*. Es seguro para muchos hilos *"en la medida que solo modificamos el diccionario"*. **Advertencia:** si una operacion toca **varios diccionarios** que tienen que quedar consistentes entre si, el lock interno no alcanza. Hace falta otro bloqueo, porque si no la ejecucion **no es equivalente a una serial**, como en Base de Datos 2. (stated)
- **`StartAsync`:** crea conexion y canal, declara la **cola temporal de respuesta** con nombre generado y guarda el nombre. Crea un consumidor que, por cada respuesta, valida el correlation id, lo busca en el diccionario, saca el `TaskCompletionSource` y le pone el resultado (`TrySetResult` o equivalente; nombre exacto TBD). Despues lo pone a escuchar.
- **`CallAsync(mensaje, cancellationToken)`:**
  - Genera el GUID, arma las propiedades con `CorrelationId` y `ReplyTo`.
  - Crea un `TaskCompletionSource` con **`TaskCreationOptions.RunContinuationsAsynchronously`** y lo registra en el diccionario.
  - Publica y **devuelve `tcs.Task`**.
- **El truco que remarco:** `CallAsync` **no se declara `async`**. Si fuera `async`, el compilador construiria la `Task` del retorno, pero aca **se arma a mano** con el `TaskCompletionSource` y se completa despues, cuando llega la respuesta. *"Si ponen async eso les va a fallar"*. *"Es un truco, porque originalmente la programacion asincronica de C# no fue pensada para estas cosas"*. (stated)
- **El `CancellationToken` esta en la firma pero no esta implementado.** Para cancelar haria falta **otro mensaje al servidor** con el GUID, que dispare del lado del servidor otro token que cancele la tarea. Dijo que lo implementa la proxima vez. **Es el mismo problema que el requerimiento 5 del Obligatorio 2.** (stated)
- `DisposeAsync` cierra conexion y canal *"para que sea mas prolijo"*.
- **Uso desde la aplicacion:**
  - `new RpcClient()`, despues `StartAsync`.
  - Cuatro numeros aleatorios entre 3 y 30, y cuatro `CallAsync` **sin `await`**, guardando las tareas.
  - Hace otra cosa y recien despues hace `await` de cada resultado.
  - Para el cliente es *"supersencillo"*. (stated)
- **Demo:** dos servidores y dos clientes con cuatro pedidos cada uno. Los pedidos **se reparten entre los dos servidores** y cada cliente recibe sus propios resultados. (stated)
- El docente dijo *"todavia no vimos RPC, lo vamos a ver en otro tema; la idea es parecida, pero es otra cosa"*. Ver [[Tema 5 - RPC y gRPC]]. (stated)

## Avisos
- El docente iba a mandar **los codigos de esta clase** por Teams (*"les mandamos igual los codigos hoy"*). **No estan en la boveda** (as of 2026-10-13).
- La hoja de requerimientos del Obligatorio 2 se sube a Teams. Ya esta ingerida.
- Proxima clase: Docker y devolucion de defensas.

## Salvedades de la transcripcion
- **Hueco** entre los minutos ~3 y ~8.
- Correcciones probables:
  - `CallAsync` (transcripto "cola sync")
  - `BasicConsumeAsync` ("Basic In Sum")
  - `QueueBindAsync` ("bind sync")
  - `rpc_queue` ("RFQ queue")
  - JSON ("Jackson")
  - `BasicQosAsync` con prefetch 10 (no se escucha el nombre)
- En el minuto ~82 la transcripcion dice que el cliente *"se conecta con Kubernetes"*. Casi seguro es **RabbitMQ**: no hay Kubernetes en el curso.
- Hay un **pasaje de lista** en el minuto ~64. No se registran los nombres.
- Se escucha *"devolucion sobre las defensas"* y *"estuvimos demasiado bien"*, en un intercambio confuso. **No se puede deducir la nota de ningun grupo.**

## Connections
- [[Obligatorio 2 - Upgrade asincrono y Docker de EDISON]] - la consigna
- [[Tema 4 - Colas de mensajes y MOM]] · [[Tema 5 - RPC y gRPC]] · [[Tema 3 - Asincronia y paralelismo]] · [[Complementario - Docker y Cloud]]
- [[Clase 2026-09-24 - Middleware orientado a mensajes, RabbitMQ y AMQP]] - la teoria de esta practica
- [[Clase 2026-09-22 - Parallel, CancellationToken y cuando usar async]] - la cancelacion que pide el requerimiento 5
- [[Programacion de Redes 2026]] · [[Enrique Latorres]]
