using BaseLibrary.Dtos;
using BaseLibrary.Entities;

namespace BaseLibrary.Interfaces
{
    public interface IAuthRepository
    {
        Task<AppUser> Login(LoginDto loginDto);
    }
}
