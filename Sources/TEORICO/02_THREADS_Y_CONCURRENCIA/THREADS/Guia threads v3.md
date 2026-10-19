Guía sobre Threads
En esta guía vas a aprender sobre:
1. Creación de hilos
2. Ejecución de hilos
3. Creación de hilos con parámetros
4. Hilos de foreground vs hilos de background
1 - Creación de hilos
Primero partimos del siguiente código:
PrintTwos();
PrintThrees();
Console.ReadLine();
static void PrintTwos()
{
for (int i = 0; i < 150; i += 2)
{
Console.Write($"{i} "); // 1
}
}
static void PrintThrees()
{
for (int i = 0; i < 150; i += 3)
{
Console.Write($"{i} ");
}
}
1. El $"{i} " es solo para agregar un espacio después del i y facilitar la lectura del
resultado.
Nota
Si están usando una versión de .NET anterior probablemente el código incluye una función
main. A partir de .NET 6 no viene por defecto la función main.

Si lo corremos nos va a dar el siguiente resultado:
0 2 4 6 8 10 12 14 16 18 20 22 24 26 28 30 32 34 36 38 40 42 44 46 48 50 52 54 56
58 60 62 64 66 68 70 72 74 76 78 80 82 84 86 88 90 92 94 96 98 100 102 104 106 108
110 112 114 116 118 120 122 124 126 128 130 132 134 136 138 140 142 144 146 148 0
3 6 9 12 15 18 21 24 27 30 33 36 39 42 45 48 51 54 57 60 63 66 69 72 75 78 81 84
87 90 93 96 99 102 105 108 111 114 117 120 123 126 129 132 135 138 141 144 147
Como pueden ver este código primero imprime todos los múltiples de 2 y después todos los
múltiples de 3 hasta 150.
No importa cuantas veces corramos el código los números siempre van a estar en el mismo
orden. PrintThrees() siempre va a correr después de PrintTwos().
¿Que pasa si yo quiero que PrintThrees no espere a que PrintTwos termine para ejecutarse?
Para eso podemos usar Threads
Clase Threads
Para aprovechar el paralelismo vamos a utilizar la clase Thread
Thread threadTwos = new Thread(PrintTwos); // 1
Thread threadThrees = new Thread(PrintThrees);
Console.ReadLine();
static void PrintTwos()
{
for (int i = 0; i < 150; i += 2)
{
Console.Write($"{i} ");
}
}
static void PrintThrees()
{
for (int i = 0; i < 150; i += 3)
{
Console.Write($"{i} ");
}
}
1. Este constructor de Thread recibe como parámetro el nombre de una función
Si ejecutamos este código ahora vamos a ver que no ocurre nada y solo espera al
Console.ReadLine()
¿Por que no se ejecutan los Threads que declaramos?

