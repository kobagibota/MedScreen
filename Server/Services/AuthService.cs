using BaseLibrary.Dtos;
using BaseLibrary.Entities;
using BaseLibrary.Extentions;
using BaseLibrary.Interfaces;
using BaseLibrary.Respones;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Protocol.Plugins;

namespace Server.Services
{
    #region Interface

    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginDto loginDto);
    }

    #endregion Interface

    public class AuthService : IAuthService
    {
        #region Private properties

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly TokenService _tokenService;

        #endregion Private properties

        #region Constructor

        public AuthService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, TokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        #endregion Constructor

        #region Methods

        public async Task<LoginResponse> Login(LoginDto loginDto)
        {
            try
            {
                var entity = await _userManager.FindByNameAsync(loginDto.UserName);
                if (entity == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, entity.PasswordHash))
                {
                    return new LoginResponse(false, "Tài khoản hoặc mật khẩu không chính xác");
                }

                var roles = await _userManager.GetRolesAsync(entity);
                var userDto = entity.ConvertToDto(roles);
                var token = _tokenService.GenerateToken(userDto);
                var refreshToken = _tokenService.GenerateRefreshToken();
                return new LoginResponse(true, "Đăng nhập thành công", token, refreshToken);
            }
            catch (Exception)
            {
                return new LoginResponse(false, "Lỗi đăng nhập");
            }
        }

        #endregion Methods
    }
}