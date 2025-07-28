using System;
using Xunit;
using OpenEchoSystem.GuardClauses;

namespace OpenEchoSystem.GuardClauses.UnitTests.StringFormatGuards
{
    public class StringFormatGuardClauseTests
    {
        // 3.1. Guard: InvalidFormat

        [Theory]
        [InlineData(@"^\d{3}-\d{2}-\d{4}$", "123-45-6789")] // IF-001
        public void InvalidFormat_ShouldNotThrow_ForMatchingRegex(string pattern, string input)
        {
            Guard.Against.InvalidFormat(input, pattern);
        }

        [Fact]
        public void InvalidFormat_ShouldThrowArgumentException_ForNonMatchingRegex() // IF-002
        {
            var myVar = "abc";
            var pattern = @"^\d{3}$";
            var ex = Assert.Throws<ArgumentException>(() => Guard.Against.InvalidFormat(myVar, pattern));
            Assert.Contains("does not match the required format", ex.Message);
            Assert.Contains(nameof(myVar), ex.Message);
        }

        [Fact]
        public void InvalidFormat_ShouldNotThrow_ForNullInput() // IF-003
        {
            string? nullInput = null;
            var pattern = @"^\d{3}$";
            Guard.Against.InvalidFormat(nullInput, pattern);
        }

        [Fact]
        public void InvalidFormat_ShouldNotThrow_ForEmptyInput() // IF-004
        {
            var emptyInput = "";
            var pattern = @"^\d{3}$";
            Guard.Against.InvalidFormat(emptyInput, pattern);
        }

        // 3.2. Guard: InvalidEmail

        [Theory]
        [InlineData("test@example.com")] // IE-001
        [InlineData("firstname.lastname@domain.co.uk")]
        public void InvalidEmail_ShouldNotThrow_ForValidEmail(string validEmail)
        {
            Guard.Against.InvalidEmail(validEmail);
        }

        [Theory]
        [InlineData("test.example.com", "invalidEmailVar")] // IE-002
        [InlineData("test@", "anotherInvalid")] // IE-003
        [InlineData("plainaddress", "notAnEmail")]
        public void InvalidEmail_ShouldThrowArgumentException_ForInvalidEmail(string invalidEmail, string paramName)
        {
            var ex = Assert.Throws<ArgumentException>(() => Guard.Against.InvalidEmail(invalidEmail, paramName));
            Assert.Contains("is not a valid email address", ex.Message);
            Assert.Contains(paramName, ex.Message);
        }

        [Fact]
        public void InvalidEmail_ShouldNotThrow_ForNullInput() // IE-004
        {
            string? nullEmail = null;
            Guard.Against.InvalidEmail(nullEmail);
        }

        [Fact]
        public void InvalidEmail_ShouldNotThrow_ForEmptyInput() // IE-005
        {
            var emptyEmail = "";
            Guard.Against.InvalidEmail(emptyEmail);
        }

        // 3.3. Guard: InvalidUrl

        [Theory]
        [InlineData("https://www.example.com/path?query=1")] // IU-001
        [InlineData("http://foo.com/blah_blah")]
        public void InvalidUrl_ShouldNotThrow_ForValidUrl(string validUrl)
        {
            Guard.Against.InvalidUrl(validUrl);
        }

        [Theory]
        [InlineData("htp://www.example.com", "invalidUrlVar")] // IU-002
        [InlineData("just a string", "notAUrl")] // IU-003
        [InlineData("foo.com", "anotherInvalidUrl")]
        public void InvalidUrl_ShouldThrowArgumentException_ForInvalidUrl(string invalidUrl, string paramName)
        {
            var ex = Assert.Throws<ArgumentException>(() => Guard.Against.InvalidUrl(invalidUrl, paramName));
            Assert.Contains("is not a valid URL", ex.Message);
            Assert.Contains(paramName, ex.Message);
        }

        [Fact]
        public void InvalidUrl_ShouldNotThrow_ForNullInput() // IU-004
        {
            string? nullUrl = null;
            Guard.Against.InvalidUrl(nullUrl);
        }

        [Fact]
        public void InvalidUrl_ShouldNotThrow_ForEmptyInput() // IU-005
        {
            var emptyUrl = "";
            Guard.Against.InvalidUrl(emptyUrl);
        }
    }
}