Si recordamos la imagen de las diapositivas, vemos como los Thread tienen varios estados. Al
declarar los threads se van a quedar en el estado Unstarted hasta que hagamos
thread.Start().
Importante
Para pasar un Thread de estado Unstarted a Running hay que hacer nombreThread.Start(). En
caso contrario el Thread nunca se va a ejecutar y no se van a consumir hilos a nivel de sistema
operativo.
2 - Ejecución de hilos
Ahora vamos a inicializar los Thread para que pasen de estado Unstarted a Running
| Thread threadTwos   | = new Thread(PrintTwos); |                      |
| ------------------- | ------------------------ | -------------------- |
| Thread threadThrees | = new                    | Thread(PrintThrees); |
threadThrees.Start();
threadTwos.Start();
Console.ReadLine();
| static void | PrintTwos() |     |
| ----------- | ----------- | --- |
{
| for (int | i = 0; i < 150; | i += 2) |
| -------- | --------------- | ------- |
{

Console.Write($"{i} ");
}
}
static void PrintThrees()
{
for (int i = 0; i < 150; i += 3)
{
Console.Write($"{i} ");
}
}
¿Cuál función se va a ejecutar primero PrintTwos() o PrintThrees()?
No sabemos, lo decide el framework de .NET y el scheduler del sistema operativo cada vez que
se ejecuta el programa, no importa a que Thread se le hizo el .Start() primero. El framework
decide cuando empezar a ejecutarse cada Thread y si se están ejecutando los dos al mismo
momento puede pasar que antes de terminarse uno se ejecute el otro y los resultados en la
consola estén mezclados.
Importante
Hacer nombreThread.Start() no hace que se ejecute el thread en ese momento, le avisa al
framework que el thread esta pronto para ejecutarse y el framework va a decidir cuando
ejecutarlo.
Ahora vamos a ejecutar esta versión varias veces. Recomendamos que ejecuten el código en
sus maquinas varias veces y analicen los resultados.
Ejecución 1:
0 2 4 6 8 10 12 14 16 18 20 22 24 26 28 30 32 0 3 6 9 12 15 18 21 24 27 30 33 36
39 42 45 48 51 54 57 60 63 66 69 72 75 78 81 84 87 90 93 96 99 102 105 108 34 36
111 114 117 120 123 126 129 132 135 38 40 42 138 44 141 46 48 50 52 54 56 144 147
58 60 62 64 66 68 70 72 74 76 78 80 82 84 86 88 90 92 94 96 98 100 102 104 106 108
110 112 114 116 118 120 122 124 126 128 130 132 134 136 138 140 142 144 146 148
A diferencia que antes, ahora usando threads, se intercalaron las salidas de ambos hilos.
Ejemplo: El primero número es un 0 y el último un 148, los cuales fueron impresos por
PrintTwos mientras que en el medio hay salidas de PrintThrees.
Ejecución 2:
0 2 4 6 8 10 12 14 0 3 6 9 16 18 20 22 12 15 18 21 24 27 30 33 36 24 26 28 30 32
34 36 38 40 42 44 46 48 50 52 39 42 45 48 51 54 57 60 63 66 69 72 75 54 56 58 60

62 64 66 68 70 72 74 76 78 80 82 84 86 88 90 92 94 96 98 100 102 104 106 108 110
112 114 116 118 120 122 124 126 128 130 132 134 136 138 140 142 144 78 81 84 87 90
93 96 99 102 105 108 146 148 111 114 117 120 123 126 129 132 135 138 141 144 147
Se sigue viendo que los threads se ejecutaron en paralelo, pero esta vez el threadThree
terminó después que el threadTwos.
Ejecución 3:
0 2 4 6 8 10 12 14 16 18 20 22 24 26 28 30 32 34 36 38 40 42 44 46 48 50 52 54 56
58 60 62 64 66 68 70 72 74 76 78 80 82 84 86 88 90 92 94 96 98 100 102 104 106 108
110 112 114 116 118 120 122 124 126 128 130 132 134 136 138 140 142 144 146 0 3 6
9 12 15 18 21 24 27 30 33 36 39 42 45 48 51 54 57 60 63 66 69 72 75 78 81 84 87 90
93 96 99 102 105 108 148 111 114 117 120 123 126 129 132 135 138 141 144 147
En este caso se ejecutó el threadTwos y una vez que terminó se ejecutó el threadThrees,
esta salida es igual de valida que las anteriores, por esta usando thread, si bien se pueden
ejecutar los threads en paralelo no quiere decir que siempre ocurra. También puede pasar que
se termine de ejecutar threadThrees antes que arranque threadTwos.
La decisión de cuando se ejecuta cada thread es del scheduler del sistema operativo y del
framework de .NET, no del programador.
Nota
Si bien ambos thread se están ejecutando en paralelo. Ambos están usando la misma consola y
solo uno puede imprimir a la vez. Aunque ambos puedan calcular sus respectivos números al
mismo tiempo, solo uno va a tener acceso a la consola para poder imprimirlo, el otro va a tener
que esperar a que la consola se libere antes de seguir.
Otra forma de inicializar los threads
Ahora vamos a ver otra manera de declarar e inicializar threads creando una función
PrintFours
Thread threadTwos = new Thread(PrintTwos);
Thread threadThrees = new Thread(PrintThrees);
threadThrees.Start();
threadTwos.Start();
new Thread(PrintFours).Start();
Console.ReadLine();
static void PrintTwos()
{
for (int i = 0; i < 150; i += 2)

{
Console.Write($"{i} ");
}
}
static void PrintThrees()
{
for (int i = 0; i < 150; i += 3)
{
Console.Write($"{i} ");
}
}
static void PrintFours()
{
for (int i = 0; i < 150; i += 4)
{
Console.Write($"{i} ");
}
}
Como ven usamos dos formas distintas para crear un Thread:
1. Escribir Thread nombreThread = new Thread(nombreFuncion) y después hacer
nombreThread.Start()
2. Escribir new Thread(nombreFunción).Start()
Si bien la segunda es más corta, la primera nos da la posibilidad de pausar, resumir, que otro
thread espere por él, o abortar.
Cuidado
Si bien los threads tienen el método .Abort(), no es buena practica abortar un thread.
Nota
El uso de new Thread requiere que el framework y el sistema operativo realicen varias
operaciones que pueden ser costosas a nivel de rendimiento.
Existen otras posibilidades como usar ThreadPool o usar Task(lo vamos a ver más adelante
en el curso) que tienen mejor rendimiento.
3 - Creación de hilos con parámetros
¿Por qué en vez de tener 3 funciones que hacen casi lo mismo, no tenemos una única función
y como parámetro le pasamos el número que les corresponde?

