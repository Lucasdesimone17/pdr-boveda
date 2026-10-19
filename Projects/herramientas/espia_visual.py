import socket
import threading
import datetime
import json
from http.server import BaseHTTPRequestHandler, ThreadingHTTPServer

LISTEN_PORT = 20001
SERVER = ("127.0.0.1", 20000)
WEB_PORT = 8765

COMMANDS = {
    1: "Register", 2: "Login", 3: "Logout",
    10: "CreateVehicle", 11: "ModifyVehicle", 12: "DeleteVehicle", 13: "SearchVehicles", 14: "GetVehicle",
    20: "UploadImage", 21: "DownloadImage",
    30: "RequestReservation", 31: "CancelReservation", 32: "GetReservations",
    40: "GetActivity", 99: "Error",
}

EXPLAIN = {
    1: "Registrarse: manda usuario y clave.",
    2: "Iniciar sesion: manda usuario y clave.",
    3: "Cerrar sesion: no lleva datos.",
    10: "Publicar un modelo: nombre, descripcion, precio, fecha y stock.",
    11: "Modificar un modelo: los mismos campos que publicar.",
    12: "Borrar un modelo: solo el nombre.",
    13: "Buscar: los 5 filtros y desde que posicion.",
    14: "Ver detalle de un modelo: el nombre.",
    20: "Subir imagen: modelo, nombre de archivo y tamano. Los bytes van aparte.",
    21: "Bajar imagen: el modelo.",
    30: "Pedir una reserva: el nombre del modelo.",
    31: "Cancelar una reserva: el numero.",
    32: "Ver mis reservas: desde que numero.",
    40: "Ver mi historial: desde que actividad.",
    99: "Error.",
}

events = []
events_lock = threading.Lock()


def add_event(e):
    with events_lock:
        e["id"] = len(events) + 1
        e["time"] = datetime.datetime.now().strftime("%H:%M:%S")
        events.append(e)


def fields(text):
    out = []
    i = 0
    while i + 4 <= len(text) and text[i:i + 4].isdigit():
        n = int(text[i:i + 4])
        out.append(text[i + 4:i + 4 + n])
        i += 4 + n
    return out, text[i:]


def recv_exact(sock, n):
    buf = b""
    while len(buf) < n:
        chunk = sock.recv(n - len(buf))
        if not chunk:
            raise ConnectionError
        buf += chunk
    return buf


def pipe(src, dst, conn_id, direction, state):
    try:
        while True:
            pending = state.pop(direction, 0)
            if pending:
                sent = 0
                while sent < pending:
                    chunk = src.recv(min(32768, pending - sent))
                    if not chunk:
                        raise ConnectionError
                    dst.sendall(chunk)
                    sent += len(chunk)
                add_event({"kind": "bytes", "client": conn_id, "dir": direction, "size": pending, "sock": state["sock"][direction]})
                continue
            fixed = recv_exact(src, 9)
            text = fixed.decode("utf-8", "replace")
            header, cmd, length = text[:3], int(text[3:5]), int(text[5:9])
            data_bytes = recv_exact(src, length) if length else b""
            dst.sendall(fixed + data_bytes)
            data = data_bytes.decode("utf-8", "replace")
            status = None
            body = data
            if header == "RES" and data:
                status = data[0]
                body = data[1:]
            parsed, rest = fields(body)
            add_event({
                "kind": "frame", "client": conn_id, "dir": direction, "sock": state["sock"][direction],
                "header": header, "cmd": cmd, "name": COMMANDS.get(cmd, "?"),
                "explain": EXPLAIN.get(cmd, ""), "length": length, "data": data[:600],
                "status": status, "fields": [p[:120] for p in parsed[:15]] if not rest else [],
                "text": body[:300] if rest else "",
            })
            if header == "REQ" and cmd == 20:
                f, _ = fields(data)
                state["upload_size"] = int(f[2]) if len(f) >= 3 else 0
            if header == "RES" and cmd == 20 and data == "1" and state.get("upload_size"):
                state["up"] = state.pop("upload_size")
            if header == "RES" and cmd == 21 and data.startswith("1"):
                f, _ = fields(data[1:])
                if len(f) >= 2:
                    state["down"] = int(f[1])
    except (ConnectionError, OSError, ValueError):
        pass
    finally:
        for s in (src, dst):
            try:
                s.shutdown(socket.SHUT_RDWR)
            except OSError:
                pass


