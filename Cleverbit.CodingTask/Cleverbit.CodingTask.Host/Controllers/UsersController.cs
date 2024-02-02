using System;
using System.Text;
using System.Threading.Tasks;
using Cleverbit.CodingTask.Data;
using Cleverbit.CodingTask.Host.Dtos;
using Cleverbit.CodingTask.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cleverbit.CodingTask.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CodingTaskContext _codingTaskContext;
        private readonly IHashService _hashService;

        public UsersController(CodingTaskContext codingTaskContext, IHashService hashService)
        {
            _codingTaskContext = codingTaskContext;
            _hashService = hashService;
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _codingTaskContext.Users
                .FirstOrDefaultAsync(x => x.UserName == loginRequestDto.Username);

            if (user == null)
            {
                return NotFound();
            }

            var hashedPassword = await _hashService.HashText(loginRequestDto.Password);
            if (hashedPassword != user.Password)
            {
                return Unauthorized();
            }

            byte[] bytes = Encoding.GetEncoding(28591)
                .GetBytes($"{loginRequestDto.Username}:{loginRequestDto.Password}");
            
            return Ok(Convert.ToBase64String(bytes));
        }
    }
}