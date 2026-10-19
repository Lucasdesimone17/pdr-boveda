# Preguntas de autoevaluación — Sockets: Comunicación en Red (PDR)

Fuente: 🟢 `5-Sockets.pdf` + 3 guías oficiales de `SOCKETS/`. Intentá responder antes de abrir la pista.

## Endpoint y direccionamiento

**1.** ¿Qué es un endpoint y por qué hacen falta 5 datos (no solo 2) para comunicarse con Sockets?

<details><summary>Pista</summary>Endpoint = IP + puerto. Pero para comunicarte necesitás esa info de los dos lados, más el protocolo.</details>

**2.** ¿Por qué el puerto 0 es útil en el cliente pero una mala idea en el servidor?

<details><summary>Pista</summary>Pensá quién necesita saber de antemano a qué puerto conectarse.</details>

## TCP vs UDP

**3.** Dado un escenario de videollamada en vivo y otro de descarga de un instalador de software, ¿qué protocolo (TCP o UDP) usarías en cada caso y por qué?

<details><summary>Pista</summary>Uno prioriza que no se pierda nada (confiabilidad); el otro prioriza velocidad y tolera perder algún paquete.</details>

## Socket de bajo nivel

**4.** Ordená correctamente los pasos del lado servidor para aceptar una conexión: `Accept()`, `Bind()`, `Listen()`, crear el `Socket`.

<details><summary>Pista</summary>Primero hay que existir, después "anotar la dirección", después "abrir la puerta", después "esperar a que golpeen".</details>

**5.** ¿Qué significa realmente el número que le pasás a `socket.Listen(n)`? ¿Es la cantidad máxima de clientes que el servidor puede atender en total?

<details><summary>Pista</summary>Es una cola de espera (backlog), no un límite total de conexiones históricas.</details>

**6.** ¿Por qué el código de ejemplo con un solo `Accept()` (sin bucle) solo puede atender a un cliente, y qué dos cambios hacen falta para atender a varios?

<details><summary>Pista</summary>Un bucle alrededor del Accept, y algo por cliente para no bloquear a los demás mientras se atiende a uno.</details>

## Envío y recepción de datos

**7.** Un servidor tiene un buffer de recepción de 8 bytes y el cliente manda un mensaje de 9 bytes. ¿Qué va a pasar exactamente, según lo que muestra la guía?

<details><summary>Pista</summary>El mensaje llega partido en dos lecturas separadas del `while`, no se pierde nada pero tampoco llega junto.</details>

**8.** Explicá el protocolo propio de "largo fijo + mensaje variable": ¿por qué la parte del largo tiene que ser siempre del mismo tamaño en bytes?

<details><summary>Pista</summary>Si el receptor no supiera cuántos bytes leer para el largo, tendría el mismo problema que quiere resolver.</details>

**9.** Según la documentación citada, `Send` puede completar "exitosamente" aunque no haya mandado todos los bytes pedidos. ¿Qué tiene que hacer la aplicación para garantizar que se mande todo el mensaje?

<details><summary>Pista</summary>Reintentar en un bucle, llevando la cuenta de cuánto se mandó (offset) hasta llegar al tamaño total.</details>

**10.** ¿Por qué `Receive` puede tirar una `SocketException` incluso antes de haber recibido nunca ningún dato del cliente?

<details><summary>Pista</summary>El cliente puede cerrarse/desconectarse en cualquier momento, incluso antes de mandar el primer mensaje.</details>

## TcpListener / TcpClient / NetworkStream

**11.** ¿Cuál es la diferencia de propósito entre `TcpListener` y `TcpClient`? ¿Por qué no es correcto decir que son intercambiables?

<details><summary>Pista</summary>Uno solo escucha por conexiones nuevas; el otro establece y administra una conexión ya sea de cliente o del lado servidor.</details>

**12.** Con `NetworkStream`, ¿hace falta un `while` manual para el envío (`Write`) igual que para la recepción (`Read`)? Justificá la diferencia.

<details><summary>Pista</summary>Una de las dos operaciones ya se encarga sola de mandar todo; la otra sigue dependiendo de que vos controles cuánto llegó.</details>

---

## Respuestas completas

<details>
<summary>Ver todas las respuestas</summary>

**1.** Un endpoint es la combinación de una dirección IP (identifica la máquina) y un número de puerto (identifica el proceso dentro de esa máquina). Para establecer una comunicación con Sockets hacen falta 5 datos: IP remota, puerto remoto, IP local, puerto local, y el protocolo de red (TCP o UDP) — porque cada extremo de la conexión (local y remoto) necesita su propio endpoint completo, y además hay que definir con qué protocolo se va a hablar.

**2.** El puerto 0 le dice al sistema "asigname cualquier puerto disponible". En el cliente esto es útil porque, si hay varios clientes corriendo en la misma máquina/IP, cada uno necesita un puerto distinto para que el servidor pueda diferenciarlos, y no importa cuál sea ese puerto exacto. En el servidor sería un problema, porque los clientes necesitan saber de antemano a qué puerto conectarse — si el servidor usara un puerto asignado dinámicamente, nadie sabría con certeza a qué puerto dirigirse antes de ejecutar el programa.

