using GG.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GG.Web.Models
{
    public partial class ModelFactory
    {
        internal static IngredientCategoryModel Create(IngredientCategory obj)
        {
            if (obj == null)
                return null;
            return new IngredientCategoryModel()
            {
                Id = obj.Id,
                Name = obj.Name
            };
        }

        internal static ICollection<IngredientCategoryModel> Create (IEnumerable<IngredientCategory> objs)
        {
            if (objs == null)
                return null;

            var models = new List<IngredientCategoryModel>();

            foreach (var obj in objs)
            {
                var model = Create(obj);
                models.Add(model);
            }

            return models;
        }
    }
}