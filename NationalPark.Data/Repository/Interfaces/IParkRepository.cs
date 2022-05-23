using NationalPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalPark.Data.Repository.Interfaces
{
    public interface IParkRepository
    {
        ICollection<Park> GetParks();
        Park GetPark(int parkId);
        bool ParkExists(string name);
        bool ParkExists(int id);
        bool CreatePark(Park park);
        bool UpdatePark(Park park);
        bool DeletePark(Park park);
        bool Save();

    }
}
