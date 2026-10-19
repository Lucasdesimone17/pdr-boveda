<!-- Slide number: 1 -->
# Docker
Ejemplos

### Notes:

<!-- Slide number: 2 -->
# Bibliografía
Docker For Noobies; Alex Parker
https://www.amazon.com/Docker-Noobies-Alex-Parker-ebook/dp/B0D9DFCJ16

https://dev.to/syedmuhammadaliraza/docker-for-beginners-2lk8
https://docker-curriculum.com/
Más otras referencias en las páginas que siguen.

### Notes:

<!-- Slide number: 3 -->
# Docker Contenedores
“Docker es un proyecto de código abierto que automatiza el despliegue de aplicaciones dentro de contenedores de software, proporcionando una capa adicional de abstracción y automatización de virtualización de aplicaciones en múltiples sistemas operativos.
Docker utiliza características de aislamiento de recursos del kernel Linux, tales como cgroups y espacios de nombres (namespaces) para permitir que "contenedores" independientes se ejecuten dentro de una sola instancia de Linux, evitando la sobrecarga de iniciar y mantener máquinas virtuales.”
Wikipedia

"Docker es una herramienta que puede empaquetar una aplicación y sus dependencias en un contenedor virtual que se puede ejecutar en cualquier servidor Linux. Esto ayuda a permitir la flexibilidad y portabilidad en donde la aplicación se puede ejecutar, ya sea en las instalaciones físicas, la nube pública, nube privada, etc."
Linux.com

### Notes:

<!-- Slide number: 4 -->
# ¿Porqué Docker?
Consistencia del entorno:  Garantiza que la aplicación se ejecute de la misma manera, independientemente de dónde se despliegue.

Aislamiento:  Los contenedores aíslan las aplicaciones entre sí, y pueden ejecutarse sin interferir unas con otras.

Eficiencia de recursos:  A diferencia de la Virtualización completa, los contenedores comparten el núcleo del sistema operativo anfitrión, y en Linux utilizan extensiones de las funciones de seguridad y protección del propio sistema operativo, lo que los hace ligeros y más rápidos.

Portabilidad:  Los contenedores Docker pueden ejecutarse en cualquier sistema compatible con Docker (Linux, Windows, Mac), lo que facilita la migración de aplicaciones entre sistemas y entornos de desarrollo, pruebas y producción.

### Notes:

<!-- Slide number: 5 -->
# Docker Contenedores
¿Qué es un contenedor?
Es un proceso que se ejecuta en un host, el cual está aislado de todos los demás procesos que se ejecutan en ese host.
En Linux, ese aislamiento aprovecha los namespaces y los cgroups del kernel.
 Estas capacidades son mecanismos de seguridad para el control de aplicaciones.
Docker hace que el uso de contenedores basados en estas capacidades sea más fácil de realizar para usuarios y desarrolladores.
Un contenedor es una instancia ejecutable de una imagen.
Un contenedor es un paquete ejecutable de software, aislado, liviano e  independiente que incluye todo lo que una aplicación necesita. Por lo que son un entorno conveniente para la distribución en las fases de desarrollo y despliegue.
Un contenedor de Docker es una instancia ejecutable de la imagen. Esta se puede crear, iniciar(ejecutar), detener, mover o borrar con comandos de Docker.

### Notes:

<!-- Slide number: 6 -->
# Docker Contenedores
¿Qué es una imagen?
Un contenedor en ejecución utiliza un sistema de archivos aislado.
Este sistema de archivos aislado lo proporciona una imagen.
La imagen es entonces una copia del  sistema de archivos inicial del contenedor.
La imagen debe contener todo lo necesario para ejecutar la o las aplicaciones contenidas.
Esto incluye: todas las dependencias, configuraciones, scripts, binarios, etc.
La imagen también contiene otras configuraciones para el contenedor, como variables de entorno, un comando predeterminado para ejecutar y otros metadatos.
Una imagen de Docker, es la estructura del sistema de archivos, y las configuraciones necesarias para instanciar un contenedor.

### Notes:

<!-- Slide number: 7 -->
# Docker Contenedores
Con los contenedores los podemos crear, iniciar, detener, mover o eliminar utilizando el Docker Desktop, la API de Docker o por línea de comando.
Puede ejecutarse en máquinas locales, máquinas virtuales o desplegarse en la nube.
Al ser funcionalidades del sistema operativo podemos ejecutarlas en máquinas virtuales de Virtualización Completa (Hipervisor). Podemos por ejemplo hacer una MV que contenga múltiples aplicaciones en sus respectivos contenedores.
Es portátil y puede ejecutarse en una amplia gama de versiones del kernel.
Está aislado de otros contenedores y ejecuta su propio software, binarios, configuraciones, etc.

### Notes:

<!-- Slide number: 8 -->
# Docker Contenedores
El concepto de namespaces fue definido por los creadores del sistema operativo Plan 9 de Bell Labs a finales de los 80 y luego incorporado en el kernel de linux en el 2002.
OpenVZ en 2005 permite virtualizar espacios de Linux dentro de Linux con un buen nivel de encapsulamiento. Esto nos permite tener múltiples ambientes aislados de usuarios, compartiendo el kernel. Incluso permitiendo teletransportar una máquina virtual entre dos hosts en pocos segundos. Es muy eficiente, personalmente administraba más de 100 entornos virtuales por máquina, esta a su vez con hardware virtualizado.
cgroups comenzó como proyecto en Google en 2006 y se incorporó al kernel de Linux en enero de 2008.
Muchas extensiones futuras para Linux las podemos ver en las versiones de (paravirtualización de) los proveedores de la nube hoy.
Ya Linux era la infraestructura preferida para soportar sistemas en la nube.

### Notes:

<!-- Slide number: 9 -->
# Docker Contenedores
Podemos correr Windows o macOS en un contenedor en Linux! Usando KVM. https://hub.docker.com/r/dockurr/windows https://hub.docker.com/_/microsoft-windows-base-os-images https://hub.docker.com/_/microsoft-dotnet
Microsoft a su vez desarrolló su Windows Server Containers (2015) y Utiliza su Host Compute Service como interface entre la aplicación y los Job objects y object namespace, etc. Soporta Docker desde el Windows 10 Anniversary Update en agosto de 2016.
Conocer el funcionamiento interno de contenedores en Windows es complicado debido a su código cerrado y restricciones legales.
Docker puede usar funcionalidades de Virtualización Completa en todas las plataformas. KVM en Linux, Hyper-V en Microsoft, e Hyperkit en macOS.

### Notes:

<!-- Slide number: 10 -->
# Docker Contenedores
Docker para macOS es posible gracias a la comunidad de software abierto, macOS container en github. Pero el sitio “desapareció” de github y de https://macoscontainers.org/ Alguna información sugiere problemas de licencias. Las soluciones en general se basaban en VM conteniendo Linux con Docker adentro. Aparentemente macoscontainers tiene un branch en Darwin-Containers e hicieron un release en setiembre de 2023. Nótese que es para Darwin, o sea MacOS libre sin interface gráfica y no aplican las restricciones de correr sólo en hardware Apple. A partir de esa fecha aparentemente Docker soporta macOS sólo para las últimas versiones.  Desde Enero de 2023 Apple no nombra a Darwin y sólo brinda una lista incompleta de fuentes de algunos paquetes de macOS. Ya no brinda una solución abierta de código para el sistema operativo.

