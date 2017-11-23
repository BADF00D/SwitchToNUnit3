using DisposableFixer.Test;
using Microsoft.CodeAnalysis.Diagnostics;
using NUnit.Framework;

namespace SwitchToNUnit3.Test.IssueTest
{
    [TestFixture]
    internal abstract class IssueSpec : Spec
    {
        protected DiagnosticAnalyzer Sut;

        protected IssueSpec()
        {
            Sut = new SwitchToNUnit3Analyzer();
        }
    }
}