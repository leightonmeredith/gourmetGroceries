using GG.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GG.Web.Models
{
    public partial class ModelFactory
    {

        internal static DirectionModel Create(Direction obj)
        {
            return new DirectionModel()
            {
                Id = obj.Id,
                Instruction = obj.Instruction,
                RecipeId = obj.RecipeId,
                Step = obj.Step
            };
        }
        internal static ICollection<DirectionModel> Create(IEnumerable<Direction> directions)
        {
            if (directions == null)
                return null;

            var models = new List<DirectionModel>();

            foreach (var direction in directions)
            {
                var model = Create(direction);
                models.Add(model);
            }

            return models;
        }
    }
}