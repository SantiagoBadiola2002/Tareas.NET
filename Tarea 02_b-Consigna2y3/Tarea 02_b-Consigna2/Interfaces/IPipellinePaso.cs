using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_02_b_Consigna2.Interfaces
{
    internal interface IPipellinePaso // Interfaz para los pasos del pipeline.
    {
        // Método para ejecutar el paso en el contexto proporcionado
        void Ejecutar(IContexto contexto);
    }
}
