namespace Consigna2.Models
{
    public class Casa : Propiedad
    {
        public int NumeroHabitaciones { get; set; }
        public int NumeroBanos { get; set; }

        public Casa(int id, decimal precio, int numeroHabitaciones, int numeroBanos)
            : base(id, precio)
        {
            NumeroHabitaciones = numeroHabitaciones;
            NumeroBanos = numeroBanos;
        }

        public Casa () { }
    }

}
