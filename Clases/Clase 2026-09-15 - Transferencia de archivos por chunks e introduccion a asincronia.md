---
date: 2026-09-15
type: clase
tags:
  - clase
  - pdr
  - tema-2
  - tema-3
ai-first: true
confidence: stated
fuente: "Grabacion Vimeo, showcase 12367503, video 15-09-2026, 1h42m, subtitulos autogenerados"
docentes:
  - Enrique Latorres
  - TBD
---

# Clase 2026-09-15 - Transferencia de archivos por chunks e introduccion a asincronia

## For future agent
Acta de la clase del 15-09-2026 (Vimeo, showcase 12367503, 102 min). **Dos mitades bien distintas**:

1. **Transferencia de archivos** (ejemplo 5): el protocolo de 4 etapas, `FileStreamHelper`, el concepto de `Stream` con cursor, y **dos bugs plantados a proposito** que hay que arreglar en el obligatorio (el `Append` que duplica el archivo y el archivo parcial que queda cuando se corta la conexion).
2. **Introduccion a programacion asincronica** (arranca Tema 3), dictada por Enrique Latorres: concurrencia vs paralelismo, TPL, promesas/futures, `Task`, `ThreadPool`, planificacion apropiativa vs no apropiativa.

**La entrega del Obligatorio 1 se confirmo para el 28-09.** Ademas hay una tanda de consejos concretos para EDISON (nombres de imagenes, borrado, persistencia opcional).

## Confirmaciones sobre el Obligatorio 1
- **Entrega el dia 28** (lunes 28-09-2026). Confirmado al inicio y repetido el 17-09.
- Lo que faltaba explicar era **la transferencia de archivos**, que se cubrio en esta clase: "no es muy complicado y lo van a hacer a partir de cosas que ya hicieron. Pero hay que agregarle una capa mas a la transferencia de datos".
- Las **diapositivas de asincronia estan en el Drive** del docente.

## Por que no se puede mandar un archivo de una sola vez
- Un archivo puede tener **varios gigas**; una imagen de alta resolucion facilmente 4, 5 o 6 GB. Levantarlo entero **come toda la RAM**.
- Solucion: **cortar el archivo en partes** y mandarlas de a una.

## El proyecto Common del ejemplo 5
Tres piezas:

| Clase | Que hace |
|---|---|
| `NetworkHelper` | `Send` / `Receive` de siempre (los del ejemplo anterior) |
| `FileStreamHelper` | **nuevo**: lee y escribe pedazos del archivo en disco |
| `Protocol` (TCP) | constantes y el calculo de la cantidad de partes |

### Constantes del protocolo
| Constante | Valor | Por que |
|---|---|---|
| Largo fijo (de campos de largo) | **4 bytes** (`int`) | tamano de un entero |
| Largo del archivo | **8 bytes** (`long`) | un `int` no alcanza (ver abajo) |
| Tamano maximo de cada parte | **32 KB** | compromiso memoria / eficiencia |

### Por que el tamano del archivo va en long y no en int
- Un `int` son **32 bits**, pero **con signo**: queda **1 bit para el signo** y **31 bits de valor**.
- 31 bits dan del orden de **2000 millones**, o sea **~2 GB como archivo maximo**. Muy chico para imagenes y archivos reales.
- Un `long` son **64 bits**: 1 de signo y **63 de valor**, que alcanza de sobra.

> "Entonces, quiere decir que con un entero, el numero mas grande que puedo representar son dos gigas. Y eso es muy chico." ([06:00])

Salvedad: al hacer la cuenta en el pizarron el docente hablo de "ocho zetta bytes"; el valor real de 2^63 bytes es del orden de **8 exabytes**. El punto que importa (el `int` no alcanza, el `long` si) no cambia. Registrado en [[Huecos y contradicciones del material PDR]].

### CalculateNumberOfParts
Recibe el tamano total del archivo y lo divide por 32 KB.
- Si es **multiplo exacto**, devuelve ese cociente.
- Si **no** es multiplo, devuelve el cociente **+ 1**, por el pedazo chico que sobra y que **igual hay que mandar**.

