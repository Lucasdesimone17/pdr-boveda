Fecha: 30/06/2025

Evaluación: Parcial 1

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Nocturno

Puntaje Máximo:     35 Puntos

Página 1 de 5

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

Fecha: 30/06/2025

Evaluación: Parcial 1

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Nocturno

Puntaje Máximo:     35 Puntos

Página 2 de 5

Ejercicio 2

Dado el siguiente código:

bool cancelar = false;

List<string> ciudades = new List<string> { "Montevideo", "Buenos Aires", "Madrid",
"Tokio" };

new Thread(() =>
{
    Console.ReadKey();
    cancelar = true;
}).Start();

void ProcesarCiudades(List<string> lista)
{
    foreach (var ciudad in lista)
    {
        new Thread(() =>
        {
            if (cancelar)
                return;

            string filename = $"{ciudad}.txt";
            string contenido = $"Archivo generado para la ciudad {ciudad}";
            File.WriteAllText(filename, contenido);
        }).Start();
    }
}

ProcesarCiudades(ciudades);

A.  Modifique el código para que utilice y aproveche las ventajas de la clase Task y el modelo

asincrónico de async/await.

Fecha: 30/06/2025

Evaluación: Parcial 1

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Nocturno

Puntaje Máximo:     35 Puntos

Página 3 de 5

Ejercicio 3

Una empresa de infraestructura energética lo ha contratado para diseñar el sistema de una nueva
aplicación llamada “CargaYa”, que permite a los usuarios consultar el estado de los cargadores
eléctricos públicos disponibles en su ciudad.

El sistema debe cumplir con los siguientes requerimientos:

●  Servidor Web: Permite que los usuarios visualicen en un mapa los cargadores eléctricos, su

ubicación y su estado (operativo, en uso, fuera de servicio).

●  Servidor de Supervisión: Recibe actualizaciones en tiempo real sobre el estado de los

cargadores, ya sea de forma automática desde sensores o mediante reporte manual de
operadores. Si un cargador presenta fallas repetidas, se genera una alerta para mantenimiento.

●  Servidor Estadístico: Recibe datos en tiempo real sobre el uso de los cargadores (inicio y fin de

carga, duración, usuario anónimo, ubicación) con fines de análisis y planificación de
infraestructura.

Debe utilizar tres de las siguientes tecnologías: Sockets, MOM, gRPC, API REST. Justifique su elección
e implemente:

●  gRPC: Escriba el archivo .proto que describe los servicios y mensajes necesarios para actualizar

el estado de los cargadores desde dispositivos de campo.

●  Sockets: Proporcione un diagrama del protocolo de comunicación utilizado para el envío de

notificaciones de eventos (por ejemplo, "nuevo cargador disponible", "cargador en
mantenimiento") hacia los clientes conectados.

●  MOM: Defina las colas utilizadas para distribuir los eventos de uso y mantenimiento entre
distintos componentes. Indique el tipo de exchange, las colas y los mensajes enviados (en
formato JSON o XML).

●  API REST: Enumere los endpoints necesarios para que los usuarios consulten los cargadores,

filtren por estado o zona, y reporten problemas. Incluya los datos enviados y sus respuestas con
códigos de estado HTTP.

Además, realice lo siguiente:

●  Diagrame la solución mostrando los canales de comunicación y los componentes involucrados.

●  Indique en cada canal quién actúa como cliente/servidor o productor/consumidor.

Fecha: 30/06/2025

Evaluación: Parcial 1

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Nocturno

Puntaje Máximo:     35 Puntos

Página 4 de 5

Aclaraciones:

●  Puede realizar suposiciones razonables sobre los datos a enviar y recibir (por ejemplo, estado

del cargador, coordenadas GPS, fecha/hora).

●  Puede utilizar más de una tecnología para un mismo componente si lo considera necesario.

Fecha: 30/06/2025

Evaluación: Parcial 1

Duración: 2 horas

Uso de Calculadora:    NO

Materia: Programación de redes

Uso de Material:     NO

Turno: Nocturno

Puntaje Máximo:     35 Puntos

Página 5 de 5

