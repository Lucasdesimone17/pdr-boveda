---
date: 2026-09-08
type: clase
tags:
  - clase
  - pdr
  - tema-2
ai-first: true
confidence: stated
fuente: "Grabacion Vimeo, showcase 12367503, video 08-09-2026, 1h25m, subtitulos autogenerados"
docentes:
  - TBD
---

# Clase 2026-09-08 - Protocolo propietario, framing y Receive parcial

## For future agent
Acta de la clase del 08-09-2026 (Vimeo, showcase 12367503). **Es la clase mas directamente util para escribir el Obligatorio 1**: como organizar la solucion en proyectos, por que existe el campo Largo, como conseguir un largo fijo, el bug de los caracteres multibyte, y **por que `Receive` puede devolver menos bytes de los pedidos y hay que llamarlo en un loop**. Tambien resuelve dos dudas abiertas de la letra. Salvedad: la dicto un docente distinto al del teorico, con foco practico; **la transcripcion automatica no permite confirmar su nombre** (se oye "Iván"), asi que queda `TBD` - no citarlo por nombre hasta confirmarlo.

## Aclaraciones sobre el Obligatorio 1 (resuelven dudas abiertas)
- **Se subio un Excel por Teams para anotarse** (grupos). Verificar estar anotado.
- **`.gitignore`**: no hace falta que tenga un monton de reglas; hay generadores en la web. Un `README` **no es obligatorio**, se agrega si al grupo le parece necesario.
- **Validaciones**: lo que importa son **validaciones de logica**, no de formato. Ejemplo dado: si no hay unidades disponibles, tiene que dar error. Se pueden hacer del lado del cliente o del servidor; **es mas practico del lado del servidor**, y las de logica (reservas, capacidades) van ahi.
- **Contradiccion de la letra resuelta**: la letra dice por un lado que el catalogo de modelos es permanente y por otro que un modelo se puede eliminar. Respuesta del docente: **si se puede eliminar** (con la restriccion de la letra: siempre que no existan reservas activas asociadas).
- **Testing**: el que hay que hacer es el **de integracion** - probar que una peticion sale del cliente, viaja, llega y vuelve. El unit testing queda a criterio del grupo; el docente argumento que **vale la pena en proyectos largos** (tres anos si, un proyecto hasta noviembre no tanto) y que no tiene sentido buscar 100% de cobertura aca. El test de usuario **si hay que hacerlo**.
- **Defensa**: es **en el laboratorio, con el grupo**, levantando la aplicacion en **varias computadoras**. Los docentes van a tener **una guia / plan de testeo** e indican que hacer. Ademas puede haber que **cambiar algo de codigo** en vivo, pero lo mas importante es probar la aplicacion.
- **En la defensa hay que configurar la IP del servidor en el cliente.** Las IP del laboratorio las da DHCP y **cambian**, asi que no se pueden cablear: hay que mirar la IP del servidor (`ipconfig`) y ponerla en el cliente. Esto es exactamente el requerimiento de **configuracion externa sin recompilar**.
- Sobre defensas de las otras entregas, en esta clase se dijo que **el segundo y el tercero tambien tienen defensa**, lo que contradice lo dicho el 27-08. Registrado en [[Huecos y contradicciones del material PDR]].

## Como organizar la solucion
- Una **solucion de .NET es un grupo de proyectos**, nada mas.
- Van a tener **una solucion con dos proyectos ejecutables**: cliente y servidor (ambos console app).
- Se pueden **agregar mas proyectos**: uno de dominio, uno de helpers, lo que el grupo quiera. Un proyecto que sea solo funciones auxiliares **no compila a ejecutable, compila a DLL**, y cada ejecutable lo importa e incluye en su compilado.
- Organizacion interna sugerida, retomando Diseno 1 (division por capas: controller, logica, acceso a datos):
  - **Servidor**: parte de control, **parte de sockets**, logica y acceso a datos (estructuras en memoria).
  - **Cliente**: **la CLI** (interfaz de usuario) y la conexion por sockets.
- Lo que se puede arrancar ya: la interfaz del cliente, la logica y el acceso a datos del servidor, y la parte de comunicacion por sockets.
- Recordatorio: los datos van **en memoria**, no en disco, **pero por eso hay que manejar concurrencia** - un hilo por cliente, varios hilos tocando la misma estructura.

## Repaso: proceso, programa e hilos
- **Programa vs proceso**: en la defensa, corriendo el servidor en una maquina y un cliente en dos maquinas mas, hay **dos programas y tres procesos**.
- **Por que usar hilos**, con la logica que quiere el docente:

> "Cada tema que vean resuelve un problema. Usar hilos, usar multiexpresion, les hace mas complicada la vida. Entonces, todo lo que ustedes hacen en esta materia es porque les resuelve un problema y resolver ese problema es mas importante que simplificarles la vida." ([13:45])

- **Dos razones para usar hilos**, y conviene sabersela asi:

