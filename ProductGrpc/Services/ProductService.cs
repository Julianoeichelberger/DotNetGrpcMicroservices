using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ProductGrpc.Data;
using ProductGrpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
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

            }

            var model = new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CreatedTime = Timestamp.FromDateTime(product.CreateTime),
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

        public override Task GetAll(GetAllRequest request, IServerStreamWriter<ProductModel> responseStream, ServerCallContext context)
        {
            return base.GetAll(request, responseStream, context);
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
