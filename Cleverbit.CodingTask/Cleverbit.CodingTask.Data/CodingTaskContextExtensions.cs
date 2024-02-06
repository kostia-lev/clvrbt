using Cleverbit.CodingTask.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Cleverbit.CodingTask.Data
{
    public static class CodingTaskContextExtensions
    {
        public static async Task Initialize(this CodingTaskContext context, IHashService hashService)
        {
            await context.Database.EnsureCreatedAsync();

            var currentUsers = await context.Users.ToListAsync();
            var currentProducts = await context.Products.ToListAsync();

            bool anyNewUser = false;
            bool anyNewProduct = false;

            if (!currentProducts.Any(u => u.Id == 1))
            {
                context.Products.Add(new Models.Product
                {
                    Name = "Product 1",
                    Price = 10
                });

                anyNewProduct = true;
            }

            if (!currentProducts.Any(u => u.Id == 2))
            {
                context.Products.Add(new Models.Product
                {
                    Name = "Product 2",
                    Price = 20
                });

                anyNewProduct = true;
            }

            if (!currentUsers.Any(u => u.UserName == "User1"))
            {
                context.Users.Add(new Models.User
                {
                    UserName = "User1",
                    Password = await hashService.HashText("Password1")
                });

                anyNewUser = true;
            }

            if (!currentUsers.Any(u => u.UserName == "User2"))
            {
                context.Users.Add(new Models.User
                {
                    UserName = "User2",
                    Password = await hashService.HashText("Password2")
                });

                anyNewUser = true;
            }

            if (!currentUsers.Any(u => u.UserName == "User3"))
            {
                context.Users.Add(new Models.User
                {
                    UserName = "User3",
                    Password = await hashService.HashText("Password3")
                });

                anyNewUser = true;
            }

            if (!currentUsers.Any(u => u.UserName == "User4"))
            {
                context.Users.Add(new Models.User
                {
                    UserName = "User4",
                    Password = await hashService.HashText("Password4")
                });

                anyNewUser = true;
            }

            if (anyNewUser || anyNewProduct)
            {
                await context.SaveChangesAsync(); 
            }
        }
    }
}
