using System;
using Dubstep.TestUtilities;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace test_logger.tests
{
    public class TestLoggerTests
    {
        internal TestLogger<TestLoggerTests> targetLogger { get; set; }
        [SetUp]
        public void Setup()
        {
            targetLogger = new TestLogger<TestLoggerTests>();
        }

        [Test]
        [TestCase(LogLevel.Trace)]
        [TestCase(LogLevel.Debug)]
        [TestCase(LogLevel.Information)]
        [TestCase(LogLevel.Warning)]
        [TestCase(LogLevel.Error)]
        [TestCase(LogLevel.Critical)]
        [TestCase(LogLevel.None)]
        public void Should_Enable_LogLevel(LogLevel logLevel)
        {
            // Arrange

            // Act
            var enabled = targetLogger.IsEnabled(logLevel);

            // Assert
            Assert.IsTrue(enabled);
        }

        [Test]
        public void Should_LogDebug_PlainText()
        {
            // Arrange
            var expectedMessage = "this is a log message";

            // Act
            targetLogger.LogDebug(expectedMessage);

            // Assert
            Assert.IsNotNull(targetLogger.LastStatement);
            Assert.AreEqual(expectedMessage, targetLogger.LastStatement.Message);
            Assert.AreEqual(LogLevel.Debug, targetLogger.LastStatement.Level);
        }

        [Test]
        public void Should_LogInfo_PlainText()
        {
            // Arrange
            var expectedMessage = "this is a log message";

            // Act
            targetLogger.LogInformation(expectedMessage);

            // Assert
            Assert.IsNotNull(targetLogger.LastStatement);
            Assert.AreEqual(expectedMessage, targetLogger.LastStatement.Message);
            Assert.AreEqual(LogLevel.Information, targetLogger.LastStatement.Level);
        }

        [Test]
        public void Should_LogError_Exception()
        {
            // Arrange 
            var expectedMessage = "this is a log message";
            var expectedException = new Exception();

            // Act
            targetLogger.LogError(expectedException, expectedMessage);

            // Assert
            Assert.IsNotNull(targetLogger.LastStatement);
            Assert.AreEqual(expectedMessage, targetLogger.LastStatement.Message);
            Assert.AreEqual(LogLevel.Error, targetLogger.LastStatement.Level);
            Assert.AreEqual(expectedException, targetLogger.LastStatement.Exception);
        }

        [Test]
        public void Should_Keep_AllExceptions()
        {
            // Arrange
            var countOfLogRecords = 10;
            var messageFormat = "{0}";

            // Act
            for (var i = 0; i < countOfLogRecords; i++)
            {
                targetLogger.LogTrace(string.Format(messageFormat, i));
            }

            // Assert
            Assert.IsNotNull(targetLogger.LogStatements);
            for (var j = 0; j < countOfLogRecords; j++)
            {
                var expectedMessage = string.Format(messageFormat, j);
                var actualStatement = targetLogger.LogStatements[j];
                Assert.AreEqual(LogLevel.Trace, actualStatement.Level);
                Assert.AreEqual(expectedMessage, actualStatement.Message);
            }
        }
    }
}