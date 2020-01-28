using GG.Components;
using GG.Data;
using GG.Services;
using GG.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace GG.Web.Controllers
{
    public class RecipesController : ApiController
    {
        private GGDataContext db = new GGDataContext();
        private readonly IRecipeService _recipeService;

        public RecipesController()
        {
            _recipeService = new RecipeService(db);
        }

        // GET: api/Recipes
        public IHttpActionResult GetRecipes()
        {
            var recipes = _recipeService.GetAll();

            var recipeModels = ModelFactory.Create(recipes);
            return Ok(recipeModels);
        }

        [HttpPost]
        [Route("api/Recipes/Search")]
        //[ResponseType(typeof(ICollection<RecipeModel>))]
        public IHttpActionResult RecipesByName(List<string> recipeName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //maybe seperate to have primary/secondary/tertiary recipe list? (See service)
            var recipes = _recipeService.GetRecipesByName(recipeName.FirstOrDefault());

            var recipeModels = ModelFactory.Create(recipes);
            return Ok(recipeModels);
        }

        // GET: api/Recipes/5
        [ResponseType(typeof(RecipeModel))]
        public IHttpActionResult GetRecipe(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var recipe = _recipeService.GetRecipeById(id);

            if (recipe == null)
                return NotFound();

            var recipeModel = ModelFactory.Create(recipe);
            return Ok(recipeModel);
        }

        // PUT: api/Recipes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRecipe(int id, RecipeModel recipeModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != recipeModel.Id)
                return StatusCode(HttpStatusCode.NotAcceptable);

            //var recipe = new Recipe();
            var recipe = _recipeService.GetRecipeById(id);
            ObjectFactory.Parse(recipeModel, recipe);

            _recipeService.Update(recipe);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                    return NotFound();
                else
                    throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = recipe.Id }, recipe);
        }

        // POST: api/Recipes
        [ResponseType(typeof(RecipeModel))]
        public IHttpActionResult PostRecipe(RecipeModel recipeModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var recipe = new Recipe();
            ObjectFactory.Parse(recipeModel, recipe);

            _recipeService.Add(recipe);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = recipe.Id }, recipe);
        }

        // DELETE: api/Recipes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteRecipe(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _recipeService.RemoveById(id);
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

        private bool RecipeExists(int id)
        {
            return db.Recipes.Count(e => e.Id == id) > 0;
        }
    }
}