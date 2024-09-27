using Tarea_02_c.Models;

namespace Tarea_02_c.Infraestructure.Data
{
    public class LibroRespository : ILibroRepository
    {

        private IDictionary<String, Libro> _libros;

        public LibroRespository()
        {
            _libros = new Dictionary<String, Libro>();
            Libro l1 = new Libro("Libro 1", "Autor 1", "ISB1");
            Libro l2 = new Libro("Libro 2", "Autor 2", "ISB2");
            Libro l3 = new Libro("Libro 3", "Autor 1", "ISB3");
            Libro l4 = new Libro("Libro 4", "Autor 3", "ISB4");
            Libro l5 = new Libro("Libro 5", "Autor 4", "ISB5");
            this._libros.Add(l1.Titulo, l1);
            this._libros.Add(l2.Titulo, l2);
            this._libros.Add(l3.Titulo, l3);
            this._libros.Add(l4.Titulo, l4);
            this._libros.Add(l5.Titulo, l5);

        }

        public void Add(Libro libro)
        {
            if (_libros.ContainsKey(libro.Titulo))
            {
                throw new InvalidOperationException($"Ya existe un libro con el título '{libro.Titulo}'.");
            }
            this._libros.Add(libro.Titulo, libro);
        }

        public void Delete(string titulo)
        {
            if (!_libros.ContainsKey(titulo))
            {
                throw new KeyNotFoundException($"No se encontró un libro con el título '{titulo}'.");
            }
            this._libros.Remove(titulo);
        }

        public Libro Get(string titulo)
        {
            if (_libros.TryGetValue(titulo, out Libro respuesta))
            {
                return respuesta;
            }

            throw new KeyNotFoundException($"No se encontró un libro con el título '{titulo}'.");
        }

        public IList<Libro> GetLibros()
        {
            return this._libros.Values.ToList();
        }
    }
}
