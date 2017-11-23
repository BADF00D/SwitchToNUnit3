using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
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

            var importForTaskAlreadyExists = root.DescendantNodes()
                .OfType<UsingDirectiveSyntax>()
                .Select(uss => uss.Name as QualifiedNameSyntax)
                .Where(qns => qns != null)
                .Any(qns => qns.Is("System", "Threading", "Tasks"));
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
            if (!importForTaskAlreadyExists)
            {
                var oldUsings = newroot.DescendantNodes().OfType<UsingDirectiveSyntax>();
                var importForTaskType = SyntaxFactory.UsingDirective(
                    SyntaxFactory.QualifiedName(
                        SyntaxFactory.QualifiedName(
                            SyntaxFactory.IdentifierName("System"),
                            SyntaxFactory.IdentifierName("Threading")),
                        SyntaxFactory.IdentifierName("Tasks")));
                
                var x = oldUsings.Last();
                newroot = newroot.InsertNodesAfter(x, new [] { importForTaskType });
            }

            return Task.FromResult(document.WithSyntaxRoot(newroot));
        }

        public override ImmutableArray<string> FixableDiagnosticIds => 
            ImmutableArray.Create(DiagnosticIds.AsyncVoidIsDeprected);
    }
}