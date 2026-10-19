# TEMA 0 — Fundamentos previos a la Clase 1 (PDR)

**Qué es esto:** a diferencia de los seis temas vigentes, este Tema 0 **no sale de ningún PDF de Aulas**. Es una nivelación de conceptos de base (sistemas operativos y redes) que la materia da por sabidos desde el primer día. Todo el contenido es **[Complementario — Claude]** salvo que se indique lo contrario.

**Por qué existe:** pediste un repaso completo antes de la primera clase de threads, semáforos y la infraestructura de redes básica. Esto es la base sobre la que se apoya *todo* lo demás: Programación Multihilo (Tema 1), Sockets, colas, RPC/gRPC y REST.

**Cómo usar este tema:** leelo una vez de punta a punta antes de la primera clase. Después, cuando llegues al Tema 2 (Threads y Concurrencia oficial) y a Sockets, vas a reconocer el vocabulario en vez de escucharlo por primera vez.

---

## 1. Programa, proceso e hilo — la base de todo

**Qué es:** tres palabras que se usan todo el semestre y que se confunden fácil.

**Por qué importa:** `Thread` es el núcleo del Tema 1. Si no tenés clara la diferencia entre proceso e hilo, todo ese tema se hace cuesta arriba.

**Cómo funciona:**

| Concepto | Qué es | Analogía |
|---|---|---|
| **Programa** | Un archivo en el disco con instrucciones (el .exe, el código compilado). No está "corriendo" todavía. | Una receta de cocina escrita en un papel. |
| **Proceso** | Un programa que se está ejecutando. Tiene su propia memoria, sus propios recursos (archivos abiertos, conexiones de red), aislado de otros procesos. | Un cocinero que agarra la receta y se pone a cocinar en **su propia cocina**, con **sus propios** ingredientes y utensilios. |
| **Hilo (Thread)** | Una "línea de ejecución" dentro de un proceso. Un proceso puede tener 1 o varios hilos, y todos comparten la misma memoria y los mismos recursos del proceso. | Varios ayudantes de cocina trabajando **en la misma cocina**, compartiendo la heladera, la mesada y los mismos ingredientes. |

> **[Complementario — Claude]** Por qué importa que los hilos compartan memoria: es rápido y eficiente (no hay que duplicar nada), pero es también la fuente de *todos* los problemas que vas a estudiar en el Tema 1. Si dos ayudantes de cocina agarran el mismo frasco de sal al mismo tiempo, se puede armar quilombo. Eso es exactamente lo que un **lock**, un **semáforo** o un **monitor** vienen a resolver.

### Concurrencia vs. Paralelismo (aclaración clásica, cita Rob Pike — aparece también en el material oficial del Tema 3)

- **Concurrencia:** manejar varias tareas al mismo tiempo, aunque no se ejecuten literalmente en el mismo instante (se van turnando muy rápido). Es una forma de **estructurar** el problema.
- **Paralelismo:** ejecutar varias tareas literalmente al mismo tiempo, en núcleos de CPU distintos. Es una forma de **ejecutar**.

> **[Complementario — Claude]** Un solo cocinero que corta la cebolla un rato, revuelve la olla un rato, y vuelve a la cebolla, es **concurrencia** (una sola persona, tareas intercaladas). Dos cocineros, cada uno en su propia hornalla, cocinando literalmente al mismo tiempo, es **paralelismo**. Con 1 solo núcleo de CPU podés tener concurrencia sin paralelismo. Con varios núcleos, podés tener las dos cosas juntas.

### Checkpoint 1
- ¿Cuál es la diferencia entre un programa y un proceso?
- ¿Por qué los hilos de un mismo proceso comparten memoria y los procesos entre sí no?
- Dame un ejemplo de concurrencia sin paralelismo.

---

## 2. El problema de fondo: la condición de carrera (race condition)

**Qué es:** lo que pasa cuando dos o más hilos acceden y modifican el mismo dato compartido al mismo tiempo, sin ningún tipo de coordinación.

**Por qué importa:** es *el* problema que todo el Tema 1 (locks, semáforos, monitores, eventos) existe para resolver. Si entendés bien este problema, entendés para qué sirve cada herramienta.

