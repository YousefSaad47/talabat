using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Errors;
using Talabat.Core.Dtos.auth;
using Talabat.Core.Service.Contract;

namespace Talabat.Apis.Controllers;

public class AccountController : BaseApiController
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var userDto = await _userService.LoginAsync(loginDto);
        
        if (userDto is null)
            return Unauthorized(new ApiResponse(StatusCodes.Status401Unauthorized));
        
        return Ok(userDto);
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        var userDto = await _userService.RegisterAsync(registerDto);
        
        if (userDto is null)
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest));
        
        return Ok(userDto);
    }
}