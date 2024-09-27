namespace Consigna2.Models
{
    public interface IApartamentoRepository
    {
        public void Add(Apartamento apartamento);

        public Apartamento Get(int Id);

        public void Delete(int Id);

        public IList<Apartamento> GetApartamentos();
    }
}
