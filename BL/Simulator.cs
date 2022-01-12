using System;
using BO;
using System.Threading;
using System.Linq;


namespace BL
{
    class Simulator
    {
        double timer = 500;
        double velocity = 25;
        public Simulator(BlApi.IBL blObject, int droneNumber, Action updatePl, Func<bool> isStopped)
        {
            while (isStopped())
            {

            }
        }
    }
}
