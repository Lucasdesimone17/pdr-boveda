# TEMA 8 — Arquitecturas Cloud (PDR)

**Cobertura oficial (programa, 8 unidades):** *"Arquitecturas Cloud: Definición y conceptos principales. Sistemas operativos Cloud. Implementación de arquitecturas Cloud."*

**Fuentes principales:** 🟢 `TEORICO/08_ARQUITECTURAS_CLOUD/Docker.pdf` (12 diapositivas) y 🟢 `TEORICO/08_ARQUITECTURAS_CLOUD/06 - Docker (Renzo Chiarino).pdf` (22 diapositivas, movido desde el inbox el 12/08/2026 con tu confirmación). Ambos oficiales de Aulas — el segundo complementa al primero (misma herramienta, cátedra distinta, más profundidad práctica), no lo reemplaza.

**Evidencia de evaluación (🔵):** ninguna citable todavía. `PARCIALES/ANALISIS_DE_FRECUENCIA.md` sigue sin procesar — mismo pendiente transversal a toda la materia.

**Convención de tags:** 🟢 Oficial · 🔵 Evidencia de evaluación · 🟠 Complementario (agregado por el asistente).

⚠️ **Sobre el programa oficial:** las dos fuentes disponibles son **100% Docker práctico** — ninguna cubre la teoría que pide literalmente el programa: "Definición y conceptos principales de Arquitecturas Cloud", "Sistemas operativos Cloud" ni "Implementación de arquitecturas Cloud" en el sentido de proveedores/modelos cloud (IaaS/PaaS/SaaS, AWS/Azure/GCP, etc.). No se inventa contenido — queda marcado como pendiente al final.

⚠️ **Material sin integrar:** además de estos 2 PDF, hay 3 archivos `.pptx` en el inbox (`Docker 2025.pptx`, `Docker 2in1 2025.pptx`, `DockerizarC#.pptx`) que no se pudieron leer con las herramientas disponibles en esta sesión (no se puede extraer texto de binarios `.pptx` directamente). Quedan pendientes de integrar si se convierten a PDF o se puede extraer su texto en otra sesión.

---

## Recuperación — preguntas antes de leer

1. ¿Por qué "funciona en mi computadora" no alcanza para llevar una app a producción?
2. ¿Qué diferencia esperás entre una máquina virtual y un contenedor?
3. ¿Por qué un Dockerfile se parece a una receta de cocina?

---

## 1. Qué es Docker y por qué existe

🟢 [Docker.pdf, Pág. 1] + 🟢 [Renzo Chiarino, Pág. 3]

**Definición formal** (Renzo Chiarino): Docker es una **plataforma de código abierto** que permite desarrollar, enviar y ejecutar aplicaciones dentro de **contenedores**, proporcionando un entorno ligero, portable y autosuficiente.

**Definición informal** (Renzo Chiarino): en otras palabras, Docker no es más que un **proceso que corre en nuestro sistema operativo** (host). Este proceso nos ayuda a ejecutar contenedores dentro del mismo, y es en esos contenedores donde los desarrolladores pueden trabajar y crear sus propias aplicaciones.

**Definición de Docker.pdf**: proyecto de código abierto que permite crear, probar y desplegar aplicaciones de forma rápida y eficiente; encapsula las dependencias de una aplicación y la ejecuta de forma independiente en cualquier entorno.

**Por qué existe** [Docker.pdf, Pág. 2, meme oficial]: *"— En mi computadora sí funciona. — Sí, pero no le vamos a dar tu computadora al cliente."* Docker resuelve el problema clásico de inconsistencia entre entornos (desarrollo, testing, producción).

**Propósito y beneficios** [Docker.pdf, Pág. 3]:

| Beneficio | Descripción |
|---|---|
| Eficiencia en el desarrollo | El código se ejecuta de forma consistente en cualquier lugar |
| Portabilidad | Corre en distintos entornos (SO, plataformas) sin modificaciones |
| Escalabilidad | Facilita replicar contenedores para gestionar tráfico creciente |
| Seguridad | Aísla aplicaciones en contenedores independientes, evitando conflictos |

<details class="autoeval">
<summary>Autoevaluación</summary>
Explicá el meme de la Pág. 2 con tus propias palabras: ¿qué problema real resuelve Docker?
</details>

---

## 2. Imágenes y Contenedores

🟢 [Docker.pdf, Pág. 4] + 🟢 [Renzo Chiarino, Pág. 4-5]

| Concepto | Definición oficial |
|---|---|
| **Imagen** | Plantilla que contiene el código, las dependencias, las bibliotecas y la configuración necesaria para ejecutar una aplicación. Se almacenan en repositorios como **Docker Hub** y se pueden compartir |
| **Contenedor** | Instancia en ejecución de una imagen. Entorno aislado que contiene todo lo necesario para ejecutar la aplicación (SO, bibliotecas, archivos de configuración) |

**Contenedor, definición informal** (Renzo Chiarino): un paquete de software **aislado** que incluye una aplicación y todas sus dependencias necesarias para ejecutarse en cualquier entorno — *"es como una caja sellada que contiene todo lo que la aplicación necesita para funcionar correctamente."*

**Ejemplos de imágenes** (Renzo Chiarino): Redis, Python, Node.js, Nginx, Apache, RabbitMQ.

**Docker Hub** (`hub.docker.com`): biblioteca pública de imágenes que se pueden usar en los contenedores; también se pueden subir imágenes propias.

<details class="autoeval">
<summary>Autoevaluación</summary>
¿Cuál es la relación entre una imagen y un contenedor? ¿Se puede tener varios contenedores de la misma imagen?
</details>

---

## 3. Docker vs. Máquinas Virtuales

🟢 [Docker.pdf, Pág. 5] + 🟢 [Renzo Chiarino, Pág. 6]

**Diagrama oficial de arquitecturas** [Docker.pdf, Pág. 5]:

- **Virtual Machine Architecture**: `APP1/APP2/APP3` → cada una con su propio `BINS/LIBS` + `GUEST OS` → todos sobre un **Hypervisor** → `HOST OS`.
- **Docker Architecture**: `CONTAINER 1/2` → `APP BINS/LIBS` (sin Guest OS propio) → **Docker Engine** → `HOST OS`.

**Explicación** (Renzo Chiarino, Pág. 6): la ventaja de los contenedores es que múltiples aplicaciones pueden estar corriendo en el mismo OS y **compartir el kernel** (que permite a las aplicaciones acceder al hardware). Los contenedores corren en procesos separados, aislados entre sí, y son **más livianos** que las VM, administrando mejor los recursos del sistema.

Las **máquinas virtuales (VM)**, en cambio, simulan todo el equipo (hardware y software), por lo que requieren mejor infraestructura y son **más pesadas** que los contenedores.

| | Máquina Virtual | Contenedor Docker |
|---|---|---|
| Virtualiza | Hardware completo (vía Hypervisor) | Solo el proceso/app (comparte kernel del host, vía Docker Engine) |
| Incluye SO propio | Sí (Guest OS por VM) | No |
| Peso | Pesado | Liviano |
| Aislamiento | Total (a nivel hardware) | A nivel proceso |

> 🟠 **Complementario.** Esta comparación conecta con Tema 1 (Introducción) y Tema 2 (Threads/procesos): un contenedor es, en esencia, un **proceso aislado** del sistema operativo host (con su propio namespace de archivos, red y procesos), no una computadora simulada completa como una VM.

**¿Cuándo usamos Docker?** [Renzo Chiarino, Pág. 7]: al necesitar desplegar software rápido; desarrollar y probar de forma iterativa; manejar una **arquitectura de microservicios** (cada componente independiente del resto); o necesitar **escalar horizontalmente** de forma simple.