### Notes:
https://learn.microsoft.com/en-us/archive/msdn-magazine/2017/april/containers-bringing-docker-to-windows-developers-with-windows-server-containers

<!-- Slide number: 11 -->
# Docker Contenedores
Ahora hay contenedores integrados en macOS, corriendo en un hipervisor (HyperKit), gestionados por Apple y es posible instalar Docker en macOS. Aunque su desempeño no sea el mejor, debido a una virtualización de hardware (liviana) e ineficiencias del acceso al sistema de archivos de macOS. Esto último Docker lo resuelve utilizando su propio mecanismo de virtualización de filesystem desde enero de 2024.
Apple hace referencia que colabora con containerd en su sitio open source, Pero Containerd no nombra ni a Apple ni a macOS.
Hay contenedores en macOS desde setiembre de 2023. Algo muy reciente.
macOS no es un sistema operativo para servidores, sino que es un S.O. para usuarios finales.

### Notes:
https://learn.microsoft.com/en-us/archive/msdn-magazine/2017/april/containers-bringing-docker-to-windows-developers-with-windows-server-containers

<!-- Slide number: 12 -->
# Docker Contenedores
Docker en Linux utiliza namespaces, cgroups y capabilities que son mecanismos de seguridad integrados en el kernel del sistemaoperativo. SOn simplemente extensiones de la seguridad de Linux.
El Windows para poder funcionar Docker es necesario utilizar virtualización por Hardware, por lo que se debe usar el anillo -1. Esto lo logra utilizando su hipervisor Host Compute Service.
En macOS la situación es similar. Debe utilizar el hipervisor (HyperKit) para virtualizar por hardware los contenedores de Docker en macOS.
Esto nos plantea algunas limitaciones. En máquinas virtuales de Windows o  macOS (que las hay, las hay) no es posible tener Docker funcionando dentro a no ser que tengamos capacidad de virtualización anidada. Y hay que ver si está previsto.
En cambio Docker en Linux es una aplicación que usa naturalmente las funcionalidades del kernel, por lo que en cualquier Linux, en cualquier condición, podemos tener contenedores Linux dentro.
No va a ser posible tener un contenedor Windows o macOS ya que precisa del hipervisor KVM para poder ejecutar. A no ser que tengamos virtualización anidada en el hardware.

### Notes:
https://learn.microsoft.com/en-us/archive/msdn-magazine/2017/april/containers-bringing-docker-to-windows-developers-with-windows-server-containers

<!-- Slide number: 13 -->
# Docker Instalación
Para instalar Docker en su sistema vaya a https://docs.docker.com/get-started/get-docker/
Y siga las instrucciones.

Estas pueden cambiar un poco con las versiones.

¡IMPORTANTE! Hay dos versiones una es “Docker Engine” que es la infraestructura de virtualización, sin una interfaz gráfica (aunque hay sucedáneos que se pueden instalar como contenedor). Esta instala de una forma a la infraestructura Docker, que es diferente que la instalación con “Docker Desktop”. Pueden ser incompatibles entre sí. Recomiendo instalar una u otra. Si es para un uso ocasional, Docker Desktop da una interfaz sencilla para la administración. El motor normalmente no se inicia hasta que se abre la aplicación. SI la intención es usarlo activamente y que docker esté como servicio pues muchas aplicaciones las tienen como contenedores o se usa ampliamente en desarrollo, entonces Docker Engine seguramente sea su preferido. Además muchas veces la integración de docker a otros paquetes y sistemas es más automática con “Docker Engine”. Recordar registrar el usuario en el grupo docker.
https://docs.docker.com/engine/install/

### Notes:

<!-- Slide number: 14 -->
# Docker en Debian
Instalación de Docker en Debian. https://docs.docker.com/engine/install/debian/

Primero desinstalar las versiones del repositorio de Debian si hay.
for pkg in docker.io docker-doc docker-compose podman-docker containerd runc; do sudo apt-get remove $pkg; done

Luego agregar los certificados del repositorio de Docker:
# Add Docker's official GPG key:
sudo apt-get update
sudo apt-get install ca-certificates curl
sudo install -m 0755 -d /etc/apt/keyrings
sudo curl -fsSL https://download.docker.com/linux/debian/gpg -o /etc/apt/keyrings/docker.asc
sudo chmod a+r /etc/apt/keyrings/docker.asc

### Notes:

<!-- Slide number: 15 -->
# Docker en Debian
Luego agregar las fuentes de repositorios de Docker:
# Add the repository to Apt sources:
echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.asc] https://download.docker.com/linux/debian $(. /etc/os-release && echo "$VERSION_CODENAME") stable" |   sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
sudo apt-get update

Instalamos la última versión de Docker:
sudo apt-get install docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin

Y verificamos nuestra instalación, creando un contenedor trivial:
sudo docker run hello-world

Otras distribuciones basadas en Debian son similares. Revisar las directivas en el sitio de Docker.

### Notes:

<!-- Slide number: 16 -->
# Docker en Windows
Instalar Docker en Windows.
Para ejecutar contenedores Windows, es necesario Windows 10 u 11 Professional o Enterprise edition. Las ediciones Windows Home o Education sólo permiten ejecutar contenedores Linux.
Hay dos sabores: Hyper-V y WSL.

Descargue el instalador desde la página en docker.com.

Haga doble clic en Docker Desktop Installer.exe para ejecutar el instalador. Por defecto se instala en C:\Archivos de programa\Docker\Docker.

Cuando se le solicite, elija WSL 2 o Hyper-V. Si su sistema sólo admite una, no hay que seleccionar nada.

Siga las instrucciones del asistente de instalación para autorizar el instalador y proceder con la instalación.

Cuando la instalación se haya realizado correctamente, seleccione Cerrar para completar el proceso de instalación.

Normalmente el servicio docker es iniciado por Docker Desktop. Por lo tanto para iniciar un contenedor, se debe ejecutar Docker Desktop, o iniciar el servicio manualmente.

### Notes:

<!-- Slide number: 17 -->
# Docker en MAC
Instalar Docker en MAC.
Descargue el instalador desde la página de Docker.com.

Haga doble clic en Docker.dmg para abrir el instalador y, a continuación, arrastre el icono de Docker a la carpeta Aplicaciones. Por defecto, Docker Desktop se instala en /Applications/Docker.app.

Haga doble clic en Docker.app en la carpeta Aplicaciones para iniciar Docker.

El menú de Docker muestra el Acuerdo de Servicio de Suscripción de Docker.
Siga las indicaciones hasta terminar.

### Notes:

<!-- Slide number: 18 -->
# Docker Contenedores
Docker y otros proveedores tienen sus propios repositorios de imágenes (Contenido inicial de contenedores).
Cualquier desarrollador puede hacer su propio repositorio o agregar contenido al repositorio de Docker. https://hub.docker.com/
En este sitio pueden buscar contenedores hechos por desarrolladores independientes, Docker mismo u otros proveedores.

Docker Hub es un servicio en la nube, donde encontrar y compartir imágenes de contenedores con los grupos que deseas, incluyendo a la comunidad. Es el registro por defecto donde Docker busca imágenes. Aunque se pueden agregar otros a Docker. Docker Hub contiene una gran colección de imágenes preconstruidas creadas por Docker, proyectos de código abierto y otros proveedores de software.

