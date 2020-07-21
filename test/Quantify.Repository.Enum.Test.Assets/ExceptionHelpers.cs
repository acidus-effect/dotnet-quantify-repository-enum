using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Quantify.Repository.Enum.Test.Assets
{
    public static class ExceptionHelpers
    {
        public static void ExpectException<TException>(Action actionCallback, Action<TException> exceptionCallback = null) where TException : Exception
        {
            ExpectExceptionAsync(() => Task.Run(() => actionCallback()), exceptionCallback).Wait();
        }

        public static async Task ExpectExceptionAsync<TException>(Func<Task> actionCallback, Action<TException> exceptionCallback = null) where TException : Exception
        {
            if (actionCallback == null)
                throw new ArgumentNullException(nameof(actionCallback));

            try
            {
                await actionCallback();

                Assert.IsTrue(false, $"Expected exception of type '{typeof(TException).FullName}' to be thrown.");
            }
            catch (TException exception)
            {
                exceptionCallback?.Invoke(exception);
                Assert.IsTrue(true);
            }
            catch (Exception exception)
            {
                Assert.IsTrue(false, $"Unexpected exception of type '{exception.GetType().FullName}' was thrown. Expected exception of type '{typeof(TException).FullName}'.");
            }
        }

        public static void ExpectArgumentNullException(string parameterName, Action actionCallback, Action<ArgumentNullException> exceptionCallback = null)
        {
            ExpectArgumentNullExceptionAsync(parameterName, () => Task.Run(() => actionCallback()), exceptionCallback).Wait();
        }

        public static async Task ExpectArgumentNullExceptionAsync(string parameterName, Func<Task> actionCallback, Action<ArgumentNullException> exceptionCallback = null)
        {
            if (actionCallback == null)
                throw new ArgumentNullException(nameof(actionCallback));

            await ExpectExceptionAsync<ArgumentNullException>(
                // Act
                async () => await actionCallback(),

                // Assert
                (exception) =>
                {
                    Assert.AreEqual(parameterName, exception.ParamName);
                    exceptionCallback?.Invoke(exception);
                }
            );
        }

        public static void ExpectArgumentException(string parameterName, Action actionCallback, Action<ArgumentException> exceptionCallback = null)
        {
            ExpectArgumentExceptionAsync(parameterName, () => Task.Run(() => actionCallback()), exceptionCallback).Wait();
        }

        public static async Task ExpectArgumentExceptionAsync(string parameterName, Func<Task> actionCallback, Action<ArgumentException> exceptionCallback = null)
        {
            if (actionCallback == null)
                throw new ArgumentNullException(nameof(actionCallback));

            await ExpectExceptionAsync<ArgumentException>(
                // Act
                async () => await actionCallback(),

                // Assert
                (exception) =>
                {
                    Assert.AreEqual(parameterName, exception.ParamName);
                    exceptionCallback?.Invoke(exception);
                }
            );
        }
    }
}