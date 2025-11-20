using EcommerceBackend.Models;
using EcommerceBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _usersService;

        public UserController(UserService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAll() =>
            Ok(await _usersService.GetAllAsync());

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<UserModel>> GetById(string id)
        {
            var user = await _usersService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Create(UserModel user)
        {
            await _usersService.CreateAsync(user);
            return Ok(user);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, UserModel updatedUser)
        {
            var existing = await _usersService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            updatedUser.Id = id;
            await _usersService.UpdateAsync(id, updatedUser);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _usersService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _usersService.DeleteAsync(id);
            return NoContent();
        }
    }
}

