using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using PetProfileManagementBackend.Services;
using PetProfileManagementBackend.Models;

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

            _petServiceMock.Setup(x => x.GetAllPets()).Returns(() => _mockPets);
            _petServiceMock.Setup(x => x.GetPetById(It.IsAny<int>())).Returns((int id) => _mockPets.Find(p => p.Id == id));
            _petServiceMock.Setup(x => x.AddPet(It.IsAny<Pet>())).Callback((Pet pet) => _mockPets.Add(pet));
            _petServiceMock.Setup(x => x.DeletePet(It.IsAny<int>())).Callback((int id) => _mockPets.RemoveAll(p => p.Id == id));
            _petServiceMock.Setup(x => x.UpdatePet(It.IsAny<Pet>())).Callback((Pet updatedPet) =>
            {
                var index = _mockPets.FindIndex(p => p.Id == updatedPet.Id);
                if (index != -1) _mockPets[index] = updatedPet;
            });
            //  _petServiceMock.Setup(x => x.GetPetsByOwner(It.IsAny<string>())).Returns((string owner) => _mockPets.Where(p => p.OwnerName == owner).ToList());
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

        [Fact]
        public void AddPet_ShouldIncreasePetCount()
        {
            var newPet = new Pet { Id = 3, Name = "Charlie", Species = "Rabbit", OwnerName = "Alice" };
            _petServiceMock.Object.AddPet(newPet);

            var pets = _petServiceMock.Object.GetAllPets();
            Assert.Equal(3, pets.Count);
            Assert.Contains(pets, p => p.Name == "Charlie");
        }

        [Fact]
        public void DeletePet_ShouldRemovePet_WhenPetExists()
        {
            _petServiceMock.Object.DeletePet(1);
            var pets = _petServiceMock.Object.GetAllPets();

            Assert.Single(pets);
            Assert.DoesNotContain(pets, p => p.Id == 1);
        }

        [Fact]
        public void DeletePet_ShouldDoNothing_WhenPetDoesNotExist()
        {
            _petServiceMock.Object.DeletePet(99);
            var pets = _petServiceMock.Object.GetAllPets();

            Assert.Equal(2, pets.Count);
        }

        [Fact]
        public void UpdatePet_ShouldModifyExistingPet()
        {
            var updatedPet = new Pet { Id = 1, Name = "Max", Species = "Dog", OwnerName = "John Doe" };
            _petServiceMock.Object.UpdatePet(updatedPet);

            var pet = _petServiceMock.Object.GetPetById(1);
            Assert.NotNull(pet);
            Assert.Equal("Max", pet.Name);
        }

        //[Fact]
        //public void GetPetsByOwner_ShouldReturnPets_WhenOwnerExists()
        //{
        //    var pets = _petServiceMock.Object.GetPetsByOwner("Jane Doe");
        //    Assert.Single(pets);
        //    Assert.Equal("Kitty", pets[0].Name);
        //}

        //[Fact]
        //public void GetPetsByOwner_ShouldReturnEmptyList_WhenOwnerHasNoPets()
        //{
        //    var pets = _petServiceMock.Object.GetPetsByOwner("Alice");
        //    Assert.Empty(pets);
        //}
    }
}