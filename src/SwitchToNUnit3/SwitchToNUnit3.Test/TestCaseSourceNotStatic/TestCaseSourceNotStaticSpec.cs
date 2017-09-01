using DisposableFixer.Test;
using NUnit.Framework;

namespace SwitchToNUnit3.Test.TestCaseSourceNotStatic
{
    [TestFixture]
    internal abstract class TestCaseSourceNotStaticSpec : Spec
    {
        protected readonly SwitchToNUnit3Analyzer Sut;

        protected TestCaseSourceNotStaticSpec()
        {
            Sut = new SwitchToNUnit3Analyzer();
        }
    }
}