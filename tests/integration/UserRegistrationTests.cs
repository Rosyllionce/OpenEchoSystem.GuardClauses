using System;
using OpenEchoSystem.GuardClauses;
using Xunit;

namespace OpenEchoSystem.GuardClauses.IntegrationTests
{
    // Mock User class for testing purposes
    public class User
    {
        public string Username { get; }
        public string Email { get; }
        public string Password { get; }

        public User(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }
    }

    // Mock UserService class to demonstrate guard clause usage
    public class UserService
    {
        public static void Register(string username, string email, string password)
        {
            Guard.Against.NullOrWhiteSpace(username, nameof(username));
            Guard.Against.OutOfRange(username.Length, 3, 20, nameof(username));
            Guard.Against.InvalidFormat(username, @"^[a-zA-Z0-9_]+$", nameof(username));

            Guard.Against.NullOrWhiteSpace(email, nameof(email));
            Guard.Against.InvalidEmail(email, nameof(email));

            Guard.Against.NullOrEmpty(password, nameof(password));
            Guard.Against.InvalidFormat(password, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", nameof(password), "Password must be at least 8 characters long and contain at least one letter and one number.");

            // In a real application, you would proceed with user creation and persistence.
            var user = new User(username, email, password);
        }
    }

    public class UserRegistrationTests
    {
        private readonly UserService _userService = new UserService();

        [Fact]
        public void ValidateUserRegistrationWithValidInputShouldNotThrow()
        {
            // Arrange
            var username = "valid_user";
            var email = "test@example.com";
            var password = "Password123";

            // Act & Assert
            var exception = Record.Exception(() => UserService.Register(username, email, password));
            Assert.Null(exception);
        }

        [Fact]
        public void ValidateUserRegistrationWithInvalidUsernameNullShouldThrow()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => UserService.Register(null!, "test@example.com", "Password123"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidateUserRegistrationWithInvalidUsernameWhitespaceShouldThrow(string username)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => UserService.Register(username, "test@example.com", "Password123"));
        }

        [Theory]
        [InlineData("a")]
        [InlineData("this_username_is_far_too_long")]
        public void ValidateUserRegistrationWithInvalidUsernameLengthShouldThrow(string username)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => UserService.Register(username, "test@example.com", "Password123"));
        }

        [Fact]
        public void ValidateUserRegistrationWithInvalidUsernameFormatShouldThrow()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => UserService.Register("invalid-user!", "test@example.com", "Password123"));
        }

        [Fact]
        public void ValidateUserRegistrationWithInvalidEmailShouldThrow()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => UserService.Register("valid_user", "not-an-email", "Password123"));
        }

        [Fact]
        public void ValidateUserRegistrationWithInvalidPasswordFormatShouldThrow()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => UserService.Register("valid_user", "test@example.com", "weak"));
        }
    }
}