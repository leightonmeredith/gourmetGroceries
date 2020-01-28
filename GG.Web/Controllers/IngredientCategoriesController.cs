using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GG.Components;
using GG.Data;
using GG.Service;
using GG.Web.Models;
using System.Threading.Tasks;

namespace GG.Web.Controllers
{
    public class IngredientCategoriesController : ApiController
    {
        private GGDataContext db = new GGDataContext();
        private readonly IIngredientCategoryService _ingredientCategoryService;

        public IngredientCategoriesController()
        {
            _ingredientCategoryService = new IngredientCategoryService(db);
        }

        // GET: api/IngredientCategories
        public IHttpActionResult GetIngredientCategories()
        {
            var ingredientCategories = _ingredientCategoryService.GetAll();

            var ingredientCategoryModels = ModelFactory.Create(ingredientCategories);
            return Ok(ingredientCategoryModels);
        }

        // GET: api/IngredientCategories/5
        [ResponseType(typeof(IngredientCategoryModel))]
        public IHttpActionResult GetGroceryCategory(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ingredientCategory = _ingredientCategoryService.GetById(id);

            if (ingredientCategory == null)
                return NotFound();

            var ingredientCategoryModel = ModelFactory.Create(ingredientCategory);
            return Ok(ingredientCategoryModel);
        }

        // PUT: api/IngredientCategories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGroceryCategory(int id, IngredientCategoryModel ingredientCategoryModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != ingredientCategoryModel.Id)
                return BadRequest();


            var ingredientCategory = _ingredientCategoryService.GetById(id);
            ObjectFactory.Parse(ingredientCategoryModel, ingredientCategory);

            _ingredientCategoryService.Update(ingredientCategory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroceryCategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/IngredientCategories
        [ResponseType(typeof(IngredientCategoryModel))]
        public IHttpActionResult PostGroceryCategory(IngredientCategoryModel ingredientCategoryModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ingredientCategory = new IngredientCategory();
            ObjectFactory.Parse(ingredientCategoryModel, ingredientCategory);

            _ingredientCategoryService.Add(ingredientCategory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ingredientCategory.Id }, ingredientCategory);
        }

        // DELETE: api/IngredientCategories/5
        [ResponseType(typeof(IngredientCategory))]
        public IHttpActionResult DeleteGroceryCategory(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _ingredientCategoryService.RemoveById(id);
            db.SaveChangesAsync();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GroceryCategoryExists(int id)
        {
            return db.IngredientCategories.Count(e => e.Id == id) > 0;
        }
    }
}