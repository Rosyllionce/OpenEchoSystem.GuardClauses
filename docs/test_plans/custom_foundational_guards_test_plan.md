# Test Plan: Custom and Foundational Guard Clauses

## 1. Introduction & Scope

This document provides a detailed test plan for the granular testing of the **Custom and Foundational Guard Clauses** feature. The implementation of these guards is a core requirement outlined in [`docs/PRDMasterPlan.v2.md`](docs/PRDMasterPlan.v2.md:63) (Phase 3, Task 4).

This plan covers the following guard clauses:
*   `CustomCondition` / `Boolean`
*   `NotFound` (new implementation)
*   `Enum`
*   `GUID`

The tests defined herein are designed to verify the AI-verifiable end result specified in the master plan: "Unit tests for all respective guard clauses pass successfully." This plan draws context from the system architecture defined in [`docs/architecture/high_level_architecture.v2.md`](docs/architecture/high_level_architecture.v2.md) and the existing implementations detailed in [`docs/reports/unplanned_features_comprehension_report.md`](docs/reports/unplanned_features_comprehension_report.md).

## 2. Test Strategy

### 2.1. London School of TDD

Our testing approach is based on the **London School of Test-Driven Development (TDD)**. This means we focus on **interaction-based testing** rather than state-based testing. The goal is to verify the observable behavior of a unit of code (the guard clause extension method) by examining how it interacts with its collaborators.

Key principles we will follow:
*   **Mocking Collaborators:** Dependencies will be mocked or stubbed. For these guard clauses, a primary collaborator might be a function delegate (e.g., a repository lookup for the `NotFound` guard). We will test that our guard clause calls the collaborator as expected and reacts correctly to the values the collaborator returns.
*   **Verifying Observable Outcomes:** Tests will assert on the final, observable outcome of a guard clause's execution. For this library, the primary observable outcome is either **throwing a specific exception** when a condition is met or **not throwing an exception** when the input is valid. We do not inspect the internal state of the guard clause itself.

### 2.2. AI-Verifiable Criteria

Every test case must have an **AI-verifiable completion criterion**. This means the success or failure of a test can be determined programmatically without human interpretation. Examples include:
*   Asserting that a specific type of exception (e.g., `ArgumentException`, `ArgumentOutOfRangeException`) is thrown.
*   Asserting that no exception is thrown during execution.
*   Verifying that an exception message contains a specific, expected substring (like a parameter name).

## 3. Recursive Testing (Regression Strategy)

A comprehensive and frequent regression testing strategy is crucial for maintaining stability. The unit tests for these foundational guard clauses are lightweight and fundamental to the library's correctness.

*   **Triggers for Re-execution:**
    *   **Continuous Integration (CI):** The entire suite of these unit tests will be executed on every `git push` to any branch, especially `main`.
    *   **Dependency Changes:** If the core `IGuardClause` interface or `Guard` static class is modified, this entire test suite must be run.
    *   **Pre-Release:** The full test suite will be executed as a mandatory step before packaging a new release.

*   **Test Prioritization and Tagging:**
    *   All tests in this plan will be tagged as `Unit` and `Core`.
    *   Their fast execution time means they provide the quickest feedback loop and should never be skipped in any test run.

*   **Test Subset Selection:**
    *   Given their fundamental nature, there is no subset selection for these tests. They will always run as a complete suite.

## 4. Test Cases

### 4.1. Guard: CustomCondition / Boolean

This guard allows for flexible, user-defined validation logic. It is treated as synonymous with the `Boolean` guard. The implementation throws an exception if the provided boolean condition is `true`.

*   **Target AI Verifiable End Result:** `docs/PRDMasterPlan.v2.md` (Phase 3, Task 4).

| Test Case ID | Description | Test Steps | Observable Outcome (AI Verifiable Criterion) | Collaborators to Mock | Recursive Scope |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **CC-01** | Throws `ArgumentException` when the custom condition is `true`. | Call `Guard.Against.CustomCondition(true, "paramName")`. | Throws `System.ArgumentException`. | None | `CI`, `FullRegression` |
| **CC-02** | Does not throw when the custom condition is `false`. | Call `Guard.Against.CustomCondition(false, "paramName")`. | Does not throw any exception. | None | `CI`, `FullRegression` |
| **CC-03** | Exception message contains the correct parameter name. | Call `Guard.Against.CustomCondition(true, "myCustomParam")`. | Throws `System.ArgumentException` with a message containing "myCustomParam". | None | `CI`, `FullRegression` |

### 4.2. Guard: NotFound

This is a new guard to validate the existence of an entity, typically by checking a repository or service. The design assumes an interaction with a lookup function.

*   **Target AI Verifiable End Result:** `docs/PRDMasterPlan.v2.md` (Phase 3, Task 4).

| Test Case ID | Description | Test Steps | Observable Outcome (AI Verifiable Criterion) | Collaborators to Mock | Recursive Scope |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **NF-01** | Throws a `NotFoundException` when the lookup function returns `null`. | 1. Define a key `var userId = "user-123"`.<br>2. Create a mock `Func<string, User>` that returns `null`.<br>3. Call `Guard.Against.NotFound(userId, mockLookup)`. | Throws a custom `NotFoundException`. | `Func<string, User>` lookup delegate. | `CI`, `FullRegression` |
| **NF-02** | Does not throw when the lookup function returns a valid object. | 1. Define a key `var userId = "user-123"`.<br>2. Create a mock `Func<string, User>` that returns a `new User()`.<br>3. Call `Guard.Against.NotFound(userId, mockLookup)`. | Does not throw any exception. | `Func<string, User>` lookup delegate. | `CI`, `FullRegression` |
| **NF-03** | Exception message contains the key of the entity that was not found. | 1. Define a key `var productId = "prod-abc"`.<br>2. Create a mock `Func<string, Product>` that returns `null`.<br>3. Call `Guard.Against.NotFound(productId, mockLookup)`. | Throws `NotFoundException` with a message containing "prod-abc". | `Func<string, Product>` lookup delegate. | `CI`, `FullRegression` |

### 4.3. Guard: Enum

This guard validates that a given value is a defined member of its enumeration type.

*   **Target AI Verifiable End Result:** `docs/PRDMasterPlan.v2.md` (Phase 3, Task 4).

| Test Case ID | Description | Test Steps | Observable Outcome (AI Verifiable Criterion) | Collaborators to Mock | Recursive Scope |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **EN-01** | Throws `ArgumentOutOfRangeException` for an undefined enum value. | 1. Define `enum TestEnum { A, B }`.<br>2. Call `Guard.Against.OutOfRange((TestEnum)99, "paramName")`. | Throws `System.ArgumentOutOfRangeException`. | None | `CI`, `FullRegression` |
| **EN-02** | Does not throw for a valid, defined enum value. | 1. Define `enum TestEnum { A, B }`.<br>2. Call `Guard.Against.OutOfRange(TestEnum.A, "paramName")`. | Does not throw any exception. | None | `CI`, `FullRegression` |
| **EN-03** | Exception message contains the correct parameter name. | 1. Define `enum TestEnum { A, B }`.<br>2. Call `Guard.Against.OutOfRange((TestEnum)99, "myEnumParam")`. | Throws `System.ArgumentOutOfRangeException` with a message containing "myEnumParam". | None | `CI`, `FullRegression` |

### 4.4. Guard: GUID

This guard validates `System.Guid` values, specifically checking for `Guid.Empty`.

*   **Target AI Verifiable End Result:** `docs/PRDMasterPlan.v2.md` (Phase 3, Task 4).

| Test Case ID | Description | Test Steps | Observable Outcome (AI Verifiable Criterion) | Collaborators to Mock | Recursive Scope |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **GUID-01** | Throws `ArgumentException` when the input Guid is `Guid.Empty`. | Call `Guard.Against.Empty(Guid.Empty, "paramName")`. | Throws `System.ArgumentException`. | None | `CI`, `FullRegression` |
| **GUID-02** | Does not throw when the input Guid is not empty. | Call `Guard.Against.Empty(Guid.NewGuid(), "paramName")`. | Does not throw any exception. | None | `CI`, `FullRegression` |
| **GUID-03** | Exception message contains the correct parameter name. | Call `Guard.Against.Empty(Guid.Empty, "myGuidParam")`. | Throws `System.ArgumentException` with a message containing "myGuidParam". | None | `CI`, `FullRegression` |