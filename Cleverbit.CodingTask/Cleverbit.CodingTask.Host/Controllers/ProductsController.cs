using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cleverbit.CodingTask.Data;
using Cleverbit.CodingTask.Host.Dtos;
using Cleverbit.CodingTask.Host.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product = Cleverbit.CodingTask.Data.Models.Product;

namespace Cleverbit.CodingTask.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly CodingTaskContext _codingTaskContext;
        private readonly IProductService _productService;

        public ProductsController(CodingTaskContext codingTaskContext, IProductService productService)
        {
            _codingTaskContext = codingTaskContext;
            _productService = productService;
        }
        
        [Authorize]
        [HttpGet("GetProducts")]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _codingTaskContext.Products.ToListAsync();

            return products;
        }

        [Authorize]
        [HttpPost("BuyProduct")]
        public async Task<ActionResult<BuyProductResponseDto>> BuyProduct(BuyProductDto buyProductDto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var response = await _productService.BuyProduct(userId, buyProductDto.ProductId);

            return new BuyProductResponseDto() { ProductId = response.ProductId, OrdersCount = response.OrdersCount };
        }

        [Authorize]
        [HttpGet("getOneProduct/{productId}")]
        public async Task<ActionResult<OneProductResponseDto>> getOneProduct([FromRoute] int productId)
        {
            var product = await _codingTaskContext.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();
            var orders = await _codingTaskContext.Orders.Where(o => o.ProductId == productId).ToListAsync();


            return new OneProductResponseDto() { Id = productId, Name = product.Name, Price = product.Price, OrdersCount = orders.Count() };
        }
       
    }
}