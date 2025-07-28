# Master Acceptance Test Plan v2

This document outlines the high-level end-to-end acceptance tests for the modernized Guard Clause C# library. These tests define the ultimate success criteria for the project.

## 1. Core Functionality Acceptance Tests

### 1.1. Null and Empty Checks
*   **Test Case:** `Test_Null_Validation`
*   **Description:** Verify that `Guard.Against.Null` throws `ArgumentNullException` for null objects and does not throw for non-null objects.
*   **Acceptance Criteria:** The test passes.

*   **Test Case:** `Test_NullOrEmpty_Validation`
*   **Description:** Verify that `Guard.Against.NullOrEmpty` throws for null/empty strings and collections.
*   **Acceptance Criteria:** The test passes.

*   **Test Case:** `Test_NullOrWhiteSpace_Validation`
*   **Description:** Verify that `Guard.Against.NullOrWhiteSpace` throws for null/whitespace strings.
*   **Acceptance Criteria:** The test passes.

### 1.2. Numeric Range Checks
*   **Test Case:** `Test_OutOfRange_Validation`
*   **Description:** Verify that `Guard.Against.OutOfRange` throws `ArgumentOutOfRangeException` for values outside the specified range.
*   **Acceptance Criteria:** The test passes.

*   **Test Case:** `Test_Zero_And_Negative_Validation`
*   **Description:** Verify that `Guard.Against.Zero` and `Guard.Against.Negative` throw exceptions for zero and negative numbers respectively.
*   **Acceptance Criteria:** The test passes.

## 2. New Feature Acceptance Tests

### 2.1. Format Validation
*   **Test Case:** `Test_InvalidEmail_Validation`
*   **Description:** Verify that `Guard.Against.InvalidEmail` throws `ArgumentException` for invalid email formats and does not throw for valid ones.
*   **Acceptance Criteria:** The test passes.

*   **Test Case:** `Test_InvalidUrl_Validation`
*   **Description:** Verify that `Guard.Against.InvalidUrl` throws `ArgumentException` for invalid URL formats and does not throw for valid ones.
*   **Acceptance Criteria:** The test passes.

### 2.2. Custom and Foundational Validation
*   **Test Case:** `Test_CustomCondition_Validation`
*   **Description:** Verify that `Guard.Against.CustomCondition` throws an exception when the custom predicate is true.
*   **Acceptance Criteria:** The test passes.

*   **Test Case:** `Test_NotFound_Validation`
*   **Description:** Verify that `Guard.Against.NotFound` throws a custom exception when a key is not found in a dictionary or a record in a database.
*   **Acceptance Criteria:** The test passes.

*   **Test Case:** `Test_Boolean_Enum_Guid_Validation`
*   **Description:** Verify that the guard clauses for `bool`, `enum`, and `Guid` types function correctly.
*   **Acceptance Criteria:** The tests pass.

## 3. Non-Functional Acceptance Tests

### 3.1. Performance
*   **Test Case:** `Test_Performance_Benchmarks`
*   **Description:** The performance of the guard clauses, especially for regex-based validations, is comparable to or better than leading libraries like Ardalis.GuardClauses.
*   **Acceptance Criteria:** The benchmark results in `docs/benchmark_results.md` meet the defined performance targets.

### 3.2. Extensibility
*   **Test Case:** `Test_Custom_Guard_Clause_Extension`
*   **Description:** A developer can easily create and use a new custom guard clause by extending `IGuardClause`.
*   **Acceptance Criteria:** A sample custom guard clause can be created and a test using it passes, following the `docs/extensibility_guide.md`.

### 3.3. CI/CD Pipeline
*   **Test Case:** `Test_CI_Pipeline_Execution`
*   **Description:** The CI pipeline in GitHub Actions runs automatically on every push to the `main` branch.
*   **Acceptance Criteria:** The pipeline successfully builds the project, runs all unit and integration tests, and reports a "success" status. A failed test must result in a "failed" pipeline status.