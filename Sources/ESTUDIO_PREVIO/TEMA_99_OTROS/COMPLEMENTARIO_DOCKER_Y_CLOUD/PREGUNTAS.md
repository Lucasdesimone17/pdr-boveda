# Preguntas complementarias — Docker y Cloud (PDR)

Fuentes: 🟢 `Docker.pdf` + `06 - Docker (Renzo Chiarino).pdf` (oficiales). Intentá responder antes de abrir la pista.

## Qué es Docker

**1.** Explicá con tus palabras el chiste/meme oficial: "En mi computadora sí funciona / Sí, pero no le vamos a dar tu computadora al cliente". ¿Qué problema real resuelve Docker?

<details><summary>Pista</summary>Pensá en diferencias de entorno entre la máquina de un desarrollador y el servidor de producción.</details>

**2.** Según la definición informal de la fuente, ¿qué es Docker "en otras palabras"?

<details><summary>Pista</summary>Un proceso que corre en el sistema operativo host.</details>

**3.** Nombrá los 4 beneficios principales de Docker según `Docker.pdf`.

<details><summary>Pista</summary>Eficiencia, portabilidad, escalabilidad, seguridad.</details>

## Imágenes y contenedores

**4.** ¿Cuál es la diferencia entre una imagen y un contenedor?

<details><summary>Pista</summary>Una es una plantilla; el otro es una instancia en ejecución de esa plantilla.</details>

**5.** ¿Qué es Docker Hub?

<details><summary>Pista</summary>Una biblioteca pública, tipo repositorio, de imágenes.</details>

## Docker vs. máquinas virtuales

**6.** Según el diagrama oficial, ¿qué reemplaza al Hypervisor en la arquitectura Docker, y qué NO tiene cada contenedor que sí tiene cada VM?

<details><summary>Pista</summary>Docker Engine; cada VM tiene su propio Guest OS, cada contenedor no.</details>

**7.** ¿Por qué los contenedores son más livianos que las máquinas virtuales?

<details><summary>Pista</summary>Comparten el kernel del host en vez de simular todo el hardware/software.</details>

**8.** Nombrá 3 de los 4 escenarios en los que la fuente recomienda usar Docker.

<details><summary>Pista</summary>Desplegar rápido, desarrollar/probar iterativamente, microservicios, escalar horizontal.</details>

## Comandos y Dockerfile

**9.** ¿Qué hace `docker build -t mi-imagen .` y qué representa el `.` al final?

<details><summary>Pista</summary>El punto indica dónde buscar el Dockerfile — la carpeta actual.</details>

**10.** Nombrá los 5 componentes principales de un Dockerfile según `Docker.pdf` y qué hace cada uno.

<details><summary>Pista</summary>FROM, RUN, COPY, WORKDIR, CMD.</details>

**11.** ¿Cuál es la diferencia entre `CMD` y `ENTRYPOINT`? ¿Qué pasa si el usuario pasa parámetros extra en cada caso?

<details><summary>Pista</summary>Uno se reemplaza fácilmente desde la consola; el otro necesita un flag especial y los parámetros se suman en vez de reemplazar.</details>

**12.** En el ejemplo `ENTRYPOINT ["sleep"]` + `CMD 5`, ¿qué pasa si ejecuto `docker run mi-imagen` sin parámetros? ¿Y si ejecuto `docker run mi-imagen 15`?

<details><summary>Pista</summary>Sin parámetros usa el CMD por defecto; con parámetro, ese valor reemplaza al CMD.</details>

## Volúmenes y networking

**13.** ¿Cuál es la diferencia entre un Bind Mount y un Volume en términos de portabilidad?

<details><summary>Pista</summary>Uno depende del sistema de archivos del host; el otro lo administra Docker y es independiente del host.</details>

**14.** ¿Por qué un Volume "no se destruye automáticamente" cuando se elimina un contenedor, y por qué es útil eso?

<details><summary>Pista</summary>Pensá en datos de una base de datos que necesitás conservar aunque reinicies o borres el contenedor.</details>

