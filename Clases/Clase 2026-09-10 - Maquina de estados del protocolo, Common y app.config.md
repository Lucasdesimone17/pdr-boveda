---
date: 2026-09-10
type: clase
tags:
  - clase
  - pdr
  - tema-2
ai-first: true
confidence: stated
fuente: "Grabacion Vimeo, showcase 12367503, video 10-09-2026, 1h40m, subtitulos autogenerados"
docentes:
  - TBD
---

# Clase 2026-09-10 - Maquina de estados del protocolo, Common y app.config

## For future agent
Acta de la clase del 10-09-2026 (Vimeo, showcase 12367503, 100 min). Cierra el repaso de `ReceiveAll` / `SendAll` y **agrega tres cosas nuevas que impactan directo en el Obligatorio 1**: (1) el servidor tiene que ser una **maquina de estados** espejo de los menus del cliente, por seguridad; (2) el **proyecto `Common`** compartido entre cliente y servidor; (3) **`app.config` + `ConfigurationManager`** para no cablear IP ni puerto, que es exactamente lo que se pide en la defensa. Cierra con un adelanto de transferencia de archivos por bloques de 32 KB. Los nombres propios de la transcripcion automatica no son confiables: hay dos docentes en la clase y ninguno se identifica con seguridad, asi que quedan como `TBD`.

## Aviso importante: el Obligatorio 1 se reusa en la optativa de Seguridad
El docente aviso que **estan coordinando con los docentes de la optativa/electiva de seguridad** para que este obligatorio les sirva despues como base: ahi le van a hacer el analisis de seguridad a este mismo sistema.

> "Entonces, cuiden este obligatorio, mas alla de que sea optativo, porque si esta en la parte de materia de seguridad." ([11:00])

Consecuencia practica: conviene que el codigo quede presentable y que las decisiones de seguridad (estados, validaciones) esten hechas en serio, no parcheadas.

## Repaso de protocolo: por que el largo va en bytes y no en caracteres
- `option.Length` **no sirve** como largo: da **caracteres**, no bytes. Hay que usar el largo del `byte[]` que devuelve el encoding.
- En **UTF-8 / Unicode** un caracter puede ocupar **1, 2, 3 o 4 bytes**. Si se pide el caracter 3 de un `string` se obtiene el caracter 3; si se pide el byte 3 del array, puede ser un pedazo del primer caracter.
- Casos concretos que rompen: **enes, tildes, signos de exclamacion invertidos** (2 bytes), caracteres chinos (hasta 4).
- Regla: **siempre convertir a `byte[]` antes de medir**, porque la conexion de red es un stream de bytes.

```csharp
byte[] data = Encoding.UTF8.GetBytes(option);
int largo = data.Length;          // bytes reales, no caracteres
```

## El stream no delimita: desincronizacion
- La red entrega **un stream de bytes sin separacion de mensajes**. El receptor necesita **pistas** para saber donde cortar.
- Protocolo minimo del ejemplo: **numero de largo, despues la cadena**; y otra vez numero, cadena, numero, cadena.
- Si el largo esta mal **por un solo byte**, el receptor corta en el lugar equivocado, toma bytes de datos como si fueran el largo del proximo mensaje y **se desincroniza todo el resto del stream**.

> "Si le erran en un numero, despues se desincroniza todo el resto del streaming, ve papas fritas y se cuelga el servidor." ([09:00])

- Ejemplo de pizarron: stream de 12 bytes + 4 bytes. Si en vez de 12 se manda 11, el receptor cree que el mensaje termina un byte antes y **toma un byte de datos como largo**.
- Causa tipica del error: **el enter que queda al final** de lo que se lee de consola, o cualquier byte que sobra o falta.
- El docente ofrecio preparar un ejemplo de **como capturar el stream para ver los bytes** y diagnosticar estos casos.

## Maquina de estados: el tema central de la clase
La idea: **el estado del menu del cliente tiene que tener un estado equivalente del lado del servidor**, y el servidor solo acepta los mensajes validos de ese estado.

### Del lado del cliente
- Menu 1: **login**, **crear usuario**, **salir**.
- Buena practica sugerida: **`0` es salir en todos los menus**.
- Despues de un login exitoso se pasa al **menu 2**, que tiene el resto de las funcionalidades. Menu 2 es en la practica un **submenu anidado** de la opcion login.
- Se implementa como un `loop` por menu, que muestra el mensaje y despacha segun la opcion.

