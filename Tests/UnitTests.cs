using Fagdag.FunctionApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        //Enkel test av kalkulator metoden

        int a = 12;
        int b = 5;
        int expected = 17;

        int sum = Kalkulator.Sum(a, b);

        Assert.That(Equals(expected, sum), $"Forventet sum: {expected}, men sum var: {sum}");
    }

    [Test]
    public void TestSomDetKanskjeErNoeGaltMed()
    {
        int a = 4;
        int b = 1;
        int expected = 5;

        int sum = Kalkulator.Sum(a, b);

        Assert.That(Equals(expected, sum), $"Forventet sum: {expected}, men sum var: {sum}");
    }

    [Test]
    public void TestNegativeNumbers()
    {
        int a = -3;
        int b = -7;
        int expected = -10;

        int sum = Kalkulator.Sum(a, b);

        Assert.That(Equals(expected, sum), $"Forventet sum: {expected}, men sum var: {sum}");
    }

    public void TestSumFunction()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<SumFunction>>();
        var function = new SumFunction(loggerMock.Object);

        var context = new DefaultHttpContext();
        var request = context.Request;
        request.QueryString = new QueryString("?a=3&b=4");

        // Act
        var result = function.Run(request) as OkObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Summen av 3 og 4 er: 7.", result.Value);
    }
}
