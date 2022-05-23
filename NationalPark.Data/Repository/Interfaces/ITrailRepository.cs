using NationalPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalPark.Data.Repository.Interfaces
{
    public interface ITrailRepository
    {
        ICollection<Trail> GetAllTrails();
        ICollection<Trail> GetTrailsInPark(int parkId);
        Trail GetTrailById(int trailId);
        bool TrailExists(string name);
        bool TrailExists(int id);
        bool CreateTrail(Trail trail);
        bool UpdateTrail(Trail trail);
        bool DeleteTrail(Trail trail);
        bool Save();
    }
}
