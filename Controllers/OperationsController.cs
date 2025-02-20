using Microsoft.AspNetCore.Mvc;
using poc_server_dotnet_cs.Services;

namespace poc_server_dotnet_cs.Controllers;

[ApiController]
[Route("[controller]")]
public class OperationsController : ControllerBase
{
  private readonly IResponseService _responseHelper;
  private readonly IOperationsService _operationsService;

  public OperationsController(IResponseService responseHelper, IOperationsService operationsService)
  {
    _responseHelper = responseHelper;
    _operationsService = operationsService;
  }

  [HttpGet]
  [Route("list/params/{times}")]
  public JsonBody<List<OperationItem>> SendListWithParams(int times) {
    var list = _operationsService.CreateList(times);
    var body = _responseHelper.Success(list, null);
    return body;
  }

  [HttpGet]
  [Route("list/query")]
  public JsonBody<List<OperationItem>> SendListWithQuery([FromQuery] int times) {
    var list = _operationsService.CreateList(times);
    var body = _responseHelper.Success(list, null);
    return body;
  }

  [HttpPost]
  [Route("list/body")]
  public JsonBody<List<OperationItem>> SendListWithBody([FromBody] OperationTimeRequest timeRequest) {
    var list = _operationsService.CreateList(timeRequest.times);
    var body = _responseHelper.Success(list, null);
    return body;
  }

  [HttpGet]
  [Route("list/headers")]
  public JsonBody<List<OperationItem>> SendListWithHeaders([FromHeader] int times) {
    var list = _operationsService.CreateList(times);
    var body = _responseHelper.Success(list, null);
    return body;
  }

  [HttpGet]
  [Route("fibonacci/sum/{number}")]
  public JsonBody<OperationSumResponse> SendFibunacciSum(int number) {
    var sum = _operationsService.FibonacciSum(number);
    var body = _responseHelper.Success(new OperationSumResponse(sum), null);
    return body;
  }

  [HttpGet]
  [Route("fibonacci/list/{number}")]
  public JsonBody<OperationListResponse> SendFibunacciList(int number) {
    var list = _operationsService.FibonacciList(number);
    var body = _responseHelper.Success(new OperationListResponse(list), null);
    return body;
  }
}

public record OperationTimeRequest(int times);

public record OperationSumResponse(long sum);

public record OperationListResponse(long[] list);