
using System.ComponentModel.DataAnnotations;

namespace FoodLoop.Models
{
    public class Food
    {
        public int Id { get; set; }

        [Required]
        public string FoodName { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Available";
        public DateTime DonationDate { get; set; } = DateTime.Now;
    }
}