| Tipo | Que es | Por que ayudan los hilos |
|---|---|---|
| **IO bound** | La mayor parte del tiempo de ejecucion se espera por entrada/salida | Mientras un hilo esta bloqueado esperando, otro hilo avanza. Si hubiera un solo hilo, **se bloquearia todo el proceso** |
| **CPU bound** | Solo depende de CPU y memoria, sin esperas | Si el trabajo se puede **paralelizar** (datos independientes), con N nucleos tarda ~1/N |

- Ejemplo de **CPU bound** desarrollado en el pizarron: **multiplicacion de matrices**. Cada celda del resultado depende de una fila y una columna y es **independiente** de las demas; las matrices de entrada son de solo lectura, asi que se puede repartir por filas. Solo vale la pena en matrices grandes: "matrices de tres por tres no va a ser mas rapido".
- **Cuales son las operaciones bloqueantes del servidor de sockets**: **`Accept`** y **`Receive`**. Con un solo hilo, o el servidor se queda esperando al proximo cliente sin procesar al anterior, o se queda bloqueado recibiendo de un cliente sin pasar al siguiente. **Ese es el motivo de usar hilos en el servidor.**
- Tambien se espera en las **secciones criticas** (ahi se espera por otros hilos, no por E/S).
- **En el cliente no hace falta multihilo**: hay un solo usuario.

## Protocolo propietario: por que existe el campo Largo
El protocolo base es `Largo` + `Datos`. La pregunta de la clase fue: por que no mandar solo los datos.

- Abajo, TCP entrega un **stream**: conceptualmente **una lista potencialmente infinita de bytes, sin separacion entre mensajes**. Mandar el largo y los datos juntos o por separado da casi lo mismo a nivel de red; **el stream no delimita nada**.
- Entonces el problema real: al recibir hay que crear un array de bytes del tamano adecuado.
  - Array **muy chico** (por ejemplo 8 bytes): si el mensaje es mas grande, **se corta el mensaje**.
  - Array **muy grande** (por ejemplo 8000 bytes por iteracion): **gasta memoria** en cada mensaje.
- **El Largo resuelve las dos cosas**: dimensiona exactamente el buffer, y ademas permite saber **si falta recibir** y seguir recibiendo.

## Como conseguir un largo fijo
Si el Largo es variable, se necesitaria otro largo para el largo. Por eso **el campo Largo tiene tamano fijo**. Dos tecnicas, las dos validas:

### 1. Texto con ceros a la izquierda
Se toma el numero, se rellena con ceros a la izquierda hasta un ancho fijo, y ese texto se pasa a bytes con un encoding.

Ejemplo de la clase: ancho 3 -> `005`, `012`, `243`. **Siempre tres bytes**, y el maximo representable es 999.

```csharp
// largo fijo por texto, ancho 3
string largoTexto = datos.Length.ToString().PadLeft(3, '0');
byte[] largoBytes = Encoding.UTF8.GetBytes(largoTexto);   // exactamente 3 bytes
```

### 2. Binario directo con `BitConverter`
`BitConverter.GetBytes` tiene varias sobrecargas y **cada una garantiza un tamano fijo por documentacion**:

| Tipo que se pasa | Bytes que devuelve |
|---|---|
| `int` (Int32) | **4** |
| `short` | **2** |
| `long` | **8** |

```csharp
// largo fijo por binario
byte[] largoBytes = BitConverter.GetBytes(datos.Length);   // siempre 4 bytes para int
```

- Observacion que hizo: `int` da hasta ~2 mil millones, **un disparate para un largo**. Con `short` (2 bytes) alcanza de sobra. Y `BitConverter` con `int` **permite negativos**, que un largo nunca va a ser, asi que se desperdicia un bit.
- **El punto no es la eficiencia maxima, es que el largo siempre ocupe lo mismo.**

> "Es importante que el desarrollador haga un protocolo. Si uno usa este, el desarrollador que usa el mismo y se va." ([34:40], transcripcion imperfecta)

La idea que quiso transmitir: **cliente y servidor tienen que acordar la misma representacion**, porque la representacion de numeros es ambigua (A1, complemento a uno, complemento a dos, little/big endian). Si el otro extremo elige otra, no funciona.

## Encodings: el bug de la enie
Este fue el momento didactico central de la clase, y **es un error tipico de correccion**.

- Se paso texto a bytes con `Encoding` y funciono con "adios".
- Con un texto que tenia caracteres fuera del rango, salio mal: **el encoding usado no soportaba esos caracteres**. Se cambio a UTF-8 y anduvo.
- Despues probaron `"quiero comer noquis"` con **enie** y **se perdio un caracter al final**.

La explicacion:

| Encoding | Bytes por caracter | Alcance |
|---|---|---|
| ASCII | **1 byte, siempre** | Solo los caracteres que entran en un byte (2^7) |
| UTF-8 | **1 a 4 bytes**, variable | Superconjunto: acentos, enie, chino, emojis (un emoji son 3 o 4 bytes) |

