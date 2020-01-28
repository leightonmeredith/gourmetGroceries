using GG.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GG.Web.Models
{
    public partial class ObjectFactory
    {
        public static Recipe Parse(RecipeModel model, Recipe obj)
        {
            if (model == null)
                return null;

            if (obj == null)
                return null;

            obj.Id = model.Id;
            obj.Image = model.Image;
            Parse(model.Ingredients, obj.Ingredients);
            obj.Name = model.Name;
            obj.Description = model.Description;
            obj.Ingredients?.ToList().ForEach(x => x.RecipeId = obj.Id);

            return obj;
        }
    }
}