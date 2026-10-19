# TEMA 1 — Introducción (PDR)

**Cobertura oficial (programa):** "Introducción: Threads. Streams de datos. Patrones de diseño orientados al procesamiento paralelo."

**Fuentes:**
- Oficial: `../../TEORICO/01_INTRODUCCION/Introducción.pdf` — diapositivas de clase, ciclo 2025.
- Oficial: `../../TEORICO/01_INTRODUCCION/4-Stream.pdf` — diapositivas de clase sobre Streams.
- Oficial: `../../TEORICO/01_INTRODUCCION/1-Introduccion-2026.pdf` — diapositivas de clase, cohorte marzo 2026 (equipo docente Soria/Bentos/Latorres/Pucciarelli/Hirshfield/Chiarino/Monjardín/Martinez). **Importante:** las fechas de evaluación de esta diapositiva (entregas 23/04, 26/05, 22/06, parcial 29/06, exoneración ≥86, derecho a examen ≥70) corresponden al ciclo **marzo 2026**, no al ciclo agosto 2026 en curso — se usan acá solo el contenido conceptual y el temario, no las fechas. Los umbrales de 86/70 puntos quedan como **"confirmado para marzo 2026, pendiente de verificar si aplica igual a agosto 2026"** — no se vuelcan a `CARATULA.md` hasta confirmarlo.
- Complementaria (no oficial): `../../TEORICO/_PROGRAMA_Y_RESUMENES/0.RESUMEN-STUDOCU.pdf` — apuntes de Ricardo Rolín, 2º semestre 2025, cátedra de Sebastián Meljem y Tomás Clavijo. Se usa solo donde agrega algo que las diapositivas oficiales no cubren (ejemplo Netflix).

Todo lo que sigue viene de estas fuentes. Donde agrego una explicación adicional para que se entienda mejor, lo marco como **[Complementario — Claude]**. Donde algo es una interpretación mía de cómo se conecta con el resto del programa (no dicho así en ningún documento), lo marco como **[Interpretación]**.

---

## 1. Por qué existe esta materia: de las mainframes a la nube

**Qué es:** un repaso de cómo llegamos a necesitar "programación de redes" como disciplina propia.

**Por qué importa:** todo lo que vas a estudiar en las unidades siguientes (threads, middleware, RPC, REST, cloud) son respuestas a un mismo problema que fue creciendo con el tiempo: hacer que programas que corren en máquinas distintas trabajen juntos, cada vez a mayor escala.

**Cómo fue evolucionando (línea de tiempo oficial, 1960→2000):**

| Era | Características |
|---|---|
| **Mainframes** (desde los 60) | Computadoras del tamaño de una habitación. Terminales "tontas" conectadas a un mainframe central. Aplicaciones monolíticas, soluciones 100% propietarias (IBM, etc.), sin interoperabilidad entre fabricantes. Todo el procesamiento centralizado. |
| **PC** | Computadoras personales, de tamaño reducido y más baratas. Aparecen las redes LAN Ethernet y la estandarización de protocolos de red. Sistemas heterogéneos: una empresa con varias computadoras y software de distintos proveedores. |
| **Aplicaciones distribuidas / Cloud computing** | Entidades lógicas autónomas que trabajan juntas en red para cumplir una tarea, aunque estén en máquinas distintas. Aparece la era de los "Servicios en la Nube". |

> **[Complementario — Claude]** Pensalo como la diferencia entre tener un único empleado que hace todo el trabajo de una empresa (mainframe) versus tener una empresa con sucursales que se comunican entre sí por teléfono y correo (sistemas distribuidos). A medida que la empresa crece, no alcanza con un empleado: hace falta coordinar equipos, y ahí aparecen los "protocolos" (las reglas de cómo se comunican las sucursales).

### Sistemas Distribuidos: definición oficial

Es la forma **genérica** de definir este tipo de arquitectura: son entidades lógicas autónomas que trabajan juntas en una red para cumplir una tarea. Las partes del sistema pueden estar en diferentes máquinas, pero colaboran como si fueran una sola entidad.

### Autoevaluación rápida
- ¿Qué diferencia a un sistema distribuido de una aplicación monolítica en términos de autonomía e independencia de ubicación?

---

## 2. Modelo Cliente-Servidor y modelos de capas

