# Master Acceptance Test Plan: Modernized Guard Clause C# Library

## 1. Introduction and Project Goal

This Master Acceptance Test Plan defines the ultimate success criteria for the modernized Guard Clause C# Library, ensuring it meets the user's overall project goals. These high-level tests are broad, user-centric, and verify complete end-to-end system functionality and integration from the perspective of a consuming application developer. They embody the Specification phase of the SPARC framework, focusing on observable behaviors and outcomes.

The core objective of the modernized library is to provide a comprehensive, high-performance, and extensible set of guard clauses that leverage modern C# features like `[CallerArgumentExpression]` for improved developer experience and robust defensive programming.

## 2. High-Level Test Strategy

The testing strategy is informed by the "Optimal High-Level Testing Strategy for Guard Clause C# Library" report and adheres to London School TDD principles, focusing on behavior and outcomes. For a library, high-level acceptance tests verify the core contracts and expected behaviors, ensuring it acts as a reliable "fail-fast" component when integrated into consuming applications.

**Key Principles:**
*   **Understandable:** Tests clearly demonstrate intended guard clause behavior.
*   **Independent:** Each test is self-contained.
*   **Reliable:** Consistent pass/fail results given the same input.
*   **Clear Feedback:** Failures provide precise information (exception type, message, parameter name).
*   **Real Data Usage & Scenarios:** Use diverse, representative inputs simulating practical application usage.
*   **Black-Box & Implementation-Agnostic:** Focus on public API behavior, not internal implementation.
*   **Performance Awareness:** Basic checks to ensure no significant overhead, contributing to "Launch Readiness".

## 3. Test Phases and Scenarios

High-level acceptance testing will be divided into the following phases, each with specific scenarios to cover the library's core functionality and integration points.

### Phase 1: Core Guard Clause Functionality Verification

This phase focuses on validating the fundamental "fail-fast" principle for each primary guard clause, ensuring correct behavior for both valid and invalid inputs.

#### Test Scenario 1.1: `Guard.Against.Null()`

**Objective:** Verify that `Guard.Against.Null()` correctly identifies null inputs and throws the appropriate exception, while allowing non-null inputs to pass.
**AI Verifiable Completion Criteria:**
*   **Valid Input:** No `ArgumentNullException` is thrown when a non-null object is passed to `Guard.Against.Null()`.
*   **Invalid Input:** An `ArgumentNullException` is thrown when a null object is passed to `Guard.Against.Null()`.
*   **Exception Message:** The `ArgumentNullException` message clearly indicates that the argument cannot be null.
*   **Parameter Name:** The `ParamName` property of the `ArgumentNullException` correctly matches the name of the input parameter (inferred by `[CallerArgumentExpression]`).

#### Test Scenario 1.2: `Guard.Against.NullOrEmpty()` (Strings)

**Objective:** Verify that `Guard.Against.NullOrEmpty()` correctly identifies null or empty strings and throws the appropriate exception, while allowing valid strings to pass.
**AI Verifiable Completion Criteria:**
*   **Valid Input:** No exception is thrown when a non-null, non-empty string is passed to `Guard.Against.NullOrEmpty()`.
*   **Invalid Input (Null):** An `ArgumentNullException` is thrown when a null string is passed to `Guard.Against.NullOrEmpty()`.
*   **Invalid Input (Empty):** An `ArgumentException` is thrown when an empty string (`""`) is passed to `Guard.Against.NullOrEmpty()`.
*   **Exception Messages:** The exception messages clearly indicate the null or empty condition.
*   **Parameter Name:** The `ParamName` property of the exceptions correctly matches the input parameter name.

#### Test Scenario 1.3: `Guard.Against.NullOrEmpty()` (Collections)

**Objective:** Verify that `Guard.Against.NullOrEmpty()` correctly identifies null or empty collections and throws the appropriate exception, while allowing valid collections to pass.
**AI Verifiable Completion Criteria:**
*   **Valid Input:** No exception is thrown when a non-null, non-empty collection is passed to `Guard.Against.NullOrEmpty()`.
*   **Invalid Input (Null):** An `ArgumentNullException` is thrown when a null collection is passed to `Guard.Against.NullOrEmpty()`.
*   **Invalid Input (Empty):** An `ArgumentException` is thrown when an empty collection is passed to `Guard.Against.NullOrEmpty()`.
*   **Exception Messages:** The exception messages clearly indicate the null or empty condition.
*   **Parameter Name:** The `ParamName` property of the exceptions correctly matches the input parameter name.

