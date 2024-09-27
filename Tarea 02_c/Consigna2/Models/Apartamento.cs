namespace Consigna2.Models
{
    public class Apartamento : Propiedad
    {
        public int Piso { get; set; }
        public bool TieneElevador { get; set; }

        public Apartamento(int id, decimal precio, int piso, bool tieneElevador)
            : base(id, precio)
        {
            Piso = piso;
            TieneElevador = tieneElevador;
        }

        public Apartamento() { }
    }

}
