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


        private static readonly LocalizableString TestFixtureSetUpAttributeDeprecatedMessageFormat =
            new LocalizableResourceString(nameof(Resources.TestFixtureSetUpAttributeDeprecatedMessageFormat), Resources.ResourceManager,
                typeof(Resources));

        private static readonly LocalizableString TestFixtureSetUpAttributeDeprecatedDescription =
            new LocalizableResourceString(nameof(Resources.TestFixtureSetUpAttributeDeprecatedDescription), Resources.ResourceManager,
                typeof(Resources));

        public static readonly DiagnosticDescriptor TestFixtureSetUpAttributeDeprectedRule = new DiagnosticDescriptor(
            DiagnosticIds.TestFixtureSetUpAttributeIsDeprecated,
            Title,
            TestFixtureSetUpAttributeDeprecatedMessageFormat,
            Category,
            DiagnosticSeverity.Error,
            true,
            TestFixtureSetUpAttributeDeprecatedDescription);



        private static readonly LocalizableString TestFixtureTearDownAttributeDeprecatedMessageFormat =
            new LocalizableResourceString(nameof(Resources.TestFixtureTearDownAttributeDeprecatedMessageFormat), Resources.ResourceManager,
                typeof(Resources));

        private static readonly LocalizableString TestFixtureTearDownAttributeDeprecatedDescription =
            new LocalizableResourceString(nameof(Resources.TestFixtureTearDownAttributeDeprecatedDescription), Resources.ResourceManager,
                typeof(Resources));

        public static readonly DiagnosticDescriptor TestFixtureTearDownAttributeDeprecatedRule = new DiagnosticDescriptor(
            DiagnosticIds.TestFixtureTearDownAttributeIsDeprecated,
            Title,
            TestFixtureTearDownAttributeDeprecatedMessageFormat,
            Category,
            DiagnosticSeverity.Error,
            true,
            TestFixtureTearDownAttributeDeprecatedDescription);



        private static readonly LocalizableString ReferencedTestCasesSourceIsNotStaticMessageFormat =
            new LocalizableResourceString(nameof(Resources.ReferencedTestCasesSourceIsNotStaticMessageFormat), Resources.ResourceManager,
                typeof(Resources));

        private static readonly LocalizableString ReferencedTestCasesSourceIsNotStaticDescription =
            new LocalizableResourceString(nameof(Resources.ReferencedTestCasesSourceIsNotStaticDescription), Resources.ResourceManager,
                typeof(Resources));

        public static readonly DiagnosticDescriptor ReferencedTestCasesSourceIsNotStaticRule = new DiagnosticDescriptor(
            DiagnosticIds.ReferencedTestCaseSourceHasToBeStatic,
            Title,
            ReferencedTestCasesSourceIsNotStaticMessageFormat,
            Category,
            DiagnosticSeverity.Error, true,
            ReferencedTestCasesSourceIsNotStaticDescription);


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
    }
}