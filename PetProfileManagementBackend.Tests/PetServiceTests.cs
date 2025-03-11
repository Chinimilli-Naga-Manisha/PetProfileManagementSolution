using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using PetProfileManagementBackend.Services;
using PetProfileManagementBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetProfileManagementBackend.Tests
{
    public class PetServiceTests
    {
        private readonly Mock<IPetService> _petServiceMock;
        private readonly List<Pet> _mockPets;

        public PetServiceTests()
        {
            _petServiceMock = new Mock<IPetService>();

            _mockPets = new List<Pet>
        {
            new Pet { Id = 1, Name = "Buddy", Species = "Dog", OwnerName = "John Doe" },
            new Pet { Id = 2, Name = "Kitty", Species = "Cat", OwnerName = "Jane Doe" }
        };

            // Mocking synchronous versions of service methods
            _petServiceMock.Setup(x => x.GetAllPets()).Returns(() => _mockPets);
            _petServiceMock.Setup(x => x.GetPetById(It.IsAny<int>())).Returns((int id) => _mockPets.Find(p => p.Id == id));
        }

        [Fact]
        public void GetAllPets_ShouldReturnListOfPets()
        {
            var pets = _petServiceMock.Object.GetAllPets();
            Assert.NotNull(pets);
            Assert.Equal(2, pets.Count);
        }

        [Fact]
        public void GetPetById_ShouldReturnPet_WhenPetExists()
        {
            var pet = _petServiceMock.Object.GetPetById(1);
            Assert.NotNull(pet);
            Assert.Equal("Buddy", pet.Name);
        }

        [Fact]
        public void GetPetById_ShouldReturnNull_WhenPetDoesNotExist()
        {
            var pet = _petServiceMock.Object.GetPetById(99);
            Assert.Null(pet);
        }
    }
}
