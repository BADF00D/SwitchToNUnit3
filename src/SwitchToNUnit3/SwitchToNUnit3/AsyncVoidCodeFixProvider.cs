using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SwitchToNUnit3
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(AsyncVoidCodeFixProvider)), Shared]
    public class AsyncVoidCodeFixProvider : CodeFixProvider
    {
        private static string Title { get; } = "Make static"; //todo localize

        public override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;


            var declaration = root.FindToken(diagnosticSpan.Start)
                .Parent.AncestorsAndSelf()
                .OfType<MethodDeclarationSyntax>()
                .First();

            context.RegisterCodeFix(
                CodeAction.Create(
                    Title,
                    c => MakeMethodAsyncTaskAsync(context.Document, declaration, c),
                    Title)
                , diagnostic);
        }

        private static Task<Document> MakeMethodAsyncTaskAsync(Document document, MethodDeclarationSyntax declaration, CancellationToken cancellationToken)
        {
            return Task.FromResult(document);
        }

        public override ImmutableArray<string> FixableDiagnosticIds => 
            ImmutableArray.Create(DiagnosticIds.AsyncVoidIsDeprected);
    }
}