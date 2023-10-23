using Microsoft.AspNetCore.Http.HttpResults;
using SharedLibrary.DTOs;
using UdemyAuthServer.Core.DTOs;

namespace UdemyAuthServer.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<Response<UserAppDto>> GetUserByNameAsync(string userName);
        Task<Response<NoContent>> CreateUserRoles(string userName);
    }
}
