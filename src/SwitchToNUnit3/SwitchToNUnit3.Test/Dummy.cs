//using System;
//using NUnit.Framework;
//namespace Testnamespace {
//    public class SomeTest {
//        [TestCaseSource("Tests")]
//        [TestCaseSource(nameof(Tests))]
//        public void SomeMethod() { }

//        public static TestCaseData[] Tests = new[] { new TestCaseData() };

//    }
//}
//namespace NUnit.Framework {
//    public class TestCaseSourceAttribute : Attribute {
//        public TestCaseSourceAttribute(string name) { }
//    }
//    public class TestCaseData {

//    }
//}