### Notes:

<!-- Slide number: 19 -->
# Docker Ejemplo
$ docker run hello-world
Unable to find image 'hello-world:latest' locally
latest: Pulling from library/hello-world
c1ec31eb5944: Pull complete
Digest: sha256:d211f485f2dd1dee407a80973c8f129f00d54604d2c90732e8e320e5038a0348
Status: Downloaded newer image for hello-world:latest

Hello from Docker!
This message shows that your installation appears to be working correctly.

To generate this message, Docker took the following steps:
 1. The Docker client contacted the Docker daemon.
 2. The Docker daemon pulled the "hello-world" image from the Docker Hub.
    (amd64)
 3. The Docker daemon created a new container from that image which runs the
    executable that produces the output you are currently reading.
 4. The Docker daemon streamed that output to the Docker client, which sent it
    to your terminal.

To try something more ambitious, you can run an Ubuntu container with:
 $ docker run -it ubuntu bash

Share images, automate workflows, and more with a free Docker ID:
 https://hub.docker.com/

For more examples and ideas, visit:
 https://docs.docker.com/get-started/

### Notes:

<!-- Slide number: 20 -->
# Docker Ejemplo
https://docs.docker.com/reference/cli/docker/container/run/
Docker busca primero la imagen en el repositorio local. Si la encuentra la usa. De lo contrario va a buscarla a DockerHub. Si la encuentra la baja, sino aborta con error. Se ve el avance en la descarga.

docker run pepito123
Unable to find image 'pepito123:latest' locally
docker: Error response from daemon: pull access denied for pepito123, repository does not exist or may require 'docker login': denied: requested access to the resource is denied.
See 'docker run --help'.

### Notes:

<!-- Slide number: 21 -->
# Docker Definiciones
Base Image - Imagen base : Imagen minimalista. Un sistema operativo mínimo como Alpine Linux o un entorno de aplicación completo como Python o Node.js.

Parent Image - Imagen padre : La imagen en la que se basa otra imagen. Cada imagen Docker tiene una imagen padre, excepto una imagen creada “from scratch” (desde cero).

Tags - Etiquetas : Las etiquetas se utilizan para versionar imágenes Docker. Por ejemplo, ubuntu:24.04 y ubuntu:20.04 son dos etiquetas, que identifican dos imágenes diferentes de Ubuntu, con versiones diferentes.

Container ID - ID de contenedor : Un identificador único asignado a cada contenedor. Además del nombre tiene el ID.

Volumes - Volúmenes : Es un sistema de archivos separado, que se conecta (monta) a un contenedor. Se usa para la persistencia de los datos de los contenedores Docker, entre otros usos.

Al ejecutar un contenedor, Docker utiliza una imagen como punto de partida. Crea un clon del contenido, a partir del cual la aplicación puede almacenar datos y realizar cambios. Esto hace que el contenedor pueda persistir los cambios a través de sus ejecuciones.

### Notes:

<!-- Slide number: 22 -->
# Docker Ejemplo
Es conveniente que se registren en DockerHub https://hub.docker.com/
Puede ir a Explore y buscar imágenes de su interés.
Por ejemplo tenemos Debian en https://hub.docker.com/_/debian
Allí podemos ver información sobre los principales Tags y ejemplos de cómo descargar una imagen y crear un contenedor.
Por ejemplo podemos ejecutar lo siguiente:

$ docker run --name midebian -it debian:latest /bin/bash
Unable to find image 'debian:latest' locally
latest: Pulling from library/debian
cdd62bf39133: Pull complete
Digest: sha256:27586f4609433f2f49a9157405b473c62c3cb28a581c413393975b4e8496d0ab
Status: Downloaded newer image for debian:latest
root@d92a4a172cdf:/#

### Notes:

<!-- Slide number: 23 -->
# Docker Ejemplo
docker run --name midebian -it debian:latest /bin/bash

La sentencia “docker run” significa crear y  ejecutar un contenedor.  https://docs.docker.com/reference/cli/docker/container/run/

El parámetro “--name midebian” es para darle un nombre al nuevo contenedor creado. No debe existir otro contenedor con el mismo nombre en el sistema. Si no le damos un nombre, docker le asigna uno utilizando combinaciones de adjetivos. Algunos pueden ser bastante curiosos.

El parámetro “-it”  es en realidad dos parámetros -i y -t. El primero es por interactivo, -i, --interactive, para que mantenga conectado al dispositivo STDIN aunque no esté vinculado a una interfaz. El segundo es -t, --tty, y significa que se conecte una pseudo consola (pseudo-TTY) para poder interactuar con las aplicaciones dentro del contenedor. Esto conecta la interfaz de usuario del contenedor a la interfaz de usuario (consola) del host. Por eso es que nos muestra un prompt de consola. Esa es la consola del contenedor. Las operaciones que hagamos allí ocurren dentro del contenedor.

 El parámetro “debian:latest” indica que el contenedor se debe basar en la imagen de la última versión de Debian disponible (bookworm-Debian 12 en este caso).

El parámetro “/bin/bash” indica el comando a ejecutar dentro del contenedor, en este caso es el intérprete de comandos bash.

### Notes:

<!-- Slide number: 24 -->
# Docker Ejemplo
Ahora podemos interactuar con la consola y verificar que estamos dentro de Debian:
root@d92a4a172cdf:/# cat /etc/os-release
PRETTY_NAME="Debian GNU/Linux 12 (bookworm)"
NAME="Debian GNU/Linux"
VERSION_ID="12"
VERSION="12 (bookworm)"
VERSION_CODENAME=bookworm
ID=debian
HOME_URL="https://www.debian.org/"
SUPPORT_URL="https://www.debian.org/support"
BUG_REPORT_URL="https://bugs.debian.org/"

Una de las primeras cosas que conviene hacer es actualizar la base de datos de repositorios y actualizar el software. Recordar que estamos ejecutando todo como root del contenedor.
apt update
apt upgrade

Con “exit” salimos de bash, y del contenedor. Y en este caso este se apaga.
exit

### Notes:

<!-- Slide number: 25 -->
# Otros comandos de Docker
docker version : Muestra la información de la versión de Docker.
docker info : Proporciona información detallada sobre la instalación de Docker.
docker ps : Lista los contenedores en ejecución.
docker images : Lista las imágenes disponibles.
docker pull : Descarga (tira de) una imagen de un repositorio.

docker pull nginx
Descarga la imagen del repositorio (Docker Hub). Pero no crea un contenedor.

### Notes:

<!-- Slide number: 26 -->
# Otros comandos de Docker
Si luego hacemos
docker run -d -p 8080:80 nginx
Entonces crea un contenedor con la imagen ya descargada.

El parámetro “-d, --detach”  indica que se ejecute el contenedor como si fuera un servicio, sin conectarse a la consola, pero imprime el ID del contenedor.

El parámetro “-p 8080:80” indica que se debe exponer el puerto 80 interno al contenedor en el exterior (localhost en este caso) con el puerto 8080.

El parámetro “nginx” indica que se debe basar en la imagen de nginx disponible.

