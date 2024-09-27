using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea_02_b_Consigna2.Interfaces;

namespace Tarea_02_b_Consigna2
{
    public class ContextoImagen : IContexto
    {
        public Bitmap Imagen { get; set; }
        public Color ColorOriginal { get; set; }
        public Color ColorNuevo { get; set; }
    }
}
