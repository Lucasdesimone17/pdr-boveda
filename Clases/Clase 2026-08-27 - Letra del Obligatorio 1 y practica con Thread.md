---
date: 2026-08-27
type: clase
tags:
  - clase
  - pdr
  - tema-1
  - evaluacion
ai-first: true
confidence: stated
fuente: "Grabacion Vimeo, showcase 12367503, video 27-08-2026, 1h59m, subtitulos autogenerados"
docentes:
  - Enrique Latorres
  - Natalia Franco
---

# Clase 2026-08-27 - Letra del Obligatorio 1 y practica con Thread

## For future agent
Acta de la clase del 27-08-2026 (Vimeo, showcase 12367503). **Es la clase mas importante del semestre hasta ahora**: se leyo completa la letra del Obligatorio 1 y se detallaron criterios de correccion, prohibiciones y modalidad de defensa. La letra desglosada vive en [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]]; aca queda el acta con las aclaraciones orales, que en varios puntos agregan cosas que no estan en el PDF. Segunda mitad: practica con `Thread`, estados, prioridades, `Join`, pasaje de parametros y el problema clasico del `for` que pasa el indice por referencia.

## Anuncio: se publico la letra del Obligatorio 1
- **Entrega: 2026-09-28.** Puntaje: **minimo 5, maximo 25**. Se entrega **por gestion** (gestion.ort.edu.uy); si no se entrega por gestion, **el puntaje es cero**.
- El sistema de gestion es "la entrega" desde el punto de vista formal. Recomendacion: no dejarlo para el ultimo momento porque el sistema se satura, y subir una preentrega dias antes.
- **Tamano maximo del archivo: 40 MB.**
- **Grupos de hasta tres personas.**
- **El Obligatorio 1 tiene defensa.** Sobre los otros hubo una afirmacion contradictoria entre esta clase y la del 08-09; ver la contradiccion registrada en [[Huecos y contradicciones del material PDR]].
- El docente planteo que la defensa "seria escrita": pasan unas preguntas y el estudiante responde ahi. Al mismo tiempo la letra dice que la defensa es presencial en laboratorio con plan de test. **Ambas cosas se dijeron; tratar como no resuelto.**
- **Gabriel es el catedratico** de la materia.
- Regla dura sobre IA generativa, dicha con el mismo enfasis que en la primera clase:

> "Nosotros usamos la inteligencia artificial generativa, pero lo que sea que ustedes ponen en el obligatorio tienen que entender, saberlo. Si yo agarro asi y senalo una linea de codigo, ustedes me tienen que decir por que esta eso. Que hace, como lo hace y por que esta ahi." ([01:31])

## Objetivos declarados del Obligatorio 1
Poner en practica lo de la primera parte del curso:
- manejo de paralelismo, concurrencia y **sincronizacion de threads**
- manejo de **sockets**
- manejo de **streams**
- **implementacion y diseno de un protocolo propietario**

## Criterios de correccion y prohibiciones (textual de la letra, leida en clase)
Esto es lo que mas conviene tener a mano:

- **Deben utilizar explicitamente la clase `Thread` y NO `Task`.** El docente remarco "importante".
- **No esta permitido `BinaryFormatter`** para el pasaje de informacion. Se espera que se usen **streams**.
- **En esta entrega no se permite usar parsers existentes** (se menciono "Jackson" como ejemplo del tipo de herramienta) **para serializar objetos**.
- En la defensa **se corre un plan de test** y se evalua cuantos puntos pasa el sistema.
- Se evalua ademas: criterios de diseno, porcentaje de cumplimiento de requerimientos, funcionamiento, cumplimiento de buenas practicas de los temas trabajados en clase (multi-threading, sockets, streams), calidad de diseno e implementacion, y completitud y calidad de la documentacion.

Ver la lista consolidada en [[Reglas y prohibiciones del curso PDR 2026]].

