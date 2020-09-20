using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string Description { get; set; }

        [BsonElement("Ingredients")]
        [Required]
        public string[] Ingredients { get; set; }

        [BsonElement("Steps")]
        [Required]
        public string[] Steps { get; set; }

        [BsonElement("ImageUrl")]
        [Display(Name="Photo")]
        [DataType(DataType.ImageUrl)]
        [Required]
        public string ImageUrl { get; set; }
        
    }
}
