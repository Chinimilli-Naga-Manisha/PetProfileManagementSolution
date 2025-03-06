using PetProfileManagementBackend.Models;

namespace PetProfileManagementBackend.Services
{
    public interface IPetService
    {
        // Adds a pet to the database
        void AddPet(Pet pet);

        // Retrieves all pets from the database
        List<Pet> GetAllPets();

        // Retrieves a pet by its ID
        Pet GetPetById(int id);

        // Updates the pet details
        void UpdatePet(Pet pet);

        // Deletes a pet from the database by its ID
        void DeletePet(int id);
    }
}