### Del lado del servidor
- El `ClientHandler` arranca en un primer estado donde **solo acepta login y crear usuario**.
- Recien cuando el login funciona pasa a **otro loop** que solo espera los mensajes de operacion.
- **Desde el menu 2 no se puede volver a hacer login**: para eso hay que volver al nivel anterior.
- Si llega un mensaje **que no corresponde al estado actual**, el servidor responde **error de protocolo / comando equivocado** y **aborta**, porque ya hay un problema de comunicacion (como minimo, desincronizacion del stream).

### Por que, el argumento de seguridad
Si todos los comandos estan en **un unico loop** del servidor, alguien que se conecte por protocolo (sin usar el cliente propio) puede **pedir operaciones sin haberse autenticado**. Mas aun, si la verificacion falla en algun punto, o el ambiente reinicia y las variables no se reinicializan, **puede quedar el dato de un usuario viejo**. Separando por estados eso no puede pasar: el mensaje ni se procesa.

Segundo beneficio: **detecta corrupcion del stream**. El cliente propio solo manda lo programado; si aparecen mensajes raros es porque los bytes estan corridos.

## Salida prolija del cliente
Dos opciones y el docente recomienda la primera:

1. Al elegir salir, el cliente **manda un mensaje de salida al servidor**. El `ClientHandler` ve que termino y **cierra prolijo**.
2. No mandar nada y **dejar que el servidor detecte la desconexion por excepcion**.

> "Como codigo, queda mucho mas prolijo... y que no tenga que esperar a una excepcion a darse cuenta de que la comunicacion se termino." ([14:00])

El docente pidio explicitamente **probar las dos** para ver la diferencia.

## ReceiveAll en detalle
Firma del ejemplo: recibe el **socket** del cliente y el **tamano total** del array de bytes que se quiere recibir.

- `while (total < size)`: mientras no se llegue al total.
- `offset` arranca en **0** y avanza con los bytes que ya llegaron.
- El tercer parametro **no es el largo total sino lo que falta**: `size - total`.
- Si `Receive` devuelve **0**, es **desconexion del cliente**: se sale del loop y se aborta.
- Devuelve un `byte[]` que despues se convierte a texto con el encoding.

### Por que hacen falta los reintentos
El docente lo desarrollo en el pizarron: entre cliente y servidor hay **routers y nodos**, la red puede tener **varios caminos** y algunos **congestionados**. El mensaje se corta en paquetes; TCP/IP lleva la cuenta y manda **acknowledges**. Si un paquete no llega, no hay acknowledge y del lado receptor hay que **reintentar la recepcion** hasta juntar todo.

Con un mensaje de 3 bytes esto es exagerado, "matar cucarachas arreando elefantes", pero la rutina se escribe **una vez como helper** y se usa igual para 3 bytes que para 1800.

> "Pero generalmente lo que nosotros hacemos es una biblioteca, un helper, que nos ayude a nosotros para la parte de comunicacion." ([29:00])

Tambien menciono que si el mensaje es muy grande no conviene un buffer unico (una imagen de varios gigas consume toda la memoria): hay que **partirlo en franjas**, que es el tema del ejemplo siguiente.

Nota: en C# la destruccion de objetos sin referencia es automatica; en otros lenguajes hay que deshacerlos a mano.

## Protocolo con tipo: el ejemplo 3
Estructura del mensaje del ejemplo:

| Campo | Tamano | Contenido |
|---|---|---|
| Tipo | 3 bytes (**ASCII**) | `STR`, `INT`, y para float algo tipo `FL` |
| Largo | 4 bytes (`int`) | largo de los datos |
| Datos | variable | el contenido |

- Los 3 caracteres del header van en **ASCII**, donde **1 caracter = 1 byte**, asi que el header mide exactamente 3 bytes. (En la transcripcion hay un tramo confuso sobre UTF-8 vs ASCII; lo consistente con el codigo mostrado es: **header ASCII fijo, datos en UTF-8 medidos por `GetBytes`**.)
- El docente aclaro que **la letra del obligatorio apunta a un protocolo de este estilo**: una cadena corta con unos pocos caracteres que indican que se pide, si es solicitud o devolucion de datos.
- Para **enteros y punto flotante no haria falta mandar el largo**, porque tienen tamano fijo (el `int` siempre 4 bytes).

