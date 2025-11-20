using EcommerceBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EcommerceBackend.Services
{
    public class UserService
    {
        private readonly IMongoCollection<UserModel> _users;

        public UserService(IOptions<MongoDbSettings> settings, IMongoClient client)
        {
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _users = database.GetCollection<UserModel>("Users");
        }

        public async Task<List<UserModel>> GetAllAsync() =>
            await _users.Find(_ => true).ToListAsync();

        public async Task<UserModel?> GetByIdAsync(string id) =>
            await _users.Find(u => u.Id == id).FirstOrDefaultAsync();

        public async Task<UserModel> CreateAsync(UserModel user)
        {
            await _users.InsertOneAsync(user);
            return user;
        }

        public async Task UpdateAsync(string id, UserModel user) =>
            await _users.ReplaceOneAsync(u => u.Id == id, user);

        public async Task DeleteAsync(string id) =>
            await _users.DeleteOneAsync(u => u.Id == id);
    }
}
