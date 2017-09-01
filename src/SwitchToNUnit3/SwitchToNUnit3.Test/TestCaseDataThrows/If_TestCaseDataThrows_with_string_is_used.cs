using System.Linq;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using NUnit.Framework;
using SwitchToNUnit3.Test.IssueTest;

namespace SwitchToNUnit3.Test.TestCaseDataThrows
{
    [TestFixture]
    internal class If_TestCaseDataThrows_with_string_is_used : IssueSpec
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
            _diagnostics.Count().Should().Be(1);

            _diagnostics[0].Id.Should().Be(DiagnosticIds.ThrowsIsDeprecated);
        }
    }
}