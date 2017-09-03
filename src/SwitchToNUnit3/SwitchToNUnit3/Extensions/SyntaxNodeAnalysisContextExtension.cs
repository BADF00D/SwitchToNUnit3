using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace SwitchToNUnit3.Extensions
{
    public static class SyntaxNodeAnalysisContextExtension
    {
        public static void ReportReferencedPropertyInTestCaseSourceHasToBeStatic(this SyntaxNodeAnalysisContext context) {
            context.ReportDiagnostic(Diagnostic.Create(Rules.ReferencedPropertyInTestCasesSourceIsNotStaticRule, context.Node.GetLocation()));
        }

        public static void ReportReferencedFieldInTestCaseSourceHasToBeStatic(this SyntaxNodeAnalysisContext context) {
            context.ReportDiagnostic(Diagnostic.Create(Rules.ReferencedFieldInTestCasesSourceIsNotStaticRule, context.Node.GetLocation()));
        }

        public static void ReportReferencedMethodInTestCaseSourceHasToBeStatic(this SyntaxNodeAnalysisContext context) {
            context.ReportDiagnostic(Diagnostic.Create(Rules.ReferencedMethodInTestCasesSourceIsNotStaticRule, context.Node.GetLocation()));
        }

        public static void ReportThrowsIsDeprecated(this SyntaxNodeAnalysisContext context) {
            context.ReportDiagnostic(Diagnostic.Create(Rules.ThrowsDeprecatedRule, context.Node.GetLocation()));
        }

        public static void ReportAsyncVoidIsDeprecated(this SyntaxNodeAnalysisContext context) {
            context.ReportDiagnostic(Diagnostic.Create(Rules.AsyncVoidIsDeprectedRule, context.Node.GetLocation()));
        }
    }
}