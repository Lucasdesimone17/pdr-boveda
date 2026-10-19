Facultad de Ingeniería
Bernard Wand-Polak
Cuareim 1451
11.100  Montevideo, Uruguay
Tel. 2902 15 05 Fax 2908 13 70
www.ort.edu.uy

EVALUACIÓN

Obligatorio 1

GRUPO

Mat. y Noct  FECHA

 Agosto 2026

MATERIA

CARRERA
CONDICIONES

Programación de Redes

Ingeniería en Sistemas y Licenciatura en Sistemas
- Puntaje máximo: 25 puntos
- Puntaje mínimo: 5 puntos
- Fecha de entrega: 28/09/2026 hasta las 21:00 horas en gestion.ort.edu.uy (max. 40Mb en
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

Defensa

  Fecha de defensa: serán coordinadas con el docente por el grupo de Teams.

  La defensa es obligatoria y eliminatoria. El docente es quien definirá y comunicará la modalidad, y
  mecánica de defensa. La no presentación a la misma implica la pérdida de la totalidad de los puntos del
Obligatorio.

IMPORTANTE:
 1)  Inscribirse
 2) Formar grupos de hasta 3 personas del mismo dictado
  3)  Subir  el  trabajo  a  Gestión  antes  de  la  hora  indicada  (ver  hoja  al  final  del  documento:
“RECORDATORIO”)

Aquellos de ustedes que presenten alguna dificultad con su inscripción o tengan inconvenientes técnicos,
por favor contactarse con la Coordinadora de cursos o Coordinación Adjunta antes de las 20:00h del día
de la entrega, a través del mail: adjuntos_ei@ort.edu.uy.

Escuela de Ingeniería

Página 1 de 9

Facultad de Ingeniería
Bernard Wand-Polak
Cuareim 1451
11.100  Montevideo, Uruguay
Tel. 2902 15 05 Fax 2908 13 70
www.ort.edu.uy

OBJETIVOS

Poner en práctica los conocimientos adquiridos en la primera parte del curso.

●  Manejo de paralelismo, concurrencia y sincronización de Threads.
●  Manejo de Sockets.
●  Manejo de Streams.
●

Implementación y diseño de un protocolo propietario.

DESCRIPCIÓN DEL SISTEMA: EDISON

La reciente llegada de una afamada empresa internacional de vehículos eléctricos a Uruguay tuvo una fuerte
repercusión. La empresa entró al mercado con precios sumamente competitivos (en algunos modelos, incluso
por debajo de los de Estados Unidos) haciendo que en las primeras 24 horas se concretarán cientos de ventas,
llegando a miles de compromisos de venta durante el primer fin de semana.

Las reservas se dieron a través de una landing page provisoria, en la cual los clientes únicamente podían señar
una unidad para recibirla en los meses posteriores.

El magnate dueño de la compañía, conocido por su ritmo imparable, se enteró de que la filial uruguaya aún no
contaba con un sistema permanente de ventas, reservas y catálogo.

Ante tal crisis usted acaba de ser contratado como Ingeniero de Sistemas y deberá llevar adelante dicho
desarrollo.

Para esto se le solicita crear un sistema que debe contar con dos aplicaciones: servidor y cliente. El catálogo de
modelos es permanente; lo que varía es el stock de unidades disponibles de cada modelo. Las reservas, cuya
confirmación está sujeta a un proceso de aprobación, afectan al stock una vez confirmadas.

REQUERIMIENTOS

Aplicación Cliente

●  CR1. Autenticación.

Los usuarios podrán crear una cuenta (nombre de usuario único y contraseña), iniciar sesión y cerrar
sesión del sistema.

●  CR2. Alta de modelo.

Los usuarios autenticados podrán dar de alta modelos de vehículos para ser reservados ingresando:
Nombre del modelo (único), descripción, precio, fecha de entrega prometida, stock inicial y
opcionalmente una imagen representativa del modelo.

●  CR3. Modificación de modelo.

El usuario que creó un modelo de vehículo podrá modificar su información mientras sigan habiendo
unidades disponibles: descripción, precio, fecha de entrega prometida, stock, imagen, etc. En caso de
actualizar stock este debe ser mayor a la cantidad de reservas existentes

●  CR4. Baja de modelo.

El usuario que creó un vehículo podrá eliminarlo siempre que no existan reservas activas asociadas al
mismo.

Escuela de Ingeniería

Página 2 de 9

Facultad de Ingeniería
Bernard Wand-Polak
Cuareim 1451
11.100  Montevideo, Uruguay
Tel. 2902 15 05 Fax 2908 13 70
www.ort.edu.uy

●  CR5. Solicitud de reserva de vehículos.

Los usuarios autenticados podrán solicitar la reserva de una unidad de cualquier vehículo disponible. La
solicitud enviada quedará en estado pendiente y pasará a confirmada o rechazada luego de un
tiempo (configurable, en segundos). Al procesarse la solicitud, si no existen unidades disponibles, la
reserva deberá ser rechazada .