Podemos conectarnos al servidor web con un navegador en http://localhost:8080
Welcome to nginx!
If you see this page, the nginx web server is successfully installed and working.
Further configuration is required.
For online documentation and support please refer to nginx.org.
Commercial support is available at nginx.com.
Thank you for using nginx.

### Notes:

<!-- Slide number: 27 -->
# Lazydocker.
Para aquellos que quieran una interfaz que les facilite el trabajo con docker por consola les sugiero lazydocker.

Es una aplicación para Linux de consola que les permite hacer la mayor parte de las operaciones sobre imágenes, contenedores y volúmenes.

Con Homebrew para MacOS.¡¡Con Scoop también para Güíndous!!

¡Pueden instalar Lazydocker en un contenedor!

https://github.com/jesseduffield/lazydocker

### Notes:

<!-- Slide number: 28 -->
# Lazydocker
LazyDocker es un utilitario de consola que nos facilita la administración de contenedores en Docker.
https://github.com/jesseduffield/lazydocker

Para instalar, ver las alternativas en: https://github.com/jesseduffield/lazydocker#installation

Lazydocker nos permite ejecutar, apagar, borrar contenedores e imágenes entre otras cosas.

Apaguemos y borremos los contenedores hechos hasta ahora.

### Notes:

<!-- Slide number: 29 -->
# Portainer
También podemos administrar nuestros contenedores con Portainer-CE (Hay también una versión enterprise) https://hub.docker.com/r/portainer/portainer-ce

Para ejecutarlo por primera vez sugiero el comando:

docker run -d -p 8000:8000 -p 9443:9443 --name portainer --restart=always -v /var/run/docker.sock:/var/run/docker.sock -v portainer_data:/data portainer/portainer-ce:latest &

/usr/bin/google-chrome-stable https://localhost:9443/#/home &

![](GoogleShape229p41.jpg)
Crea un nuevo usuario administrador y su password. Es recomendable cambiar admin.

### Notes:

<!-- Slide number: 30 -->
# Portainer

![](GoogleShape237p42.jpg)
Seleccionar Get Started para el ambiente local.
Y allí ya se pueden ver los contenedores existentes y su estado, etc.

Para las siguientes ejecuciones el comando es:
docker start portainer -p 9443:9443 &
/usr/bin/google-chrome-stable https://localhost:9443/#/home &

Este podría ser su script:
cat ~/Scripts/portainer.sh
#!/bin/bash
# docker run -d -p 8000:8000 -p 9443:9443 --name portainer --restart=always -v /var/run/docker.sock:/var/run/docker.sock -v portainer_data:/data portainer/portainer-ce:latest &
docker start portainer -p 9443:9443 &
/usr/bin/google-chrome-stable https://localhost:9443/#/home &

### Notes:

<!-- Slide number: 31 -->
# Dockerfile
Un Dockerfile es un archivo de texto que contiene una serie de instrucciones sobre cómo construir una imagen Docker. Especifica la imagen base, los paquetes de software a instalar, los ajustes de configuración y los comandos a ejecutar tanto para configurar la imagen como cuando se inicia el contenedor.

FROM : Especifica la imagen base a utilizar.

WORKDIR : Establece el directorio de trabajo dentro del contenedor donde se ejecutarán las acciones.

RUN : Ejecuta comandos para instalar paquetes de software.

COPY : Copia archivos de la máquina anfitriona a la imagen.

CMD : Especifica el comando a ejecutar cuando se inicia el contenedor.

https://docs.docker.com/get-started/docker-concepts/building-images/writing-a-dockerfile/
https://www.josedomingo.org/pledin/2016/02/ejemplos-de-ficheros-dockerfile-creando-imagenes-docker/

### Notes:

<!-- Slide number: 32 -->
# Dockerfile
mkdir mi-web-server
cd mi-web-server

nano Dockerfile
# Usar la imagen oficial de Python
FROM python:3.9-slim
# Setear el directorio de trabajo dentro del contenedor a /app
WORKDIR /app
# Copiar todo el contenido del directorio local en el host al directorio en la imágen.
COPY . /app
# Instalar Flask, un framework y bibliotecas para hacer aplicación web en Python
RUN pip install Flask
# Definir variables de ambiente para Flask
ENV FLASK_APP=app.py
# Ejecutar la aplicación Flask cuando se inicia el contenedor, en localhost
CMD ["flask", "run", "--host=0.0.0.0"]
Ctrl-o
Ctrl-x

### Notes:

<!-- Slide number: 33 -->
# Dockerfile
Creamos la aplicación (dice “Hello, World!”):
nano app.py
from flask import Flask
app = Flask(__name__)
@app.route('/')
def hello_world():
    return 'Hello, World!'
Ctrl-o
Ctrl-x

Construimos la nueva imágen basada en el Dockerfile recién creado. El punto es importante, indica con los datos que se encuentran en el directorio actual (sino hay que poner un path/camino)
docker build -t mi-web-server .

Y lo ejecutamos con el comando:
docker run -p 5000:5000 mi-web-server

Ahora podemos conectarnos a la aplicación con
http://localhost:5000

podemos verlo con lazydocker y con portainer.

### Notes:

<!-- Slide number: 34 -->
# Recomendaciones Generales
Siempre que sea posible, utilice imágenes de base oficiales. Están bien mantenidas y son seguras. Aunque veremos que hacer las nuestras propias también es posible. Y para producción por seguridad conviene tener muy claro qué hay en nuestras imágenes.

Cada instrucción en un Dockerfile crea una nueva capa. Minimice el número de capas para reducir el tamaño de la imagen.

Elimine archivos y paquetes innecesarios para mantener su imagen segura, pequeña y eficiente.

Cree un archivo .dockerignore para excluir archivos y directorios del contexto de compilación que no sean necesarios en la imagen.

Ejecute las aplicaciones como usuario no root para mejorar la seguridad.

Mantenga actualizadas sus imágenes base y dependencias para incorporar parches de seguridad.

### Notes:

<!-- Slide number: 35 -->
# Dockerfile
Otros comandos de Dockerfile
La instrucción ENV setea las variables de ambiente en el contenedor. Estas son utilizadas por las aplicaciones en la configuración de inicio y en su ejecución.
ENV FLASK_ENV=development

La instrucción EXPOSE documenta los puertos que se expondrán en tiempo de ejecución.
EXPOSE 5000

La instrucción ENTRYPOINT permite configurar un contenedor qué se ejecutará al inicio. Es similar a CMD pero proporciona más flexibilidad como parámetros, etc.
ENTRYPOINT ["python", "app.py"]

La instrucción VOLUME crea un punto de montaje con la ruta especificada y lo marca como contenedor de volúmenes montados externamente desde el host nativo u otros volúmenes.
VOLUMEN /datos

### Notes:

<!-- Slide number: 36 -->
# Docker Network
Network Namespace : El espacio de nombres de red de un contenedor proporciona aislamiento para sus interfaces de red, direcciones IP y tablas de enrutamiento.

Bridge : Un dispositivo de red virtual que conecta contenedores a la misma red y les permite comunicarse entre sí.

Network Driver : Un componente que proporciona la conexión a una red.

Bridge Network (por defecto): Los contenedores en la misma red puente pueden comunicarse entre sí. Es útil para aplicaciones independientes o durante el desarrollo.

