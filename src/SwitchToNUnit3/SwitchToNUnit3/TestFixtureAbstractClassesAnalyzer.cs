using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using SwitchToNUnit3.Extensions;

namespace SwitchToNUnit3
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class TestFixtureAbstractClassesAnalyzer : DiagnosticAnalyzer
    {
        private const string TestFixtureAttribute = "NUnit.Framework.TestFixtureAttribute";
        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyseAttribute, SyntaxKind.Attribute);
        }

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
            ImmutableArray.Create(Rules.TestFixtureOnAbstractClassIsUselessRule);

        private static void AnalyseAttribute(SyntaxNodeAnalysisContext context) {
            var node = context.Node as AttributeSyntax;

            var nodeType = context.SemanticModel.GetTypeInfo(node).Type;
            var fullname = nodeType.GetFullNameWithNameSpace();
            if (fullname != TestFixtureAttribute) return;

            var @class = node.FindContainingClass();
            if (@class == null) return;
            var classType = context.SemanticModel.GetEnclosingSymbol(node.GetLocation().SourceSpan.Start);
            if (!classType.IsAbstract) return;

            context.ReportTestFixtureOnAbstractClassIsUseless();
        }
    }
}