### Por que 32 KB y no 1 byte ni 10 MB
Argumento del docente, que vale tanto para disco como para red:
- Toda transferencia (escribir a disco, mandar por red) **tiene un costo de protocolo por operacion**: negociar, pedir, confirmar.
- Si los bloques son **muy chicos**, ese costo es comparable al tiempo de transferencia util y **el ancho de banda efectivo cae**.
- Si son **muy grandes**, se come la memoria.
- 32 KB es "grandecito, pero no demasiado grande".

## El concepto de Stream (repaso que vale para todo el curso)
Explicado en el pizarron, partiendo de productor-consumidor:

- Un stream es un **buffer con un cursor**. Se puede **leer** y **escribir**, y ademas **mover el cursor** con **`Seek`** a una posicion arbitraria.
- Cada `Read` o `Write` **mueve el cursor** por la cantidad transferida.
- Si se asocia a un **archivo**, se puede leer por bloques de 32 K: primer bloque, segundo, tercero, y al final lo que queda. Se puede volver al inicio, ir al final, sobrescribir.
- Si se escribe al final, **el archivo crece**.
- Origen historico: la **unidad de cinta**. "Es un casete virtual."

### Jerarquia de Stream en .NET
`Stream` es una clase **abstracta** de la que derivan varios:

| Tipo | Uso |
|---|---|
| `FileStream` | asociado a un archivo en disco (`System.IO`) |
| `NetworkStream` | asociado a una conexion de red: lo que escribo de un lado aparece del otro |
| `MemoryStream` | "archivo virtual" en memoria |
| Streams con cifrado | por ejemplo `NetworkStream` + **SSL** para mandar cifrado |

El docente pidio **mirar la jerarquia de `Stream` en la documentacion de Microsoft**. `NetworkStream` y SSL se ven mas adelante.

## FileStreamHelper.Read
Recibe **path**, **offset** y **length**. Crea un `byte[]` del largo pedido.

- Hace `Seek` al **offset** y lee desde ahi.
- Tiene un **loop de reintentos** igual que `NetworkHelper.Receive`: `count = length - bytesRead`.
- Si lee **0 bytes**, tira una **excepcion** de archivo.
- **Por que reintentar en disco**: en una maquina fisica pedir 32 KB normalmente devuelve 32 KB, pero **en una maquina virtual con storage remoto** (un disco virtual al otro lado de una red, una imagen en un drive compartido) puede devolver 4 KB y el resto de a pedazos.

> "Yo tengo que reintentar, porque no se en que medio va a ejecutar mi codigo." ([22:00])

- El `offset` **se mantiene afuera**, en el codigo que recorre el archivo; el helper solo lee el bloque que le piden.

## FileStreamHelper.Write y el primer bug plantado
El codigo del ejemplo abre el `FileStream` con:
- **`FileMode.Append`** si el archivo **ya existe**
- crearlo si no existe

**Esto tiene un bug a proposito.** Cuando se manda **el mismo archivo por segunda vez**, el archivo ya existe y el helper **concatena**: el resultado pesa el doble y contiene dos copias.

> "Esta es una buena forma de enviar informacion oculta. Porque cuando abran este archivo, la imagen que ustedes van a ver es esta. Pero todo esto esta relleno con datos que no van a ver." ([27:00])

**Como arreglarlo**: tratar **el primer bloque de forma especial**. Si el archivo ya existe, al escribir el primer bloque hay que **llevar el cursor a cero y truncar** (reescribir, no `Append`). Del segundo bloque en adelante, `Append` esta bien.

> "Eso lo van a tener que corregir." / "No se por que quedo asi, pero esta bien porque asi ven el problema." ([28:00])

## El protocolo de transferencia: 4 etapas
Mismo esquema, espejado en cliente y servidor.

### Cliente
1. **Largo del nombre del archivo** (via `NetworkHelper.Send`, es texto).
2. **Nombre del archivo** (`FileInfo.Name`).
3. **Largo del archivo real** (`FileInfo.Length`, en `long`).
4. **El archivo**, por chunks, via `FileStreamHelper` + `NetworkHelper.Send`.