●  CR6. Cancelación de reserva.

Un usuario que haya reservado un vehículo podrá cancelarlo siempre y cuando la reserva no sea
rechazada . Al cancelar una reserva, la unidad correspondiente debe quedar disponible para otra
reserva.

●  CR7. Búsqueda y filtrado de vehículos.

El sistema deberá permitir visualizar los vehículos disponibles y filtrar por al menos 3 criterios (por
ejemplo, nombre del modelo, cantidad de unidades disponibles, precio máximo, precio mínimo, fecha de
entrega  etc.). Criterios a definir por los estudiantes.

●  CR8. Consultar vehículo.

Los usuarios podrán consultar todos los detalles de un vehículo, incluyendo descripción, precio,
cantidad de unidades disponibles, cantidad de unidades reservadas y opcionalmente descargar la
imagen.

●  CR9. Consultar el estado de reservas.

Los usuarios podrán consultar el estado de sus reservas. Viendo modelo, fecha de creación, estado y
última actualización.

●  CR10. Historial de actividades.

Los usuarios podrán ver un historial de sus actividades: inicio de sesión, vehículos creados, eliminados,
modificados, reservas hechas y compras realizadas.

Aplicación Servidor

●  SR1. Funcionalidad completa.

El servidor debe contar con todas las funcionalidades necesarias para permitir a los clientes realizar los
requerimientos anteriores.

●  SR2. Conexiones múltiples.

El servidor debe aceptar múltiples conexiones simultáneas. Las acciones de un cliente no deben afectar
el rendimiento o la funcionalidad de los demás.

●  SR3. Eliminación de archivos.

Cuando un vehículo sea eliminado, las imágenes asociadas deberán eliminarse del servidor.

●  SR4. Cierre controlado.

Desde la consola del servidor debe haber una opción que permite cerrarlo de forma controlada,
finalizando las conexiones activas con los clientes.

Requerimientos en común
●  Configuración.

Escuela de Ingeniería

Página 3 de 9

Facultad de Ingeniería
Bernard Wand-Polak
Cuareim 1451
11.100  Montevideo, Uruguay
Tel. 2902 15 05 Fax 2908 13 70
www.ort.edu.uy

El sistema debe ser portable entre máquinas sin necesidad de recompilar. Los valores específicos de
entorno deben estar configurados externamente (por ejemplo, mediante archivos de configuración).

●  Desconexiones.

Las desconexiones de clientes no deben causar errores en el servidor ni en otros clientes.

●  Manejo de errores.

Si ocurre un error durante el procesamiento de una solicitud, el servidor debe notificar adecuadamente
al cliente y continuar funcionando normalmente.

Datos de prueba (opcional)
Se puede entregar el sistema con datos precargados para facilitar su validación.

Compilados
El repositorio debe incluir las aplicaciones compiladas en modo Release con todo lo necesario para ejecutarlas
(archivos  de  configuración,  etc.),  con  ejecutables  para  Windows  (.exe, .dll, etc.). Se recomienda subir todo el
proyecto junto con los archivos .sln para facilitar la corrección.

Escuela de Ingeniería

Página 4 de 9

Facultad de Ingeniería
Bernard Wand-Polak
Cuareim 1451
11.100  Montevideo, Uruguay
Tel. 2902 15 05 Fax 2908 13 70
www.ort.edu.uy

ESPECIFICACIÓN DE PROTOCOLO

➢  El siguiente protocolo es una sugerencia, los estudiantes pueden (y deben) hacer cambios al mismo.

➢  Protocolo orientado a caracteres.

➢  Implementado sobre TCP/IP.

➢  Los valores deberán ir alineados a la derecha, los bytes de relleno deberán tener el valor 0.

➢  Los campos HEADER, CMD y LARGO tendrán largo fijo. El campo DATOS tendrá largo variable, según el

valor indicado en LARGO.

➢  Formato general de la trama.

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

1.  Alcance de la aplicación
2.  Errores conocidos (si los hay)
3.  Supuestos de la letra que hayan realizado
4.  Descripción de la arquitectura
5.  Diseño detallado de cada uno de sus componentes
6.  Diseño del protocolo de comunicación

Es  muy  importante  (tanto  como  la  aplicación)  que  la  documentación  explique  las  principales  decisiones
tomadas para construirla. Esas decisiones dependen de lo que cada trabajo persiga, pero a modo de ejemplo,
sería bueno documentar por qué se tomaron las diferentes decisiones a nivel de protocolo, cómo se manejan
los errores que se pueden presentar, cuáles son los mecanismos de concurrencia utilizados, etc.

Se debe documentar además el funcionamiento de la aplicación. No es necesario un manual de usuario, pero si
aquellas  aclaraciones  que  consideren  necesarias  para  poder  probar  y  utilizar  la  aplicación,  así  como  para
comprender su estructura.

