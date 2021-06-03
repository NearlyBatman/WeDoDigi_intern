using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;

namespace WeDoDigi_intern.Models
{
    public class RecipeDb
    {
 
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        [Required]
        public string Name { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Ingredients")]
        [Required]
        public string Ingredients { get; set; }

        [BsonElement("Steps")]
        [Required]
        public string Steps { get; set; }

        [BsonElement("ImageUrl")]
        [Display(Name = "Photo")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [BsonElement("Servings")]
        [Required]
        public string Servings { get; set; }

        [BsonElement("PrepTime")]
        [Required]
        public string PrepTime { get; set; }

        [BsonElement("CookTime")]
        [Required]
        public string CookTime { get; set; }

        [BsonElement("Tags")]
        [Required]
        public List<string> Tags = new List<string>();

        [BsonElement("Source")]
        public string Source { get; set; }

        public RecipeDb(string name, string description, string ingredients, string steps)
        {
            this.Name = name;
            this.Description = description;
            this.Ingredients = ingredients;
            this.Steps = steps;
        }
    }
}
