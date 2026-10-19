Facultad de
Ingeniería
Bernard Wand-Polak
Cuareim 1451
11.100  Montevideo,
Uruguay
Tel. 2902 15 05 Fax 2908
13 70
www.ort.edu.uy

EVALUACIÓN
MATERIA
CARRERA
CONDICIONES

Obligatorio 1

GRUPO  Mat. y Noct

FECHA  Marzo 2026

Programación de Redes
Ingeniería en Sistemas y Licenciatura en Sistemas
- Puntaje máximo: 25 puntos
- Puntaje mínimo:  5 puntos
- Fecha de entrega: 23-04-2026 hasta las 21:00 horas en gestion.ort.edu.uy

Defensas: Las defensas serán coordinadas con el docente por el grupo
de Teams.

La defensa es obligatoria y eliminatoria. La no presentación a la misma implica
la pérdida de la totalidad de los puntos del Obligatorio.

Uso de material de apoyo y/o consulta

Inteligencia Artificial Generativa
-

Seguir las pautas de los docentes: Se deben seguir las instrucciones específicas de los
docentes sobre cómo utilizar la IA en cada curso.
Citar correctamente las fuentes y usos de IA: Siempre que se utilice una herramienta de IA para
generar contenido, se debe citar adecuadamente la fuente y la forma en que se utilizó.
Verificar el contenido generado por la IA: No todo el contenido generado por la IA es correcto o
preciso. Es esencial que los estudiantes verifiquen la información antes de usarla.
Ser responsables con el uso de la IA: Conocer los riesgos y desafíos, como la creación de
“alucinaciones”, los peligros para la privacidad, las cuestiones de propiedad intelectual, los
sesgos inherentes y la producción de contenido falso
En caso de existir dudas sobre la autoría, plagio o uso no atribuido de IAG, el docente tendrá la
opción de convocar al equipo de obligatorio a una defensa específica e individual sobre el tema

-

-

-

-

IMPORTANTE:

1.  Inscribirse
2.  Formar grupos de hasta 3 personas
3.  Subir el trabajo a Gestión antes de la hora indicada (ver hoja al final del

documento: “RECORDATORIO”)

4.  Se habilitará un foro para realizar consultas del obligatorio.

Aquellos de ustedes que presenten alguna dificultad con su inscripción o tengan
inconvenientes técnicos, por favor contactarse con el Coordinador de cursos o
Coordinación adjunta antes de las 20:00h del día de la entrega, a través de los mails
crosa@ort.edu.uy / posada_l@ort.edu.uy (matutino) / larrosa@ort.edu.uy (nocturno), o
vía Ms Teams.

Obligatorio de: Programación de Redes

Código de materia: 4864

Fecha: Marzo 2026

Grupo: Todos

Hoja 2 de 8

Escuela de Ingeniería

OBJETIVOS

Poner en práctica los conocimientos adquiridos en la primera parte del curso.

•  Manejo de paralelismo, concurrencia y sincronización de Threads.
•  Manejo de Sockets.
•  Manejo de Streams.
•

Implementación y diseño de un protocolo propietario.

DESCRIPCIÓN DEL SISTEMA: JETORT

Se debe construir un sistema compuesto por dos aplicaciones: un servidor, que administra la información
relacionada con vuelos, usuarios y pasajes; y una aplicación cliente, que permite a los usuarios
interactuar con el sistema, crear vuelos, comprar pasajes y consultar información de los mismos.

REQUERIMIENTOS

Aplicación Cliente

•  CR1. Autenticación.

Los usuarios podrán crear una cuenta (nombre de usuario único y contraseña), iniciar sesión y
cerrar sesión del sistema.

•  CR2. Alta de vuelo.

Los usuarios autenticados podrán dar de alta vuelos ingresando: código de vuelo (único), origen,
destino, fecha y hora de salida, cantidad de asientos disponibles (cupos), precio del pasaje,
tiempo estimado de vuelo y opcionalmente una imagen representativa del vuelo.

•  CR3. Modificación de vuelo.

El usuario que creó un vuelo podrá modificar su información mientras no haya partido: origen,
destino, fecha, precio, imagen, etc.

•  CR4. Baja de vuelo.

