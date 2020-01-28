using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GG.Components;
using GG.Data;
using GG.Service;
using GG.Web.Models;

namespace GG.Web.Controllers
{
    public class RecipeIngredientsController : ApiController
    {
        private GGDataContext db = new GGDataContext();
        private readonly IRecipeIngredientService _recipeIngredientService;

        public RecipeIngredientsController()
        {
            _recipeIngredientService = new RecipeIngredientService(db);
        }

        // GET: api/RecipeIngredients
        public IHttpActionResult GetRecipeIngredients()
        {
            var recipes = _recipeIngredientService.GetAll();

            var recipeModels = ModelFactory.Create(recipes);
            return Ok(recipeModels);
        }

        // PUT: api/RecipeIngredients/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRecipeIngredient(int id, RecipeIngredientModel recipeIngredientModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != recipeIngredientModel.Id)
                return BadRequest();

            var recipeIngredient = _recipeIngredientService.GetRecipeIngredientById(id);
            ObjectFactory.Parse(recipeIngredientModel, recipeIngredient);

            _recipeIngredientService.Update(recipeIngredient);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeIngredientExists(id))
                    return NotFound();
                else
                    throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/RecipeIngredients
        [HttpPost]
        [Route("api/Recipe/{recipeId:int}/Ingredients")]
        [ResponseType(typeof(IEnumerable<RecipeIngredientModel>))]
        public IHttpActionResult PostRecipeIngredient(int recipeId, IEnumerable<RecipeIngredientModel> recipeIngredientModels)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var recipeIngredients = new List<RecipeIngredient>();
            recipeIngredients = ObjectFactory.Parse(recipeIngredientModels, recipeIngredients).ToList();

            _recipeIngredientService.AddList(recipeId, recipeIngredients);

            db.SaveChanges();

            var newRecipes = _recipeIngredientService.GetRecipeIngredients(recipeIngredients);
            var newRecipeIngredientModels = ModelFactory.Create(newRecipes);

            return Ok(newRecipeIngredientModels);
        }

        // DELETE: api/RecipeIngredients/5
        [ResponseType(typeof(RecipeIngredient))]
        public IHttpActionResult DeleteRecipeIngredient(int id)
        {
            RecipeIngredient recipeIngredient = db.RecipeIngredients.Find(id);
            if (recipeIngredient == null)
                return NotFound();

            _recipeIngredientService.RemoveById(id);
            db.SaveChanges();

            return Ok(recipeIngredient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecipeIngredientExists(int id)
        {
            return db.RecipeIngredients.Count(e => e.Id == id) > 0;
        }
    }
}