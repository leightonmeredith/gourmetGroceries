using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GG.Components;
using System.Data.Entity;

namespace GG.Data
{
    public interface IGGDataContext
    {

    }

    public class GGDataContext : DbContext

    {
        //Enable-Migrations -ProjectName GG.Data -StartUpProjectName GG.Web -Verbose
        //Add-Migration -ProjectName GG.Data -StartUpProjectName GG.Web -Verbose
        //Update-Database -ProjectName GG.Data -StartUpProjectName GG.Web -Verbose

        public GGDataContext() : base("SqlServerDatabase") { }
        public IDbSet<Menu> Menus { get; set; }
        public IDbSet<Recipe> Recipes { get; set; }
        public IDbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public IDbSet<CustomIngredient> CustomIngredients { get; set; }
        public IDbSet<Ingredient> Ingredients { get; set; }
        public IDbSet<Direction> Directions { get; set; }
        public IDbSet<IngredientCategory> IngredientCategories { get; set; }

    }
}
