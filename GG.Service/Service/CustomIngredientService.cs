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
    public interface ICustomIngredientService
    {
        CustomIngredient GetCustomIngredientById(int id);
        IQueryable<CustomIngredient> GetAllCustomIngredients();
        void Update(CustomIngredient customIngredient);
        void AddList(IEnumerable<CustomIngredient> customIngredients);
        void Add(CustomIngredient customIngredient);
        void RemoveCustomIngredients(IEnumerable<CustomIngredient> customIngredients);
        void RemoveCustomIngredientById(int id);
        IQueryable<CustomIngredient> GetCustomIngredientByIds(IEnumerable<int> ids);
        IQueryable<CustomIngredient> GetCustomIngredients(IEnumerable<CustomIngredient> customIngredients);
    }
    public class CustomIngredientService : ICustomIngredientService
    {
        private GGDataContext _db;

        public CustomIngredientService(GGDataContext db)
        {
            _db = db;
        }

        public CustomIngredient GetCustomIngredientById(int id)
        {
            return GetAllCustomIngredients().FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<CustomIngredient> GetAllCustomIngredients()
        {
            return _db.CustomIngredients
                .Include(x => x.Recipe)
                .Include(x => x.Ingredient.IngredientCategory)
                .AsQueryable();
        }

        public void Update(CustomIngredient customIngredient)
        {
            _db.Entry(customIngredient).State = EntityState.Modified;
            _db.SaveChanges();
        }
        public void AddList(IEnumerable<CustomIngredient> customIngredients)
        {
            foreach (var customIngredient in customIngredients)
            {
                Add(customIngredient);
            }
        }

        public void Add(CustomIngredient customIngredient)
        {
            _db.CustomIngredients.Add(customIngredient);
        }

        public void RemoveCustomIngredientById(int id)
        {
            var customIngredient = GetCustomIngredientById(id);
            _db.CustomIngredients.Remove(customIngredient);
        }

        public void RemoveCustomIngredients(IEnumerable<CustomIngredient> customIngredients)
        {
            foreach (var item in customIngredients)
                RemoveCustomIngredientById(item.Id);
        }

        public IQueryable<CustomIngredient> GetCustomIngredientByIds(IEnumerable<int> ids)
        {
            return GetAllCustomIngredients().Where(x => ids.Contains(x.Id));
        }

        public IQueryable<CustomIngredient> GetCustomIngredients(IEnumerable<CustomIngredient> customIngredients)
        {
            var customIngredientIds = customIngredients.Select(x => x.Id);
            return GetCustomIngredientByIds(customIngredientIds);
        }
    }
}