## Orden de bytes: HostToNetworkOrder
Tema que viene de Arquitectura de Sistemas y que aparece aca:

- Intel/AMD guardan los numeros en memoria en **little endian**: los bytes menos significativos primero. Otras arquitecturas (Motorola, algunos chips de IoT) los guardan en el orden inverso, **big endian**.
- Si el cliente y el servidor corren en **plataformas distintas**, el numero se lee dado vuelta.
- `IPAddress.HostToNetworkOrder` toma la codificacion nativa de la maquina y la lleva al orden que usa la red, para que el codigo sea **independiente de plataforma**.

> "El problema es que yo puedo, justo en Intel no pasa nada, pero si yo voy a correr mi programa en un ambiente que usa otro tipo de procesadores, puede ser que la codificacion sea diferente." ([43:00])

Salvedad: en la clase se dijo que la red usa little endian; la convencion real de TCP/IP es **big endian / network byte order**. Para el codigo da lo mismo: **usar siempre `HostToNetworkOrder` / `NetworkToHostOrder`** y no asumir el orden nativo. Registrado en [[Huecos y contradicciones del material PDR]].

## SendAll: espejo de ReceiveAll
- Mismo esquema: `total = 0`, `offset` arranca en 0, el largo pasa a ser `largo - total`.
- `Send` **devuelve la cantidad de bytes efectivamente transferidos** (los que la otra punta reconocio). Ese es el numero que se acumula.
- Orden de envio del ejemplo: primero los 3 bytes de tipo, despues los 4 bytes del entero de largo, despues la cadena.

## Serializacion de objetos
Para mandar un objeto conviene **serializarlo** (JSON, texto) con todos sus atributos y reconstruirlo del otro lado. **En este obligatorio no se usa**: la idea del Obligatorio 1 es justamente experimentar con sockets a bajo nivel.

> "La idea es que vean, que experimenten con socket a bajo nivel, vean sus limitaciones, vean sus problemas, capturen las excepciones, vean un poco que armar el protocolo que funcione, todo es un trabajito. Asegurarte de que no se te pierdan bytes en ningun lado. Despues vamos a ver modelos que ya te generan todo automatizado." ([50:00])

## El proyecto Common
- La solucion del ejemplo 4 tiene **tres proyectos**: `server`, `client` y `common`.
- `common` **no es aplicacion de consola**: son **solo clases** (compila a biblioteca).
- Hay que **agregar la referencia a `common`** en el cliente **y** en el servidor (click derecho, `Add Reference`, tildar `common`). Si no, da error de compilacion.
- Conviene un **namespace comun** para que todos vean las bibliotecas.
- Que va en `common`: todo lo que se usa de los dos lados. **El servidor manda mensajes al cliente y el cliente al servidor**, asi que las rutinas de comunicacion son las mismas de las dos puntas. Tambien metodos por tipo de dato: mandar string, mandar entero, mandar fecha, mandar imagen.

## app.config y ConfigurationManager
Este es el mecanismo que resuelve el requerimiento de **configuracion externa sin recompilar**.

- `app.config` es un **XML** con pares clave/valor que la aplicacion lee al arrancar.
- Se agrega la biblioteca **`System.Configuration.ConfigurationManager`** (paquete) y la clase va **en `common`**.
- Se declara una **interfaz de settings** con un metodo de lectura: recibe la clave, devuelve el valor como string. Se asume **solo lectura**: no se toca el archivo desde el codigo.
- Ademas cada ejecutable tiene su **`Config.cs`** con las **constantes** (por ejemplo `ClientIpConfigKey`), que hacen de alias interno del nombre del atributo del XML. El motivo dado: **aplicaciones multilenguaje** - la clave interna esta en ingles y el nombre visible en el XML puede estar en otro idioma.
- Que se guarda ahi: **IP y puerto** (del cliente y del servidor), y opcionalmente **el directorio de persistencia de datos** y **donde se guardan las imagenes recibidas**.
- **Ojo con los tipos**: el puerto es un **entero**, pero la direccion IP es un **array de 4 bytes**. Hay que parsear el string a **`IPAddress`**, que es un objeto, no una cadena.

