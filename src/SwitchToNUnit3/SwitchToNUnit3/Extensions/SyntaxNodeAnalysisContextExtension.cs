using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace SwitchToNUnit3.Extensions
{
    public static class SyntaxNodeAnalysisContextExtension
    {
        public static void ReportExpectedExceptionIsDeprecated(this SyntaxNodeAnalysisContext context)
        {
            context.ReportDiagnostic(Diagnostic.Create(Rules.ExpectedExceptionDeprecatedRule, context.Node.GetLocation()));
        }

        public static void ReportReferencedTestCaseSourceHasToBeStatic(this SyntaxNodeAnalysisContext context) {
            context.ReportDiagnostic(Diagnostic.Create(Rules.ReferencedTestCasesSourceIsNotStatic, context.Node.GetLocation()));
        }

        public static void ReportThrowsIsDeprecated(this SyntaxNodeAnalysisContext context) {
            context.ReportDiagnostic(Diagnostic.Create(Rules.ThrowsDeprecatedRule, context.Node.GetLocation()));
        }
    }
}