using FluentAssertions;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace SwitchToNUnit3.Test.AsyncVoid
{
    internal class If_Analyser_runs_on_test_with_async_void : AsyncVoidSpec
    {
        private const string Code = @"
        using System;
        using NUnit.Framework;
        namespace Testnamespace {
            public class SomeTest {
                [Test]
                public async void SomeMethod() { }
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
        public void Then_there_should_be_one_Diagnostics() {
            _diagnostics.Length.Should().Be(1);

            _diagnostics[0].Id.Should().Be(DiagnosticIds.AsyncVoidIsDeprected);
        }
    }
}