Fecha: 24/06/2024

Evaluación: Parcial

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Matutino

Puntaje Máximo:     35 Puntos

Página 1 de 2

Ejercicio 1

Responda brevemente las siguientes preguntas

A. Explique la diferencia entre un proceso y un hilo. De un ejemplo de una tecnología para comunicar

dos procesos.

B. Explique qué ventajas tiene usar asincronía y la clase Task en vez de utilizar directamente hilos.
C. Explique las diferencias entre los patrones de MOM: Competing Consumers y Publish/Subscribe

Ejercicio 2

Dado el siguiente código:

int total = 0;

static void SumarAlTotal(int n){

total += n;

}

Realice y modifique el código necesario para:

● Disparar dos hilos, uno que sume 5  al total y otro que sume 8.
● Luego imprimir el total
● Evitar ocasionar condiciones de carrera

Ejercicio 3

El siguiente código intenta imprimir el contenido de una página web. Al ejecutarlo no tira ninguna
excepción pero no funciona como esperado.

using System.Net.Http;

HttpClient client = new HttpClient();
var contenidoDeLaPagina = client.GetStringAsync("https://www.ort.edu.uy/");
Console.WriteLine(contenidoDeLaPagina);

Comente qué problema presenta y cómo lo resolvería

Segundo Parcial Programación de Redes

Fecha: 24/06/2024

Evaluación: Parcial

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Matutino

Puntaje Máximo:     35 Puntos

Página 2 de 2

Ejercicio 4

Un comercio los contrata para diseñar un sistemas con los siguientes requerimientos:

Página web
Los clientes pueden ver y comprar productos desde la página web del comercio. La página web se
encuentra en un servidor en el comercio.

Aplicación repartidores
Los repartidores tienen una aplicación de celular que debe enviar la ubicación en tiempo real al servidor y
que debe permitir confirmar cuando un pedido sea entregado.

Servidor de logs
El servidor principal envía logs a otro servidor externo para que sean almacenados.

Para cumplir estos requerimientos se requiere utilizar mínimo 3 de las siguientes tecnologías sockets,
MOM, gRPC, API REST.
Quedando bajo su criterio cómo y cuáles utilizar.

Elija y justifique  las tecnologías a utilizar y para cada una indique:

● gRPC: Especificar archivo .proto
● Sockets: Diagrama del protocolo a utilizar (con un ejemplo)
● MOM: Especificar colas, tipo de exchanges y  los mensajes que se envían (ej. contenido del JSON

o similar)

● API REST: Defina los endpoints ( especificar datos a enviar y posibles respuestas con sus status

code)

Pueden hacer suposiciones de que datos se precisan enviar y recibir.
Pueden usar más de una tecnología para un mismo componente.

Diagrame la solución mostrando los canales de comunicación y los componentes en ambos extremos de
la comunicación.

Segundo Parcial Programación de Redes