**Qué es:** formas de organizar dónde vive la lógica de una aplicación (interfaz, lógica de negocio, datos).

**Por qué importa:** determina qué tan bien escala tu sistema y qué tan fácil es mantenerlo.

### Modelo Cliente-Servidor

Es una tecnología particular **dentro** de los sistemas distribuidos, no un sinónimo de ellos:
- Servidor: brinda el servicio. Cliente: consume el servicio.
- Relación 1 a muchos.
- Protocolos asimétricos.
- Independencia de ubicación: cliente y servidor pueden residir en el mismo host.

### Modelo de 2 capas
- La lógica de la aplicación puede estar en el cliente, en el servidor, o en ambos.
- Es sencilla su implementación y desarrollo.
- Ideal para sistemas pequeños (según la diapositiva 2025, "menores a 10 usuarios").
- **No escala bien.**

### Modelo de 3 capas
- La lógica se concentra en el servidor de aplicaciones; el cliente es liviano.
- Independencia entre lógica y datos.
- Desarrollo inicial más complejo, requiere herramientas de desarrollo.
- Escala bien: es sencillo agregar nuevos módulos.

### Modelo de N capas
- Generalización del modelo de 3 capas cuando la capa lógica está conformada por "n" componentes lógicos.
- Cada componente realiza una función específica del negocio y los componentes interactúan entre ellos.
- Permite desarrollo gradual en pasos pequeños y reúso de componentes.
- Requiere frameworks complejos de desarrollo.

### Comparación de los tres modelos (diapositiva 2026)

| | 2 Capas | 3 Capas | N Capas |
|---|---|---|---|
| Complejidad | Baja | Media | Alta |
| Escalabilidad | Limitada | Buena | Excelente |
| Mantenimiento | Difícil | Moderado | Fácil |
| Caso de uso | Apps simples | Web apps | Sistemas empresariales |

> **[Complementario — Claude]** Analogía: 2 capas es como un mostrador de atención al cliente donde la misma persona atiende, resuelve y factura. 3 capas es como separar "recepción" (cliente liviano), "gestión" (servidor con la lógica) y "archivo" (base de datos). N capas es cuando "gestión" se divide en varios departamentos especializados (facturación, stock, envíos), cada uno con su propia responsabilidad.

### Modelos según la escala

| Escala | Cómo se organiza |
|---|---|
| **Sistema básico** (1 cliente) | Servidor, middleware y cliente corren en la misma máquina. Se podría diseñar en forma monolítica (desarrollo rápido, no escala). No requiere herramientas complejas. Ejemplo oficial: PHP + Laravel/Codeigniter. |
| **Sistema empresarial** (~100 clientes) | Servidor en máquinas dedicadas, acceso humano solo para administración. Cliente en máquinas de menor poder de cómputo, manejadas por los usuarios. Si hay más de un servidor, por lo general no interactúan entre sí. |
| **Sistema masivo** (miles de clientes) | Servidor en máquinas dedicadas, arquitecturas distribuidas con tendencia a dividir responsabilidades en módulos específicos (microservicios). Las aplicaciones servidoras son híbridas: actúan como servidoras para los clientes y como clientes de otros servidores. Diseño escalable desde el inicio. |
| **Sistema masivo en la nube** | Aplicación servidora corre en infraestructura cloud bajo el modelo IaaS (Infrastructure as a Service); los recursos (VMs, storage, redes) son provistos y gestionados por el proveedor. Escalado dinámico según demanda. Gestión de infraestructura (redes, hardware, a veces el SO) a cargo del proveedor, liberando al equipo de desarrollo para enfocarse en la lógica de negocio. Cliente: corre en máquinas de los usuarios, se comunica vía APIs y protocolos estándar de Internet. |

### Autoevaluación rápida
- ¿Por qué el modelo de 3 capas escala mejor que el de 2 capas?
- ¿Qué significa que una aplicación servidora sea "híbrida" en un sistema masivo?
- (Pregunta de discusión oficial) Una app web de e-commerce con miles de usuarios concurrentes: ¿qué modelo elegirías — 2, 3 o N capas — y por qué?

---

## 3. Bloques fundamentales: Cliente, Servidor y Middleware

**Qué es:** los tres componentes lógicos presentes en cualquier sistema distribuido.

