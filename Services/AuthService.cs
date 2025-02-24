using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OtpNet;

namespace poc_server_dotnet_cs.Services;

public interface IAuthService
{
  string CreateJwtFromString(string message);
  string ValidateJwtFromToken(string token);
  string CreateHash(string text);
  string CreateTotp(string text);
}

public class AuthService : IAuthService
{

  private string FIRST_JWT_SECRET = "ThisIsAVeryLongStringToAlignWithTheIDX10720RequirementForDotNetWebApps";

  public string CreateJwtFromString(string message)
  {
    var claims = new List<Claim>
    {
      new Claim("message", message)     
    };
    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(FIRST_JWT_SECRET));
    var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var token = new JwtSecurityToken(claims: claims, signingCredentials: cred);
    var jwt = new JwtSecurityTokenHandler().WriteToken(token);
    return jwt;
  }

  public string ValidateJwtFromToken(string token)
  {
    var handler = new JwtSecurityTokenHandler();
    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(FIRST_JWT_SECRET));
    var validations = new TokenValidationParameters
    {
      IssuerSigningKey = key,
      ValidateAudience = false,
      ValidateLifetime = false,
      ValidateIssuer = false
    };
    var claims = handler.ValidateToken(token, validations, out var tokenSecure);
    return claims.FindFirstValue("message") ?? "";
  }

  public string CreateHash(string text)
  {
    var sha512 = SHA512.Create();
    var bytes = Encoding.UTF8.GetBytes(text);
    var hash = sha512.ComputeHash(bytes);
    var sb = new StringBuilder();
    for (int i= 0; i < hash.Length; i++) {
      sb.Append(hash[i].ToString("X2"));
    }
    return sb.ToString().ToLower();
  }

  public string CreateTotp(string text)
  {
    var totp = new Totp(Base32Encoding.ToBytes(text), mode: OtpHashMode.Sha512, step: 30, totpSize: 8);
    var code = totp.ComputeTotp();
    return code;
  }
}