El usuario que creó un vuelo podrá eliminarlo siempre que no tenga pasajes vendidos y no haya
partido.

•  CR5. Compra de pasajes.

Los usuarios autenticados podrán comprar pasajes para los vuelos disponibles, siempre y
cuando haya cupos disponibles.

•  CR6. Cancelación de pasaje.

Un usuario que haya comprado un pasaje podrá cancelarlo siempre y cuando el vuelo no haya
iniciado.. Al cancelar un pasaje, el cupo queda disponible para otro usuario.

•  CR7. Búsqueda y filtrado de vuelos.

El sistema deberá permitir visualizar los vuelos disponibles y filtrar por al menos 3 criterios (por
ejemplo, por origen, destino, fecha, rango de precios, disponibilidad de cupos etc.). Criterios a
definir por los estudiantes.

Escuela de Ingeniería

Obligatorio de: Programación de Redes

Código de materia: 4864

Fecha: Marzo 2026

Grupo: Todos

Hoja 3 de 8

•  CR8. Consultar vuelo.

Los usuarios podrán consultar todos los detalles de un vuelo, incluyendo la cantidad de asientos
disponibles, el listado de pasajeros y opcionalmente descargar la imagen.

•  CR9. Historial de actividades.

Los usuarios podrán ver un historial de sus actividades: vuelos creados, pasajes comprados.

Aplicación Servidor

•  SR1. Funcionalidad completa.

El servidor debe contar con todas las funcionalidades necesarias para permitir a los clientes
realizar los requerimientos anteriores.

•  SR2. Conexiones múltiples.

El servidor debe aceptar múltiples conexiones simultáneas. Las acciones de un cliente no deben
afectar el rendimiento o la funcionalidad de los demás.

•  SR3. Eliminación de archivos.

Cuando un vuelo sea eliminado, las imágenes asociadas deberán eliminarse del servidor.

•  SR4. Cierre controlado.

Desde la consola del servidor debe haber una opción que permite cerrarlo de forma
controlada, finalizando las conexiones activas con los clientes.

Requerimientos en común
•  Configuración.

El sistema debe ser portable entre máquinas sin necesidad de recompilar. Los valores
específicos de entorno deben estar configurados externamente (por ejemplo, mediante archivos
de configuración).

•  Desconexiones.

Las desconexiones de clientes no deben causar errores en el servidor ni en otros clientes.

•  Manejo de errores.

Si ocurre un error durante el procesamiento de una solicitud, el servidor debe notificar
adecuadamente al cliente y continuar funcionando normalmente.

Datos de prueba (opcional)
Se puede entregar el sistema con vuelos y usuarios precargados para facilitar su validación.

Compilados
El  repositorio  debe  incluir  las  aplicaciones  compiladas  en  modo  Release  con  todo  lo  necesario  para
ejecutarlas  (archivos  de  configuración,  etc.),  con  ejecutables  para  Windows  (.exe,  .dll,  etc.).  Se
recomienda subir todo el proyecto junto con los archivos .sln para facilitar la corrección.

Obligatorio de: Programación de Redes

Código de materia: 4864

Fecha: Marzo 2026

Grupo: Todos

Hoja 4 de 8

Escuela de Ingeniería

ESPECIFICACIÓN DE PROTOCOLO

1.  El siguiente protocolo es una sugerencia, los estudiantes pueden (y deben) hacer cambios al

mismo.

2.  Protocolo orientado a caracteres.

3.

Implementado sobre TCP/IP.

4.  Los valores deberán ir alineados a la derecha, los bytes de relleno deberán tener el valor 0.

5.  Los campos HEADER, CMD y LARGO tendrán largo fijo. El campo DATOS tendrá largo variable,

según el valor indicado en LARGO.

6.  Formato general de la trama.

Nombre
Del Campo
Valores
Largo

DOCUMENTACIÓN

HEADER

CMD

LARGO

DATOS

RES/REQ
3

0-99
2

Entero
4

Variable
Variable

Como documentación se espera que incluya:

●  Alcance de la aplicación
●  Errores conocidos (si los hay)
●  Supuestos de la letra que hayan realizado
●  Descripción de la arquitectura
●  Diseño detallado de cada uno de sus componentes
●  Diseño del protocolo de comunicación

