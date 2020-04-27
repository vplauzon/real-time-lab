using AppInsights.TelemetryInitializers;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    internal class Simulator: IDaemon
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly SimulatorConfiguration _configuration = new SimulatorConfiguration();
        private readonly TelemetryClient _telemetryClient;

        public Simulator()
        {
            _telemetryClient = InitAppInsights(_configuration.AppInsightsKey);
        }

        async Task IDaemon.RunAsync()
        {
            await Task.Delay(1);
        }

        void IDaemon.Stop()
        {
            _cancellationTokenSource.Cancel();
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