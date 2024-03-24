using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;
using Microsoft.AspNetCore.Identity;

namespace Server.Services
{
    #region Interface

    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginDto loginDto);        
    }

    #endregion

    public class AuthService : IAuthService
    {
        #region Private properties

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;

        #endregion

        #region Constructor

        public AuthService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        #endregion

        #region Methods

        public async Task<LoginResponse> Login(LoginDto loginDto)
        {
            try
            {
                var entity = await _unitOfWork.UserRepository.GetBy(x => x.UserName == loginDto.UserName);
                if (entity == null)
                {
                    return new LoginResponse(false, "Tài khoản không tồn tại");
                }
                var checkPass = BCrypt.Net.BCrypt.Verify(loginDto.Password, entity.PasswordHash);
                if (!checkPass)
                {
                    return new LoginResponse(false, "Mật khẩu chưa chính xác");
                }

                var roles = await _userManager.GetRolesAsync(entity);
                var userDto = entity.ConvertToDto(roles);

                var token = _tokenService.GenerateToken(userDto);
                return new LoginResponse(true, "Đăng nhập thành công", token);
            }
            catch (Exception)
            {
                return new LoginResponse(false, "Lỗi đăng nhập");
            }
        }


        #endregion
    }
}
