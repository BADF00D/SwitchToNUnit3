using System.Linq;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using NUnit.Framework;

namespace SwitchToNUnit3.Test.IssueTest
{
    [TestFixture]
    internal class If_analyser_runs_on_code_from_Issue13 : IssueSpec
    {
        private const string Code = @"
        using System;
        using System.Xml.Serialization;
        namespace Testnamespace {
            [XmlRoot(" + "Test" + @")]
            [Serializable]
            public class SomeTest {
                [XmlElement(" + "\"Xml\"" + @")]
                [XmlChoiceIdentifier(" + "\"asdsad\"" + @")]
                public string SomeMethod { get; set; }
                public string Xml { get; set; }
            }
        }";

        private Diagnostic[] _diagnostics;

        protected override void BecauseOf()
        {
            _diagnostics = MyHelper.RunAnalyser(Code, Sut);
        }

        [Test]
        public void Then_there_should_be_one_Diagnostics()
        {
            _diagnostics.Count().Should().Be(0);
        }
    }
}