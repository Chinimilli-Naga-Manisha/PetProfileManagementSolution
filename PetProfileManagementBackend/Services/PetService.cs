using PetProfileManagementBackend.Data;
using PetProfileManagementBackend.Models;

namespace PetProfileManagementBackend.Services
{
    public class PetService : IPetService
    {
        private readonly PetDbContext _context;

        public PetService(PetDbContext context)
        {
            _context = context; // Dependency injection to access the DbContext
        }

        // Adds a new pet to the database
        public void AddPet(Pet pet)
        {
            _context.Pet.Add(pet); // Add the pet to the DbContext
            _context.SaveChanges();  // Save changes to the database
        }

        // Retrieves all pets from the database
        public List<Pet> GetAllPets()
        {
            return _context.Pet.ToList(); // Get all pets
        }

        // Retrieves a pet by its ID
        public Pet GetPetById(int id)
        {
            return _context.Pet.FirstOrDefault(p => p.Id == id); // Get a pet by its ID
        }

        // Updates an existing pet's details
        public void UpdatePet(Pet pet)
        {
            var existingPet = _context.Pet.FirstOrDefault(p => p.Id == pet.Id); // Find the existing pet by ID
            if (existingPet != null)
            {
                existingPet.Name = pet.Name;               // Update Name
                existingPet.OwnerName = pet.OwnerName;     // Update OwnerName
                existingPet.Species = pet.Species;         // Update Species
                existingPet.Breed = pet.Breed;             // Update Breed
                existingPet.Age = pet.Age;                 // Update Age
                existingPet.Color = pet.Color;             // Update Color
                existingPet.DateOfBirth = pet.DateOfBirth; // Update DateOfBirth
                existingPet.MedicalHistory = pet.MedicalHistory; // Update MedicalHistory

                _context.Pet.Update(existingPet); // Update the pet in the DbContext
                _context.SaveChanges();            // Save changes to the database
            }
        }

        // Deletes a pet by its ID
        public void DeletePet(int id)
        {
            var pet = _context.Pet.FirstOrDefault(p => p.Id == id); // Find the pet by ID
            if (pet != null)
            {
                _context.Pet.Remove(pet);  // Remove the pet from the DbContext
                _context.SaveChanges();     // Save changes to the database
            }
        }
    }
}
