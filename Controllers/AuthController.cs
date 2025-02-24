using Microsoft.AspNetCore.Mvc;
using poc_server_dotnet_cs.Services;

namespace poc_server_dotnet_cs.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
  private readonly IResponseService _responseHelper;
  private readonly IAuthService _authService;

  public AuthController(IResponseService responseHelper, IAuthService authService)
  {
    _responseHelper = responseHelper;
    _authService = authService;
  }

  [HttpPost]
  [Route("jwt/string")]
  public JsonBody<AuthTokenResponse> SendJwtToken([FromBody] AuthMessageRequest request) {
    var token = _authService.CreateJwtFromString(request.message);
    var body = _responseHelper.Success(new AuthTokenResponse(token), null);
    return body;
  }

  [HttpPost]
  [Route("hash/string")]
  public JsonBody<AuthHashResponse> SendHash([FromBody] AuthMessageRequest request) {
    var hash = _authService.CreateHash(request.message);
    var body = _responseHelper.Success(new AuthHashResponse(hash), null);
    return body;
  }

  [HttpPost]
  [Route("totp/string")]
  public JsonBody<AuthCodeResponse> SendTotp([FromBody] AuthMessageRequest request) {
    var code = _authService.CreateTotp(request.message);
    var body = _responseHelper.Success(new AuthCodeResponse(code), null);
    return body;
  }
}

public record AuthMessageRequest(string message);

public record AuthTokenResponse(string token);

public record AuthHashResponse(string hash);

public record AuthCodeResponse(string code);