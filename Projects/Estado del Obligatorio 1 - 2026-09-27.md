---
date: 2026-09-27
type: status-report
tags:
  - pdr
  - obligatorio-1
  - estado
ai-first: true
confidence: verified
commit: 8e80066
fuente: "Verificacion directa de main en 8e80066, de la carpeta Entrega Obligatorio 1 y del PDF generado, el 2026-09-27"
---

# Estado del Obligatorio 1 - EDISON (27-09)

## For future agent
Estado al cierre del **2026-09-27**, un dia antes de la entrega del 28-09 a las 21:00. **Reemplaza a [[Estado del Obligatorio 1 - 2026-09-24]]**. El detalle de como se llego aca esta en [[2026-09-27 - Obligatorio 1 EDISON]].

## Codigo (as of 2026-09-27)
- `main` en **`8e80066`** (PR #29) a la tarde; a la noche en **`3616c86`** (PR #30, parche de defensa `e269b13`). Los diez requerimientos de cliente y los cuatro de servidor estan implementados.
- Compilados Release en el repo: `bin/Release/win-x64` (autocontenido, corre en Windows sin .NET instalado) y `bin/Release/net8.0` (necesita el runtime de .NET 8), para servidor y cliente.
- Arreglos del 27-09 (commit `c470b57`, PR #28): una cabecera con bytes invalidos ya no tira el servidor, cualquier excepcion inesperada cierra solo esa conexion, el precio con separador de miles se rechaza y una carpeta de descarga invalida ya no cierra el cliente.
- Probado en dos maquinas cambiando solo los `.dll.config` (dicho por el usuario el 27-09).
- Repo privado, con los tres integrantes como admin (verificado el 27-09).
- Menores conocidos y **no corregidos**, para tener preparados en la defensa: el servidor no hace `Shutdown` antes de cerrar el socket de un cliente; cuando el servidor cierra, el cliente muestra "La operación se completó correctamente"; se puede crear un modelo con stock 0; los errores de estado muestran el enum en ingles; la maquina de estados de la sesion es implicita (un unico loop que chequea `_username`); hay I/O de disco dentro del lock global. **Actualizacion (as of 2026-09-27, noche):** los cuatro primeros quedan corregidos en `fix/pulido-defensa` (`e269b13`) cuando se mergee a `main`; la sesion implicita y el I/O en el lock siguen igual.

## Documentacion (as of 2026-09-27)
- PDF de **22 paginas** a la tarde; **17 paginas** a la noche, despues del recorte (ver [[Decision - formato y voz de la documentacion del Obligatorio 1]] punto 8). Formato de la entrega de DA2 del usuario: portada ORT con integrantes, docente y link al repo, indice automatico, encabezado y pie grises, Georgia justificada.
- Secciones: 1 Alcance, 2 Errores conocidos, 3 Supuestos, 4 Arquitectura, 5 Diseno detallado, 6 Protocolo, 7 Decisiones de diseno, 8 Configuracion y ejecucion, 9 Uso de IA.
- Siete diagramas UML estilo Astah en PlantUML: paquetes, hilos, clases del patron State, estados, dos secuencias y despliegue. **As of 2026-09-27 noche: seis**, se saco el de clases del patron State porque repetia el de estados (respaldo en `Entrega Obligatorio 1_respaldodiagrama-3-patron-state-quitado`).
- **Pendiente: el punto 9 tiene un `[COMPLETAR]`**. Lo redacta el grupo con las herramientas y el contexto de uso. La letra exige esa cita.
- Todo en `[ruta local]`. El `.docx` y el `.pdf` se generan desde `DOCUMENTACION.md` con la skill `documentacion-entrega`; el `LEEME.txt` tiene los comandos.

## Auditoria de cierre (as of 2026-09-27, noche)
Workflow de 37 agentes con verificacion adversarial, detalle en [[Auditoria de cierre del Obligatorio 1 - 2026-09-27]]. Codigo, binarios commiteados (mismo IL que un build fresco) y prueba real con dos clientes: **OK**. Lo que falta es de entrega y logistica. Suma a la lista de abajo: inscripcion en Gestion, planilla de Teams, ensayo en laboratorio, correcciones factuales de la docu y un parche candidato de defensa (despues aplicado en `fix/pulido-defensa`, commit `e269b13`, todavia no en `main`).

## Lo que falta para entregar
- [ ] Redactar el punto 9 (uso de IA) y regenerar el PDF y el zip.
- [x] Correcciones factuales de la docu aplicadas el 2026-09-27 (noche), verificadas por dos agentes contra `main` `3616c86`, PDF de 21 paginas y zip rehechos, `LEEME.txt` al dia. Respaldo previo en `Entrega Obligatorio 1\_respaldo\2026-09-27-antes-correcciones-auditoria`. Tambien se arreglo `generar_docx.py` de la skill `documentacion-entrega`: los estilos del indice nunca se aplicaban (Word los renombraba a `TOC 11`); ahora el indice sale en Arial y entra en una pagina. Detalle original de la lista: corregir fila 99 del catalogo, 8.2 paso 3, reserva sin unidades como supuesto, y actualizar `LEEME.txt` (sigue diciendo fix sin commitear y `main` en `57bd085`).
- [x] Decidir si se aplica el parche candidato de defensa: **aplicado** el 2026-09-27 en la rama local `fix/pulido-defensa` (desde `main` `8e80066`), 4 `.cs` mas `Edison.Server.dll/.pdb` y `Edison.Client.dll/.pdb` recompilados en las 4 carpetas `bin/Release`. Verificado por dos agentes independientes: IL de las 4 DLL igual a un build fresco del fuente parchado, `Edison.Common` intacto, y prueba real con dos clientes (stock 0 rechazado, "reserva rechazada/cancelada" en espanol, "Se perdió la conexión con el servidor." al hacer `salir`, sin regresiones).
- [x] Commit `e269b13` en `fix/pulido-defensa`, pusheado a origin el 2026-09-27 (autor Lucas, sin coautor). Falta: PR a `main` (o recrear `Develop` y pasar por ahi). Recien despues el Release.
- [x] Con el parche, la docu cambia: hecho, supuesto 3 con stock inicial mayor a cero y 7.2 con el mensaje del cliente al caer el server. `main` quedo en **`3616c86`** (PR #30, mergeado por el usuario directo a `main` el 2026-09-27).
- [ ] Cada integrante confirma que figura **Inscripto** en la evaluacion de Gestion.
- [ ] Release de GitHub sobre `main` con el PDF adjunto; bajarlo y probarlo. Despues, no tocar `main`.
- [ ] Subir `Entrega Gestion\EDISON - Obligatorio 1 - [repo-del-grupo].zip` a Gestion antes del **28-09 a las 21:00**, temprano.
- [ ] Anotarse en la **planilla de defensa de Teams** (va todo el grupo).
- [ ] Ensayar el Release en dos PC cableadas del laboratorio el martes 29-09: `0.0.0.0` en `Edison.Server.dll.config`, IP del server en el cliente, firewall/SmartScreen, login de GitHub (repo privado).
- [ ] [[Santiago Louro]] verifica `[email]` en su cuenta de GitHub.
- [ ] Despues del Release: recrear `Develop` desde `main`.
- [ ] Ingestar la clase del 22-09 (sin acta).
- [ ] Preparar la defensa del **jueves 01-10** (ver `CRITICAL_FACTS.md`): cada integrante tiene que poder explicar toda la solucion.

## Connections
- [[Auditoria de cierre del Obligatorio 1 - 2026-09-27]] - auditoria posterior, con hallazgos verificados y el parche candidato
- [[Paula Poli]] · [[Santiago Louro]] - el resto del grupo
- [[2026-09-27 - Obligatorio 1 EDISON]] - la sesion que produjo este estado
- [[Decision - formato y voz de la documentacion del Obligatorio 1]]
- [[Estado del Obligatorio 1 - 2026-09-24]] - el informe que este reemplaza
- [[Obligatorio 1 - Sistema de reservas de vehiculos electricos]] · [[Programacion de Redes 2026]]
