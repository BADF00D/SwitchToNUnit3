using DisposableFixer.Test;
using NUnit.Framework;

namespace SwitchToNUnit3.Test.TestCaseDataThrows
{
    [TestFixture]
    internal abstract class TestCaseDataThrowsSpec : Spec
    {
        protected readonly SwitchToNUnit3Analyzer Sut;

        protected TestCaseDataThrowsSpec()
        {
            Sut = new SwitchToNUnit3Analyzer();
        }
    }
}