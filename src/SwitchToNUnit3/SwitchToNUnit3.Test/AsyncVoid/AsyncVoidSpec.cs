using DisposableFixer.Test;
using NUnit.Framework;

namespace SwitchToNUnit3.Test.AsyncVoid
{
    [TestFixture]
    internal abstract class AsyncVoidSpec : Spec
    {
        protected readonly SwitchToNUnit3Analyzer Sut;

        protected AsyncVoidSpec()
        {
            Sut = new SwitchToNUnit3Analyzer();
        }
    }
}