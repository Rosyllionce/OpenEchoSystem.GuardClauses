using OpenEchoSystem.Guard;
using System;
using Xunit;

namespace OpenEchoSystem.GuardClauses.IntegrationTests
{
    // This class simulates a piece of application logic that uses the Guard Clause library.
    // It serves as the "Unit Under Test" for the integration tests.
    public class ServiceExample
    {
        public void CreateUser(string name, int age, string email)
        {
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.OutOfRange(age, 0, 120, nameof(age));
            // The following guard clause is expected to be implemented in the core library.
            Guard.Against.InvalidEmail(email, nameof(email));
        }
    }

    public class ServiceExampleTests
    {
        private readonly ServiceExample _service;

        public ServiceExampleTests()
        {
            _service = new ServiceExample();
        }

        /// <summary>
        /// Test Case ID: INT-GC-001
        /// Verifies that the CreateUser method executes successfully with valid inputs.
        /// </summary>
        [Fact]
        public void CreateUser_WithValidInputs_DoesNotThrow()
        {
            _service.CreateUser("John Doe", 30, "john.doe@example.com");
        }

        /// <summary>
        /// Test Case ID: INT-GC-002
        /// Verifies that an ArgumentException is thrown for a null name.
        /// </summary>
        [Fact]
        public void CreateUser_WithNullName_ThrowsArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>("name", () => _service.CreateUser(null, 30, "john.doe@example.com"));
            Assert.Equal("name", ex.ParamName);
        }

        /// <summary>
        /// Test Case ID: INT-GC-003
        /// Verifies that an ArgumentOutOfRangeException is thrown for an out-of-range age.
        /// </summary>
        [Fact]
        public void CreateUser_WithOutOfRangeAge_ThrowsArgumentOutOfRangeException()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>("age", () => _service.CreateUser("Jane Doe", 150, "jane.doe@example.com"));
            Assert.Equal("age", ex.ParamName);
        }

        /// <summary>
        /// Test Case ID: INT-GC-004
        /// Verifies that an ArgumentException is thrown for an invalid email.
        /// This test is expected to fail initially, representing the "Red" state in TDD,
        /// as the Guard.Against.InvalidEmail method is not yet implemented.
        /// </summary>
        [Fact]
        public void CreateUser_WithInvalidEmail_ThrowsArgumentException()
        {
            // This test will remain skipped until the InvalidEmail guard is implemented.
            var ex = Assert.Throws<ArgumentException>("email", () => _service.CreateUser("Peter Pan", 25, "invalid-email"));
            Assert.Equal("email", ex.ParamName);
        }
    }
}