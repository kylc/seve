using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILC.Seve
{
    abstract class GASimulation
    {
        public abstract void Step();
        public abstract void Stop();
    }
}