Vamos a hacer eso:
| Thread threadTwos      | =   | new Thread(Print);   |
| ---------------------- | --- | -------------------- |
| Thread threadThrees    |     | = new Thread(Print); |
| threadThrees.Start(3); |     | // (1)               |
threadTwos.Start(2);
new Thread(Print).Start(4);
Console.ReadLine();
| static void | Print(int | number) |
| ----------- | --------- | ------- |
{
| for (int | i = 0; | i < 150; i += number) |
| -------- | ------ | --------------------- |
{
Console.Write($"{i} ");
}
}
1. No se le puede poner los parámetros en el new Thread hay que hacerlo en el .Start().
Si intentan ejecutar este código verán que no compila.
Si revisamos la documentación vemos que .Start() o no recibe ningún parámetro (como
hicimos antes) o recibe un objeto.

Vamos a cambiar el código para que Print reciba un objeto:
|  1 Thread | threadTwos   | = new | Thread(Print);     |     |
| --------- | ------------ | ----- | ------------------ | --- |
|  2 Thread | threadThrees | =     | new Thread(Print); |     |
 3
 4 threadThrees.Start(3);
 5 threadTwos.Start(2);
|  6 new | Thread(Print).Start(4); |     |     |     |
| ------ | ----------------------- | --- | --- | --- |
 7
 8 Console.ReadLine();
 9
| 10 static | void Print(object |     | number) //  | 1   |
| --------- | ----------------- | --- | ----------- | --- |
11 {
12
| 13  | int step = | (int)number;//  | 2   |     |
| --- | ---------- | --------------- | --- | --- |
14
| 15  | for (int i              | = 0; i | < 150; i += | step) |
| --- | ----------------------- | ------ | ----------- | ----- |
| 16  | {                       |        |             |       |
| 17  | Console.Write($"{i} "); |        |             |       |
| 18  | }                       |        |             |       |
19 }
1. Cambiamos de int a object

2. Hay que convertir el objeto que recibimos a tipo int
Ahora el código si compila y va a funcionar siempre y cuando le pasemos un int . Pero, ¿qué
pasa si en vez de pasarle 2, le pasamos "2"? Como .Start() recibe object, no tenemos
veri cación al momento de compilación del tipo del parámetro.
Por lo tanto, pasar parámetros a los thread utilizando el object de .Start() tiene varias
desventajas:
No hay chequeo al momento de compilación que los parámetros que le estemos pasando
a la función del Thread sean del tipo adecuado. Todos los tipos heredan del tipo object y
por lo tanto se le puede pasar cualquier valor/tipo a .Start, lo que puede generar errores
de ejecución.
Es más complicado pasarle multiples parámetros a la función del thread, tendríamos que
crear un objeto con varias propiedades.
La  rma de la función no me dice de que tipo son los parámetros, me tengo que  jar
adentro de la función (a ver como los convierte) o  jarme si esta documentado.
Arrow Functions
Debido a las complicaciones de la solución anterior, vamos a utilizar Arrow Functions:
| const int             | top          | = 150; | // (1)        |         |                      |        |
| --------------------- | ------------ | ------ | ------------- | ------- | -------------------- | ------ |
| Thread                | threadTwos   |        | = new Thread( |         | () => Print(2,top)); | // (2) |
| Thread                | threadThrees |        | = new         | Thread( | () => Print(3,top)); |        |
| threadThrees.Start(); |              |        | // (3)        |         |                      |        |
threadTwos.Start();
| new Thread(() |     | =>  | Print(4,top)).Start(); |     |     |     |
| ------------- | --- | --- | ---------------------- | --- | --- | --- |
Console.ReadLine();
| static | void | Print(int | step, | int | top) |     |
| ------ | ---- | --------- | ----- | --- | ---- | --- |
{
| for | (int | i = | 0; i < top; | i   | += step) |     |
| --- | ---- | --- | ----------- | --- | -------- | --- |
{
Console.Write($"{i} ");
}
}
1. Vamos a pasarla el número máximo que va a imprimir Print. Así vemos como funciona
pasarle multiples parámetros a la función de un thread.
2. Al igual que antes, las funciones de los threads devuelven void.
3. Los parámetros ya se los pasamos cuando inicializamos el thread, no hay que pasárselo
cuando hacemos el Start.

