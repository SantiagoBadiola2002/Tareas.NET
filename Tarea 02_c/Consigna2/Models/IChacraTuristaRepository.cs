namespace Consigna2.Models
{
    public interface IChacraTuristaRepository
    {
        public void Add(ChacraTuristica chacraTuristica);

        public ChacraTuristica Get(int Id);

        public void Delete(int Id);

        public IList<ChacraTuristica> GetChacrasTuristicas();
    }
}