Detalles:
- Se pide el **path completo con nombre y extension** por consola, pero **solo se manda el nombre**, no el path.
- **Por que no el path**: el archivo se va a guardar en otro lugar, y la maquina destino puede ser **de otro sistema operativo**, donde el path se representa distinto.
- Loop de chunks: `currentPart = 1`, `offset = 0`, y `while (offset < fileSize)`.
- Si **no** es la ultima parte: leer `MaxPacketSize` (32 K) desde `offset`, mandar, `offset += 32K`.
- Si **es** la ultima parte: `partSize = fileSize - offset`, leer ese largo, mandar, `offset += partSize`.
- **Invariante de control**: al terminar, `offset` tiene que quedar **exactamente igual al tamano del archivo**. Si no coincide, algo salio mal.
- `NetworkHelper.Send` sigue teniendo su loop interno de reintentos: verifica cuanto se transmitio de verdad y reintenta lo que falta.

### Servidor
Las mismas 4 etapas al reves: recibe largo del nombre (convierte a `int`), recibe el nombre (decodifica), recibe el largo del archivo, y despues recibe el archivo por chunks.
- **Calcula la cantidad de partes con el mismo metodo de `Common`**, porque es codigo compartido entre cliente y servidor.
- Cada bloque recibido se escribe con `FileStreamHelper.Write`.
- En el ejemplo, **al terminar la transferencia se cierra la conexion de los dos lados**. Para mandar otro archivo se abre una conexion nueva. En el obligatorio no: despues de transferir se vuelve al menu.

### Chunks tambien en HTTP
> "Cuando veamos la parte de HTTP y transferencia de archivos en HTTP, que el protocolo HTTP tambien utiliza un mecanismo de chunks." ([33:00])

Las imagenes que baja el navegador usan un protocolo parecido a este.

## Demo: que pasa si se corta la transferencia, y el segundo bug plantado
El docente corto la transferencia de un archivo grande a mitad de camino.

- El archivo del lado del servidor **queda a medio escribir**.
- Al abrirlo, el visor de imagenes **lee el encabezado**, entiende las dimensiones y **muestra la parte que llego**; el resto **lo rellena de gris**.
- El ejemplo **no atrapa la excepcion**, a proposito.

**Que hay que hacer en el obligatorio**:
1. **Atrapar la excepcion**.
2. Darse cuenta de que lo que se estaba transfiriendo era **un archivo a medio transferir** y **borrarlo**.
3. Mostrar un mensaje de que no se pudo, y que el usuario reintente reconectandose.

> "El servidor lo que tiene que asegurarse es que si no vino todo el archivo, se perdio la conexion, ese archivo se borre." ([54:00])

## Consejos concretos para EDISON (Obligatorio 1)
Sobre la foto asociada a cada modelo de auto:

- En memoria (un `ConcurrentDictionary` o similar) van los datos del modelo **mas un path a la imagen asociada**.
- **No usar el nombre de archivo que trae el cliente.** Los nombres **colisionan** (imagenes bajadas de internet que se llaman todas igual, archivos de distintos usuarios).
- Guardar con un **nombre unico derivado del modelo o de un codigo propio** (por ejemplo `modelo1.jpg`), de forma que **haya exactamente una imagen por modelo**.
- Si ese archivo ya existe, **sobrescribirlo**.
- **Si se borra el modelo hay que borrar el archivo.** Esto la letra lo pide y **lo van a chequear**: la imagen es parte de los datos del modelo.

> "Despues, la letra les dice que si yo voy a borrar ese modelo, tengo que asegurarme que tambien se borre el archivo." ([58:00])

- **En la defensa hay que mostrar que al transferir un archivo la imagen aparece**, y con que nombre queda. Conviene una **carpeta visible**, potencialmente **fuera del contenedor**.
- Si opcionalmente quieren devolverle al cliente el archivo con el nombre original, se puede, pero del lado del servidor el nombre canonico es el propio.

