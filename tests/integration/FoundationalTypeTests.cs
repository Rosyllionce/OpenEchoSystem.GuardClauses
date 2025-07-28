using System;
using OpenEchoSystem.GuardClauses;
using Xunit;

namespace OpenEchoSystem.GuardClauses.IntegrationTests
{
    public class SystemSettings
    {
        public Guid TenantId { get; }
        public bool IsActive { get; }
        public Feature EnabledFeatures { get; }

        public SystemSettings(Guid tenantId, bool isActive, Feature enabledFeatures)
        {
            Guard.Against.Empty(tenantId, nameof(tenantId));
            Guard.Against.CustomCondition(!isActive, nameof(isActive), "System must be active.");
            Guard.Against.OutOfRange(enabledFeatures, nameof(enabledFeatures));

            TenantId = tenantId;
            IsActive = isActive;
            EnabledFeatures = enabledFeatures;
        }
    }

    [Flags]
    public enum Feature
    {
        None = 0,
        A = 1,
        B = 2,
        C = 4
    }

    public class FoundationalTypeTests
    {
        [Fact]
        public void SystemSettingsWithValidInputsShouldNotThrow()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var isActive = true;
            var features = Feature.A | Feature.C;

            // Act
            var exception = Record.Exception(() => new SystemSettings(tenantId, isActive, features));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void SystemSettingsWithEmptyGuidShouldThrowArgumentException()
        {
            // Arrange
            var tenantId = Guid.Empty;
            var isActive = true;
            var features = Feature.A;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new SystemSettings(tenantId, isActive, features));
        }

        [Fact]
        public void SystemSettingsWithIsActiveFalseShouldThrowArgumentException()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var isActive = false;
            var features = Feature.A;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new SystemSettings(tenantId, isActive, features));
        }

        [Fact]
        public void SystemSettingsWithUndefinedEnumShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var tenantId = Guid.NewGuid();
            var isActive = true;
            var features = (Feature)99; // Undefined value

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new SystemSettings(tenantId, isActive, features));
        }
    }
}