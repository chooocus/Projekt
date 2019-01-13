using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Ruch
    {
        public Pole p1 { get; set; }
        public Pole p2 { get; set; }
        public Ruch(Pole p1, Pole p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }
    }
}