**15.** Por defecto, ¿un contenedor Docker expone algún puerto? ¿Cómo se indica que sí?

<details><summary>Pista</summary>No, por defecto no expone nada; se usa la flag `-p`.</details>

## Docker Compose

**16.** ¿Cuál es la diferencia principal entre un Dockerfile y un archivo `docker-compose.yml`?

<details><summary>Pista</summary>Uno construye una sola imagen; el otro coordina múltiples contenedores/servicios.</details>

**17.** En el ejemplo oficial de `docker-compose.yml` con los servicios `web` y `redis`, ¿por qué `web` usa `build: .` y `redis` usa `image: "redis:alpine"`?

<details><summary>Pista</summary>Uno necesita construirse desde un Dockerfile propio; el otro ya existe como imagen pública en Docker Hub.</details>

## Pendientes del tema

**18.** ¿Qué parte del programa oficial de la Unidad 8 ("Arquitecturas Cloud") no está cubierta por ninguna de las fuentes disponibles?

<details><summary>Pista</summary>Pensá en la diferencia entre "usar Docker en una máquina" y "arquitecturas cloud / sistemas operativos cloud" en el sentido de AWS/Azure/GCP.</details>

**19.** En un Dockerfile multi-stage de .NET, ¿por qué se usa una imagen `sdk` en la etapa `build` y una imagen `runtime` en la etapa final?

**20.** Compará redes `bridge`, `host`, `none` y `overlay`. ¿Cuál elegirías para dos contenedores de la misma aplicación en un único host?

**21.** ¿Qué diferencia práctica hay entre un `Volume` y un montaje `tmpfs`?

**22.** En Compose, ¿por qué `depends_on` no garantiza por sí solo que una base de datos esté lista para aceptar conexiones?

---

## Respuestas completas

<details>
<summary>Ver todas las respuestas</summary>

**1.** El meme muestra a un desarrollador defendiendo que su código "funciona en mi computadora", y a otra persona respondiéndole que eso no sirve porque el cliente no va a usar esa misma computadora. El problema real que resuelve Docker es la **inconsistencia entre entornos**: una aplicación puede comportarse distinto en la máquina del desarrollador, en un servidor de testing y en el servidor de producción final, por diferencias de sistema operativo, versiones de librerías, configuración, etc. Docker empaqueta la aplicación junto con todas sus dependencias dentro de un contenedor, garantizando que se ejecute de la misma forma sin importar en qué máquina corra ese contenedor.

**2.** Según la definición informal de la fuente, Docker "en otras palabras" no es más que un **proceso que corre en nuestro sistema operativo** (el host). Ese proceso nos ayuda a ejecutar contenedores dentro del mismo sistema, y es dentro de esos contenedores donde los desarrolladores trabajan y crean sus propias aplicaciones.

**3.** Eficiencia en el desarrollo (código consistente en cualquier lugar), Portabilidad (correr en distintos entornos sin modificaciones), Escalabilidad (replicar contenedores fácilmente para gestionar tráfico creciente) y Seguridad (aislar aplicaciones en contenedores independientes, evitando conflictos entre ellas).

**4.** Una **imagen** es una plantilla estática: contiene el código, las dependencias, las bibliotecas y la configuración necesaria para ejecutar una aplicación, y se almacena en repositorios como Docker Hub. Un **contenedor**, en cambio, es una **instancia en ejecución** de esa imagen: un entorno aislado y activo que corre con todo lo que la imagen definió. De una misma imagen se pueden levantar múltiples contenedores distintos, todos corriendo simultáneamente a partir de la misma plantilla.

**5.** Docker Hub (`hub.docker.com`) es una **biblioteca pública de imágenes** que cualquiera puede usar para sus propios contenedores. También permite que los usuarios suban y compartan sus propias imágenes con la comunidad.

**6.** En la arquitectura Docker, el **Docker Engine** ocupa el lugar que el **Hypervisor** ocupa en la arquitectura de máquinas virtuales. Cada contenedor **no tiene** su propio "Guest OS" (sistema operativo invitado) como sí lo tiene cada máquina virtual — los contenedores comparten directamente el sistema operativo del host a través del Docker Engine, mientras que cada VM corre un sistema operativo completo propio por encima del Hypervisor.

