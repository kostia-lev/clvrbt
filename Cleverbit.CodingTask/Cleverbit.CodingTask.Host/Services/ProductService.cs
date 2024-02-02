using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Cleverbit.CodingTask.Data;
using Cleverbit.CodingTask.Data.Models;
using Cleverbit.CodingTask.Host.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Cleverbit.CodingTask.Host.Services
{
    public class ProductService : IProductService
    {
        private readonly CodingTaskContext _codingTaskContext;

        public ProductService(CodingTaskContext codingTaskContext)
        {
            _codingTaskContext = codingTaskContext;
        }

        public async Task<BuyProductResponseDto> BuyProduct(int userId, int productId)
        {
            try
            {
                var product = await _codingTaskContext.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();

                var order = new Order()
                {
                    ProductId = productId,
                    UserId = userId
                };

                _codingTaskContext.Orders.Add(order);

                await _codingTaskContext.SaveChangesAsync();
                var orders = await _codingTaskContext.Orders.Where(o => o.ProductId == productId).ToListAsync();

                return new BuyProductResponseDto() { ProductId = product.Id, OrdersCount = orders.Count() };
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot join match");
            }
        }

            }
        }