<details class="autoeval">
<summary>Autoevaluación</summary>
¿Por qué compartir el kernel del host hace que los contenedores sean más livianos que las VM?
</details>

---

## 4. Comandos básicos de Docker

🟢 [Docker.pdf, Pág. 6] + 🟢 [Renzo Chiarino, Pág. 9-11]

| Comando | Qué hace |
|---|---|
| `docker build [options] ruta` | Crea una imagen a partir de un Dockerfile local |
| `docker run [options] imagen` | Ejecuta una imagen como contenedor (si no está localmente, la busca en Docker Hub) |
| `docker images [options]` | Lista las imágenes guardadas localmente |
| `docker ps [options]` | Lista los contenedores en ejecución |
| `docker start` / `docker stop nombre` | Inicia o detiene un contenedor |
| `docker rm` | Elimina un contenedor |

**Opciones de `docker run`** [Renzo Chiarino, Pág. 10]:

| Opción | Qué hace |
|---|---|
| `-d` | Ejecuta el contenedor en segundo plano (detached), muestra el ID |
| `-p puerto-host:puerto-contenedor` | Mapea puertos |
| `-e` | Establece variables de entorno |
| `--name` | Establece el nombre del contenedor |
| `-i` | Agrega un proceso de entrada (junto con `-t`, forma `-it`, permite interactuar con el contenedor) |
| `-t` | Asigna un pseudo-TTY |
| `--mount` / `-v` | Asocian/dan persistencia a un contenedor |

**Opciones de `docker build`** [Renzo Chiarino, Pág. 11]: `-t` (asigna nombre/tag a la imagen), `-q` (quiet, no muestra logs), `--no-cache` (deshabilita el caché, fuerza construcción completa).

---

## 5. Dockerfile: estructura, componentes y ejemplos

🟢 [Docker.pdf, Pág. 7-8] + 🟢 [Renzo Chiarino, Pág. 12-16]

**Qué es** [Docker.pdf, Pág. 7]: un archivo de texto que contiene instrucciones para construir una imagen Docker. Cada instrucción define un paso en el proceso de construcción. Flujo: `Dockerfile → Docker Image → Docker Container`.

**Analogía oficial** (Renzo Chiarino, Pág. 12): *"Es como si fuera una receta de cocina, donde le decimos los ingredientes, proporciones y los pasos para obtener el resultado esperado"* — en este caso, una imagen para ejecutar en los contenedores. Los pasos tienen un orden y se ejecutan en secuencia.

**Componentes principales** [Docker.pdf, Pág. 8]:

| Instrucción | Función |
|---|---|
| `FROM` | Define la imagen base, punto de partida |
| `RUN` | Ejecuta comandos para instalar dependencias, configurar el entorno |
| `COPY` | Copia archivos del sistema local al contenedor |
| `WORKDIR` | Establece el directorio de trabajo dentro del contenedor |
| `CMD` | Define el comando que se ejecuta al iniciar el contenedor |

**Ejemplo oficial completo** [Renzo Chiarino, Pág. 13]:

```dockerfile
FROM node:16-alpine   # Utilizamos una imagen base de Node.js
WORKDIR /app           # Establecemos el directorio de trabajo
COPY package*.json ./  # Copiamos los archivos de nuestra aplicación
RUN npm install         # Instalamos las dependencias
COPY . .                # Copiamos el resto de los archivos de la aplicación
EXPOSE 3000             # Exponemos el puerto 3000
CMD ["node", "app.js"]  # Ejecutamos nuestra aplicación Node.js
```

**Comando para construir la imagen**: `docker build -t mi-imagen-custom .` — el `.` indica que el Dockerfile está en la carpeta actual; se puede reemplazar por la ruta completa a otro Dockerfile.

### CMD vs. ENTRYPOINT [Renzo Chiarino, Pág. 14-16]

