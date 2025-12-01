using MongoDB.Driver;
using EcommerceBackend.Models;
using Microsoft.Extensions.Options;
using System.Security.Cryptography.X509Certificates;

namespace EcommerceBackend.Services
{
    public class CategoryService
    {
        private readonly IMongoCollection<CategoryModel> _categories;
        // create multiple category
        public CategoryService(IOptions<MongoDbSettings> settings, IMongoClient client)
        {
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _categories = database.GetCollection<CategoryModel>("Category");
        }

        public async Task<List<CategoryModel>> CreateAsync(CategoryModel[] categories)
        {
            await _categories.InsertManyAsync(categories);
            return categories.ToList();
        }
        public async Task<List<CategoryModel>> GetAllAsync() =>
                await _categories.Find(_ => true).ToListAsync();
        
       
    }
    }
