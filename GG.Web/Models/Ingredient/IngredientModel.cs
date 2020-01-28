using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GG.Web.Models
{
    public class IngredientModel
    {
        public int Id { get; set; }
        public int IngredientCategoryId { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public IngredientCategoryModel IngredientCategory { get; set; }
    }
}