| | `CMD` | `ENTRYPOINT` |
|---|---|---|
| Qué especifica | Comando por defecto al iniciar el contenedor | Instrucciones por defecto del contenedor |
| ¿Se puede sobreescribir? | Sí, editable desde la consola (ej. `docker run -it mi-imagen /bin/bash` reemplaza el CMD) | No admite cambios por parámetros, salvo usando `--entrypoint` como flag |
| Parámetros extra del usuario | Reemplazan al CMD | Se **suman** a los ya existentes en el ENTRYPOINT, no los reemplazan |

**Combinando ambos** [Pág. 16]: se pueden usar en simultáneo — `CMD` establece los parámetros por defecto si el usuario no pasa ninguno.

```dockerfile
ENTRYPOINT ["sleep"]
CMD 5
```

```bash
docker run mi-imagen 15   # Duerme 15s (parámetro enviado sobreescribe el CMD)
docker run mi-imagen      # Duerme 5s (usa el CMD por defecto, no se recibió parámetro)
```

<details class="autoeval">
<summary>Autoevaluación</summary>
¿Por qué `docker run -it mi-imagen /bin/bash` termina abriendo una consola en vez de ejecutar la app de Node?
</details>

---

## 6. Volúmenes: persistencia de datos

🟢 [Renzo Chiarino, Pág. 17-19]

Los contenedores tienen un sistema de archivos **aislado** del sistema host, lo que da mejor portabilidad y seguridad, pero plantea el problema de cómo **persistir datos** más allá de la vida útil de un contenedor. Tres mecanismos oficiales: **Bind Mount**, **Volume**, **tmpfs**.

| Mecanismo | Qué hace | Ejemplo |
|---|---|---|
| **Bind Mount** | Monta un directorio del host directamente en el contenedor. Cambios en el host se ven al instante en el contenedor, pero genera dependencia del sistema host | `docker run -v /var/www/html:/app -it …` |
| **Volume** | Administrado por Docker; desvincula completamente al contenedor del sistema operativo del host. Se puede compartir entre varios contenedores; **no se destruye** al eliminar/detener un contenedor | `docker volume create my-vol` → `docker run -d -v my-vol:/ruta/en/el/container imagen` |

<details class="autoeval">
<summary>Autoevaluación</summary>
¿Por qué un Volume es más portable que un Bind Mount, aunque ambos "persistan" datos?
</details>

---

## 7. Networking en Docker

🟢 [Renzo Chiarino, Pág. 20]

Por defecto, un contenedor nuevo **no expone ningún puerto** — hay que indicarlo con `-p puertoHost:puertoContainer`.

También se pueden crear **redes virtuales** entre contenedores, para que se intercomuniquen e intercambien información:

```bash
docker network create -d bridge my-net    # Crear una red
docker run --network=my-net …             # Usar la red
```

> Usualmente, si se necesita una network, también se usa Docker Compose — las redes suelen establecerse ahí directamente, no desde la consola.

---

## 8. Docker Compose

🟢 [Docker.pdf, Pág. 10-11] + 🟢 [Renzo Chiarino, Pág. 21-22]

**Qué es**: herramienta que permite definir y ejecutar **múltiples contenedores** Docker como un servicio (aplicación multi-contenedor). Se configura en un archivo `docker-compose.yml` (o `compose.yaml`), que describe los servicios (contenedores) de una aplicación y sus dependencias. Se enfoca en describir cómo desplegar y coordinar contenedores.

**Comando para ejecutar**: `docker compose up`.

**Dockerfile vs. Docker Compose (tabla oficial)** [Docker.pdf, Pág. 11]:

| Dockerfile | Docker Compose |
|---|---|
| Define cómo construir **una** imagen | Define cómo ejecutar **múltiples** contenedores |
| Describe entorno y dependencias de un solo contenedor | Maneja la configuración de múltiples contenedores |
| Comandos: `FROM`, `COPY`, `RUN`, `ENTRYPOINT`, `CMD` | Comandos: `docker-compose up`, `down`, `build`, `logs` |
| No gestiona redes ni volúmenes | Define redes y volúmenes para múltiples contenedores |

