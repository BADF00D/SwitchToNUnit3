using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SwitchToNUnit3.Extensions
{
    public static class SyntaxNodeExtension
    {
        public static ClassDeclarationSyntax FindContainingClass(this SyntaxNode node) {
            while (true) {
                if (node.Parent == null) return null;
                var @class = node.Parent as ClassDeclarationSyntax;
                if (@class != null)
                    return @class;

                node = node.Parent;
            }
        }
    }
}