**Por qué importa:** es el vocabulario base que se usa en el resto de la materia para describir cualquier arquitectura (sockets, MOM, RPC, REST son todos formas distintas de implementar el bloque "Middleware").

### Cliente
Formado por: software que implementa la aplicación cliente (ej. browser, app propietaria, cliente de BD), el stack de protocolos de red, la parte cliente del middleware, sistema operativo y hardware adecuado.

### Servidor
Formado por: software que implementa los servicios (motor de BD, PHP, servidor web/JBoss), sistema operativo, stack de protocolos de red, la parte servidora del middleware, y hardware adecuado para la solución.

### Red o Middleware
Es todo lo que existe entre medio de las entidades lógicas que forman el sistema distribuido: puede ser un stack de protocolos de red o un framework que le oculte al programador los detalles de la red. La diapositiva oficial lo representa en capas:

```
WEB Services   CORBA   REST
gRPC (.NET)    RMI (Java)
RPC   NFS   MOM
TCP/IP   HTTP
```

> **[Interpretación]** Esta pila de bloques anticipa varias unidades del programa: RPC/gRPC son la Unidad 5, MOM es la Unidad 4, REST/Web Services son las Unidades 6 y 7. TCP/IP y HTTP son la base sobre la que se apoya todo lo demás (Sockets).

### Requerimientos de software distribuido

El software que implementa un cliente o un servidor tiene estos requerimientos:

| Requerimiento | Para qué sirve |
|---|---|
| Multi-thread | Atender a varios clientes al mismo tiempo |
| Mecanismo de concurrencia | Sincronizar los datos comunes |
| Comunicación entre procesos | Permite dividir en varios procesos, para escalar o para robustez |
| Soporte de comunicación de red | API de protocolos de red |
| Mecanismos de seguridad | Autenticación y autorización, para que el servidor asegure la identidad de los clientes y proteja la información |
| Servicios orientados a objetos | Interacción con la parte del middleware que provee publicación de objetos o web services |

> **[Interpretación]** Esta tabla es literalmente el mapa de las unidades que siguen: multi-thread y concurrencia → Unidades 2 y 3; comunicación entre procesos y de red → Sockets y Unidad 4/5; seguridad → Unidad 7 (REST); servicios orientados a objetos → Unidad 6 (Web Services).

### Autoevaluación rápida
- ¿Por qué la comunicación entre procesos se relaciona tanto con la escalabilidad como con la robustez de un sistema?

---

## 4. Arquitecturas distribuidas de ejemplo

**Qué es:** tres formas concretas de organizar cómo se conectan los nodos de un sistema distribuido entre sí.

| Arquitectura | Cómo funciona |
|---|---|
| **Spoke-Hub** ("Sistema San Francisco") | Un hub centralizado distribuye las peticiones dentro del sistema. Todas las comunicaciones pasan por ese punto único de coordinación. |
| **ESB** (Enterprise Service Bus) | Variante de cliente-servidor. Conecta múltiples servicios que hablan protocolos distintos (SOAP, REST, RMI, JNI) a través de un bus central de comunicación. |
| **Point to Point** | Cada nodo se comunica directamente con los demás, sin intermediario central. Más descentralizado, pero más complejo de gestionar a medida que crece la cantidad de nodos. |

### Ejemplos reales

**Google (evolución de infraestructura):** arrancó en 1998 con 1 Sun Ultra a 200 Mhz y 256 MB de RAM, 2 Pentium II a 300 Mhz y un IBM RS/6000 F50. Hoy se estima (sin datos oficiales confirmados por Google) más de 450.000 servidores clase PC con 16 GB de RAM, con cada dato replicado en al menos 3 lugares distintos.

**Netflix (arquitectura de Playback, ejemplo StuDocu + diapositiva oficial):**
1. El dispositivo cliente hace un pedido "Play".
2. Pasa por un AWS ELB (balanceador de carga) y un API Gateway Service.
3. El gateway enruta a la Play API dentro de la capa "Application API".
4. La Play API llama a un microservicio.
5. El microservicio consulta caché y datastores.
6. Se alimenta un Stream Processing Pipeline que manda datos a AWS S3 y Hadoop para analítica.
7. Por separado, el CDN de Netflix (OCAs) sirve los archivos de video directamente al dispositivo cliente, según rutas indicadas por el Steering Service (CODA) y el Cache Control Service (CCS).

