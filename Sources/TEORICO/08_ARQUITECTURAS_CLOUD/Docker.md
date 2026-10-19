¿Qué es Docker?

Docker es un proyecto de código abierto que
permite a los desarrolladores crear, probar y
desplegar aplicaciones de forma rápida y eficiente.
Es una herramienta indispensable para el desarrollo
moderno, permitiendo crear contenedores que
encapsulan las dependencias de una aplicación y la
ejecutan de forma independiente en cualquier
entorno.

Propósito y beneficios de
Docker

1 Eficiencia en el desarrollo
Simplifica el proceso de
desarrollo al garantizar que el
código se ejecute de forma
consistente en cualquier lugar.

2 Portabilidad

Permite ejecutar aplicaciones
en diferentes entornos
(sistemas operativos,
plataformas) sin necesidad de
modificaciones.

3 Escalabilidad

4 Seguridad

Facilita el escalado de
aplicaciones, permitiendo
replicar contenedores
fácilmente para gestionar el
tráfico creciente.

Aísla las aplicaciones en
contenedores independientes,
mejorando la seguridad al
evitar conflictos entre
aplicaciones.

Conceptos básicos de Docker: Imágenes y

Contenedores

Imágenes
Son plantillas que contienen el código, las
dependencias, las bibliotecas y la
configuración necesaria para ejecutar una
aplicación. Las imágenes se almacenan en
repositorios como Docker Hub y se
pueden compartir fácilmente con otros.

Contenedores
Son instancias en ejecución de una
imagen. Un contenedor es un entorno
aislado que contiene todo lo que necesita
para ejecutar una aplicación, incluyendo el
sistema operativo, las bibliotecas y los
archivos de configuración.

Comandos básicos de Docker

docker build
Crea una imagen Docker a partir de
un Dockerfile.

docker run
Ejecuta una imagen Docker como
un contenedor.

docker ps
Muestra una lista de los
contenedores en ejecución.

docker stop
Detiene un contenedor en
ejecución.

docker rm
Elimina un contenedor.

Dockerfile

Un Dockerfile es un archivo de texto que contiene instrucciones para construir una
imagen Docker. Cada instrucción en el Dockerfile define un paso en el proceso de
construcción de la imagen.

Estructura y componentes de un Dockerfile

FROM
Define la imagen base,
proporcionando un punto de partida
para construir la imagen.

COPY
Copia archivos desde el sistema de
archivos local al contenedor, como el
código fuente o la configuración de
la aplicación.
CMD
Define el comando que se ejecutará
cuando se inicia un contenedor,
como iniciar un servidor web o
ejecutar una aplicación.

1

2

3

4

5

RUN
Ejecuta comandos para instalar
dependencias, configurar el entorno
y ejecutar tareas dentro de la imagen.

WORKDIR
Establece el directorio de trabajo
dentro del contenedor, donde se
ejecutan los comandos.

Ejemplos de uso de Docker

Docker es utilizado en una variedad de escenarios, incluyendo el desarrollo de
aplicaciones web, la creación de entornos de prueba, la gestión de bases de datos y la
implementación de microservicios.

Desarrollo web
Facilita la creación de entornos de
desarrollo web consistentes, asegurando
que las aplicaciones se ejecuten igual en
cualquier lugar.

Gestión de bases de datos
Permite ejecutar diferentes versiones de
bases de datos en contenedores
separados, simplificando el manejo y la
configuración.

Docker Compose

Es una herramienta que permite definir y ejecutar múltiples contenedores Docker como un
servicio. Con Docker Compose, podemos configurar la forma en que se conectan y se
relacionan varios contenedores (como aplicaciones frontend, bases de datos, y servicios
de mensajería) en un archivo de configuración llamado docker-compose.yml.

Se enfoca en describir cómo desplegar y coordinar contenedores.

Dockerfile y Docker Compose

Dockerfile

Docker Compose

Define cómo construir una imagen.

Define cómo ejecutar múltiples
contenedores.

Describe el entorno y dependencias de un
solo contenedor.

Maneja la configuración de múltiples
contenedores.

Comandos principales: FROM, COPY,
RUN, ENTRYPOINT, CMD.

Comandos principales: docker-compose
up, down, build, logs.

No gestiona redes ni volúmenes.

Define redes y volúmenes para múltiples
contenedores.

