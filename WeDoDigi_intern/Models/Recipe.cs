using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeDoDigi_intern.Models
{
    public class Recipe
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Ingredient> Ingredients = new List<Ingredient>();
        public List<string> Steps = new List<string>();

    }
}
