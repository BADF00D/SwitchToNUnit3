using System.Linq;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using NUnit.Framework;
using SwitchToNUnit3.Test.TestCaseSourceNotStatic;

namespace SwitchToNUnit3.Test.TestFixture
{
    [TestFixture]
    internal class If_TestFixture_is_on_non_abstract_class: TestCaseSourceNotStaticSpec
    {
        public If_TestFixture_is_on_non_abstract_class()
        {
            Sut = new TestFixtureAbstractClassesAnalyzer();
        }
        private const string Code = @"
        using NUnit.Framework;
        namespace Testnamespace
        {
            [TestFixture]
            public class SomeTest
            {
            }
        }
        namespace NUnit.Framework {
            public class TestFixtureAttribute : Attribute{
                public ExpectedExceptionAttribute() {}
            }
        }";

        private Diagnostic[] _diagnostics;
        
        protected override void BecauseOf() {
            _diagnostics = MyHelper.RunAnalyser(Code, Sut);
        }

        [Test]
        public void Then_there_should_be_two_Diagnostics() {
            _diagnostics.Count().Should().Be(0);
        }
    }
}