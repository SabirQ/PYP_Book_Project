using Microsoft.AspNetCore.Identity;
using PYP_Book.Application.Common.Dtos;

namespace PYP_Book.Application.Common.Interfaces;
public interface IIdentityService
{
    Task<IdentityResult> RegisterAsync(RegisterDto registerDto);
    Task<string> LoginAsync(LoginDto loginDto);
    Task<IdentityResult> DeleteUserAsync(string username);
    Task<bool> IsInRoleAsync(string userId, string roleName);
    Task<IdentityResult> AddRoleAsync(string username, string role);
    Task CreateRoles(string role);
    Task<IEnumerable<UserDto>> GetAll();
}
