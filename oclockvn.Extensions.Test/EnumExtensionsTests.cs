using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace oclockvn.Extensions.Test
{
    [TestFixture]
    public class EnumExtensionsTests
    {
        internal enum Tests
        {
            [Display(Name = "First value")]
            FirstValue,
            SecondValue
        }

        [Test]
        public void ShouldReturnCorrentDisplayName()
        {
            var first = Tests.FirstValue;
            var second = Tests.SecondValue;

            Assert.AreEqual("First value", first.ToDisplayName());
            Assert.AreEqual("SecondValue", second.ToDisplayName());
        }
    }
}
