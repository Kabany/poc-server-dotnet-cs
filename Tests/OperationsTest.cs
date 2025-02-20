using Microsoft.VisualStudio.TestTools.UnitTesting;
using poc_server_dotnet_cs.Services;

namespace poc_server_dotnet_cs.Tests;

[TestClass]
public class OperationsTest
{

  private IOperationsService _operationService;

  [TestInitialize]
  public void TestInitialize()
  {
    var services = new ServiceCollection();
    services.AddTransient<IOperationsService, OperationsService>();

    var serviceProvider = services.BuildServiceProvider();

    _operationService = serviceProvider.GetService<IOperationsService>();
  }

  [TestMethod]
  public void ShouldCreateListOfElements()
  {
    var list = _operationService.CreateList(1000);
    Assert.AreEqual(1000, list.Count());
    // First item
    Assert.AreEqual(1, list[0].id);
    Assert.AreEqual("This is the message number 1", list[0].message);
    // 100th item
    Assert.AreEqual(100, list[99].id);
    Assert.AreEqual("This is the message number 100", list[99].message);
    // 1000th item
    Assert.AreEqual(1000, list[999].id);
    Assert.AreEqual("This is the message number 1000", list[999].message);
  }

  [TestMethod]
  public void ShouldAggregateItemsFromFibonacciSequence()
  {
    Assert.AreEqual(0, _operationService.FibonacciSum(-1));
    Assert.AreEqual(0, _operationService.FibonacciSum(0));
    Assert.AreEqual(1, _operationService.FibonacciSum(1));
    Assert.AreEqual(2, _operationService.FibonacciSum(2));
    Assert.AreEqual(4, _operationService.FibonacciSum(3));
    Assert.AreEqual(7, _operationService.FibonacciSum(4));
    Assert.AreEqual(12, _operationService.FibonacciSum(5));
    Assert.AreEqual(143, _operationService.FibonacciSum(10));
    Assert.AreEqual(32951280098L, _operationService.FibonacciSum(50));
    // Assert.AreEqual(927372692193078999175L, _operationService.FibonacciSum(100));
  }

  [TestMethod]
  public void ShouldGenerateFibonacciList()
  {
    Assert.AreEqual(0, _operationService.FibonacciList(-1).Count());
    var list0 = _operationService.FibonacciList(0);
    Assert.AreEqual(1, list0.Count());
    Assert.AreEqual(0, list0[0]);
    var list1 = _operationService.FibonacciList(1);
    Assert.AreEqual(2, list1.Count());
    Assert.AreEqual(1, list1[1]);
    var list2 = _operationService.FibonacciList(2);
    Assert.AreEqual(3, list2.Count());
    Assert.AreEqual(1, list2[2]);
    var list3 = _operationService.FibonacciList(3);
    Assert.AreEqual(4, list3.Count());
    Assert.AreEqual(2, list3[3]);
    var list4 = _operationService.FibonacciList(4);
    Assert.AreEqual(5, list4.Count());
    Assert.AreEqual(3, list4[4]);
    var list5 = _operationService.FibonacciList(5);
    Assert.AreEqual(6, list5.Count());
    Assert.AreEqual(5, list5[5]);
    var list6 = _operationService.FibonacciList(6);
    Assert.AreEqual(7, list6.Count());
    Assert.AreEqual(8, list6[6]);
    var list7 = _operationService.FibonacciList(10);
    Assert.AreEqual(11, list7.Count());
    Assert.AreEqual(55, list7[10]);
    var list8 = _operationService.FibonacciList(50);
    Assert.AreEqual(51, list8.Count());
    Assert.AreEqual(12586269025L, list8[50]);
    var list9 = _operationService.FibonacciList(78);
    Assert.AreEqual(79, list9.Count());
    Assert.AreEqual(8944394323791464L, list9[78]);
  }
}