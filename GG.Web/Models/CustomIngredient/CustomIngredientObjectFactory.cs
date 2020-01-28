using GG.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GG.Web.Models
{
    public partial class ObjectFactory
    {
        internal static CustomIngredient Parse(CustomIngredientModel model, CustomIngredient obj)
        {
            if (model == null)
                return null;

            if (obj == null)
                return null;

            obj.Description = model.Description;
            obj.Id = model.Id;
            obj.IngredientId = model.IngredientId;
            obj.RecipeId = model.RecipeId;
            Parse(model.Recipe, obj.Recipe);
            Parse(model.Ingredient, obj.Ingredient);

            return obj;
        }

        internal static ICollection<CustomIngredient> Parse(IEnumerable<CustomIngredientModel> models, ICollection<CustomIngredient> objs)
        {
            if (models == null)
                return null;

            objs = new List<CustomIngredient>(); //CustomIngredientRepository.CreateInstanceList();

            foreach (var model in models)
            {
                var obj = new CustomIngredient();//CustomIngredientRepository.CreateInstance();
                Parse(model, obj);
                objs.Add(obj);
            }

            return objs;
        }
    }
}