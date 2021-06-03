using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeDoDigi_intern.Models
{
    public class FoodPlan
    {
        // Kig på database strukturen og prøv at lave en bruger med modeller som defineret liste klasser, bare for sjovs skyld
        // Ikke implementeret del til madplanen
        public RecipeDb fpDb { get; set; }
        public DateTime servingTime { get; set; }
    }
}
