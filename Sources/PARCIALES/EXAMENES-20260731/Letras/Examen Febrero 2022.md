Escuela de Ingeniería

Universidad ORT Uruguay

Evaluación
Materia
Carrera
Condiciones

Grupo

Examen
Programación de Redes
Ingeniería de Sistemas, ID
1 a 100 Puntos, 2 horas desde que se lee la letra, sin material

No aplica

Fecha

14-Febrero-2022

Ejercicio 1 (40 puntos)

Implemente  un  servidor  que  pueda  mantener  N  conexiones  de  distintos  clientes  (se  deben
verificar  desconexiones).  Dicho  servidor  atenderá  pedidos  entrantes  de  conexión  y,  cuando
establezca una conexión, deberá comportarse de la siguiente manera:

a.  Cuando un cliente se conecta, el servidor le asigna un id único.
b.  Luego  de  esto,  el  servidor  envía  un  mensaje  de  keep  alive  al  cliente  con  un  formato

especificado por el alumno.

c.  Si luego de cierto tiempo el cliente no contesta, el servidor lo desconecta (el tiempo es

a criterio del alumno).

NOTA: Se sugiere construir un protocolo sencillo para soportar la comunicación mencionada

Ejercicio 2 (25 puntos)

•  Defina cada uno de los siguientes conceptos:

1.  Proceso
2.  Thread
3.  Task
4.  Asincronía
5.  Paralelismo

•  Explique la relación que existe entre Asincronía y Paralelismo

Ejercicio 3 (10 puntos)

Explique  que  es  gRPC  y  cuando  lo  usaría  en  un  entorno  de  aplicaciones  distribuidas.  ¿Qué
diferencia guarda con una comunicación basada en sockets y que ventajas y desventajas tiene
frente a esta?

Ejercicio 4 (15 puntos)

class NotThreadSafe {

    static string varToCheck = “hola”;

    static string name = “pepe”;

static void Run() {

      if (varToCheck == “pepe”)

{

varToCheck = name;

Console.WriteLine($”{varToCheck} is  the value”);

Escuela de Ingeniería

Universidad ORT Uruguay

} else {

Console.WriteLine($”{varToCheck} is the first value”);

varToCheck += name;

Console.WriteLine($”{varToCheck} is the second value”);

}

  }

}

¿Qué  errores  podría  presentar  el  código  visto  arriba  en  caso  de  ejecutarse  en  paralelo?
Explique en sus palabras y reescriba el mismo (manteniendo la funcionalidad) para solucionar
estos problemas.

Ejercicio 5 (10 puntos)

Un  servicio  consultas  de  reservas  para  un  consultorio  quiere  que  usted  diseñe  las  URLs  para
una arquitectura REST. De momento, lo necesario es:

1.  Operaciones CRUD con usuarios y reservas
2.  Filtrar a los usuarios por fecha de reserva
3.  Listado de reservas de un usuario.

Diseñe sólo las URLs para cubrir estos requerimientos

