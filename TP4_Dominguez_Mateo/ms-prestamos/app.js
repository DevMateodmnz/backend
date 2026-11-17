const express = require('express');

const app = express();
const PUERTO = 3001;

app.use(express.json());

app.post('/api/prestamos', async (req, res) => {
  const { id_libro, nombre_usuario } = req.body ?? {};

  if (id_libro === undefined || !nombre_usuario) {
    return res.status(400).json({
      error: 'Se requieren id_libro y nombre_usuario',
    });
  }

  try {
    const respuestaCatalogo = await fetch(
      `http://localhost:3000/api/libros/${encodeURIComponent(id_libro)}`
    );

    if (respuestaCatalogo.status === 404) {
      return res.status(400).json({
        error: 'El libro no está disponible',
      });
    }

    if (!respuestaCatalogo.ok) {
      return res.status(502).json({
        error: 'El servicio de catálogo respondió con un error',
      });
    }

    const libro = await respuestaCatalogo.json();
    return res.status(201).json({
      mensaje: 'Préstamo aprobado',
      id_libro: libro.id,
      titulo: libro.titulo,
      nombre_usuario,
    });
  } catch (error) {
    console.error('No se pudo contactar el servicio de catálogo:', error.message);
    return res.status(500).json({
      error: 'El servicio de catálogo no responde. Intente más tarde',
    });
  }
});

app.listen(PUERTO, () => {
  console.log(`Microservicio de préstamos escuchando en http://localhost:${PUERTO}`);
});
