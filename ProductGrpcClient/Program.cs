using Google.Protobuf.WellKnownTypes;
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
        private const string host = "https://localhost:5009";

        static async Task Main(string[] args)
        {
            Thread.Sleep(2000);
            using var channel = GrpcChannel.ForAddress(host);
            var Client = new ProductProtoService.ProductProtoServiceClient(channel);

            await GetProductAsync(Client);
            await GetAllProductsAsync(Client);
            await AddProductAsync(Client);
            await UpdateProductAsync(Client);
            await DeleteProductAsync(Client);

            await InsertListAsync(Client); 

            Console.ReadKey();
        }

        private static async Task InsertListAsync(ProductProtoService.ProductProtoServiceClient client)
        {
            Console.WriteLine("InsertList Started...");
            using var clientList = client.InsertAll();
            for (int i = 7; i < 10; i++)
            {
                var productModel = new ProductModel
                {
                    Id = i,
                    Name = "Product_" + i.ToString(),
                    Description = "Description of product " + i.ToString(),
                    Price = 99,
                    Status = ProductStatus.InStock,
                    CreatedTime = Timestamp.FromDateTime(DateTime.UtcNow)
                };
                await clientList.RequestStream.WriteAsync(productModel);
            }

            await clientList.RequestStream.CompleteAsync();
            var response = await clientList;

            Console.WriteLine($"Status: {response.Success}. Insert Count: {response.InsertedCount}");
        }

        private static async Task DeleteProductAsync(ProductProtoService.ProductProtoServiceClient client)
        {
            Console.WriteLine("DeleteProduct Started...");
            var response = await client.DeleteAsync(
                    new DeleteRequest
                    {
                        Id = 4
                    }
                );
            Console.WriteLine(response.ToString());
        }

        private static async Task UpdateProductAsync(ProductProtoService.ProductProtoServiceClient client)
        {
            Console.WriteLine("UpdateProduct Started...");
            var response = await client.UpdateAsync(
                    new UpdateRequest { 
                         Product = new ProductModel {
                             Id = 4,
                             Name = "Product_4",
                             Description = "Description of product 4 update",
                             Price = 73,
                             Status = ProductStatus.InStock,
                             CreatedTime = Timestamp.FromDateTime(DateTime.UtcNow)
                         }
                    }
                );
            Console.WriteLine(response.ToString());
        }

        private static async Task AddProductAsync(ProductProtoService.ProductProtoServiceClient client)
        {
            Console.WriteLine("Get AddProduct Started...");

            var response = await client.AddAsync(
                new AddRequest 
                { 
                    Product = new ProductModel
                    {
                        Id = 4,
                        Name = "Product_4",
                        Description = "Description of product 4",
                        Price = 73,
                        Status = ProductStatus.InStock,
                        CreatedTime = Timestamp.FromDateTime(DateTime.UtcNow)
                    }
                }
                );

            Console.WriteLine("AddProduct: " + response.ToString());
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
