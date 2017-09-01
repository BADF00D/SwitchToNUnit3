using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace SwitchToNUnit3 {

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SwitchToNUnit3Analyzer : DiagnosticAnalyzer {

        

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Obsolete";
        private static DiagnosticDescriptor ExpectedExceptionDeprecatedRule = new DiagnosticDescriptor(
            DiagnosticIds.ExpectedExceptionAttributeisDeprecated,
            Title,
            MessageFormat,
            Category,
            DiagnosticSeverity.Error, 
            true,
            Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
            => ImmutableArray.Create(ExpectedExceptionDeprecatedRule);

        public override void Initialize(AnalysisContext context) {
            context.RegisterSyntaxNodeAction(AnalyseAttribute, SyntaxKind.Attribute);
        }

        private void AnalyseAttribute(SyntaxNodeAnalysisContext context) {
            var node = context.Node as AttributeSyntax;
            if (node == null) return;
            if (node.IsExpectedExceptionAttribute()) {
                context.ReportDiagnostic(Diagnostic.Create(ExpectedExceptionDeprecatedRule, node.GetLocation()));
            } 
        }
    }
}