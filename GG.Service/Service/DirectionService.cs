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
    public interface IDirectionService
    {
        IQueryable<Direction> GetAll();
        Direction GetDirectionById(int id);
        void Update(Direction direction);
        void AddList(IEnumerable<Direction> directions);
        void RemoveById(int id);
        IQueryable<Direction> GetDirections(IEnumerable<Direction> directions);
        IQueryable<Direction> GetDirectionByIds(IEnumerable<int> directions);
    }
    public class DirectionService : IDirectionService
    {
        private GGDataContext _db;

        public DirectionService(GGDataContext db)
        {
            _db = db;
        }

        public IQueryable<Direction> GetAll()
        {
            return _db.Directions;
        }

        public Direction GetDirectionById(int id)
        {
            return _db.Directions.Find(id);
        }

        public void Update(Direction direction)
        {
            _db.Entry(direction).State = EntityState.Modified;
        }

        public void AddList(IEnumerable<Direction> directions)
        {
            foreach (var direction in directions)
            {
                if (direction.Id == 0)
                    Add(direction);
                else
                    Update(direction);
            }
        }

        public void Add(Direction direction)
        {
            _db.Directions.Add(direction);
        }

        public void RemoveById(int id)
        {
            var direction = GetDirectionById(id);
            _db.Directions.Remove(direction);
        }

        public IQueryable<Direction> GetDirectionByIds(IEnumerable<int> ids)
        {
            return GetAll().Where(x => ids.Contains(x.Id));
        }

        public IQueryable<Direction> GetDirections(IEnumerable<Direction> directions)
        {
            var directionIds = directions.Select(x => x.Id);
            return GetDirectionByIds(directionIds);
        }
    }
}
