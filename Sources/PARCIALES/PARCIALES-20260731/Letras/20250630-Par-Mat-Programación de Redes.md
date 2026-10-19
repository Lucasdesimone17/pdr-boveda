Fecha: 30/06/2025

Evaluación: Parcial 1

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Matutino

Puntaje Máximo:     35 Puntos

Página 1 de 2

Ejercicio 1

 1 | FROM mcr.microsoft.com/dotnet/sdk:8.0
 2 | WORKDIR /app
 3 | COPY *.csproj ./
 4 | RUN dotnet restore
 5 | COPY . .
 6 | RUN dotnet publish -c Release -o ./publish
 7 | EXPOSE 5000
 8 | ENTRYPOINT ["dotnet", "./publish/Server.dll"]

A.  Explica qué problema soluciona Docker.
B.  Elija tres líneas del Dockerfile anterior y explique en detalle qué utilidad tienen y qué hacen

esas instrucciones de Docker.

Ejercicio 2

El siguiente sistema permite a los usuarios subir URLs de vídeos los cuales van a ser convertidos a
distintas resoluciones y almacenados en el sistema.

Fecha: 30/06/2025

Evaluación: Parcial 1

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Matutino

Puntaje Máximo:     35 Puntos

Página 2 de 2

●  Nombre y describa brevemente qué patrón de mensajería y tipo de exchange se puede usar.
●  Explique una posible solución en caso que un servidor W fallé en procesar un vídeo.
●  En caso que el usuario final quiera saber lo antes posible cuando el procesamiento de su vídeo

finalizó:

○  Explique los cambios necesarios para lograrlo.
○  Realice un nuevo diagrama, incluya qué tecnologías utiliza y cómo.
○

Indique en cada canal quién actúa como cliente/servidor o productor/consumidor.

Nota: No es necesario escribir código.

Ejercicio 3
Dado el siguiente código:

bool cancelar = false;

List<string> usuarios = new List<string> { "Ana", "Luis", "María", "Juan" // 400 items

new Thread(() =>
{
    Console.ReadKey();
    cancelar = true;
}).Start();

void ProcesarUsuarios(List<string> lista)
{
    foreach (var usuario in lista)
    {
        new Thread(() =>
        {
            if (cancelar)
                return;

            string filename = $"{usuario}.txt";
            string contenido = $"Archivo generado para usuario {usuario}";
            File.WriteAllText(filename, contenido);
        }).Start();
    }
}

ProcesarUsuarios(usuarios);

A.  Modifique el código para que utilice y aproveche las ventajas de la clase Task y el modelo

asincrónico de async/await.

B.  Explique qué ventajas tiene utilizar la clase Task sobre Threads.

