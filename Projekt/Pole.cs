using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    [Serializable]
    public class Pole
    {
        public bool zajety { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Pole(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
            zajety = false;
        }
    }
}
