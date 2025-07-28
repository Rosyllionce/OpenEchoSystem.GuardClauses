# Test Plan: Null Guard Clauses

## 1. Introduction

This document outlines the granular test plan for the **Null Guard Clauses** feature of the OpenEchoSystem.GuardClauses library. The feature includes three specific guard clauses: `Null<T>`, `NullOrEmpty`, and `NullOrWhiteSpace`.

This plan is derived from the feature specification outlined in [`docs/PRDMasterPlan.v2.md`](docs/PRDMasterPlan.v2.md:48) (Phase 3, Task 1) and the system design detailed in [`docs/architecture/high_level_architecture.v2.md`](docs/architecture/high_level_architecture.v2.md:0).

The primary goal of these tests is to verify the AI Verifiable End Result for the feature, which is the successful execution of all unit tests for the specified guard clauses.

## 2. Test Scope & Strategy

### 2.1. Scope

The scope of this test plan is limited to the unit testing of the following extension methods on the `IGuardClause` interface:
*   `Null<T>(T inputValue, ...)`
*   `NullOrEmpty(string inputValue, ...)`
*   `NullOrWhiteSpace(string inputValue, ...)`

Testing will cover both negative scenarios (where an exception is expected) and positive scenarios (where the guard should pass silently).

### 2.2. Test Strategy (London School of TDD)

The testing strategy adheres to the **London School of TDD**. We will not test the internal state of the guard clauses. Instead, we will focus on **interaction-based testing** to verify observable outcomes.

*   **Collaborators:** The primary collaborator for the guard clauses is the .NET runtime's exception handling mechanism.
*   **Mocking:** No complex mocking is required for these specific unit tests, as we are testing for thrown exceptions, which is a direct, observable outcome. The `IGuardClause` instance is a concrete, lightweight object as per the architecture.
*   **Observable Outcome:** The key observable outcome is either the throwing of a specific exception type (`ArgumentNullException`, `ArgumentException`) for invalid input or the successful completion of the method call (i.e., no exception thrown) for valid input.

## 3. Recursive Testing (Regression Strategy)

To ensure ongoing stability and catch regressions early, a recursive testing strategy will be employed.

*   **Triggers for Re-running Tests:**
    *   **On Every Code Change:** The full suite of tests defined in this plan will be executed automatically via a Git pre-commit hook or as part of the CI pipeline on every push.
    *   **Before Merging:** The full test suite must pass before any feature branch is merged into the `main` branch.
    *   **Nightly Builds:** The entire project's test suite, including these tests, will be run during a nightly build to catch any subtle integration issues.

*   **Test Prioritization & Tagging:**
    *   Tests will be tagged with `[Trait("Category", "CoreGuard")]` and `[Trait("Feature", "NullGuards")]`.
    *   This allows for running specific subsets of tests. For example, during focused development on null guards, a developer can run only the tests with the "NullGuards" trait.

*   **AI Verifiable Criterion:** The regression strategy is considered successful if the CI build (`.github/workflows/ci.yml`) is configured to run these tests on every push and pull request, and the build fails if any test fails.

## 4. Test Cases

### 4.1. Test Cases for `Guard.Against.Null<T>`

This guard protects against null values for any reference type.

| Test Case ID | Description                                       | Input (`inputValue`) | Expected Outcome                               | AI Verifiable Completion Criterion                                                              |
| :----------- | :------------------------------------------------ | :------------------- | :--------------------------------------------- | :---------------------------------------------------------------------------------------------- |
| **NULL-N-01**  | A null object is provided.                        | `null`               | Throws `ArgumentNullException`.                | Test passes when an `ArgumentNullException` is caught.                                          |
| **NULL-P-01**  | A valid, non-null object is provided.             | `new object()`       | No exception is thrown.                        | Test passes when the method call completes without throwing any exception.                      |
| **NULL-P-02**  | A valid, non-null string is provided.             | `"hello"`            | No exception is thrown.                        | Test passes when the method call completes without throwing any exception.                      |
| **NULL-P-03**  | An empty string is provided (not the same as null). | `""`                 | No exception is thrown.                        | Test passes when the method call completes without throwing any exception.                      |

### 4.2. Test Cases for `Guard.Against.NullOrEmpty`

This guard protects against null or empty strings.

| Test Case ID | Description                                       | Input (`inputValue`) | Expected Outcome                               | AI Verifiable Completion Criterion                                                              |
| :----------- | :------------------------------------------------ | :------------------- | :--------------------------------------------- | :---------------------------------------------------------------------------------------------- |
| **NOE-N-01**   | A null string is provided.                        | `null`               | Throws `ArgumentNullException`.                | Test passes when an `ArgumentNullException` is caught.                                          |
| **NOE-N-02**   | An empty string is provided.                      | `string.Empty`       | Throws `ArgumentException`.                    | Test passes when an `ArgumentException` is caught.                                              |
| **NOE-P-01**   | A non-empty string is provided.                   | `"hello"`            | No exception is thrown.                        | Test passes when the method call completes without throwing any exception.                      |
| **NOE-P-02**   | A whitespace string is provided.                  | `" "`                | No exception is thrown.                        | Test passes when the method call completes without throwing any exception.                      |

### 4.3. Test Cases for `Guard.Against.NullOrWhiteSpace`

This guard protects against null, empty, or whitespace-only strings.

| Test Case ID | Description                                       | Input (`inputValue`) | Expected Outcome                               | AI Verifiable Completion Criterion                                                              |
| :----------- | :------------------------------------------------ | :------------------- | :--------------------------------------------- | :---------------------------------------------------------------------------------------------- |
| **NOW-N-01**   | A null string is provided.                        | `null`               | Throws `ArgumentNullException`.                | Test passes when an `ArgumentNullException` is caught.                                          |
| **NOW-N-02**   | An empty string is provided.                      | `string.Empty`       | Throws `ArgumentException`.                    | Test passes when an `ArgumentException` is caught.                                              |
| **NOW-N-03**   | A whitespace-only string is provided.             | `" "`                | Throws `ArgumentException`.                    | Test passes when an `ArgumentException` is caught.                                              |
| **NOW-N-04**   | A string with multiple whitespace chars is provided. | `"\t\r\n"`           | Throws `ArgumentException`.                    | Test passes when an `ArgumentException` is caught.                                              |
| **NOW-P-01**   | A non-empty, non-whitespace string is provided.   | `"hello"`            | No exception is thrown.                        | Test passes when the method call completes without throwing any exception.                      |
| **NOW-P-02**   | A string with leading/trailing whitespace is provided. | `"  hello  "`       | No exception is thrown.                        | Test passes when the method call completes without throwing any exception.                      |

## 5. Test Environment

*   **Framework:** xUnit
*   **Runner:** .NET Test SDK
*   **Assertions:** xUnit's assertion library (e.g., `Assert.Throws<T>`).

**AI Verifiable Criterion:** The test project `tests/unit/OpenEchoSystem.GuardClauses.UnitTest.csproj` must reference the xUnit and .NET Test SDK packages.