Es muy importante (tanto como la aplicación) que la documentación explique las principales decisiones
tomadas  para  construirla.  Esas  decisiones  dependen  de  lo  que  cada  trabajo  persiga,  pero  a  modo de
ejemplo,  sería  bueno  documentar  por  qué  se  tomaron  las  diferentes  decisiones  a  nivel  de  protocolo,
cómo  se  manejan  los  errores  que  se  pueden  presentar,  cuáles  son  los  mecanismos  de  concurrencia
utilizados, etc.

Se debe documentar además el funcionamiento de la aplicación. No es necesario un manual de usuario,
pero  si  aquellas  aclaraciones  que  consideren  necesarias  para  poder  probar  y  utilizar  la  aplicación,  así
como para comprender su estructura.

No se espera ningún formato particular para la entrega. El orden, las técnicas y nomenclaturas a utilizar
serán decisión de los alumnos. Se evaluará la correcta selección y uso de los mismos.

Se espera el adecuado uso de diagramas que complementen las explicaciones.

Obligatorio de: Programación de Redes

Código de materia: 4864

Fecha: Marzo 2026

Grupo: Todos

Hoja 5 de 8

Escuela de Ingeniería

CÓDIGO

La  gestión  del  código  debe  realizarse  utilizando  UN  ÚNICO  repositorio  de  GitHub.  Para  crear  el
repositorio deben seguir los siguientes pasos:

●  Entrar a https://classroom.github.com/a/JhwWWMcw

●

 El primer integrante del equipo que entre al link debe crear un grupo con el siguiente formato
de nombre:
 “GRUPO-CARRERA-#Estudiante1-#Estudiante2-#Estudiante3”
Ej. : N6A-Licenciatura-123456-987654-123321

●  El  resto  de  integrantes  del  equipo  deben  unirse  al  grupo  de  GitHub siguiendo el mismo link y

eligiendo el grupo creado por el primer integrante .

●  Automáticamente se va a crear un repositorio Privado (no lo deben cambiar)

●  El link al repositorio debe estar en la primera página de la documentación

●  Los commits deben ser representativos de qué parte del obligatorio desarrollo cada integrante.
En  caso  que  hayan  trabajado  varios  en  los  mismos  commits  (e.g.  peer  programming)  pueden
usar PR e indicar los colaboradores correspondientes.

●  Al realizar la entrega se debe realizar un Release de GitHub en el repositorio y la rama main (o
master)  no  debe  ser  afectada  luego  del  hito que corresponde a la entrega del obligatorio. El
release debe incluir el código fuente, los compilados y la documentación.

Obligatorio de: Programación de Redes

Código de materia: 4864

Fecha: Marzo 2026

Grupo: Todos

Hoja 6 de 8

Escuela de Ingeniería

ENTREGA

Se debe entregar por Gestión un archivo PDF que contenga la documentación  y en la primera página el
link al repositorio que cumpla los 6 puntos de la sección anterior.

Si no se realiza la entrega por Gestión, el puntaje final será 0.

El tamaño máximo del archivo a subir es de 40mb.

Importante:  Solo  se aceptará lo entregado en gestión y el código en el repositorio de la organización
de GitHub, por lo que es de suma importancia revisar los medios y la documentación que se entrega
antes de hacerlo.

EVALUACIÓN

El criterio para la corrección se basará en los siguientes aspectos:

•  Deberán  utilizar  explícitamente  la  clase  Thread  y  no  Task  para  el  desarrollo  del  obligatorio.

Tampoco  estará  permitido  el  uso  de binary formatter para el pasaje de información, se espera
que se utilicen streams para hacer esto.

•

•

En  esta entrega NO se permitirá que se utilicen parsers existentes (como puede ser JSON) para

serializar objetos.

En la defensa se correrá un plan de testing, donde se evaluará cuantos puntos pasa el sistema.

•  Criterios de Diseño.

•  Porcentaje de cumplimiento con los requerimientos presentados.

•

•

Funcionamiento de la aplicación.

El cumplimiento de buenas prácticas de los temas trabajados en clase (Multi-threading, sockets,

etc.)

•  Calidad de diseño e implementación.

•  Completitud y calidad de la documentación.

Obligatorio de: Programación de Redes

Código de materia: 4864

Fecha: Marzo 2026

