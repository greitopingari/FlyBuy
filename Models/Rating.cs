using System.ComponentModel.DataAnnotations;

namespace FlyBuy.Models
{
    public class Rating
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You need to give a  description")]
        [StringLength(1000, ErrorMessage = "You cant give a description more than 1000 letters")]
        public string Description { get; set; }

        public float Rate { get; set; }

        [StringLength(30, ErrorMessage = "We cant accept names longer than 30 letters")]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; } = DateTime.Now;
    }
}