**3.** Para la videollamada en vivo conviene UDP: prioriza la velocidad y la baja latencia, y tolera perder algún paquete (un pequeño corte de audio/video es aceptable) antes que esperar reenvíos que generarían más demora. Para la descarga de un instalador conviene TCP: necesita que todos los bytes lleguen completos y en orden (mensajes confiables), porque un instalador corrupto o incompleto no sirve — acá la velocidad es secundaria frente a la integridad de los datos.

**4.** El orden correcto es: crear el `Socket` → `Bind()` (enlazar a una dirección y puerto) → `Listen()` (pasar a modo escucha) → `Accept()` (esperar y aceptar la conexión entrante). Primero el socket tiene que existir, después "anotar su dirección" (Bind), después habilitarlo para recibir pedidos (Listen), y recién ahí puede quedar esperando una conexión concreta (Accept).

**5.** No es la cantidad máxima de clientes que el servidor puede atender en total. El parámetro de `Listen(n)` es el tamaño del **backlog**: cuántos clientes pueden quedar en cola de espera sin que el servidor todavía haya ejecutado `Accept()` sobre ellos. Una vez que un cliente es aceptado, sale de esa cola y libera un lugar para que otro cliente nuevo pueda esperar. Si se intentan conectar más clientes de los que el backlog permite antes de que el servidor los acepte, las conexiones adicionales fallan hasta que se libere espacio.

**6.** Con un solo `Accept()` sin bucle, el programa ejecuta esa línea una única vez, acepta al primer cliente que llega, y después sigue con el resto del código sin volver a esperar más conexiones — por eso solo atiende a uno. Para atender a varios clientes hacen falta dos cambios: (1) envolver el `Accept()` en un bucle (`while(true)`) para que, apenas se acepta un cliente, el servidor vuelva a quedar esperando el próximo; y (2) lanzar un `Thread` nuevo por cada cliente aceptado, para que la comunicación con un cliente en particular no bloquee ni retrase la atención de los demás.

**7.** El servidor va a recibir el mensaje partido en dos lecturas distintas del bucle `while`. En la primera vuelta, `Receive` llena el buffer de 8 bytes con los primeros 8 caracteres del mensaje ("12345678") y deja de leer ahí porque el buffer se llenó — no importa que el mensaje real tuviera un byte más. En la siguiente iteración del `while`, el `Receive` vuelve a ejecutarse y recibe el byte restante ("9") como si fuera un mensaje aparte. El mensaje no se pierde, pero llega fragmentado en dos recepciones en vez de una sola.

**8.** La parte del largo tiene que tener siempre el mismo tamaño en bytes (por ejemplo 4 bytes, porque un `int` en C# ocupa 32 bits) para que el receptor sepa, sin ninguna ambigüedad, exactamente cuántos bytes tiene que leer para obtener el número que indica el largo del mensaje real. Si el tamaño de esa parte fuera variable, el receptor tendría el mismo problema original: no sabría dónde termina esa primera parte y empieza la siguiente, y el protocolo dejaría de resolver nada.

**9.** La aplicación tiene que reintentar el envío en un bucle, llevando un contador (`offset`) de cuántos bytes ya se enviaron exitosamente. En cada vuelta del bucle se llama a `Send` pasando el buffer, el offset actual, y la cantidad de bytes que todavía faltan enviar (`size - offset`); el valor que devuelve `Send` (cuántos bytes realmente mandó esa vez) se suma al offset. El bucle continúa hasta que `offset` llegue al tamaño total del mensaje, garantizando así que se hayan enviado todos los bytes, incluso si `Send` los mandó de a partes en distintas llamadas.

**10.** Porque la conexión puede cerrarse o interrumpirse en cualquier momento, incluyendo antes de que el cliente haya llegado a enviar ningún dato — por ejemplo, si el cliente se desconecta abruptamente o cierra el programa apenas después de conectarse. En ese caso, el intento de `Receive` del lado servidor no encuentra una conexión válida para leer y lanza una `SocketException` directamente, en vez de simplemente devolver 0 bytes recibidos (que es el comportamiento cuando la conexión se cierra de forma "prolija" después de haber intercambiado datos). Por eso hace falta un `try/catch` además de chequear si el valor devuelto es 0.

**11.** `TcpListener` tiene un único propósito: escuchar por conexiones TCP entrantes y aceptarlas — no se usa para enviar ni recibir datos una vez que la conexión ya está establecida. `TcpClient`, en cambio, se usa tanto del lado del cliente (para conectarse a un servidor) como del lado del servidor una vez que `TcpListener` ya aceptó una conexión (`AcceptTcpClient()` devuelve justamente un `TcpClient` para trabajar con ese cliente en particular). No son intercambiables porque cada uno resuelve una etapa distinta del proceso: `TcpListener` resuelve "quién se quiere conectar", y `TcpClient` resuelve "cómo hablo con quien ya se conectó" — en un servidor típico se usan los dos juntos, cada uno en su rol.

**12.** No hace falta un `while` manual para el envío con `NetworkStream.Write`, porque el propio `NetworkStream` ya se encarga internamente de garantizar que se manden todos los bytes solicitados en una sola llamada. Para la recepción con `NetworkStream.Read`, en cambio, sigue haciendo falta un `while` manual, igual que con `Socket.Receive`: `Read` puede devolver menos bytes de los pedidos en una sola llamada, así que la aplicación tiene que seguir llamando a `Read` y acumulando en un `offset` hasta completar la cantidad total esperada. La asimetría está documentada explícitamente en la guía oficial de TcpListener/TcpClient.

</details>
