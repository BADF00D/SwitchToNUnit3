using System.Linq;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace SwitchToNUnit3.Test.IssueTest
{
    [TestFixture]
    internal class If_analyser_runs_on_code_from_issue6 : IssueSpec
    {
        private const string Code = @"
        using System;
        using NUnit.Framework;
        namespace Testnamespace {
            public class SomeTest {
                [TestFixtureSetUp]
                public async void SomeMethod() { }
            }
        }
        namespace NUnit.Framework {
            public class TestFixtureSetUpAttribute : Attribute {
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

            _diagnostics[0].Id.Should().Be(DiagnosticIds.TestFixtureSetUpAttributeIsDeprecated);
        }
    }
}