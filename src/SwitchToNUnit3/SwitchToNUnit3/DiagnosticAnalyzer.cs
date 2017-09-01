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
        private const string Category = "Obsolete";

        private static readonly LocalizableString Title = new LocalizableResourceString(
            nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof (Resources));

        private static readonly LocalizableString MessageFormat =
            new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager,
                typeof (Resources));

        private static readonly LocalizableString MessageFormat3 =
            new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat3), Resources.ResourceManager,
                typeof(Resources));

        private static readonly LocalizableString Description =
            new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager,
                typeof (Resources));

        private static readonly LocalizableString Description3 =
            new LocalizableResourceString(nameof(Resources.AnalyzerDescription3), Resources.ResourceManager,
                typeof(Resources));

        private static readonly DiagnosticDescriptor ExpectedExceptionDeprecatedRule = new DiagnosticDescriptor(
            DiagnosticIds.ExpectedExceptionAttributeisDeprecated,
            Title,
            MessageFormat,
            Category,
            DiagnosticSeverity.Error,
            true,
            Description);

        private static readonly DiagnosticDescriptor ReferencedTestCasesSourceIsNotStatic = new DiagnosticDescriptor(
            DiagnosticIds.ReferencedTestCaseSourceHasToBeStatic, 
            Title, 
            MessageFormat3, 
            Category, 
            DiagnosticSeverity.Error, true, 
            Description3);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
            => ImmutableArray.Create(ExpectedExceptionDeprecatedRule, ReferencedTestCasesSourceIsNotStatic);

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyseAttribute, SyntaxKind.Attribute);
        }

        private void AnalyseAttribute(SyntaxNodeAnalysisContext context)
        {
            var node = context.Node as AttributeSyntax;
            if (node == null) return;
            if (node.IsExpectedExceptionAttribute())
            {
                context.ReportDiagnostic(Diagnostic.Create(ExpectedExceptionDeprecatedRule, node.GetLocation()));
            }
            else
            {
                AnalyseTestCaseSourceAttribute(context, node);
            }
        }

        private void AnalyseTestCaseSourceAttribute(SyntaxNodeAnalysisContext context, AttributeSyntax node) {
            var argument = node
                .DescendantNodes()
                .OfType<AttributeArgumentSyntax>()
                .FirstOrDefault();
            if (argument == null) return;
            var name_of_testcase_member = GetName(argument);
            if (string.IsNullOrWhiteSpace(name_of_testcase_member)) return;

            var containing_class = node.FindContainingClass();
            if (containing_class == null) return;

            var property = containing_class
                .DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .FirstOrDefault(pds => pds.Identifier.Text == name_of_testcase_member);
            if (property != null) {
                var symbol = context.SemanticModel.GetDeclaredSymbol(property);
                if (symbol.IsStatic) return;

                context.ReportDiagnostic(Diagnostic.Create(ReferencedTestCasesSourceIsNotStatic, argument.GetLocation()));
                return;
            }
            var method = containing_class
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .FirstOrDefault(mds => mds.Identifier.Text == name_of_testcase_member);
            if (method != null) {
                var symbol = context.SemanticModel.GetDeclaredSymbol(method);
                if (symbol.IsStatic) return;
                context.ReportDiagnostic(Diagnostic.Create(ReferencedTestCasesSourceIsNotStatic, argument.GetLocation()));
                return;
            }
            var field = containing_class
                .DescendantNodes()
                .OfType<FieldDeclarationSyntax>()
                .SelectMany(fds => fds.DescendantNodes().OfType<VariableDeclaratorSyntax>())
                .FirstOrDefault(vds => vds.Identifier.Text == name_of_testcase_member);
            if (field != null) {
                var symbol = context.SemanticModel.GetDeclaredSymbol(field);
                if (symbol.IsStatic) return;
                context.ReportDiagnostic(Diagnostic.Create(ReferencedTestCasesSourceIsNotStatic, argument.GetLocation()));
                return;
            }
        }

        private static string GetName(AttributeArgumentSyntax argument) {
            //argument can be nameof(TestCases) or "TestCases"
            if (argument.Expression is LiteralExpressionSyntax) {
                return (argument.Expression as LiteralExpressionSyntax).Token.ValueText;
            }
            //search for argument of nameof()
            return argument
                .DescendantNodes()
                .OfType<ArgumentSyntax>()
                .SelectMany(@as => @as.DescendantNodes().OfType<IdentifierNameSyntax>())
                .FirstOrDefault()
                .Identifier
                .Text;
        }
    }
}