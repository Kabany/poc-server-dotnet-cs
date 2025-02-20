namespace poc_server_dotnet_cs.Services;

public interface IResponseService
{
  JsonBody<T> Success<T>(T? data, string? message);
}

public class ResponseService : IResponseService
{
  public JsonBody<T> Success<T>(T? data, string? message)
  {
    JsonBody<T> body = new JsonBody<T>(data, message, true);
    return body;
  }
}

public record JsonBody<T> (T? data, string? message, bool success);