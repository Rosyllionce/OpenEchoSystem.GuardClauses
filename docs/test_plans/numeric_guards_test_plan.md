# Test Plan: Numeric Guard Clauses

**Document Version:** 1.0
**Date:** 2025-07-04

## 1. Introduction

### 1.1. Document Purpose
This document provides a detailed test plan for the **Numeric Guard Clauses** feature of the `OpenEchoSystem.GuardClauses` library. It outlines the testing strategy, scope, and specific test cases required to verify the correct implementation and behavior of these guards.

### 1.2. Feature Under Test
This test plan covers the following numeric guard clauses, which will be implemented as extension methods on the `IGuardClause` interface:
*   `OutOfRange`
*   `Zero`
*   `Negative`

### 1.3. References
*   **Master Project Plan:** [`docs/PRDMasterPlan.v2.md`](docs/PRDMasterPlan.v2.md:53)
*   **Architecture Document:** [`docs/architecture/high_level_architecture.v2.md`](docs/architecture/high_level_architecture.v2.md:45)

---

## 2. Test Scope and AI Verifiable End Result

The scope of this test plan is strictly limited to the validation of the Numeric Guard Clauses.

The successful execution of all test cases defined in this plan will satisfy the following AI Verifiable End Result from the Master Project Plan:

*   **Phase:** 3 - Core Guard Clause Implementation
*   **Micro Task:** 2 - Implement Numeric Guard Clauses
*   **AI Verifiable Deliverable:** Unit tests for `Guard.Against.OutOfRange`, `Zero`, and `Negative` pass.

---

## 3. Test Strategy

### 3.1. London School of TDD Principles

This test plan adopts the **London School of TDD** (Interaction-Based Testing). Our focus is on verifying the **observable behavior** of our units under test, not their internal state.

*   **Unit Under Test:** Each guard clause extension method (e.g., `public static void Zero(...)`).
*   **Collaborators:** For these specific guard clauses, there are no external dependencies to mock. The primary "collaboration" is with the .NET runtime to throw exceptions.
*   **Interaction Testing:** We will test the interaction by calling the guard method with specific inputs and verifying the outcome.
*   **Observable Outcome:** The sole observable outcome is whether the method **throws a specific exception** or **completes execution without throwing an exception**. This is a clear, verifiable behavior.

### 3.2. Recursive Testing (Regression Strategy)

A comprehensive and frequent regression testing strategy is crucial to maintain stability as the library evolves.

#### 3.2.1. Regression Triggers
Test suites will be automatically re-executed based on the following Software Development Life Cycle (SDLC) touchpoints:

1.  **On-Commit/Push:** All tests related to the changed files will run on every commit to a feature branch. The full suite runs on every push to the `main` branch via the CI pipeline.
2.  **Targeted File Changes:** Any modification to `src/GuardClauseNumericExtensions.cs` will trigger the entire numeric guard test suite.
3.  **Core Component Changes:** Any modification to core files like `src/IGuardClause.cs`, `src/Guard.cs`, or `src/GuardClause.cs` will trigger the entire project test suite.
4.  **Pre-Release:** The full test suite for the entire library will be executed before any new release is packaged.

#### 3.2.2. Test Categorization
To facilitate selective re-testing, each test case will be categorized:

*   **`Category: CriticalPath`**: Tests the "happy path" where valid input is provided and no exception should be thrown. These verify the guard does not interfere with correct code flow.
*   **`Category: NegativePath`**: Tests the "unhappy path" where invalid input is provided and an exception is expected. These are the core validation tests.
*   **`Category: EdgeCase`**: Tests boundary conditions (e.g., `int.MaxValue`, `double.Epsilon`, values at the exact limit of a range).

#### 3.2.3. Test Selection for Regression
The test selection strategy is as follows:

*   **Local Development:** Developers working on numeric guards should run all tests tagged for `NumericGuards`.
*   **CI (Feature Branch):** The CI pipeline will intelligently run tests related to the changed files. If `GuardClauseNumericExtensions.cs` is changed, all `NumericGuards` tests run.
*   **CI (Main Branch):** The full test suite (`CriticalPath`, `NegativePath`, `EdgeCase` for all features) is executed.

---

## 4. Test Environment

*   **Test Framework:** xUnit
*   **Assertion Library:** FluentAssertions will be used for more readable and expressive assertion syntax (e.g., `action.Should().Throw<ArgumentException>().WithMessage(...)`).

---

## 5. Test Cases

### 5.1. Guard: `OutOfRange`

#### Test Case: NUM-OOR-001
*   **Target AI Verifiable End Result:** `docs/PRDMasterPlan.v2.md` (Phase 3, Task 2)
*   **Description:** Verifies that `OutOfRange` throws an `ArgumentOutOfRangeException` when the input value is less than the specified `from` limit.
*   **Unit Under Test:** `GuardClauseNumericExtensions.OutOfRange<T>(this IGuardClause, T, T, T, ...)`
*   **Input Data:** `input: 4`, `from: 5`, `to: 10`
*   **Expected Observable Outcome (AI Verifiable):** Throws `ArgumentOutOfRangeException`.
*   **Recursive Testing Scope:** `NegativePath`

#### Test Case: NUM-OOR-002
*   **Target AI Verifiable End Result:** `docs/PRDMasterPlan.v2.md` (Phase 3, Task 2)
*   **Description:** Verifies that `OutOfRange` throws an `ArgumentOutOfRangeException` when the input value is greater than the specified `to` limit.
*   **Unit Under Test:** `GuardClauseNumericExtensions.OutOfRange<T>(this IGuardClause, T, T, T, ...)`
*   **Input Data:** `input: 11`, `from: 5`, `to: 10`
*   **Expected Observable Outcome (AI Verifiable):** Throws `ArgumentOutOfRangeException`.
*   **Recursive Testing Scope:** `NegativePath`

