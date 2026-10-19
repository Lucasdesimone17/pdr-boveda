---
date: 2026-10-13
type: class-note
tags:
  - pdr
  - clase
  - obligatorio-2
  - rabbitmq
  - docker
ai-first: true
confidence: high
duracion: "~1h42"
fuente: "Grabacion del showcase de Vimeo 12367503, video 1236337884, subtitulos autogenerados es-x-autogen"
---

# Clase 2026-10-13 - Usuarios de RabbitMQ y Docker para el Obligatorio 2

## For future agent
Acta de la clase del **2026-10-13** (martes). Dictan dos docentes: una docente que lleva la demo y las diapositivas, y [[Enrique Latorres]], que agrega comentarios y muestra los ejemplos de C#. La transcripcion no da el nombre de la docente; por el plantel probablemente es [[Natalia Franco]], sin confirmar. Tiene dos partes: hasta el minuto ~22, **RabbitMQ con usuarios de permisos minimos**; despues, **Docker**: teoria, Dockerfile, Compose, el ejemplo multi-stage de la catedra y **la version sin multi-stage que pide el Obligatorio 2**. Escrita desde transcripcion automatica.

**La devolucion de las defensas del Obligatorio 1**, anunciada el 08-10 para esta clase, **no aparece en la grabacion** (as of 2026-10-19).

## RabbitMQ en produccion: usuarios con permisos minimos
**Por que** (min ~1-3). En los ejemplos el `ConnectionFactory` usa `localhost` y el usuario `guest`. En produccion las colas y los exchanges **ya existen** y la aplicacion solo los usa. Poner claves de administrador en el ejecutable es inseguro: se encuentran buscando dentro del binario, y encriptarlas solo sube un poco la vara. La aplicacion distribuida tiene que usar **un usuario con los permisos minimos** para sus colas, sin poder cambiar la configuracion ni usar el broker para otra cosa. (stated)

**Demo en la consola de administracion de RabbitMQ:**
1. Crear un usuario **administrador**, que puede crear exchanges, usuarios, canales, colas y bindings. Entrar como ese admin.
2. Crear dos usuarios: **receptor** y **sender** (transcripto "centro"). Password corta solo para la demo: *"No usen passwords asi"*. (stated)
3. Los permisos son tres expresiones regulares: **configuracion, escritura y lectura**. `.*` da todo; una expresion que no coincide con ningun nombre no da nada. (stated)
4. Al receptor: sin configuracion, sin escritura, y **lectura sobre la cola `logs-receptor`**.
5. Crear el exchange **`direct_logs`**: tipo direct, durable, sin autodelete, no internal. Crear la cola `logs-receptor`, durable.
6. **Bindings** del exchange a la cola con routing keys `info`, `warning` y `error`.
7. Al sender: sin configuracion, **escritura sobre `direct_logs`**, sin lectura.

**Cambios en el codigo** (min ~12-18):
- El `ConnectionFactory` lleva **`UserName` y `Password`** del usuario creado. (stated)
- Se borran las declaraciones de exchange, cola y binding: *"ya lo hicimos, ya esta en Rabbit"*. El receptor solo necesita una constante con el nombre de la cola. (stated)
- Demo: un receptor con `info` y otro con `error`; un `warning` no le llega a nadie. (stated)

**Para el obligatorio:** *"No es necesario, pero si lo hacen, mas fino"*. (stated)

**Imagen preconfigurada.** Se puede configurar un contenedor con usuarios, admin y colas, **crear una imagen** a partir de el y publicarla en Docker Hub. Solo el equipo conoce la clave del admin y los usuarios solo usan la cola. (stated)

## Docker: teoria
Repaso, porque ya lo vieron en Diseno 2, Infraestructura y Taller de Tecnologias. (stated)

- **Ventajas:** ejecucion consistente en cualquier maquina y sistema operativo, escalable porque se replican contenedores, y aislamiento entre aplicaciones. Apunta a **microservicios**, como el **obligatorio final**, con varios servidores en contenedores distintos. (stated)
- **Imagen:** archivo estatico con todo lo que la aplicacion necesita. Enrique la comparo con **clonar un disco bloque a bloque**: la imagen es la estructura del disco, congelada, y de una imagen salen muchos contenedores. Se guardan en **Docker Hub**, de donde salio la de RabbitMQ. (stated)
- **Contenedor:** instancia en ejecucion de una imagen, donde ocurre la entrada/salida. Lo que cambia se persiste en ese contenedor. (stated)
- **Contra una VM:** Docker es mucho mas liviano porque levanta solo lo necesario. (stated)
- **Linux vs Windows y Mac:** en Linux el contenedor es una extension del sistema operativo (namespaces y cgroups) y es mucho mas eficiente. Windows tenia que virtualizar por hardware. Windows 11 agrego algo parecido. (stated)
- **Comandos:** `docker build`, `docker run`, `docker ps`, `docker stop`, `docker rm`. (stated)
- **Dockerfile:** `FROM` (imagen base), `RUN` (instalar dependencias), `COPY`, `WORKDIR` (si no existe lo crea), `CMD` o `ENTRYPOINT` (lo que ejecuta al arrancar). Patron tipico: `WORKDIR /app` y despues `COPY . .`. (stated)