## Persistencia de datos: opcional pero recomendada
- **No esta en la letra.** Si no llegan a hacerlo, no pasa nada. Pero el docente la recomienda.
- Como: **serializar el diccionario** a un **TXT, JSON o XML**.
- Cuando arranca el servidor, **antes de abrir las conexiones**: lee el archivo y construye las estructuras. Al terminar, **antes de salir**: sobrescribe el archivo con la estructura.
- La ruta del archivo se puede dejar en el **`app.config`** (ConfigurationManager, visto el 10-09).
- Que hay que guardar: **usuarios, modelos de autos y eventos/reservas**.
- Beneficio practico grande: **no hay que recrear los datos de prueba** cada vez que se prende el servidor. La alternativa es una rutina que inyecte datos de prueba.
- Si **no** hacen persistencia: como todos los datos se pierden al apagar el servidor, las imagenes pueden quedar huerfanas en el directorio. **Es decision del grupo** borrarlas al cerrar o dejarlas, **siempre que se documente**. Si se dejan, al recrear el mismo modelo la nueva imagen **le pisa** la vieja, asi que no molesta.

## Docker (adelanto, no es para el Obligatorio 1)
- En general se va a pedir que **cada aplicacion este en un contenedor separado**: el cliente en uno, el servidor en otro. Para la **ultima entrega**, con varios servidores y varios clientes.
- Para tener varias aplicaciones coordinadas: **Docker Compose**, donde se escriben tambien las **relaciones de precedencia** (quien tiene que estar levantado primero).
- **Volumenes compartidos**: un directorio del contenedor se comparte con un directorio del host y otro contenedor escribe ahi tambien. "Hacen lo que se llama tipo bandejas, que uno pone cosas y el otro las recibe."

## Segunda mitad: introduccion a programacion asincronica (Tema 3)
Dictada por Enrique Latorres, con sus propias diapositivas.

### Concurrencia vs paralelismo
| | Que es |
|---|---|
| **Paralelismo** | dos cosas se ejecutan **realmente al mismo tiempo**; implica **hardware repetido** (dos nucleos, dos procesadores) |
| **Concurrencia** | dos hilos **logicamente** al mismo tiempo, pero pueden estar alternando en **un solo nucleo** por **time slice / quantum** |

- Analogias del docente: **concurrencia** = estudiar para dos examenes el mismo dia, un rato cada uno; **paralelismo** = cocinar mientras hablas por celular.
- Las **maquinas modernas hacen las dos cosas**: varios nucleos, y cada nucleo alternando entre hilos.
- **Clave**: desde el punto de vista de los problemas de **exclusion mutua**, concurrencia y paralelismo son **iguales**. Las soluciones son las mismas: semaforos, exclusion mutua, regiones criticas, monitores.

### Thread casi no se usa en codigo profesional
> "La verdad es que, en realidad, esto que estamos usando hasta ahora, en un programa profesional, rara vez se usaria threads." ([69:00])

`Thread` solo cuando hace falta **control especial del hilo**. En general se prefieren abstracciones de mas alto nivel, que usan `Thread` por debajo:
- **Task Parallel Library (TPL)**
- **ThreadPool**
- **programacion asincronica** (`async` / `await`)

### Task Parallel Library
- Sirve para ejecutar **tareas chicas y cortas** en paralelo. **No** para cosas que van a procesar mucho tiempo.
- Conceptualmente parecido al **`cobegin` / `coend`** visto antes, con otra notacion.
- Se declaran varias tareas, se agrupan y se pasan a una funcion que las ejecuta en paralelo.

```csharp
Parallel.Invoke(
    () => SomeTask("A", 500),
    () => SomeTask("B", 500));

Parallel.ForEach(lista, item => Procesar(item));

Parallel.For(0, 10, i => Procesar(i));   // ejecuta 0..9, el 10 NO
```

- `Parallel.For(0, 10, ...)` ejecuta **del 0 al 9**: el limite superior **no se incluye**.

### Promesas / valores futuros
- Problema: si invoco un metodo y se ejecuta en paralelo, **no puede devolverme un resultado consistente hasta terminar**.
- Solucion: devuelve una **promesa** - en C#, un **`Task<T>` generico** donde `T` es el tipo del resultado.
- Comportamiento del `Task`:
  - Si le **pido el contenido** (`Result` / `await`) y todavia no esta, **me bloquea** hasta que el metodo lo ponga.
  - Si le **pregunto el estado**, me dice en que anda sin bloquearme.
