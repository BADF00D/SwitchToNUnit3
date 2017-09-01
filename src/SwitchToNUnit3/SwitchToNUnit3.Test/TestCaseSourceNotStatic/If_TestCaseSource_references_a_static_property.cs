using System.Linq;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace SwitchToNUnit3.Test.TestCaseSourceNotStatic
{
    [TestFixture]
    internal class If_TestCaseSource_references_a_static_property : TestCaseSourceNotStaticSpec
    {
        private const string Code = @"
        using System;
        using System.Collections.Generic;
        using NUnit.Framework;
        namespace Testnamespace{
            public class SomeTest{
                [TestCaseSource(" + "Tests" + @")]
                [TestCaseSource(nameof(Tests))]
                public void SomeMethod(){}

                public static IEnumerable<TestCaseData> Tests{
                    get{
                        yield return new TestCaseData();
                    }
                }
            }
        }
        namespace NUnit.Framework {
            public class TestCaseSourceAttribute : Attribute {
                public TestCaseSourceAttribute(string name) { }
            }
            public class TestCaseData {

            }
        }";


        private Diagnostic[] _diagnostics;
        
        protected override void BecauseOf() {
            _diagnostics = MyHelper.RunAnalyser(Code, Sut);
        }

        [Test]
        public void Then_there_should_be_one_Diagnostics_for_STN002() {
            _diagnostics.Count().Should().Be(0);
        }
    }
}