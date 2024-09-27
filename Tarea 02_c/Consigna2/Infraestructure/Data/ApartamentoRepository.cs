using Consigna2.Models;

namespace Consigna2.Infraestructure.Data
{
    public class ApartamentoRepository : IApartamentoRepository
    {
        private IDictionary<int, Apartamento> _apartamentos;

        public ApartamentoRepository()
        {
            _apartamentos = new Dictionary<int, Apartamento>();
            Apartamento a1 = new Apartamento(1, 50, 3, false);
            Apartamento a2 = new Apartamento(2, 70, 1, false);
            Apartamento a3 = new Apartamento(3, 75, 2, true);
            this._apartamentos.Add(a1.Id, a1);
            this._apartamentos.Add(a2.Id, a2);
            this._apartamentos.Add(a3.Id, a3);

        }
        public void Add(Apartamento apartamento)
        {
            if (!_apartamentos.ContainsKey(apartamento.Id))
            {
                _apartamentos.Add(apartamento.Id, apartamento);
            }
            else
            {
                throw new ArgumentException("Ya existe un apartamento con el mismo ID.");
            }
        }

        public void Delete(int Id)
        {
            if (_apartamentos.ContainsKey(Id))
            {
                _apartamentos.Remove(Id);
            }
            else
            {
                throw new KeyNotFoundException("No se encontró un apartamento con el ID proporcionado.");
            }
        }

        public Apartamento Get(int Id)
        {
            if (_apartamentos.ContainsKey(Id))
            {
                return _apartamentos[Id];
            }
            else
            {
                throw new KeyNotFoundException("No se encontró un apartamento con el ID proporcionado.");
            }
        }

        public IList<Apartamento> GetApartamentos()
        {
            return _apartamentos.Values.ToList();
        }
    }
}
