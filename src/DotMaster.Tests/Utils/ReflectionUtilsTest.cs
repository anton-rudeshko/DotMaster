using DotMaster.Core.Utils;
using NUnit.Framework;

namespace DotMaster.Tests.Utils
{
    [TestFixture]
    public class ReflectionUtilsTest
    {
        [Test]
        public void MyTest()
        {
            Assert.That(ReflectionUtils.NameOf((TestBO x) => x.MyProperty), Is.EqualTo("MyProperty"));
        }
    }
}