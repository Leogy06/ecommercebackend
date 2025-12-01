using EcommerceBackend.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace EcommerceBackend.Services
{
    public class MenuItemService
    {
        private readonly IMongoCollection<MenuItemModel> _menuItems;

        public MenuItemService(IOptions<MongoDbSettings> settings, IMongoClient client)
        {
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _menuItems = database.GetCollection<MenuItemModel>("MenuItem");
        }

        public async Task<List<MenuItemModel>> CreateAsync(MenuItemModel[] menuItems)
        {
            await _menuItems.InsertManyAsync(menuItems);
            return menuItems.ToList(); 
        }
        public async Task<List<MenuItemModel>> GetAllAsync(string? categoryId = null)
        
            {
            if(string.IsNullOrEmpty(categoryId))
                {
                return await _menuItems.Find(_ => true).ToListAsync();
            }

            return await _menuItems.Find(i => i.CategoryId == categoryId).ToListAsync();
        }
        
    }
}
