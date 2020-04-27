using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimulatorClient
{
    public interface IDaemon
    {
        Task RunAsync();

        void Stop();
    }
}