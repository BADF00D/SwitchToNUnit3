using DisposableFixer.Test;
using NUnit.Framework;

namespace SwitchToNUnit3.Test.IssueTest
{
    [TestFixture]
    internal abstract class IssueSpec : Spec
    {
        protected readonly SwitchToNUnit3Analyzer Sut;

        protected IssueSpec()
        {
            Sut = new SwitchToNUnit3Analyzer();
        }
    }
}