using Microsoft.EntityFrameworkCore;
using NationalPark.Data.Repository.Interfaces;
using NationalPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalPark.Data.Repository.Implementations
{
    public class TrailRepository : ITrailRepository
    {
        private readonly ApplicationDbContext _context;
        public TrailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateTrail(Trail trail)
        {
            _context.Trails.Add(trail);
            return Save();
        }

        public bool DeleteTrail(Trail trail)
        {
            _context.Trails.Remove(trail);
            return Save();
        }

        public ICollection<Trail> GetAllTrails()
        {
            var result = _context.Trails.Include(c => c.Park).OrderBy(X => X.Name).ToList();
            return result;
        }

        public Trail GetTrailById(int trailId)
        {
            var result = _context.Trails.Include(c => c.Park).FirstOrDefault(x => x.Id == trailId);
            return result;
        }

        public ICollection<Trail> GetTrailsInPark(int parkId)
        {
            var result = _context.Trails.Include(c => c.Park).Where(c => c.ParkId == parkId).ToList();
            return result;
        }

        
        public bool TrailExists(string name)
        {
           bool result = _context.Trails.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            return result;  
        }

        public bool TrailExists(int id)
        {
            var result = _context.Trails.Any(x => x.Id == id);
            return result;
        }

        public bool UpdateTrail(Trail trail)
        {
           _context.Trails.Update(trail);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

    }
}