#### Test Case: NUM-OOR-003
*   **Target AI Verifiable End Result:** `docs/PRDMasterPlan.v2.md` (Phase 3, Task 2)
*   **Description:** Verifies that `OutOfRange` does not throw an exception when the input value is within the specified range.
*   **Unit Under Test:** `GuardClauseNumericExtensions.OutOfRange<T>(this IGuardClause, T, T, T, ...)`
*   **Input Data:** `input: 7`, `from: 5`, `to: 10`
*   **Expected Observable Outcome (AI Verifiable):** Does not throw any exception.
*   **Recursive Testing Scope:** `CriticalPath`

#### Test Case: NUM-OOR-004
*   **Target AI Verifiable End Result:** `docs/PRDMasterPlan.v2.md` (Phase 3, Task 2)
*   **Description:** Verifies that `OutOfRange` does not throw an exception when the input value is equal to the `from` limit.
*   **Unit Under Test:** `GuardClauseNumericExtensions.OutOfRange<T>(this IGuardClause, T, T, T, ...)`
*   **Input Data:** `input: 5`, `from: 5`, `to: 10`
*   **Expected Observable Outcome (AI Verifiable):** Does not throw any exception.
*   **Recursive Testing Scope:** `EdgeCase`

#### Test Case: NUM-OOR-005
*   **Target AI Verifiable End Result:** `docs/PRDMasterPlan.v2.md` (Phase 3, Task 2)
*   **Description:** Verifies that `OutOfRange` does not throw an exception when the input value is equal to the `to` limit.
*   **Unit Under Test:** `GuardClauseNumericExtensions.OutOfRange<T>(this IGuardClause, T, T, T, ...)`
*   **Input Data:** `input: 10`, `from: 5`, `to: 10`
*   **Expected Observable Outcome (AI Verifiable):** Does not throw any exception.
*   **Recursive Testing Scope:** `EdgeCase`

### 5.2. Guard: `Zero`

#### Test Case: NUM-ZERO-001
*   **Target AI Verifiable End Result:** `docs/PRDMasterPlan.v2.md` (Phase 3, Task 2)
*   **Description:** Verifies that `Zero` throws an `ArgumentException` when the input value is 0.
*   **Unit Under Test:** `GuardClauseNumericExtensions.Zero<T>(this IGuardClause, T, ...)`
*   **Input Data:** `input: 0`
*   **Expected Observable Outcome (AI Verifiable):** Throws `ArgumentException`.
*   **Recursive Testing Scope:** `NegativePath`

#### Test Case: NUM-ZERO-002
*   **Target AI Verifiable End Result:** `docs/PRDMasterPlan.v2.md` (Phase 3, Task 2)
*   **Description:** Verifies that `Zero` does not throw an exception for a non-zero positive value.
*   **Unit Under Test:** `GuardClauseNumericExtensions.Zero<T>(this IGuardClause, T, ...)`
*   **Input Data:** `input: 1`
*   **Expected Observable Outcome (AI Verifiable):** Does not throw any exception.
*   **Recursive Testing Scope:** `CriticalPath`

#### Test Case: NUM-ZERO-003
*   **Target AI Verifiable End Result:** `docs/PRDMasterPlan.v2.md` (Phase 3, Task 2)
*   **Description:** Verifies that `Zero` does not throw an exception for a non-zero negative value.
*   **Unit Under Test:** `GuardClauseNumericExtensions.Zero<T>(this IGuardClause, T, ...)`
*   **Input Data:** `input: -1`
*   **Expected Observable Outcome (AI Verifiable):** Does not throw any exception.
*   **Recursive Testing Scope:** `CriticalPath`

### 5.3. Guard: `Negative`

#### Test Case: NUM-NEG-001
*   **Target AI Verifiable End Result:** `docs/PRDMasterPlan.v2.md` (Phase 3, Task 2)
*   **Description:** Verifies that `Negative` throws an `ArgumentException` when the input value is less than 0.
*   **Unit Under Test:** `GuardClauseNumericExtensions.Negative<T>(this IGuardClause, T, ...)`
*   **Input Data:** `input: -1`
*   **Expected Observable Outcome (AI Verifiable):** Throws `ArgumentException`.
*   **Recursive Testing Scope:** `NegativePath`

#### Test Case: NUM-NEG-002
*   **Target AI Verifiable End Result:** `docs/PRDMasterPlan.v2.md` (Phase 3, Task 2)
*   **Description:** Verifies that `Negative` does not throw an exception when the input value is 0.
*   **Unit Under Test:** `GuardClauseNumericExtensions.Negative<T>(this IGuardClause, T, ...)`
*   **Input Data:** `input: 0`
*   **Expected Observable Outcome (AI Verifiable):** Does not throw any exception.
*   **Recursive Testing Scope:** `EdgeCase`

#### Test Case: NUM-NEG-003
*   **Target AI Verifiable End Result:** `docs/PRDMasterPlan.v2.md` (Phase 3, Task 2)
*   **Description:** Verifies that `Negative` does not throw an exception when the input value is greater than 0.
*   **Unit Under Test:** `GuardClauseNumericExtensions.Negative<T>(this IGuardClause, T, ...)`
*   **Input Data:** `input: 1`
*   **Expected Observable Outcome (AI Verifiable):** Does not throw any exception.
*   **Recursive Testing Scope:** `CriticalPath`