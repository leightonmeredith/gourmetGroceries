using GG.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GG.Web.Models
{
    public partial class ModelFactory
    {
        internal static RecipeIngredientModel Create(RecipeIngredient obj)
        {
            if (obj == null)
                return null;

            return new RecipeIngredientModel()
            {
                Id = obj.Id,
                Count = obj.Count,
                Description = obj.Description,
                Ingredient = Create(obj.Ingredient),
                Measurement = obj.Measurement,
                RecipeId = obj.RecipeId,
                IngredientId = obj.Ingredient == null ? obj.IngredientId : obj.Ingredient.Id
            };
        }

        internal static ICollection<RecipeIngredientModel> Create(IEnumerable<RecipeIngredient> objs)
        {
            if (objs == null)
                return null;

            var models = new List<RecipeIngredientModel>();

            foreach (var obj in objs)
            {
                var model = Create(obj);
                models.Add(model);
            }

            return models;
        }
    }
}