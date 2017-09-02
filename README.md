# SwitchToNUnit3

A simple extension that notifies you about upcomming changes in NUnit3 while working on NUnit2. It allows you to upgrade your source code step-by-step to be NUnit3 complient.

Currently the follwoing deprected modification are detected:

* ExpectedExceptionAttribute - will be removed in NUnit3
* TestCaseSource.Throws methods -  will be removed in NUnit3
* TestFixtureSetUpAttribute - deprecated, replaced by OneTimeSetUp
* TestFixtureTearDownAttribute - deprecated, replaced by OneTimeTearDown
* Async void test-methods - tests will note be excecuted (use async Task instead)
* Non-Static references in TestCasesSource - tests will note be excecuted


Currently there are no Code-Fixes implemented, but I intend to add them.