using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductGrpc.Protos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProductWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly ProductFactory _factory;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, ProductFactory factory)
        {
            _logger = logger;
            _configuration = configuration;
            _factory = factory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Thread.Sleep(2000);
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
              

                using var channel = GrpcChannel.ForAddress(_configuration.GetValue<string>("WorkerService:ServerUrl"));
                var Client = new ProductProtoService.ProductProtoServiceClient(channel);

                Console.WriteLine("Get product Started...");

                var response = await Client.AddAsync(await _factory.Generate());

                Console.WriteLine("Get product response: " + response.ToString());

                await Task.Delay(_configuration.GetValue<int>("WorkerService:TaskInterval"), stoppingToken);
            }
        }
    }
}
