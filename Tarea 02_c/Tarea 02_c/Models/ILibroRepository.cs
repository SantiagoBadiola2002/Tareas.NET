
namespace Tarea_02_c.Models
{
    public interface ILibroRepository
    {
        public void Add(Libro libro);

        public Libro Get(String titulo);

        public void Delete(String titulo);

        public IList<Libro> GetLibros();
    }
}
