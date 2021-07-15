using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductGrpc.Data;
using ProductGrpc.Models;
using ProductGrpc.Protos;
using System;
using System.Threading.Tasks;

namespace ProductGrpc
{
    public class ProductService: ProductProtoService.ProductProtoServiceBase
    {
        private readonly ILogger<ProductService> _logger;
        private readonly ProductContext _context;
        private readonly IMapper _mapper; 
        public ProductService(ProductContext productContext, IMapper mapper, ILogger<ProductService> logger)
        {
            _logger = logger;
            _context = productContext;
            _mapper = mapper;
        }
        public override async Task<ProductModel> Get(GetRequest request, ServerCallContext context)
        {
            var product = await _context.Product.FindAsync(request.Id);

            if (product == null)
            {
                return null;
            }

            var model = _mapper.Map<ProductModel>(product);
             
            return model;
        }


        public override async Task<ProductModel> Add(AddRequest request, ServerCallContext context)
        {
            var Item = _mapper.Map<Product>(request.Product); 

            _context.Product.Add(Item);
            await _context.SaveChangesAsync();

            var model = _mapper.Map<ProductModel>(Item); 
            return model;
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
            var list = await _context.Product.ToListAsync();

            foreach (var item in list)
            {
                var model = _mapper.Map<ProductModel>(item); 
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
