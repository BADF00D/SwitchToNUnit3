namespace SwitchToNUnit3
{
    public static class DiagnosticIds
    {
        public const string ExpectedExceptionAttributeIsDeprecated = "STN3000";
        public const string ReferencedPropertyInTestCaseSourceHasToBeStatic = "STN3010";
        public const string ReferencedFieldInTestCaseSourceHasToBeStatic = "STN3011";
        public const string ReferencedMethodInTestCaseSourceHasToBeStatic = "STN3012";
        public const string ThrowsIsDeprecated = "STN3020";
        public const string AsyncVoidIsDeprected = "STN3030";
        public const string TestFixtureUselessOnAbstractClass = "STN3040";
        public const string ReferencedPropertyDoesNotExists = "STN3050";
    }
}