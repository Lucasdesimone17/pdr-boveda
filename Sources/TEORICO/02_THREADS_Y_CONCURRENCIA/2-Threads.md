Threads
Multihilos y programación asincrónica

Repaso/Introducción

Multiprogramación
La figura deja claro que, si los procesos gastan 80 por
ciento de su tiempo esperando las operaciones de
E/S, por lo menos debe haber 10 procesos en
memoria a la vez para que el desperdicio de la CPU
esté por debajo de 10%.
Los tiempos de espera de E/S de 80 por ciento o más no son poco
comunes

Proceso
En este modelo, todo el software ejecutable en la computadora, que algunas veces incluye al sistema operativo, se organiza
en varios procesos secuenciales(procesos, para abreviar). Un proceso no es más que una instancia de un programa en
ejecución, incluyendo los valores actuales del contador de programa, los registros y las variables

Hilo

Hilo
En los sistemas operativos tradicionales, cada proceso tiene un espacio de
direcciones y un solo hilo de control. De hecho, ésa es casi la definición de un
proceso. Sin embargo, con frecuencia hay situaciones en las que es conveniente
tener varios hilos de control en el mismo espacio de direcciones que se ejecuta en
cuasi paralelo, como si fueran procesos (casi) separados (excepto por el espacio de
direcciones compartido).
Un hilo es simplemente una tarea que puede ser ejecutada al mismo tiempo que
otra tarea.
¿Por qué alguien querría tener un tipo de proceso dentro de otro proceso? Resulta
ser que hay varias razones de tener estos miniprocesos, conocidos como hilos

Hilo
“a diferencia de un proceso, las hebras (hilos) que pertenecen a un mismo
proceso comparten la sección de código, la sección de datos y otros recursos
proporcionados por el sistema operativo, como los manejadores de los archivos
abiertos o las banderas. Precisamente, esta diferencia con respecto a un proceso
es lo que supone su principal ventaja.”
“la idea de hilo surge de la posibilidad de compartir recursos y permitir el acceso
concurrente a esos recursos”

...hilos
La principal razón de tener hilos es que en muchas
aplicaciones se desarrollan varias actividades a la vez.
Algunas de ésas se pueden bloquear de vez en cuando.
Al descomponer una aplicación en varios hilos
secuenciales que se ejecutan en cuasi paralelo, el
modelo de programación se simplifica
Por último, los hilos son útiles en los sistemas con varias
CPUs, en donde es posible el verdadero paralelismo.

Entendiendo hilos
Si el programa tuviera solo un hilo, entonces cada vez que iniciara un respaldo en el disco se ignorarían los comandos del teclado y el ratón
hasta que se terminara el respaldo.

Proceso, como hilo de ejecución
El hilo tiene un contador de programa que lleva el registro de cuál instrucción se va a
ejecutar a continuación. Tiene registros que contienen sus variables de trabajo actuales.
Tiene una pila, que contiene el historial de ejecución, con un conjunto de valores para
cada procedimiento al que se haya llamado, pero del cual no se haya devuelto todavía.

Hilos, motivos...
Lo que agregan los hilos al modelo de procesos es permitir que se lleven a cabo varias ejecuciones en el mismo entorno del proceso, que son en gran parte independientes unas de las otras. Tener varios hilos ejecutándose en paralelo en un proceso es algo similar a tener varios procesos ejecutándose en paralelo en una computadora.

mismo espacio de direcciones, lo cual significa que también comparten las mismas variables globales.

Cambio de contexto
- Se deja de ejecutar el proceso A, se salva su
estado… se cede el control al núcleo del SO
(sucede un cambio de contexto) .
Pasando de modo usuario a supervisor.
Estudia si el proceso B está preparado para su
ejecución y si es así, realiza el cambio de
contexto correspondiente. Pasando de
supervisor a usuario. Y se pone a ejecutar el
proceso B.

Condiciones de carrera
Situaciones como ésta, en donde dos o más procesos están leyendo o escribiendo
algunos datos compartidos y el resultado final depende de quién se ejecuta y
exactamente cuándo lo hace, se conocen como condiciones de carrera.
Depurar programas que contienen condiciones de carrera no es nada divertido.
Los resultados de la mayoría de las ejecuciones de prueba están bien, pero en
algún momento poco frecuente ocurrirá algo extraño e inexplicable.

Exclusión mutua,
La clave para evitar problemas en situaciones en las que se involucran la memoria
compartida, los archivos compartidos y todo lo demás compartido es buscar alguna
manera de prohibir que más de un proceso lea y escriba los datos compartidos al mismo
tiempo. Dicho en otras palabras, lo que necesitamos es exclusión mutua.
Esa parte del programa en la que se accede a la memoria compartida se conoce como
región crítica o sección crítica. Si pudiéramos ordenar las cosas de manera que dos
procesos nunca estuvieran en sus regiones críticas al mismo tiempo, podríamos evitar
las carreras.

