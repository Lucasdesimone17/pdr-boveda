---
date: 2026-09-15
type: literature
tags:
  - pdr
  - sockets
  - tema-2
  - codigo
  - obligatorio-1
ai-first: true
confidence: stated
fuente: "`Sources/TEORICO/SOCKETS_COMUNICACION_EN_RED/EJEMPLOS_03_SOCKET_EJS/` - 8 proyectos de ejemplo entregados por la catedra (archivo `03_Socket_ejs.zip`), ingeridos el 2026-09-15"
---

# Ejemplos de codigo de sockets de clase

## For future agent
Analisis de los **8 proyectos de ejemplo de sockets** que la catedra reparte como material de clase. No son teoria: son el codigo que los docentes escriben en vivo y contra el cual corrigen, asi que definen el estilo esperado en el Obligatorio 1. La nota mapea cada ejemplo con lo que ensena y senala que partes estan marcadas por los propios docentes como "no hagan esto". Vigencia: **as of 2026-09-15**; el material es de la catedra y no cambia dentro del semestre. La conclusion operativa vive en [[Como escribir sockets al estilo del curso PDR]].

## Que hay en la carpeta

Ocho proyectos, en orden pedagogico creciente. Cada uno agrega exactamente un concepto sobre el anterior.

| Proyecto | Que agrega | API |
|---|---|---|
| `03_Sockets_00` | Conexion minima: `Bind`, `Listen(1)`, `Accept` bloqueante. Un solo cliente, no se transmite nada | `Socket` |
| `03_Sockets_01` | Servidor multi-cliente: `while(true)` con `Accept` y **un `Thread` por cliente**. Deteccion de desconexion con `Poll` | `Socket` + `Thread` |
| `03_Sockets_02` | Primer framing: envia **4 bytes de largo** y despues los datos. Aparece el bug de `Receive` parcial | `Socket` |
| `03_Sockets_03` | Framing tipado: cabecera `TIPO(3) + LARGO(4)` y despacho por `switch`. Helpers `SendAll` / `ReceiveAll`. Orden de red | `Socket` |
| `03_Sockets_04` | Configuracion externa via `App.config` y un `SettingsManager` detras de `ISettingsManager`. Proyecto `Common` compartido | `Socket` + `ConfigurationManager` |
| `03_Sockets_TCP_01` | Lo mismo con la API de alto nivel: `TcpListener` / `TcpClient` / `NetworkStream`. Constante `Protocol.WordLength` en `Common` | `TcpListener` |
| `03_Socket_02_Simple_File_Transfer` | Transferencia de archivos por partes de 32 KB. `NetworkHelper`, `FileStreamHelper`, `Protocol` con el calculo de partes | `Socket` |

## El hilo conductor

Los ejemplos cuentan una sola historia, y es la del Obligatorio 1.

1. **Conectar** (`00`) - la mecanica de `Socket`, `IPEndPoint`, `Bind`, `Listen`, `Accept`.
2. **Escalar a varios clientes** (`01`) - `Accept` en loop, `Thread` por conexion.
3. **Delimitar mensajes** (`02`) - TCP es un flujo continuo, hay que decir donde termina cada mensaje.
4. **Tipar mensajes** (`03`) - una cabecera que dice **que** viene, no solo cuanto.
5. **Sacar la configuracion del codigo** (`04`) - IP y puerto fuera del binario.
6. **Subir de nivel de abstraccion** (`TCP_01`) - `NetworkStream` en vez de `Socket` crudo.
7. **Mover datos grandes** (`File_Transfer`) - partir en bloques, helpers reutilizables en `Common`.

Ese orden es el mismo de los requerimientos del [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]]: SR2 es el paso 2, la especificacion de trama es el paso 4, el requerimiento de configuracion externa es el paso 5, y la imagen opcional del vehiculo (CR2, CR8) es el paso 7.

## Lo que los docentes marcan como incorrecto

Los ejemplos traen **comentarios de autocritica escritos por la catedra**. Son la lista de lo que no hay que copiar, y estan en el codigo fuente:

- `while (true) // <= No hagan esto ...` en el loop de `Accept` de `03_Sockets_TCP_01_Server.cs`. Motivo: **SR4 pide cierre controlado del servidor**, y un `while(true)` no se puede cortar.
- `while (true) // Ojo, no es la forma correcta de hacerlo.` en el manejo de cada conexion, mismo archivo.
- `while (true) // No se deberia hacer un while (true)` en `03_Sockets_04_Client/Client.cs`, con la correccion al lado: *"La mejor practica seria poner una variable booleana que empiece en false y aca pasarla a true"*.
- En `03_Sockets_00_Server.cs`, tres preguntas abiertas al final del archivo que son material de parcial: que pasa con varios clientes, que pasa si el cliente fija su puerto local, y que pasa con el tercer cliente si `Listen(1)`.

