using Cleverbit.CodingTask.Data.Models;
using System;

namespace Cleverbit.CodingTask.Host.Dtos
{
    public class OneProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int OrdersCount { get; set; }
    }
}