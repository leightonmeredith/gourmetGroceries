using GG.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GG.Web.Models
{
    public partial class ObjectFactory
    {
        public static ICollection<Ingredient> Parse(IEnumerable<IngredientModel> models, ICollection<Ingredient> objs)
        {
            if (models == null)
                return null;

            //if (objs == null)
            objs = new List<Ingredient>();

            foreach (var model in models)
            {
                var obj = new Ingredient();
                Parse(model, obj);
                objs.Add(obj);
            }
            return objs;
        }

        public static Ingredient Parse(IngredientModel model, Ingredient obj)
        {
            if (model == null)
                return null;

            if (obj == null)
                return null;

            obj.Id = model.Id;
            obj.Brand = model.Brand;
            obj.Name = model.Name;
            //obj.Description = model.Description;
            //obj.Count = model.Count;
            //obj.Measurement = model.Measurement;
            Parse(model.IngredientCategory, obj.IngredientCategory);
            obj.IngredientCategoryId = model.IngredientCategory == null ? model.IngredientCategoryId : model.IngredientCategory.Id;

            return obj;
        }
    }
}