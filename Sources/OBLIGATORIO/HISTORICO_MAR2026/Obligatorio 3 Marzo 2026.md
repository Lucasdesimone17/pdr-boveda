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

Obligatorio 3

GRUPO  Mat. y Noct

FECHA  Marzo 2026

Programación de Redes
Ingeniería en Sistemas y Licenciatura en Sistemas
- Puntaje máximo: 25 puntos
- Puntaje mínimo:  5 puntos
- Fecha de entrega: 22/06/2026 hasta las 21:00 horas en gestion.ort.edu.uy

Defensa
 Fecha de defensa: Las defensas serán coordinadas con el docente
por el grupo de Teams.

  La defensa es obligatoria y eliminatoria. El docente es quien definirá y
comunicará la modalidad, y mecánica de defensa. La no presentación a
la misma implica la pérdida de la totalidad de los puntos del
Obligatorio.

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

 1)  Inscribirse
 2) Formar grupos de hasta 3 personas del mismo dictado
  3) Subir el trabajo a Gestión antes de la hora indicada (ver hoja al final
del documento: “RECORDATORIO”)
  4) Se habilitará un foro para realizar consultas del obligatorio.

Aquellos de ustedes que presenten alguna dificultad con su inscripción o tengan
inconvenientes técnicos, por favor contactarse con el Coordinador de cursos o
Coordinación adjunta antes de las 20:00h del día de la entrega, a través de los mails
crosa@ort.edu.uy / posada_l@ort.edu.uy (matutino) / larrosa@ort.edu.uy (nocturno), o
vía Ms Teams.

Obligatorio de: Programación de Redes

Código de materia: 4864

Fecha: Marzo 2026

Grupo: Todos

Hoja 2 de 7

Escuela de Ingeniería

OBJETIVOS

Poner en práctica los conocimientos adquiridos en la tercera parte del curso:

●  Manejo de MOM, RPC,  APIs REST, WebSocket, Docker

DESCRIPCIÓN DEL SISTEMA: SEGUNDA UPGRADE A JETORT ONLINE

Después del éxito del lanzamiento de la aplicación, el cliente le pide nuevas funcionalidades.
Esto involucra el desarrollo de nuevas aplicaciones y la modificación del servidor existente.

ARQUITECTURA Y REQUERIMIENTOS FUNCIONALES

El sistema consta de al menos siete módulos de software:

●  Servidor.
●  Cliente.
●  Cliente administrador interno
●  Servidor especializado en reportes de usuarios
●  Servidor especializado en reportes de vuelos
●  Servidor de reservas online
●  Cliente de Reservas online

REQUERIMIENTOS FUNCIONALES

●  Aplicación Cliente administrador interno.

o  CAI1. Visualización en vivo de los próximos pasajes comprados. El cliente debe ser capaz de
ver  en  vivo  los  próximos  pasajes  comprados  indicando  vuelo  y  comprador.  El  tiempo  de
respuesta debe ser considerablemente bajo.

o  CAI2.  Cancelación  de  vuelos  por  motivos  de  fuerza  mayor.  El  cliente  debe  ser  capaz  de
enviar al servidor principal una solicitud para cancelar un vuelo indicando código de vuelo y
motivo de la cancelación.

Escuela de Ingeniería

Obligatorio de: Programación de Redes

Código de materia: 4864

Fecha: Marzo 2026

Grupo: Todos

Hoja 3 de 7

●  Aplicación Servidor Especializado en Reportes de Usuario.

o  SERU1. Generación de reportes por usuario. Se debe mover la implementación de reportes

creada previamente a un nuevo servidor, aligerando así la carga sobre el servidor principal.

o  SERU2.  Obtener  estado  de  reporte.  Los  reportes  pueden  llegar  a  demorar  mucho,  por lo
que  se solicita poder obtener en vivo el progreso (cantidad de reportes a hacer / Cantidad
de reportes hechos) que va teniendo un reporte.

