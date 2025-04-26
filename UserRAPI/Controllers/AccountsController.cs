using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using UserRAPI.DTO;
using UserRAPI.Models;

namespace UserRAPI.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserforRegistrationDTO userForRegistration)
        {
            if (userForRegistration is null)
                return BadRequest();

            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user, userForRegistration.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDTO { Errors = errors });
            }

            return StatusCode(201);
        }

        // get api/accounts
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByID(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(new {user.Id, user.FirstName, user.LastName, user.Email});
        }

        [HttpGet("getAll")]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.Select(u => new {u.Id, u.FirstName, u.LastName, u.Email});
            return Ok(users);
        }

        // Update user
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserForUpdateDTO userForUpdate)
        {
            if (userForUpdate is null)
                return BadRequest();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            user.Email = userForUpdate.Email;
            if (!string.IsNullOrWhiteSpace(userForUpdate.Password))
            {
                // 1. Generar un token de restablecimiento de contraseña
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                // 2. Aplicar el nuevo password usando el token
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, userForUpdate.Password);

                // 3. Manejar errores si falla
                if (!passwordResult.Succeeded)
                    return BadRequest(passwordResult.Errors.Select(e => e.Description));
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDTO { Errors = errors });
            }
            return NoContent();
        }

        // Delete user
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDTO { Errors = errors });
            }
            return NoContent();
        }
    }
}