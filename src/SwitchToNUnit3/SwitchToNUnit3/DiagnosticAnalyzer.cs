using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using SwitchToNUnit3.Extensions;

namespace SwitchToNUnit3
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SwitchToNUnit3Analyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
            => ImmutableArray.Create(
                Rules.ExpectedExceptionDeprecatedRule,
                Rules.ReferencedPropertyInTestCasesSourceIsNotStaticRule,
                Rules.ThrowsDeprecatedRule,
                Rules.AsyncVoidIsDeprectedRule);

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyseAttribute, SyntaxKind.Attribute);
            context.RegisterSyntaxNodeAction(AnalyseInvocationExpression, SyntaxKind.SimpleMemberAccessExpression);
            context.RegisterSyntaxNodeAction(AnalyseMethodDeclaration, SyntaxKind.MethodDeclaration);
        }

        private static void AnalyseMethodDeclaration(SyntaxNodeAnalysisContext context)
        {
            var method = context.Node as MethodDeclarationSyntax;
            if (method == null) return;

            var attributes = method.DescendantNodes().OfType<AttributeSyntax>();

            if (
                !attributes.Any(
                    att =>
                        att.DescendantNodes()
                            .OfType<IdentifierNameSyntax>()
                            .Any(ins => ins.Identifier.Text == "Test")))
            {
                return;
            }
            var methodSymbol = context.SemanticModel.GetDeclaredSymbol(method);
            if (!methodSymbol.IsAsync) return;
            if (methodSymbol.ReturnsVoid)
            {
                context.ReportAsyncVoidIsDeprecated();
            }
        }

        private static void AnalyseInvocationExpression(SyntaxNodeAnalysisContext context)
        {
            var node = context.Node as MemberAccessExpressionSyntax;
            if (node == null) return;

            var symbolInfo = context.SemanticModel.GetSymbolInfo(node);
            var symbol = symbolInfo.Symbol as IMethodSymbol;

            var type = symbol?.ReturnType as INamedTypeSymbol;
            var fullname = type?.GetFullNameWithNameSpace();
            if (fullname != "NUnit.Framework.TestCaseData") return;
            var ids = node.DescendantNodes().OfType<IdentifierNameSyntax>().ToArray();
            var throws = ids.LastOrDefault(id => id.Identifier.Text == "Throws");
            if (throws != null)
            {
                context.ReportThrowsIsDeprecated();
            }
        }

        private static void AnalyseAttribute(SyntaxNodeAnalysisContext context)
        {
            var node = context.Node as AttributeSyntax;
            if (node == null) return;
            DiagnosticDescriptor deprecatedAttributeDiagnosticId;
            if (node.TryGetDiagnosticIdForDeprecatedAttribute(out deprecatedAttributeDiagnosticId))
            {
                context.ReportDiagnostic(Diagnostic.Create(deprecatedAttributeDiagnosticId, node.GetLocation()));
            }
            else
            {
                AnalyseTestCaseSourceAttribute(context, node);
            }
        }

        private static void AnalyseTestCaseSourceAttribute(SyntaxNodeAnalysisContext context, AttributeSyntax node)
        {
            var argument = node
                .DescendantNodes()
                .OfType<AttributeArgumentSyntax>()
                .FirstOrDefault();
            if (argument == null) return;
            var nameOfTestcaseMember = GetNameOrDefault(argument);
            if (string.IsNullOrWhiteSpace(nameOfTestcaseMember)) return;

            var containingClass = node.FindContainingClass();
            if (containingClass == null) return;

            var property = containingClass
                .DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .FirstOrDefault(pds => pds.Identifier.Text == nameOfTestcaseMember);
            if (property != null)
            {
                var symbol = context.SemanticModel.GetDeclaredSymbol(property);
                if (symbol.IsStatic) return;

                context.ReportReferencedPropertyInTestCaseSourceHasToBeStatic();
                return;
            }
            var method = containingClass
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .FirstOrDefault(mds => mds.Identifier.Text == nameOfTestcaseMember);
            if (method != null)
            {
                var symbol = context.SemanticModel.GetDeclaredSymbol(method);
                if (symbol.IsStatic) return;
                context.ReportReferencedMethodInTestCaseSourceHasToBeStatic();
                return;
            }
            var field = containingClass
                .DescendantNodes()
                .OfType<FieldDeclarationSyntax>()
                .SelectMany(fds => fds.DescendantNodes().OfType<VariableDeclaratorSyntax>())
                .FirstOrDefault(vds => vds.Identifier.Text == nameOfTestcaseMember);
            if (field != null)
            {
                var symbol = context.SemanticModel.GetDeclaredSymbol(field);
                if (symbol.IsStatic) return;
                context.ReportReferencedFieldInTestCaseSourceHasToBeStatic();
            }
        }

        private static string GetNameOrDefault(AttributeArgumentSyntax argument)
        {
            //argument can be nameof(TestCases) or "TestCases"
            var syntax = argument.Expression as LiteralExpressionSyntax;
            if (syntax != null) return syntax.Token.ValueText;

            //search for argument of nameof()
            var identifier = argument
                .DescendantNodes()
                .OfType<ArgumentSyntax>()
                .SelectMany(@as => @as.DescendantNodes().OfType<IdentifierNameSyntax>())
                .FirstOrDefault();

            return identifier?.Identifier.Text;
        }
    }
}