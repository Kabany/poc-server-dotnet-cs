using Microsoft.AspNetCore.Mvc;
using poc_server_dotnet_cs.Services;

namespace poc_server_dotnet_cs.Controllers;

[ApiController]
[Route("[controller]")]
public class MetaController : ControllerBase
{
  private readonly IResponseService _responseHelper;
  private readonly IConfiguration _configuration;

  public MetaController(IResponseService responseHelper, IConfiguration configuration)
  {
    _responseHelper = responseHelper;
    _configuration = configuration;
  }

  [HttpGet]
  [Route("ping")]
  public JsonBody<String?> SendPing() {
    JsonBody<String?> body = _responseHelper.Success<String?>(null, "ping!");
    return body;
  }

  [HttpGet]
  [Route("health-check")]
  public JsonBody<String?> SendHealthCheck() {
    var body = _responseHelper.Success<String?>(null, "ok!");
    return body;
  }

  [HttpGet]
  [Route("version")]
  public JsonBody<String?> SendVersion() {
    var version = _configuration["App:Version"];
    var body = _responseHelper.Success<String?>(null, version);
    return body;
  }
}