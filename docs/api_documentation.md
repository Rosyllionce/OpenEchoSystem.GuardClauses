# API Documentation for OpenEchoSystem.GuardClauses

This document provides a comprehensive overview of the public API for the `OpenEchoSystem.GuardClauses` library. It is generated from the DocFX .yml files.

## Namespace: OpenEchoSystem.GuardClauses

### `EnumGuardExtensions` Class

**Summary:** Provides extension methods for `IGuardClause` to validate enum values.

**Syntax:** `public static class EnumGuardExtensions`

#### Methods

| Name                                       | Summary                                                                                                     |
| ------------------------------------------ | ----------------------------------------------------------------------------------------------------------- |
| `OutOfRange<TEnum>(IGuardClause, TEnum, string?)` | Throws an `ArgumentOutOfRangeException` if the provided enum `input` is not a defined value for its enum type. |

### `Guard` Class

**Summary:** Provides a static entry point for all guard clause validations.

**Syntax:** `public static class Guard`

#### Properties

| Name      | Type           | Summary                                                                      |
| --------- | -------------- | ---------------------------------------------------------------------------- |
| `Against` | `IGuardClause` | Gets an instance of `IGuardClause` to begin a guard validation chain. |

### `GuardClauseGuidExtensions` Class

**Summary:** Provides extension methods for `IGuardClause` to validate `Guid` values.

**Syntax:** `public static class GuardClauseGuidExtensions`

#### Methods

| Name                               | Summary                                                              |
| ---------------------------------- | -------------------------------------------------------------------- |
| `InvalidFormat(IGuardClause, Guid, string)` | Throws an `ArgumentException` if the provided `Guid` `value` is empty. |

### `GuardClauseNumericExtensions` Class

**Summary:** Provides extension methods for `IGuardClause` to validate numeric values.

**Syntax:** `public static class GuardClauseNumericExtensions`

#### Methods

| Name                                         | Summary                                                                                                                            |
| -------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| `OutOfRange<T>(IGuardClause, T, T, T, string?)` | Throws an `ArgumentOutOfRangeException` if the provided numeric `value` is outside the specified `minimum` and `maximum` range (inclusive). |
| `Zero<T>(IGuardClause, T, string?)`           | Throws an `ArgumentException` if the provided numeric `value` is zero.                                                              |
| `Negative<T>(IGuardClause, T, string?)`       | Throws an `ArgumentException` if the provided numeric `value` is negative.                                                          |

### `GuardClauseObjectExtensions` Class

**Summary:** Provides extension methods for `IGuardClause` to validate objects.

**Syntax:** `public static class GuardClauseObjectExtensions`

#### Methods

| Name                                                     | Summary                                                                                                                                                           |
| -------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `Null<T>(IGuardClause, T?, string?)`                      | Throws an `ArgumentNullException` if the provided object `input` is null.                                                                                         |
| `NotFound(IGuardClause, object?, string?)`                | Throws an `ArgumentException` if the provided object `input` is null, indicating that an item was not found.                                                        |
| `NullOrEmpty<T>(IGuardClause, ICollection<T>?, string?)`   | Throws an `ArgumentNullException` if the provided collection `input` is null, or an `ArgumentException` if it is empty.                                            |
| `OutOfRange(IGuardClause, DateTime, DateTime, DateTime, string?)` | Throws an `ArgumentOutOfRangeException` if the provided `DateTime` `input` is outside the specified `minimum` and `maximum` range (inclusive). |
| `CustomCondition(IGuardClause, bool, string?)`            | Throws an `ArgumentException` if the provided `condition` is true. This method allows for custom validation logic to be integrated with the guard clause pattern. |

### `GuardClauseStringExtensions` Class

**Summary:** Provides extension methods for `IGuardClause` to validate string values.

**Syntax:** `public static class GuardClauseStringExtensions`

#### Methods

| Name                                               | Summary                                                                                                                                                             |
| -------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `NullOrEmpty(IGuardClause, string?, string?)`        | Throws an `ArgumentNullException` if the provided string `input` is null, or an `ArgumentException` if it is empty.                                                  |
| `NullOrWhiteSpace(IGuardClause, string?, string?)`   | Throws an `ArgumentNullException` if the provided string `input` is null, or an `ArgumentException` if it is empty or consists only of white-space characters.        |
| `InvalidFormat(IGuardClause, string, string, string?)` | Throws an `ArgumentException` if the provided string `input` does not match the specified `regexPattern`.                                                           |
| `InvalidEmail(IGuardClause, string, string?)`        | Throws an `ArgumentException` if the provided string `input` is not a valid email address.                                                                          |
| `InvalidUrl(IGuardClause, string, string?)`          | Throws an `ArgumentException` if the provided string `input` is not a valid absolute URL.                                                                           |

### `IGuardClause` Interface

**Summary:** Marker interface for guard clauses, providing an extensibility point.

**Syntax:** `public interface IGuardClause`

## Namespace: OpenEchoSystem.ValueGuards.Core

### `GuidGuard` Class

**Summary:** Provides common guard clauses for `Guid` values.

**Syntax:** `public static class GuidGuard`

#### Methods

| Name                               | Summary                                           |
| ---------------------------------- | ------------------------------------------------- |
| `GuardForEmpty(Guid, string, bool)` | Guards against an empty `Guid` value. |

### `NumericGuard` Class

**Summary:** Common Numeric-Type Guards

**Syntax:** `public static class NumericGuard`

#### Methods

| Name                                         | Summary                                                        |
| -------------------------------------------- | -------------------------------------------------------------- |
| `GuardForLessEqualZero(byte, string, bool)`    | Guards against a byte value being less than or equal to zero.    |
| `GuardForLessEqualZero(short, string, bool)`   | Guards against a short value being less than or equal to zero.   |
| `GuardForLessEqualZero(int, string, bool)`     | Guards against an integer value being less than or equal to zero.|
| `GuardForLessEqualZero(long, string, bool)`    | Guards against a long value being less than or equal to zero.    |
| `GuardForLessEqualZero(decimal, string, bool)` | Guards against a decimal value being less than or equal to zero. |
| `GuardForLessEqualZero(float, string, bool)`   | Guards against a float value being less than or equal to zero.   |

### `ObjectGuard` Class

**Summary:** Common Object Guards

**Syntax:** `public static class ObjectGuard`

#### Methods

| Name                                   | Summary                        |
| -------------------------------------- | ------------------------------ |
| `ForNullReference(object, string, bool)` | Guard For Object Null Reference |

### `StringGuard` Class

**Summary:** Common String Guards

**Syntax:** `public static class StringGuard`

#### Methods

| Name                                               | Summary                                                                                                                            |
| -------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| `EnsureNotNullOrEmptyAndWhiteSpace(string?, string?)` | Ensures that a string value is not null, empty, or white space. If it is, returns a default value.                               |
| `EnsureNotNullOrEmptyOrWhiteSpace(string?, string?, bool)` | Guards against a string value being null, empty, or consisting only of white-space characters.                               |
| `EnsureNotNullOrWhiteSpace(string?, string?, bool)` | Guards against a string value being null or consisting only of white-space characters.                                             |
| `EnsureNotNullOrEmpty(string?, string?, bool)`      | Guards against a string value being null or empty.                                                                                 |
