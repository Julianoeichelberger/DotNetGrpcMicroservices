using Grpc.Core;
using Grpc.Net.Client;
using ProductGrpc.Protos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProductGrpcClient
{
    class Program
    {
        private const string host = "http://localhost:5008";

        static async Task Main(string[] args)
        {
            Thread.Sleep(2000);
            using var channel = GrpcChannel.ForAddress(host);
            var Client = new ProductProtoService.ProductProtoServiceClient(channel);

            await GetProductAsync(Client);
            await GetAllProductsAsync(Client);

            Console.ReadKey();
        }

        private static async Task GetAllProductsAsync(ProductProtoService.ProductProtoServiceClient Client)
        {
            Console.WriteLine("Get Allproducts Started...");
            var clientData = Client.GetAll(new GetAllRequest());
            await foreach (var responseData in clientData.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine(responseData.ToString());
            }
            Console.WriteLine("Get Allproducts End...");
        }

        private static async Task GetProductAsync(ProductProtoService.ProductProtoServiceClient Client)
        {
            Console.WriteLine("Get product Started...");

            var response = await Client.GetAsync(new GetRequest { Id = 1 });

            Console.WriteLine("Get product response: " + response.ToString()); 
        }
    }
}