> **[Complementario — Claude]** Este ejemplo es útil para ver en la práctica varios conceptos que vas a estudiar sueltos: balanceo de carga, microservicios, caché, y procesamiento de streams de datos a gran escala. No hace falta memorizarlo, pero sirve como "caso real" para anclar la teoría.

---

## 5. Threads: conceptos base

**Qué es:** la diferencia entre núcleo (hardware) e hilo (unidad lógica de ejecución). En el programa oficial esta introducción a Threads vive en la Unidad 1, antes de que la Unidad 2 la profundice.

**Por qué importa:** es la base de todo lo que viene después sobre concurrencia y paralelismo (Unidades 2 y 3).

**Cómo funciona:**

- **Núcleo:** unidad de procesamiento física de un procesador. Cada núcleo ejecuta un solo proceso a la vez. Más núcleos físicos = más procesos simultáneos reales.
- **Hilo (de un procesador):** unidad lógica (virtual) que representa una tarea manejada por el núcleo. Con tecnologías como hyper-threading, un núcleo físico puede manejar 2 (o más) hilos.

> Núcleo vs. hilo: los núcleos son físicos, los hilos son virtuales y están soportados por el núcleo. Más hilos mejora la multitarea, pero no duplica necesariamente la performance real de los núcleos.

### Proceso vs. hilo (programático)

- **Proceso:** instancia de un programa en ejecución. Tiene su propio espacio de memoria aislado (más robusto y seguro), pero la comunicación entre procesos es compleja y costosa por el cambio de contexto del sistema operativo.
- **Hilo (programático):** flujo de control dentro de un proceso. Varios hilos pueden coexistir en el mismo proceso, compartiendo memoria y recursos. Cambiar entre hilos es más rápido que cambiar entre procesos.

**¿Por qué usar hilos?** Una aplicación de un solo hilo se bloquea al hacer operaciones largas. Usar múltiples hilos permite ejecutar varios métodos a la vez, esperar a que otros terminen, o cancelarlos dinámicamente.

### Tipos de hilos

| Tipo | Comportamiento | Ejemplo |
|---|---|---|
| **Foreground** | No dejan terminar al proceso padre mientras estén activos | Guardado de archivos, tareas críticas que deben esperarse |
| **Background** | El proceso padre puede terminar sin esperarlos | Notificaciones, actualización de caché, estadísticas de uso |

### Patrón de diseño: Delegación

Un objeto ("gerente") no hace la tarea él mismo, sino que se la encarga a otro objeto ("especialista"). Ejemplo clásico: un navegador web, donde el hilo principal debe seguir atendiendo la interfaz mientras un hilo secundario hace una descarga.

```csharp
FileDownloader myDownloader = new FileDownloader("http://example.com/large_file.zip");
Thread downloadThread = new Thread(myDownloader.Download);
downloadThread.Start();
```

El hilo principal instancia al descargador, delega el método `Download` a un nuevo hilo, y lo arranca con `Start()`. El hilo principal sigue ejecutando el código siguiente sin bloquearse.

**Ventajas:** separación de responsabilidades (el hilo principal se enfoca en la UI) y reutilización/modularización.

> **[Complementario — Claude]** Este patrón es la base conceptual de por qué existen los hilos "en background": el gerente (hilo principal) no puede quedarse esperando a que el especialista (hilo secundario) termine, o toda la aplicación se congelaría.

### Autoevaluación rápida
- ¿Cuál es la diferencia entre un núcleo y un hilo de procesador?
- ¿Por qué cambiar de contexto entre hilos es más barato que entre procesos?
- Dado un ejemplo concreto (autoguardado de un documento vs. sincronizar estadísticas de uso), ¿qué tipo de hilo usarías, foreground o background? ¿Por qué?

---

## 6. Streams de datos

**Qué es:** una abstracción que representa una secuencia de bytes, sin importar de dónde vengan o hacia dónde vayan. Fuente 100% oficial: `4-Stream.pdf`.

**Por qué importa:** es la forma unificada en la que .NET (y la mayoría de los lenguajes) maneja archivos, red, memoria y dispositivos de entrada/salida — todo con las mismas operaciones básicas.

