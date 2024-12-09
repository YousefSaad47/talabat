using Talabat.Core.Dtos.auth;

namespace Talabat.Core.Service.Contract;

public interface IUserService
{
    Task<UserDto> LoginAsync(LoginDto loginDto);
    Task<UserDto> RegisterAsync(RegisterDto registerDto);
    
    Task<bool> CheckEmailExists(string email);
}