## Entregables y repositorio
- Se entrega **un PDF con el informe** por gestion; **el software va en un repositorio de GitHub**.
- **El link al repositorio va en la primera pagina de la documentacion** (caratula).
- Los **commits deben ser representativos** de que parte hizo cada integrante. Si trabajaron varios en el mismo commit, se pueden usar pull requests e indicar colaboradores.
- Al entregar hay que hacer un **release de GitHub**, y la rama `main`/`master` **no debe ser afectada despues del hito de entrega**. El release incluye codigo fuente, compilados y documentacion.
- Aclaracion oral: dejar el repositorio quieto **unos dias despues del 28/09** porque los docentes bajan el repo en esa ventana. Alternativa sugerida: usar `main` solo para releases, como si fuera produccion.
- El repositorio **debe incluir las aplicaciones compiladas en modo release**, con todo lo necesario para ejecutarlas (configuracion incluida) y **ejecutables para Windows**. Hay que **modificar el `.gitignore`** para que la compilacion en release se suba.
- Se recomienda subir todo el proyecto junto con los archivos `.sln` para facilitar la correccion.
- Aclaracion oral: los docentes **bajan el repositorio y lo prueban**. Recomendacion concreta: hacer una preentrega, bajarla en una maquina limpia y verificar que todo funcione.

## Especificacion del protocolo (lo que dio la letra)
- Protocolo **orientado a caracteres implementado sobre TCP/IP**.
- Los valores van **alineados a la derecha**, rellenando por delante.
- Los campos **Header, comando y Largo tienen largo fijo**. El campo **Datos tiene largo variable** segun el valor indicado en Largo.
- **Los estudiantes pueden y deben hacer cambios** al protocolo propuesto. Es una base, no una imposicion.

## Documentacion esperada
- Alcance de la aplicacion.
- **Errores conocidos**, si los hay.
- **Supuestos** que se hayan tomado sobre la letra.
- Descripcion de la arquitectura y diseno detallado de cada componente.
- **Diseno del protocolo de comunicacion.**
- **Las principales decisiones tomadas y por que**: decisiones a nivel de protocolo, como se manejan los errores, cuales son los mecanismos de concurrencia. El docente dijo que esto es "tan importante como la aplicacion".
- Funcionamiento de la aplicacion. No hace falta manual de usuario, pero si aclaraciones necesarias para probarla: por ejemplo **formatos de fecha y formatos numericos aceptados**. Prueba sugerida: mostrarselo a alguien que no sepa y ver si entiende.
- No se exige un formato particular. Se evalua la correcta seleccion y uso de tecnicas y nomenclaturas, y **el uso adecuado de diagramas** - pero explicitamente **no al nivel de Diseno 2**.
- "Detallado no es superdetallado, pero si lo suficiente para que en un vistazo podamos entender como lo hicieron" ([14:53]).

## Defensa
- Objetivo: evaluar el conocimiento general **de todos los integrantes** sobre la solucion propuesta.
- **La nota del obligatorio es individual** y puede variar segun el resultado de la defensa y el trabajo de cada integrante.
- Se espera que **cada integrante haya codificado al menos una parte significativa**.
- Es **presencial** y **requiere ejecutar la aplicacion en varias maquinas de laboratorio**.
- Las maquinas de laboratorio usan **Windows 10 / 11**: hay que probar antes y tener los compilados en la entrega.
- El lugar y la fecha se coordinan por el grupo (Teams).

## Otras aclaraciones orales
- Persistencia de datos es **opcional** pero recomendada, porque facilita las pruebas: al levantar, el servidor lee un archivo; al salir, lo vuelve a escribir.
- Se puede entregar el sistema **con datos precargados** para facilitar la validacion.
- Sobre los dos roles (vendedor y comprador): **queda a criterio del grupo**. La opcion mas simple es que el mismo usuario pueda hacer las dos cosas; otra opcion valida es que al entrar al menu elija actuar como vendedor o como cliente. **Hay que aclararlo en la documentacion.**
- El "historial de actividades" es como un log; el docente adelanto que **probablemente en un obligatorio posterior eso se mande a otro servidor** para hacer consultas.
- El documento tambien se puede poner en GitHub, dentro del proyecto, pero **la entrega formal es la de gestion**.

## Practica: `Thread` en detalle

### Repaso del primer ejemplo
Mismo codigo de la clase anterior. Reiteracion del punto clave: `t.Start()` solo avisa al despachador; **hasta el `Start` hay un unico hilo**, recien despues pueden haber dos, y por eso los dos ultimos mensajes pueden intercalarse pero **los dos primeros nunca** (los emite el mismo hilo, en orden).

