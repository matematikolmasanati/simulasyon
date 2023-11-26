using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    internal class Cevap
    {
        public char DogruCevap { get; set; }
        public char VerilenCevap { get; set; }
        public bool Dogruluk { get { return DogruCevap == VerilenCevap; }}
    }
}
