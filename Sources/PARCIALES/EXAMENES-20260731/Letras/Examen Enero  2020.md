| Escuela de Ingeniería  |                             |        |            | Universidad ORT Uruguay  |                |
| ---------------------- | --------------------------- | ------ | ---------- | ------------------------ | -------------- |
| Evaluación             | Examen                      | Grupo  | No aplica  | Fecha                    | 30-Enero-2020  |
| Materia                | Programación de Redes       |        |            |                          |                |
| Carrera                | Ingeniería de Sistemas, ID  |        |            |                          |                |
Condiciones  1 a 100 Puntos, 2 horas desde que se lee la letra, sin material

Ejercicio 1 (35 puntos)
Implemente un servidor que pueda mantener N conexiones de distintos clientes (se deben
verificar desconexiones). Dicho servidor atenderá pedidos entrantes de conexión y, cuando
establezca una conexión, deberá comportarse de la siguiente manera:

a.  Cuando un cliente se conecta, el servidor le asigna un id único y se lo envía al cliente
conectado.
b.  Luego de esto, el cliente puede enviar una solicitud de envío de mensaje a otro cliente,
enviando el id y el texto a enviar.
c.  Cada vez que el mensaje sea entregado a su destinatario, el servidor debe enviar un
mensaje notificando al emisor de que el mensaje fue entregado correctamente.

NOTA:  Se  sugiere  construir  un  protocolo  sencillo  para  soportar  los  tres  comandos
mencionados.

Ejercicio 2 (30 puntos)
•  Defina cada uno de los siguientes conceptos:

| 1.  | Monitores   |     |     |     |     |
| --- | ----------- | --- | --- | --- | --- |
2.
AutoResetEvent
| 3.  | ManualResetEvent  |     |     |     |     |
| --- | ----------------- | --- | --- | --- | --- |
| 4.  | Proceso           |     |     |     |     |
5.
Thread
| 6.  | Task  |     |     |     |     |
| --- | ----- | --- | --- | --- | --- |

•  Explique en que entorno es más conveniente utilizar Semáforos y en cual es preferible
utilizar locks.

Ejercicio 3 (10 puntos)
Explique que es MOM y que es Remoting. ¿Cuáles son las principales diferencias entre uno y el
otro? Explique en detalle. ¿En qué casos sería propicio interconectar dos sistemas mediante
una tecnologia o la otra?
|     |     |     |     |     |     |
| --- | --- | --- | --- | --- | --- |

Escuela de Ingeniería Universidad ORT Uruguay
Ejercicio 4 (15 puntos)
class NotThreadSafe
{
static int divider=2;
static void Go(string name, int number)
{
if (divider != 0)
{
Console.WriteLine (number/divider); divider = 0;
Console.WriteLine($”{name} was here”);
}
}
}
¿Qué errores podría presentar el código visto arriba en caso de ejecutarse en paralelo?
Explique en sus palabras y reescriba el mismo (manteniendo la funcionalidad) para solucionar
estos problemas.
Ejercicio 5 (10 puntos)
Un servicio de streaming quiere que usted diseñe las URLs para una arquitectura REST. De
momento, lo necesario es:
1. Operaciones CRUD con usuarios y películas
2. Filtrar a los usuarios por fecha de nacimiento
3. Filtrar películas por género, director y / o año de lanzamiento.
4. Interesa también obtener un listado de que películas vio cada usuario o su inversa,
dada una película que usuarios la vieron.
Diseñe sólo las URLs para cubrir estos requerimientos