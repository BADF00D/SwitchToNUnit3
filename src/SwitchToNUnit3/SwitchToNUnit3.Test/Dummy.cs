using System;
using NUnit.Framework;
namespace Testnamespace {
    public class SomeTest {
        [ExpectedException]
        public void SomeMethod() { }
    }
}
namespace NUnit.Framework {
    public class ExpectedExceptionAttribute : Attribute {
        public ExpectedExceptionAttribute() { }
    }
}