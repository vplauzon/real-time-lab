using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    internal class Gateway
    {
        public event EventHandler<GatewayMessage>? NewMessage;

        private IImmutableQueue<DroneEvent> _eventQueue = ImmutableQueue<DroneEvent>.Empty;

        public async Task RunAsync(int droneCount, CancellationToken cancellationToken)
        {
            var drones = CreateDrones(droneCount, cancellationToken);
            var tasks = from d in drones
                        select d.RunAsync(cancellationToken);
            var pushMessageTask = PushMessagesAsync(cancellationToken);

            await Task.WhenAll(tasks);
        }

        private IImmutableList<Drone> CreateDrones(int droneCount, CancellationToken cancellationToken)
        {
            var dronesEnumerable = from i in Enumerable.Range(0, droneCount)
                                   select new Drone();
            var drones = dronesEnumerable.ToImmutableArray();

            foreach (var d in drones)
            {
                //  Accumulate messages to push in batches
                d.NewEvent += (sender, eventData) =>
                {
                    do
                    {   //  Optimistic queuing
                        var eventQueue = _eventQueue;
                        var original = Interlocked.CompareExchange(
                            ref _eventQueue,
                            eventQueue.Enqueue(eventData),
                            _eventQueue);

                        if (object.ReferenceEquals(eventQueue, original))
                        {
                            return;
                        }
                    }
                    while (!cancellationToken.IsCancellationRequested);
                };
            }

            return drones;
        }

        private async Task PushMessagesAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(3);

                if (_eventQueue.Any())
                {
                    //  Pack events into one message
                    var message = new GatewayMessage
                    {
                    };

                    NewMessage?.Invoke(this, message);
                }
            }
        }
    }
}