def handle(client, conn_id):
    try:
        server = socket.create_connection(SERVER)
    except OSError:
        add_event({"kind": "info", "client": conn_id, "text": "No se pudo conectar con el servidor. Esta levantado?"})
        client.close()
        return
    ep = lambda a: f"{a[0]}:{a[1]}"
    cli, spy_in, spy_out, srv = ep(client.getpeername()), ep(client.getsockname()), ep(server.getsockname()), ep(server.getpeername())
    state = {"sock": {"up": {"from": cli, "to": srv, "via": spy_in}, "down": {"from": srv, "to": cli, "via": spy_in}}}
    add_event({"kind": "info", "client": conn_id, "text": f"Cliente conectado. Socket del cliente {cli} -> espia {spy_in} -> espia {spy_out} -> servidor {srv}. El servidor hizo Accept y le creo su propio hilo."})
    t1 = threading.Thread(target=pipe, args=(client, server, conn_id, "up", state), daemon=True)
    t2 = threading.Thread(target=pipe, args=(server, client, conn_id, "down", state), daemon=True)
    t1.start(); t2.start(); t1.join(); t2.join()
    client.close(); server.close()
    add_event({"kind": "info", "client": conn_id, "text": "Cliente desconectado. Su hilo en el servidor termino."})


def proxy():
    listener = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    listener.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    listener.bind(("127.0.0.1", LISTEN_PORT))
    listener.listen()
    n = 0
    while True:
        client, _ = listener.accept()
        n += 1
        threading.Thread(target=handle, args=(client, n), daemon=True).start()


