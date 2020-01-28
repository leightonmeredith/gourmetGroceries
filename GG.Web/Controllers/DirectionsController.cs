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
    public class DirectionsController : ApiController
    {
        private GGDataContext db = new GGDataContext();
        private readonly IDirectionService _directionService;

        public DirectionsController()
        {
            _directionService = new DirectionService(db);
        }

        // GET: api/Directions
        public IHttpActionResult GetDirections()
        {
            var directions = _directionService.GetAll();

            var directionModels = ModelFactory.Create(directions);
            return Ok(directionModels);
        }

        // GET: api/Directions/5
        [ResponseType(typeof(DirectionModel))]
        public IHttpActionResult GetDirection(int id)
        {
            //Direction direction = 
            var direction = _directionService.GetDirectionById(id);

            if (direction == null)
                return NotFound();

            var directionModel = ModelFactory.Create(direction);
            return Ok(directionModel);
        }

        // PUT: api/Directions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDirection(int id, DirectionModel directionModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != directionModel.Id)
                return BadRequest();

            var direction = _directionService.GetDirectionById(id);
            ObjectFactory.Parse(directionModel, direction);

            _directionService.Update(direction);            

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectionExists(id))
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

        // POST: api/Directions
        [Route("api/Directions/Save")]
        [ResponseType(typeof(IEnumerable<DirectionModel>))]
        public IHttpActionResult PostDirection(IEnumerable<DirectionModel> directionModels)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var directions = new List<Direction>();
            directions = ObjectFactory.Parse(directionModels, directions).ToList();

            _directionService.AddList(directions);

            db.SaveChanges();

            var newDirections = _directionService.GetDirections(directions);
            var newDirectionsModels = ModelFactory.Create(newDirections);

            return Ok(newDirectionsModels);
        }

        // DELETE: api/Directions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteDirection(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _directionService.RemoveById(id);
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

        private bool DirectionExists(int id)
        {
            return db.Directions.Count(e => e.Id == id) > 0;
        }
    }
}