Host Network : El contenedor comparte la red del host, eliminando el aislamiento de red entre el contenedor y el host.

None Network : El contenedor no tiene interfaces de red, proporcionando un completo aislamiento de red.

Overlay Network : Se utiliza para redes multi-host, permitiendo que los contenedores que se ejecutan en diferentes hosts Docker se comuniquen entre sí.

Macvlan Network : Asigna una dirección MAC a cada contenedor, haciéndolo aparecer como un dispositivo físico en la red.

### Notes:

<!-- Slide number: 37 -->
# Docker Network
Crear una red tipo Bridge
docker network create mi-bridge-network

Crea contenedores con docker run conectados a una red específica:
docker run -d --name contenedor1 --network mi-bridge-network nginx
docker run -it --name contenedor2 --network mi-bridge-network busybox /bin/sh
/ # ping contenedor1
PING contenedor1 (172.19.0.2): 56 data bytes
64 bytes from 172.19.0.2: seq=0 ttl=64 time=0.316 ms
64 bytes from 172.19.0.2: seq=1 ttl=64 time=0.200 ms
64 bytes from 172.19.0.2: seq=2 ttl=64 time=0.165 ms
64 bytes from 172.19.0.2: seq=3 ttl=64 time=0.183 ms
64 bytes from 172.19.0.2: seq=4 ttl=64 time=0.167 ms
^C
--- contenedor1 ping statistics ---
5 packets transmitted, 5 packets received, 0% packet loss
round-trip min/avg/max = 0.165/0.206/0.316 ms
/ #

### Notes:

<!-- Slide number: 38 -->
# Docker Network
Busybox es un conjunto de reemplazos de utilidades comunes de Linux, pero recortadas a sus operaciones esenciales y sus ejecutables del menor tamaño posible. Estos ejecutables luego se incluyen en imágenes de docker hechas por diferentes proveedores. Docker tiene su imagen oficial.

Una vez que los contenedores están en la misma red, pueden comunicarse entre sí, tanto por dirección IP, como por nombre (DNS).
docker exec -it contenedor2 ping contenedor1

Cuando un contenedor está en la red del host, quiere decir eso mismo. No hay aislación entre el host y el contenedor.
docker run --name nginxenhost -d --network host nginx

El puerto 80 debe estar disponible.

### Notes:

<!-- Slide number: 39 -->
# Docker Network
Los “Overlay Networks” o redes superpuestas se utilizan para conectar contenedores a través de múltiples hosts Docker. Esto permite que las aplicaciones y servicios distribuidos que se ejecutan en contenedores formando un Swarm (enjambre) o en un clúster de Kubernetes, etc.

El modo Swarm de Docker permite la gestión conjunta de contenedores distribuidos.
https://docs.docker.com/engine/swarm/
Esto va un poco más allá de lo que va este curso. Pero con esta funcionalidad se puede gestionar el conjunto de servicios en contenedores para grandes aplicaciones en la nube, incluyendo escalamiento, seguridad, brokers, balanceo de carga, gestión de actualizaciones y rollback. etc.

docker swarm init
Si hay varias direcciones en el host entonces se debe especificar en cual se conecta:
docker swarm init --advertise-addr 192.168.50.17

Creamos la Overlay Network.
docker network create -d overlay mi-overlay-network

Creamos un servicio asociado a la Overlay Network.
docker service create --name mi-servicio --network mi-overlay-network nginx

### Notes:

<!-- Slide number: 40 -->
# Docker Compose
https://docs.docker.com/compose/gettingstarted/
Docker Compose permite definir y gestionar aplicaciones multicontenedor en un único archivo YAML.

Esto simplifica la tarea de orquestar y coordinar varios servicios.

Fácil reproducción. Los archivos de configuración de Docker Compose son fáciles de compartir, y facilita la reproducción del ambiente de producción/desarrollo.

La reutilización de contenedores permite realizar cambios en el entorno con gran rapidez.

Compose admite variables en el archivo que permiten personalizar su composición para distintos entornos o distintos usuarios.

### Notes:

<!-- Slide number: 41 -->
# Docker Compose
docker network create mi_network

nano docker-compose.yml
version: '3'
services:
 web:
   image: nginx
   ports:
     - "8080:80"
   networks:
     - mi_network
 db:
   image: postgres
   environment:
     POSTGRES_USER: admin
     POSTGRES_PASSWORD: admin
   networks:
     - mi_network
networks:
 mi_network:

Ctrl-o
Ctrl-x

docker-compose up
https://docs.docker.com/compose/gettingstarted/
Para desarrollo y pequeñas aplicaciones docker compose es más amigable que Docker Swarm.

Esta aplicación incluye una página web y una base de datos PostgreSQL. Ambas están conectadas a través de mi_network.

Pero no hay verdadera conexión porque no se incluye la aplicación que efectivamente interactúe entre ambas.
https://docs.docker.com/compose/gettingstarted/

Crea los dos contenedores en un servicio y los conecta por red virtual.

### Notes:

<!-- Slide number: 42 -->
# Otro ejemplo
# version is now using "compose spec"
# v2 and v3 are now combined!
# docker-compose v1.27+ required

services:
  vote:
    build:
      context: ./vote
      target: dev
    depends_on:
      redis:
        condition: service_healthy
    healthcheck:
      test: ["CMD", "curl", "-f", "http://localhost"]
      interval: 15s
      timeout: 5s
      retries: 3
      start_period: 10s
    volumes:
     - ./vote:/usr/local/app
    ports:
      - "8080:80"
    networks:
      - front-tier
      - back-tier
  result:
    build: ./result
    # use nodemon rather than node for local dev
    entrypoint: nodemon --inspect=0.0.0.0 server.js
    depends_on:
      db:
        condition: service_healthy
    volumes:
      - ./result:/usr/local/app
    ports:
      - "8081:80"
      - "127.0.0.1:9229:9229"
    networks:
      - front-tier
      - back-tier

  worker:
    build:
      context: ./worker
    depends_on:
      redis:
        condition: service_healthy
      db:
        condition: service_healthy
    networks:
      - back-tier
https://github.com/dockersamples/example-voting-app
gh repo clone dockersamples/example-voting-app
docker compose --profile seed up -d
8080

front-tier
8080
8081
voting-app
Python
result-app
Node.js
9229
back-tier
6379
5432
redis
Redis db
worker
.NET
db
PostgreSQL

### Notes:

<!-- Slide number: 43 -->
# Otro ejemplo

  redis:
    image: redis:alpine
    volumes:
      - "./healthchecks:/healthchecks"
    healthcheck:
      test: /healthchecks/redis.sh
      interval: "5s"
    networks:
      - back-tier

  db:
    image: postgres:15-alpine
    environment:
      POSTGRES_USER: "postgres"
      POSTGRES_PASSWORD: "postgres"
    volumes:
      - "db-data:/var/lib/postgresql/data"
      - "./healthchecks:/healthchecks"
    healthcheck:
      test: /healthchecks/postgres.sh
      interval: "5s"
    networks:
      - back-tier
  # this service runs once to seed the database with votes
  # it won't run unless you specify the "seed" profile
  # docker compose --profile seed up -d
  seed:
    build: ./seed-data
    profiles: ["seed"]
    depends_on:
      vote:
        condition: service_healthy
    networks:
      - front-tier
    restart: "no"

