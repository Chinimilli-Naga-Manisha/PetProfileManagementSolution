namespace PetProfileManagementFrontend.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string Species { get; set; }  // Added Species of the pet (e.g., Dog, Cat, etc.)
        public string Breed { get; set; }    // Added Breed of the pet (e.g., Labrador, Siamese, etc.)
        public int Age { get; set; }         // Added Age of the pet (in years)
        public string Color { get; set; }    // Added Color of the pet (e.g., Brown, White, etc.)
        public DateTime DateOfBirth { get; set; } // Added Date of birth for the pet
        public string MedicalHistory { get; set; } // Added Medical History for the pet (optional details about any medical conditions)
    }
}
