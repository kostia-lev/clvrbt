using Cleverbit.CodingTask.Data.Models;
using Cleverbit.CodingTask.Host.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Host.Services
{
    public interface IProductService
    {
        Task<BuyProductResponseDto> BuyProduct(int userId, int matchId);
    }
}