PAGE = r"""<!doctype html><html lang="es"><head><meta charset="utf-8"><title>EDISON // NET SPY</title>
<meta name="viewport" content="width=device-width,initial-scale=1">
<style>
@import url('https://fonts.googleapis.com/css2?family=Orbitron:wght@500;700&family=Share+Tech+Mono&display=swap');
:root{--bg:#07060d;--card:#0e0c1a;--tx:#e6f7ff;--mut:#7f8bb0;--bd:#2a2350;--req:#ff2bd6;--reqbg:rgba(255,43,214,.12);--res:#00f0ff;--resbg:rgba(0,240,255,.12);--err:#ff3b3b;--errbg:rgba(255,59,59,.14);--h:#ffe600;--hbg:rgba(255,230,0,.12);--c:#00f0ff;--cbg:rgba(0,240,255,.12);--l:#ff2bd6;--lbg:rgba(255,43,214,.12);--d:#39ff14;--dbg:rgba(57,255,20,.10)}
*{box-sizing:border-box}
body{margin:0;background:var(--bg);color:var(--tx);font:15px/1.5 'Share Tech Mono',ui-monospace,Consolas,monospace;
background-image:linear-gradient(rgba(0,240,255,.05) 1px,transparent 1px),linear-gradient(90deg,rgba(255,43,214,.05) 1px,transparent 1px);background-size:40px 40px}
body::after{content:"";pointer-events:none;position:fixed;inset:0;background:repeating-linear-gradient(0deg,rgba(0,0,0,.25) 0 1px,transparent 1px 3px);z-index:9}
header{padding:18px 22px;border-bottom:1px solid var(--req);box-shadow:0 0 18px rgba(255,43,214,.35);display:flex;gap:18px;align-items:center;flex-wrap:wrap;background:rgba(14,12,26,.85)}
h1{font-family:Orbitron,sans-serif;font-size:22px;font-weight:700;margin:0;color:var(--res);text-shadow:0 0 6px var(--res),0 0 18px var(--res);letter-spacing:2px}
h1 b{color:var(--req);text-shadow:0 0 6px var(--req),0 0 18px var(--req)}
.mut{color:var(--mut);font-size:13px}
.lanes{display:grid;grid-template-columns:150px 1fr 150px;padding:0 22px;position:sticky;top:0;background:rgba(7,6,13,.92);border-bottom:1px solid var(--bd);z-index:3}
.lane{text-align:center;padding:10px 0;font-family:Orbitron,sans-serif;font-size:13px;letter-spacing:2px;color:var(--h);text-shadow:0 0 8px var(--h)}
main{padding:10px 22px 60px;max-width:1150px;margin:0 auto}
.ev{display:grid;grid-template-columns:150px 1fr 150px;align-items:center;margin:14px 0;animation:in .35s ease-out}
@keyframes in{from{opacity:0;transform:translateY(8px)}to{opacity:1;transform:none}}
.card{background:var(--card);border:1px solid var(--bd);border-radius:4px;padding:12px 14px;clip-path:polygon(0 0,calc(100% - 14px) 0,100% 14px,100% 100%,14px 100%,0 calc(100% - 14px))}
.card.up{border-color:var(--req);box-shadow:0 0 14px rgba(255,43,214,.35) inset}
.card.down{border-color:var(--res);box-shadow:0 0 14px rgba(0,240,255,.3) inset}
.card.bad{border-color:var(--err);box-shadow:0 0 14px rgba(255,59,59,.35) inset}
.arrow{font-size:13px;color:var(--mut);display:flex;align-items:center;gap:8px;margin-bottom:6px;flex-wrap:wrap}
.pill{font-size:12px;padding:2px 8px;border-radius:2px;border:1px solid currentColor}
.req{background:var(--reqbg);color:var(--req);text-shadow:0 0 6px var(--req)}.res{background:var(--resbg);color:var(--res);text-shadow:0 0 6px var(--res)}.bad{background:var(--errbg);color:var(--err)}
.frame{font-size:14px;display:flex;flex-wrap:wrap;gap:4px;margin:8px 0}
.seg{padding:4px 8px;border:1px solid currentColor;border-radius:2px;display:flex;flex-direction:column;align-items:center;max-width:100%}
.seg small{font-size:10px;opacity:.75;letter-spacing:1px;text-transform:uppercase}
.seg span{word-break:break-all;text-shadow:0 0 6px currentColor}
.sH{background:var(--hbg);color:var(--h)}.sC{background:var(--cbg);color:var(--c)}.sL{background:var(--lbg);color:var(--l)}.sD{background:var(--dbg);color:var(--d)}
.flds{font-size:13px;margin-top:6px;color:var(--mut)}.flds code{background:#000;border:1px solid var(--bd);color:var(--d);padding:1px 6px;margin:2px;display:inline-block;font-size:12px}
.info{text-align:center;grid-column:1/4;font-size:13px;color:var(--h);padding:7px;border:1px dashed var(--h);text-shadow:0 0 6px var(--h);background:rgba(255,230,0,.05)}
.sock{font-size:12px;color:var(--h);margin:2px 0 4px;letter-spacing:.5px}.sock b{color:var(--h);text-shadow:0 0 6px var(--h);font-weight:400}
.side{text-align:center;font-size:12px;color:var(--mut);letter-spacing:1px}
.empty{text-align:center;color:var(--res);padding:70px 20px;text-shadow:0 0 8px var(--res);animation:blink 1.6s infinite}
@keyframes blink{50%{opacity:.35}}
button{font:inherit;padding:6px 14px;border-radius:2px;border:1px solid var(--req);background:transparent;color:var(--req);cursor:pointer;text-shadow:0 0 6px var(--req);letter-spacing:1px}
button:hover{background:var(--reqbg);box-shadow:0 0 12px var(--req)}
</style></head><body>
<header><h1>EDISON <b>//</b> NET SPY</h1><span class="mut">Cada mensaje que viaja por el socket, en vivo. El cliente de prueba se conecta al espia (puerto 20001) y el espia reenvia al servidor (20000).</span>
<button onclick="fetch('/clear').then(()=>{shown=0;document.getElementById('list').innerHTML='';empty()})">[ LIMPIAR ]</button></header>
<div class="lanes"><div class="lane">Cliente</div><div class="lane mut" style="font-weight:400">
<span class="pill sH">HEADER 3</span> <span class="pill sC">CMD 2</span> <span class="pill sL">LARGO 4</span> <span class="pill sD">DATOS</span></div><div class="lane">Servidor</div></div>
<main id="list"></main>
<script>
let shown=0;
function esc(s){return String(s).replace(/[&<>"]/g,c=>({'&':'&amp;','<':'&lt;','>':'&gt;','"':'&quot;'}[c]))}
function empty(){if(!shown)document.getElementById('list').innerHTML='<div class="empty">&gt; ESPERANDO TRAFICO EN EL SOCKET_  usa el cliente de prueba</div>'}
function sk(e){return e.sock?'<div class="sock">socket <b>'+esc(e.sock.from)+'</b> ⟶ <b>'+esc(e.sock.to)+'</b> <span class="mut">(pasa por el espía '+esc(e.sock.via)+')</span></div>':''}
function render(e){
 const el=document.createElement('div');el.className='ev';
 if(e.kind==='info'){el.innerHTML='<div class="info">'+esc(e.time)+' · cliente '+e.client+' · '+esc(e.text)+'</div>';return el}
 const up=e.dir==='up';
 const left=up?'<div class="side">cliente '+e.client+'</div>':'<div></div>';
 const right=up?'<div></div>':'<div class="side">hilo del cliente '+e.client+'</div>';
 let inner;
 if(e.kind==='bytes'){
  inner='<div class="arrow">'+(up?'Cliente → Servidor':'Servidor → Cliente')+' · '+esc(e.time)+'</div><b>'+e.size.toLocaleString()+' bytes crudos de imagen</b>'+sk(e)+'<div class="mut">Viajan fuera del formato de trama, en bloques de 32 KB.</div>';
 } else {
  const cmd=String(e.cmd).padStart(2,'0'),len=String(e.length).padStart(4,'0');
  let tag=e.header==='REQ'?'<span class="pill req">REQ · pedido</span>':(e.status==='0'?'<span class="pill bad">RES · error</span>':'<span class="pill res">RES · respuesta OK</span>');
  let data=e.data.length>140?e.data.slice(0,140)+'…':e.data;
  inner='<div class="arrow">'+(up?'Cliente → Servidor':'Servidor → Cliente')+' · '+esc(e.time)+' '+tag+' <b style="color:var(--tx)">'+esc(e.name)+'</b></div>'+sk(e)+
  '<div class="frame"><div class="seg sH"><small>header</small><span>'+e.header+'</span></div><div class="seg sC"><small>cmd</small><span>'+cmd+'</span></div><div class="seg sL"><small>largo</small><span>'+len+'</span></div>'+
  (e.length?'<div class="seg sD"><small>datos ('+e.length+' bytes)</small><span>'+esc(data)+'</span></div>':'<div class="seg sD"><small>datos</small><span>vacío</span></div>')+'</div>';
  let parts=[];
  if(e.header==='REQ'&&e.explain)parts.push('<div class="mut">'+esc(e.explain)+'</div>');
  if(e.status!==null&&e.status!==undefined)parts.push('<div class="flds">Primer carácter de DATOS: <code>'+e.status+'</code> = '+(e.status==='1'?'salió bien':'hubo un error')+'</div>');
  if(e.fields.length)parts.push('<div class="flds">Campos (largo de 4 dígitos + contenido): '+e.fields.map(f=>'<code>'+esc(f===''?'(vacío)':f)+'</code>').join('')+'</div>');
  else if(e.text)parts.push('<div class="flds">Mensaje: <code>'+esc(e.text)+'</code></div>');
  inner+=parts.join('');
 }
 el.innerHTML=left+'<div class="card '+(e.status==="0"?'bad':(up?'up':'down'))+'" style="'+(up?'margin-right:40px':'margin-left:40px')+'">'+inner+'</div>'+right;
 return el;
}
async function poll(){
 try{const r=await fetch('/events?after='+shown);const list=await r.json();
  if(list.length){const box=document.getElementById('list');if(!shown)box.innerHTML='';
   list.forEach(e=>{box.appendChild(render(e));shown=e.id});window.scrollTo({top:document.body.scrollHeight,behavior:'smooth'})}}catch(_){}
 setTimeout(poll,600)}
empty();poll();
</script></body></html>"""


class Web(BaseHTTPRequestHandler):
    def log_message(self, *args):
        pass

    def do_GET(self):
        if self.path.startswith("/events"):
            after = int(self.path.split("after=")[-1]) if "after=" in self.path else 0
            with events_lock:
                body = json.dumps([e for e in events if e["id"] > after]).encode()
            self.send_response(200)
            self.send_header("Content-Type", "application/json")
            self.end_headers()
            self.wfile.write(body)
        elif self.path.startswith("/clear"):
            with events_lock:
                events.clear()
            self.send_response(200)
            self.end_headers()
        else:
            body = PAGE.encode("utf-8")
            self.send_response(200)
            self.send_header("Content-Type", "text/html; charset=utf-8")
            self.end_headers()
            self.wfile.write(body)


if __name__ == "__main__":
    threading.Thread(target=proxy, daemon=True).start()
    print(f"Espia en 127.0.0.1:{LISTEN_PORT} -> {SERVER[0]}:{SERVER[1]}")
    print(f"Interfaz: http://127.0.0.1:{WEB_PORT}", flush=True)
    ThreadingHTTPServer(("127.0.0.1", WEB_PORT), Web).serve_forever()