#### Test Scenario 1.4: `Guard.Against.NullOrWhiteSpace()` (Strings)

**Objective:** Verify that `Guard.Against.NullOrWhiteSpace()` correctly identifies null, empty, or whitespace-only strings and throws the appropriate exception, while allowing valid strings to pass.
**AI Verifiable Completion Criteria:**
*   **Valid Input:** No exception is thrown when a non-null, non-empty, non-whitespace string is passed to `Guard.Against.NullOrWhiteSpace()`.
*   **Invalid Input (Null):** An `ArgumentNullException` is thrown when a null string is passed to `Guard.Against.NullOrWhiteSpace()`.
*   **Invalid Input (Empty):** An `ArgumentException` is thrown when an empty string (`""`) is passed to `Guard.Against.NullOrWhiteSpace()`.
*   **Invalid Input (Whitespace):** An `ArgumentException` is thrown when a whitespace-only string (`"   "`) is passed to `Guard.Against.NullOrWhiteSpace()`.
*   **Exception Messages:** The exception messages clearly indicate the null, empty, or whitespace condition.
*   **Parameter Name:** The `ParamName` property of the exceptions correctly matches the input parameter name.

#### Test Scenario 1.5: `Guard.Against.OutOfRange()` (Numbers)

**Objective:** Verify that `Guard.Against.OutOfRange()` correctly identifies numeric inputs outside a specified range and throws the appropriate exception, while allowing in-range values to pass.
**AI Verifiable Completion Criteria:**
*   **Valid Input (Inclusive Boundaries):** No `ArgumentOutOfRangeException` is thrown when a number within the specified inclusive range is passed.
*   **Invalid Input (Below Lower Bound):** An `ArgumentOutOfRangeException` is thrown when a number below the lower bound is passed.
*   **Invalid Input (Above Upper Bound):** An `ArgumentOutOfRangeException` is thrown when a number above the upper bound is passed.
*   **Exception Message:** The `ArgumentOutOfRangeException` message clearly indicates the range violation.
*   **Parameter Name:** The `ParamName` property of the `ArgumentOutOfRangeException` correctly matches the input parameter name.

#### Test Scenario 1.6: `Guard.Against.Zero()` (Numbers)

**Objective:** Verify that `Guard.Against.Zero()` correctly identifies zero numeric inputs and throws the appropriate exception, while allowing non-zero values to pass.
**AI Verifiable Completion Criteria:**
*   **Valid Input:** No `ArgumentException` is thrown when a non-zero number is passed to `Guard.Against.Zero()`.
*   **Invalid Input:** An `ArgumentException` is thrown when a zero number (0, 0.0, etc.) is passed to `Guard.Against.Zero()`.
*   **Exception Message:** The exception message clearly indicates that the argument cannot be zero.
*   **Parameter Name:** The `ParamName` property of the `ArgumentException` correctly matches the input parameter name.

#### Test Scenario 1.7: `Guard.Against.Negative()` (Numbers)

**Objective:** Verify that `Guard.Against.Negative()` correctly identifies negative numeric inputs and throws the appropriate exception, while allowing positive or zero values to pass.
**AI Verifiable Completion Criteria:**
*   **Valid Input (Positive/Zero):** No `ArgumentOutOfRangeException` is thrown when a positive or zero number is passed to `Guard.Against.Negative()`.
*   **Invalid Input:** An `ArgumentOutOfRangeException` is thrown when a negative number is passed to `Guard.Against.Negative()`.
*   **Exception Message:** The `ArgumentOutOfRangeException` message clearly indicates that the argument cannot be negative.
*   **Parameter Name:** The `ParamName` property of the `ArgumentOutOfRangeException` correctly matches the input parameter name.

#### Test Scenario 1.8: `Guard.Against.InvalidFormat()` (Strings with Regex)

