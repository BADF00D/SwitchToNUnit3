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
        
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
            => ImmutableArray.Create(default(DiagnosticDescriptor));

        public override void Initialize(AnalysisContext context) {
        }
    }
}