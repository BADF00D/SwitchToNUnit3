using System.Linq;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace SwitchToNUnit3.Test.IssueTest
{
    [TestFixture]
    internal class If_analyser_runs_on_code_from_issue16 : IssueSpec
    {
        private const string Code = @"
using NUnit.Framework;
namespace MissingTestCasesProperty
{
    public class No2
    {

        [TestCaseSource(""TestCases"")]
        public void Bla() {

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

            _diagnostics[0].Id.Should().Be(DiagnosticIds.ReferencedPropertyDoesNotExists);
        }
    }
}