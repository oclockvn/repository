using NUnit.Framework;
using System;

namespace oclockvn.Extensions.Test
{
    [TestFixture]
    public class DateTimeExtensionTest
    {
        [Test]
        public void ShouldReturnCorrectVietnameseTimeAgo()
        {
            var now = DateTime.Now;

            var yesterday = now.AddDays(-1).AddHours(-now.Hour).ToTimeAgo();
            var dayAgo = now.AddDays(-2).ToTimeAgo();
            var monthAgo = now.AddMonths(-1).ToTimeAgo();
            var yearAgo = now.AddYears(-2).ToTimeAgo();

            Assert.AreEqual("ngày hôm qua", yesterday);            
            Assert.AreEqual("2 ngày trước", dayAgo);
            Assert.AreEqual("1 tháng trước", monthAgo);
            Assert.AreEqual("2 năm trước", yearAgo);
        }

        [Test]
        public void ShouldReturnCorrectEnglishTimeAgo()
        {
            var now = DateTime.Now;
            var lang = "en";

            var yesterday = now.AddDays(-1).AddHours(-now.Hour).ToTimeAgo(lang);
            var dayAgo = now.AddDays(-2).ToTimeAgo(lang);
            var monthAgo = now.AddMonths(-1).ToTimeAgo(lang);
            var monthsAgo = now.AddMonths(-2).ToTimeAgo(lang);
            var yearAgo = now.AddYears(-2).ToTimeAgo(lang);

            Assert.AreEqual("yesterday", yesterday);
            Assert.AreEqual("2 days ago", dayAgo);
            Assert.AreEqual("a month ago", monthAgo);
            Assert.AreEqual("2 months ago", monthsAgo);
            Assert.AreEqual("2 years ago", yearAgo);
        }
    }
}
