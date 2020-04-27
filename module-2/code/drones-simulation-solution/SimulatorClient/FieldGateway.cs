using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    internal class FieldGateway
    {
        public FieldGateway()
        {
        }

        public async Task RunAsync(int droneCount, CancellationToken cancellationToken)
        {
            var devices = CreateDevices(droneCount);
            var tasks = from d in devices
                        select d.RunAsync(cancellationToken);

            await Task.WhenAll(tasks);
        }

        public void PushEvent(DeviceEvent deviceEvent)
        {
            throw new NotImplementedException();
        }

        private IImmutableList<Device> CreateDevices(int droneCount)
        {
            var devices = from i in Enumerable.Range(0, droneCount)
                          select new Device();

            return devices.ToImmutableArray();
        }
    }
}