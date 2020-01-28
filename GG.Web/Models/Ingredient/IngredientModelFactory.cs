using GG.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GG.Web.Models
{
    public partial class ModelFactory
    {

        public static ICollection<IngredientModel> Create(IEnumerable<Ingredient> objs)
        {
            if (objs == null)
                return null;

            var modelList = new List<IngredientModel>();

            foreach (var obj in objs)
            {
                var model = Create(obj);
                modelList.Add(model);
            }
            return modelList;
        }
        public static IngredientModel Create(Ingredient obj)
        {
            if (obj == null)
                return null;

            return new IngredientModel()
            {
                Id = obj.Id,
                Brand = obj.Brand,
                IngredientCategory = Create(obj.IngredientCategory),
                //Count = obj.Count,
                //Description = obj.Description,
                //Measurement = obj.Measurement,
                Name = obj.Name,
            };
        }
    }
}