`03_Sockets_02` **contiene el bug a proposito**: llama a `client.Receive(buffer2, 0, length, ...)` una sola vez y asume que llegaron los `length` bytes. `03_Sockets_03` lo corrige con `ReceiveAll`. La comparacion entre esos dos archivos es la leccion central del tema, y se dio en clase el 08-09 ([[Clase 2026-09-08 - Protocolo propietario, framing y Receive parcial]]).

## Las piezas reutilizables

Tres clases aparecen una y otra vez, siempre en un proyecto `Common` compartido por cliente y servidor:

**`NetworkHelper`** - envuelve un `Socket` y expone `Send(byte[])` y `Receive(int)`, ambos con el loop de acumulacion. Es la pieza que resuelve el envio y la recepcion parcial de una vez y para siempre.

**`Protocol`** - constantes del formato: `LargoFijo = 4`, `LargoFijoArchivo = 8`, `MaxFileSizePart = 32768`, y `CalcularCantidadDePartes(long)`. Ningun numero magico queda suelto en el codigo de red.

**`FileStreamHelper`** - `Read(path, offset, length)` y `Write(fileName, data)` con `using` y loop de lectura. Para las imagenes de los vehiculos.

**`SettingsManager` / `ISettingsManager`** - lectura de `App.config` detras de una interfaz. Es el unico lugar del material donde los docentes muestran inyeccion de dependencias, y coincide con el requerimiento de configuracion externa de la letra.

## Detalles tecnicos que aparecen

- **Puerto local del cliente en `0`**: en `03_Sockets_00_Client` esta fijo en 15000 y el comentario dice *"cambiar a 0"*. Con puerto fijo, el segundo cliente en la misma maquina no puede levantar. Con `0` el sistema operativo asigna uno libre.
- **`Bind` en el cliente es opcional**: dicho textual en `03_Sockets_04_Client/Client.cs`.
- **Orden de red**: `03_Sockets_03` usa `IPAddress.HostToNetworkOrder` / `NetworkToHostOrder` para los enteros. Los demas ejemplos usan `BitConverter` directo, que es little-endian de la maquina. **Contradiccion sin resolver en el material**: los ejemplos no son consistentes entre si. `TBD` cual exigen en la correccion.
- **`Shutdown(SocketShutdown.Both)` antes de `Close()`**: en todos los ejemplos, sin excepcion.
- **Deteccion de desconexion**: `received == 0` significa que el otro extremo cerro. `03_Sockets_01` ademas muestra `socket.Poll(1000, SelectMode.SelectRead) && socket.Available == 0`.
- **`Encoding.UTF8` para texto, `Encoding.ASCII` para la cabecera de tipo** en `03_Sockets_03`.

## Huecos del material

- Ningun ejemplo implementa el **cierre controlado del servidor** que pide SR4. Los docentes senalan el problema con el comentario pero no dan la solucion.
- Ningun ejemplo maneja **respuestas del servidor al cliente**: todos son unidireccionales. El Obligatorio 1 necesita request/response.
- `03_Sockets_04_Server` **pierde el multi-cliente**: vuelve a un solo `Accept` sin `Thread`. Es un retroceso deliberado para aislar el tema de configuracion.
- `03_Sockets_04_Server/Server.cs` hace `Encoding.UTF8.GetString(buffer)` sobre el **buffer completo de 1024 bytes**, no sobre los `bytesRecibidos`. Es otro error que queda en el codigo sin marcar.

## Enlaces

- [[Como escribir sockets al estilo del curso PDR]] - la conclusion operativa: como hay que programar segun este material
- [[Tema 2 - Sockets y protocolos]] - la teoria que estos ejemplos ilustran
- [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]] - la consigna que estos ejemplos preparan
- [[Errores caros en Programacion de Redes]] - los errores que estos ejemplos demuestran
- [[Clase 2026-09-08 - Protocolo propietario, framing y Receive parcial]] - la clase donde se dio el `Receive` parcial
- [[Reglas y prohibiciones del curso PDR 2026]] - `Thread` obligatorio, `Task` prohibido
- [[Como escribir sockets al estilo del curso PDR]] - las 10 reglas de estilo que salen de este analisis
