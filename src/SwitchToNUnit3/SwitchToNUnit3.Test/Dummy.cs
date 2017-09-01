﻿        using System;
        using System.Collections.Generic;
        using NUnit.Framework;
        namespace Testnamespace {
            public class SomeTest {
                [TestCaseSource(nameof(Tests))]
                public void SomeMethod() { }

                public IEnumerable<TestCaseData> Tests {
                    get {
                        yield return new TestCaseData().Throws(nameof(Exception));
                    }
                }
            }
        }
        namespace NUnit.Framework {
            public class TestCaseSourceAttribute : Attribute {
                public TestCaseSourceAttribute(string name) { }
            }
            public class TestCaseData {
                public TestCaseData Throws(string exceptionName) {
                    return this;
                }
                public TestCaseData Throws(Type exceptionType) {
                    return this;
                }
            }
        }