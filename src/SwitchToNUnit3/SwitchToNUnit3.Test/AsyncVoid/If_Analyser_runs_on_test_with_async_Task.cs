using FluentAssertions;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace SwitchToNUnit3.Test.AsyncVoid
{
    internal class If_Analyser_runs_on_test_with_async_Task : AsyncVoidSpec
    {
        private const string Code = @"
        using System;
        using System.Threading.Tasks;
        using NUnit.Framework;
        namespace Testnamespace {
            public class SomeTest {
                [Test]
                public async Task SomeMethod()
                {
                    await Task.Delay(1);
                }
            }
        }
        namespace NUnit.Framework {
            public class TestAttribute : Attribute {
            }
        }";

        private Diagnostic[] _diagnostics;


        protected override void BecauseOf() {
            _diagnostics = MyHelper.RunAnalyser(Code, Sut);
        }

        [Test]
        public void Then_there_should_be_no_Diagnostics() {
            _diagnostics.Length.Should().Be(0);
        }
    }
}