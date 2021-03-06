using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalPark.Models
{
    public class Trail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }
        public enum DifficultyType { Easy, Moderate, Difficult, Expert}
        public DifficultyType Difficulty { get; set; }
        [Required]
        public int ParkId { get; set; }
        [ForeignKey("ParkId")]
        public Park Park { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
