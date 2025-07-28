# Test Plan: String Format Guard Clauses

## 1. Introduction and Scope

This document outlines the granular test plan for the 'String Format Guard Clauses' feature of the OpenEchoSystem.GuardClauses library. This feature is a core component of the library's validation capabilities as defined in [`docs/PRDMasterPlan.v2.md`](docs/PRDMasterPlan.v2.md) under **Phase 3, Micro Task 3**.

The scope of this test plan covers the following extension methods on `IGuardClause`:
*   `InvalidFormat`
*   `InvalidEmail`
*   `InvalidUrl`

The tests are designed to verify the correct behavior of these guards, ensuring they throw the appropriate exceptions for invalid inputs and pass silently for valid inputs. This plan adheres to the **London School of TDD**, focusing on testing the observable outcomes (interactions) of the methods, which in this case is primarily whether an exception is thrown.

**AI-Verifiable Goal for this Test Plan:** The successful creation and verification of this test plan document at [`docs/test_plans/string_format_guards_test_plan.md`](docs/test_plans/string_format_guards_test_plan.md).

## 2. Test Strategy

### 2.1. London School of TDD Principles

Our testing approach will focus on the external behavior of the guard clause methods, not their internal implementation details. For these guard clauses, the primary observable outcome is the throwing of an `ArgumentException` or a derivative when the validation rule is violated.

*   **Collaborators:** The primary collaborator for these guards is the .NET regular expression engine. We will not mock this collaborator. Instead, we will test the interaction by providing inputs that cause the guard to either throw an exception or not, thus verifying the guard's correct usage of the regex engine.
*   **Outcome Verification:** Tests will pass or fail based on the exceptions thrown. We will assert the type of exception and, critically, the exception message. The message must correctly identify the parameter name that failed validation, which verifies the correct implementation of the `[CallerArgumentExpression]` attribute as specified in the architecture.

### 2.2. Recursive Testing (Regression Strategy)

To ensure ongoing stability, a multi-layered and recursive regression testing strategy will be employed. Tests will be tagged to run at different stages of the development lifecycle.

*   **Triggers for Re-running Tests:**
    1.  **On-Commit (CI):** Every time code is committed to a feature branch, all tests related to the `StringFormat` guards will be executed.
    2.  **Pull Request to `main`:** Before merging a PR into the `main` branch, the **Full Regression Suite** (all tests for the entire library) must pass.
    3.  **Nightly/Scheduled Builds:** The Full Regression Suite will be run on a nightly basis to catch any subtle regressions.
    4.  **Release Candidate Builds:** The Full Regression Suite is mandatory before packaging a new release.

*   **Test Selection and Tagging:**
    *   **`[Smoke]`:** A small, fast-running subset of tests covering the most critical happy path and one key negative path for each guard. These are designed to give a quick "is the build sane?" signal.
    *   **`[CI]`:** All unit tests for the `StringFormat` guards. This is the default scope for feature branch builds.
    *   **`[FullRegression]`:** All tests in the entire project. This is the default for PRs to `main` and release builds.

## 3. Test Cases

### 3.1. Guard: `InvalidFormat`

**Targeted AI Verifiable End Result:** Unit tests for `Guard.Against.InvalidFormat` pass. (From `PRDMasterPlan.v2.md`, Phase 3, Micro Task 3).

| Test Case ID | Description | Test Steps | Observable Outcome (AI-Verifiable Criterion) | Recursive Scope |
| :--- | :--- | :--- | :--- | :--- |
| **IF-001** | `InvalidFormat` should not throw for a string that matches the custom regex. | 1. Define a valid regex pattern (e.g., `@"^\d{3}-\d{2}-\d{4}$"` for a SSN).<br>2. Define an input string that matches the pattern (e.g., `"123-45-6789"`).<br>3. Call `Guard.Against.InvalidFormat(input, pattern)`. | Test passes if **no exception** is thrown. | `[Smoke]`, `[CI]` |
| **IF-002** | `InvalidFormat` should throw `ArgumentException` for a string that does not match the custom regex. | 1. Define a valid regex pattern (e.g., `@"^\d{3}$"`).<br>2. Define an input string that does **not** match (e.g., `"abc"`).<br>3. Call `Guard.Against.InvalidFormat(myVar, pattern)`. | Test passes if an `ArgumentException` is thrown with a message containing "does not match the required format" and the parameter name `"myVar"`. | `[Smoke]`, `[CI]` |
| **IF-003** | `InvalidFormat` should not throw for a null input string. | 1. Define a regex pattern.<br>2. Define a `string?` variable as `null`.<br>3. Call `Guard.Against.InvalidFormat(nullInput, pattern)`. | Test passes if **no exception** is thrown. Nulls are typically handled by `Null` guards. | `[CI]` |
| **IF-004** | `InvalidFormat` should not throw for an empty input string. | 1. Define a regex pattern.<br>2. Define an input string as `""`.<br>3. Call `Guard.Against.InvalidFormat(emptyInput, pattern)`. | Test passes if **no exception** is thrown. Emptiness is handled by `NullOrEmpty` guards. | `[CI]` |

