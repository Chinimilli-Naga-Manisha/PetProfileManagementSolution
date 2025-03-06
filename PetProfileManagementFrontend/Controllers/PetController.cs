using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetProfileManagementFrontend.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class PetController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly string apiUrl = "http://localhost:5000/api/pets"; // Backend API URL

    public PetController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // GET: /Pet
    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetStringAsync(apiUrl);
        var pets = JsonConvert.DeserializeObject<List<Pet>>(response);
        return View(pets);
    }

    // GET: /Pet/Create
    public IActionResult Create()
    {
        var pet = new Pet();
        return View(pet);
    }

    // POST: /Pet/Create
    [HttpPost]
    [ValidateAntiForgeryToken] // Add anti-forgery token for security
    public async Task<IActionResult> Create(Pet pet)
    {
        if (ModelState.IsValid)
        {
            var response = await _httpClient.PostAsJsonAsync(apiUrl, pet);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Error while saving the pet.");
        }
        return View(pet);
    }

    // GET: /Pet/Edit/{id}
    public async Task<IActionResult> Edit(int id)
    {
        var response = await _httpClient.GetFromJsonAsync<Pet>($"{apiUrl}/{id}");
        if (response == null)
        {
            return NotFound();
        }
        return View(response);
    }

    // POST: /Pet/Edit/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Pet pet)
    {
        if (id != pet.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var response = await _httpClient.PutAsJsonAsync($"{apiUrl}/{id}", pet); // Use PUT for update
            if (response.IsSuccessStatusCode)
            {
                // After updating, get the latest pets from the backend
                var updatedPetsResponse = await _httpClient.GetStringAsync(apiUrl);
                var updatedPets = JsonConvert.DeserializeObject<List<Pet>>(updatedPetsResponse);
                return View("Index", updatedPets); // Return to Index with updated list
            }
            ModelState.AddModelError("", "Error while saving the pet.");
        }
        return View(pet);
    }

    // Directly DELETE: /Pet/Delete/{id}
    [HttpPost, ActionName("Delete")] // Handle POST request with ActionName "Delete"
    [ValidateAntiForgeryToken] // Add anti-forgery token for security
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _httpClient.DeleteAsync($"{apiUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index"); // Redirect to the Index after deletion
        }
        return View("Error"); // Handle any errors (you can define an error view if needed)
    }
}
