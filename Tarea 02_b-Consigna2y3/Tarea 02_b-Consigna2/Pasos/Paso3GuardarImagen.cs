using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea_02_b_Consigna2.Interfaces;

namespace Tarea_02_b_Consigna2.Pasos
{
    public class Paso3GuardarImagen : IPipellinePaso
    {
        private readonly string _rutaArchivo;

        public Paso3GuardarImagen(string rutaArchivo)
        {
            _rutaArchivo = rutaArchivo;
        }

        public void Ejecutar(IContexto contexto)
        {
            contexto.Imagen.Save(_rutaArchivo);
            Console.WriteLine($"Paso 3: Imagen guardada en {_rutaArchivo}");
        }
    }

}
