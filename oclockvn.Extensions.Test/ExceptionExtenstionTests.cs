using NUnit.Framework;
using System;

namespace oclockvn.Extensions.Test
{
    [TestFixture]
    public class ExceptionExtenstionTests
    {
        [Test]
        public void ShouldReturnExceptionWithInnerLevel1()
        {
            string msg = string.Empty;

            try
            {
                var zero = 0;
                var ex = 1 / zero;
            }
            catch (Exception ex)
            {
                msg = ex.ToErrorMessage();
            }

            Assert.IsNotEmpty(msg);
        }
    }
}
