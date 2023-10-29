using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TanuEntityFramework.Interface;
using TanuEntityFramework.Model;
using TanuEntityFramework.Model.DTO;

namespace TanuEntityFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            var data = _userRepository.GetAll();
            if(data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        //[HttpPost("AddUser")]
        //public async Task<IActionResult> Add(User user)
        //{
        //    await _userRepository.AddUser(user);
        //    return Ok();
        //}

        [HttpPost("AddUser")]
        public async Task<IActionResult> Add(UserDto userDto)
        {
            try
            {
                //Map DTO to Domain Model
                var tanu = new User
                {
                    Name = userDto.Name,
                    Email = userDto.Email,
                    Password = userDto.Password,
                };
                var data = await _userRepository.AddUser(tanu);
                if(data == null)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            await _userRepository.UpdateAsync(id,user);
            return Ok();
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userRepository.DeleteAsync(id);
            return Ok();
        }
    }
}