```csharp
int port = int.Parse(settings.ReadSetting(ServerConfig.ServerPortConfigKey));
IPAddress ip = IPAddress.Parse(settings.ReadSetting(ServerConfig.ServerIpConfigKey));
var endpoint = new IPEndPoint(ip, port);
```

- Al compilar, el archivo **cambia de nombre** y aparece en `bin\Debug\net8.0\` (y en `bin\Release\...` al generar release) como el `.config` del ejecutable. Hay **uno para el cliente y uno para el servidor**.
- En el ejemplo, el cliente tenia la IP cableada a `localhost`. Eso funciona mientras el servidor este en la misma maquina; **falla si el servidor esta en otra**.

### Como se usa esto en la defensa
> "Es una de las cosas que vamos a hacer en la defensa: ustedes van, ponen el release en maquinas de laboratorio, el cliente en un lado, el servidor en otro, inspeccionan cuales son las direcciones IP de las maquinas de laboratorio y configuran los archivos de configuracion, levantan y se tienen que poner." ([84:00])

Concuerda con lo dicho el 08-09: las IP del laboratorio las da DHCP y cambian.

## Apagado prolijo del servidor
La letra pide una **sentencia / comando de salida** en la consola del servidor.

- Se lanza un **hilo aparte** que hace `ReadLine` y espera comandos desde la consola.
- Cuando llega el comando, pone las **banderas** de los loops en `false` (en vez de `while(true)`, `while(todoBien)`) y hace **`Join`** con los hilos para esperar que terminen.
- **El problema real**: `Accept` es **bloqueante**. El hilo que esta esperando conexiones **queda trancado ahi** y la bandera no lo saca.

> "Te podes asegurar que todos los client handlers terminaron, pero ese hilo lo vas a tener que abortar, necesariamente." ([70:00])

- Lo mismo pasa con el hilo trancado en `ReadLine`.
- Solucion sugerida para el Obligatorio 1: que ese hilo sea de los que se **abortan** al terminar.
- Adelanto: con **programacion asincronica y `CancellationToken`** esto se resuelve bien. Se ve en el Obligatorio 2.

## TcpListener y TcpClient
- Son **clases de mas alto nivel** que `Socket`, apuntadas especificamente a **TCP/IP**.
- `Socket` es mas abierto: permitiria hablar protocolos propietarios (IPX/SPX de Novell, NetBIOS/NetBEUI de Microsoft), que hoy practicamente no se usan porque solo servian en redes locales.
- **En el Obligatorio 1 hay que usarlo con `Socket`.** En el proximo si se va a usar `TcpClient` junto con programacion asincronica.
- En la carpeta de codigos que paso Enrique por Teams hay ejemplos que usan `TcpClient`.

## Adelanto: transferencia de archivos
- Problema: un archivo puede ser **enorme comparado con la memoria**. No se puede levantar entero a RAM.
- Solucion: **buffer de tamano manejable, tipicamente 32 KB**. Se lee un bloque del archivo, se manda con **`SendAll`**, se lee el siguiente, y asi.
- Hay **dos loops anidados**: uno recorre el archivo de a pedazos, el otro (dentro de `SendAll`) se asegura de que cada pedazo se transmita completo.
- Hay que **llevar la cuenta del tamano del archivo** para saber cuantos bloques de 32 K son, y **el ultimo bloque necesita tratamiento especial** porque casi nunca es de 32 K exactos.

## Tarea para la clase siguiente
- **Arrancar el obligatorio ya.** "Ya tienen casi todo para armar."
- Tener **los menus y los dialogos del cliente armados**, y la logica del servidor.
- Hacer una **biblioteca en `common`** con metodos probados: mandar string, mandar entero, mandar fecha. Revisar como se codifica la fecha.
- **Hacer pruebas de que no se pierden bytes**: mandar una secuencia de cosas (texto, numero, fecha) y verificar que no se desincroniza.
- Mirar los **ejemplos 3 y 4**, jugar con ellos, cambiarles puertos y agregarles parametros.
- Quien quiera puede armar un chat como practica.
- Se paso lista.

## Enlaces
- [[Clase 2026-09-08 - Protocolo propietario, framing y Receive parcial]]
- [[Clase 2026-09-15 - Transferencia de archivos por chunks e introduccion a asincronia]]
- [[Como escribir sockets al estilo del curso PDR]]
- [[Reglas y prohibiciones del curso PDR 2026]]
- [[Programacion de Redes 2026]]
