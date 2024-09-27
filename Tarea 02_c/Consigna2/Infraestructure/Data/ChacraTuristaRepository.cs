using Consigna2.Models;

namespace Consigna2.Infraestructure.Data
{
    public class ChacraTuristaRepository : IChacraTuristaRepository
    {
        private IDictionary<int, ChacraTuristica> _chacras;

        public ChacraTuristaRepository()
        {
            _chacras = new Dictionary<int, ChacraTuristica>();
            ChacraTuristica c1 = new ChacraTuristica(1, 200, 100, false);
            ChacraTuristica c2 = new ChacraTuristica(2, 250, 150, true);
            ChacraTuristica c3 = new ChacraTuristica(3, 300, 200, true);
            this._chacras.Add(c1.Id, c1);
            this._chacras.Add(c2.Id, c2);
            this._chacras.Add(c3.Id, c3);

        }
        public void Add(ChacraTuristica chacraTuristica)
        {
            if (!_chacras.ContainsKey(chacraTuristica.Id))
            {
                _chacras.Add(chacraTuristica.Id, chacraTuristica);
            }
            else
            {
                throw new ArgumentException("Ya existe una chacra turística con el mismo ID.");
            }
        }

        public void Delete(int Id)
        {
            if (_chacras.ContainsKey(Id))
            {
                _chacras.Remove(Id);
            }
            else
            {
                throw new KeyNotFoundException("No se encontró una chacra turística con el ID proporcionado.");
            }
        }

        public ChacraTuristica Get(int Id)
        {
            if (_chacras.ContainsKey(Id))
            {
                return _chacras[Id];
            }
            else
            {
                throw new KeyNotFoundException("No se encontró una chacra turística con el ID proporcionado.");
            }
        }

        public IList<ChacraTuristica> GetChacrasTuristicas()
        {
            return _chacras.Values.ToList();
        }
    }
}
