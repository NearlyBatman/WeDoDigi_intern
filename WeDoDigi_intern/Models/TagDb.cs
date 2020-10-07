using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeDoDigi_intern.Models
{
    public class TagDb
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("TagName")]
        [Required]
        public string tagName { get; set; }

        [BsonElement("IdList")]
        public List<string> idList = new List<string>();

    }
}