**Seguridad de la imagen (Enrique):**
- **`FROM scratch`**: imagen vacia, solo con las interfaces al kernel. Se compila la aplicacion como **single file** y se copia sola. Si alguien entra al contenedor *"no hay nada"*: ni editor ni shell. Interesante para distribuir productos. (stated)
- **No dejar el SDK en la imagen final.** Compilar adentro de una imagen con todo el ambiente de desarrollo es tentador, pero deja las herramientas a mano de un atacante. *"Tengan absolutamente lo minimo necesario para funcionar"*. (stated)
- **Usuario no root** dentro del contenedor, para que la aplicacion no pueda modificar la mayor parte del sistema de archivos. (stated)

**Usos:** desarrollo web, bases de datos con varias versiones lado a lado y entornos de prueba. Con **`--rm`** el contenedor se borra al terminar, y con **`-v`** se le pasan carpetas del host. Ejemplo: un respaldo que comprime una carpeta y se autodestruye, y que **solo puede tocar las carpetas que se le pasan**. (stated)

**Dockerfile vs Docker Compose:**
- El Dockerfile dice **como construir una imagen**. Compose dice **como ejecutar y coordinar varios contenedores**, en `docker-compose.yaml`. *"Una cosa no quita la otra"*. (stated)
- Compose sin Dockerfile sirve si las imagenes se usan tal cual. Si hay que compilar, instalar o agregar configuracion, cada servicio apunta a su Dockerfile. (stated)
- Compose define **precedencia**: levantar primero el servidor y despues el cliente. (stated)
- Comandos de Compose: `up`, `down`, `build`, `logs`. **Compose crea redes y volumenes**; el Dockerfile no. (stated)

## Ejemplos de C# en Docker (Enrique)
**Solucion por linea de comandos.** `dotnet new sln`, `dotnet new classlib` para `Common`, dos `dotnet new console` para servidor y cliente, y las referencias a `Common`. Son seis comandos y se pueden meter en un script. (stated)

**Ejemplo `04-Async-04-Docker`, multi-stage** (min ~57-80). Esta en el drive. Es una aplicacion de consola que recorre directorios para ver el sistema de archivos del contenedor. Etapas:
1. `base`: runtime de .NET 8, un usuario no root pasado por parametro, `WORKDIR /app`.
2. `build`: imagen del SDK, `BUILD_CONFIGURATION=Release`. Copia el `.csproj`, hace `dotnet restore`, despues copia el resto del fuente y hace `dotnet build -o /app/build`.
3. `publish`: `dotnet publish` con **`UseAppHost=false`**, *"para que no de unos errores"* adentro del contenedor.
4. `final`: parte de `base` y copia **solo lo publicado**. `ENTRYPOINT` con `dotnet` y la dll.

Restore antes de copiar el resto, porque las bibliotecas salen del `.csproj`. En `COPY . .`, el primer punto es el host y el segundo el `WORKDIR` del contenedor. (stated)

**El multi-stage no va en el Obligatorio 2.** *"En el segundo obligatorio no. Despues, en el tercero si puede hacerlo"*. Si se le pide a la IA o a Rider que dockerice, **casi seguro genera multi-stage**. *"Tengan cuidado con eso"*, porque *"le tenemos que sacar los puntos"*. (stated)

**Con `Common`.** El Dockerfile de la diapositiva parecia no copiar `Common`. Al revisarlo, **copia toda la solucion** y despues construye solo el servidor, que trae `Common` por la referencia del `.csproj`. El docente dijo que igual lo revisa. (stated)

**Single file.** `dotnet publish` con single file en `true` mete DLL, PDB y runtime en **un solo ejecutable**, que se puede poner en una imagen `scratch`. (stated)

**Variables de entorno.** El servidor recibe por variables de entorno el **directorio de las fotos**, la **IP**, que es **`0.0.0.0`** para aceptar desde todas las interfaces, y el **puerto**. Tambien se pueden fijar con **`ENV`** en el Dockerfile. Un **bind mount** del host a `/shared/photos` hace que las imagenes que recibe el servidor se vean en la maquina. En Windows la ruta del host es del estilo `C:/Users/...`. (stated)

