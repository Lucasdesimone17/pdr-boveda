---
date: 2026-09-22
type: decision
tags:
  - pdr
  - obligatorio-1
  - clean-code
  - decision
ai-first: true
confidence: verified
fuente: "Decision del usuario el 2026-09-22 sobre la rama feature/pulido-final"
---

# Decision: el codigo del Obligatorio 1 va sin comentarios

## For future agent
El 2026-09-22 se borraron **todos** los comentarios del codigo C# del Obligatorio 1, en la rama `feature/pulido-final`. Antes de tocar ese proyecto: **no agregar comentarios**. Si hace falta explicar algo, va en el mensaje de commit, en la documentacion entregable o en esta boveda, nunca en el `.cs`. Esta nota guarda el texto exacto de lo que se borro, por si alguna vez se quiere recuperar algo.

## Que se decidio

Cero comentarios en los archivos `.cs` de `Edison.Client`, `Edison.Server` y `Edison.Common`. Se borraron 31 lineas repartidas en 13 bloques sobre 11 archivos. Compila con 0 errores y 0 warnings, y el sistema se probo corriendo despues del borrado.

## Por que

1. **El proyecto no tenia un solo comentario.** Verificado con `git grep -nE "^\s*//" a1fdbf3 -- '*.cs'`: cero resultados en toda la rama `Develop`. Los comentarios agregados el 22-09 desentonaban con el estilo del grupo, y la consistencia pesa mas que el valor individual de cada comentario.
2. **La catedra evalua buenas practicas y diseno.** La postura de Clean Code que se ensena en la carrera es que el comentario suele tapar codigo que no se explica solo. Aunque un comentario de "por que" es defendible, discutirlo en la defensa cuesta mas de lo que aporta.
3. **Decision explicita del usuario**, con el criterio de "borralos todos por si acaso, de ultima los agregamos despues". El costo de volver a ponerlos es bajo y esta nota lo hace trivial.

El contraargumento que existe y no se tomo: cuatro de esos comentarios explicaban restricciones externas que no se pueden expresar con un nombre de metodo, como que el formato de persistencia no usa JSON porque **la letra lo prohibe**. Ese contenido **no se perdio**: vive en los mensajes de commit de `feature/pulido-final` y tiene que entrar en la documentacion entregable, en el punto de decisiones de diseno.

## Que se borro, textual

**`Edison.Client/VehicleClient.cs`**, en `Save`:
> La imagen es opcional: si falla, se sigue igual con el resto de los datos en vez de tirar abajo el alta entera y obligar a cargar todo el formulario de nuevo.

**`Edison.Server/BusinessLogic/VehicleService.cs`**, en `SetImage`:
> Cambiar solo la imagen no es una modificacion del modelo: no se registra actividad, para que publicar con imagen no deje dos entradas seguidas en el historial. Devuelve la imagen anterior, que el llamador tiene que borrar del disco.

**`Edison.Server/BusinessLogic/VehicleService.cs`**, en `Search`:
> Consulta de solo lectura: no toma el lock de inventario. El repositorio devuelve una copia de su lista bajo su propio lock, asi que el filtrado y el orden se hacen afuera.

**`Edison.Server/Networking/VehicleRequestHandler.cs`**, en `Handle`:
> El detalle y la busqueda son de solo lectura, asi que no entran al lock de inventario: una busqueda larga no tiene por que frenar a los demas clientes. Cada repositorio ya protege su propia lista, y el peor caso es leer un stock que cambio recien.

**`Edison.Server/Domain/Entities/Reservation.cs`**, en `Restore`:
> Usado solo al levantar el estado guardado en disco: rearma el estado y la fecha tal cual estaban, sin pasar por las transiciones, que no aceptarian un estado terminal como origen.

**`Edison.Server/DataAccess/StateStore.cs`**, en la clase:
> Persistencia simple pedida en clase: el servidor lee un archivo al levantar y lo escribe al cerrar. El formato es el mismo campo-con-largo-adelante que usa el protocolo, con DataBuilder y DataReader, para no depender de ningun serializador existente, que la letra prohibe.

**`Edison.Server/DataAccess/StateStore.cs`**, en `Save`:
> Se escribe a un temporal y recien despues se reemplaza, para no dejar el archivo a medio escribir si el proceso se corta justo en el cierre.

**`Edison.Server/DataAccess/StateStore.cs`**, en `Load`:
> Devuelve false si todavia no hay archivo, que es el caso de la primera corrida.

**`Edison.Server/DataAccess/StateStore.cs`**, en `ApplyReservedUnits`:
> Las unidades reservadas no se guardan: se recalculan contando las reservas que consumen stock. Asi el inventario nunca puede quedar desincronizado con las reservas al levantar.

**`Edison.Server/DataAccess/SeedData.cs`**, en la clase:
> Datos de prueba de la primera corrida, cuando todavia no hay archivo de estado. La letra los marca como opcionales; sirven para no tener que cargar todo a mano antes de mostrar el sistema.

**`Edison.Server/DataAccess/UserRepository.cs`** y **`VehicleRepository.cs`**, en `Load`:
> Reemplaza el contenido con el estado levantado del archivo. Se llama una sola vez, al arrancar, antes de que haya clientes conectados.

**`Edison.Server/DataAccess/ReservationRepository.cs`**, en `Load`:
> Reemplaza el contenido con el estado levantado del archivo, conservando los numeros de reserva que ya vio el usuario y siguiendo la numeracion a partir del mayor.

**`Edison.Server/DataAccess/ActivityLogRepository.cs`**, en `Load`:
> Reemplaza el contenido con el estado levantado del archivo y sigue la numeracion a partir del mayor.

**`Edison.Server/Program.cs`**, en `LoadState`:
> Un archivo ilegible no puede impedir que el servidor arranque, pero tampoco se pisa: se aparta con otro nombre para poder mirarlo despues.

## Como recuperarlos

El borrado es un commit propio en `feature/pulido-final`. Para ver los comentarios en su lugar original alcanza con `git show <commit>` de ese commit, o `git revert` si se los quiere de vuelta todos. Esta nota sirve para reponer solo alguno a mano.

## Connections
- [[Estado del Obligatorio 1 - 2026-09-22]] - el estado del proyecto en ese momento (superado; as of 2026-09-27 el vigente es [[Estado del Obligatorio 1 - 2026-09-27]])
- [[Decisiones de la capa de protocolo - Obligatorio 1]] - el otro bloque de decisiones que va a la documentacion
- [[Programacion de Redes 2026]] - proyecto del cursado
