using Microsoft.CodeAnalysis;

namespace SwitchToNUnit3
{
    public static class Rules
    {
        private const string Category = "Obsolete";

        private static readonly LocalizableString Title = new LocalizableResourceString(
            nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));


        private static readonly LocalizableString ExpectedExceptionDeprecatedMessageFormat =
            new LocalizableResourceString(nameof(Resources.ExpectedExceptionDeprecatedMessageFormat), Resources.ResourceManager,
                typeof(Resources));

        private static readonly LocalizableString ExpectedExceptionDeprecatedDescription =
            new LocalizableResourceString(nameof(Resources.ExpectedExceptionDeprecatedDescription), Resources.ResourceManager,
                typeof(Resources));

        public static readonly DiagnosticDescriptor ExpectedExceptionDeprecatedRule = new DiagnosticDescriptor(
            DiagnosticIds.ExpectedExceptionAttributeIsDeprecated,
            Title,
            ExpectedExceptionDeprecatedMessageFormat,
            Category,
            DiagnosticSeverity.Error,
            true,
            ExpectedExceptionDeprecatedDescription);


        private static readonly LocalizableString ReferencedPropertyInTestCasesSourceIsNotStaticMessageFormat =
            new LocalizableResourceString(nameof(Resources.ReferencedPropertyInTestCasesSourceIsNotStaticMessageFormat), Resources.ResourceManager,
                typeof(Resources));

        private static readonly LocalizableString ReferencedPropertyInTestCasesSourceIsNotStaticDescription =
            new LocalizableResourceString(nameof(Resources.ReferencedPropertyInTestCasesSourceIsNotStaticDescription), Resources.ResourceManager,
                typeof(Resources));

        public static readonly DiagnosticDescriptor ReferencedPropertyInTestCasesSourceIsNotStaticRule = new DiagnosticDescriptor(
            DiagnosticIds.ReferencedPropertyInTestCaseSourceHasToBeStatic,
            Title,
            ReferencedPropertyInTestCasesSourceIsNotStaticMessageFormat,
            Category,
            DiagnosticSeverity.Error, true,
            ReferencedPropertyInTestCasesSourceIsNotStaticDescription);


        private static readonly LocalizableString ReferencedFieldInTestCasesSourceIsNotStaticMessageFormat =
            new LocalizableResourceString(nameof(Resources.ReferencedPropertyInTestCasesSourceIsNotStaticMessageFormat), Resources.ResourceManager,
                typeof(Resources));

        private static readonly LocalizableString ReferencedFieldInTestCasesSourceIsNotStaticDescription =
            new LocalizableResourceString(nameof(Resources.ReferencedFieldInTestCasesSourceIsNotStaticDescription), Resources.ResourceManager,
                typeof(Resources));

        public static readonly DiagnosticDescriptor ReferencedFieldInTestCasesSourceIsNotStaticRule = new DiagnosticDescriptor(
            DiagnosticIds.ReferencedFieldInTestCaseSourceHasToBeStatic,
            Title,
            ReferencedFieldInTestCasesSourceIsNotStaticMessageFormat,
            Category,
            DiagnosticSeverity.Error, true,
            ReferencedFieldInTestCasesSourceIsNotStaticDescription);


        private static readonly LocalizableString ReferencedMethodInTestCasesSourceIsNotStaticMessageFormat =
            new LocalizableResourceString(nameof(Resources.ReferencedPropertyInTestCasesSourceIsNotStaticMessageFormat), Resources.ResourceManager,
                typeof(Resources));

        private static readonly LocalizableString ReferencedMethodInTestCasesSourceIsNotStaticDescription =
            new LocalizableResourceString(nameof(Resources.ReferencedMethodInTestCasesSourceIsNotStaticDescription), Resources.ResourceManager,
                typeof(Resources));

        public static readonly DiagnosticDescriptor ReferencedMethodInTestCasesSourceIsNotStaticRule = new DiagnosticDescriptor(
            DiagnosticIds.ReferencedMethodInTestCaseSourceHasToBeStatic,
            Title,
            ReferencedMethodInTestCasesSourceIsNotStaticMessageFormat,
            Category,
            DiagnosticSeverity.Error, true,
            ReferencedMethodInTestCasesSourceIsNotStaticDescription);


        private static readonly LocalizableString ThrowsDeprecatedMessageFormat =
            new LocalizableResourceString(nameof(Resources.ThrowsDeprecatedMessageFormat), Resources.ResourceManager,
                typeof(Resources));

        private static readonly LocalizableString ThrowsDeprecatedDescription =
            new LocalizableResourceString(nameof(Resources.ThrowsDeprecatedDescription), Resources.ResourceManager,
                typeof(Resources));

        public static readonly DiagnosticDescriptor ThrowsDeprecatedRule = new DiagnosticDescriptor(
            DiagnosticIds.ThrowsIsDeprecated,
            Title,
            ThrowsDeprecatedMessageFormat,
            Category,
            DiagnosticSeverity.Error, true,
            ThrowsDeprecatedDescription);

        private static readonly LocalizableString AsyncVoidIsDeprectedMessageFormat =
            new LocalizableResourceString(nameof(Resources.AsyncVoidIsDeprectedMessageFormat), Resources.ResourceManager,
                typeof(Resources));

        private static readonly LocalizableString AsyncVoidIsDeprectedDescription =
            new LocalizableResourceString(nameof(Resources.AsyncVoidIsDeprectedDescription), Resources.ResourceManager,
                typeof(Resources));

        public static readonly DiagnosticDescriptor AsyncVoidIsDeprectedRule = new DiagnosticDescriptor(
            DiagnosticIds.AsyncVoidIsDeprected,
            Title,
            AsyncVoidIsDeprectedMessageFormat,
            Category,
            DiagnosticSeverity.Error, true,
            AsyncVoidIsDeprectedDescription);

        private static readonly LocalizableString TestFixtureOnAbstractClassIsUselessMessageFormat =
            new LocalizableResourceString(nameof(Resources.TestFixtureOnAbstractClassIsUselessMessageFormat), Resources.ResourceManager,
                typeof(Resources));

        private static readonly LocalizableString TestFixtureOnAbstractClassIsUselessDescription =
            new LocalizableResourceString(nameof(Resources.TestFixtureOnAbstractClassIsUselessDescription), Resources.ResourceManager,
                typeof(Resources));

        public static readonly DiagnosticDescriptor TestFixtureOnAbstractClassIsUselessRule = new DiagnosticDescriptor(
            DiagnosticIds.TestFixtureUselessOnAbstractClass,
            Title,
            TestFixtureOnAbstractClassIsUselessMessageFormat,
            Category,
            DiagnosticSeverity.Error, true,
            TestFixtureOnAbstractClassIsUselessDescription);

       private static readonly LocalizableString ReferencedMemberDoesNotExistsMessageFormat =
            new LocalizableResourceString(nameof(Resources.ReferencedMemberDoesNotExistsXMessageFormat), Resources.ResourceManager,
                typeof(Resources));

        private static readonly LocalizableString ReferencedMemberDoesNotExistsDescription =
            new LocalizableResourceString(nameof(Resources.ReferencedMemberDoesNotExistsDescription), Resources.ResourceManager,
                typeof(Resources));

        public static readonly DiagnosticDescriptor ReferencedMemberDoesNotExistsRule = new DiagnosticDescriptor(
            DiagnosticIds.ReferencedMemberDoesNotExists,
            Title,
            ReferencedMemberDoesNotExistsMessageFormat,
            Category,
            DiagnosticSeverity.Error, true,
            ReferencedMemberDoesNotExistsDescription);
    }
}