### 3.2. Guard: `InvalidEmail`

**Targeted AI Verifiable End Result:** Unit tests for `Guard.Against.InvalidEmail` pass. Performance benchmarks show efficient execution. (From `PRDMasterPlan.v2.md`, Phase 3, Micro Task 3).

| Test Case ID | Description | Test Steps | Observable Outcome (AI-Verifiable Criterion) | Recursive Scope |
| :--- | :--- | :--- | :--- | :--- |
| **IE-001** | `InvalidEmail` should not throw for a valid email address. | 1. Define a valid email string (e.g., `"test@example.com"`).<br>2. Call `Guard.Against.InvalidEmail(validEmail)`. | Test passes if **no exception** is thrown. | `[Smoke]`, `[CI]` |
| **IE-002** | `InvalidEmail` should throw `ArgumentException` for an invalid email address (e.g., missing '@'). | 1. Define an invalid email string (e.g., `"test.example.com"`).<br>2. Call `Guard.Against.InvalidEmail(invalidEmailVar)`. | Test passes if an `ArgumentException` is thrown with a message containing "is not a valid email address" and the parameter name `"invalidEmailVar"`. | `[Smoke]`, `[CI]` |
| **IE-003** | `InvalidEmail` should throw `ArgumentException` for an invalid email address (e.g., missing domain). | 1. Define an invalid email string (e.g., `"test@"`).<br>2. Call `Guard.Against.InvalidEmail(anotherInvalid)`. | Test passes if an `ArgumentException` is thrown with a message containing "is not a valid email address" and the parameter name `"anotherInvalid"`. | `[CI]` |
| **IE-004** | `InvalidEmail` should not throw for a null input string. | 1. Define a `string?` variable as `null`.<br>2. Call `Guard.Against.InvalidEmail(nullEmail)`. | Test passes if **no exception** is thrown. | `[CI]` |
| **IE-005** | `InvalidEmail` should not throw for an empty input string. | 1. Define an input string as `""`.<br>2. Call `Guard.Against.InvalidEmail(emptyEmail)`. | Test passes if **no exception** is thrown. | `[CI]` |

### 3.3. Guard: `InvalidUrl`

**Targeted AI Verifiable End Result:** Unit tests for `Guard.Against.InvalidUrl` pass. Performance benchmarks show efficient execution. (From `PRDMasterPlan.v2.md`, Phase 3, Micro Task 3).

| Test Case ID | Description | Test Steps | Observable Outcome (AI-Verifiable Criterion) | Recursive Scope |
| :--- | :--- | :--- | :--- | :--- |
| **IU-001** | `InvalidUrl` should not throw for a valid absolute URL. | 1. Define a valid URL string (e.g., `"https://www.example.com/path?query=1"`).<br>2. Call `Guard.Against.InvalidUrl(validUrl)`. | Test passes if **no exception** is thrown. | `[Smoke]`, `[CI]` |
| **IU-002** | `InvalidUrl` should throw `ArgumentException` for an invalid URL (e.g., invalid scheme). | 1. Define an invalid URL string (e.g., `"htp://www.example.com"`).<br>2. Call `Guard.Against.InvalidUrl(invalidUrlVar)`. | Test passes if an `ArgumentException` is thrown with a message containing "is not a valid URL" and the parameter name `"invalidUrlVar"`. | `[Smoke]`, `[CI]` |
| **IU-003** | `InvalidUrl` should throw `ArgumentException` for a string that is not a URL. | 1. Define an invalid URL string (e.g., `"just a string"`).<br>2. Call `Guard.Against.InvalidUrl(notAUrl)`. | Test passes if an `ArgumentException` is thrown with a message containing "is not a valid URL" and the parameter name `"notAUrl"`. | `[CI]` |
| **IU-004** | `InvalidUrl` should not throw for a null input string. | 1. Define a `string?` variable as `null`.<br>2. Call `Guard.Against.InvalidUrl(nullUrl)`. | Test passes if **no exception** is thrown. | `[CI]` |
| **IU-005** | `InvalidUrl` should not throw for an empty input string. | 1. Define an input string as `""`.<br>2. Call `Guard.Against.InvalidUrl(emptyUrl)`. | Test passes if **no exception** is thrown. | `[CI]` |

## 4. Test Data Examples

*   **Valid Emails:** `test@domain.com`, `firstname.lastname@domain.co.uk`, `email@subdomain.domain.com`
*   **Invalid Emails:** `plainaddress`, `#@%^%#$@#$@#.com`, `@domain.com`, `Joe Smith <email@domain.com>`, `email.domain.com`, `email@domain@domain.com`
*   **Valid URLs:** `http://foo.com/blah_blah`, `https://www.example.com/foo/?bar=baz&inga=42&quux`, `http://userid:password@example.com:8080`
*   **Invalid URLs:** `foo.com`, `http://`, `https://.com`, `://
`
## 5. Test Environment

*   **Framework:** xUnit
*   **Runner:** .NET Test SDK
*   **Target Framework:** .NET 9.0
*   **Assertions:** xUnit's built-in assertion library (e.g., `Assert.Throws<T>`).