Si ejecutamos este programa vamos a ver que funciona igual que antes, solo que ahora le
pasamos los valores por parámetro.
Otra forma de usar Arrow Functions
En vez de tener una función a parte, también se puede de nir la función adentro de la arrow
function:
| const int     | top = 150;  |     |           |     |
| ------------- | ----------- | --- | --------- | --- |
| new Thread(() | => {        |     |           |     |
| for           | (int i = 0; | i < | top; i += | 4)  |
{
Console.Write($"{i} ");
}
}).Start();
Console.ReadLine();
Esta versión no es tan prolija como la anterior (donde de nimos la función aparte del thread).
Además si queremos usar la función en más de un thread, tenemos que repetir el código.
4 - Hilos de foreground vs hilos de background
Si emprolijamos un poco el código anterior nos queda de la siguiente manera:
|  1 const | int top | = 150; |     |     |
| -------- | ------- | ------ | --- | --- |
 2
|  3 Thread | threadTwos   | =   | new Thread(   | () => Print(2,top)); |
| --------- | ------------ | --- | ------------- | -------------------- |
|  4 Thread | threadThrees |     | = new Thread( | () => Print(3,top)); |
|  5 Thread | threadFours  |     | = new Thread( | () => Print(4,top)); |
 6
 7 threadThrees.Start();
 8 threadTwos.Start();
 9 threadFours.Start();
10
11 Console.ReadLine();
12
| 13 static | void Print(int |     | step, | int top) |
| --------- | -------------- | --- | ----- | -------- |
14 {
| 15  | for (int                | i = 0; | i < top; | i += step) |
| --- | ----------------------- | ------ | -------- | ---------- |
| 16  | {                       |        |          |            |
| 17  | Console.Write($"{i} "); |        |          |            |
| 18  | }                       |        |          |            |
19 }

¿Cuántos threads tenemos en nuestro código?
4 threads: 3 para cada uno de los threads que creamos (threadTwos, threadThrees y
threadFours) + 1 para el main thread (el hilo principal del programa). Siempre hay un main
thread.
La siguiente imagen muestra un diagrama de los hilos:
Si bien parece que el hilo dos termina después del 3 y el 4, esto es solo una de las muchas
posibilidades. Podría pasar que el dos sea el primer hilo en terminar. Tambien podría pasar que
el hilo principal llegue a la línea 12 antes que los otros hilos terminen.
En ese caso, ¿siguen el resto de los hilos ejecutándose?
Para responder esta pregunta, hay que saber que hay dos tipos de hilos:
Tipos de Hilos
Hilos de foreground: Cuando el hilo principal llega al  n, si hay un hilo de foreground hijo en
estado Running, se esperará a que terminen todos los hilos de foreground antes de cerrar el
proceso.
Los hilos que se crean con new Thread(funcion) son por defecto hilos de foreground
Hilos de background: Cuando el hilo principal llega al  n, si solo quedan hilos hijos de
background, se abortan dichos hilos y el proceso  naliza.
Los hilos que corren usando ThreadPool son hilos de background
Por lo tanto, si corremos el código anterior verán que se ejecuta normalmente, ya que, aunque
el hilo principal pueda llegar al  nal antes que los otros terminen, siempre va a esperar a que
los hilos de foreground (los que creamos usando new) terminen antes de  nalizar.

Que pasa si agregamos las siguientes líneas:
|  1 const | int top | = 150; |     |     |
| -------- | ------- | ------ | --- | --- |
 2
