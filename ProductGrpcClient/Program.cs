using Grpc.Net.Client;
using ProductGrpc.Protos;
using System;
using System.Threading.Tasks;

namespace ProductGrpcClient
{
    class Program
    {
        private const string host = "https://localhost:5009";

        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress(host);
            var Client = new ProductProtoService.ProductProtoServiceClient(channel);

            Console.WriteLine("Get product Started..."); 
            
            var response = await Client.GetAsync(new GetRequest { Id = 1 });
            
            Console.WriteLine("Get product response: " + response.ToString());
        }
    }
}
