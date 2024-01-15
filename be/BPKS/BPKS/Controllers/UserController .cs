using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;

public class UserController : ControllerBase
{
    private readonly UserRepository _userRepository;

    public UserController(DbConnectionFactory dbConnectionFactory)
    {
        _userRepository = new UserRepository(dbConnectionFactory);
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterModel model)
    {
        try
        {
            var user = _userRepository.RegisterUser(model.UserName, model.Password, model.Name, model.Email, model.PhoneNumber, model.AvatarUrl);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