o  SERU3.  Obtener  reporte  finalizado.  Se  debe  poder  ver  el  resultado  de  los  reportes  que

están finalizados.

●  Aplicación Servidor Especializado en Reportes de Vuelos

o  SERV1.  Generación  de  reportes  para  vuelos  cancelados.  Aquellos  vuelos  que  fueron

cancelados por medio del requerimiento CAI2 deben generar un reporte indicando

i.

ii.

Información del vuelo

Hora de la cancelación

iii.  Motivo de la cancelación

iv.

Nombre o identificador de la persona que canceló el vuelo

o  SERV2.  Generación  de  reportes  paralela.  Se debe ser capaz de aumentar manualmente la
cantidad  de  instancia  del  servidor de forma tal que en casos de alta demanda de reportes
estos se puedan hacer de forma paralela repartiendo la carga entre los distintos servidores.

o  SERV3.  Envío  de  reportes  finalizados.  Los  reportes  que  sean  generados   deben  tener  la
opción  de  aceptar  una  URL  a  un  webhook.  Luego  de  finalizado  el  reporte  el  sistema
automáticamente debe notificar al webhook indicando identificador del reporte.

●  Aplicación Servidor de Reservas Online

o  SRO1.  Gestión  de  reservas  online.  Se  debe  crear  una  aplicación  independiente  para
visualizar  la  disponibilidad  de  los  distintos  vuelos.  Dicho  requerimiento  debe  tener  bajos
tiempos de respuesta.

●  Aplicación Cliente de Reservas Online

o  CRO1.  Reservar  pasajes  online.  Se  debe  permitir  que  los  clientes  seleccionen  un  vuelo  y

adquieran los pasajes al vuelo que desean.

o  CRO2.  Cancelación  de  pasajes  online.  Se  debe  permitir  que  los  clientes  visualicen  sus
pasajes  comprados  y  en  caso  de  que  el avión aún no haya despegado se puedan cancelar
dichos boletos.

Escuela de Ingeniería

Obligatorio de: Programación de Redes

Código de materia: 4864

Fecha: Marzo 2026

Grupo: Todos

Hoja 4 de 7

o  CRO3. Visualización Cuando un pasaje se reserva o se cancela se debe actualizar la cantidad

de vuelos disponibles en el resto de clientes de reservas que estén activos.

●  Aplicaciones Servidor y Cliente

Mismos clientes/servidor que el obligatorio 1 y 2

●  SCR1: Se puede ignorar la funcionalidad de reservar pasajes por medio del cliente.

Se deben seguir cumpliendo los requerimientos funcionales de las entregas anteriores.

ESPECIFICACIÓN DE TECNOLOGÍAS

1.  Se debe utilizar gRPC, RabbitMQ, REST API y WebSocket. En todos los casos deben utilizar el modelo

asincrónico de .NET.

2.  Que aplicación usa que tecnología depende del equipo, pudiéndose distribuir las mismas como el

equipo entienda mejor, siendo obligatorio utilizar las 4.

3.  Docker:

○  Se debe utilizar Docker para desplegar todas las aplicaciones que sean posibles.

○

 RabbitMQ se debe desplegar utilizando Docker.

○  Se puede utilizar docker-compose para desplegar todas las distintas imágenes.

○  El servidor principal debe utilizar Docker multi-stage build, y el alumno debe justificar, con

números  concretos, por qué el uso de multi-stage build puede ser mejor que un single-stage
build.

DOCUMENTACIÓN

Como  documentación  se  espera  que  se  incluya  las  modificaciones  realizadas  al código y la motivación de
estas  así  también  como  la  arquitectura  principal  (qué  módulos  o  componentes  tiene  el  sistema y en qué
tecnologías fueron desarrollados y porque se eligió dicha tecnología).

Se tomarán en cuenta los siguientes puntos para la corrección de la documentación:

o  Descripción de la arquitectura.
o  Documentación de diseño detallada de cada componente.

