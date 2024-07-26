using Microsoft.AspNetCore.Mvc;
using UserData.Models;
using UserData.Repository;

namespace UserData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleController(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRole>>> GetUserRoles()
        {
            var userRoles = await _userRoleRepository.GetUserRoles();
            return Ok(userRoles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserRole>> GetUserRole(int id)
        {
            var userRole = await _userRoleRepository.GetUserRolesById(id);
            if (userRole == null)
            {
                return NotFound();
            }
            return Ok(userRole);
        }

        [HttpPost]
        public async Task<ActionResult<UserRole>> PostUserRole(UserRole userRole)
        {
            var createdUserRole = await _userRoleRepository.AddUserRole(userRole);
            return CreatedAtAction(nameof(GetUserRole), new { id = createdUserRole.Id }, createdUserRole);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRole(int id, UserRole userRole)
        {
            if (id != userRole.Id)
            {
                return BadRequest();
            }
            await _userRoleRepository.UpdateUserRole(userRole);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRole(int id)
        {
            await _userRoleRepository.DeleteUserRole(id);
            return NoContent();
        }
    }
}