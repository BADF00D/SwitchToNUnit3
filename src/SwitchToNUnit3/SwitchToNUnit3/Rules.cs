﻿using Microsoft.CodeAnalysis;

namespace SwitchToNUnit3
{
    public class Rules
    {
        private const string Category = "Obsolete";

        private static readonly LocalizableString Title = new LocalizableResourceString(
            nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));

        private static readonly LocalizableString MessageFormat =
            new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager,
                typeof(Resources));

        private static readonly LocalizableString Description =
            new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager,
                typeof(Resources));

        public static readonly DiagnosticDescriptor ExpectedExceptionDeprecatedRule = new DiagnosticDescriptor(
            DiagnosticIds.ExpectedExceptionAttributeIsDeprecated,
            Title,
            MessageFormat,
            Category,
            DiagnosticSeverity.Error,
            true,
            Description);



        private static readonly LocalizableString MessageFormat3 =
            new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat3), Resources.ResourceManager,
                typeof(Resources));

        private static readonly LocalizableString Description3 =
            new LocalizableResourceString(nameof(Resources.AnalyzerDescription3), Resources.ResourceManager,
                typeof(Resources));

        public static readonly DiagnosticDescriptor ReferencedTestCasesSourceIsNotStatic = new DiagnosticDescriptor(
            DiagnosticIds.ReferencedTestCaseSourceHasToBeStatic,
            Title,
            MessageFormat3,
            Category,
            DiagnosticSeverity.Error, true,
            Description3);


        private static readonly LocalizableString MessageFormat2 =
            new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat3), Resources.ResourceManager,
                typeof(Resources));

        private static readonly LocalizableString Description2 =
            new LocalizableResourceString(nameof(Resources.AnalyzerDescription3), Resources.ResourceManager,
                typeof(Resources));

        public static readonly DiagnosticDescriptor ThrowsDeprecatedRule = new DiagnosticDescriptor(
            DiagnosticIds.ThrowsIsDeprecated,
            Title,
            MessageFormat2,
            Category,
            DiagnosticSeverity.Error, true,
            Description2);

        private static readonly LocalizableString MessageFormat4 =
            new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat3), Resources.ResourceManager,
                typeof(Resources));

        private static readonly LocalizableString Description4 =
            new LocalizableResourceString(nameof(Resources.AnalyzerDescription3), Resources.ResourceManager,
                typeof(Resources));

        public static readonly DiagnosticDescriptor AsyncVoidIsDeprectedRule = new DiagnosticDescriptor(
            DiagnosticIds.AsyncVoidIsDeprected,
            Title,
            MessageFormat4,
            Category,
            DiagnosticSeverity.Error, true,
            Description4);
    }
}