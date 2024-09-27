namespace Consigna2.Models
{
    public abstract class Propiedad
    {
        public int Id { get; set; }
        public decimal Precio { get; set; }

        public Propiedad(int id, decimal precio)
        {
            Id = id;
            Precio = precio;
        }

        public Propiedad() { }
    }

}
