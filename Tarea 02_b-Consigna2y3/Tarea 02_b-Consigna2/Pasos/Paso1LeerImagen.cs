using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea_02_b_Consigna2.Interfaces;

namespace Tarea_02_b_Consigna2.Pasos
{
    public class Paso1LeerImagen : IPipellinePaso
    {
        private readonly string _rutaArchivo;

        public Paso1LeerImagen(string rutaArchivo)
        {
            _rutaArchivo = rutaArchivo;
        }

        public void Ejecutar(IContexto contexto)
        {
            contexto.Imagen = new Bitmap(_rutaArchivo);
            Console.WriteLine($"Paos1: Imagen leída desde {_rutaArchivo}");
        }
    }

}