**Objective:** Verify that `Guard.Against.InvalidFormat()` correctly identifies strings that do not match a specified regex pattern and throws the appropriate exception, while allowing valid format strings to pass.
**AI Verifiable Completion Criteria:**
*   **Valid Input:** No `ArgumentException` is thrown when a string matching the regex pattern is passed.
*   **Invalid Input:** An `ArgumentException` is thrown when a string not matching the regex pattern is passed.
*   **Exception Message:** The exception message clearly indicates an invalid format.
*   **Parameter Name:** The `ParamName` property of the `ArgumentException` correctly matches the input parameter name.

#### Test Scenario 1.9: `Guard.Against.NotFound()` (Generic for ID/Key Lookups)

**Objective:** Verify that `Guard.Against.NotFound()` correctly identifies when a lookup result is null (indicating not found) and throws the appropriate exception.
**AI Verifiable Completion Criteria:**
*   **Valid Input:** No `NotFoundException` (or `ArgumentException`) is thrown when a non-null object is passed.
*   **Invalid Input:** A `NotFoundException` (or `ArgumentException`) is thrown when a null object is passed.
*   **Exception Message:** The exception message clearly indicates the item was not found.
*   **Parameter Name:** The `ParamName` property of the exception correctly matches the input parameter name.

### Phase 2: Library Extensibility and Integration Simulation

This phase focuses on verifying the library's extensibility mechanism and simulating its usage in a broader context.

#### Test Scenario 2.1: Custom Guard Clause Integration

**Objective:** Verify that developers can easily extend the library by creating custom guard clauses, and these custom guards function correctly within the `Guard.Against` fluent API.
**AI Verifiable Completion Criteria:**
*   A custom guard clause extension method is successfully created and callable via `Guard.Against.MyCustomGuard()`.
*   When the custom guard's condition for an invalid input is met, the expected custom exception (or `ArgumentException`) is thrown.
*   When the custom guard's condition for a valid input is met, no exception is thrown.
*   The exception message and parameter name for the custom guard are correct.

#### Test Scenario 2.2: Performance Impact (Basic Check)

**Objective:** Conduct a basic high-level check to ensure that the guard clauses do not introduce significant performance overhead in typical usage scenarios. This is a preliminary check, not a full benchmark.
**AI Verifiable Completion Criteria:**
*   A simple test that executes a common guard clause (e.g., `Guard.Against.Null`) many times within a short duration completes without excessive delay (e.g., within a predefined millisecond threshold). This confirms the "lightweight and fast" requirement at a high level.

## 4. AI Verifiability of Tests

All test scenarios are designed to be AI verifiable. This means an AI can programmatically execute the tests and determine success or failure based on:
*   **Absence/Presence of Exceptions:** Checking if a specific exception type (e.g., `ArgumentNullException`, `ArgumentOutOfRangeException`) is thrown or not thrown.
*   **Exception Properties:** Inspecting the `Message` and `ParamName` properties of thrown exceptions for correctness.
*   **Return Values:** (Less common for Guard Clauses, but applicable if any guard clause were to return a value).
*   **Execution Time:** Basic checks on execution duration for performance validation.

The use of xUnit and FluentAssertions provides a robust framework for defining these verifiable outcomes within the test code itself.

## 5. Tools and Frameworks

*   **Testing Framework:** xUnit.net (for its extensibility, data-driven test support, and alignment with independent tests).
*   **Assertion Library:** FluentAssertions (for highly readable and expressive assertions).
*   **Code Coverage:** Coverlet (to ensure test coverage, aiding in identifying untested paths).
*   **Benchmarking (Optional, for deeper analysis):** BenchmarkDotNet (for precise performance comparisons, contributing to "Launch Readiness" non-functional requirements).

## 6. Conclusion

This Master Acceptance Test Plan, along with the implemented high-level end-to-end acceptance tests, serves as the definitive Specification for the Guard Clause C# Library. Passing these tests signifies that the library fulfills its core requirements, adheres to modern C# best practices, and is ready for integration into real-world applications. This document guides all subsequent development efforts, ensuring a focus on delivering a robust, performant, and user-friendly library.