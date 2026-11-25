using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registrodealumnosequipojuanjose
{
    internal class Alumnos
    {
        public string Nombre { get; set; }
        public string Grupo { get; set; }
        public double Cal1 { get; set; }
        public double Cal2 { get; set; }
        public double Cal3 { get; set; }
        public double Cal4 { get; set; }
        public double Cal5 { get; set; }

        public double Promedio
        {
            get { return (Cal1 + Cal2 + Cal3 + Cal4 + Cal5) / 5; }
        }
    }
}
