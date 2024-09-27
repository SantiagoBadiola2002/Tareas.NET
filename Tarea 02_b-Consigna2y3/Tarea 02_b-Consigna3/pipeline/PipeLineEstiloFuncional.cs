using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_02_b_Consigna3.pipeline
{
    internal class PipeLineEstiloFuncional
    {
        private IDictionary<string, Action<string>> _endpoints;

        public PipeLineEstiloFuncional()
        {
            _endpoints = new Dictionary<string, Action<string>>();
        }

        public void AgregarEndPoint(string uri, Action<string> accionAEjecutar)
        {
            _endpoints[uri] = accionAEjecutar;
        }

        public void ProcesarRequestEntrante(string urlRequestEntrante)
        {
            if (_endpoints.TryGetValue(urlRequestEntrante, out var accion))
            {
                string param1 = "valorParam1";
                accion(param1);
            }
            else
            {
                Console.WriteLine("No se encontró un endpoint para la URL: " + urlRequestEntrante);
            }
        }
    }

}