**Tres definiciones oficiales equivalentes de Stream:**
1. Una estructura de datos: una secuencia de datos potencialmente ilimitada, pedidos a demanda.
2. Un objeto que permite el flujo de datos entre un programa y algún dispositivo de E/S (ej. archivos).
3. Una abstracción de una secuencia de bytes, como un archivo, un dispositivo de entrada/salida, un canal de comunicación interprocesos, o un canal TCP/IP.

**Ejemplos de streams ya conocidos:** `Console.WriteLine` (standard output stream), `Console.ReadLine` (standard input stream), `NetworkStream`, `FileStream`.

### `System.IO.Stream` en .NET

Es la clase abstracta padre de todos los tipos de stream: define métodos y propiedades comunes (`Read`, `Write`, `Seek`, etc.) y soporta operaciones sincrónicas y asincrónicas, aislando al programador de los detalles específicos del sistema operativo y sus dispositivos subyacentes.

| Tipo de stream | Para qué se usa |
|---|---|
| `FileStream` | Lectura y escritura de archivos |
| `MemoryStream` | Buffer temporal accesible en memoria, para guardar datos |
| `BufferedStream` | Añade un buffer que puede usarse junto con otros streams, como `FileStream` |
| `NetworkStream` | Representa un stream a través de la red (se apoya en Sockets) |
| `CryptoStream` | Funcionalidades de criptografía |
| `IsolatedStorageFileStream`, `PipeStream`, `StringReader`, `StringWriter`, `BinaryReader`, `BinaryWriter` | Casos más específicos (almacenamiento aislado, comunicación entre procesos en la misma máquina, lectura/escritura de strings o binarios) |

### Tres operaciones fundamentales

- **Lectura (Reading):** transferir información desde un stream hacia una estructura de datos (ej. un array de bytes).
- **Escritura (Writing):** transferir información hacia un stream.
- **Búsqueda (Seeking):** búsqueda y posicionamiento dentro de un stream. **No está implementada para `NetworkStream`** (no tiene sentido "rebobinar" una conexión de red en vivo).

Por esto se puede consultar la existencia de estas funcionalidades en cualquier stream con las propiedades `CanRead`, `CanWrite`, `CanSeek`.

> **[Complementario — Claude]** Pensá en `Stream` como "un caño": no importa si el agua viene de la canilla (archivo), de la red pública (network) o de un tanque (memoria) — el caño siempre se abre, se lee/escribe y se cierra de la misma forma. Los envoltorios (`BufferedStream`, `CryptoStream`) son como filtros que le agregás al caño sin cambiar cómo lo usás. Que `NetworkStream` no soporte `Seek` tiene sentido con esta analogía: no podés "retroceder" el agua que ya pasó por la canilla de la red.

### Autoevaluación rápida
- ¿Por qué la abstracción de `Stream` facilita escribir código reutilizable entre archivo, red y memoria?
- ¿Por qué `NetworkStream` no implementa `Seek`?
- ¿Qué stream usarías para procesar datos intermedios en una API sin escribir a disco? ¿Por qué?

---

## Resumen ejecutivo del tema

1. La materia responde a un problema de escala: cómo hacer que aplicaciones en máquinas distintas trabajen juntas, desde sistemas monolíticos hasta arquitecturas cloud distribuidas.
2. El modelo cliente-servidor es una tecnología particular dentro de los sistemas distribuidos; los modelos de capas (2, 3, N) son formas de repartir dónde vive la lógica — a más capas, mejor escalabilidad y modularidad, a costa de más complejidad.
3. Todo sistema distribuido se describe con tres bloques: cliente, servidor y middleware (red); el middleware se puede implementar con RPC, MOM, REST, gRPC, etc. — cada una es una unidad distinta de la materia.
4. Los requerimientos de software distribuido (multi-thread, concurrencia, comunicación entre procesos, seguridad, servicios orientados a objetos) anticipan las unidades siguientes.
5. Un hilo es una unidad lógica de ejecución dentro de un proceso; existen para no bloquear la aplicación mientras se hacen tareas largas (patrón de Delegación).
6. Los streams son la forma unificada de trabajar con secuencias de bytes, vengan de archivo, red, memoria o dispositivos de E/S — todos heredan de `System.IO.Stream`.

## Pendiente de confirmar

- Si el umbral de exoneración (≥86 puntos) y derecho a examen (≥70 puntos) que figura en la diapositiva de marzo 2026 aplica igual al ciclo agosto 2026 en curso.
