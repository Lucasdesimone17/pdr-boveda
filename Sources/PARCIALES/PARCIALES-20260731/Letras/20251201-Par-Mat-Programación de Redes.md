Fecha: 1/12/2025

Evaluación: Parcial 1

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Matutino

Puntaje Máximo:     35 Puntos

Página 1 de 3

Ejercicio 1

A.  Deﬁna programa, proceso e hilo. Incluya la relación con el uso de recursos, CPU y memoria.
B.  Explique las ventajas de utilizar Task en lugar de Thread en .NET. Describa cómo alguna de estas

ventajas se relaciona con el concepto de Thread Pool.

C.  Un sistema utiliza colas de mensajes para procesar los resultados de encuestas de forma

asincrónica. ¿Qué recomienda para el manejo de errores en caso de que falle el procesamiento
de un mensaje?

D.  Explique la diferencia entre short-polling y long-polling, incluya un diagrama
E.  Explica la diferencia entre operaciones de entrada/salida (I/O bound) y operaciones CPU-bound.

Incluya cómo se relacionan con el concepto de concurrencia o el de paralelismo.

Ejercicio 2

Dado los siguientes logs de comunicación entre servicios:

Hora

IP Origen

Puerto
Origen

IP Destino

Puerto
Destino  Protocolo

Verbo
HTTP

Ruta

Status
Code

14:00:03  18.211.45.92

54011  203.0.113.77

443  HTTPS

POST

14:00:03  203.0.113.77

443  18.211.45.92

54011  HTTPS

/task/start

202

14:05:06  203.0.113.77

51822  18.211.45.92

443  HTTPS

POST

14:05:06  18.211.45.92

443  203.0.113.77

51822  HTTPS

/callback/result

200

1.  Realice un diagrama mostrando la comunicación
2.  Explique y justiﬁque qué patrón o tecnología/s se está utilizando y cuáles son sus beneﬁcios.

Fecha: 1/12/2025

Evaluación: Parcial 1

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Matutino

Puntaje Máximo:     35 Puntos

Página 2 de 3

Ejercicio 3

Usted trabaja en ConsultORT, una consultora de comunicaciones entre aplicaciones digitales. Parte de tu
trabajo es evaluar los mecanismos de comunicación que utilizan los diferentes servicios de aplicaciones
digitales y realizar recomendaciones de los mismos.

Fue  contratado  por  la  empresa  FotosYa,  una  compañía  para  aﬁcionados  de  la fotografía. Tiene varios
servicios,  incluyendo  plataformas  digitales  y  servicios  de  imprenta.  Por  reseñas  de  los  usuarios  (la
aplicación  no  anda  en  todos  los navegadores, es lenta), los trabajadores de FotosYa detectaron que la
forma de comunicar los diferentes servicios que tienen no era la mejor, así que contratan a ConsultORT
para que los ayuden.

La plataforma tiene los siguientes servicios:

●  Una aplicación cliente, que funciona como red social donde los usuarios ponen sus fotos y otros
pueden darle like y comentar. Desde esta aplicación los usuarios piden la impresión de las fotos.
Esta aplicación es web. El nombre interno de esta aplicación es InstaSnap.

●  Otra aplicación cliente, que funciona como un editor de fotos. Acá los usuarios pueden tomar sus
fotos  de  InstaSnap,  editarlas  y hasta mejorarlas con IA. Es un editor de fotos muy potente, que
hasta  permite  editar  fotos  de  manera  colaborativa,  es  decir,  con  varios  usuarios  editando  la
misma foto al mismo tiempo. El nombre interno de esta aplicación es RS. Esta aplicación es web,
que  los usuarios la perciben como extremadamente lenta. El nombre interno de esta aplicación
es Kanva.

●  Un  servidor  backend,  que  guarda  en  su  base  de  datos  toda  la información de los usuarios, las
fotos, los likes y comentarios, entre otros datos. El nombre interno de esta aplicación es Servidor
Principal.

●  Para  intentar  mejorar  la performance, separaron el backend de la aplicación Kanva del Servidor
Principal,  por lo que crearon un segundo servidor para manejar sus peticiones. Este servidor no
maneja  una  base  de  datos,  pero  debe  ser  capaz  de  traer  en  tiempo  real  las  imágenes  de
InstaSnap  para  que  la  aplicación  ﬂuya  mejor.  Este  servidor  también  tiene  dentro  el  modelo  de
mejora de fotos de IA. El nombre interno de esta aplicación es Servidor Fotos.

●  Un cliente interno para los trabajadores de la imprenta, que se usa para manejar las peticiones de
la  imprenta.  La opción de imprimir fotos es solo válida para usuarios de InstaSnap. Cuando los
clientes  hacen  la  petición  de  imprimir una foto, saben que queda lista a los 7 días hábiles y no
tienen  que  recibir  una  notiﬁcación  de  que  está  lista.   El  nombre  interno  de  esta  aplicación  es
Cliente Imprenta.

Fecha: 1/12/2025

Evaluación: Parcial 1

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Matutino

Puntaje Máximo:     35 Puntos

Página 3 de 3

El diagrama de los servicios y sus conexiones es el siguiente:

Como parte de tus servicios, deberás:

●  Plantear  un  nuevo  diagrama  de  comunicación  entre  los  componentes  de  la  plataforma  de  la
empresa  FotosYa,  donde  hagas  cambios  a  los  mecanismos  de  comunicación  actuales  o  los
mantengas. Deberás justiﬁcar todas tus decisiones.

○  El stack tecnológico de los trabajadores de FotosYa es: gRPC, API Rest, MOM, Sockets y
WebSockets. Además, saben implementar Polling (Long y Short) y Server Sent Events. Por
lo que podrás usar cualquiera de estas tecnologías.

●  De  acuerdo  a  la  solución  propuesta,  para  facilitar  el  trabajo  a  los  programadores  de  FotosYa

deberás hacer dos de las siguientes tres cosas, de acuerdo al diagrama realizado:

○  Para  API  Rest,  deﬁnir  hasta  5  endpoints,  poniendo

la  URI  y  el  verbo  HTTP

correspondiente.

○  Para gRPC, deﬁnir el archivo .proto que describe los servicios y mensajes necesarios para
la  comunicación.  (en  el  caso de usarlo para el envío de imágenes, puede asumir que se
pasan como un array de bytes)

○  Para MOM, deﬁna las colas utilizadas para distribuir los eventos de uso y mantenimiento
entre  distintos  componentes.  Indique  el  tipo  de  exchange,  las  colas  y  los  mensajes
enviados (en formato JSON o XML).

Aclaraciones:

-  Puede realizar los supuestos que crea necesarios. Deberán documentarlos en la respuesta.
-  Puede  usar  más de un mecanismo de comunicación entre dos componentes. Los mecanismos

de comunicación se pueden repetir.

