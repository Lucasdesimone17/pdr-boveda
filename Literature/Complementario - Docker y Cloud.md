---
date: 2026-08-26
type: literature
tags:
  - literature
  - pdr
  - docker
  - cloud
  - complementario
ai-first: true
source: "Sources/TEORICO/08_ARQUITECTURAS_CLOUD/"
source_extra:
  - "Sources/ESTUDIO_PREVIO/TEMA_99_OTROS/COMPLEMENTARIO_DOCKER_Y_CLOUD/CHEATSHEET.md"
confidence: stated
frecuencia_parcial: "4/16 (25%), en alza desde 2025"
---

# Complementario - Docker y Cloud

## For future agent
Nota complementaria: Docker y contenedores. **No es uno de los 6 temas vigentes de Aulas** (venía de la unidad 8 "Arquitecturas Cloud" del programa histórico), pero aparece en parciales recientes: explicar un Dockerfile (30/06/2025, matutino y nocturno) e interpretar un `docker-compose.yml` (01/12/2025, nocturno). Frecuencia total 4 de 16, en alza. La teoría amplia de arquitecturas y sistemas operativos cloud **no tiene cobertura** en el material local: eso es un hueco real, ver [[Huecos y contradicciones del material PDR]].

## Source
`Sources/TEORICO/08_ARQUITECTURAS_CLOUD/` - `Docker.md`, `Docker 2025.md`, `Docker 2in1 2025.md`, `06 - Docker (Renzo Chiarino).md`, `DockerizarC#.md`.

## Key Claims

### Conceptos
| Concepto | Definición |
|---|---|
| Docker | Plataforma open source; proceso que corre en el host y ejecuta contenedores |
| Imagen | Plantilla: código + dependencias + configuración. Vive en Docker Hub |
| Contenedor | Instancia en ejecución de una imagen; entorno aislado |
| Docker Hub | Biblioteca pública de imágenes (`hub.docker.com`) |

### Contenedor vs máquina virtual (pregunta clásica)
| | VM | Contenedor |
|---|---|---|
| Virtualiza | Hardware completo | Solo el proceso |
| SO propio | Sí (guest OS) | No: comparte el kernel del host |
| Motor | Hypervisor | Docker Engine |
| Peso | Pesado | Liviano |

Cuándo conviene: despliegue rápido, desarrollo y testing iterativo, microservicios, escalado horizontal.

### Comandos
- `docker build -t nombre .` · `docker run [opts] imagen` · `docker images` · `docker ps` · `docker start/stop` · `docker rm`.
- Opciones de `run`: `-d` detached, `-p host:contenedor` mapeo de puertos, `-e` variable de entorno, `--name`, `-it` interactivo con TTY, `-v`/`--mount` persistencia.
- Opciones de `build`: `-t` tag, `-q` quiet, `--no-cache`.

### Dockerfile
| Instrucción | Función |
|---|---|
| `FROM` | Imagen base |
| `WORKDIR` | Directorio de trabajo dentro del contenedor |
| `COPY` | Copia del host al contenedor |
| `RUN` | Ejecuta al construir la imagen |
| `EXPOSE` | Declara el puerto de la app |
| `CMD` | Comando por defecto, sobreescribible |
| `ENTRYPOINT` | Comando fijo; los parámetros extra se suman |

`CMD` vs `ENTRYPOINT`: `CMD` se puede reemplazar desde la consola, `ENTRYPOINT` no (salvo `--entrypoint`), y los argumentos del usuario se **suman** al entrypoint en vez de reemplazarlo. Con `ENTRYPOINT ["sleep"]` + `CMD 5`: `docker run img 15` duerme 15 segundos, `docker run img` duerme 5.

Multi-stage para .NET, que es el patrón que el parcial pide explicar:
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish MiApp.csproj -c Release -o /out
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "MiApp.dll"]
```
La razón del multi-stage: la imagen final lleva runtime, no SDK. Dejar herramientas de compilación en la imagen final está en la lista de errores caros.

### Volúmenes
| Mecanismo | Detalle |
|---|---|
| Bind mount | Monta un directorio del host: `-v /host/dir:/app` |
| Volume | Administrado por Docker, portable, sobrevive al contenedor: `docker volume create my-vol` |
| tmpfs | En memoria; no persiste |

### Networking
`docker network create -d bridge my-net` y `docker run --network=my-net ...`. Por defecto **no se expone ningún puerto**: hay que usar `-p host:contenedor`.

| Red | Idea |
|---|---|
| `bridge` | Contenedores del mismo host, aislados del exterior |
| `host` | Comparte la red del host |
| `none` | Sin red |
| `overlay` | Varios hosts, Swarm |

### Dockerfile vs Compose
Dockerfile define **una** imagen y un contenedor y no gestiona redes ni volúmenes. Compose describe **varios** servicios y sí gestiona redes y volúmenes (`docker-compose up/down/build/logs`).

```yaml
services:
  web:
    build: .
    ports:
      - "8000:5000"
  redis:
    image: "redis:alpine"
```

## Methodology
Cinco materiales sobre el mismo tema (dos PDF y tres PPTX), con solapamiento alto. `DockerizarC#` es el más específico del curso; `Docker 2in1 2025` el más extenso.

## Critique
- El material es todo Docker práctico: del enunciado del programa histórico ("Definición y conceptos principales. Sistemas operativos Cloud. Implementación de arquitecturas Cloud") solo cubre la parte de implementación local. IaaS/PaaS/SaaS, modelos de despliegue y decisiones de arquitectura cloud no están.
- Duplicación entre los cinco archivos sin indicación de cuál es la versión vigente.
- No trata healthchecks ni límites de recursos más allá de mencionarlos en el corpus previo.

## Dicho en clase

### 2026-10-13 ([[Clase 2026-10-13 - Usuarios de RabbitMQ y Docker para el Obligatorio 2]])
- Imagen estatica, como un disco clonado; contenedor como instancia en ejecucion. Docker rinde mucho mejor en Linux (namespaces y cgroups). (stated)
- Seguridad: **`FROM scratch` con single file**, **no dejar el SDK en la imagen final** y **usuario no root**. (stated)
- `--rm` y `-v` para encapsular operaciones que solo tocan las carpetas pasadas. (stated)
- Dockerfile construye una imagen; **Compose coordina contenedores**, con precedencia (`depends_on`), redes y volumenes. (stated)
- Ejemplo multi-stage `04-Async-04-Docker` (`base`, `build`, `publish` con **`UseAppHost=false`**, `final`). **No va en el Obligatorio 2, si en el 3.** (stated)
- Version de una etapa: release en `bin/Release/net8.0`, `COPY` al `WORKDIR` y `CMD dotnet Server.dll`. Configuracion por **variables de entorno** o `ENV`, IP `0.0.0.0` y bind mounts para ver los archivos en el host. (stated)
- **DNS interno de Docker**: el cliente encuentra al servidor por nombre de contenedor. **`docker attach` necesita `-it`**, que en Compose son `stdin_open: true` y `tty: true`. (stated)
- Ejemplo de Compose de Base de Datos 2: Oracle XE en 1521 con scripts en `/opt/oracle/scripts/startup`, MongoDB y Node con `depends_on`. (stated)

## Connections
- Se usa para levantar el broker de [[Tema 4 - Colas de mensajes y MOM]] y para desplegar los servicios de [[Tema 6 - APIs REST y WebSockets]].
- Frecuencia y formato: [[Patrones de examen en Programacion de Redes]] · [[Errores caros en Programacion de Redes]].
- Mapa: [[Mapa del curso Programacion de Redes]]. Huecos: [[Huecos y contradicciones del material PDR]].
