using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.Components
{
    public interface ICustomIngredient
    {
        int Id { get; set; }
        string Description { get; set; }
        int? RecipeId { get; set; }
        int? IngredientId { get; set; }
        Recipe Recipe { get; set; }
        Ingredient Ingredient { get; set; }
    }
    public class CustomIngredient : ICustomIngredient
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? RecipeId { get; set; }
        public int? IngredientId { get; set; }
        public virtual Recipe Recipe { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }    
}
