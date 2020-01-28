using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GG.Web.Models
{
    public class RecipeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ICollection<RecipeIngredientModel> Ingredients { get; set; }
        public ICollection<DirectionModel> Directions { get; set; }
    }
}