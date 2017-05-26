using NUnit.Framework;

namespace oclockvn.Extensions.Test
{
    [TestFixture]
    public class StringExtensionTests
    {
        [Test]
        public void ShouldReturnCorrectFriendlyUrl()
        {
            var samsung = "sản phẩm samsung galaxy a7 2017";
            var check = "Kiểm tra có hàng tại nơi bạn ở không?";
            var mark = "(đường dẫn [có dấu ngoặc]  và nhiều khoảng trắng     )";
            var symbol = "----có dấu gạch__ * khác với gạch ngang!@#$% aaa";

            var samsungUrl = samsung.ToFriendlyUrl();
            var checkUrl = check.ToFriendlyUrl();
            var markUrl = mark.ToFriendlyUrl();
            var symbolUrl = symbol.ToFriendlyUrl();

            Assert.AreEqual("san-pham-samsung-galaxy-a7-2017", samsungUrl);
            Assert.AreEqual("kiem-tra-co-hang-tai-noi-ban-o-khong", checkUrl);
            Assert.AreEqual("duong-dan-co-dau-ngoac-va-nhieu-khoang-trang", markUrl);
            Assert.AreEqual("co-dau-gach-khac-voi-gach-ngang-aaa", symbolUrl);
        }
    }
}
