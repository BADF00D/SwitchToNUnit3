using System.Linq;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace SwitchToNUnit3.Test.IssueTest
{
    [TestFixture]
    internal class If_analyser_runs_on_code_from_issue2 : IssueSpec
    {
        private const string Code = @"
        using System;
        using System.Collections.Generic;
        using NUnit.Framework;
        namespace Testnamespace {
            public class SomeTest {
                public IEnumerable<TestCaseData> Tests {
                    get {
                        yield return new TestCaseData().Throws(nameof(Exception));
                    }
                }
            }
        }
        namespace NUnit.Framework {
            public class TestCaseSourceAttribute : Attribute {
                public TestCaseSourceAttribute(string name) { }
            }
            public class TestCaseData {
                public TestCaseData Throws(string exceptionName) {
                    return this;
                }
                public TestCaseData Throws(Type exceptionType) {
                    return this;
                }
            }
        }";

        private Diagnostic[] _diagnostics;


        protected override void BecauseOf()
        {
            _diagnostics = MyHelper.RunAnalyser(Code, Sut);
        }

        [Test]
        public void Then_there_should_be_one_Diagnostics()
        {
            _diagnostics.Length.Should().Be(1);

            _diagnostics[0].Id.Should().Be(DiagnosticIds.ThrowsIsDeprecated);
        }
    }
}