## La version sin multi-stage (lo que pide el Obligatorio 2)
Ejemplo sobre la solucion **SynchronousSimpleClientServer**, con cliente, servidor y `Common`. El link de la diapositiva *"no se si esta todavia bien"*. (stated)
1. `dotnet restore` del servidor, ir al directorio del servidor y compilar **en Release**. Queda en `bin/Release/net8.0`, con `Common.dll` adentro. Usar la version de .NET que corresponda. (stated)
2. En ese directorio, un Dockerfile de una etapa: `FROM` el runtime de .NET, `WORKDIR` (*"puede llamarse A, puede llamarse Victor"*), `COPY` del release al `WORKDIR`, y `CMD` con `dotnet Server.dll`. (stated)
3. `docker build` para crear la imagen. (stated)
4. Crear en el host las carpetas a compartir: fotos y archivos de persistencia. (stated)
5. `docker run` con nombre, **`-it`**, `-p 9000`, un `-v` por carpeta, las variables de entorno y la imagen. (stated)

**El cliente ahora va en contenedor.** *"Antes pedian que el cliente estaba afuera. Ahora pedimos que el cliente este adentro del contenedor"*. Hay que decirle donde esta el servidor, con variable de entorno o archivo de configuracion. (stated)

**Por que Compose.** Docker tiene **DNS interno**: el cliente se conecta al servidor **por nombre de contenedor**, sin IP. Y `docker compose up` levanta todo de una vez, sin pasar a mano los parametros de cada `docker run`. (stated)

**`docker attach`.** Para entrar a la consola de un contenedor hace falta **`-it`**. En Compose: **`stdin_open: true`** es el `-i` y **`tty: true`** es el `-t`. (stated)

## Ejemplo de Compose con bases de datos (de Base de Datos 2)
Esta en la carpeta de Base de Datos 2, seccion Mongo (ruta exacta TBD). Tres servicios:
- **Oracle Express**, puerto **1521**, password del admin por variable. Un volumen a `/opt/oracle/scripts/startup` con **scripts que corren la primera vez**: crean usuario, password, permisos y tablas. Con `stdin_open` y `tty` para entrar y usar SQL*Plus.
- **MongoDB**, con usuario admin y una base de prueba inicializada por script (`mongo-init`).
- **Node.js** como ambiente de desarrollo, con el fuente montado desde el host y **`depends_on` Oracle y Mongo**. Su Dockerfile instala herramientas y las bibliotecas de Oracle y Mongo.

Sirve de idea para **inicializar la base del servidor** con las tablas listas. Hay ejemplos de C# leyendo y escribiendo tablas en Oracle, que el docente pasa despues. (stated)

## Avisos para el Obligatorio 2
- *"¿Ya tienen pensado que base de datos van a usar? Deberian ahora"*. (stated)
- Si ya usaron Docker con bases en Diseno, *"tomense su tiempo para hacerlo bien"*. (stated)
- Lista dicha al cierre: pasar las funciones a asincronico, **dockerizar cliente y servidor con las dependencias en comun**, y **el release**. (stated)
- **El release va igual en el repositorio**, como en el Obligatorio 1, aunque Compose lo genere. *"Es medio redundante"*. (stated)
- Se puede elegir **solo Dockerfiles o Dockerfiles con Compose**, y documentar como se hizo. (stated)
- Consultas por mensaje siempre que se tranquen. (stated)
- En el minuto ~99 la transcripcion dice *"Te entrega el Iris 29"*. Casi seguro es **"se entrega el 29"**, que coincide con la letra (correccion probable).

## Salvedades de la transcripcion
- Correcciones probables: **sender** (transcripto "centro"), `direct_logs` ("direct load"), `UseAppHost` ("use app host"), `stdin_open` ("STDI no Open").
- El ejemplo en Rider se llama `04-Async-04-Docker`. En la boveda la carpeta es `04_Async_04_Docker`: ver [[Ejemplos de codigo de asincronia de clase]].
- En el minuto ~99 hay un intercambio confuso (*"¿Como vienen con el Juli?"*): no se registra.

## Connections
- [[Obligatorio 2 - Upgrade asincrono y Docker de EDISON]] - la consigna que baja esta clase
- [[Complementario - Docker y Cloud]] · [[Tema 4 - Colas de mensajes y MOM]]
- [[Clase 2026-10-08 - Letra del Obligatorio 2 y patron RPC sobre RabbitMQ]] - la clase anterior
- [[Clase 2026-10-15 - RPC, gRPC y Protocol Buffers]] - la siguiente
- [[Programacion de Redes 2026]] · [[Enrique Latorres]]
