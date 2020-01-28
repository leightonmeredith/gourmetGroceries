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
    public interface IIngredientCategoryService
    {
        IQueryable<IngredientCategory> GetAll();
        IngredientCategory GetById(int id);
        void Update(IngredientCategory ingredientCategory);
        void Add(IngredientCategory ingredientCategory);
        void RemoveById(int id);
    }
    public class IngredientCategoryService : IIngredientCategoryService
    {
        private readonly GGDataContext _db;

        public IngredientCategoryService(GGDataContext db)
        {
            _db = db;
        }

        public IQueryable<IngredientCategory> GetAll()
        {
            return _db.IngredientCategories;
        }

        public IngredientCategory GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public void Update(IngredientCategory ingredientCategory)
        {
            _db.Entry(ingredientCategory).State = EntityState.Modified;
        }
        public void Add(IngredientCategory ingredientCategory)
        {
            _db.IngredientCategories.Add(ingredientCategory);
        }
        public void RemoveById(int id)
        {
            var ingredientCategory = GetById(id);
            _db.IngredientCategories.Remove(ingredientCategory);
        }
    }
}
