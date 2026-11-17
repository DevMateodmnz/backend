# TP4: Arquitectura de Microservicios

El proyecto contiene dos aplicaciones Node.js independientes. Cada una tiene su propio `package.json` y debe ejecutarse en una terminal distinta.

## Iniciar los servicios

Terminal 1:

```bash
cd ms-catalogo
npm install
npm start
```

Terminal 2:

```bash
cd ms-prestamos
npm install
npm start
```

El catálogo escucha en `http://localhost:3000` y préstamos en `http://localhost:3001`.

## Probar las rutas

Consultar un libro disponible:

```bash
curl http://localhost:3000/api/libros/1
```

Solicitar un préstamo aprobado (HTTP 201):

```bash
curl -i -X POST http://localhost:3001/api/prestamos \
  -H 'Content-Type: application/json' \
  -d '{"id_libro":"1","nombre_usuario":"Ana Pérez"}'
```

El libro `3` no tiene stock; la solicitud correspondiente devuelve HTTP 400:

```bash
curl -i -X POST http://localhost:3001/api/prestamos \
  -H 'Content-Type: application/json' \
  -d '{"id_libro":"3","nombre_usuario":"Ana Pérez"}'
```

Para comprobar la tolerancia a fallos, detén el servicio de catálogo con `Ctrl+C` y repite el POST a préstamos. Debe responder HTTP 500 con el mensaje `El servicio de catálogo no responde. Intente más tarde`. Luego puedes volver a iniciar el catálogo.