volumes:
  db-data:

networks:
  front-tier:
  back-tier:

https://github.com/dockersamples/example-voting-app
gh repo clone dockersamples/example-voting-app
docker compose --profile seed up -d
8080

front-tier
8080
8081
voting-app
Python
result-app
Node.js
9229
back-tier
6379
5432
redis
Redis db
worker
.NET
db
PostgreSQL

### Notes:

<!-- Slide number: 44 -->
# Docker Volumes
docker volume create mi_volumen

docker volume inspect mi_volumen
[
    {
        "CreatedAt": "2024-10-14T10:16:26-03:00",
        "Driver": "local",
        "Labels": null,
        "Mountpoint": "/var/snap/docker/common/var-lib-docker/volumes/mi_volumen/_data",
        "Name": "mi_volumen",
        "Options": null,
        "Scope": "local"
    }
]

$ docker volume inspect portainer_data
[
    {
        "CreatedAt": "2024-10-14T00:06:40-03:00",
        "Driver": "local",
        "Labels": null,
        "Mountpoint": "/var/snap/docker/common/var-lib-docker/volumes/portainer_data/_data",
        "Name": "portainer_data",
        "Options": null,
        "Scope": "local"
    }
]
Los volúmenes Docker son mecanismos de almacenamiento diseñados para mantener los datos fuera del ciclo de vida del contenedor. Se almacenan en el sistema de archivos del host y son gestionados por Docker, lo que garantiza que los datos permanezcan intactos aunque el contenedor se detenga, se reinicie o se elimine.
Esto nos permite por ejemplo, hacer un upgrade de todo el contenedor a una nueva versión de la aplicación y servicios, cambiando totalmente el contenedor, pero mantener los datos intactos.

Volumes : Gestionados por Docker y almacenados en una parte del sistema de archivos del host que Docker controla. Son el método preferido para la persistencia de datos.

Bind Mounts : Montar directamente un directorio o archivo desde el sistema de archivos host a un contenedor. Proporcionan flexibilidad pero requieren una gestión más cuidadosa. Hay algunos aspectos de seguridad que pueden ser relevantes.

tmpfs Mounts : Almacena datos en la memoria del sistema anfitrión, lo que resulta útil para datos temporales que no deben persistir.

### Notes:

<!-- Slide number: 45 -->
# Docker Volumes

Pueden ser usados para compartir datos entre varios contenedores.
docker run -d -v mi_volumen:/data contenedor1
docker run -d -v mi_volumen:/data contenedor2

docker run -it --name miubuntu -v mi_volumen:/data ubuntu:latest /bin/bash

Antes que nada actualizar
root@2073d65de3a9:/# apt update
root@2073d65de3a9:/# apt upgrade
root@2073d65de3a9:/# cd /data
root@2073d65de3a9:/data# ls
root@2073d65de3a9:/data# apt install nano -y
root@2073d65de3a9:/data# nano prueba.txt
Esta es una prueba.
Esta es una prueba.
Esta es una prueba.
Esta es una prueba.
Ctrl-o
Ctrl-x
root@2073d65de3a9:/data# ls
prueba.txt
root@2073d65de3a9:/data# cat prueba.txt
Esta es una prueba.
Esta es una prueba.
Esta es una prueba.
Esta es una prueba.
root@2073d65de3a9:/data#

### Notes:

<!-- Slide number: 46 -->
# Docker Volumes
nano docker-compose.yml
version: '3'
services:
  web:
    image: nginx
    ports:
      - "8080:80"
    volumes:
      - mi_volumen:/usr/share/nginx/html
  db:
    image: postgres
    environment:
       POSTGRES_USER: example
       POSTGRES_PASSWORD: example
    volumes:
       - mi_data:/var/lib/postgresql/data
volumes:
  mi_volumen:
  mi_data:
Ctrl-o
Ctrl-x

docker-compose up
Bind Mounts (Host)
docker run -ti --name ubuntu -v C:\Users\MiUsuario\MisDocumentos:/home/alumno/intercambio ubuntu:latest /bin/bash

tmpfs Mounts (Memoria)
docker run -d --tmpfs /tmp my-image

docker volume create mi_volumen
docker volume create mi_data

### Notes:

<!-- Slide number: 47 -->
# Docker Volumes Backup - contenedor efímero - un uso
Este script ejecuta un contenedor temporal --rm de busybox y monta el volumen indicado por <volume> en /data dentro del contenedor y monta el directorio actual $(pwd) en el directorio /backup del contenedor, y luego ejecuta el respaldo con tar creando en el directorio actual del host el archivo backup.tar. Al terminar el contenedor es borrado del sistema.

docker run --rm -v <volume>:/data -v $(pwd):/backup busybox tar cvf /backup/backup.tar /data

Revisar el espacio utilizado por los volúmenes:
$ docker system df
TYPE            TOTAL     ACTIVE    SIZE      RECLAIMABLE
Images          6         5         1.051GB   79.05MB (7%)
Containers      5         3         63.93MB   63.93MB (99%)
Local Volumes   5         4         47.94MB   0B (0%)
Build Cache     0         0         0B        0B

### Notes:

<!-- Slide number: 48 -->
# Crear una aplicación en Docker 1

https://www.ninjaone.com/blog/how-to-containerize-an-application-using-docker/

https://docs.docker.com/get-started/02_our_app/

https://docs.docker.com/guides/walkthroughs/containerize-your-app/

https://learn.microsoft.com/en-us/dotnet/core/docker/build-container

https://docs.docker.com/guides/walkthroughs/run-a-container/

https://www.docker.com/blog/docker-init-initialize-dockerfiles-and-compose-files-with-a-single-cli-command/ docker init

### Notes:

<!-- Slide number: 49 -->
# Crear una aplicación en Docker 2
https://docs.docker.com/get-started/02_our_app/
cd Documents/
mkdir docker_app
cd docker_app/
git clone https://github.com/docker/getting-started-app.git

...docker_app/
    └─ getting-started-app/
         ├── package.json
         ├── README.md
         ├── spec/
         ├── src/
         └── yarn.lock

cd getting-started-app

### Notes:

<!-- Slide number: 50 -->
# Crear una aplicación en Docker 3
nano Dockerfile
	# syntax=docker/dockerfile:1

FROM node:18-alpine	<= Usamos image con Linux Alpine 18 y nodejs
WORKDIR /app		<= Creamos el directorio /app en el contenedor y cambiamos a él
COPY . .		<= Copiamos la aplicación desde el directorio actual (primer punto)
al directorio dentro que acabamos de crear y movernos (/app - segundo punto)
RUN yarn install --production	<= Instalamos las bibliotecas/dependencias de producción

CMD ["node", "src/index.js"]	<= Al iniciar el contenedor invocamos
a node y le pasamos nuestra aplicación
EXPOSE 3000	<= Exponemos el puerto 3000 en localhost para interactuar con la aplicación

Ctrl-O
Ctrl-X

ls
Dockerfile  package.json  README.md  spec  src  yarn.lock

Sobre dockerfile:
https://thenewstack.io/docker-basics-how-to-use-dockerfiles/
https://docs.docker.com/develop/develop-images/dockerfile_best-practices/

