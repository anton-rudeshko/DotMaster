using DotMaster.Core.Trust.Attributes;

namespace DotMaster.Tests.Trust
{
    public static class TestTypes
    {
        public class A
        {
            public int MyProperty { get; set; }
        }

        public class B
        {
            public int MyProperty { get; set; }
        }

        public class D : TestTypes.A
        {
            public new int MyProperty { get; set; }
        }

        public class E : TestTypes.D
        {

        }

        [FixedScore(10)]
        [FixedScore(30, ForSource = "AAA")]
        [FixedScore(50, ForSource = "CCC")]
        public class C
        {
            public int NoTrust { get; set; }

            [GenericTrustStrategy(typeof(TestTrustStrategy), ForSource = "AAA")]
            public int MyProperty { get; set; }

            [FixedScore(10)]
            public int MyProperty1 { get; set; }
            
            [LinearDecrease(From = 90, To = 10, Decay = 50)]
            [GenericTrustStrategy(typeof(TestTrustStrategy), ForSource = "AAA")]
            public int MyProperty2 { get; set; }
            
            [LinearDecrease(From = 90, To = 10, Decay = 50)]
            [GenericTrustStrategy(typeof(TestTrustStrategy), ForSource = "BBB")]
            [FixedScore(5, ForSource = "CCC")]
            public int MyProperty3 { get; set; }
        }
    }
}