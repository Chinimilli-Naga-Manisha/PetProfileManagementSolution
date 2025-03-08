using System.ComponentModel.DataAnnotations;

namespace PetProfileManagementBackend.Models
{
    public class Pets
    {
        [Key]
        public int PetId { get; set; }

        [Required]
        public string Name { get; set; }

        public string OwnerName { get; set; }

        [Required]
        public string QRCodeUrl { get; set; }
    }
}
