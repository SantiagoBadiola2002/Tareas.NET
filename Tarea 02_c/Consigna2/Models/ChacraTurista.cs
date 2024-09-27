namespace Consigna2.Models
{
    public class ChacraTuristica : Propiedad
    {
        public int Area { get; set; }
        public bool TienePiscina { get; set; }

        public ChacraTuristica(int id, decimal precio, int area, bool tienePiscina)
            : base(id, precio)
        {
            Area = area;
            TienePiscina = tienePiscina;
        }

        public ChacraTuristica() { }
    }

}
