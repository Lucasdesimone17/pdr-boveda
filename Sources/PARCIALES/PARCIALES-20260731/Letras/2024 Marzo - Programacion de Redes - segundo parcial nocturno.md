Fecha: 24/06/2024

Evaluación: Parcial

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Nocturno

Puntaje Máximo:     35 Puntos

Página 1 de 2

Ejercicio 1

Responda brevemente los siguientes puntos:

A. Explique la diferencia entre un proceso y un hilo. De un ejemplo de una tecnología para comunicar

dos procesos.

B. ¿Qué ventaja proporciona Task sobre Threads?
C. Realice o comente los pasos necesarios del código de la conexión (utilizando sockets) de un

cliente con un servidor que se encuentra en la IP 192.168.3.43 en el puerto 15000.

Ejercicio 2

Dado el siguiente código:

int acumulado = 0;

static void Acumular(int n){
acumulado += n;

}

Realice y modifique el código necesario para:

● Disparar dos hilos, uno que sume 4  al acumulado y otro que sume 7 al acumulado.
● Luego imprimir el acumulado.
● Evitar ocasionar condiciones de carrera.

Ejercicio 3

El siguiente código intenta imprimir el contenido de una página web. Al ejecutarlo no tira ninguna
excepción pero no funciona como esperado.

using system.data.sqlclient;

DBClient client = new DBClient();
string consulta = “SELECT count(*) FROM v_arbolado where v_arbolado.nombre = ‘PLATANO’;”;
var resultadoDeConsulta = client.GetResultadoAsync(consulta);
Console.WriteLine(resultadoDeConsulta);

Comente qué problema presenta y cómo lo resolvería

Segundo Parcial Programación de Redes

Fecha: 24/06/2024

Evaluación: Parcial

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Nocturno

Puntaje Máximo:     35 Puntos

Página 2 de 2

Ejercicio 4

Un empresa de repartos de productos lo contrata para diseñar un sistemas con los siguientes
requerimientos:

Página web
Los clientes pueden ver y comprar productos desde la web. La página web se encuentra en un servidor en
el local central.

Aplicación repartidores
Los repartidores tienen una aplicación de celular que debe enviar la ubicación en tiempo real al servidor y
que debe permitir confirmar cuando un pedido sea entregado.

Servidor de logs
El servidor principal envía logs a otro servidor externo para que sean almacenados.

Para cumplir estos requerimientos se requiere utilizar mínimo 3 de las siguientes tecnologías: SOCKETS,
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

