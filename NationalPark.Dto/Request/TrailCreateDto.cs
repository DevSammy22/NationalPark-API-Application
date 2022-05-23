using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NationalPark.Models.Trail;

namespace NationalPark.Dto.Request
{
    public class TrailCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        public DifficultyType Difficulty { get; set; }
        [Required]
        public int ParkId { get; set; }
    }
}