### Notes:
# syntax=docker/dockerfile:1
FROM node:18-alpine
WORKDIR /app
COPY . .
RUN yarn install --production
CMD ["node", "src/index.js"]
EXPOSE 3000

<!-- Slide number: 51 -->
# Crear una aplicación en Docker 4
docker build -t getting-started .
# -t –tag para darle un nombre/etiqueta por ejemplo enrique/getting-started
# Construimos la imágen getting-started

docker run --name mi_getting_started -d --publish 127.0.0.1:3000:3000 getting-started
# -p, --publish publicar puertos TCP;
# -d, --detach , correr el contenedor desconectado de la consola.

firefox/chrome http://locahost:3000/

#encender
docker start mi_getting_started

#apagar
docker stop mi_getting_started

#entrar en shell para debugging
docker exec -it mi_getting_started sh
# algunas veces puede ser necesario dar todo el camino al shell e.g. /bin/bash

#Iniciar y entrar en shell para debugging
docker run --entrypoint /bin/sh -it getting_started

Otros ejemplos para probar https://docs.google.com/document/d/10vH3UDXT5tI-mh5Ng1owJbrQOSjwwanaxAMUkg3TKqA/edit
https://docs.google.com/document/d/1-nb7eSe__vCgB5yVRe3xNTdZhJNjZKjz8vxgqA_DbeA/edit

### Notes:
# syntax=docker/dockerfile:1

FROM node:18-alpine
WORKDIR /app
COPY . .
RUN yarn install --production
CMD ["node", "src/index.js"]
EXPOSE 3000

<!-- Slide number: 52 -->
# Docker Contenedores
Crear una imagen a partir de un contenedor.

https://thenewstack.io/tutorial-create-a-docker-image-from-a-running-container/

https://kodekloud.com/blog/docker-create-image-from-container/

https://www.dataset.com/blog/create-docker-image/

### Notes:

<!-- Slide number: 53 -->
# Crear una imagen a partir de un contenedor 1.
Vamos a buscar el index.html y le modificamos el título en el head
<title>La Aplicacion de Enrique</title>
Y agregamos un Heading en el body
<h1>La Aplicacion de Enrique</h1>
nano getting-started-app/src/static/index.html

docker cp getting-started-app/src/static/index.html mi_getting_started:/app/src/static/index.html

Hay que copiarlo a /app/src/static/ dentro del contenedor
docker cp index.html mi_getting-started:/app/src/static/index.html

Confirmamos los cambio en el contenedor.
docker commit mi_getting_started

docker start mi_getting_started
firefox http://localhost:3000

![](GoogleShape426p65.jpg)

### Notes:

<!-- Slide number: 54 -->
# Crear una imagen a partir de un contenedor 2.
Confirmamos los cambios en el contenedor. Esto crea una imagen a partir del contenedor.
docker commit mi_getting_started

Verificamos la creación de una imágen sin nombre:
docker images
REPOSITORY                                             TAG               IMAGE ID       CREATED              SIZE
<none>                                                 <none>            f95f66e02c78   About a minute ago   223MB

Y le ponemos un nombre:
docker tag f95f66e02c78 getting-started-2

docker images

Si confirmamos los cambios en el contenedor y le pasamos un nombre ese nombre será el de la nueva imágen.
docker commit mi_getting_started  mi_otra_getting-started-2

### Notes:

<!-- Slide number: 55 -->
# Crear una imagen a partir de un contenedor 3.
docker ps -a
docker ps
docker stop mi_getting_started
docker rm mi_getting_started

docker run --name mi_getting_started -d --publish 127.0.0.1:3000:3000 getting-started-2

firefox http://locahost:3000/

### Notes:

<!-- Slide number: 56 -->
# Docker Contenedores
Crear una imagen desde cero en Docker

https://tbhaxor.com/create-docker-base-image/

https://docs.docker.com/build/building/base-images/

https://kodekloud.com/blog/how-to-build-a-docker-image/

https://docs.docker.com/develop/develop-images/dockerfile_best-practices/

### Notes:

<!-- Slide number: 57 -->
# Crear una imagen desde cero en Docker 1.
debootstrap p.15

sudo chroot noble-root /bin/bash
 lsb_release -a
No LSB modules are available.
Distributor ID:	Ubuntu
Description:	Ubuntu 24.04 LTS
Release:	24.04
Codename:	noble
 exit
exit
sudo chroot sid-root /bin/bash
 lsb_release -a
No LSB modules are available.
Distributor ID:	Debian
Description:	Debian GNU/Linux trixie/sid
Release:	n/a
Codename:	trixie
 exit
exit
sudo chroot jammy-root /bin/bash
 lsb_release -a
bash: lsb_release: command not found
 cat /etc/lsb-release
DISTRIB_ID=Ubuntu
DISTRIB_RELEASE=22.04
DISTRIB_CODENAME=jammy
DISTRIB_DESCRIPTION="Ubuntu 22.04 LTS"
 exit

### Notes:
mkdir miubuntu
sudo debootstrap jammy miubuntu
sudo fakechroot fakeroot chroot miubuntu /bin/bash
apt install -y nginx
exit
sudo cp ~/Documents/index.html ~/miubuntu/var/www/html
sudo cp ~/Documents/yo.png ~/miubuntu/var/www/html
sudo tar -C miubuntu -c . | docker import - miubuntudocker images
docker run --name mi_ubuntu -t -d -p 127.0.0.1:8000:80 -v /home/enrique/Downloads:/home/shared  miubuntu /etc/init.d/nginx start
docker ps -a
firefox http://127.0.0.1:8000

nginx -g 'daemon=off'

<!-- Slide number: 58 -->
# Crear una imagen desde cero en Docker 1.
 sudo tar -C noble-root -c . | docker import - minoble
	sha256:97adae07f859172c22bcf6d5c7ef8e0bdf3ca5562b332726a656ada1b1c2fd0c
docker run --rm -ti minoble /usr/bin/bash
root@6cdc1ded89f1:/# lsb_release -a
	No LSB modules are available.
	Distributor ID:	Ubuntu
	Description:	Ubuntu 24.04 LTS
	Release:	24.04
	Codename:	noble
root@6cdc1ded89f1:/# exit

sudo tar -C sid-root -c . | docker import - misid
sha256:c9041296bade7be3915d856d4d2627b233946569daf0b92b5f555ab3b36e5cce
alumno@debian-infraestructura:~/Documents$ docker run --rm -ti misid /usr/bin/bash
root@4f78e26396d1:/# whoami
root
root@4f78e26396d1:/# ps
    PID TTY          TIME CMD
      1 pts/0    00:00:00 bash
      9 pts/0    00:00:00 ps
root@4f78e26396d1:/# lsb_release -a
No LSB modules are available.
Distributor ID:	Debian
Description:	Debian GNU/Linux trixie/sid
Release:	n/a
Codename:	trixie
root@4f78e26396d1:/# exit
sudo tar -C jammy-root -c . | docker import - mijammy
sha256:6b22d30fc1f53049dba96cb25e003b41947acb5b374a8defbd1101376fcf5b2f
alumno@debian-infraestructura:~/Documents$ docker run --rm -ti mijammy /usr/bin/bash
root@d4982600d4c2:/# whoami
root
root@d4982600d4c2:/# ps
    PID TTY          TIME CMD
      1 pts/0    00:00:00 bash
     10 pts/0    00:00:00 ps
