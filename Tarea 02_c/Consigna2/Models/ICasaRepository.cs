namespace Consigna2.Models
{
    public interface ICasaRepository
    {
        public void Add(Casa casa);

        public Casa Get(int Id);

        public void Delete(int Id);

        public IList<Casa> GetCasas();
    }
}
