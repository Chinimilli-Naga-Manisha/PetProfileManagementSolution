using Microsoft.AspNetCore.Mvc;
using PetProfileManagementBackend.Data;
using PetProfileManagementBackend.Models;

namespace PetProfileManagementBackend.Controllers
{
    public class PetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PetController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPet([FromBody] Pets pet)
        {
            if (pet == null || string.IsNullOrEmpty(pet.Name))
                return BadRequest("Invalid pet data");

            pet.QRCodeUrl = QrCodeService.GenerateQRCode(pet.PetId.ToString());

            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Pet added successfully", pet });
        }

        [HttpGet("get/{id}")]
        public IActionResult GetPet(int id)
        {
            var pet = _context.Pets.FirstOrDefault(p => p.PetId == id);
            if (pet == null)
                return NotFound("Pet not found");

            return Ok(pet);
        }
    }
}
