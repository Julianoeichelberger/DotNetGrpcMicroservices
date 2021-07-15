using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; 

namespace ProductGrpc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            SeedDataBase(host);
            host.Run();
        }

        private static void SeedDataBase(IHost host)
        {
            using var scope = host.Services.CreateScope(); 
            var context = scope.ServiceProvider.GetRequiredService<Data.ProductContext>();
            Data.ProductContextSeed.Async(context);
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
