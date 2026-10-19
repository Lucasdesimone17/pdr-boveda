Stream

Deﬁniciones

● Stream es una estructura de datos. Una secuencia de
datos potencialmente ilimitada, pedidos a demanda.

● Stream es un objeto que permite el ﬂujo de datos
entre un programa y alguna dispositivo de E/S (ej.
archivos).

● Stream es una abstracción de una secuencia de bytes,
como un archivo, un dispositivo de entrada/salida, un
canal de comunicación interprocesos o un canal
TCP/IP.

Ejemplos de streams

● standard output stream (Console.WriteLine)
● standard input stream (Console.ReadLine)

● NetworkStream
● FileStream

Stream

● .NET ofrece un framework que permite la lectura y escritura sincrónica y

asincrónica desde y hacia datos y archivos.

● Heredan de la clase Stream
●  La clase Stream y sus clases derivadas proporcionan una visión genérica de
diferentes tipos de entrada y salida, aislando al programador de los detalles
especíﬁcos del sistema operativo y sus dispositivos subyacentes

Stream

● FileStream

○

lectura y escritura de archivos

● MemoryStream

○

buffer temporal accesible en la memoria, para guardar datos

● BufferedStream

○

añade un buffer que puede utilizarse conjuntamente con otros streams, como ser FileStream

● NetworkStream

○

Representa un stream a través de la red. (Reﬁere a Sockets)

● CryptoStream

○ Contiene funcionalidades de criptografía

● Existen también, StringReader, StringWriter, BinaryReader, BinaryWriter,

IsolatedStorageFilStream,PipeStream, etc..

Generalmente, por ser Streams se permite:

● Lectura (Reading)

○ Transferir información desde un stream hacia una estructura de

datos (como ser un array de bytes)

● Escritura (Writing)

○ Transﬁeren información hacia un Stream.

● Búsqueda (Seeking)

○ búsqueda y posicionamiento dentro de un stream. (no

implementada para networkstream)

Debido a ello, se puede consultar por la existencia de funcionalidades:

● CanRead,CanWrite,Canseek.

