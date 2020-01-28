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
    public class IngredientsController : ApiController
    {
        private GGDataContext db = new GGDataContext();
        private readonly IIngredientService _ingredientService;

        public IngredientsController()
        {
            _ingredientService = new IngredientService(db);
        }

        // GET: api/Ingredients
        public IHttpActionResult GetIngredients()
        {
            var ingredients = _ingredientService.GetAll();

            var ingredientsModel = ModelFactory.Create(ingredients);
            return Ok(ingredientsModel);
        }

        // GET: api/Ingredients/5
        [ResponseType(typeof(IngredientModel))]
        public IHttpActionResult GetIngredient(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ingredient = _ingredientService.GetIngredientById(id);

            if (ingredient == null)
                return NotFound();

            var ingredientModel = ModelFactory.Create(ingredient);
            return Ok(ingredientModel);
        }

        // PUT: api/Ingredients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIngredient(int id, IngredientModel ingredientModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != ingredientModel.Id)
                return BadRequest();

            var ingredient = _ingredientService.GetIngredientById(id);
            ObjectFactory.Parse(ingredientModel, ingredient);

            _ingredientService.Update(ingredient);
            db.SaveChanges();

            return Ok();
            //return CreatedAtRoute("DefaultApi", new { id = ingredient.Id }, ingredient);
        }

        //[HttpPost]
        //[Route("api/Ingredients/SaveToMaster")]
        [ResponseType(typeof(IngredientModel))]
        public IHttpActionResult PostIngredientToMaster(IngredientModel ingredientModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ingredient = new Ingredient();
            ObjectFactory.Parse(ingredientModel, ingredient);

            _ingredientService.Add(ingredient);

            db.SaveChanges();

            var newIngredientModel = ModelFactory.Create(ingredient);
            return Ok(newIngredientModel);
        }

        [HttpPost]
        [Route("api/Ingredients/SaveToRecipe")]
        [ResponseType(typeof(IEnumerable<IngredientModel>))]
        public IHttpActionResult PostIngredientToRecipe(IEnumerable<IngredientModel> ingredientModels)
        {
            //Where is this coming from ?
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return null;
            //var ingredients = new List<Ingredient>();
            //ObjectFactory.Parse(ingredientModels, ingredients);

            //_ingredientService.SaveIngredientsToRecipe(ingredients);

            //db.SaveChanges();

            //var newIngredients = _ingredientService.GetIngredients(ingredients);
            //var newIngredientModels = ModelFactory.Create(newIngredients);
            //return Ok(newIngredientModels);
        }

        // DELETE: api/Ingredients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteIngredient(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _ingredientService.RemoveById(id);

            db.SaveChanges();

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

        private bool IngredientExists(int id)
        {
            return db.Ingredients.Count(e => e.Id == id) > 0;
        }
    }
}