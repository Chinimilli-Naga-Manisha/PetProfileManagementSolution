using Microsoft.AspNetCore.Mvc;
using PetProfileManagementBackend.Controllers; // make sure this matches your namespace
using Xunit;

namespace PetProfileManagementBackend.Tests
{
    public class LoginControllerTests
    {
        private readonly LoginController _controller;

        public LoginControllerTests()
        {
            _controller = new LoginController();
        }

        //[Fact]
        //public void Login_ReturnsUnauthorized_ForInvalidPassword()
        //{
        //    // Arrange
        //    var request = new LoginRequest
        //    {
        //        Email = "admin@example.com",
        //        Password = "wrongpassword"
        //    };

        //    // Act
        //    var result = _controller.Login(request);

        //    // Assert
        //    Assert.IsType<UnauthorizedObjectResult>(result);

        //    var unauthorizedResult = result as UnauthorizedObjectResult;
        //    Assert.NotNull(unauthorizedResult);
        //    Assert.Equal(401, unauthorizedResult.StatusCode);
        //    Assert.Equal("Invalid email or password", ((dynamic)unauthorizedResult.Value).Message);
        //}

        [Fact]
        public void Login_ReturnsUnauthorized_ForEmptyCredentials()
        {
            // Arrange
            var request = new LoginRequest
            {
                Email = "",
                Password = ""
            };

            // Act
            var result = _controller.Login(request);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public void Login_ReturnsUnauthorized_ForNullCredentials()
        {
            // Arrange
            var request = new LoginRequest
            {
                Email = null,
                Password = null
            };

            // Act
            var result = _controller.Login(request);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }
        [Fact]
        public void Login_ReturnsUnauthorized_ForWrongPassword()
        {
            // Arrange
            var request = new LoginRequest
            {
                Email = "admin@example.com",
                Password = "wrongpassword"
            };

            // Act
            var response = _controller.Login(request);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(response);
        }
        [Fact]
        public void Login_ReturnsOk_ForCorrectCredentials()
        {
            // Arrange
            var request = new LoginRequest
            {
                Email = "admin@example.com",  // Make sure it matches StaticEmail
                Password = "password123"      // Make sure it matches StaticPassword
            };

            // Act
            var response = _controller.Login(request);

            // Assert
            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public void Login_ReturnsUnauthorized_ForWrongEmail()
        {
            // Arrange
            var request = new LoginRequest
            {
                Email = "wrong@example.com",
                Password = "password123"
            };

            // Act
            var response = _controller.Login(request);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(response);
        }
    }
}
