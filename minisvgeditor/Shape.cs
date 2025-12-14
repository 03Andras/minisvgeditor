using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing; // Point és Color miatt

namespace minisvgeditor
{
    internal class Shape
    {
        public string Type { get; set; } // "Vonal", "téglalap", "Elipszis stb.."
        public Point Start { get; set; } // Startpont
        public Point End { get; set; }   // Végpont
        public Color Color { get; set; } // Szín
    }
}