**Cómo funciona — ejemplo clásico, la cuenta bancaria:**

Imaginate una variable `saldo = 100`. Dos hilos quieren depositar $50 cada uno al mismo tiempo. Internamente, "depositar" no es una operación atómica (de un solo paso): son 3 pasos:

1. Leer el saldo actual (`100`).
2. Sumarle el depósito (`100 + 50 = 150`).
3. Guardar el nuevo saldo (`saldo = 150`).

Si los dos hilos hacen el paso 1 (leer `100`) **antes** de que cualquiera de los dos llegue al paso 3, los dos calculan `150` por separado, y el segundo en escribir "pisa" al primero. Resultado: el saldo termina en `150`, cuando debería terminar en `200`. Se perdió un depósito entero.

> **[Complementario — Claude]** Esto se llama **race condition** porque el resultado final depende de quién "gana la carrera" — el orden exacto en el que el sistema operativo decide turnar a los hilos, que es básicamente impredecible. El bug más peligroso de la concurrencia es que a veces funciona bien (por suerte, no se pisan) y a veces no — son bugs intermitentes, difíciles de reproducir y de detectar en testing.

**La solución general:** identificar la "zona peligrosa" del código (la que lee-modifica-escribe un dato compartido) — se llama **sección crítica** — y asegurarse de que **un solo hilo a la vez** pueda estar ejecutando esa sección. Eso es **mutua exclusión** (mutex viene de "mutual exclusion").

### Checkpoint 2
- ¿Por qué "depositar $50" no es una operación atómica?
- ¿Qué es una sección crítica?
- ¿Qué significa "mutua exclusión"?

---

## 3. Herramientas de sincronización (vista panorámica, se profundiza en Tema 2 oficial)

**Qué es:** los mecanismos que ofrece C#/.NET para resolver el problema de la sección crítica. Acá va la intuición de cada uno; el detalle técnico con código real lo da el Tema 1 (`2-Threads.pdf` + guías).

### Lock / Mutex — "un solo pase para entrar al baño"

Un `lock` (en .NET, basado en `Monitor` por debajo) es como el pestillo de la puerta de un baño de un solo inodoro: el primero que llega, entra y traba la puerta. Cualquiera que llegue después tiene que **esperar en la fila** hasta que el de adentro salga y destrabe.

- Solo **un hilo a la vez** puede estar "adentro" (ejecutando la sección crítica).
- Si otro hilo intenta entrar, se **bloquea** (espera) hasta que se libere.
- Un **Mutex** es una versión más pesada de esto, que además puede compartirse **entre procesos distintos** (no solo entre hilos del mismo proceso) — como un baño público compartido por varios edificios en vez de uno solo.

### Semáforo — "un estacionamiento con N lugares"

Un semáforo generaliza la idea del lock: en vez de dejar pasar a **uno solo**, deja pasar hasta a **N** hilos al mismo tiempo. Es como un estacionamiento con una barrera automática y un contador de lugares libres:

- Cada vez que un auto entra, el contador baja 1.
- Si el contador llega a 0, la barrera no deja pasar a nadie más — hay que esperar a que salga un auto.
- Cada vez que un auto sale, el contador sube 1 y se libera un lugar.

Un **semáforo binario** (N=1) se comporta igual que un lock (un solo lugar). Un **semáforo contador** (N > 1) sirve para limitar cuántos hilos pueden usar un recurso limitado en simultáneo (por ejemplo, "máximo 5 conexiones a la base de datos al mismo tiempo").

### Monitor — "el lock, con timbre para avisar cuándo seguir"

