using AppInsights.TelemetryInitializers;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    class Program
    {
        static public async Task Main(string[] args)
        {
            var configuration = new SimulatorConfiguration();
            var telemetryClient = InitAppInsights(configuration.AppInsightsKey);
            var simulator = new Simulator(configuration, telemetryClient);
            var _cancellationTokenSource = new CancellationTokenSource();
            var task = simulator.RunAsync(_cancellationTokenSource.Token);

            AppDomain.CurrentDomain.ProcessExit += (object? sender, EventArgs e) =>
            {
                _cancellationTokenSource.Cancel();
            };

            await task;
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