Ingeniería de Sistemas, Licenciatura de Sistemas.
Programación de Redes.
Práctico 1
Ejercicio1)
Desarrolleunprogramaquetenga2hilosdeejecución,unoqueimprima20‘x’,yelotroque
imprima20‘y’, entrelaimpresióndecadacarácterhagaunaesperade100mseg. Logreque
primeroseimprimantodaslas‘x’.Hagaestodealmenos2formasdistintas.
Ejercicio2)
Desarrolle una aplicaciónqueenunthreadescribaporconsolacienceros,luegodeterminarsedebe
indicardesdeelthreadprincipalqueelnuevothreadterminó.
Ejercicio3)
Desarrolle una aplicación que tenga un método para recibir dos números y devuelva la suma,
además de otro método que realice lo mismocontressumandos,deberáutilizardosthreads.Luego
delafinalizacióndeloshilos,esnecesariomostrarlasumatotal.
Ejercicio4*)
● ¿Cualesladiferenciaentreprograma,procesoehilo?
● ¿Cualeslaventajadetenervarioshilos?
Ejercicio5)
Desarrolle una aplicación que al comenzar, dispare 10 hilos y estos hilos se queden esperando por
una condición. Esta condición cambia cuando elusuarioescribe“start”enlaconsolay,actoseguido,
cadahiloimprime“holasoyelhiloX”(siendoXelnúmerodehilocreado)yluegotermina.
Ejercicio6)
Desarrolle una aplicación que ejecute un número al azar de threads o reciba la misma cantidadpor
consola y que el primer thread en estar activo escriba en consola "Bienvenidos a Programación de
Redes2022",luegodequeunthreadloescribaunavezlosdemásnodeberánhacerlo
Ejercicio7*)
Desarrolle una aplicación que ingrese personas (con varios datos) a una lista. La aplicación debe
permitir que se ingresen personas desde varios hilos y que en caso de que una persona tenga el
mismonombresedispareunaexcepciónmostrandounmensajeporpantalla.
Ejercicio8*)
Desarrolleunaaplicaciónquetengadoslistasdeenteros:AyB.
● Calcule, el cociente entre el elemento A[i] y B[i] para 0 <= i < largoDeLasListas, cada
operaciónenunhiloindependiente.
● Cuandofinalicentodosloshilos,muestreelpromediodeloscocientes.
● ¿QuésucedesiB[i]=0?

Ingeniería de Sistemas, Licenciatura de Sistemas.
Programación de Redes.
Práctico 1
Ejercicio9)
Desarrolle una aplicación que simule a una discoteca. El máximo de personas simultáneas que se
permiten dentro de la discoteca es cien, además cuando la discoteca recaude 50 mil pesos no se
permitirá el ingreso de más personas (aunque haya menos de cien adentro) perosilasalida.Simule
que cada persona es un hilo y el costo de las entradas es 300. Cada persona espera un tiempo
aleatoriodentrodeladiscoteca.Finalizaalquedarvacíaladiscoteca.Resuelvautilizandosemáforos.
Ejercicio10)
Idemanteriormediantemonitores(conwaitpulse)
Ejercicio11)
Desarrolle una aplicación que simule el problema del productor consumidor, cadaunoenunthread
independienteutilizandomonitores.
Ejercicio12*)
Idemanteriorutilizandosemáforos
Ejercicio13)
Dado un programa que recibe una frase (string) y un entero, inicialice un hilo que encuentre los
divisores del número. Inicialice otro hilo que recorra letra a letra la frase y la devuelva invertida.
Ejecutevariasveceselcódigo,¿haypreferenciadeunhiloporsobreelotro?Inviertadichasituación.
Siguienteejercicioenlasiguientepágina

| Ingeniería   | de Sistemas, | Licenciatura | de Sistemas. |     |     |
| ------------ | ------------ | ------------ | ------------ | --- | --- |
| Programación | de Redes.    |              |              |     |     |
| Práctico     | 1            |              |              |     |     |
Ejercicio14)
DadoelsiguientePseudoCódigodelproblemadeLectoresyEscritores.ResolverconMonitores.
MonitorLectEsc
Variables
CantLec,CantEsc:int=0
Main
|                  | NoLec,NoEsc:condition |     |     | Cobegin |        |
| ---------------- | --------------------- | --- | --- | ------- | ------ |
| ProcComenzarLeer |                       |     |     |         | Lector |
|                  | IfCantEsc>0           |     |     |         | …      |
Lector
ThenNoEsc.wait
|     | Endif//alllegaraquí,sabemosqueno |     |     |     | Escritor |
| --- | -------------------------------- | --- | --- | --- | -------- |
…
hayescritores
Escritor
CantLec++
Coend
| EndComenzarLeer |     |     |     | EndMain |     |
| --------------- | --- | --- | --- | ------- | --- |
ProcFinLeer
CantLec--
Proc Lector
IfCantLec=0
|            | ThenNoLec.signal |     |     | While true | do                   |
| ---------- | ---------------- | --- | --- | ---------- | -------------------- |
|            | Endif            |     |     |            | LectEsc.ComenzarLeer |
| EndFinLeer |                  |     |     |            | LEER                 |
ProcComenzarEscribir
LectEsc.FinLeer
CantEsc++
OtrasTareasLector
IfCantEsc>1
EndWhile
ThenNoEsc.wait
End Lector
EndIf
//nohayescritores
IfCantLec>0
Proc Escritor
ThenNoLec.wait
|     |     |     |     | While true | do  |
| --- | --- | --- | --- | ---------- | --- |
End//nohaynilectoresniescritores
LectEsc.ComenzarEscribir
EndComenzarEscribir
ESCRIBIR
ProcFinEscribir
LectEsc.FinEscribir
CantEsc--
OtrasTareasEscritor
NoEsc.signal
| EndFinEscribir |     |     |     | EndWhile     |     |
| -------------- | --- | --- | --- | ------------ | --- |
| FinLecEsc      |     |     |     | End Escritor |     |