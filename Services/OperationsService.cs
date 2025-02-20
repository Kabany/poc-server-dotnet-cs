namespace poc_server_dotnet_cs.Services;

public interface IOperationsService
{
  List<OperationItem> CreateList(int times);
  long FibonacciSum(int n);
  long[] FibonacciList(int n);
}

public class OperationsService : IOperationsService
{
  public List<OperationItem> CreateList(int times)
  {
    var list = new List<OperationItem>();
    for (int x = 0; x < times; x++)
    {
      list.Add(new OperationItem(x+1, $"This is the message number {x+1}"));
    }
    return list;
  }

  public long FibonacciSum(int n)
  {
    if (n <= 0) {
      return 0;
    }

    long[] fibo = new long[n+1];
    fibo[0] = 0; 
    fibo[1] = 1;
  
    // Initialize result
    long sum = fibo[0] + fibo[1];
  
    // Add remaining terms
    for (int i=2; i<=n; i++)
    {
      fibo[i] = fibo[i-1]+fibo[i-2];
      sum += fibo[i];
    }

    return sum;
  }

  public long[] FibonacciList(int n) {
    if (n < 0)
    { 
      return []; 
    } else if (n == 0) {
      return [0];
    }
  
    long[] fibo = new long[n+1];
    fibo[0] = 0; 
    fibo[1] = 1;
  
    // Add remaining terms
    for (int i=2; i<=n; i++)
    {
      fibo[i] = fibo[i-1]+fibo[i-2];
    }
  
    return fibo;
  }
}

public record OperationItem(int id, string message);