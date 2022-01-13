using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
namespace BL
{
    internal sealed partial class BL : BlApi.IBL
    {
        public void OperateSimulator(int droneNumber, Action updatePl, Func<bool> isStopped)
        {
            Simulator simulator = new Simulator(this, droneNumber, updatePl, isStopped);
        }
    }
}