Analogia que uso para explicar la asociacion metodo-hilo: es como POSIX en C, donde se declaraba la funcion y hasta que no se le daba `create` no se generaba el hilo.

### `ThreadState`
- Se agrego un ejemplo que consulta `t.ThreadState` desde el `main` y muestra `Running` / terminado, con `Sleep` de 300 y 500 ms para ver la secuencia.
- Los diagramas de estado cambian segun Windows / Linux / .NET, pero los conceptos son los mismos.

### Abortar hilos: no
- "En general, vamos a ver que matar los hilos es muy desprolijo" ([36:37]).
- **El patron correcto** que se va a usar: el hilo revisa de tanto en tanto una bandera; el hilo que lee la consola pone esa bandera al recibir el comando de salida; cada hilo hace un **final ordenado**.
- Adelanto: en programacion asincronica existe un objeto para esto, el **`CancellationToken`**, que se reparte a todos los hilos.
- Motivo: si se mata un hilo en medio de una transaccion, quedan **datos inconsistentes en memoria**.

### Foreground / background, con la regla del curso
- Repaso: foreground espera al terminar el `main` (join implicito); background aborta.
- **Regla del curso**: "En general, con lo que nosotros vamos a trabajar, todo son hilos de foreground. Y a lo que vamos a apuntar es que ustedes hagan disenos de programas donde el servidor no pueda terminar hasta que todos sus hijos hayan terminado" ([43:48]).
- Con persistencia, hay que hacer **`Join` explicito** para poder guardar los datos despues de que todos los hijos terminaron. Y muchas veces hay que **llevar la cuenta de cuantos clientes hay**.
- Cuando si se puede usar background: un servicio que **no modifica datos**, solo entrega respuestas simples; por ejemplo un hilo que escribe logs.
- Confirmacion pedida en clase: **por defecto un `Thread` es foreground**, salvo que se le ponga background explicitamente.

### Prioridades
- Windows: prioridad 0 para el administrador de memoria del sistema (memoria virtual); **1 a 15 para aplicaciones estandar**; **16 a 31 para tiempo real** (controladores especializados). Windows usa **colas multinivel con realimentacion**: una tarea arranca en el nivel medio (8) y el sistema **sube las interactivas y baja las intensivas en CPU**, para que el usuario perciba respuesta rapida.
  - En la interfaz de .NET esos 15 niveles se exponen como cinco nombres: `Highest` = 15, `AboveNormal` = ~11, `Normal` = 8, `BelowNormal` = 4, `Lowest` = 1.
  - Anecdota: en Windows viejos, hacer muchos clics en una ventana hacia que el sistema la creyera interactiva y **le subiera la prioridad**. No confirmado en Windows 11.
- Linux: rango **-20 a +19**, la propiedad **`nice`**. El despachador de Linux es **round robin con time slice variable**, no colas multinivel como Windows. Ademas tiene un conjunto grande de niveles de tiempo real (robotica, aplicaciones criticas).
- **En general modificar prioridades no se recomienda.** Windows no esta pensado para que los usuarios las cambien.
- Demo: se dio `Highest` a un hilo y `Lowest` a otro y **no se noto diferencia**, porque la maquina tiene muchos nucleos libres. Se nota cuando el sistema esta cargado.

### `Sleep`, `Suspend`, `Resume`, `Abort`
- `Thread.Sleep` **bloquea** el hilo por ese tiempo (espera bloqueada, no activa) y despues el despachador lo vuelve a despachar. Garantiza al menos ese tiempo sin procesador.
- **`Suspend` no es lo mismo que bloquear**: es una tarea administrativa, "a este hilo, por ahora, no le des mas procesador". Se usa para liberar CPU y memoria para algo mas importante (el sistema de memoria virtual puede mandar ese proceso a disco). Con `Resume` vuelve.
- Herramienta recomendada por el docente: **Sysinternals** de Microsoft, en particular **Process Explorer** - "un task manager pero con esteroides". No requiere instalacion. Permite suspender procesos y ver, por ejemplo, cuanto tiempo hay nucleos sin nada que hacer.
- **`Abort` es riesgoso** por la inconsistencia de datos. Evitarlo.

