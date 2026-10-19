Facultad de
Ingeniería
Bernard Wand-Polak
Cuareim 1451
11.100  Montevideo, Uruguay
Tel. 2902 15 05 Fax 2908 13 70
www.ort.edu.uy

EVALUACIÓN
MATERIA
CARRERA
CONDICIONES

Obligatorio 2

GRUPO  Mat. y Noct

FECHA  Marzo 2026

Programación de Redes
Ingeniería en Sistemas y Licenciatura en Sistemas
- Puntaje máximo: 15 puntos
- Puntaje mínimo:  5 puntos
- Fecha de entrega: 26/05/2026 hasta las 21:00 horas en gestion.ort.edu.uy

Uso de material de apoyo y/o consulta
Inteligencia Artificial Generativa
-

Seguir las pautas de los docentes: Se deben seguir las instrucciones específicas de los
docentes sobre cómo utilizar la IA en cada curso.
Citar correctamente las fuentes y usos de IA: Siempre que se utilice una herramienta de IA para
generar contenido, se debe citar adecuadamente la fuente y la forma en que se utilizó.
Se debe indicar:

la /las herramientas utilizadas y

a.
b.  el contexto de uso: por ejemplo, generación de ideas, redacción inicial, análisis de

datos, corrección de estilo, etc.

Todo contenido producido por IAG debe ser revisado y verificado; cualquier error presente será
responsabilidad del estudiante.
Ser responsables con el uso de la IA: Conocer los riesgos y desafíos, como la creación de
“alucinaciones”, los peligros para la privacidad, las cuestiones de propiedad intelectual, los
sesgos inherentes y la producción de contenido falso.
El uso de herramientas de Inteligencia Artificial Generativa (IAG) puede emplearse como apoyo
en el proceso de aprendizaje. Sin embargo, no sustituye el razonamiento crítico ni la
elaboración personal. El estudiante es responsable de que el trabajo presentado refleje su
propia comprensión, análisis y proceso cognitivo sobre el tema.
La defensa es obligatoria y eliminatoria.
La defensa será presencial salvo en casos en que se habilite la defensa oral sincrónica en
forma remota.

-

-

-

-

-

-
-

IMPORTANTE:

 1)  Inscribirse
 2) Formar grupos de hasta 3 personas del mismo dictado

3)  Subir  el  trabajo  a  Gestión  antes  de  la  hora  indicada  (ver  hoja  al  final  del  documento:

“RECORDATORIO”)

Aquellos de ustedes que presenten alguna dificultad con su inscripción o tengan inconvenientes técnicos,
por favor contactarse con la Coordinadora de cursos o Coordinación Adjunta antes de las 20:00h del día
de la entrega, a través del mail: adjuntos_ei@ort.edu.uy.

Obligatorio de: Programación de Redes

Código de materia: 4864

Fecha: Marzo 2026

Grupo: Todos

Hoja 2 de 5

Escuela de Ingeniería

OBJETIVOS

Poner en práctica los conocimientos adquiridos en la segunda parte del curso.

●  Manejo de patrón asincrónico (con async / await)
●  Docker

DESCRIPCIÓN DEL SISTEMA: UPGRADE AL SISTEMA JETORT

1.  Se debe migrar toda la estructura orientada a Threads al modelo asincrónico de .NET. (Esto implica

sustituir todos los threads por Tasks y modelar todo en base a Async/Await).

2.  Se debe Dockerizar la aplicación Servidor y Cliente

○  Deben  cambiar  el  uso  de  los  archivos  de  configuración  por  alguna  herramienta  más

adecuada para contenedores

○  Pueden ignorar la funcionalidad de cerrado de servidor por consola
○  Deben  poder  acceder  a  las  imágenes  de  vuelos  del  servidor  desde  la  máquina  host.

Documentar cómo realizarlo. Lo mismo aplica para los archivos del requerimiento 4.

○  No se debe utilizar multi-stage builds de Docker

3.  Se  debe  implementar  las  mejoras  a  la entrega anterior especificada por los docentes o que hayan

faltado para cumplir los requerimientos de la primera entrega

4.  Debe  existir  del  lado  del  Servidor  una  opción  para  generar  un  reporte  por  cada  usuario  en  el

sistema:

○  Cada reporte debe incluir: Información relativa de cada usuario.

i.
ii.

iii.

Información de vuelos creados (incluir imagen en caso de tener)
Cantidad  de  tickets  comprados,  promedio  de  coste  del  ticket,  ticket  más  barato y
más caro.
Cantidad  de  vuelos  con  imagen,  tamaño  total  de  las  imágenes  y  promedio  del
tamaño de las imágenes.

○  El código para generar el reporte en formato PDF ya está dado, aunque pueden realizar las

modificaciones que deseen:

i.

https://github.com/ArqSis-PDR-2026-1/codigo-para-obligatorio

○  Para agilizar el proceso, deben utilizar las funcionalidades de procesamiento en paralelo de

la clase Task.

○  Como  generar  el  reporte  puede  demorar,  deben  implementar  una  funcionalidad  para
cancelar su creación, aprovechando las funcionalidades de cancelamiento de tareas que trae
la clase Task.