Grupo: Todos

Hoja 7 de 8

Escuela de Ingeniería

DEFENSA

La defensa del trabajo intenta:

●  Evaluar el conocimiento general de los integrantes del grupo sobre la solución propuesta. Todos

los integrantes deben conocer toda la solución.

●  La  nota  del  obligatorio  es  individual  y puede variar dependiendo del resultado de la defensa y

trabajo de cada integrante.

●  Evaluar  el  aporte  individual  al  trabajo  por  parte de cada uno de los integrantes del equipo. Se
espera  que  como mínimo cada uno de los integrantes haya participado en la codificación de al
menos alguna parte significativa del obligatorio.

●  La defensa es presencial  y requiere ejecutar la aplicación en varias máquinas del laboratorio.
●  Las  máquinas  de  los  laboratorios  utilizan Windows 10 u 11 por lo que es importante probar la

aplicación antes de la defensa y tener los compilados en la entrega.

●  Se coordinará el lugar y fecha de las defensas por los canales de Teams de cada grupo.

Información importante

Plazo máximo de entrega: 23/04/2025 21:00hs

Puntaje mínimo/máximo: 5/25 puntos.

Defensas: a coordinar.

Los obligatorios se forman como máximo por grupos de hasta 3 (tres) estudiantes.

Todas las entregas se realizan en gestión (gestión.ort.edu.uy) y hasta las 21.00 hs del día de entrega.

Obligatorio de: Programación de Redes

Código de materia: 4864

Fecha: Marzo 2026

Grupo: Todos

Hoja 8 de 8

Escuela de Ingeniería

●  RECORDATORIO: IMPORTANTE PARA LA ENTREGA

Obligatorios

La entrega de los obligatorios será en formato digital online, a excepción de algunas materias que se entregarán en
Bedelía y en ese caso recibirá información específica en el dictado de la misma.

Los principales aspectos a destacar sobre la entrega online de obligatorios son:

Ingresá al sistema de Gestión.

1)
2)  En el menú, seleccioná el ítem “Evaluaciones” y la instancia de evaluación correspondiente, que figura bajo

el título “Inscripto”.

3)  Para iniciar la entrega hacé clic en el ícono:
4)

Ingresá  el  número  de  estudiante  de  cada  uno  de  los  integrantes  y  hacé  clic  en  “Agregar”.  El  sistema
confirmará que los integrantes estén inscriptos al obligatorio y, de ser así, mostrará el nombre y la fotografía
de cada uno de ellos. Una vez agregados todos los integrantes, hacé clic en “Crear equipo”.

• Modificar la integración del equipo.
• Subir el archivo de la entrega.

5)  Cualquier integrante podrá:
6)
7)
8)  Seleccioná  el  archivo  que  deseás entregar. Verificá el nombre del archivo que aparecerá en la pantalla y
hacé  clic  en  “Subir”  para  iniciar  la  entrega.  Cada  equipo  (hasta  3  estudiantes)  debe  entregar un  único
archivo  en  formato  pdf,  zip  o  rar (los  documentos  de  texto  deben  ser pdf,  y  deben  ir dentro del zip o
rar). El archivo a subir debe tener un tamaño máximo de 40mb.

9)  Cuando el archivo quede subido, se mostrará el nombre generado por el sistema (1), el tamaño y la fecha

en que fue subido.

10)  El  sistema  enviará  un  e-mail  a  todos  los  integrantes  del  equipo  informando  los  detalles  del  archivo

entregado y confirmando que la entrega fue realizada correctamente.

11)  Podés cerrar la pestaña de entrega y continuar utilizando Gestión o salir del sistema.
12)  La hora tope para subir el archivo será las 21:00 del día fijado para la entrega.
13)  La entrega se podrá realizar desde cualquier lugar (ej. hogar del estudiante, laboratorios de la Universidad,

etc).

14) Aquellos de ustedes que presenten alguna dificultad con su inscripción o tengan inconvenientes técnicos,
por favor contactarse con el Coordinador de cursos o Coordinación adjunta antes de las 20:00h del día de
la entrega, a través de los mails crosa@ort.edu.uy / posada_l@ort.edu.uy (matutino) /
larrosa@ort.edu.uy (nocturno),  o vía Ms Teams.

