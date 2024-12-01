using BaseLibrary.Dtos;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class TokenService(IOptions<JwtSection> config)
{
    #region Private members

    private readonly IOptions<JwtSection> _config = config;

    #endregion Private members

    #region Methods

    public string GenerateToken(UserDto userDto)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Value.SecurityKey!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var userClaim = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
            new Claim(ClaimTypes.Name, userDto.UserName),
            new Claim(ClaimTypes.Surname, userDto.LastName),
            new Claim(ClaimTypes.GivenName, userDto.FirstName),
            new Claim(ClaimTypes.Role, userDto.Roles)
        };

        var token = new JwtSecurityToken(
            issuer: _config.Value.ValidIssuer,
            audience: _config.Value.ValidAudience,
            claims: userClaim,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config.Value.ExpiryInMinutes)),
            signingCredentials: credentials
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public string? RefreshToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var keyBytes = SHA256.HashData(Encoding.UTF8.GetBytes(_config.Value.SecurityKey!));

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = true,
            ValidateLifetime = true
        };

        try
        {
            // Xác thực token cũ
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

            // Nếu token cũ hợp lệ, tạo token mới với thời gian hết hạn mới
            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                var expiryTime = jwtSecurityToken.ValidTo;
                var now = DateTime.UtcNow;

                // Nếu token đã hết hạn hoặc sắp hết hạn, tạo token mới
                if (expiryTime.Subtract(now).TotalMinutes <= _config.Value.ExpiryInMinutes)
                {
                    // Trích xuất thông tin người dùng từ token cũ
                    var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var userName = principal.FindFirst(ClaimTypes.Name)?.Value;
                    var userLastName = principal.FindFirst(ClaimTypes.Surname)?.Value;
                    var userFirstName = principal.FindFirst(ClaimTypes.GivenName)?.Value;
                    var userRoles = principal.FindFirst(ClaimTypes.Role)?.Value;

                    // Tạo token mới với thông tin người dùng tương tự
                    var userDto = new UserDto
                    {
                        Id = Guid.Parse(userId!),
                        UserName = userName!,
                        LastName = userLastName!,
                        FirstName = userFirstName!,
                        Roles = userRoles!
                    };

                    return GenerateToken(userDto);
                }
            }
        }
        catch
        {
            // Nếu có ngoại lệ xảy ra trong quá trình xác thực hoặc tạo token, trả về null
            return null;
        }

        // Nếu token cũ vẫn hợp lệ, trả về null
        return null;
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var keyBytes = SHA256.HashData(Encoding.UTF8.GetBytes(_config.Value.SecurityKey!));

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = true,
            ValidateLifetime = true
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            return principal;
        }
        catch
        {
            return null;
        }
    }

    #endregion Methods
}