|  3 Thread | threadTwos   | =   | new Thread(   | () => Print(2,top)); |
| --------- | ------------ | --- | ------------- | -------------------- |
|  4 Thread | threadThrees |     | = new Thread( | () => Print(3,top)); |
|  5 Thread | threadFours  |     | = new Thread( | () => Print(4,top)); |
 6
|  7 threadTwos.IsBackground   |     |     | = true; |       |
| ---------------------------- | --- | --- | ------- | ----- |
|  8 threadThrees.IsBackground |     |     | =       | true; |
|  9 threadFours.IsBackground  |     |     | = true; |       |
10
11 threadThrees.Start();
12 threadTwos.Start();
13 threadFours.Start();
14
15 //Fin del hilo principal
16
| 17 static | void Print(int |     | step, | int top) |
| --------- | -------------- | --- | ----- | -------- |
18 {
| 19  | for (int                | i = 0; | i < top; | i += step) |
| --- | ----------------------- | ------ | -------- | ---------- |
| 20  | {                       |        |          |            |
| 21  | Console.Write($"{i} "); |        |          |            |
| 22  | }                       |        |          |            |
23 }
Si corremos el código ahora vamos a ver que no ocurre nada
Eso sucede porque le cambiamos la propiedad IsBackground a los hilos hijos y ahora son
hilos de background. Al  nalizar el hilo principal (llegar a la líneas 15), si no hay ningún otro
thread de foreground, se abortan todos los thread de background y se  naliza el proceso (fue
lo que nos sucedió).
Si hay hilos de foreground en ejecución el hilo principal no termina, en caso contrario si termina
y aborta esos hilos hijos.
Nota
Si corren el código, puede llegar a pasar que a ustedes si les imprima algún resultado. Esto se
debe a que alguno de los hilos hijos empezó la ejecución antes de la  nalización del hilo
principal. Mientras algún hilo de foreground siga corriendo, los hilos de background van a poder
seguir también.
Si ahora cambiamos nuestro código y dejamos algún hilo de foreground y alguno de
background, vemos como el resultado puede ir variando.
|  1 const | int top | = 150; |     |     |
| -------- | ------- | ------ | --- | --- |
 2
|  3 Thread | threadTwos   | =   | new Thread(   | () => Print(2,top)); |
| --------- | ------------ | --- | ------------- | -------------------- |
|  4 Thread | threadThrees |     | = new Thread( | () => Print(3,top)); |

|  5 Thread | threadFours | = new Thread( | () => Print(4,top)); |
| --------- | ----------- | ------------- | -------------------- |
 6
|  7 //threadTwos.IsBackground = true; //  |     |         | 1     |
| ---------------------------------------- | --- | ------- | ----- |
|  8 threadThrees.IsBackground             |     | =       | true; |
|  9 threadFours.IsBackground              |     | = true; |       |
10
11 threadThrees.Start();
12 threadTwos.Start();
13 threadFours.Start();
14
15 //Fin del hilo principal
16
| 17 static | void Print(int | step, | int top) |
| --------- | -------------- | ----- | -------- |
18 {
| 19  | for (int i =                                     | 0; i < top; | i += step) |
| --- | ------------------------------------------------ | ----------- | ---------- |
| 20  | {                                                |             |            |
| 21  | Console.Write($"{i} ");                          |             |            |
| 22  | }                                                |             |            |
| 23  | Console.WriteLine($"Finalizó el thread {step}"); |             |            |
24 }
1. Por defecto los hilos creados con new Thread... son hilos de foreground. No es
| necesario hacer .IsBackground |     | =   | false; |
| ----------------------------- | --- | --- | ------ |
Recomendamos probar el código y analizar las distintas salidas.
En el código anterior, ¿que hilos siempre van a llegar a su  n?
De los 4 hilos totales, el hilo principal y el threadTwos siempre van a llegar al  nal ya que ambos
son hilos de foreground.
Puede ser que threadThrees y threadFours lleguen al  nal o no, dependiendo si terminaron de
ejecutarse antes que los otros dos hilos de foreground.
Si quieren profundizar más aca esta la documentación o cial de hilos de Foreground y
Background
En esta guía aprendiste sobre:
Como crear y utilizar hilos
Diferencia entre hacer new Thread y hacer thread.Start()
Cuando un hilo esta en estado Unstarted y cuando pasa a estado Running
Como crear hilos con funciones que reciben parámetros
Diferencias entre hilos de foreground y de background. Como in uyen en la ejecución del
programa.