Obligatorio de: Programación de Redes

Código de materia: 4864

Fecha: Marzo 2026

Grupo: Todos

Hoja 5 de 7

Escuela de Ingeniería

o

Justificación/motivación  de  mecanismos  de  comunicación  de  los  componentes  de  su
solución
Justificación de las decisiones importantes de diseño e implementación.

o
o  Prolijidad, completitud y organización del documento
o  Correcta utilización de diagramas (deben utilizarse para aportar claridad a lo documentando,

pueden realizar los diagramas que consideren necesarios o útiles)

o  Descripción  de  archivos  de  configuración  e  información  relevante sobre la ejecución de la

solución

o  Supuestos que se hayan tomado
o  Cambios que consideren relevantes sobre requerimientos anteriores
o  Detalle de errores encontrados y posibles mejoras

ENTREGA

Se debe entregar por Gestión un archivo PDF que contenga la documentación  y en la primera página el link
al repositorio con el código.

El repositorio debe incluir una colección de Postman o similar (aclarar en la documentación) para probar las
acciones disponibles que expongan los servicios pertinentes.

Deben usar el mismo repositorio que la entrega anterior el cual debe pertenecer a la organización de GitHub
“ArqSis-PDR-2026-1”.

Al  realizar  la  entrega  se  debe  realizar  un  release  en  el  repositorio  y  la rama master (o main) no debe ser
afectada luego del hito que corresponde a la entrega del obligatorio.

Dependiendo de la aplicación deben incluir en el repositorio:

●  Archivos  Dockerfile  con

instrucciones  para  ejecutar

las

imágenes  y  opcionalmente  el

docker-compose.yml. Opcionalmente pueden subir sus imágenes a DockerHub.

●  Las  aplicaciones  compiladas  en  release,  con  ejecutables  para  Windows  (.exe,  .dll,  etc.)   y  todo  lo

necesario para poder ejecutarlas (archivos de configuración, etc.)

Datos de prueba (opcional)
Entregar el sistema con una fuente de datos de prueba propia para facilitar la corrección.

EVALUACIÓN
El criterio para la corrección se basará en los siguientes aspectos:

●  Deberán evitar el uso de la clase Thread para el desarrollo del obligatorio.

●  Para  esta  entrega  se  permitirá  que  se  utilicen  parsers  existentes  (como  puede  ser  Json)  para

serializar objetos.

●  En la defensa se correrá un plan de testing, donde se evaluará cuantos puntos pasa el sistema.

Obligatorio de: Programación de Redes

Código de materia: 4864

Fecha: Marzo 2026

Grupo: Todos

Hoja 6 de 7

Escuela de Ingeniería

●  Criterios de Diseño.

●  Documentación presentada.

●  Correcta utilización de archivos de configuración

●  Porcentaje de cumplimiento con los requerimientos presentados.

●  Funcionamiento de la aplicación.

●  Calidad de diseño e implementación.

DEFENSA

La defensa del trabajo intenta:

-

-

Evaluar el conocimiento general de los integrantes del grupo sobre la solución propuesta. Todos los
integrantes deben conocer toda la solución.
Evaluar el aporte individual al trabajo por parte de cada uno de los integrantes del equipo. Se espera
que  como  mínimo  cada  uno  de  los  integrantes  haya  participado  en  la  codificación  de  al  menos
alguna parte significativa del obligatorio.

Información importante

Plazo máximo de entrega: 22/06/2026

Puntaje mínimo/máximo: 5/25 puntos.

Defensas: a coordinar.

Los obligatorios se forman como máximo por grupos de 3 estudiantes.

Todas las entregas se realizan en gestión (gestión.ort.edu.uy) y hasta las 21.00 hs del día de entrega.

Escuela de Ingeniería

Obligatorio de: Programación de Redes

Código de materia: 4864

Fecha: Marzo 2026

Grupo: Todos

Hoja 7 de 7

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

