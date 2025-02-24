using Microsoft.VisualStudio.TestTools.UnitTesting;
using poc_server_dotnet_cs.Services;

namespace poc_server_dotnet_cs.Tests;

[TestClass]
public class AuthTest
{

  private IAuthService _authService;

  [TestInitialize]
  public void TestInitialize()
  {
    var services = new ServiceCollection();
    services.AddTransient<IAuthService, AuthService>();

    var serviceProvider = services.BuildServiceProvider();

    _authService = serviceProvider.GetService<IAuthService>();
  }

  [TestMethod]
  public void ShouldCreateListOfElements()
  {
    var message = "Hello World!";
    var token = _authService.CreateJwtFromString(message);
    Assert.AreEqual("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJtZXNzYWdlIjoiSGVsbG8gV29ybGQhIn0.q_OAmeTO_BJ0X1_6SMzGEat8qHvcMZ6LtPhLg8yKXR4", token);
  }

  [TestMethod]
  public void ShouldCreateJwtThenDecodeAndMustMatchWithOriginalMessage()
  {
    var message = "Hello World!";
    var token = _authService.CreateJwtFromString(message);
    var decoded = _authService.ValidateJwtFromToken(token);
    Assert.AreEqual(message, decoded);

    // From Ruby & Python
    var decoded2 = _authService.ValidateJwtFromToken("eyJhbGciOiJIUzI1NiJ9.eyJtZXNzYWdlIjoiSGVsbG8gV29ybGQhIn0.yX3llK_oxmp-qhJ7l-B0AL8wOlzCzsDHlw7xtCU2d4s");
    Assert.AreEqual(message, decoded2);
    // From Swift
    var decoded3 = _authService.ValidateJwtFromToken("eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJtZXNzYWdlIjoiSGVsbG8gV29ybGQhIn0.Qn62lWxZ5VZKovUbE8KTu_xGeDSp739uapAuBDK360Y");
    Assert.AreEqual(message, decoded3);
  }

  [TestMethod]
  public void ShouldCreateHashWithSha512Algorithm()
  {
    var message = "Hello World!";
    var hash = _authService.CreateHash(message);
    Assert.AreEqual("861844d6704e8573fec34d967e20bcfef3d424cf48be04e6dc08f2bd58c729743371015ead891cc3cf1c9d34b49264b510751b1ff9e537937bc46b5d6ff4ecc8", hash);
  }

  [TestMethod]
  public void ShouldCreateTotp()
  {
    var message = "JBSWY3DPEHPK3PXP";
    var code1 = _authService.CreateTotp(message);
    var code2 = _authService.CreateTotp(message);
    Assert.AreEqual(code1, code2);
  }
}