- Eso permite un **hilo principal** que dispara varias tareas, sigue haciendo otras cosas, y recien al final recolecta resultados.
- **Terminologia y referencias historicas dadas en clase**: los terminos fueron propuestos en **1976 por Friedman y Wise**; Hibbard lo llamo **"eventual"**; introducidos tambien en **1977 por Baker y Hewitt**.
- Analogia del docente: encargar una torta. Te dan **la factura** (la promesa) ahora; el producto esta en una semana; mientras tanto seguis trabajando en otra cosa.
- Se desarrollo en el pizarron un ejemplo de expresion anidada (`x = y.A().C(z.B())`): con promesas se puede **ir calculando `z.B()` en paralelo** mientras `y.A()` todavia se resuelve, en vez de hacer todo en profundidad con backtracking.

### Hilos de background: el peligro numero uno
> "Todos estos mecanismos usan hilos de background. Entonces, como usan hilos de background, si el main termina, se termino todo." ([85:00])

Consecuencia: en el `Main` hay que **esperar** las tareas, o mueren a medio camino.

### async / await
- **`async`** marca el metodo como asincronico (le indica al compilador que compile distinto y que trate `await` como palabra reservada).
- **`await`** espera el resultado.
- Se puede mezclar codigo sincronico y asincronico, **pero es "un sacrilegio"**. Lo que se va a hacer es que **el `Main` sea asincronico y todo lo demas tambien**.
- Especialmente conveniente **para el servidor**.

### Lo que hay debajo: ThreadPool y segmentos
- Detras hay un **`ThreadPool`**: reserva un conjunto de hilos y los reparte entre las tareas asincronicas. Si hace falta, **crea mas**.
- **Diferencia clave con `Thread`**: con `Thread`, cada hilo se crea (stack + Process Control Block), ejecuta, y **muere**; y cada cambio de contexto guarda y restaura todo el estado. Eso **cuesta caro** cuando hay muchos hilos.
- Con asincronia, **los hilos del pool no mueren nunca**: el compilador corta el programa en **segmentos** y en los puntos de corte el hilo decide si continua o agarra otro segmento.
- Los **puntos de corte** son los puntos de transicion asincronica: un `await`, una invocacion de tarea, un `Task.Delay`.
- **Consecuencia importante**: el codigo **parece sincronico** pero **no se puede garantizar el orden** en que se ejecutan los segmentos de tareas distintas. Dentro de un segmento si esta garantizado.
- Las tareas asincronicas de C# **se compilan a una maquina de estados** en el lenguaje interno de .NET.
- Se pueden agregar **continuations** (continuaciones) **en tiempo de ejecucion**: "despues que termine aquella cosa, correme esto".
- El **scheduler** gestiona el pool con una **cola global** y **colas locales por hilo**, y **escala dinamicamente** la cantidad de hilos.

### Planificacion apropiativa vs no apropiativa
Encuadre historico que dio el docente:
- **No apropiativa**: el despachador le da el procesador a un proceso y **corre hasta terminar**. Asi funcionaban **Windows y Mac OS en sus primeras versiones**.
- Problema: un proceso largo (una consulta compleja de base de datos) **acapara el procesador** y posterga todo; y un programa malicioso podia **robarse** el procesador.
- Los sistemas operativos modernos (Windows, Linux, macOS) son **apropiativos**: el quantum le saca el procesador al proceso.
- **La programacion asincronica es un sistema no apropiativo metido adentro de un sistema apropiativo.**

### CancellationToken (adelanto)
Con `CancellationToken` se le puede avisar a todos los hilos que terminen, **incluso cuando estan bloqueados**. Muchos metodos bloqueantes pasan a ser **awaitables** y se pueden interrumpir. Eso resuelve el problema del `Accept` trancado que quedo abierto el 10-09.

## Enlaces
- [[Clase 2026-09-10 - Maquina de estados del protocolo, Common y app.config]]
- [[Clase 2026-09-17 - Task en detalle, continuations, ThreadPool y TPL]]
- [[Concurrencia de Thread a Task]]
- [[Como escribir sockets al estilo del curso PDR]]
- [[Errores caros en Programacion de Redes]]
- [[Programacion de Redes 2026]]
