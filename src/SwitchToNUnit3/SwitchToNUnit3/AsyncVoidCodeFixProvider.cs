using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

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
                    c => MakeMethodAsyncTaskAsync(context.Document, root, declaration, c),
                    Title)
                , diagnostic);
        }

        private static Task<Document> MakeMethodAsyncTaskAsync(Document document, SyntaxNode root, MethodDeclarationSyntax oldmethod, CancellationToken cancellationToken)
        {
            var wns = root.DescendantNodes()
                .OfType<UsingDirectiveSyntax>()
                .Select(uss => uss.Name as QualifiedNameSyntax)
                .ToArray();

            var usingForTaskAlreadyExists = root.DescendantNodes()
                .OfType<UsingDirectiveSyntax>()
                .Select(uss => uss.Name as QualifiedNameSyntax)
                .Where(qns => qns != null)
                .Any(qns => (qns.Left as IdentifierNameSyntax)?.Identifier.Text == "NUnit" 
                            && (qns.Right as IdentifierNameSyntax)?.Identifier.Text == "Framework");
            var oldAttribtues = oldmethod.AttributeLists;


            var returnType = SyntaxFactory.IdentifierName("Task");

            var newmethod = SyntaxFactory.MethodDeclaration(oldAttribtues,
                oldmethod.Modifiers,
                returnType,
                oldmethod.ExplicitInterfaceSpecifier,
                oldmethod.Identifier,
                oldmethod.TypeParameterList,
                oldmethod.ParameterList,
                oldmethod.ConstraintClauses,
                oldmethod.Body,
                oldmethod.ExpressionBody);

            var newroot = root.ReplaceNode(oldmethod, newmethod);
            if (!usingForTaskAlreadyExists)
            {
                var oldUsings = root.DescendantNodes().OfType<UsingDirectiveSyntax>();
                var usingTask = SyntaxFactory.UsingDirective(
            SyntaxFactory.QualifiedName(
                SyntaxFactory.QualifiedName(
                    SyntaxFactory.IdentifierName("System"),
                    SyntaxFactory.IdentifierName("Threading")),
                SyntaxFactory.IdentifierName("Tasks")));
                var newUsings = oldUsings.Concat(new [] { usingTask });

                newroot = newroot.ReplaceSyntax(oldUsings, newUsings);
            }

            return Task.FromResult(document.WithSyntaxRoot(newroot));

        }

        public override ImmutableArray<string> FixableDiagnosticIds => 
            ImmutableArray.Create(DiagnosticIds.AsyncVoidIsDeprected);
    }
}