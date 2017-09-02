using DisposableFixer.Test;
using NUnit.Framework;

namespace SwitchToNUnit3.Test.Attribute
{
    [TestFixture]
    internal abstract class AttributeSpec : Spec
    {
        protected readonly SwitchToNUnit3Analyzer Sut;

        protected AttributeSpec()
        {
            Sut = new SwitchToNUnit3Analyzer();
        }
    }
}