No se espera ningún formato particular para la entrega. El orden, las técnicas y nomenclaturas a utilizar serán
decisión de los alumnos. Se evaluará la correcta selección y uso de los mismos.

Se espera el adecuado uso de diagramas que complementen las explicaciones.

Escuela de Ingeniería

Página 5 de 9

Facultad de Ingeniería
Bernard Wand-Polak
Cuareim 1451
11.100  Montevideo, Uruguay
Tel. 2902 15 05 Fax 2908 13 70
www.ort.edu.uy

CÓDIGO

La gestión del código debe realizarse utilizando UN ÚNICO repositorio de GitHub, y deben seguir lo siguiente:

1.  El repositorio debe pertenecer a la organización https://github.com/orgs/ArqSis-PDR-2026-2

○  Sus docentes les avisan cómo acceder a la organización
○  Una vez tengan acceso deben crear el repositorio e invitar a los integrantes.
○

Incluyan el número de estudiante en el nombre del repositorio y opcionalmente el/los dictados
. ej. m6a-ingenieria-123456-9876543

2.  El repositorio debe ser privado
3.  Todos los integrantes del obligatorio deben pertenecer al repositorio
4.  Los commits deben ser representativos de qué parte del obligatorio desarrollo cada integrante. En caso
que hayan trabajado varios en los mismos commits (e.g. peer programming) pueden usar PR e indicar
los colaboradores correspondientes.

5.  El link al repositorio debe estar en la primera página de la documentación
6.  Al  realizar  la  entrega  se  debe  realizar  un  Release  de  GitHub  en  el  repositorio  y  la  rama  main  (o
master) no debe ser afectada luego del hito que corresponde a la entrega del obligatorio. El release
debe incluir el código fuente, los compilados y la documentación.

Escuela de Ingeniería

Página 6 de 9

Facultad de
Ingeniería
Bernard Wand-Polak
Cuareim 1451
11.100  Montevideo, Uruguay
Tel. 2902 15 05 Fax 2908 13
70
www.ort.edu.uy

ENTREGA

Se debe entregar por Gestión un archivo PDF que contenga la documentación  y en la primera página el
link al repositorio que cumpla los puntos de la sección anterior.

Si no se realiza la entrega por Gestión, el puntaje final será 0.

El tamaño máximo del archivo a subir es de 40mb.

Importante:  Solo  se aceptará lo entregado en gestión y el código en el repositorio de la organización
de GitHub, por lo que es de suma importancia revisar los medios y la documentación que se entrega
antes de hacerlo.

EVALUACIÓN

El criterio para la corrección se basará en los siguientes aspectos:

●  Deberán  utilizar  explícitamente  la  clase  Thread  y  no  Task  para  el  desarrollo  del  obligatorio.
Tampoco  estará  permitido  el  uso  de binary formatter para el pasaje de información, se espera
que se utilicen streams para hacer esto.

●  En  esta entrega NO se permitirá que se utilicen parsers existentes (como puede ser JSON) para

serializar objetos.

●  En la defensa se correrá un plan de testing, donde se evaluará cuantos puntos pasa el sistema.

●  Criterios de Diseño.

●  Porcentaje de cumplimiento con los requerimientos presentados.

●  Funcionamiento de la aplicación.

●  El cumplimiento de buenas prácticas de los temas trabajados en clase (Multi-threading, sockets,

etc.)

●  Calidad de diseño e implementación.

●  Completitud y calidad de la documentación.

Escuela de Ingeniería

Página 7 de 9

Facultad de
Ingeniería
Bernard Wand-Polak
Cuareim 1451
11.100  Montevideo, Uruguay
Tel. 2902 15 05 Fax 2908 13
70
www.ort.edu.uy

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

Plazo máximo de entrega: 28/09/2026 21:00hs

Puntaje mínimo/máximo: 5/25 puntos.

Defensas: a coordinar.

Los obligatorios se forman como máximo por grupos de hasta 3 (tres) estudiantes.

Todas las entregas se realizan en gestión (gestión.ort.edu.uy) y hasta las 21.00 hs del día de entrega.

Escuela de Ingeniería

Página 8 de 9

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

2.
título “Inscripto”.

3.

Para iniciar la entrega hacé clic en el ícono:

4.
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

6.
confirmando que la entrega fue realizada correctamente.

Podés cerrar la pestaña de entrega y continuar utilizando Gestión o salir del sistema.

La hora tope para subir el archivo será las 21:00 del día fijado para la entrega.

La  entrega  se  podrá  realizar  desde  cualquier  lugar (ej. hogar del estudiante, laboratorios de la Universidad,

7.

8.

9.
etc).

10.  Aquellos de ustedes que presenten alguna dificultad con su inscripción o tengan inconvenientes técnicos, por favor

contactarse con la Coordinadora de Cursos o Coordinación Adjunta antes de las 20:00h del día de la entrega, a través del
mail: adjuntos_ei@ort.edu.uy.

Escuela de Ingeniería

Página 9 de 9

