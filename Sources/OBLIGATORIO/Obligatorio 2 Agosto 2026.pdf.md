Facultad de Ingeniería
Bernard Wand-Polak
Cuareim 1451
11.100  Montevideo, Uruguay
Tel. 2902 15 05 Fax 2908 13 70
www.ort.edu.uy

EVALUACIÓN

Obligatorio 2

GRUPO

Mat. y Noct  FECHA

 Agosto 2026

MATERIA

CARRERA
CONDICIONES

Programación de Redes

Ingeniería en Sistemas y Licenciatura en Sistemas
- Puntaje máximo: 15 puntos
- Puntaje mínimo: 5 puntos
- Fecha de entrega: 29/10/2026 hasta las 21:00 horas en gestion.ort.edu.uy (max. 40Mb en
   formato zip, rar o pdf)

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
2)  Formar grupos de hasta 3 personas del mismo dictado
3)  Subir el trabajo a Gestión antes de la hora indicada (ver hoja al final del documento:
“RECORDATORIO”)

Aquellos de ustedes que presenten alguna dificultad con su inscripción o tengan inconvenientes técnicos,
por favor contactarse con la Coordinadora de cursos o Coordinación Adjunta antes de las 20:00h del día
de la entrega, a través del mail: adjuntos_ei@ort.edu.uy.

Escuela de Ingeniería

Página 1 de 4

Facultad de Ingeniería
Bernard Wand-Polak
Cuareim 1451
11.100  Montevideo, Uruguay
Tel. 2902 15 05 Fax 2908 13 70
www.ort.edu.uy

OBJETIVOS
Poner en práctica los conocimientos adquiridos en la segunda parte del curso.

●  Manejo de patrón asincrónico (con async / await)
●  Docker

DESCRIPCIÓN DEL SISTEMA: UPGRADE AL SISTEMA EDISON

1.  Se  debe  migrar  toda  la  estructura  orientada  a  Threads  al  modelo  asincrónico  de .NET. (Esto implica

sustituir todos los threads por Tasks y modelar todo en base a Async/Await).

2.  Se debe Dockerizar la aplicación Servidor y Cliente

○  Deben  cambiar el uso de los archivos de configuración por alguna herramienta más adecuada

para contenedores

○  Pueden ignorar la funcionalidad de cerrado de servidor por consola
○  Deben  poder  acceder  a  las  imágenes  de  modelos  del  servidor  desde  la  máquina  host.

Documentar cómo realizarlo. Lo mismo aplica para los archivos del requerimiento 5.

○  No se debe utilizar multi-stage builds de Docker

3.  Se  debe  agregar  una  base  de  datos  MySQL  o  de  su  preferencia  y  migrar  el  guardado  de  datos  (no

imágenes) a la misma. La base de datos debe correr en Docker.

4.  Se  debe  implementar  las  mejoras  a  la  entrega  anterior  especificada  por  los  docentes  o  que  hayan

faltado para cumplir los requerimientos de la primera entrega

5.  La  cancelación  de  reservas  que  aún  están  pendientes  debe  cancelar  el  mecanismo  que  la  haría
rechazarse.   Las reservas aprobadas deben generar un archivo como comprobante. Se debe aprovechar
las funcionalidades de cancelamiento de tareas que trae la clase Task.

REQUERIMIENTOS FUNCIONALES

●  Aplicación Servidor:

○

Iguales al obligatorio anterior + req. 5

●  Aplicación Cliente.

○

Iguales al obligatorio anterior.

ESPECIFICACIÓN DE PROTOCOLO

Pueden seguir utilizando el que habían implementado anteriormente o implementar nuevos cambios.

DOCUMENTACIÓN

Como  documentación  se  espera  que  se  incluyan  las modificaciones realizadas al código y la motivación de la
misma (que beneficios traen sobre el código anterior).

ENTREGA

Escuela de Ingeniería

Página 2 de 4

Facultad de Ingeniería
Bernard Wand-Polak
Cuareim 1451
11.100  Montevideo, Uruguay
Tel. 2902 15 05 Fax 2908 13 70
www.ort.edu.uy

Se debe entregar por Gestión un archivo PDF que contenga una nueva documentación y en la primera página el
link al repositorio con el código.

