using AppInsights.TelemetryInitializers;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    internal class Simulator
    {
        private readonly SimulatorConfiguration _configuration = new SimulatorConfiguration();
        private readonly TelemetryClient _telemetryClient;

        public Simulator()
        {
            _telemetryClient = InitAppInsights(_configuration.AppInsightsKey);
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Hub Feeder");
            Console.WriteLine(
                $"Register {_configuration.GatewayCount} gateways with "
                + $"{_configuration.DronePerGateway} drones per gateway...");

            var gateways = CreateGateways();
            var tasks = from g in gateways
                        select g.RunAsync(_configuration.DronePerGateway, cancellationToken);

            Console.WriteLine("Start simulation...");

            await Task.WhenAll(tasks);
        }

        private IImmutableList<FieldGateway> CreateGateways()
        {
            var gateways = from i in Enumerable.Range(0, _configuration.GatewayCount)
                           select new FieldGateway();

            return gateways.ToImmutableArray();
        }

        private static TelemetryClient InitAppInsights(string appInsightsKey)
        {
            //  Create configuration
            var configuration = TelemetryConfiguration.CreateDefault();

            //  Set Instrumentation Keys
            configuration.InstrumentationKey = appInsightsKey;
            //  Customize App Insights role name
            configuration.TelemetryInitializers.Add(new RoleNameInitializer("drones-simulator"));

            return new TelemetryClient(configuration);
        }
    }
}