root@d4982600d4c2:/# lsb_release
bash: lsb_release: command not found
root@d4982600d4c2:/# cat /etc/lsb-release
DISTRIB_ID=Ubuntu
DISTRIB_RELEASE=22.04
DISTRIB_CODENAME=jammy
DISTRIB_DESCRIPTION="Ubuntu 22.04 LTS"
root@d4982600d4c2:/# exit

### Notes:
mkdir miubuntu
sudo debootstrap jammy miubuntu
sudo fakechroot fakeroot chroot miubuntu /bin/bash
apt install -y nginx
exit
sudo cp ~/Documents/index.html ~/miubuntu/var/www/html
sudo cp ~/Documents/yo.png ~/miubuntu/var/www/html
sudo tar -C miubuntu -c . | docker import - miubuntudocker images
docker run --name mi_ubuntu -t -d -p 127.0.0.1:8000:80 -v /home/enrique/Downloads:/home/shared  miubuntu /etc/init.d/nginx start
docker ps -a
firefox http://127.0.0.1:8000

nginx -g 'daemon=off'

<!-- Slide number: 59 -->
#

### Notes:

<!-- Slide number: 60 -->
# Crear una imagen desde cero en Docker 1.
lsb_release -c
Codename:	jammy

sudo apt install debootstrap

mkdir miimagen

sudo debootstrap jammy miimagen

fakechroot fakeroot chroot miimagen bash 	lsb_release -c
	exit

ls -l miimagen/

sudo tar -C miimagen -c . | docker import - miimagen
sha256:973d68a23fa671c976c252598a2469b67f93217bed70c69c2710db274b213be6

docker images
REPOSITORY                                             TAG               IMAGE ID       CREATED              SIZE
miimagen                                               latest            973d68a23fa6   About a minute ago   374MB

docker run --name mi_ubuntu_jammy miimagen cat /etc/lsb-release
DISTRIB_ID=Ubuntu
DISTRIB_RELEASE=22.04
DISTRIB_CODENAME=jammy
DISTRIB_DESCRIPTION="Ubuntu 22.04 LTS"

### Notes:
mkdir miubuntu
sudo debootstrap jammy miubuntu
sudo fakechroot fakeroot chroot miubuntu /bin/bash
apt install -y nginx
exit
sudo cp ~/Documents/index.html ~/miubuntu/var/www/html
sudo cp ~/Documents/yo.png ~/miubuntu/var/www/html
sudo tar -C miubuntu -c . | docker import - miubuntudocker images
docker run --name mi_ubuntu -t -d -p 127.0.0.1:8000:80 -v /home/enrique/Downloads:/home/shared  miubuntu /etc/init.d/nginx start
docker ps -a
firefox http://127.0.0.1:8000

nginx -g 'daemon=off'

<!-- Slide number: 61 -->
# Crear una imagen desde cero en Docker 2.
El contenedor que hicimos antes puede que no lo puedan levantar interactivo. Entonces lo borramos y lo volvemos a crear con el siguiente comando:

docker stop mi_getting_started
docker rm mi_getting_started

docker run -ti --name mi_ubuntu_jammy miimagen /bin/bash
root@771e033b9f41:/#
whoami
apt update
apt upgrade
apt install . . .
exit

docker stop mi_getting_started
docker start mi_getting_started

docker exec -it mi_ubuntu_jammy bash
root@771e033b9f41:/#

### Notes:

<!-- Slide number: 62 -->
# Docker from scratch
mkdir scratch
cd scratch
nano static-example.cpp
#include <iostream>
#include <filesystem>
// g++ -std=c++17 -o static-example static-example.cpp --static

int main() {
    // Print "Hello, World!"
    std::cout << "Hello, World!" << std::endl;

    // Specify the directory to list (you can change this to your desired path)
    std::string directory = "/";

    // Use std::filesystem to list directory contents
    std::cout << "Contents of " << directory << ":" << std::endl;

    try {
        for (const auto& entry : std::filesystem::directory_iterator(directory)) {
            std::cout << entry.path().string() << std::endl;
        }
    } catch (const std::filesystem::filesystem_error& err) {
        std::cerr << "Error accessing directory: " << err.what() << std::endl;
    }

    return 0;
}
Ctrl-o
Ctrl-x

g++ -std=c++17 -o static-example static-example.cpp --static

./static-example
Docker tiene una forma absolutamente minimalista para crear contenedores.
Y es un contenedor sin contenido!
En el podemos poner solamente la aplicación que queremos correr, compilándola como static (GNU) o como Single File Deployment.

### Notes:

<!-- Slide number: 63 -->
# Docker from scratch
nano Dockerfile
FROM scratch
WORKDIR /app
COPY static-example .
ENTRYPOINT [ "/app/static-example" ]
Ctrl-o
Ctrl-x

docker build -t static-example .
docker run --name example -it static-example
Hello, World!
Contents of /:
/app
/proc
/etc
/dev
/.dockerenv
/sys

Docker tiene una forma absolutamente minimalista para crear contenedores.
Y es un contenedor sin contenido!
En el podemos poner solamente la aplicación que queremos correr, compilándola como static (GNU) o como Single File Deployment.

### Notes:

<!-- Slide number: 64 -->
# Publicar una imágen
Etiquetado/Tag de imágenes: [HOST[:PORT_NUMBER]/]PATH[:TAG]

Antes que nada registrarse en DockerHub.
PATH es del tipo: [NAMESPACE/]REPOSITORY
donde NAMESPACE es el usuario o la organización y
REPOSITORY hace referencia al producto
TAG generalmente es la versión.

# crear una imagen con una etiqueta
docker build -t my-username/my-image .
# cambiarle la etiqueta por otra
docker image tag my-username/my-image another-username/another-image:v1
# publicar la imagen
docker login
	USING WEB-BASED LOGIN ...
docker push my-username/my-image

Ver: https://docs.docker.com/get-started/docker-concepts/building-images/build-tag-and-publish-an-image/

### Notes:

<!-- Slide number: 65 -->
# Publicar una imágen
Agregar
Otros
Ver: https://docs.docker.com/get-started/docker-concepts/building-images/build-tag-and-publish-an-image/

Docker

### Notes:

<!-- Slide number: 66 -->
# Comandos de Docker.
docker images

docker rm

docker rmi

docker ps

docker run

docker start -i ubuntu

docker container ls

docker container ls -a

### Notes:

<!-- Slide number: 67 -->
# Referencias de Docker.
Buscar otros útiles en la documentación.
https://docs.docker.com/reference/cli/docker/
https://docs.docker.com/engine/reference/commandline/cli/
https://www.mygreatlearning.com/blog/top-essential-docker-commands/

Algunos ejemplos simples https://drive.google.com/drive/folders/1kRQqzuUTD0tuePdtTG1yIkwV5H2YLkf8
Bases de datos https://drive.google.com/drive/folders/0B6Z3Lx2Bss3Bfm0tRTRNWlkyam5UMDkwTUJKLWFxbDBTZW9GZ0l0c2hibkMzQlk0c1ZVLVE?resourcekey=0-yGnPJHvQlJt1tIdpL2HSNg

### Notes:

<!-- Slide number: 68 -->
#

### Notes: