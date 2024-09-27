namespace Tarea02_SantiagoBadiola.Models
{
    public class Tarea
    {
        // Atributos
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int? DuracionHoras { get; set; }
        public string? Responsable { get; set; }
        public DateTime? FechaCreacion { get; set; }  // Nuevo atributo de tipo fecha

        // Constructor
        public Tarea(int id, string nombre, string descripcion, int duracionHoras, string responsable, DateTime fechaCreacion)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            DuracionHoras = duracionHoras;
            Responsable = responsable;
            FechaCreacion = fechaCreacion;  // Inicializar el nuevo atributo
        }

        public Tarea()
        {

        }

        // Método para mostrar información de la tarea
        public void MostrarInformacion()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Descripción: {Descripcion}");
            Console.WriteLine($"Duración: {DuracionHoras} horas");
            Console.WriteLine($"Responsable: {Responsable}");
            Console.WriteLine($"Fecha de Creación: {FechaCreacion}");  // Mostrar la fecha en formato año-mes-día
        }
    }
}