### `Join` y devolver un resultado
Ejemplo de una clase `Suma` que recibe los operandos en el constructor, un metodo `Add` que simula cinco segundos de trabajo y guarda el resultado internamente, y un `main` que dispara el hilo, hace otras cosas, y **hace `Join` antes de leer el resultado**.

```csharp
// idea del ejemplo de la clase
Suma sum = new Suma(6, 42);
Thread t = new Thread(sum.Add);   // se puede pasar el metodo directo, sin instanciar el delegado
t.Start();
// ... el main hace otras cosas ...
t.Join();                          // espera a que el hilo termine
Console.WriteLine(sum.Resultado);  // recien ahora el resultado es valido
```

- El `Join` es lo que **garantiza que el `main` pueda leer el resultado** del otro hilo. "Si termino, inmediatamente sigue. Pero si no termino, queda esperando" ([99:05]).
- Nota de estilo: se puede pasar el metodo directamente al constructor de `Thread` en vez de instanciar el `ThreadStart`; **acorta el codigo**.

### Pasar parametros a un hilo
- Con `ParameterizedThreadStart` se puede pasar **un** parametro, **pero se pasa como `object`**: no hay verificacion de tipo. Si se mete un tipo equivocado, hay **excepcion en tiempo de ejecucion**. Hay que asegurarse de que el objeto sea del tipo que espera el metodo.
- Alternativa que uso y prefirio: **expresion lambda**.

```csharp
Thread t = new Thread(() => Suma(a, b));
t.Start();
```

### El problema del `for` que comparte el indice (caso que va a salir en el parcial)
Enunciado: imprimir los numeros del 1 al 100, cada `Console.WriteLine` en un hilo distinto, sin importar el orden.

```csharp
// INCORRECTO: pasa una referencia al mismo i
for (int i = 0; i < 100; i++)
{
    Thread t = new Thread(Imprimir);
    t.Start(i);          // i sigue cambiando mientras el hilo arranca
}
```

Que pasa y por que:
- **En .NET hasta un entero es un objeto**; las estructuras complejas **no se pasan por valor, se pasan por referencia**.
- Mientras el hilo se esta creando y despachando, **el hilo del `for` sigue incrementando `i`**. El `Start` no arranca el hilo, lo encola en el despachador.
- Resultado observado en la demo: **numeros repetidos y numeros faltantes**; nunca se pasa de 100, pero puede aparecer el 100 aunque no debiera generarse (el `for` ya salio y un hilo anterior lee el `i` en 100).

```csharp
// CORRECTO: una variable nueva por iteracion
for (int i = 0; i < 100; i++)
{
    int n = i;                      // n se instancia dentro del bloque constructivo
    Thread t = new Thread(Imprimir);
    t.Start(n);                     // cada hilo recibe su propio n
}
```

Por que funciona, con el argumento que uso el docente: **mientras exista una referencia al objeto, el garbage collector no lo borra**, aunque el bloque constructivo ya haya terminado. Cada iteracion del loop **genera un `n` distinto**, y cada `Imprimir` recibe el suyo.

> "Yo le estaba pensando como un solo bucle. - Nunca mas tenes que pensar asi. Nunca mas en el resto de la carrera, porque si no, no te va a ir muy bien." ([114:27])

Cierre del docente: hay que prestarle atencion a la ejecucion concurrente **hasta en el pasaje de parametros**; a cada hilo hay que pasarle **una copia**, o entender que si se pasa la misma referencia va a haber problemas de concurrencia y hay que bloquear el acceso.

### Detalle util sobre la consola de .NET
- "Una cosa importante en .NET es que la consola tiene una especie de productor consumidor" ([77:16]): varios hilos pueden imprimir y la consola **lineariza** los mensajes. **Una cadena de texto no se corta a la mitad.**

## Connections
- Evaluacion: [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]] · [[Reglas y prohibiciones del curso PDR 2026]]
- Tema: [[Tema 1 - Programacion multihilo]] · [[Errores caros en Programacion de Redes]]
- Anterior: [[Clase 2026-08-20 - Modelos de arquitectura, procesos e hilos]]
- Siguiente: [[Clase 2026-09-01 - Exclusion mutua en CSharp y arranque de sockets]]