Un **Monitor** (en C#, la clase `Monitor`, que es lo que usa `lock` por debajo) agrega algo más que el lock básico: además de la mutua exclusión, permite que un hilo se quede **esperando una condición** (`Wait`) y que otro hilo lo **despierte** cuando esa condición se cumple (`Pulse` / `PulseAll`).

> **[Complementario — Claude]** Es como el pestillo del baño, pero con un timbre: si entrás y te das cuenta de que falta el jabón (`Wait`), tocás el timbre y te quedás esperando pacientemente afuera, cediendo el paso; cuando alguien repone el jabón, aprieta un botón (`Pulse`) que te avisa que podés volver a intentar entrar.

### Eventos: `ManualResetEvent` y `AutoResetEvent` — "el semáforo de tránsito"

Estos no protegen una sección crítica: sirven para que un hilo le **avise a otro(s)** que "ya puede continuar", como una luz de semáforo de tránsito.

- **`ManualResetEvent`**: como un semáforo en verde que se queda en verde hasta que alguien lo pone en rojo a mano. Mientras está en verde, **todos** los hilos que estén esperando pasan libremente.
- **`AutoResetEvent`**: como un molinete de subte — cada vez que "abre" (se señaliza), deja pasar **a un solo hilo**, y automáticamente se vuelve a cerrar solo, sin que nadie tenga que hacer nada.

### Tabla resumen

| Herramienta | Analogía | Para qué sirve |
|---|---|---|
| `lock` / `Monitor` | Pestillo de baño de 1 inodoro, con timbre | Mutua exclusión simple dentro de un mismo proceso, con posibilidad de esperar condiciones |
| `Mutex` | Pestillo de baño compartido entre edificios | Mutua exclusión entre procesos distintos |
| `Semaphore` | Estacionamiento con N lugares | Limitar cuántos hilos usan un recurso al mismo tiempo |
| `ManualResetEvent` | Semáforo de tránsito en verde fijo | Avisar a **todos** los hilos en espera que pueden continuar |
| `AutoResetEvent` | Molinete de subte | Avisar a **un solo** hilo en espera que puede continuar, y se vuelve a cerrar solo |

> Esta tabla es intuición para arrancar la materia con vocabulario claro. El comportamiento exacto, la sintaxis en C# y los casos de uso reales los da `2-Threads.pdf` y las 3 guías oficiales de `THREADS/` — eso se procesa a fondo en el Tema 2.

### Checkpoint 3
- ¿En qué se diferencia un semáforo binario de un lock?
- ¿Para qué sirve un semáforo contador (N > 1)?
- ¿Cuál es la diferencia entre `ManualResetEvent` y `AutoResetEvent`?

---

## 4. Infraestructura de redes: lo mínimo para entender Sockets

**Qué es:** el vocabulario de redes que la materia asume que ya sabés antes de llegar a la Unidad de Sockets.

**Por qué importa:** todo el resto de la materia (Sockets, MOM, RPC, REST) es "cómo hacer que programas en máquinas distintas se hablen" — y para eso hace falta entender cómo se identifican y se comunican esas máquinas.

### Dirección IP y puerto — "la calle y el número de puerta"

- **Dirección IP:** identifica a **una máquina** dentro de una red (como la dirección de una casa: calle y número). Ejemplo: `192.168.1.10`.
- **Puerto:** identifica **qué aplicación específica**, dentro de esa máquina, debe recibir los datos (como el número de departamento dentro de un edificio). Va de 0 a 65535. Ejemplo: el puerto 80 es "la puerta" que normalmente usan los servidores web.

> **[Complementario — Claude]** Una dirección IP sin puerto es como decirle al cartero "andá a tal calle" sin decirle a qué departamento entregar la carta — la casa (máquina) puede tener muchos "inquilinos" (aplicaciones) escuchando en puertos distintos al mismo tiempo: el navegador en uno, el cliente de mail en otro, el juego online en otro.

### TCP vs. UDP — "carta certificada vs. postal"

Son los dos protocolos de transporte más comunes, construidos sobre IP:

| | TCP | UDP |
|---|---|---|
| **Analogía** | Carta certificada: confirmación de entrega, orden garantizado | Postal: la mandás y listo, sin garantía de que llegue ni en qué orden |
| **Confiabilidad** | Garantiza que los datos lleguen, completos y en orden (retransmite si algo se pierde) | No garantiza nada — puede perderse o llegar desordenado |
| **Velocidad** | Más lento (por todo el control que hace) | Más rápido (sin overhead de confirmaciones) |
| **Uso típico** | Web (HTTP), email, transferencia de archivos — todo lo que necesita integridad | Streaming de video/audio en vivo, juegos online — donde perder un paquete es mejor que esperar a que se reenvíe |
| **Relación con la materia** | `TcpClient` / `TcpListener` (Unidad Sockets) usan TCP | No es el foco central de la materia, pero conviene saber que existe |

### ¿Qué es un Socket?

Un **socket** es el "enchufe" que usa un programa para conectarse a la red: es la combinación de una dirección IP + un puerto + un protocolo (TCP o UDP), que le permite a una aplicación enviar y recibir datos a través de la red como si fuera un archivo (de hecho, en la Unidad 1 viste que `NetworkStream` hereda de `Stream`).

> **[Complementario — Claude]** Pensá en el socket como el enchufe físico de un teléfono de línea: para que dos personas hablen, cada una necesita un aparato conectado a un enchufe (el socket), identificado por un número (el puerto), dentro de una casa (la IP). La "llamada telefónica" en sí (la conversación) es la conexión TCP.

### Cliente y servidor, en términos de sockets

- El **servidor** hace: `Bind` (elige un puerto y "se plántale ahí"), `Listen` (se queda esperando conexiones entrantes), `Accept` (acepta una conexión de un cliente).
- El **cliente** hace: `Connect` (se conecta a la IP y puerto del servidor).
- Una vez conectados, ambos lados pueden `Send` (enviar) y `Receive` (recibir) datos por ese canal.

> Esto es solo el mapa mental — la sintaxis exacta en C# con `Socket`, `TcpClient` y `TcpListener` la da la sección oficial de Sockets (`5-Sockets.pdf` + las 3 guías), que todavía no tiene su propio Tema en `ESTUDIO/`.

### Capas (versión mínima, sin entrar en el modelo OSI completo)

Para esta materia alcanza con este mapa simplificado — el programa oficial no exige el modelo OSI de 7 capas, pero ayuda a ubicar dónde vive cada cosa:

| Capa (simplificada) | Qué hace | Ejemplos en la materia |
|---|---|---|
| Aplicación | El "idioma" que hablan los programas entre sí | HTTP (REST), AMQP (RabbitMQ), gRPC |
| Transporte | Cómo se garantiza (o no) la entrega de los datos | TCP, UDP |
| Red | Cómo se identifican las máquinas y se rutean los paquetes | IP |
| Física / Enlace | El cable, el wifi, la infraestructura física | (fuera del alcance de la materia) |

> **[Complementario — Claude]** Analogía completa de "carta": la capa de Aplicación es el idioma en que está escrita la carta (español, inglés). La de Transporte es si mandás carta certificada o postal (TCP/UDP). La de Red es el sistema de código postal que permite que el correo sepa a qué ciudad y barrio llevarla (IP). La Física es el camión que efectivamente transporta la carta.

### Checkpoint 4
- ¿Por qué hace falta un puerto además de una IP?
- ¿Cuándo usarías UDP en vez de TCP?
- ¿Qué hace un servidor de sockets antes de poder aceptar conexiones (en orden)?

---

## Resumen ejecutivo del Tema 0

1. Programa (archivo quieto) → Proceso (programa corriendo, memoria propia) → Hilo (línea de ejecución dentro de un proceso, memoria compartida). Los hilos comparten memoria: rápido, pero peligroso.
2. Concurrencia es estructurar tareas intercaladas; paralelismo es ejecutarlas literalmente al mismo tiempo en núcleos distintos.
3. El problema central del Tema 1 es la **condición de carrera**: dos hilos leen-modifican-escriben el mismo dato sin coordinación y se pisan. La solución es la **mutua exclusión** de la **sección crítica**.
4. Las herramientas de sincronización son variaciones sobre la misma idea: `lock`/`Monitor` (un pase), `Mutex` (un pase entre procesos), `Semaphore` (N pases), `ManualResetEvent`/`AutoResetEvent` (avisar que se puede continuar, a todos o a uno solo).
5. Una **IP** identifica la máquina, un **puerto** identifica la aplicación dentro de esa máquina. **TCP** garantiza entrega y orden (más lento); **UDP** no garantiza nada (más rápido). Un **socket** es la combinación de IP + puerto + protocolo que usa un programa para hablar por red.
6. El servidor hace `Bind` → `Listen` → `Accept`; el cliente hace `Connect`; después ambos `Send`/`Receive`.

Con esta base ya podés entrar a la Clase 1 reconociendo el vocabulario, en vez de escucharlo por primera vez.
