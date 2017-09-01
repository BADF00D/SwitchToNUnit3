using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SwitchToNUnit3 {

    internal static class SyntaxNodeExtensions {
        private const string _EXPECTED_EXCEPTION = "ExpectedException";
        private const string _TEST_CASE_SOURCE = "TestCaseSource";

        public static bool IsExpectedExceptionAttribute(this AttributeSyntax attribute) {
            return attribute?
                .DescendantNodes()
                .OfType<IdentifierNameSyntax>()
                .FirstOrDefault(ins => ins.Identifier.Text == _EXPECTED_EXCEPTION)
                != null;
        }

        public static bool IsTestCaseSourceAttribute(this AttributeSyntax attribute) {
            return attribute?
                .DescendantNodes()
                .OfType<IdentifierNameSyntax>()
                .FirstOrDefault(ins => ins.Identifier.Text == _TEST_CASE_SOURCE)
                != null;
        }

        public static string GetReferencedProjectOrDefault(this AttributeSyntax attribute) {
            var argument = attribute.DescendantNodes().OfType<AttributeArgumentSyntax>().FirstOrDefault();
            var literalExpression = argument?.Expression as LiteralExpressionSyntax;
            return literalExpression?.Token.ValueText;
        }
    }
}