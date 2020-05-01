using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.ApplicationInsights;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatorClient
{
    internal class Simulator
    {
        private static readonly JsonSerializerOptions JSON_SERIALIZER_OPTIONS = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly SimulatorConfiguration _configuration;
        private readonly TelemetryClient _telemetryClient;
        private readonly EventHubProducerClient _producerClient;

        public Simulator(SimulatorConfiguration configuration, TelemetryClient telemetryClient)
        {
            _configuration = configuration;
            _telemetryClient = telemetryClient;
            _producerClient =
                new EventHubProducerClient(_configuration.EventHubConnectionString);
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
            const double START_LONGITUDE = -70.916935;
            const double START_LATITUDE = 46.783386;

            var height = (int)Math.Max(1, Math.Sqrt(_configuration.GatewayCount) / 2);
            var gatewaysEnumerable = from i in Enumerable.Range(0, _configuration.GatewayCount)
                                     let x = i / height
                                     let y = i % height
                                     select new Gateway(
                                         START_LONGITUDE + x * 0.05,
                                         START_LATITUDE + x * 0.02 - y * 0.04);
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

        private async Task PushMessageAsync(GatewayMessage message, CancellationToken cancellationToken)
        {
            using (var eventBatch = await _producerClient.CreateBatchAsync(cancellationToken))
            {
                var buffer = JsonSerializer.SerializeToUtf8Bytes(message, JSON_SERIALIZER_OPTIONS);

                eventBatch.TryAdd(new EventData(buffer));
                await _producerClient.SendAsync(eventBatch);
            }
        }
    }
}