**7.** Porque los contenedores **comparten el kernel** del sistema operativo host, que es lo que les permite acceder al hardware — no necesitan simular ni levantar un sistema operativo completo propio como sí hace cada máquina virtual. Las VM, al simular todo el equipo (hardware y software) de forma independiente para cada una, requieren muchos más recursos y son considerablemente más pesadas que los contenedores, que solo aíslan el proceso de la aplicación.

**8.** Cualquier 3 de estos 4: necesitar desplegar software de forma rápida; desarrollar y probar de forma iterativa; manejar una arquitectura de microservicios (donde cada componente es independiente del resto); o necesitar escalar horizontalmente, algo que los contenedores permiten hacer de forma muy simple (replicándolos).

**9.** `docker build -t mi-imagen .` construye (build) una nueva imagen Docker, asignándole el nombre/tag "mi-imagen" (`-t`), usando como fuente el Dockerfile ubicado en la ruta indicada al final del comando. El `.` representa el directorio actual: le está diciendo a Docker que busque el archivo Dockerfile en la misma carpeta desde la que se está ejecutando el comando. Si el Dockerfile estuviera en otra ubicación, ese `.` se reemplazaría por la ruta completa hacia ese archivo.

**10.** `FROM`: define la imagen base, el punto de partida para construir la nueva imagen. `RUN`: ejecuta comandos para instalar dependencias, configurar el entorno o realizar tareas dentro de la imagen mientras se construye. `COPY`: copia archivos desde el sistema de archivos local (el código fuente, configuración, etc.) hacia el contenedor. `WORKDIR`: establece el directorio de trabajo dentro del contenedor, donde se van a ejecutar los siguientes comandos. `CMD`: define el comando que se va a ejecutar automáticamente cuando se inicia un contenedor a partir de esa imagen (por ejemplo, arrancar un servidor web o correr la aplicación).

**11.** `CMD` especifica el comando por defecto que se ejecuta al iniciar el contenedor, pero se puede sobreescribir fácilmente desde la línea de comandos al hacer `docker run` (por ejemplo, pasando `/bin/bash` en vez del comando original, lo cual haría que se abra una consola en lugar de ejecutar la aplicación). `ENTRYPOINT`, en cambio, especifica las instrucciones fijas por defecto del contenedor, que **no** se pueden cambiar simplemente pasando otro comando al `docker run` — haría falta usar explícitamente el flag `--entrypoint` para reemplazarlo. Si el usuario agrega parámetros extra al ejecutar un contenedor que usa `ENTRYPOINT`, esos parámetros **se suman** a las instrucciones ya definidas en el ENTRYPOINT (no las reemplazan), lo que permite "parametrizar" la ejecución del contenedor sin tener que recompilar la imagen.

**12.** Si ejecuto `docker run mi-imagen` sin pasar ningún parámetro extra, Docker usa el valor definido en `CMD` como parámetro por defecto para el `ENTRYPOINT` — en este caso, el contenedor ejecuta `sleep 5`, es decir, duerme 5 segundos. Si en cambio ejecuto `docker run mi-imagen 15`, ese `15` se pasa como parámetro adicional al `ENTRYPOINT` (`sleep`), y como los parámetros pasados por el usuario se suman/reemplazan el valor de CMD en este contexto, el contenedor termina ejecutando `sleep 15`, durmiendo 15 segundos en lugar de los 5 por defecto.

**13.** Un Bind Mount monta directamente un directorio específico del sistema de archivos del **host** dentro del contenedor — esto lo hace muy dependiente de la estructura de archivos y del sistema operativo de esa máquina host en particular, porque la ruta que se monta tiene que existir exactamente ahí. Un Volume, en cambio, es administrado completamente por **Docker** (no por el sistema operativo del host directamente), lo que lo desvincula por completo de la dependencia hacia el sistema operativo del equipo host — se puede mover o compartir entre distintos contenedores y entornos de forma mucho más portable, sin depender de una ruta específica de un host en particular.

