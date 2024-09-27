using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea_02_b_Consigna2.Interfaces;

namespace Tarea_02_b_Consigna2.Pasos
{
    public class Paso2AplicarFiltroColor : IPipellinePaso
    {
        public void Ejecutar(IContexto contexto)
        {
            for (int y = 0; y < contexto.Imagen.Height; y++)
            {
                for (int x = 0; x < contexto.Imagen.Width; x++)
                {
                    Color pixelColor = contexto.Imagen.GetPixel(x, y);
                    if (pixelColor.ToArgb() == contexto.ColorOriginal.ToArgb())
                    {
                        contexto.Imagen.SetPixel(x, y, contexto.ColorNuevo);
                    }
                }
            }
            Console.WriteLine($"Paso2: Filtro de color aplicado: {contexto.ColorOriginal.Name} a {contexto.ColorNuevo.Name}");
        }
    }

}