- La **enie en UTF-8 ocupa 2 bytes**, no 1.
- **La causa del bug**: se estaba mandando como Largo **el largo del string** (la cantidad de caracteres), no **la cantidad de bytes**. Con enie, los bytes son mas que los caracteres, y el ultimo byte se pierde.
- Intento fallido que probaron en clase: **sumarle 1 al largo**. Anduvo para ese caso y volvio a fallar con otro caracter especial. "Sabes que le pongas +1 sabiendo que hay una ahi, claro, pero esa cosa siguiente no va a saber" ([58:26]). **No es la solucion.**
- Tambien se descarto contar los caracteres especiales con un `for` o preguntandole a UTF-8: valido pero innecesario.
- **La solucion correcta**: convertir primero a bytes y **mandar como Largo el `Length` del array de bytes**.

```csharp
// CORRECTO: el largo es la cantidad de bytes, no de caracteres
byte[] datos = Encoding.UTF8.GetBytes(texto);
byte[] largo = BitConverter.GetBytes(datos.Length);   // datos.Length, NO texto.Length
socket.Send(largo);
socket.Send(datos, datos.Length, SocketFlags.None);
```

> "Siempre que mando un dato, el largo no es el largo de la string, el largo es el largo de la cantidad de bytes que voy a enviar." ([60:27])

**Regla para el Obligatorio 1: el campo Largo cuenta bytes.** Anotado tambien en [[Errores caros en Programacion de Redes]].

## `Send` y `Receive` en detalle

### Firma y `SocketFlags`
- `Send(buffer, size, socketFlags)`: el **buffer** es el array de bytes a enviar y el **size** cuantos bytes de ese array enviar. El `size` es **opcional** en la practica; sirve cuando el array es mas grande que lo que se quiere mandar.
- `SocketFlags` es un vestigio de que **`Socket` es viejo y se uso con muchos protocolos**. Hay una flag para **broadcast** (mandar a todos), que soporta **UDP**, no TCP.
  - Anecdota util: antes las **impresoras hacian broadcast** anunciandose en la red ("aca estoy yo, esta es mi IP"), y los servidores tambien. Hoy se cuida mas.

### `Send` casi siempre manda todo; `Receive` NO
- Con la configuracion por defecto y TCP, **`Send` en general manda todo**.
- **`Receive` puede devolver menos bytes de los pedidos.** Esto es lo importante.
- `Receive(buffer, offset, size, socketFlags)`: llena `buffer` desde la posicion `offset` hasta como maximo `size` bytes, y **devuelve cuantos bytes efectivamente recibio**.
- **`Receive` es bloqueante**: espera **hasta recibir al menos un byte**. Por eso **nunca devuelve 0 en operacion normal**; si devuelve 0, **es la senal de que la conexion se cerro**.
- Por que puede llegar parcial: un paquete TCP tiene un tamano maximo, la red puede estar cargada, puede haber varios mensajes en vuelo. En la practica con mensajes chicos casi nunca pasa; con 10.000 bytes si.

### El patron obligatorio: loop hasta completar
La responsabilidad de recibir el mensaje completo **es del programador**.

```csharp
// patron de recepcion completa; el docente lo pidio explicitamente
int recibido = 0;
byte[] buffer = new byte[size];
while (recibido < size)
{
    int n = socket.Receive(buffer, recibido, size - recibido, SocketFlags.None);
    if (n == 0) throw new SocketException();   // conexion cerrada
    recibido += n;
}
```

Ejemplo numerico que uso en clase: se esperan **11 bytes**. Se llama a `Receive` con offset 0 y size 11, y devuelve **5**. Entonces se vuelve a llamar con **offset 5 y size 6**; si devuelve 2, se llama con **offset 7 y size 4**; y asi hasta completar.

- **No hay que poner `Sleep` en el medio del loop.** El docente insistio: "recibo no me va a devolver hasta que no reciba un byte", entonces esperar es al pie de la letra **mala practica** y no aporta nada.
- **No usar un `if`, usar un `while`**: puede llegar en mas de dos tandas.
- El mismo patron aplica **tambien a `Send`** si se cambia la configuracion del socket. Con la configuracion de la clase no es necesario, **pero para `Receive` si es obligatorio siempre**.
- Detalle: **el socket es bidireccional**, con dos canales distintos. Se puede mandar y recibir al mismo tiempo, y **no se recibe lo que uno mando**.
- Sobre orden y perdidas: TCP lo resuelve por debajo. Si un paquete se pierde, TCP espera y retransmite; si falla de verdad, **el socket tira excepcion**, no llega un mensaje truncado silencioso a esta capa.

## Avisos y consignas de esta clase
- **Los codigos estan en Teams**, en un zip: los de threads y los de sockets. Se corresponden con lo visto en clase.
- **Tarea de laboratorio**: implementar el patron de recepcion completa. Hay varias soluciones validas.
- Lo que se puede tener andando ya: cliente y servidor, la interfaz del cliente, la logica y el acceso a datos del servidor, y la conexion entre ambos.

## Connections
- Tema: [[Tema 2 - Sockets y protocolos]]
- Evaluacion: [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]] · [[Reglas y prohibiciones del curso PDR 2026]]
- Errores: [[Errores caros en Programacion de Redes]]
- Anterior: [[Clase 2026-09-03 - Minimizar bloqueos y sockets en detalle]]
