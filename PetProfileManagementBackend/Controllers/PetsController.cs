using Microsoft.AspNetCore.Mvc;
using PetProfileManagementBackend.Models;
using PetProfileManagementBackend.Services;

namespace PetProfileManagementBackend.Controllers
{
    [Route("api/pets")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        // GET: api/pets
        [HttpGet]
        public IActionResult GetPets()
        {
            var pets = _petService.GetAllPets();
            return Ok(pets);
        }

        // GET: api/pets/{id}
        [HttpGet("{id}")]
        public IActionResult GetPetById(int id)
        {
            var pet = _petService.GetPetById(id);
            if (pet == null)
            {
                return NotFound();
            }
            return Ok(pet);
        }

        // POST: api/pets
        [HttpPost]
        public IActionResult CreatePet([FromBody] Pet pet)
        {
            if (pet == null)
            {
                return BadRequest("Invalid pet data.");
            }

            // If necessary, add any validation for the fields in the pet object here.
            if (string.IsNullOrEmpty(pet.Name) || string.IsNullOrEmpty(pet.OwnerName))
            {
                return BadRequest("Pet name and owner name are required.");
            }

            _petService.AddPet(pet);
            return CreatedAtAction(nameof(GetPetById), new { id = pet.Id }, pet);
        }

        // PUT: api/pets/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatePet(int id, [FromBody] Pet pet)
        {
            if (pet == null || pet.Id != id)
            {
                return BadRequest("Pet ID mismatch.");
            }

            // If necessary, add validation to ensure that the necessary fields are provided
            if (string.IsNullOrEmpty(pet.Name) || string.IsNullOrEmpty(pet.OwnerName))
            {
                return BadRequest("Pet name and owner name are required.");
            }

            _petService.UpdatePet(pet);
            return NoContent(); // No content is returned after successful update
        }

        // DELETE: api/pets/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePet(int id)
        {
            var pet = _petService.GetPetById(id);
            if (pet == null)
            {
                return NotFound(); // Return 404 if the pet doesn't exist
            }

            _petService.DeletePet(id);
            return NoContent(); // Return 204 status after successful deletion
        }
    }
}