Recurso
Refiere a un elemento del hardware, que tiene una potencialidad de cómputo. Esa
capacidad bruta de cómputo, se transforma en neta cuando el sistema operativo
propicia el uso de dicho dispositivo.
RSR
Es un recurso serialmente reusable, es decir, un recurso que se puede usar muchas
veces, un proceso por vez, por ejemplo una impresora.

Programación Concurrente
Refiere al estudio de técnicas de programación que expresan el paralelismo entre
tareas, aplicado a la sincronización de procesos.

Multithreading

Multithreading
Una aplicación multithreading es aquella que divide el trabajo en diferentes hilos o unidades de
ejecución.
Beneficia el diseño, porque cada hilo suele capturar una única abstracción o idea. Favorece el
Mantenimiento del SW.
Aplica a la estrategia “Divide y vencerás”.
Mejora la utilización del tiempo de la CPU
“El procesador será responsable de dar a cada hilo de ejecución una cierta cantidad de tiempo en la
que ejecutarse (timeslice, rodaja de tiempo). (...) y el programador no tiene que preocuparse cuándo
ceder el control para que el resto de los hilos de ejecución puedan ejecutarse.”#3

Aplicación de Sleep(), wait(),suspend() o una solicitud de I/O
Creado pero no se le
aplicó Start.
Para conocer el estado del thread…
Console.WriteLine("ThreadState: {0}",thr1.ThreadState);

Ejemplo 1
¿Qué se ejecuta primero?
¿Cuál es la salida?

¿Cuál función se va a ejecutar
primero?
No sabemos, lo decide el framework de .NET y el scheduler del sistema operativo
cada vez que se ejecuta el programa, no importa a que Thread se le hizo el .Start()
primero. El framework decide cuando empezar a ejecutarse cada Thread y si se están
ejecutando los dos al mismo momento puede pasar que antes de terminarse uno se
ejecute el otro y los resultados en la consola estén mezclados
Hacer nombreThread.Start() no hace que se ejecute el thread en ese momento, le
avisa al framework que el thread esta pronto para ejecutarse y el framework va a
decidir cuando ejecutarlo.

Tipos de hilo
● Hilos de foreground: Cuando el hilo principal llega al fin, si hay un hilo de
foreground hijo en estado Running, se esperará a que terminen todos los
hilos de foreground antes de cerrar el proceso. Los hilos que se crean con
new Thread(función) son por defecto hilos de foreground
● Hilos de background: Cuando el hilo principal llega al fin, si solo quedan hilos
hijos de background, se abortan dichos hilos y el proceso finaliza. Los hilos
que corren usando ThreadPool son hilos de background

Delegation (patrón de diseño)
“En Ingeniería de software, el patrón de diseño de delegación es una técnica en la que un
objeto de cara al exterior expresa cierto comportamiento pero en realidad delega la
responsabilidad de implementar dicho comportamiento a un objeto asociado en una
relación inversa de responsabilidad.” #1
“Por ejemplo, considere la ordenación de una lista de estrellas en una aplicación de astronomía.
Puede decidir ordenar las estrellas por su distancia con respecto a la Tierra, por la magnitud de la
estrella o por su brillo percibido.
En todos estos casos, el método Sort() hace básicamente lo mismo: organiza los elementos en la
lista en función de una comparación. El código que compara dos estrellas es diferente para cada
uno de los criterios de ordenación.” #2

Ejemplo 1
● Notar las librerías importadas
● ThreadStart es el delegado
○ Indica el método a disparar en el thread.
● Inicializa el thread. Toma el delegado
como argumento.

Ejercicio
● ¿Qué cambiaría para lograr la siguiente salida?
● ¿Es posible la siguiente salida?

Ejemplo 2

Flujo

Interacción entre hilos
Ejemplo 3

Gestión de un hilo.
● Detener un hilo en ejecución.
○ int sleeptime = 5000;
thread.Sleep(sleeptime);
○ t.Suspend() {obsoletos}
○ t.Resume() {obsoletos}
○ t.Abort() {obsoletos}

Prioridades…
● Thread.Priority.
○ Highest (Maxima)
○ AboveNormal (por encima de lo normal)
○ Normal
○ BelowNormal (por debajo de lo normal)
○ Lowest (Mínima)
● Tener en cuenta que lo anterior está basado en .NET, pero es el sistema
operativo el que finalmente define la prioridad.
● A modo de ejemplo, en un procesador intel existen de 0 a 31 prioridades.
● Modificar las prioridades puede traer inconvenientes, ejemplo,
○ si tuviéramos una aplicación con dos hilos corriendo internamente realizando cierto trabajo
asincrónico, y decidimos darles una prioridad muy alta. Puede suceder que la interfaz gráfica
resulte demasiado lenta, ya que como hilo en paralelo tendrá menor prioridad respecto a los
otros dos.
○ En caso de tener varios hilos con la misma prioridad, se aplica una asignación de tipo round robin
por un q particular.

