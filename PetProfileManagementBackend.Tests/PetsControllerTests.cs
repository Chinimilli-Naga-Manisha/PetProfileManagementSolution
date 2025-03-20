using Microsoft.AspNetCore.Mvc;
using Moq;
using PetProfileManagementBackend.Controllers;
using PetProfileManagementBackend.Models;
using PetProfileManagementBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProfileManagementBackend.Tests
{
    public class PetsControllerTests
    {
        private readonly Mock<IPetService> _petServiceMock;
        private readonly PetsController _petsController;
        private readonly List<Pet> _mockPets;

        public PetsControllerTests()
        {
            _petServiceMock = new Mock<IPetService>();
            _petsController = new PetsController(_petServiceMock.Object);

            _mockPets = new List<Pet>
        {
            new Pet { Id = 1, Name = "Buddy", Species = "Dog", OwnerName = "John Doe" },
            new Pet { Id = 2, Name = "Kitty", Species = "Cat", OwnerName = "Jane Doe" }
        };

            // Mock service methods without async/await
            _petServiceMock.Setup(x => x.GetAllPets()).Returns(() => _mockPets);
            _petServiceMock.Setup(x => x.GetPetById(It.IsAny<int>())).Returns((int id) => _mockPets.Find(p => p.Id == id));
        }

        //[Fact]
        //public void GetAllPets_ShouldReturnOk_WithListOfPets()
        //{
        //    var result = _petsController.GetAllPets() as OkObjectResult;

        //    Assert.NotNull(result);
        //    Assert.Equal(200, result.StatusCode);

        //    var pets = result.Value as List<Pet>;
        //    Assert.NotNull(pets);
        //    Assert.Equal(2, pets.Count);
        //}

        [Fact]
        public void GetPetById_ShouldReturnOk_WhenPetExists()
        {
            var result = _petsController.GetPetById(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var pet = result.Value as Pet;
            Assert.NotNull(pet);
            Assert.Equal("Buddy", pet.Name);
        }

        [Fact]
        public void GetPetById_ShouldReturnNotFound_WhenPetDoesNotExist()
        {
            var result = _petsController.GetPetById(99) as NotFoundResult;

            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }
        [Fact]
        public void CreatePet_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var newPet = new Pet(); // Missing required fields like Name, Species, etc.
            _petsController.ModelState.AddModelError("Name", "Required");

            // Act
            var result = _petsController.CreatePet(newPet) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }
    }
}