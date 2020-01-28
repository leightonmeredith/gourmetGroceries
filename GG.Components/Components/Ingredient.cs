using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GG.Components
{
    public class Ingredient
    {
        public int Id { get; set; }
        public int IngredientCategoryId { get; set; }

        //HOW DO I GET RID OF THIS OBJECT? ITS A PRIMARY FOREIGN KEY
        public int? RecipeId { get; set; }

        public string Brand { get; set; }
        public string Name { get; set; }
        //public string Type { get; set; }
        public virtual IngredientCategory IngredientCategory { get; set; }
    }

    public static class IngredientRepo
    {
        public static List<Ingredient> CreateInstance()
        {
            return new List<Ingredient>()
            {
                new Ingredient()
                       {
                           Id=0,
                           Name="Ing Name (Potato)",
                           Brand = "Ing Brand",
                           //Count = 4,
                           //Description = "Ing Desc (chopped)",
                           //Measurement = "lbs",
                           //Type and description is now the same thing ...Type = "Ing Name Type (red)",
                       }
            };
        }
    }
}