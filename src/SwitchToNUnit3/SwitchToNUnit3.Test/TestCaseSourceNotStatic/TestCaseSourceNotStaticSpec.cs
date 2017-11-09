using DisposableFixer.Test;
using Microsoft.CodeAnalysis.Diagnostics;
using NUnit.Framework;

namespace SwitchToNUnit3.Test.TestCaseSourceNotStatic
{
    [TestFixture]
    internal abstract class TestCaseSourceNotStaticSpec : Spec
    {
        protected DiagnosticAnalyzer Sut;

        protected TestCaseSourceNotStaticSpec()
        {
            Sut = new SwitchToNUnit3Analyzer();
        }
    }
}