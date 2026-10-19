---
date: 2026-09-24
type: status-report
tags:
  - pdr
  - obligatorio-1
  - estado
ai-first: true
confidence: verified
commit: 3660c71
fuente: "Verificacion directa del arbol de Develop en 3660c71 y de la carpeta de entrega, el 2026-09-24"
relations:
  superseded_by:
    - "[[Estado del Obligatorio 1 - 2026-09-27]]"
---

# Estado del Obligatorio 1 - EDISON (24-09)

## For future agent
> **Reemplazado el 2026-09-27 por [[Estado del Obligatorio 1 - 2026-09-27]]**, que es el estado vigente del Obligatorio 1.

Estado al **2026-09-24**, a **cuatro dias** de la entrega del 28-09. **Reemplaza a [[Estado del Obligatorio 1 - 2026-09-22]]**. No es una auditoria nueva con agentes: es verificacion directa del arbol en `3660c71` contra los hallazgos que quedaron abiertos el 22-09, mas el estado de la carpeta de entrega.

Entre el 22-09 y hoy no se registro nada en la boveda, aunque si hubo trabajo: el PR #25 y toda la documentacion. Esta nota cubre ese hueco.

## Lo que se cerro desde el 22-09

Todo por el **PR #25, `feature/pulido-final`**, que dejo `Develop` en `3660c71`:

- **El lock global ya no envuelve las lecturas.** `VehicleRequestHandler.Handle` ahora deriva `GetVehicle` y `SearchVehicles` directo a `Execute`, sin pasar por `_inventory.Execute`. Cierra el hallazgo de SR2 (`f247876`).
- **Codigo muerto borrado.** `Edison.Common/FileStreamHelper.cs` eliminado y `Protocol.CalculateParts` tambien (`e607e22`). Ya no hay nada sin llamador de lo que se habia marcado.
- **El `.gitignore` ya permite los compilados en Release** (`cf7d49a`), con una seccion comentada que explica por que.
- **Persistencia y datos de prueba.** Aparecieron `StateStore.cs` (175 lineas) y `SeedData.cs`, mas el guardado al cerrar en `Program.cs`. Eran los dos opcionales del plan (`e58610c`).
- **Se frena el procesador antes de guardar** y las consultas se toman en un instante consistente (`62a9ec8`).
- **Comentarios quitados del codigo** (`bab5d2c`), ver [[Decision - sin comentarios en el codigo del Obligatorio 1]].

**La documentacion existe.** En `[ruta local]` hay `DOCUMENTACION.md` (38 KB), el `.docx`, el `.pdf`, una carpeta `diagramas` y un `LEEME.txt`, todos del 22-09. Fuera del repo a proposito: se entrega por Gestion, no se commitea. La letra pide la documentacion en el Release de GitHub, y eso se resuelve **adjuntando el PDF al Release**, que no es commitearlo al arbol.

## Lo que sigue abierto

1. **Critico. Los compilados siguen sin estar en el repositorio.** El `.gitignore` ya los permite, pero `git ls-files` da **cero archivos bajo `bin/Release`**, y `Edison.Client/bin/` y `Edison.Server/bin/` figuran como **no trackeados**. O sea: el arreglo del ignore se hizo y el paso de agregarlos quedo sin dar. Es requisito textual de la letra y del punto 6 de la entrega.
2. **Alto. `GetReservations` y `GetActivity` siguen paginando con un numero fijo.** `ReservationRequestHandler.cs:49` usa `Take(20)` y `ActivityRequestHandler.cs:28` usa `Take(10)`, sin medir el tamano codificado. Con detalles o descripciones largas la pagina puede pasar los 9999 bytes de `DATOS` y la trama revienta, tirando la conexion. `VehicleRequestHandler` si lo hace bien: mide con `Protocol.Encoding.GetByteCount` y recorta. Sigue igual que el 22-09.
3. **Probar en dos maquinas** del laboratorio cambiando solo los `App.config`, sin recompilar. Es literalmente lo que se corre en la defensa y no consta que se haya hecho.
4. **Armar el Release de GitHub** con fuente, compilados y documentacion, y despues no tocar `main`.

## Plan de los cuatro dias

- [ ] **Imprescindible.** Agregar los compilados de Release al repositorio. El ignore ya esta listo, falta el `git add` y el commit, en una rama, no en `Develop`.
- [ ] **Imprescindible.** Acotar `GetReservations` y `GetActivity` por bytes, copiando el patron de `VehicleRequestHandler`.
- [ ] **Imprescindible.** Probar en dos maquinas con Windows, solo cambiando `App.config`.
- [ ] **Imprescindible.** Release de GitHub con fuente, compilados y el PDF de documentacion adjunto.
- [ ] Repasar que todos los integrantes puedan explicar toda la solucion: la defensa es individual y eliminatoria.

## Reglas de trabajo vigentes
- Nunca commitear ni pushear directo a `Develop` ni a `main`: rama de trabajo y pull request.
- Sin comentarios en los `.cs`; el porque va al mensaje de commit y a la documentacion.
- La documentacion nunca se commitea al arbol.

## Connections
- [[Estado del Obligatorio 1 - 2026-09-22]] - el informe que este reemplaza
- [[Estado del Obligatorio 1 - 2026-09-21]] - el primero, ya obsoleto
- [[Decisiones de la capa de protocolo - Obligatorio 1]] · [[Decision - sin comentarios en el codigo del Obligatorio 1]]
- [[2026-09-21 - Obligatorio 1 EDISON]] · [[Programacion de Redes 2026]]
- [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]]
