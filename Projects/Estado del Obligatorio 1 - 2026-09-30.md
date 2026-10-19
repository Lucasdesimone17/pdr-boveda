---
date: 2026-09-30
type: status-report
tags:
  - pdr
  - obligatorio-1
  - estado
  - defensa
ai-first: true
confidence: verified
commit: 0756597
fuente: "Sesion de Claude Code del 2026-09-28 al 2026-09-30: verificacion directa de origin/main, del zip del Release v1.1 y de los PDF"
relations:
  depends_on:
    - "[[Estado del Obligatorio 1 - 2026-09-27]]"
  relates_to:
    - "[[2026-09-28 - Obligatorio 1 EDISON]]"
    - "[[Estudio para la defensa del Obligatorio 1]]"
---

# Estado del Obligatorio 1 - EDISON (30-09)

## For future agent
Estado despues de la entrega del 28-09 y antes de la defensa del **jueves 01-10-2026**. **Reemplaza a [[Estado del Obligatorio 1 - 2026-09-27]]**. Lo que se hizo en la sesion esta en [[2026-09-28 - Obligatorio 1 EDISON]]; lo que se estudio, en [[Estudio para la defensa del Obligatorio 1]].

## Codigo y Release (as of 2026-09-30)
- `main` en **`0756597`** (PR #32, rama `feature/usabilidad-modificar`, commit `d31d7f5` de Lucas). Antes: `95e6370` (PR #31, Santiago subio `Documentacion/EDISON - Documentacion Obligatorio 1.pdf`).
- Tags: `v1.0` sobre `95e6370` (Santiago, 28-09 15:06, **sin** los cambios de usabilidad) y **`v1.1` sobre `0756597`** (Lucas, 28-09). **El Release de la defensa es `v1.1`.** Su "Source code (zip)" trae codigo, `bin/Release/win-x64` y `net8.0` de cliente y servidor, y el PDF.
- Verificado: el `Edison.Client.dll` win-x64 del zip `v1.1` tiene el mismo hash que el compilado probado (`dc7de84c...`).
- **Regla de la letra: despues del hito de entrega `main` no se toca.** Cualquier cambio va a otra rama.

## Documentacion (as of 2026-09-30)
- El PDF valido es el de Santiago: **19 paginas**, exportado desde Google Docs, **punto 9 (uso de IA) completo**, link al repo en la portada. Esta en `main`, en el Release `v1.1` y descomprimido en `Escritorio\EDISON-v1.1\[repo-del-grupo]-1.1\Documentacion\`.
- El PDF de 17 paginas de `Entrega Obligatorio 1\` **esta viejo**: tiene `[COMPLETAR]` en el punto 9. Comparados palabra por palabra, la unica diferencia de contenido es el punto 9; lo demas es formato (Google Docs repite encabezados de tabla).
- Entrega por Gestion: la letra pide un PDF con la documentacion y el link al repo en la primera pagina. Se le paso al usuario el PDF de 19 paginas para subir. **Confirmacion de que se subio: TBD.**
- Riesgo de defensa: el punto 9 dice que el sistema "se valido ejecutandolo en dos maquinas del laboratorio". El 28-09 se probo en dos PC de la casa de Lucas, no del laboratorio.

## Prueba en dos PC (as of 2026-09-28)
- Servidor del Release en `0.0.0.0:20000` en la PC de Lucas (`192.168.68.52`, Wi-Fi); cliente en otra PC (`192.168.68.51`). Conexion confirmada con `Get-NetTCPConnection`, y un usuario `lucas` registrado desde la otra PC fue visto por el servidor.
- En el laboratorio: PC cableadas (la wifi tiene microsegmentacion), `ipconfig`, `0.0.0.0` en el server, IP del server en el cliente, Permitir en el firewall. El repo es privado: bajar el Release pide login de GitHub con 2FA. Plan B: zip en pendrive.

## Pendientes
- [ ] Confirmar la subida a Gestion (el 28-09 cerraba a las 21:00). TBD.
- [ ] Estar anotados en la planilla de defensa de Teams (jueves 01-10, respaldo martes 06-10).
- [ ] Ensayar en el laboratorio si hay oportunidad.
- [ ] Carpeta colgada `.git\worktrees\edison-main-wt` en el clon de Lucas: Windows no dejo borrarla. Inofensiva; borrar a mano.
