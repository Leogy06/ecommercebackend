using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
namespace EcommerceBackend.Models
{
    public class OrdersModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "";

        [Required(ErrorMessage ="User id is required.")]
        [BsonElement("userId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = "";

        [Required]
        [MinLength(1, ErrorMessage ="Order must contain at least 1 item.")]
        [BsonElement("items")]
        public List<OrderedItems> Items { get; set; } = new List<OrderedItems>();

        [Range(0, double.MaxValue, ErrorMessage = "Subtotal must be positve.")]
        public decimal Subtotal { get; set; } = decimal.Zero;

    }

    public class OrderedItems
    {
        [Required]
        [StringLength(50)]
        [BsonElement("label")]
        public string Label { get; set; } = "";

        [Required]
        [StringLength(50)]
        [BsonElement("choice")]
        public string Choice { get; set; } = "";


    }
}
