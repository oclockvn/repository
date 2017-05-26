using NUnit.Framework;

namespace oclockvn.Extensions.Test
{
    [TestFixture]
    public class MoneyExtensionTests
    {
        [Test]
        public void TestRoundMoney()
        {
            decimal d1 = 500.01m;
            decimal d2 = 1520.0m;
            decimal d3 = 22875.2m;

            int i1 = d1.ToVnd();
            int i2 = d2.ToVnd();
            int i3 = d3.ToVnd();

            Assert.AreEqual(500, i1);
            Assert.AreEqual(1500, i2);
            Assert.AreEqual(23000, i3);
        }
    }
}
