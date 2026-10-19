---
date: 2026-09-15
type: synthesis
tags:
  - pdr
  - sockets
  - tema-2
  - obligatorio-1
  - estilo
ai-first: true
confidence: high
fuente: "Derivado de los 8 proyectos en `Sources/TEORICO/SOCKETS_COMUNICACION_EN_RED/EJEMPLOS_03_SOCKET_EJS/`, cruzados con la letra en `Sources/OBLIGATORIO/Obligatorio 1 Agosto 2026.pdf.md` y las guias en `Sources/TEORICO/SOCKETS_COMUNICACION_EN_RED/SOCKETS/`"
---

# Como escribir sockets al estilo del curso PDR

## For future agent
La conclusion operativa de los ejemplos de codigo de la catedra: **como quieren los docentes que se programe**, en reglas accionables y no en teoria. Sale de leer los 8 proyectos de ejemplo, incluidos los comentarios donde los propios docentes marcan que su codigo esta mal y por que. Es la nota a leer antes de escribir una linea de red en el Obligatorio 1. Vigencia: **as of 2026-09-15**. El analisis fuente por fuente esta en [[Ejemplos de codigo de sockets de clase]].

## La tesis

El material no ensena sockets: ensena **un metodo para construir un protocolo encima de un flujo de bytes**. Todo lo demas es consecuencia. Los ejemplos van agregando una pieza por vez, y el orden en que las agregan es el orden en que hay que construir el obligatorio.

Tres ideas atraviesan todo el material:

1. **TCP no entrega mensajes, entrega bytes.** Toda la complejidad del curso nace de ahi.
2. **Lo que se repite se extrae a `Common`.** Cliente y servidor comparten formato, o no hay comunicacion.
3. **Nada se asume.** Ni que llego todo, ni que se envio todo, ni que el otro sigue vivo.

## Las 10 reglas

### 1. Nunca confiar en un solo `Receive` ni en un solo `Send`

Es la regla central. `Receive` devuelve **lo que haya disponible ahora**, no lo que se pidio. Siempre en loop acumulando hasta completar.

```csharp
public byte[] Receive(int dataLength)
{
    byte[] response = new byte[dataLength];
    int offset = 0;
    while (offset < dataLength)
    {
        int received = _socket.Receive(response, offset, dataLength - offset, SocketFlags.None);
        if (received == 0)
        {
            throw new SocketException();
        }
        offset += received;
    }
    return response;
}
```

Lo mismo para `Send`: devuelve cuantos bytes acepto, que puede ser menos que los pedidos. `03_Sockets_02` tiene el bug a proposito y `03_Sockets_03` lo corrige: la comparacion entre esos dos archivos es el examen.

### 2. `received == 0` significa desconexion, no "no habia nada"

Es la unica senal confiable de que el otro extremo cerro. Cortar el loop, cerrar el socket, terminar el hilo. Nunca ignorarlo ni reintentar.

### 3. Largo fijo primero, datos despues

El unico framing que ensenan: una cabecera de tamano conocido que dice cuanto viene, y despues exactamente esa cantidad. `Protocol.LargoFijo = 4` para un `int`, `LargoFijoArchivo = 8` para un `long`.

La letra del obligatorio lo formaliza como `HEADER(3) + CMD(2) + LARGO(4) + DATOS(variable)`, que es la misma idea con un campo de tipo agregado, igual que en `03_Sockets_03`.

### 4. Ni un numero magico en el codigo de red

Todos los largos van como constantes en una clase `Protocol` dentro de `Common`. Si el `4` aparece escrito en el cliente y en el servidor por separado, esta mal aunque funcione.

### 5. Un `Thread` por cliente, y el `Accept` en loop

El patron literal de `03_Sockets_01`:

```csharp
while (running)
{
    Socket client = listener.Accept();
    new Thread(() => HandleClient(client)).Start();
}
```

`Thread`, no `Task`: lo prohibe la letra explicitamente. Ver [[Reglas y prohibiciones del curso PDR 2026]].

### 6. `while (true)` es un error, y los docentes lo escriben en sus propios ejemplos

