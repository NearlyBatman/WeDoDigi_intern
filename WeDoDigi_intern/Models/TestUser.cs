using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeDoDigi_intern.Models
{
    public class TestUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("UserName")]
        [Required]
        public string UserName { get; set; }

        [BsonElement("Email")]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [BsonElement("Password")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [BsonElement("Recipes")]
        [Required]
        public List<RecipeDb> recipeDbs = new List<RecipeDb>();

        [BsonElement("Favorites")]
        [Required]
        public List<RecipeDb> favorites = new List<RecipeDb>();

        [BsonElement("Mealplan")]
        [Required]
        public List<FoodPlan> mealPlan = new List<FoodPlan>();

        [BsonElement("ShoppingList")]
        [Required]
        public List<string> shoppingList = new List<string>();
    }
}
