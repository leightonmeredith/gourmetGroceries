using GG.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GG.Web.Models
{
    public partial class ObjectFactory
    {
        public static IngredientCategory Parse(IngredientCategoryModel model, IngredientCategory obj)
        {
            if (model == null)
                return null;

            if (obj == null)
                return null;

            obj.Id = model.Id;
            obj.Name = model.Name;

            return obj;
        }
    }
}