Tres archivos distintos traen el comentario. La correccion que dan es textual: *"poner una variable booleana que empiece en false y aca pasarla a true"*. En el obligatorio pesa doble, porque **SR4 exige cierre controlado del servidor** y un `while(true)` no se puede cortar desde la consola.

Version esperada: un `bool` o un flag compartido que el comando de cierre baja, mas el `listener.Stop()` para desbloquear el `Accept`.

### 7. Configuracion afuera del binario

`App.config` con `appSettings`, leido por un `SettingsManager` detras de `ISettingsManager`. IP, puerto y **el tiempo de procesamiento de reservas** del Obligatorio 1 van ahi. La letra lo pide: portable entre maquinas sin recompilar.

Es ademas el unico lugar del material donde los docentes muestran una interfaz con su implementacion, o sea que ese es el nivel de abstraccion que esperan.

### 8. Puerto local del cliente en `0`

Para que varios clientes corran en la misma maquina, que es exactamente el escenario de la defensa en el laboratorio. Con puerto fijo, el segundo cliente no levanta. El `Bind` en el cliente es opcional; si se hace, va con `0`.

### 9. `Shutdown` antes de `Close`

En los ocho ejemplos, sin excepcion:

```csharp
socket.Shutdown(SocketShutdown.Both);
socket.Close();
```

### 10. Lo grande va por partes

Archivos e imagenes en bloques de tamano fijo, con la ultima parte calculada aparte. `Protocol.MaxFileSizePart = 32768` y `CalcularCantidadDePartes(long)`. Para CR2 y CR8 del obligatorio, que permiten imagen del vehiculo.

## Como se ve la solucion que esperan

Del ejemplo `03_Sockets_04` y del `File_Transfer` sale la forma de organizar los proyectos:

```
Common/          <- referenciado por cliente y servidor
  Protocol.cs           constantes y formato de trama
  NetworkHelper.cs      Send y Receive completos
  FileStreamHelper.cs   lectura y escritura por partes
  ISettingsManager.cs
  SettingsManager.cs
Server/
Client/
```

`Common` es el contrato. Si una constante de protocolo vive fuera de ahi, esta duplicada.

## Estilo de codigo observado

- **Nombres en ingles para las clases, comentarios en espanol.** Mezcla de ambos idiomas en variables, sin criterio uniforme.
- **Metodos estaticos privados** para los helpers dentro de `Program` en los ejemplos chicos; **clases con constructor** cuando la pieza se comparte (`NetworkHelper` recibe el `Socket` en el constructor y lo guarda en un `readonly`).
- **`try`/`catch (SocketException)`** alrededor de cada operacion de red, con el `finally` cerrando el socket.
- **`using`** para todo `FileStream`.
- **Comentarios que explican el por que**, no el que. Los ejemplos estan densamente comentados y esa es la expectativa en la defensa.

## Lo que el material no resuelve y hay que resolver igual

| Hueco | Por que importa | Donde se decide |
|---|---|---|
| Cierre controlado del servidor | SR4 lo exige y ningun ejemplo lo hace | Diseno propio |
| Respuesta del servidor al cliente | Todos los ejemplos son unidireccionales; el obligatorio necesita request/response | Diseno del protocolo |
| Orden de red consistente | `03_Sockets_03` usa `HostToNetworkOrder`, el resto usa `BitConverter` crudo. El material se contradice | `TBD` - preguntar en clase |
| Concurrencia sobre el estado compartido | Los ejemplos no comparten datos entre hilos; el obligatorio si (stock, reservas) | [[Tema 1 - Programacion multihilo]] |

## Enlaces

- [[Ejemplos de codigo de sockets de clase]] - el analisis proyecto por proyecto del que sale esta nota
- [[Tema 2 - Sockets y protocolos]] - la teoria
- [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]] - la consigna
- [[Reglas y prohibiciones del curso PDR 2026]] - `Thread` obligatorio, `Task`, binary formatter y parsers prohibidos
- [[Errores caros en Programacion de Redes]] - el catalogo de errores, varios demostrados en estos ejemplos
- [[Tema 1 - Programacion multihilo]] - el `lock` que hace falta sobre el estado compartido
