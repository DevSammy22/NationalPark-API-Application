using NationalPark.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NationalPark.Models.Trail;

namespace NationalPark.Dto.Request
{
    public class TrailRequestDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        public DifficultyType Difficulty { get; set; }
        [Required]
        public int ParkId { get; set; }
        public ParkRequestDto Park { get; set; }
    }
}
