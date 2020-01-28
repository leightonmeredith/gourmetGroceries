using GG.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GG.Web.Models
{
    public partial class ModelFactory
    {
        internal static CustomIngredientModel Create(CustomIngredient obj)
        {
            if (obj == null)
                return null;
            return new CustomIngredientModel()
            {
                Description = obj.Description,
                Id = obj.Id,
                Ingredient = Create(obj.Ingredient),
                IngredientId = obj.Ingredient == null ? obj.IngredientId : obj.Ingredient.Id,
                Recipe = Create(obj.Recipe),
                RecipeId = obj.RecipeId
            };
        }

        internal static ICollection<CustomIngredientModel>Create (IEnumerable<CustomIngredient> objs)
        {
            if (objs == null)
                return null;

            var models = new List<CustomIngredientModel>();

            foreach (var obj in objs)
            {
                var model = Create(obj);
                models.Add(model);
            }

            return models;
        }
    }
}