Ejemplo 4

Hacer esperar un hilo. Ejemplo 5

comentar el thread.Join() y observar el resultado

Parámetros en un hilo
● Pasaje de parámetros a un hilo
○ thread.Start(object: “hola”) // se le puede pasar un objeto genérico al Start.
No es bueno, ya que no se especifica el tipo de parametro.

Pasaje de Parámetros

Alcance de variables
entre hilos
Ejercicio: Imprimir los números del 1 al 100 usando un for de tal forma que
cada Console.WriteLine sea ejecutado en un hilo diferente
Nota: No es necesario que lo números se impriman en orden

Comportamiento de variables
¿Cuál es la salida?
¿Hay números repetidos?
¿Por qué…?

Al ejecutarlo vemos como tenemos números repetidos y
como hay números que no aparecen
¿Cómo lo podemos solucionar?
Esta relacionado a cuando arranca a ejecutarse un hilo
nuevo. No depende de nosotros sino del sistema operativo y
el framework.
Tambien hay que tener en cuenta a que variables puede
acceder un hilo, en general varios hilos pueden acceder a
las mismas variables globales

Solución
En el código original, cada
iteración del for modifica la
variable i la cual es usada por
todos los hilos, como no
controlamos cuando inicia cada
hilo, es posible que la variable i
sea modificada antes que el
valor anterior haya sido impreso
por pantalla
Al crear una variable local para
cada iteración, nos aseguramos
que no se modifique el valor

Checkpoint!
● ¿En qué se diferencian los procesos de los hilos?
● ¿Para qué sirve el .Start() de Threading?
● ¿Cual es la diferencia entre Thread de Foreground y de Background?
● ¿Como pasamos parametros a una función que queremos ejecutar en un
hilo aparte?
● ¿En qué casos queremos usar Thread.Sleep?

Monitores

Problema
● Existe un método que actualiza una BD el cual no puede ser ejecutado por dos
o más hilos de ejecución al mismo tiempo. Pero tenemos varios hilos que lo
llaman… ¿Cómo garantizamos que solo un hilo pueda invocarlo a la vez?

Ejemplo 6

Monitor
● Permite acceder de manera secuencial a bloques de
código por medio de bloqueos y señales.
● Enter
○ Cuando se ejecuta, intenta obtener un bloqueo exclusivo del objeto.
○ Si otro hilo lo tiene ya bloqueado, el método será inaccesible hasta
que dicho bloqueo haya sido liberado.
● Exit.
○ Se invoca para liberar el bloqueo

Ejemplo 7. Monitor

Problemas del anterior ejemplo. (7)

● El primer hilo no llega a ejecutar el exit (liberar el monitor) por ende, el
segundo hilo nunca obtiene acceso a la zona de mutua exclusión, es decir,
permanece bloqueado indefinidamente.

● Es por ello que se recomienda encerrar mediante try-catch la llamada al
enter y al dentro del finally, el exit.

Limitar la zona de exclusión mutua
● Tener mucho código dentro de un lock o monitor no buena práctica.
● Lo mejor es tener la mínima cantidad de código dentro del lock para limitar
el tiempo que esperan los otros hilos.

Locks

Comportamiento
| lock(myLock)         | Monitor.Enter(myLock); |     |
| -------------------- | ---------------------- | --- |
| {                    | try                    |     |
|     //Critical code  | {                      |     |
| }                    |     // Critical code   |     |
}
finally
{
|     |     Monitor.Exit(myLock); |     |
| --- | ------------------------- | --- |
}

Ejemplo 8

…lock
● Son una abstracción de los
monitores
● Utilizan la clase monitor y
generan el código
respectivo, utilizando try
catch.

…lock
● Locks pueden utilizarse para realizar el patrón Singleton. De forma de
garantizar una única instancia de la clase. #4

Locks Anidados
● lock anidado: en general no son buena práctica:
Funcion1:
lock(a) {
// código
lock(b){
// código
}
}
● puede generarse un deadlock :
Funcion2:
lock(b) {
// código
lock(a){
// código
}
}

Monitores
wait pulse

wait pulse
● equivalente al pedir-devolver de monitores.
● PulseAll – Notifica a todos los thread que están
bloqueados en un Wait.

Ejemplo 9

Ejemplo 9

