using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_02_b_Consigna3.pipeline
{
    internal class Endpoint
    {
        private string _uri;
        private Action<string> _accion;

        public Endpoint(string uri, Action<string> accionAEjecutar)
        {
            _uri = uri;
            _accion = accionAEjecutar;
        }

        public string Uri => _uri;

        public void Ejecutar(string parametro)
        {
            _accion(parametro);
        }
    }
}
