using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace SwitchToNUnit3.Extensions
{
    public static class SyntaxNodeAnalysisContextExtension
    {
        public static void ReportReferencedTestCaseSourceHasToBeStatic(this SyntaxNodeAnalysisContext context) {
            context.ReportDiagnostic(Diagnostic.Create(Rules.ReferencedTestCasesSourceIsNotStaticRule, context.Node.GetLocation()));
        }

        public static void ReportThrowsIsDeprecated(this SyntaxNodeAnalysisContext context) {
            context.ReportDiagnostic(Diagnostic.Create(Rules.ThrowsDeprecatedRule, context.Node.GetLocation()));
        }

        public static void ReportAsyncVoidIsDeprecated(this SyntaxNodeAnalysisContext context) {
            context.ReportDiagnostic(Diagnostic.Create(Rules.AsyncVoidIsDeprectedRule, context.Node.GetLocation()));
        }

        public static void ReportTestFixtureOnAbstractClassIsUseless(this SyntaxNodeAnalysisContext context) {
            context.ReportDiagnostic(Diagnostic.Create(Rules.TestFixtureOnAbstractClassIsUselessRule, context.Node.GetLocation()));
        }
    }
}