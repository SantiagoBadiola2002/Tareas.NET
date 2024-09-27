using Consigna2.Models;

namespace Consigna2.Infraestructure.Data
{
    public class CasaRepository : ICasaRepository
    {
        private IDictionary<int, Casa> _casas;

        public CasaRepository()
        {
            _casas = new Dictionary<int, Casa>();
            Casa c1 = new Casa(1, 155, 3, 2);
            Casa c2 = new Casa(2, 55, 1, 1);
            Casa c3 = new Casa(3, 100,2, 1);
            this._casas.Add(c1.Id, c1);
            this._casas.Add(c2.Id, c2);
            this._casas.Add(c3.Id, c3);

        }
        public void Add(Casa casa)
        {
            if (!_casas.ContainsKey(casa.Id))
            {
                _casas.Add(casa.Id, casa);
            }
            else
            {
                throw new ArgumentException("Ya existe una casa con el mismo ID.");
            }
        }

        public void Delete(int Id)
        {
            if (_casas.ContainsKey(Id))
            {
                _casas.Remove(Id);
            }
            else
            {
                throw new KeyNotFoundException("No se encontró una casa con el ID proporcionado.");
            }
        }

        public Casa Get(int Id)
        {
            if (_casas.ContainsKey(Id))
            {
                return _casas[Id];
            }
            else
            {
                throw new KeyNotFoundException("No se encontró una casa con el ID proporcionado.");
            }
        }

        public IList<Casa> GetCasas()
        {
            return _casas.Values.ToList();
        }
    }
}
