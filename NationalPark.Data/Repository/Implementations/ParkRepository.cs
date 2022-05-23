using NationalPark.Data.Repository.Interfaces;
using NationalPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalPark.Data.Repository.Implementations
{
    public class ParkRepository : IParkRepository
    {
        private readonly ApplicationDbContext _Context;

        public ParkRepository(ApplicationDbContext context)
        {
            _Context = context;
        }

        public bool CreatePark(Park park)
        {
            _Context.Parks.Add(park);
            return Save();
        }

        public bool DeletePark(Park park)
        {
            _Context.Parks.Remove(park);
            return Save();
        }

        public Park GetPark(int parkId)
        {
            var result = _Context.Parks.FirstOrDefault(x => x.Id == parkId);
            return result;
        }

        public ICollection<Park> GetParks()
        {
            var result = _Context.Parks.OrderBy(x => x.Name).ToList();
            return result;
        }

        public bool ParkExists(string name)
        {
            bool result = _Context.Parks.Any(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            return result;
        }

        public bool ParkExists(int id)
        {
            bool result = _Context.Parks.Any(x => x.Id == id);
            return result;
        }

        public bool Save()
        {
            return _Context.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdatePark(Park park)
        {
            _Context.Parks.Update(park);
            return Save();
        }
    }
}
