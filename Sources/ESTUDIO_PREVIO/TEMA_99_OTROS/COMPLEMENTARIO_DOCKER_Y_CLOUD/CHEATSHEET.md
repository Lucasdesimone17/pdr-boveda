# Cheat Sheet complementario — Docker y Cloud (PDR)

Fuentes: 🟢 los 2 PDF y los 3 PPTX oficiales de Docker integrados en `TEORICO/08_ARQUITECTURAS_CLOUD/`. Sin prosa, solo tablas/sintaxis de referencia rápida.

⚠️ Solo cubre Docker práctico — el programa pide también teoría de "Arquitecturas Cloud" y "Sistemas operativos Cloud", sin cobertura en el material disponible.

## Docker — lo esencial

| Concepto | Definición |
|---|---|
| Docker | Plataforma de código abierto; proceso que corre en el host y ejecuta contenedores |
| Imagen | Plantilla (código + dependencias + config); vive en Docker Hub |
| Contenedor | Instancia en ejecución de una imagen; entorno aislado |
| Docker Hub | Biblioteca pública de imágenes (`hub.docker.com`) |

## Docker vs. Máquina Virtual

| | VM | Contenedor |
|---|---|---|
| Virtualiza | Hardware completo | Solo el proceso |
| SO propio | Sí (Guest OS) | No (comparte kernel del host) |
| Motor | Hypervisor | Docker Engine |
| Peso | Pesado | Liviano |

## Cuándo usar Docker

Desplegar rápido · Desarrollo/testing iterativo · Microservicios · Escalado horizontal

## Comandos básicos

| Comando | Qué hace |
|---|---|
| `docker build -t nombre .` | Crea imagen desde Dockerfile |
| `docker run [opts] imagen` | Ejecuta un contenedor |
| `docker images` | Lista imágenes locales |
| `docker ps` | Lista contenedores en ejecución |
| `docker start/stop nombre` | Inicia/detiene contenedor |
| `docker rm` | Elimina contenedor |

## Opciones de `docker run`

| Opción | Qué hace |
|---|---|
| `-d` | Segundo plano (detached) |
| `-p host:contenedor` | Mapea puertos |
| `-e` | Variable de entorno |
| `--name` | Nombre del contenedor |
| `-it` | Interactivo + pseudo-TTY |
| `-v` / `--mount` | Persistencia (volumen) |

## Opciones de `docker build`

| Opción | Qué hace |
|---|---|
| `-t` | Nombre/tag de la imagen |
| `-q` | Quiet, sin logs |
| `--no-cache` | Fuerza build completo |

## Dockerfile — instrucciones

| Instrucción | Función |
|---|---|
| `FROM` | Imagen base |
| `WORKDIR` | Directorio de trabajo en el contenedor |
| `COPY` | Copia archivos del host al contenedor |
| `RUN` | Ejecuta comandos al construir la imagen |
| `EXPOSE` | Declara el puerto que usa la app |
| `CMD` | Comando por defecto al iniciar (sobreescribible) |
| `ENTRYPOINT` | Comando fijo (params se suman, no reemplazan) |

### Multi-stage .NET

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

## CMD vs ENTRYPOINT

| | CMD | ENTRYPOINT |
|---|---|---|
| Sobreescribible | Sí, desde consola | No (salvo `--entrypoint`) |
| Params extra del usuario | Reemplazan | Se suman |

```dockerfile
ENTRYPOINT ["sleep"]
CMD 5
```
`docker run mi-imagen 15` → duerme 15s · `docker run mi-imagen` → duerme 5s (usa CMD)

## Ejemplo Dockerfile completo

```dockerfile
FROM node:16-alpine
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
EXPOSE 3000
CMD ["node", "app.js"]
```

## Volúmenes

| Mecanismo | Detalle |
|---|---|
| Bind Mount | Monta directorio del host; `docker run -v /host/dir:/app -it …` |
| Volume | Administrado por Docker, portable, sobrevive al contenedor; `docker volume create my-vol` |
| tmpfs | Memoria temporal; no persiste al eliminar el contenedor |

## Networking

```bash
docker network create -d bridge my-net
docker run --network=my-net …
```
Por defecto NO se expone ningún puerto — usar `-p host:contenedor`.

| Red | Idea |
|---|---|
| `bridge` | Contenedores del mismo host, aislados del exterior |
| `host` | Comparte la red del host |
| `none` | Sin red |
| `overlay` | Varios hosts/Swarm |

## Dockerfile vs Docker Compose

| Dockerfile | Docker Compose |
|---|---|
| Una imagen | Múltiples contenedores |
| Un contenedor | Configuración de varios |
| `FROM`, `COPY`, `RUN`, `CMD` | `docker-compose up/down/build/logs` |
| No gestiona redes/volúmenes | Sí gestiona redes/volúmenes |

## Ejemplo docker-compose.yml

```yaml
services:
  web:
    build: .
    ports:
      - "8000:5000"
  redis:
    image: "redis:alpine"
```

## Preguntas típicas de parcial

🔵 Reciente: explicar un Dockerfile (30/06/2025 matutino y nocturno) e interpretar un `docker-compose.yml` (01/12/2025 nocturno).

## ⚠️ Pendiente

Teoría amplia de "Arquitecturas Cloud" y "Sistemas operativos Cloud" sin cobertura oficial. Las 3 presentaciones `.pptx` ya están integradas.
