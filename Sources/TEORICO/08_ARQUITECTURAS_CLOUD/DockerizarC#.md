<!-- Slide number: 1 -->
# DockerizarC#
Para Linux y Windows

### Notes:

<!-- Slide number: 2 -->
# Hacer un proyecto en c#
En particular queremos interactuar mediante consola.
Es una aplicación de consola.
Nos va a mostrar directorios que le indiquemos.
Es un ejemplo:
No tiene buen diseño.
No controla adecuadamente las excepciones.
Se puede demorar mucho con directorios con estructuras complejas.
No tiene hilos, ni tasks, ni puertos, etc.

### Notes:

<!-- Slide number: 3 -->
# Nuestro código es el siguiente:
// File: Program.cs
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Enter a directory path to list files (or 'exit' to quit):");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input) || input.ToLower() == "exit") break;

            if (Directory.Exists(input))
            {
                Console.WriteLine($"Listing files in: {input}");
                try
                {
                    var files = Directory.GetFiles(input, "*", SearchOption.AllDirectories);

                    foreach (var file in files)
                        Console.WriteLine(file);
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    Console.ResetColor();
                    Console.WriteLine("Could not scan directory {0}", input);

                }
            }
            else
            {
                Console.WriteLine("Directory does not exist.");
            }
        }
    }
}

### Notes:

<!-- Slide number: 4 -->
# DockerFile por defecto generado por el GUI
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["04_Async_04_Docker/04_Async_04_Docker.csproj", "04_Async_04_Docker/"]
RUN dotnet restore "04_Async_04_Docker/04_Async_04_Docker.csproj"
COPY . .
WORKDIR "/src/04_Async_04_Docker"
RUN dotnet build "./04_Async_04_Docker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./04_Async_04_Docker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "04_Async_04_Docker.dll"]

### Notes:

<!-- Slide number: 5 -->
# DockerFile para Linux
# Dockerfile.linux
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish 04_Async_04_Docker.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "04_Async_04_Docker.dll"]

### Notes:

<!-- Slide number: 6 -->
# Comandos para construir en Linux
Asumo que estoy en:
mipc:~/RiderProjects/ProgramacionDeRedes/04_Async_04_Docker$

build-lnx.sh
#!/bin/bash
# For Linux
docker build -t mydockerapp-lnx -f Dockerfile.linux .

dockerrun.sh
#!/bin/bash
docker run -it --rm  -v $(pwd)/shared:/app/shared  -v mydockerapp_data:/app/data mydockerapp-lnx

### Notes:

<!-- Slide number: 7 -->
# Ejecución
miPC:~/RiderProjects/ProgramacionDeRedes/04_Async_04_Docker$ ./dockerrun.sh
Enter a directory path to list files (or 'exit' to quit):
/app
Listing files in: /app
/app/04_Async_04_Docker
/app/04_Async_04_Docker.deps.json
/app/04_Async_04_Docker.pdb
/app/04_Async_04_Docker.runtimeconfig.json
/app/04_Async_04_Docker.dll
Could not scan directory /app
Enter a directory path to list files (or 'exit' to quit):
exit
miPC:~/RiderProjects/ProgramacionDeRedes/04_Async_04_Docker$

### Notes:

<!-- Slide number: 8 -->
# DockerFIle para Windows
﻿# File: Dockerfile.windows
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish 04_Async_04_Docker.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "04_Async_04_Docker.dll"]

### Notes:

<!-- Slide number: 9 -->
# Comandos para construir en Linux
Asumo que estoy en:
c:\Users\enrique\Documents\04_Async_04_Docker

build-win.bat
REM #!/bin/bash
REM # For Windows
docker build -t mydockerapp-win -f Dockerfile.windows .

dockerrun.bat
echo off
docker run -it --rm -v .\shared:/app/shared -v mydockerapp_data:/app/data mydockerapp-win

### Notes:

<!-- Slide number: 10 -->
# Podemos probar también con …
ClientServer_02_server_directory

dotnet publish -c Release -r linux-x64 --self-contained true /p:PublishSingleFile=true

dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true

### Notes:

<!-- Slide number: 11 -->
# Podemos probar también con …
Right-click your project → Publish.
Choose:
Target Framework: .NET 8.0 or your target
Configuration: Release
Deployment Mode: Self-contained
Target Runtime:
linux-x64 (Linux 64-bit)
win-x64 (Windows 64-bit)
osx-x64 (macOS)
 Enable "Produce single file"
 (Optional) Enable "Trim unused assemblies"
 (Optional) Enable Native AOT (if desired)

### Notes:

<!-- Slide number: 12 -->
# Dockerfile para Common
﻿FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Servidor/Servidor.csproj", "Servidor/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "Servidor/Servidor.csproj"
COPY . .
WORKDIR "/src/Servidor"
RUN dotnet build "Servidor.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Servidor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Servidor.dll"]

### Notes:

<!-- Slide number: 13 -->
# docker run para Common
docker build -t imagenServidorDocker .

docker run -it --name miservidor -p 164.73.50.17:5000:5000  -v /home/user/RiderProjects/miservidor/Fotos:/shared/Fotos-v /home/user/RiderProjects/miservidor/Files:/shared/Files-e ElLugarDeLasFotos=/shared/Fotos -e ElLugarDeLosArchivos=/shared/Archivos -e LaDireccionDelServidor=0.0.0.0  -e ServerPort=5000 imagenServidorDocker

Las variables de ambiente pueden estar en el Dockerfile.
ENV LaDireccionDelServidor=0.0.0.0

### Notes:

<!-- Slide number: 14 -->
# docker

### Notes:

<!-- Slide number: 15 -->
# Fin

¿Preguntas?

### Notes: