using GG.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GG.Web.Models
{
    public partial class ObjectFactory
    {
        internal static ICollection<RecipeIngredient> Parse(IEnumerable<RecipeIngredientModel> models, ICollection<RecipeIngredient> objs)
        {
            if (models == null)
                return null;

            if (objs == null)
            objs = new List<RecipeIngredient>();

            foreach (var model in models)
            {
                var obj = new RecipeIngredient();
                Parse(model, obj);
                objs.Add(obj);
            }
            return objs;
        }

        internal static RecipeIngredient Parse(RecipeIngredientModel model, RecipeIngredient obj)
        {
            if (model == null)
                return null;

            if (obj == null)
                return null;

            obj.Id = model.Id;
            obj.Count = model.Count;
            obj.Description = model.Description;
            obj.Measurement = model.Measurement;
            obj.RecipeId = model.RecipeId;
            obj.IngredientId = model.IngredientId;
            //Parse(model.Ingredient, obj.Ingredient);

            return obj;
        }
    }
}