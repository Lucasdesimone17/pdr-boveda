<!-- Slide number: 1 -->

![preencoded.png](GoogleShape40p10.jpg)
¿Qué es Docker?
Docker es un proyecto de código abierto que permite a los desarrolladores crear, probar y desplegar aplicaciones de forma rápida y eficiente. Es una herramienta indispensable para el desarrollo moderno, permitiendo crear contenedores que encapsulan las dependencias de una aplicación y la ejecutan de forma independiente en cualquier entorno.

### Notes:

<!-- Slide number: 2 -->

![](GoogleShape47p11.jpg)

### Notes:

<!-- Slide number: 3 -->
Propósito y beneficios de Docker

Eficiencia en el desarrollo

Portabilidad
1
2
Permite ejecutar aplicaciones en diferentes entornos (sistemas operativos, plataformas) sin necesidad de modificaciones.
Simplifica el proceso de desarrollo al garantizar que el código se ejecute de forma consistente en cualquier lugar.

Escalabilidad

Seguridad
4
3
Facilita el escalado de aplicaciones, permitiendo replicar contenedores fácilmente para gestionar el tráfico creciente.
Aísla las aplicaciones en contenedores independientes, mejorando la seguridad al evitar conflictos entre aplicaciones.

### Notes:

<!-- Slide number: 4 -->
Conceptos básicos de Docker: Imágenes y Contenedores
Imágenes
Contenedores
Son instancias en ejecución de una imagen. Un contenedor es un entorno aislado que contiene todo lo que necesita para ejecutar una aplicación, incluyendo el sistema operativo, las bibliotecas y los archivos de configuración.
Son plantillas que contienen el código, las dependencias, las bibliotecas y la configuración necesaria para ejecutar una aplicación. Las imágenes se almacenan en repositorios como Docker Hub y se pueden compartir fácilmente con otros.

### Notes:

<!-- Slide number: 5 -->

![](GoogleShape84p14.jpg)

### Notes:

<!-- Slide number: 6 -->
Comandos básicos de Docker

![preencoded.png](GoogleShape92p15.jpg)
docker build
Crea una imagen Docker a partir de un Dockerfile.

![preencoded.png](GoogleShape90p15.jpg)

![preencoded.png](GoogleShape95p15.jpg)
docker run
Ejecuta una imagen Docker como un contenedor.

![preencoded.png](GoogleShape98p15.jpg)
docker ps
Muestra una lista de los contenedores en ejecución.

![preencoded.png](GoogleShape101p15.jpg)
docker stop
Detiene un contenedor en ejecución.

![preencoded.png](GoogleShape104p15.jpg)
docker rm
Elimina un contenedor.

### Notes:

<!-- Slide number: 7 -->

![preencoded.png](GoogleShape112p16.jpg)

![preencoded.png](GoogleShape113p16.jpg)
Dockerfile
Un Dockerfile es un archivo de texto que contiene instrucciones para construir una imagen Docker. Cada instrucción en el Dockerfile define un paso en el proceso de construcción de la imagen.

### Notes:

<!-- Slide number: 8 -->
Estructura y componentes de un Dockerfile

FROM

1

Define la imagen base, proporcionando un punto de partida para construir la imagen.
RUN

2

Ejecuta comandos para instalar dependencias, configurar el entorno y ejecutar tareas dentro de la imagen.
COPY

3

Copia archivos desde el sistema de archivos local al contenedor, como el código fuente o la configuración de la aplicación.
WORKDIR

4

Establece el directorio de trabajo dentro del contenedor, donde se ejecutan los comandos.
CMD

5

Define el comando que se ejecutará cuando se inicia un contenedor, como iniciar un servidor web o ejecutar una aplicación.

### Notes:
https://docs.docker.com/reference/dockerfile/#expose

<!-- Slide number: 9 -->
Ejemplos de uso de Docker
Docker es utilizado en una variedad de escenarios, incluyendo el desarrollo de aplicaciones web, la creación de entornos de prueba, la gestión de bases de datos y la implementación de microservicios.

![preencoded.png](GoogleShape155p18.jpg)

![preencoded.png](GoogleShape158p18.jpg)
Desarrollo web
Gestión de bases de datos
Facilita la creación de entornos de desarrollo web consistentes, asegurando que las aplicaciones se ejecuten igual en cualquier lugar.
Permite ejecutar diferentes versiones de bases de datos en contenedores separados, simplificando el manejo y la configuración.

### Notes:

<!-- Slide number: 10 -->
Docker Compose
Es una herramienta que permite definir y ejecutar múltiples contenedores Docker como un servicio. Con Docker Compose, podemos configurar la forma en que se conectan y se relacionan varios contenedores (como aplicaciones frontend, bases de datos, y servicios de mensajería) en un archivo de configuración llamado docker-compose.yml.

Se enfoca en describir cómo desplegar y coordinar contenedores.

![](GoogleShape168p19.jpg)

### Notes:

<!-- Slide number: 11 -->
Dockerfile y Docker Compose
| Dockerfile | Docker Compose |
| --- | --- |
| Define cómo construir una imagen. | Define cómo ejecutar múltiples contenedores. |
| Describe el entorno y dependencias de un solo contenedor. | Maneja la configuración de múltiples contenedores. |
| Comandos principales: FROM, COPY, RUN, ENTRYPOINT, CMD. | Comandos principales: docker-compose up, down, build, logs. |
| No gestiona redes ni volúmenes. | Define redes y volúmenes para múltiples contenedores. |

### Notes:

<!-- Slide number: 12 -->

![Docker in a Nutshell. Well, my friends if you are reading… | by A. Yigit Ogun | DevOps.dev](GoogleShape180p21.jpg)

### Notes: