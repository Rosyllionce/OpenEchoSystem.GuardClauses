# Integration Test Plan: Guard Clause C# Library

## 1. Introduction

This document outlines the detailed integration test plan for the Guard Clause C# library. The primary goal of these tests is to verify the seamless interaction between the Guard Clause library and application-level code, ensuring that the guard clauses function correctly in real-world scenarios and that the overall system behaves as expected when these validations are applied. This plan adheres to London School of TDD principles, focusing on interaction-based testing and verifying observable outcomes. It also defines a comprehensive recursive testing strategy to ensure continuous stability and early regression detection.

## 2. Test Scope

The integration tests specifically target the AI Verifiable End Result defined in the Master Project Plan (`docs/PRDMasterPlan.v2.md:98`) for Phase 5, Micro Task 2: "Successful execution of all integration tests in the CI pipeline." These tests will validate the correct behavior of application logic that utilizes the `OpenEchoSystem.GuardClauses` library, ensuring that:

*   Application methods correctly invoke guard clauses with appropriate arguments.
*   Guard clauses, when triggered by invalid inputs, correctly throw the expected exceptions.
*   Application methods, when provided with valid inputs, execute without unexpected exceptions from guard clauses.
*   The integration of multiple guard clauses within a single application flow behaves predictably.

## 3. Test Strategy: London School of TDD Principles

Our integration testing strategy is rooted in the London School of TDD, emphasizing the verification of observable outcomes through interactions with collaborators.

*   **Interaction-Based Testing:** Instead of inspecting the internal state of the `Guard` class (which is primarily handled by unit tests), integration tests will focus on the *behavior* of the application code (the "unit under test") as it interacts with the `Guard` library. This means we observe whether the application method correctly throws an exception when a guard clause is violated, or completes successfully when inputs are valid.
*   **Collaborators & Mocking:** In the context of integration tests for a static utility library like `OpenEchoSystem.GuardClauses`, the "collaborators" are the specific guard clause extension methods (e.g., `Guard.Against.NullOrWhiteSpace`, `Guard.Against.OutOfRange`, `Guard.Against.InvalidEmail`). Since these are static methods, traditional mocking frameworks (like Moq for interfaces/classes) are not directly applicable for *replacing* the `Guard` functionality. Instead, our "mocking" or "setup" involves carefully crafting test data (inputs to the application method) to *trigger* specific behaviors within the `Guard` clauses, thereby verifying the application's response to these interactions. The observable outcome is the exception type and message, or the successful completion of the application method.

## 4. Recursive Testing Strategy (Regression)

A robust recursive testing strategy is crucial for maintaining the quality and stability of the Guard Clause library as it evolves. This strategy ensures frequent regression testing at various SDLC touch points.

### 4.1. Triggers for Re-execution

Test suites or subsets will be re-executed based on the following triggers:

*   **Every Commit/Push to `main` or Feature Branches:** A subset of critical integration tests (Smoke Regression) will run as part of the Continuous Integration (CI) pipeline.
*   **Pull Request Creation/Update:** All integration tests (Full Regression) will run to ensure no regressions are introduced before merging.
*   **Nightly Builds:** A comprehensive set of integration tests (Daily/Nightly Regression) will run to catch regressions that might have slipped through earlier stages.
*   **Before Major Releases/Version Bumps:** All integration tests (Full Regression) will be executed as a final quality gate.
*   **After Dependency Updates:** If any NuGet packages or core .NET framework versions are updated, a Full Regression will be triggered.
*   **After Significant Code Refactoring in `OpenEchoSystem.GuardClauses`:** Targeted integration tests related to the refactored areas, along with a Smoke Regression, will be run.

### 4.2. Test Prioritization and Tagging

Integration tests will be tagged to facilitate selective execution based on regression scope:

*   **`[IntegrationTest]`:** All integration tests will carry this tag.
*   **`[Smoke]`:** A small, fast subset of critical integration tests covering core functionalities.
*   **`[Full]`:** All integration tests.

### 4.3. Test Subset Selection for Regression

*   **Smoke Regression (Fast Feedback):**
    *   **Trigger:** Every commit/push, minor code changes.
    *   **Tests Included:** Tests tagged `[Smoke]`. Focus on the most common and critical integration paths (e.g., `CreateUser_WithValidInputs_DoesNotThrow`, `CreateUser_WithNullName_ThrowsArgumentException`).
    *   **Execution Command (Example):** `dotnet test --filter Category=Smoke`
    *   **AI Verifiable Outcome:** Successful execution of all `[Smoke]` tagged integration tests in the CI pipeline.
*   **Daily/Nightly Regression (Comprehensive Check):**
    *   **Trigger:** Scheduled nightly builds.
    *   **Tests Included:** A broader set of tests, including `[Smoke]` and additional `[IntegrationTest]` cases covering more complex scenarios and edge cases.
    *   **Execution Command (Example):** `dotnet test --filter Category=IntegrationTest` (excluding `[Full]` if a separate `[Full]` tag is used for all tests, or simply running all integration tests if `[IntegrationTest]` implies all).
    *   **AI Verifiable Outcome:** Successful execution of all `[IntegrationTest]` tagged tests (excluding `[Full]` if applicable) in the nightly CI pipeline.
*   **Full Regression (Release Readiness/Major Changes):**
    *   **Trigger:** Pull Request merge, before major releases, significant refactoring, dependency updates.
    *   **Tests Included:** All integration tests.
    *   **Execution Command (Example):** `dotnet test tests/integration/OpenEchoSystem.GuardClauses.IntegrationTests.csproj` (runs all tests in the project).
    *   **AI Verifiable Outcome:** Successful execution of all integration tests within the `OpenEchoSystem.GuardClauses.IntegrationTests.csproj` project.

## 5. Integration Test Cases

The following test cases are designed to verify the integration of the Guard Clause library with application logic, directly mapping to the AI Verifiable End Result of "Successful execution of all integration tests in the CI pipeline."

### 5.1. Test Case: CreateUser_WithValidInputs_DoesNotThrow

*   **Test Case ID:** INT-GC-001
*   **Description:** Verifies that the `CreateUser` method, when provided with valid inputs, executes successfully without any exceptions being thrown by the integrated guard clauses.
*   **Targeted AI Verifiable End Result:** Successful execution of all integration tests in the CI pipeline (`docs/PRDMasterPlan.v2.md:98`).
*   **Unit Under Test:** `ServiceExample.CreateUser(string name, int age, string email)`
*   **Interactions to Test:**
    *   Call to `Guard.AgainstNullOrWhiteSpace` with a valid `name`.
    *   Call to `Guard.AgainstOutOfRange` with a valid `age`.
    *   Call to `Guard.AgainstInvalidEmail` with a valid `email`.
*   **Collaborators & Mocking Strategy:** No explicit mocking of `Guard` is performed. The test verifies the observable outcome of the `ServiceExample`'s interaction with the static `Guard` methods.
*   **Expected Observable Outcome:** The `CreateUser` method completes execution without throwing any `ArgumentException`, `ArgumentNullException`, or `ArgumentOutOfRangeException`.
*   **Test Data & Mock Configuration:**
    *   `name`: "John Doe"
    *   `age`: 30
    *   `email`: "john.doe@example.com"
*   **Recursive Testing Scope:** `[Smoke]`, `[Daily/Nightly Regression]`, `[Full Regression]`

### 5.2. Test Case: CreateUser_WithNullName_ThrowsArgumentException

*   **Test Case ID:** INT-GC-002
*   **Description:** Verifies that the `CreateUser` method correctly throws an `ArgumentException` when the `name` input is `null`, due to the `Guard.AgainstNullOrWhiteSpace` clause.
*   **Targeted AI Verifiable End Result:** Successful execution of all integration tests in the CI pipeline (`docs/PRDMasterPlan.v2.md:98`).
*   **Unit Under Test:** `ServiceExample.CreateUser(string name, int age, string email)`
*   **Interactions to Test:**
    *   Call to `Guard.AgainstNullOrWhiteSpace` with a `null` `name`.
*   **Collaborators & Mocking Strategy:** No explicit mocking. The test verifies the observable outcome (exception) of the `ServiceExample`'s interaction with `Guard.AgainstNullOrWhiteSpace`.
*   **Expected Observable Outcome:** An `ArgumentException` is thrown by the `CreateUser` method, with a message indicating the `name` argument cannot be null or whitespace.
*   **Test Data & Mock Configuration:**
    *   `name`: `null`
    *   `age`: 30
    *   `email`: "john.doe@example.com"
*   **Recursive Testing Scope:** `[Smoke]`, `[Daily/Nightly Regression]`, `[Full Regression]`

### 5.3. Test Case: CreateUser_WithOutOfRangeAge_ThrowsArgumentOutOfRangeException

*   **Test Case ID:** INT-GC-003
*   **Description:** Verifies that the `CreateUser` method correctly throws an `ArgumentOutOfRangeException` when the `age` input is outside the valid range (e.g., 150), due to the `Guard.AgainstOutOfRange` clause.
*   **Targeted AI Verifiable End Result:** Successful execution of all integration tests in the CI pipeline (`docs/PRDMasterPlan.v2.md:98`).
*   **Unit Under Test:** `ServiceExample.CreateUser(string name, int age, string email)`
*   **Interactions to Test:**
    *   Call to `Guard.AgainstOutOfRange` with an `age` value outside the specified range.
*   **Collaborators & Mocking Strategy:** No explicit mocking. The test verifies the observable outcome (exception) of the `ServiceExample`'s interaction with `Guard.AgainstOutOfRange`.
*   **Expected Observable Outcome:** An `ArgumentOutOfRangeException` is thrown by the `CreateUser` method, with a message indicating the `age` argument is out of range.
*   **Test Data & Mock Configuration:**
    *   `name`: "Jane Doe"
    *   `age`: 150
    *   `email`: "jane.doe@example.com"
*   **Recursive Testing Scope:** `[Smoke]`, `[Daily/Nightly Regression]`, `[Full Regression]`

### 5.4. Test Case: CreateUser_WithInvalidEmail_ThrowsArgumentException

*   **Test Case ID:** INT-GC-004
*   **Description:** Verifies that the `CreateUser` method correctly throws an `ArgumentException` when the `email` input is in an invalid format (e.g., "invalid-email"), due to the `Guard.AgainstInvalidEmail` clause.
*   **Targeted AI Verifiable End Result:** Successful execution of all integration tests in the CI pipeline (`docs/PRDMasterPlan.v2.md:98`).
*   **Unit Under Test:** `ServiceExample.CreateUser(string name, int age, string email)`
*   **Interactions to Test:**
    *   Call to `Guard.AgainstInvalidEmail` with an `email` value in an invalid format.
*   **Collaborators & Mocking Strategy:** No explicit mocking. The test verifies the observable outcome (exception) of the `ServiceExample`'s interaction with `Guard.AgainstInvalidEmail`.
*   **Expected Observable Outcome:** An `ArgumentException` is thrown by the `CreateUser` method, with a message indicating the `email` argument is not a valid email address.
*   **Test Data & Mock Configuration:**
    *   `name`: "Peter Pan"
    *   `age`: 25
    *   `email`: "invalid-email"
*   **Recursive Testing Scope:** `[Smoke]`, `[Daily/Nightly Regression]`, `[Full Regression]`

## 6. AI Verifiable Completion Criteria for this Test Plan

The AI verifiable completion criterion for this Test Plan document is its successful creation and storage at the specified path: `docs/test_plans/integration_test_plan.md`. The content within this document, including the detailed test cases, recursive testing strategy, and adherence to London School TDD principles, serves as the comprehensive blueprint for subsequent test implementation by other agents.