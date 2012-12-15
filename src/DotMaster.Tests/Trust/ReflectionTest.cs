using NUnit.Framework;

namespace DotMaster.Tests.Trust
{
    public class ReflectionTest
    {
        [Test]
        public void PropertyEquals()
        {
            var prop1 = typeof (TestTypes.A).GetProperty("MyProperty");
            var prop2 = typeof (TestTypes.A).GetProperty("MyProperty");

            Assert.That(prop1, Is.EqualTo(prop2));
        }

        [Test]
        public void PropertyNotEquals()
        {
            var prop1 = typeof (TestTypes.A).GetProperty("MyProperty");
            var prop2 = typeof (TestTypes.B).GetProperty("MyProperty");

            Assert.That(prop1, Is.Not.EqualTo(prop2));
        }

        [Test]
        public void SameNamedProperties()
        {
            var prop1 = typeof (TestTypes.D).GetProperty("MyProperty");
            var prop2 = typeof (TestTypes.A).GetProperty("MyProperty");

            Assert.That(prop1, Is.Not.EqualTo(prop2));
        }

        [Test]
        public void SameNamedProperties2()
        {
            var prop1 = typeof (TestTypes.E).GetProperty("MyProperty");
            var prop2 = typeof (TestTypes.D).GetProperty("MyProperty");

            Assert.That(prop1, Is.Not.EqualTo(prop2));
        }
    }
}