using System;
using System.Linq;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace SwitchToNUnit3.Test.IssueTest {
    [TestFixture]
    internal class IfAnalyserRunsOnCodeFromIssue1 : IssueSpec {
        private readonly string _code = @"
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


        protected override void BecauseOf() {
            _diagnostics = MyHelper.RunAnalyser(_code, Sut);
        }

        [Test]
        public void Then_there_should_be_one_Diagnostics_for_STN001()
        {
            _diagnostics.Count().Should().Be(1);

            _diagnostics[0].Id.Should().Be(DiagnosticIds.ExpectedExceptionAttributeisDeprecated);
        } 
    }
}
