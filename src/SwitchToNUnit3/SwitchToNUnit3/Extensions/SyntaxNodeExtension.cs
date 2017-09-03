using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SwitchToNUnit3.Extensions
{
    public static class SyntaxNodeExtension
    {
        public static ClassDeclarationSyntax FindContainingClass(this SyntaxNode node) {
            while (true) {
                if (node.Parent == null) return null;
                var @class = node.Parent as ClassDeclarationSyntax;
                if (@class != null)
                    return @class;

                node = node.Parent;
            }
        }

        private const string TestCaseSource = "TestCaseSource";

        private static readonly Dictionary<string, DiagnosticDescriptor> DeprecatedAttributeDiagnostics = new Dictionary<string, DiagnosticDescriptor> {
            ["ExpectedException"] = Rules.ExpectedExceptionDeprecatedRule,
        };
        /// <summary>
        /// 
        /// </summary>
        public static bool TryGetDiagnosticIdForDeprecatedAttribute(this AttributeSyntax attribute,
            out DiagnosticDescriptor diagnsticId) {
            var attributeName = attribute?
                .DescendantNodes()
                .OfType<IdentifierNameSyntax>()
                .Select(ins => ins.Identifier.Text)
                .FirstOrDefault();
            return DeprecatedAttributeDiagnostics.TryGetValue(attributeName ?? string.Empty, out diagnsticId);
        }

        public static bool IsTestCaseSourceAttribute(this AttributeSyntax attribute) {
            return attribute?
                .DescendantNodes()
                .OfType<IdentifierNameSyntax>()
                .FirstOrDefault(ins => ins.Identifier.Text == TestCaseSource)
                != null;
        }

        public static string GetReferencedProjectOrDefault(this AttributeSyntax attribute) {
            var argument = attribute.DescendantNodes().OfType<AttributeArgumentSyntax>().FirstOrDefault();
            var literalExpression = argument?.Expression as LiteralExpressionSyntax;
            return literalExpression?.Token.ValueText;
        }
    }
}