using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea_02_b_Consigna2.Interfaces;

namespace Tarea_02_b_Consigna2.Orchestrator
{
    internal class PipelineOrquestador // Clase para orquestar la ejecución de los pasos.
    {
        private readonly List<IPipellinePaso> _pasos = new List<IPipellinePaso>();

        // Método para agregar un paso al pipeline
        public void AgregarPaso(IPipellinePaso paso)
        {
            _pasos.Add(paso);
        }

        // Método para ejecutar todos los pasos en secuencia
        public void Ejecutar(IContexto contexto)
        {
            foreach (var paso in _pasos)
            {
                paso.Ejecutar(contexto);
            }
        }
    }
}
