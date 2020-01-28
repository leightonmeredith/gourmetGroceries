using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GG.Components;

namespace GG.Web.Models
{
    public partial class ObjectFactory
    {
        public static ICollection<Direction> Parse(IEnumerable<DirectionModel> models, ICollection<Direction> objs)
        {
            if (models == null)
                return null;
            //if (objs == null)
                objs = new List<Direction>();

            foreach (var model in models)
            {
                var obj = new Direction();
                Parse(model, obj);
                objs.Add(obj);
            }
            return objs;
        }

        public static Direction Parse(DirectionModel model, Direction obj)
        {
            if (model == null)
                return null;

            if (obj == null)
                return null;

            obj.Id = model.Id;
            obj.Instruction = model.Instruction;
            obj.RecipeId = model.RecipeId;
            obj.Step = model.Step;

            return obj;
        }
    }
}