**14.** Porque los Volumes son gestionados de forma independiente por Docker, separados del ciclo de vida de cualquier contenedor individual — cuando un contenedor se elimina o se detiene, el Volume que estaba usando sigue existiendo intacto, con todos sus datos, listo para ser reutilizado por ese mismo contenedor si se vuelve a crear, o por otro contenedor distinto. Esto es fundamental para casos como bases de datos: si los datos vivieran solo dentro del contenedor y este se eliminara (por ejemplo, al actualizar la imagen a una versión nueva), se perderían todos los datos guardados; con un Volume, esos datos persisten más allá de la vida del contenedor específico que los estaba usando.

**15.** No, por defecto un contenedor Docker recién creado **no expone ningún puerto** hacia el exterior. Para exponer un puerto específico y que sea accesible desde fuera del contenedor (por ejemplo, desde el navegador en la máquina host), hay que indicarlo explícitamente usando la flag `-p puertoHost:puertoContainer` al ejecutar `docker run`.

**16.** Un Dockerfile define cómo **construir una única imagen**: describe el entorno y las dependencias de un solo contenedor, usando comandos como `FROM`, `COPY`, `RUN`, `ENTRYPOINT` y `CMD`, y no gestiona redes ni volúmenes por sí mismo. Un archivo `docker-compose.yml`, en cambio, define cómo **ejecutar y coordinar múltiples contenedores** como parte de una misma aplicación: maneja la configuración de varios servicios/contenedores a la vez (sus imágenes o Dockerfiles, sus puertos, cómo se conectan entre sí), y sí puede definir redes y volúmenes compartidos entre esos múltiples contenedores. Se usa con comandos propios como `docker-compose up`, `down`, `build` y `logs`.

**17.** El servicio `web` usa `build: .` porque esa parte de la aplicación necesita construirse desde cero a partir de un **Dockerfile propio** que está en el proyecto (probablemente el código específico de esa aplicación, que no existe como imagen pública en ningún lado). El servicio `redis`, en cambio, usa `image: "redis:alpine"` porque Redis ya es un software estándar y ampliamente conocido que tiene **imágenes oficiales publicadas en Docker Hub** — no hace falta construir nada desde cero: simplemente se indica qué imagen pública usar, tal como se haría con un `docker run` directo sobre esa imagen, sin necesitar un Dockerfile propio para ese servicio.

**18.** El programa oficial de la Unidad 8 pide explícitamente "Definición y conceptos principales" de Arquitecturas Cloud, "Sistemas operativos Cloud" e "Implementación de arquitecturas Cloud" — es decir, teoría sobre **modelos y proveedores de computación en la nube** (por ejemplo, IaaS/PaaS/SaaS, AWS/Azure/Google Cloud, virtualización a nivel de datacenter y sistemas operativos pensados para entornos cloud). Ninguna de las cinco fuentes disponibles cubre esa teoría: todas están enfocadas en Docker como herramienta práctica para contenedores, sin entrar en la visión más amplia que pide el programa.

**19.** La imagen `sdk` contiene compilador y herramientas para restaurar, compilar y publicar. La imagen `runtime` solo contiene lo necesario para ejecutar el resultado. Separarlas reduce tamaño y superficie de ataque y evita distribuir herramientas de compilación o código fuente.

**20.** `bridge` conecta contenedores aislados dentro de un host; `host` comparte directamente la red del host; `none` elimina conectividad; `overlay` conecta servicios en varios hosts. Para dos servicios de la misma app en un único host, usaría una red `bridge` definida por el usuario.

**21.** Un `Volume` persiste en disco y sobrevive al contenedor; `tmpfs` vive en memoria y desaparece al detener/eliminar el contenedor. `tmpfs` sirve para datos temporales o sensibles que no deben persistir.

**22.** `depends_on` ordena dependencias/arranque, pero el proceso de la base puede seguir inicializándose. Hace falta `healthcheck`, reintentos o espera explícita de disponibilidad.

</details>
