Fecha: 02/12/2024

Evaluación: Parcial

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Matutino

Puntaje Máximo:     35 Puntos

Página 1 de 3

Ejercicio 1

A.  Explique la diferencia entre proceso, hilo y programa. Ejemplifique.
B.  En su empresa utilizan un MOM donde hay una aplicación Consumidor que procesa mensajes.

Dicho consumidor puede fallar. Explique una posible opción para manejar los mensajes en dicho
caso.

Ejercicio 2

Para cada afirmación indique Verdadero o Falso y justifique

A.  La principal ventaja de utilizar una ThreadPool es que soluciona problemas de mutua exclusión.
B.  En MOM siempre se utiliza una cola por aplicación.
C.  Para comunicar dos aplicaciones con gRPC deben estar en el mismo lenguaje, mientras que con

API REST pueden ser lenguajes distintos.

D.  El código de la derecha es más rápido que el de la izquierda.

HttpClient client = new HttpClient();
var a = client.GetString("a.com");
var b = client.GetString("b.com");
var res = a.Length > b.Length ? a : b;

HttpClient client = new HttpClient();
var a = await client.GetStringAsync("a.com");
var b = await client.GetStringAsync("b.com");
var res = a.Length > b.Length ? a : b;

Console.WriteLine(“Página más larga:”);
Console.WriteLine(res);

Console.WriteLine(“Página más larga:”);
Console.WriteLine(res);

Fecha: 02/12/2024

Evaluación: Parcial

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Matutino

Puntaje Máximo:     35 Puntos

Página 2 de 3

Ejercicio 3

Dado el siguiente código:

Socket socketListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
ProtocolType.Tcp);
int cantidadDeClientesConectados = 0;
// COMPLETAR (1);

while (true)
{

Socket clientSocket = socketListener.Accept();

// COMPLETAR (2)

}

private static void HandleClient(Socket clientSocket)
{

try
{

RecibirMensajes(clientSocket);

}
catch (ClienteDesconectado ex)
{

// COMPLETAR (3)

}

}

Realice y modifique el código necesario para:

1.  En la parte (1) hacer que el servidor escuche en el puerto 5000
2.  En la parte (2) ejecutar la función HandleClient para cada cliente aceptado simultáneamente
3.  En la parte (3) cerrar la conexión correctamente
4.  La variable cantidadDeClientesConectados contiene en todo momento la cantidad de clientes

conectados actuales.

Notas:

●  En .NET no es necesario tener una función Main  para que el código se pueda ejecutar. La

pueden agregar si quieren.

●  Pueden asumir que la función RecibirMensajes esta bien programada y que tira la excepción

ClienteDesconectado cuando el cliente se desconecta.

Fecha: 02/12/2024

Evaluación: Parcial

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Matutino

Puntaje Máximo:     35 Puntos

Página 3 de 3

Ejercicio 4

Una importante empresa internacional de software lo contrata para diseñar el sistema para su más
innovadora idea hasta el momento:

“¿Merendaste?” es una aplicación donde los usuarios pueden avisarle a su círculo de amigos si ya
merendaron ese día, y no solamente eso, sino que también está habilitada la opción de subir fotos de
sus meriendas.
Debe cumplir con los siguientes requerimientos:

Servidor web:
Las personas pueden ver y publicar meriendas en la página web desde su celular.

Servidor analítico:
Es el servidor encargado de recibir las fotos de las meriendas para ser procesadas por una IA con el
fin de validar el cumplimiento de las normas de la empresa. Una vez revisada la imagen, si no cumple
las normas, se bloquea al usuario y se da de baja su publicación.

Servidor estadístico:
Este servidor se encarga de recibir en tiempo real las ubicaciones desde dónde los usuarios publican
su merienda con el fin de mejorar la estrategia de marketing de la empresa a futuro.

Para implementar este sistema, deberá utilizar 3 de las siguientes tecnologías: Sockets, MOM, gRPC, API
REST. Queda bajo su criterio cómo y cuáles tecnologías utilizar. Justiﬁque su elección y para cada
tecnología seleccionada, realice lo siguiente:

●  gRPC: Escriba el archivo .proto que describe los servicios y mensajes.
●  Sockets: Proporcione un diagrama del protocolo de comunicación (mensajes, respuestas).
●  MOM: Especiﬁque las colas a utilizar, tipo de exchanges y los mensajes que se envían (ej.

formato JSON o XML).

●  API REST: Deﬁna los endpoints necesarios para el sistema, incluyendo los datos que se enviarán,

las posibles respuestas y sus status codes.

Además, realice lo siguiente:

1.  Diagrame la solución mostrando los canales de comunicación y los componentes involucrados

en cada extremo.

2.  Indique en cada canal quién actúa como cliente/servidor o productor/consumidor (según

corresponda).

Aclaraciones:

●  Puede realizar suposiciones necesarias, por ejemplo, de que datos se precisan enviar y recibir.
●  Puede utilizar más de una tecnología para un mismo componente, si lo considera apropiado.

