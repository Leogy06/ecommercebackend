using EcommerceBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EcommerceBackend.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<OrdersModel> _orders;
        private readonly IMongoCollection<UserModel> _users;

        public OrderService (IOptions<MongoDbSettings> settings, IMongoClient client)
        {
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _orders = database.GetCollection<OrdersModel>("OrderModel");

            _users = database.GetCollection<UserModel>("UserModel");
        }

        //create 
        public async Task<OrdersModel> CreateAsync(OrdersModel dto)
        {
            var user = await _users.Find(u => u.Id == dto.UserId).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("User does not exist, cannot create order.");
            }

            await _orders.InsertOneAsync(dto);
            return dto;
        }

        //get all orders
        public async Task<List<OrdersModel>> GetAllAsync() => 
            await _orders.Find(_ => true).ToListAsync();
    }
}
