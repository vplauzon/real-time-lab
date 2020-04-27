using Microsoft.ApplicationInsights;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    internal class Simulator
    {
        private readonly SimulatorConfiguration _configuration;
        private readonly TelemetryClient _telemetryClient;

        public Simulator(SimulatorConfiguration configuration, TelemetryClient telemetryClient)
        {
            _configuration = configuration;
            _telemetryClient = telemetryClient;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Hub Feeder");
            Console.WriteLine(
                $"Register {_configuration.GatewayCount} gateways with "
                + $"{_configuration.DronePerGateway} drones per gateway...");

            var gateways = CreateGateways(cancellationToken);
            var droneTasks = from g in gateways
                             select g.RunAsync(_configuration.DronePerGateway, cancellationToken);

            Console.WriteLine("Start simulation...");

            await Task.WhenAll(droneTasks);
        }

        private IImmutableList<Gateway> CreateGateways(CancellationToken cancellationToken)
        {
            var gatewaysEnumerable = from i in Enumerable.Range(0, _configuration.GatewayCount)
                                     select new Gateway() { };
            var gateways = gatewaysEnumerable.ToImmutableArray();

            foreach (var g in gateways)
            {
                g.NewMessage += async (sender, message) =>
                {
                    await PushMessageAsync(message, cancellationToken);
                };
            }

            return gateways;
        }

        private Task PushMessageAsync(GatewayMessage message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}