**Ejemplo oficial** [Renzo Chiarino, Pág. 22]:

```yaml
services:
  web:                    # Primer servicio, crea un container "web"
    build: .               # Ruta hacia un Dockerfile
    ports:
      - "8000:5000"
  redis:                  # Segundo servicio, crea un container "redis"
    image: "redis:alpine"  # Imagen directa de Docker Hub, sin Dockerfile propio
```

<details class="autoeval">
<summary>Autoevaluación</summary>
En el ejemplo, ¿por qué el servicio "web" usa `build: .` y el servicio "redis" usa `image: "redis:alpine"` en vez de `build`?
</details>

---

## 9. Ejemplos de uso de Docker

🟢 [Docker.pdf, Pág. 9]

| Escenario | Cómo ayuda Docker |
|---|---|
| Desarrollo web | Entornos de desarrollo consistentes: la app corre igual en cualquier lugar |
| Gestión de bases de datos | Ejecutar distintas versiones de BD en contenedores separados, simplificando manejo y configuración |
| Creación de entornos de prueba | (mencionado en la introducción de la diapositiva) |
| Implementación de microservicios | (mencionado en la introducción de la diapositiva) |

---

## Pendiente de verificar

- **El programa oficial pide teoría de "Arquitecturas Cloud"** (definición y conceptos principales), **"Sistemas operativos Cloud"** e **"Implementación de arquitecturas Cloud"** — en el sentido de modelos y proveedores cloud (IaaS/PaaS/SaaS, AWS/Azure/GCP, virtualización a nivel de datacenter). **Ninguna de las dos fuentes disponibles cubre esto**: ambas son 100% Docker práctico (contenedores en una máquina local/servidor), no arquitecturas cloud en sentido amplio. Mismo patrón que "Activación/ciclo de vida" en Tema 5 y "Precedencia explícita" en Tema 3: no se inventa contenido ausente.
- **3 archivos `.pptx` sin integrar** en el inbox (`Docker 2025.pptx`, `Docker 2in1 2025.pptx`, `DockerizarC#.pptx`) — no se pudo extraer su texto con las herramientas disponibles en esta sesión. `DockerizarC#.pptx` en particular podría traer un ejemplo específico de .NET que complementaría bien este resumen (los ejemplos oficiales actuales usan Node.js). Pendiente de integrar si se convierten a PDF/texto.
- No hay evidencia de parcial (🔵) citable: `PARCIALES/ANALISIS_DE_FRECUENCIA.md` sigue desactualizado.
- `tmpfs`, mencionado como tercer mecanismo de almacenamiento junto a Bind Mount y Volume [Renzo Chiarino, Pág. 17], no se desarrolla en ninguna diapositiva — solo aparece nombrado en el título de la sección.

---

## Repaso espaciado

**Día 3:**
1. Explicá con tus palabras la diferencia entre una imagen y un contenedor Docker.
2. ¿Por qué un contenedor es más liviano que una máquina virtual?
3. Nombrá los 5 componentes principales de un Dockerfile.

**Día 7:**
1. Reconstruí de memoria el ejemplo de Dockerfile para una app Node.js (FROM, WORKDIR, COPY, RUN, COPY, EXPOSE, CMD).
2. ¿Cuándo usarías CMD y cuándo ENTRYPOINT en un Dockerfile?
3. ¿Qué parte del programa oficial de Tema 8 sigue sin cobertura en el material disponible?

---

*Generado a partir de `TEORICO/08_ARQUITECTURAS_CLOUD/Docker.pdf` y `TEORICO/08_ARQUITECTURAS_CLOUD/06 - Docker (Renzo Chiarino).pdf` (fuentes oficiales).*
