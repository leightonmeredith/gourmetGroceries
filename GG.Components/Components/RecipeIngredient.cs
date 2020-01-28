using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.Components
{
    public class RecipeIngredient
    {   
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public string Count { get; set; }
        public string Measurement { get; set; }
        public string Description { get; set; }
        public virtual Ingredient Ingredient { get; set; }

    }
}
