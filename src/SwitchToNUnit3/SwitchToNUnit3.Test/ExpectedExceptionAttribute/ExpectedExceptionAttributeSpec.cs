using DisposableFixer.Test;
using NUnit.Framework;

namespace SwitchToNUnit3.Test.ExpectedExceptionAttribute
{
    [TestFixture]
    internal abstract class ExpectedExceptionAttributeSpec : Spec
    {
        protected readonly SwitchToNUnit3Analyzer Sut;

        protected ExpectedExceptionAttributeSpec()
        {
            Sut = new SwitchToNUnit3Analyzer();
        }
    }
}