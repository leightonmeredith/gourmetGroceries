using GG.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GG.Web.Models
{
    public partial class ModelFactory
    {
        internal static RecipeModel Create(Recipe obj)
        {
            if (obj == null)
                return null;

            return new RecipeModel()
            {
                Id = obj.Id,
                Description = obj.Description,
                Directions = Create(obj.Directions),
                Image = obj.Image,
                Ingredients = Create(obj.Ingredients),
                Name = obj.Name
            };
        }

        internal static ICollection<RecipeModel> Create(IEnumerable<Recipe> recipes)
        {
            if (recipes == null)
                return null;

            var models = new List<RecipeModel>();

            foreach (var recipe in recipes)
            {
                var model = Create(recipe);
                models.Add(model);
            }

            return models;
        }
    }
}