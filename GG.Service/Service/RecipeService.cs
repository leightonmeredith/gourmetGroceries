
using System;
using GG.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GG.Components;
using System.Data.Entity;

namespace GG.Services
{
    public interface IRecipeService
    {
        Recipe GetRecipeById(int recipeId);
        IQueryable<Recipe> GetAll();
        void Update(Recipe recipe);
        void Add(Recipe recipe);
        void RemoveById(int recipeId);
        IQueryable<Recipe> GetRecipesByName(string recipeName);
    }
    public class RecipeService : IRecipeService
    {
        private GGDataContext _db;

        public RecipeService(GGDataContext db)
        {
            _db = db;
        }


        //public List<Recipe> recipeRepo = RecipeRepo.CreateInstance();
        public Recipe GetRecipeById(int recipeId)
        {
            return GetAll().FirstOrDefault(x => x.Id == recipeId);

            //using (var db = new GGDataContext())
            //{
            //    return db.Recipes.FirstOrDefault(x => x.Id == recipeId);
            //}

        }

        public IQueryable<Recipe> GetAll()
        {
            return _db.Recipes
                .Include(x => x.Directions)
                .Include(x => x.Ingredients.Select(z => z.Ingredient.IngredientCategory))
                .AsQueryable();
        }

        public void Add(Recipe recipe)
        {
            _db.Recipes.Add(recipe);
        }

        public void Update(Recipe recipe)
        {
            _db.Entry(recipe).State = EntityState.Modified;
        }

        public void RemoveById(int id)
        {
            var recipe = GetRecipeById(id);
            _db.Recipes.Remove(recipe);
        }

        public IQueryable<Recipe> GetRecipesByName(string recipeName)
        {
            var primaryRecipe = GetAll().Where(x => x.Name == recipeName).ToList();

            var recipeNameArray = recipeName.Split(' ');
            var ingredientRecipes = new List<Recipe>();
            foreach (var recipeText in recipeNameArray)
            {
                var tempRecipe = GetAll().Where(x => x.Ingredients.Any(c => c.Ingredient.Name.Contains(recipeText))).ToList();
                ingredientRecipes.AddRange(tempRecipe);
            }
            var secondaryRecipe = ingredientRecipes.Except(primaryRecipe);
            primaryRecipe.AddRange(secondaryRecipe);

            var suggestedRecipes = new List<Recipe>();
            foreach (var recipeText in recipeNameArray)
            {
                var tempRecipe = GetAll().Where(x => x.Name.Contains(recipeText)).ToList();
                suggestedRecipes.AddRange(tempRecipe);
            }
            var tertiaryRecipe = suggestedRecipes.Except(primaryRecipe);
            primaryRecipe.AddRange(tertiaryRecipe);

            return primaryRecipe.AsQueryable();

            //return GetAll().Where(x => x.Ingredients.Any(c => nameArray.Contains(x.Name)));//.Where(x => nameArray.Contains(x.Name));
            //.Where(x=>nameArray.Contains(x.Ingredients.Where(c=> nameArray.Contains(c.Ingredient.Name)));
        }
    }
}
