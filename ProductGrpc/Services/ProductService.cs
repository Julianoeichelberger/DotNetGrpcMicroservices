using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductGrpc.Data;
using ProductGrpc.Protos; 
using System.Threading.Tasks;

namespace ProductGrpc
{
    public class ProductService: ProductProtoService.ProductProtoServiceBase
    {
        private readonly ILogger<ProductService> _logger;
        private readonly ProductContext Context;
        public ProductService(ProductContext productContext, ILogger<ProductService> logger)
        {
            _logger = logger;
            Context = productContext;
        }
        public override async Task<ProductModel> Get(GetRequest request, ServerCallContext context)
        {
            var product = await Context.Product.FindAsync(request.Id);
            if (product == null)
            {
                return null;
            }

            var model = new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Status = ProductStatus.InStock
            };

            return model;
        }


        public override Task<ProductModel> Add(AddRequest request, ServerCallContext context)
        {
            return base.Add(request, context);
        }

        public override Task<DeleteResponse> Delete(DeleteRequest request, ServerCallContext context)
        {
            return base.Delete(request, context);
        }

        public override Task<InsertAllResponse> InsertAll(IAsyncStreamReader<ProductModel> requestStream, ServerCallContext context)
        {
            return base.InsertAll(requestStream, context);
        }

        public override async Task GetAll(GetAllRequest request, IServerStreamWriter<ProductModel> responseStream, ServerCallContext context)
        {
            var list = await Context.Product.ToListAsync();

            foreach (var item in list)
            {
                var model = new ProductModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    Status = ProductStatus.InStock
                };

                await responseStream.WriteAsync(model);
            } 
        }

        public override Task<Empty> Test(Empty request, ServerCallContext context)
        {
            return base.Test(request, context);
        }

        public override Task<ProductModel> Update(UpdateRequest request, ServerCallContext context)
        {
            return base.Update(request, context);
        }

    }
}
