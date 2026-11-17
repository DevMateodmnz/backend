const express = require('express');

const app = express();
const PUERTO = 3000;

// Datos locales de ejemplo para simular la base de datos del catálogo.
const libros = [
  { id: '1', titulo: 'Cien años de soledad', stock: 3 },
  { id: '2', titulo: 'El principito', stock: 2 },
  { id: '3', titulo: 'Rayuela', stock: 0 },
];

app.get('/api/libros/:id', (req, res) => {
  const libro = libros.find((item) => item.id === req.params.id);

  if (!libro || libro.stock <= 0) {
    return res.status(404).json({
      error: 'El libro no existe o no tiene stock disponible',
    });
  }

  return res.status(200).json(libro);
});

app.listen(PUERTO, () => {
  console.log(`Microservicio de catálogo escuchando en http://localhost:${PUERTO}`);
});
