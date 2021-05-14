## Dubstep.TestUtilities.TestLogger

Create a `Microsoft.Extensions.Logging.ILogger<T>` instance and assert log calls.

By default, `LogXXX` methods (e.g. `LogDebug`, `LogException`) are non-virtual members of a `ILogger` class, making it difficult to substituted. <sup>[Link](https://github.com/nsubstitute/NSubstitute.Analyzers/blob/master/documentation/rules/NS1001.md)</sup>

This package is created to make it eaiser to verify your log calls in unit tests. It holds a list of all the log statements and expose the list so you can perform asserts on it.

`LogStatements` exposes the entire list while `LastStatement` expose the last call.

### Usage
[![Dubstep.TestUtilities.TestLogger on fuget.org](https://www.fuget.org/packages/Dubstep.TestUtilities.TestLogger/badge.svg)](https://www.fuget.org/packages/Dubstep.TestUtilities.TestLogger)

```bash
dotnet add package Dubstep.TestUtilities.TestLogger
```

```csharp
[Test]
public void Should_Log_ExpectedMessage()
{
    // Arrange
    var loggerStub = new TestLogger<DubstepService>();
    var service = new DubstepService(loggerStub);
    var expectedMessage = "Task Completed."

    // Act
    service.DoSomethingAndLog();
    // loggerStub.LogDebug("Task Completed.");

    // Assert
    Assert.AreEqual(LogLevel.Debug, loggerStub.LastStatement.Level);
    Assert.AreEqual(expectedMessage, loggerStub.LastStatement.Message);
    // Or check `loggerStub.LogStatements` for complex asserts
}

[Test]
public void Should_Log_Exception()
{
    // Arrange
    var loggerStub = new TestLogger<DubstepService>();
    var service = new DubstepService(loggerStub);
    var expectedMessage = "Failed."

    // Act
    service.DoSomethingAndLogException();
    // loggerStub.LogError(exception, "Failed.");

    // Assert
    Assert.AreEqual(LogLevel.Error, loggerStub.LastStatement.Level);
    Assert.AreEqual(expectedMessage, loggerStub.LastStatement.Message);
    var exception = loggerStub.LastStatement.Exception;
    // add some asserts on the exception. `Type`, `Message`, `InnerException` etc.
}
```

