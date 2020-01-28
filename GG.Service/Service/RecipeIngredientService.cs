using GG.Components;
using GG.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GG.Service
{
    public interface IRecipeIngredientService
    {
        IQueryable<RecipeIngredient> GetAll();
        void Update(RecipeIngredient recipeIngredient);
        void AddList(int recipeId, IEnumerable<RecipeIngredient> recipeIngredient);
        RecipeIngredient GetRecipeIngredientById(int recipeIngredientId);
        IQueryable<RecipeIngredient> GetRecipeIngredientByIds(IEnumerable<int> ids);
        IQueryable<RecipeIngredient> GetRecipeIngredients(IEnumerable<RecipeIngredient> recipeIngredients);
        void RemoveById(int id);
    }
    public class RecipeIngredientService : IRecipeIngredientService
    {
        private GGDataContext _db;

        public RecipeIngredientService(GGDataContext db)
        {
            _db = db;
        }
        public IQueryable<RecipeIngredient> GetAll()
        {
            return _db.RecipeIngredients
                .Include(x => x.Ingredient.IngredientCategory)
                .AsQueryable();
        }
        public void Update(RecipeIngredient recipeIngredient)
        {
            _db.Entry(recipeIngredient).State = EntityState.Modified;
        }
        public void Add(RecipeIngredient recipeIngredient)
        {
            _db.RecipeIngredients.Add(recipeIngredient);
        }
        public void AddList(int recipeId, IEnumerable<RecipeIngredient> recipeIngredients)
        {
            var recipeIngredient = _db.RecipeIngredients.Where(x => x.RecipeId == recipeId);
            var deleteRecipeIngredientIds = recipeIngredient.Select(x => x.Id).Except(recipeIngredients.Select(x => x.Id)).ToList();

            deleteRecipeIngredientIds.ForEach(x => RemoveById(x));

            foreach (var ingredient in recipeIngredients)
            {
                //ingredient.IngredientId = ingredient.IngredientId;
                ingredient.Ingredient = null;
                ingredient.RecipeId = recipeId;

                if (ingredient.Id == 0)
                    Add(ingredient);
                else
                    Update(ingredient);
            }
        }

        public RecipeIngredient GetRecipeIngredientById(int recipeIngredientId)
        {
            return GetAll().FirstOrDefault(x => x.Id == recipeIngredientId);

            //using (var db = new GGDataContext())
            //{
            //    return db.Recipes.FirstOrDefault(x => x.Id == recipeId);
            //}

        }

        public void RemoveById(int id)
        {
            var recipeIngredient = GetRecipeIngredientById(id);
            _db.RecipeIngredients.Remove(recipeIngredient);
        }

        public IQueryable<RecipeIngredient> GetRecipeIngredientByIds(IEnumerable<int> ids)
        {
            return GetAll().Where(x => ids.Contains(x.Id));
        }

        public IQueryable<RecipeIngredient> GetRecipeIngredients(IEnumerable<RecipeIngredient> recipeIngredients)
        {
            var recipeIngredientIds = recipeIngredients.Select(x => x.Id);
            return GetRecipeIngredientByIds(recipeIngredientIds);
        }
    }
}
