DOCKER

Qué es?
Para que lo usamos?
Primeros pasos…

Renzo Chiarino
2025

Contenido

01 - Que es Docker

04 - Dockerﬁle

Introducimos Docker como
plataforma junto con los conceptos
más importantes

Explicación de los diferentes usos y
comandos que se pueden incluir

02 - Virtualización

05 - Almacenamiento

Conocemos la virtualización y la
compraramos frente a Docker

Cómo persistir información dentro de
un contenedor y cómo vincular un
directorio

03 - Usando Docker

06 - Networks y Docker compose

Comenzamos a utilizar Docker
generando nuestros propios
contenedores e imágenes

Concepto de redes y cómo se crean,
para después empezar a utilizar
muchos servicios interconectados

01

Que es?

DEFINICIÓN FORMAL

Docker es una plataforma de código abierto que
permite desarrollar, enviar y ejecutar aplicaciones
dentro de contenedores, proporcionando un
entorno ligero, portable y autosuﬁciente.

DEFINICIÓN INFORMAL

En otras palabras Docker, no es más que un proceso que corre en
nuestro sistema operativo. (host)
Este nos nos ayuda a ejecutar contenedores dentro del mismo, y es en
estos contenedores es donde los desarrolladores pueden trabajar y
crear sus propias aplicaciones.

BIEN Y QUE SON ESTOS “Contenedores”

Contenedores

01

QUÉ SON?

Un contenedor es un paquete de software aislado que
incluye una aplicación y todas sus dependencias
necesarias para ejecutarse en cualquier entorno.

“Es como una caja sellada que contiene todo lo que la
aplicación necesita para funcionar correctamente.”

EJEMPLOS

Redis     Python   Node.js      Nginx – Apache    RabbitMQ

De donde salen?

Los contenedores se generan a partir de imágenes.
Estas imágenes las podemos generar nosotros o utilizar
imágenes públicas en docker hub.

01

DOCKER HUB

hub.docker.com

GENERADA POR NOSOTROS
Para generar una imagen Docker necesita que deﬁnamos
ciertos pasos y conﬁguraciones que va a necesitar para
poder empaquetar nuestra aplicación con sus dependencias
dentro de la imagen.

Estos “pasos” los vamos a deﬁnir dentro de un archivo
conocido como: Dockerﬁle  no tiene extensión

Dentro de este archivo vamos documentar los pasos que
queremos que Docker realice cuando “buildee” la imagen.

Esta página es una biblioteca pública de imágenes que
podemos utilizar en nuestros contenedores.

También podemos subir nuestras propias imágenes..

Virtualización

02

Qué pasa con Docker y las VM?

La ventaja de los contenedores es que múltiples
aplicaciones pueden estar “corriendo” en el mismo OS y
compartir el kernel. (permite a las aplicaciones acceder al hardware)

Estos contenedores, corren en procesos separados y están
aislados el uno del otro. Los contenedores son más livianos
que las VM y administran mejor los recursos del sistema.

En cambio, las máquinas virtuales (VM), simulan todo el
equipo, esto engloba hardware y software, es por eso que
requieren mejor infraestructura y son más pesadas que los
contenedores.

02

Entonces…

CUANDO USAMOS DOCKER?

Vamos a usar Docker cuando:

Necesitemos desplegar software de forma rápida.

Desarrollar y probar de forma iterativa.

Cuando necesitamos manejar una arquitectura de
microservicios. (cada componente es independiente del resto)

Cuando necesitamos escalar horizontal, esto es algo que
los contenedores pueden hacer de forma muy simple.

Comencemos

Para descargar Docker vamos a: https://docker.com

03

03

Comandos

Diferentes usos

docker actions [options]

docker run [options] imagen  - Nos permite ejecutar un contenedor con la imagen especiﬁcada

docker build [options] - Nos crear una imagen a partir de un Dockerﬁle (son la base de todas las imágenes)

docker images [options] - Lista las imágenes que tenemos guardadas en Docker y podemos usar

docker ps [options] - Lista los contenedores que se encuentran en ejecución

docker start o stop nombre_del_container - Permite iniciar o detener un contenedor

03

Run

Para ejecutar una imagen utilizamos
documentación

docker run [options] imagen  - Nos permite ejecutar un contenedor con la imagen especiﬁcada.

En caso que la imagen no esté, va a buscar en docker hub.

OPCIONES QUE PODEMOS USAR

-d: Ejecuta el contenedor en segundo plano y nos muestra el ID

-p: Se usa para especiﬁcar los puertos (uso: -p puerto-host:puerto-contenedor)

-e: Establecer variables de entorno

--name: Establece el nombre del contenedor

-i: Agrega un proceso de entrada (conjunto con el -t nos permite interactuar con el contenedor -it)

-t: Asigna un  pseudo-TTY (asociación entre el proceso que ejecuta el comando y el proceso que corre el container)

--mount / -v: Asocian/dan persistencia a un contenedor

Build

Para generar una imagen desde cero
documentación

03

docker build [options] ruta_al_Dockerﬁle  - Nos permite crear una imagen en Docker a partir de un Dockerﬁle local, esta imagen

se almacena de forma local y tenemos la opción de publicarla en docker hub.

OPCIONES QUE PODEMOS USAR

-t: Nos permite asignar un nombre a la imagen (tag)

-q: Quiet, no nos muestra los logs al buildear

--no-cache: Deshabilita el uso del caché, forzando la construcción completa

Estas son algunas de las más usadas, pero el comando admite otras opciones que no son de interes para
este curso.

Dockerﬁle

04

