using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using EcommerceBackend.Enums;
using System.ComponentModel.DataAnnotations;


namespace EcommerceBackend.Models
{
    public class MenuItemModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; } // _id in MongoDB

        [BsonElement("name")]
        public string Name { get; set; } = "";

        public decimal Price { get; set; }

        [BsonElement("description")]
        public string Description { get; set; } = "";

        [BsonElement("category_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; } = string.Empty;

        [BsonElement("options")]
        public List<OptionEnum> Options { get; set; } = new List<OptionEnum>();

        [BsonElement("images")]
        public List<string> Images { get; set; } = new List<string>();

        [BsonElement("rating")]
        public double Rating { get; set; } = 0;

        [BsonElement("rating_count")]
        public int RatingCount { get; set; } = 0;

        [BsonElement("created_at")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
