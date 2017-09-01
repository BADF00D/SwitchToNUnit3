using FluentAssertions;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace SwitchToNUnit3.Test.ExpectedExceptionAttribute
{
    [TestFixture]
    internal class ExpectedExceptionAttribute_without_exception : ExpectedExceptionAttributeSpec
    {
        private const string Code = @"
        using System;
        using NUnit.Framework;
        namespace Testnamespace{
            public class SomeTest{
                [ExpectedException]
                public void SomeMethod(){}
            }
        }
        namespace NUnit.Framework {
            public class ExpectedExceptionAttribute : Attribute{
                public ExpectedExceptionAttribute() {}
            }
        }";

        private Diagnostic[] _diagnostics;


        protected override void BecauseOf()
        {
            _diagnostics = MyHelper.RunAnalyser(Code, Sut);
        }

        [Test]
        public void Then_there_should_be_one_Diagnostics_for_STN001()
        {
            _diagnostics.Length.Should().Be(1);

            _diagnostics[0].Id.Should().Be(DiagnosticIds.ExpectedExceptionAttributeisDeprecated);
        }
    }
}