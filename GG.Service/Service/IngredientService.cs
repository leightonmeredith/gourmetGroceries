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
    public interface IIngredientService
    {
        IQueryable<Ingredient> GetAll();
        Ingredient GetIngredientById(int id);
        void Update(Ingredient ingredient);
        void Add(Ingredient ingredient);
        void SaveIngredientsToRecipe(IEnumerable<Ingredient> ingredients);
        void RemoveById(int id);
        IQueryable<Ingredient> GetIngredientByIds(IEnumerable<int> ingredients);
        IQueryable<Ingredient> GetIngredients(IEnumerable<Ingredient> ingredients);
    }
    public class IngredientService : IIngredientService
    {
        private readonly GGDataContext _db;

        public IngredientService(GGDataContext db)
        {
            _db = db;
        }

        public IQueryable<Ingredient> GetAll()
        {
            return _db.Ingredients
                .Include(x => x.IngredientCategory)
                .AsQueryable();
        }

        public Ingredient GetIngredientById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public void Update(Ingredient ingredient)
        {
            _db.Entry(ingredient).State = EntityState.Modified;
        }

        public void Add(Ingredient ingredient)
        {
            ingredient.IngredientCategoryId = ingredient.IngredientCategory == null ? ingredient.IngredientCategoryId : ingredient.IngredientCategory.Id;
            ingredient.IngredientCategory = null;
            _db.Ingredients.Add(ingredient);
        }

        public void SaveIngredientsToRecipe(IEnumerable<Ingredient> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                if (ingredient.Id == 0)
                    Add(ingredient);
                else
                    Update(ingredient);
            }
        }

        public void RemoveById(int id)
        {
            var ingredient = GetIngredientById(id);
            _db.Ingredients.Remove(ingredient);
        }

        public IQueryable<Ingredient> GetIngredientByIds(IEnumerable<int> ids)
        {
            return GetAll().Where(x => ids.Contains(x.Id));
        }

        public IQueryable<Ingredient> GetIngredients(IEnumerable<Ingredient> ingredients)
        {
            var ingredientIds = ingredients.Select(x => x.Id);
            return GetIngredientByIds(ingredientIds);
        }
    }
}