Deben  usar  el  mismo repositorio que la entrega anterior el cual debe pertenecer a la organización de GitHub
“ArqSis-PDR-2026-2”.

Al  realizar  la  entrega  se  debe  realizar  un  release  en  el  repositorio  y  la  rama  master  (o  main)  no  debe  ser
afectada luego del hito que corresponde a la entrega del obligatorio.

Deben  entregar los Dockerfile para el cliente y para el servidor junto a una guía para ejecutar las aplicaciones
utilizando Docker. Opcionalmente puede incluir un  docker-compose.yml.

EVALUACIÓN
El criterio para la corrección se basará en los siguientes aspectos:

●  Deberán evitar el uso de la clase Thread para el desarrollo del obligatorio. Tampoco estará permitido el
uso  de  binaryformatter  para  el  pasaje  de  información,  se  espera  que  se  utilicen  streams  para  hacer
esto.

●  Por esta entrega no se permitirá que se utilicen parsers existentes (como puede ser Json) para serializar

objetos.

DEFENSA

La defensa se evaluará junto con la tercera entrega.

Información importante

Plazo máximo de entrega: 29/10/2026 21:00hs

Puntaje mínimo/máximo: 5/15 puntos.

Defensas: a coordinar.

Los obligatorios se forman como máximo por grupos de hasta 3 (tres) estudiantes.

Todas las entregas se realizan en gestión (gestión.ort.edu.uy) y hasta las 21.00 hs del día de entrega.

Escuela de Ingeniería

Página 3 de 4

Facultad de Ingeniería
Bernard Wand-Polak
Cuareim 1451
11.100  Montevideo, Uruguay
Tel. 2902 15 05 Fax 2908 13 70
www.ort.edu.uy

RECORDATORIO: IMPORTANTE PARA LA ENTREGA

La entrega de los obligatorios será en formato digital online, a excepción de algunas materias que se entregarán en Bedelía
y en ese caso recibirá información específica en el dictado de la misma.

Los principales aspectos a destacar sobre la entrega online de obligatorios son:

1.

Ingresá al sistema de Gestión.

En el menú, seleccioná el ítem “Evaluaciones” y la instancia de evaluación correspondiente, que figura bajo el

●
título “Inscripto”.

3.

Para iniciar la entrega hacé clic en el ícono:

6.
Ingresá el número de estudiante de cada uno de los integrantes y hacé clic en “Agregar”. El sistema confirmará
que los integrantes estén inscriptos al obligatorio y, de ser así, mostrará el nombre y la fotografía de cada uno de ellos.
Una vez agregados todos los integrantes, hacé clic en “Crear equipo”.

Cualquier integrante podrá:

• Modificar la integración del equipo.

• Subir el archivo de la entrega.

5.
Seleccioná el archivo que deseás entregar. Verificá el nombre del archivo que aparecerá en la pantalla y hacé
clic en “Subir” para iniciar la entrega. Cada equipo (hasta 3 estudiantes) debe entregar un único archivo en formato
zip  o  rar (los  documentos  de  texto  deben  ser pdf,  y  deben  ir  dentro  del  zip  o rar). El archivo a subir debe tener un
tamaño máximo de 40mb

Cuando el archivo quede subido, se mostrará el nombre generado por el sistema (1), el tamaño y la fecha en que
fue subido.

El sistema enviará un e-mail a todos los integrantes del equipo informando los detalles del archivo entregado y

●
confirmando que la entrega fue realizada correctamente.

Podés cerrar la pestaña de entrega y continuar utilizando Gestión o salir del sistema.

La hora tope para subir el archivo será las 21:00 del día fijado para la entrega.

La  entrega  se  podrá  realizar  desde  cualquier  lugar (ej. hogar del estudiante, laboratorios de la Universidad,

8.

9.
etc).

10.  Aquellos de ustedes que presenten alguna dificultad con su inscripción o tengan inconvenientes técnicos, por favor

contactarse con la Coordinadora de Cursos o Coordinación Adjunta antes de las 20:00h del día de la entrega, a través del
mail: adjuntos_ei@ort.edu.uy.

Escuela de Ingeniería

Página 4 de 4

