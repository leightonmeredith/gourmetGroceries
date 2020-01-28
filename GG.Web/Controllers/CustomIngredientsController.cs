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
    public class CustomIngredientsController : ApiController
    {
        private GGDataContext db = new GGDataContext();
        private readonly ICustomIngredientService _customIngredientService;

        public CustomIngredientsController()
        {
            _customIngredientService = new CustomIngredientService(db);
        }

        // GET: api/CustomIngredients
        public IHttpActionResult GetCustomIngredients()
        {
            var customIngredients = _customIngredientService.GetAllCustomIngredients();

            var customIngredientsModel = ModelFactory.Create(customIngredients);
            return Ok(customIngredientsModel);
        }

        // GET: api/CustomIngredients/5
        [ResponseType(typeof(CustomIngredientModel))]
        public IHttpActionResult GetCustomIngredient(int id)
        {
            var customIngredient = _customIngredientService.GetCustomIngredientById(id);

            if (customIngredient == null)
                return NotFound();

            var customIngredientModel = ModelFactory.Create(customIngredient);
            return Ok(customIngredientModel);
        }

        // PUT: api/CustomIngredients/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCustomIngredient(int id, CustomIngredientModel customIngredientModels)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != customIngredientModels.Id)
                return StatusCode(HttpStatusCode.NotAcceptable);

            var customIngredient = _customIngredientService.GetCustomIngredientById(id);
            ObjectFactory.Parse(customIngredientModels, customIngredient);

            _customIngredientService.Update(customIngredient);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomIngredientExists(id))
                    return NotFound();
                else
                    throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CustomIngredients
        [ResponseType(typeof(CustomIngredientModel))]
        public IHttpActionResult PostCustomIngredient(CustomIngredientModel customIngredientModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customIngredient = new CustomIngredient(); //CustomIngredientRepository.CreateInstanceList(); 
            customIngredient = ObjectFactory.Parse(customIngredientModel, customIngredient);

            _customIngredientService.Add(customIngredient);
            db.SaveChanges();

            var newCustomIngredient = _customIngredientService.GetCustomIngredientById(customIngredient.Id);
            var newCustomIngredientsModels = ModelFactory.Create(newCustomIngredient);

            return CreatedAtRoute("DefaultApi", new { id = newCustomIngredient.Id }, newCustomIngredient);
        }
        
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteCustomIngredient(int id)
        {
            //It's the same as the customIngredients/DeleteRecipe but I want to keep them seperate for now
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _customIngredientService.RemoveCustomIngredientById(id);
            db.SaveChanges();

            return Ok();
        }


        // POST: api/CustomIngredients
        [HttpPost]
        [Route("api/CustomIngredients/Save")]
        [ResponseType(typeof(IEnumerable<CustomIngredientModel>))]
        public IHttpActionResult PostCustomIngredient(IEnumerable<CustomIngredientModel> customIngredientModels)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customIngredients = new List<CustomIngredient>(); //CustomIngredientRepository.CreateInstanceList(); 
            customIngredients = ObjectFactory.Parse(customIngredientModels, customIngredients).ToList();

            _customIngredientService.AddList(customIngredients);
            db.SaveChanges();

            var newCustomIngredients = _customIngredientService.GetCustomIngredients(customIngredients);
            var newCustomIngredientsModels = ModelFactory.Create(newCustomIngredients);

            return Ok(newCustomIngredientsModels);
        }

        [HttpPost]
        [Route("api/CustomIngredients/Delete")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteCustomIngredient(IEnumerable<CustomIngredientModel> customIngredientModels)
        {
            //It's the same as the customIngredients/DeleteRecipe but I want to keep them seperate for now
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customIngredients = new List<CustomIngredient>(); //CustomIngredientRepository.CreateInstanceList();
            customIngredients = ObjectFactory.Parse(customIngredientModels, customIngredients).ToList();

            _customIngredientService.RemoveCustomIngredients(customIngredients);

            db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("api/CustomIngredients/DeleteRecipe")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteRecipe(IEnumerable<CustomIngredientModel> customIngredientModels)
        {
            //It's the same as the customIngredients/Delete but I want to keep them seperate for now
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customIngredients = new List<CustomIngredient>(); //CustomIngredientRepository.CreateInstanceList();
            customIngredients = ObjectFactory.Parse(customIngredientModels, customIngredients).ToList();

            _customIngredientService.RemoveCustomIngredients(customIngredients);

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

        private bool CustomIngredientExists(int id)
        {
            return db.CustomIngredients.Count(e => e.Id == id) > 0;
        }
    }
}