Resultado
● Ejemplo de
https://www.c-sharpcorner.com/UploadFile/1d42da/wait-and-pulse-method-in-threading-C-Sharp/

Mutex

Mutex
● Es una representación de la primitiva del sistema Win32 Mutex.

Ejemplo 10

Mutex Vs Monitor Vs Lock
● lock es equivalente a utilizar monitores
○ Lock es una instrucción, mutex y monitores hacen referencia a clases.
● Monitores es mucho más rápido que Mutex
● Mutex es una primitiva de sincronización. Permite manejar bloqueos a nivel de
Sistema Operativo (a diferencia de monitores, donde el alcance del bloqueo
depende del objeto donde es instanciado).
● Alcance, mutex me permite sincronizar threads entre aplicaciones.
● Mutex > Monitor > Lock

Semáforos

Semaphore
● Abstracción que permiten coordinar la ejecución entre varios threads.
Funcionamiento:
○ Mantiene un contador.
○ Cada vez que se le solicita el recurso decrementa el contador.
○ Cada vez que se libera el recurso incrementa el contador.
○ Si el contador llega a cero, rechaza los siguientes pedidos de recursos.
● Permite sincronización entre procesos.
● Similar a monitor y a mutex, pero te deja definir un límite de threads que pueden ingresar a la zona de mutua
exclusión (patio).
● El constructor recibe dos parámetros.
○ primero, el número de lugares disponibles inicial.
○ máximo número de lugares disponible.
● P(x) = WaitOne()
● V(x) = Release()

Ejemplo 11

● Proba de cambiar la línea y comente el
comportamiento
static Semaphore sem = new Semaphore(0, 3);

Comportamiento
Semaphore sem = new
Semaphore (1, 1);
lock (obj){
Procesar();
Sem.waitOne();
}
Procesar();
Sem.release();

Ejemplo extraído de
https://www.c-sharpcorner.com/UploadFile/1d42da/threadi
ng-with-semaphore-in-C-Sharp/

Material en Aulas
● Práctico 1 - Threads y exclusión mutua
● Guías:
○ Introducción a Threads
○ Ejercicio de Threads
○ Threads - Sleep y Join

Otros mecanismos de exclusión mutua
● SemaphoreSlim: Implementación más “lightweight” de los semáforos
● AutoResetEvent y ManualResetEvent: Son mecanismos de
sincronización de hilos (al igual que Monitores + PulseAll/Pulse)
● Barrier: También sincronización (Thread signaling)

Patrones
(material extra)

Patrones
● Son útiles para implementar mecanismos de notificación de eventos y despacho
de mensajes.
● Permiten controlar de forma automática la cantidad de recursos que se
consumen ya que los threads son creados al inicio y se mantienen a lo largo de
toda la ejecución.

Patrón Tarea
- Permite encapsular todo lo relativo a la
ejecución de una tarea desatendida:
- Creación del thread que procesa la
tarea.
- Sincronización con el thread que procesa
la tarea.

Ejemplo 12

Delegados
● Se busca disparar un “callback” al finalizar la ejecución de un thread.

Ejemplo 13

Eventos
● Un evento es un señal que envía un objeto cuando ocurre una acción o
condición.
● Los eventos son capturados y manejados por otros objetos interesados a través
de métodos.
● El remitente desconoce el tipo y el método que se ejecuta.
● En .NET Framework los eventos son implementados mediante delegados.

Patrón Tarea con Evento
Permite encapsular todo lo relativo a la ejecución de una tarea desatendida:
Creación del thread que procesa la tarea.
Sincronización con el thread que procesa la tarea.
Notificación de finalización mediante el disparo de un evento.

Ejemplo 14

BackgroundWorker Class
Clase que permite gestionar hilos.

Ejemplo 15

Backgroundworker
● Los hilos se ejecutan concurrentemente.
● Al finalizar se ejecuta FinishFunction

Citas
#1 https://es.wikipedia.org/wiki/Delegation_(patr%C3%B3n_de_dise%C3%B1o)
#2 https://docs.microsoft.com/es-es/dotnet/csharp/delegates-overview
#3 A fondo C#. ver bibliografia
#4 https://www.c-sharpcorner.com/UploadFile/8911c4/singleton-design-pattern-in-C-Sharp/

Bibliografia
● “Inside C#” Tom Archer Andrew Whitechapel
https://sisbibliotecas.ort.edu.uy/cgi-bin/koha/opac-detail.pl?biblionumber=233
43&query_desc=kw%2Cwrdl%3A%20Inside%20tom
● “A fondo C#” 0735612889 ISBN
● Repaso
○ Sistemas Operativos Modernos. Tanenbaum
https://sisbibliotecas.ort.edu.uy/cgi-bin/koha/opac-detail.pl?biblionumber=4770