“Es como si fuera una receta de cocina, donde le decimos los ingredientes,
proporciones y los pasos para obtener el resultado esperado”

En nuestro caso una imagen para ejecutar en los contenedores.
Los pasos tienen un orden y se ejecutan en secuencia.

docker build -t mi-imagen-custom .

Le indicamos que queremos generar una imagen ‘mi-imagen-custom’ y que utilice el
Dockerﬁle que se encuentra en la misma carpeta que ejecutamos el comando.

Si queremos generar una imagen de otro Dockerﬁle debemos reemplazar el ‘.’ por la ruta
completa hacia el archivo.

Ejemplo 1

04

FROM node:16-alpine    # Utilizamos una imagen base de Node.js

WORKDIR /app

# Establecemos el directorio de trabajo

COPY package*.json ./   # Copiamos los archivos de nuestra aplicación

RUN npm install

# Instalamos las dependencias

COPY . .

# Copiamos el resto de los archivos de la aplicación

EXPOSE 3000

# Exponemos el puerto 3000

CMD ["node", "app.js"]

# Ejecutamos nuestra aplicación Node.js

Ejemplo 2

04

FROM node:16-alpine

WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . .

EXPOSE 3000

ENTRYPOINT ["node", "app.js"]    # Ejecutamos el contenedor con un entrypoint

Cual usamos?

04

CMD: Especiﬁca el comando por defecto que va a ejecutar cuando el container sea iniciado.
Generalmente se usa para deﬁnir los parámetros por defecto que corre nuestra imagen, ya que
son editables desde la consola…

CMD ["node", "app.js"]    # CMD en nuestro Dockerﬁle

Si ejecutamos: docker run -it mi-imagen-custom /bin/bash, nos ejecuta la consola en vez de
la aplicación en node.

ENTRYPOINT: Especiﬁca las instrucciones por defecto en nuestro contenedor, esta no
admiten cambios por parámetros a menos que utilicemos --entrypoint como ﬂag.

Si el usuario decide agregar parámetros como en el caso anterior, estos se van a sumar a los ya
existentes en el entrypoint y no reemplazarlos. Es una forma de “pasar parámetros” al
contenedor y variar la ejecución sin tener que recompilar la imagen.

Los dos…

04

Se puede utilizar ambos en simultáneo, en este caso usamos CMD para establecer los
parámetros en caso de que el usuario no ingrese al ejecutar el contenedor.

ENTRYPOINT [“sleep”]
CMD 5

docker run mi-imagen 15
docker run mi-imagen

# Duerme por 15s, ya que envíe el parámetro
# Utiliza 5s por no haber recibido parámetros y existir el CMD.

Volumenes

BIND MOUNT         \       VOLUME         \        TMPFS

05

Los contenedores de Docker tienen un sistema de archivos que está aislado del
sistema host. Esto permite una mejor portabilidad y seguridad.

Sin embargo, también plantea la cuestión de cómo persistir los datos más allá de la
vida útil de un contenedor.

05

Bind Mount

Un bind mount es una forma de montar un directorio en el sistema host directamente
en un contenedor Docker.

Esto nos permite realizar cambios desde el host y que estos impacten en nuestro
contenedor, pero también lo vuelve dependiente de que sistema utilizamos en
nuestro equipo host.

A modo de ejemplo:
Para montar el directorio /var/www/html del host en el contenedor /app se puede
utilizar el siguiente comando:

docker run -v /var/www/html:/app -it …

Esto genera un nuevo contenedor y monta el directorio del host en el directorio /app
del contenedor. Cualquier cambio realizado en el directorio del host será visible
inmediatamente en el contenedor.

Volume

05

Los volúmenes son una herramienta esencial en Docker para gestionar datos de
forma persistente y segura. Al utilizar volúmenes y estos ser administrados por
Docker, nos desvincula completamente de la dependencia hacia el sistema operativo
del equipo host.

Los volúmenes pueden ser compartidos entre múltiples contenedores, a su vez
estos no se destruyen automáticamente cuando un contenedor es eliminado o
detenido, permitiendo así mantener la persistencia de los datos.

A modo de ejemplo:

Creamos un nuevo volumen:

Utilizando el volumen en un contenedor:

docker volume create my-vol

docker run -d -v my-vol:/ruta/en/el/container imagen

Networking

En Docker cuando creamos un nuevo contenedor, por defecto no se expone ningún
puerto, es por eso que para hacerlo lo tenemos que indicar usando la ﬂag.

-p puertoHost:puertoContainer

También podemos crear redes virtuales entre los contenedores, esto nos permite
intercomunicarse y intercambiar información.

06

Crear una red: docker network create -d bridge my-net

Usando la red: docker run --network=my-net …

Usualmente si necesitamos una network también vamos a usar un docker compose,
por lo que generalmente las redes se establecen ahí mismo y no desde la consola.

Docker compose

Docker Compose es una herramienta que permite deﬁnir y ejecutar aplicaciones
multi-contenedor de Docker.

06

Un docker-compose.yaml o compose.yaml es un archivo de conﬁguración que
describe los servicios (contenedores) de una aplicación y sus dependencias.

Ejecutamos un docker compose con el siguiente comando:

docker compose up

Ejemplo

services:
  web:
    build:  .
    ports:
      - "8000:5000"
  redis:
    image: "redis:alpine"

// Primer servicio, se va a crear un container “web”
// Podemos indicar una ruta hacia un Dockerﬁle

 // Segundo servicio, se va a crear un container “redis”
// Si no vamos a usar un Dockerﬁle, podemos usar la
imagen directamente, como si la estuviéramos
usando en el docker run.

06

