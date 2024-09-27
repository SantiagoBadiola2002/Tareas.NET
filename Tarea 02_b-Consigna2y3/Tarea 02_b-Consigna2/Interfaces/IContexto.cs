using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_02_b_Consigna2.Interfaces
{
    public interface IContexto // Interfaz para el contexto compartido
                               // Esta interfaz representa el contexto que contiene el estado compartido entre los pasos del pipeline. 
    {
        // Propiedad para la imagen
        Bitmap Imagen { get; set; }

        // Parámetros adicionales, como colores de reemplazo
        Color ColorOriginal { get; set; }
        Color ColorNuevo { get; set; }

    }
}
