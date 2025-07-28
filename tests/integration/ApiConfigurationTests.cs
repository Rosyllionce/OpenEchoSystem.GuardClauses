using System;
using OpenEchoSystem.GuardClauses;
using Xunit;

namespace OpenEchoSystem.GuardClauses.IntegrationTests
{
    // Mock ApiConfiguration class for testing purposes
    public class ApiConfiguration
    {
        public string ApiKey { get; }
        public string ApiUrl { get; }
        public int Timeout { get; }

        public ApiConfiguration(string apiKey, string apiUrl, int timeout)
        {
            Guard.Against.NullOrWhiteSpace(apiKey, nameof(apiKey));
            Guard.Against.InvalidUrl(apiUrl, nameof(apiUrl));
            Guard.Against.OutOfRange(timeout, 5, 120, nameof(timeout));

            ApiKey = apiKey;
            ApiUrl = apiUrl;
            Timeout = timeout;
        }
    }

    public class ApiConfigurationTests
    {
        [Fact]
        public void ValidateApiConfigurationWithValidInputShouldNotThrow()
        {
            // Arrange
            var validApiKey = "my-secret-key";
            var validApiUrl = "https://api.example.com";
            var validTimeout = 30;

            // Act & Assert
            var exception = Record.Exception(() => new ApiConfiguration(validApiKey, validApiUrl, validTimeout));
            Assert.Null(exception);
        }

        [Fact]
        public void ValidateApiConfigurationWithInvalidApiKeyNullShouldThrow()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ApiConfiguration(null!, "https://api.example.com", 30));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void ValidateApiConfigurationWithInvalidApiKeyWhitespaceShouldThrow(string apiKey)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new ApiConfiguration(apiKey, "https://api.example.com", 30));
        }

        [Fact]
        public void ValidateApiConfigurationWithInvalidApiUrlShouldThrow()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new ApiConfiguration("my-secret-key", "not-a-valid-url", 30));
        }

        [Theory]
        [InlineData(4)]
        [InlineData(121)]
        public void ValidateApiConfigurationWithInvalidTimeoutShouldThrow(int timeout)
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new ApiConfiguration("my-secret-key", "https://api.example.com", timeout));
        }
    }
}