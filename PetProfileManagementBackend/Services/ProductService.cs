using PetProfileManagementBackend.Models;
using System.Collections.Generic;
using System.Linq;

namespace PetProfileManagementBackend.Services
{
    public class ProductService
    {
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Pet Collar", Description = "Comfortable collar", Price = 10.5M, Stock = 100 },
            new Product { Id = 2, Name = "Pet Bed", Description = "Soft and cozy bed", Price = 25.99M, Stock = 50 },
            new Product { Id = 3, Name = "Chew Toy", Description = "Durable rubber chew toy for dogs", Price = 5.99M, Stock = 200 },
            new Product { Id = 4, Name = "Pet Leash", Description = "Adjustable nylon leash", Price = 8.49M, Stock = 150 },
            new Product { Id = 5, Name = "Cat Scratching Post", Description = "Sturdy post with sisal rope", Price = 19.99M, Stock = 80 },
            new Product { Id = 6, Name = "Dog Food Bowl", Description = "Stainless steel food bowl", Price = 7.99M, Stock = 120 },
        };

        public List<Product> GetAll() => _products;

        public Product GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public void Add(Product product)
        {
            product.Id = _products.Max(p => p.Id) + 1;
            _products.Add(product);
        }

        public void Update(Product product)
        {
            var existingProduct = GetById(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
            }
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }
    }
}
