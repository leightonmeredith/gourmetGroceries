using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GG.Components
{
    public interface IRecipe
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Image { get; set; }
        //virtual ICollection<Ingredient> Ingredients { get; set; }
    }

    public class Recipe : IRecipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public int MenuId { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public virtual ICollection<RecipeIngredient> Ingredients { get; set; }

        /// <summary>
        /// Direction could split to 
        /// DIRECTION TABLE => string id/direction 
        /// and STEP TABLE => id/step/directionId
        /// </summary>
        public virtual ICollection<Direction> Directions { get; set; }

        public Recipe Clone()
        {
            return new Recipe()
            {
                Id = Id,
                Ingredients = Ingredients,
                Description = Description,
                Directions = Directions,
                Image = Image,
                Name = Name
            };
        }
    }

    public static class RecipeRepository
    {
        public static List<Recipe> CreateInstance()
        {
            return new List<Recipe>()
            {
                new Recipe()
                {
                    Id=0,
                    Image="IMAGE",
                    //Directions = new List<String>() {"Step 1","Step 2","Step 3" },
                    Ingredients = new List<RecipeIngredient>()
                    {
                       new RecipeIngredient()
                       {
                           Id=0,
                           Ingredient = new Ingredient()
                           {
                                Name ="Ing Name (Potato)",
                                Brand = "Ing Brand",
                                //Type = "Ing Name Type (red)"                                
                           },
                           Count = "4",
                           Description = "Ing Desc (chopped)",
                           Measurement = "lbs",
                       }
                    }
                }
            };
        }

    }
}