REQUERIMIENTOS FUNCIONALES

●  Aplicación Servidor:

o

Iguales al obligatorio anterior + req. 4

●  Aplicación Cliente.

o

Iguales al obligatorio anterior.

Obligatorio de: Programación de Redes

Código de materia: 4864

Fecha: Marzo 2026

Grupo: Todos

Hoja 3 de 5

Escuela de Ingeniería

ESPECIFICACIÓN DE PROTOCOLO

Pueden seguir utilizando el que habían implementado anteriormente o implementar nuevos cambios.

DOCUMENTACIÓN

Como documentación se espera que se incluyan las modificaciones realizadas al código y la motivación de la
misma (que beneficios traen sobre el código anterior).

ENTREGA

Se  debe  entregar  por  Gestión  un  archivo  PDF  que  contenga  una  nueva  documentación    y  en  la  primera
página el link al repositorio con el código.

Deben usar el mismo repositorio que la entrega anterior el cual debe pertenecer a la organización de GitHub
“ArqSis-PDR-2026-1”.

Al  realizar  la  entrega  se  debe  realizar  un  release  en  el  repositorio  y  la rama master (o main) no debe ser
afectada luego del hito que corresponde a la entrega del obligatorio.

Deben entregar los Dockerfile para el cliente y para el servidor junto a una guía para ejecutar las aplicaciones
utilizando Docker. Opcionalmente puede incluir un  docker-compose.yml.

EVALUACIÓN
El criterio para la corrección se basará en los siguientes aspectos:

●  Deberán  evitar  el  uso  de  la  clase  Thread  para  el  desarrollo  del  obligatorio.  Tampoco  estará
permitido el uso de binaryformatter para el pasaje de información, se espera que se utilicen streams
para hacer esto.

●  Por  esta  entrega  no  se  permitirá  que  se  utilicen  parsers  existentes  (como  puede  ser  Json)  para

serializar objetos.

DEFENSA

La defensa se evaluará junto con la tercera entrega.

Obligatorio de: Programación de Redes

Código de materia: 4864

Fecha: Marzo 2026

Grupo: Todos

Hoja 4 de 5

Escuela de Ingeniería

Información importante

Lectura de Obligatorio:

Plazo máximo de entrega: 26/05/2026

Puntaje mínimo/máximo: 5/15 puntos.

Los obligatorios se forman como máximo por grupos de 3 estudiantes.

Todas las entregas se realizan en gestión (gestión.ort.edu.uy) y hasta las 21.00 hs del día de entrega.

Escuela de Ingeniería

Obligatorio de: Programación de Redes

Código de materia: 4864

Fecha: Marzo 2026

Grupo: Todos

Hoja 5 de 5

●  RECORDATORIO: IMPORTANTE PARA LA ENTREGA

Obligatorios

La  entrega  de  los  obligatorios  será  en  formato  digital  online,  a  excepción  de algunas materias que se entregarán en
Bedelía y en ese caso recibirá información específica en el dictado de la misma.

Los principales aspectos a destacar sobre la entrega online de obligatorios son:

Ingresá al sistema de Gestión.

1.
2.  En el menú, seleccioná el ítem “Evaluaciones” y la instancia de evaluación correspondiente, que figura bajo el

título “Inscripto”.

3.  Para iniciar la entrega hacé clic en el ícono:
4.

Ingresá el número de estudiante de cada uno de los integrantes y hacé clic en “Agregar”. El sistema confirmará
que los integrantes estén inscriptos al obligatorio y, de ser así, mostrará el nombre y la fotografía de cada uno
de ellos. Una vez agregados todos los integrantes, hacé clic en “Crear equipo”.

Cualquier integrante podrá:

• Modificar la integración del equipo.

• Subir el archivo de la entrega.

1.  Seleccioná el archivo que deseás entregar. Verificá el nombre del archivo que aparecerá en la pantalla y hacé
clic  en  “Subir”  para iniciar la entrega. Cada equipo (hasta 2 estudiantes) debe entregar un único archivo en
formato  zip  o rar (los documentos de texto deben ser pdf, y deben ir dentro del zip o rar). El archivo a subir
debe tener un tamaño máximo de 40mb

Cuando el archivo quede subido, se mostrará el nombre generado por el sistema (1), el tamaño y la fecha en
que fue subido.

9.  El sistema enviará un e-mail a todos los integrantes del equipo informando los detalles del archivo entregado y

confirmando que la entrega fue realizada correctamente.

10.  Podés cerrar la pestaña de entrega y continuar utilizando Gestión o salir del sistema.
11.  La hora tope para subir el archivo será las 21:00 del día fijado para la entrega.
12.  La entrega se podrá realizar desde cualquier lugar (ej. hogar del estudiante, laboratorios de la Universidad, etc).
13. Aquellos de ustedes que presenten alguna dificultad con su inscripción o tengan inconvenientes técnicos, por
favor contactarse con el Coordinador de cursos o Coordinación adjunta antes de las 20:00h del día de la
entrega, a través de los mails crosa@ort.edu.uy / posada_l@ort.edu.uy (matutino) / larrosa@ort.edu.uy
(nocturno),  o vía Ms Teams.

