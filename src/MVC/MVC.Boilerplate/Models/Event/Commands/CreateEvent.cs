using System.ComponentModel.DataAnnotations;

namespace MVC.Boilerplate.Models.Event.Commands
{
    public class CreateEvent
    {
        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Date")]
        [DataType(DataType.DateTime, ErrorMessage ="Date should be in format dd-mm-yy hh:mm am/pm")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Price field is required")]
        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Price should be greater than zero")]
        public int Price { get; set; }

        [Required]
        public string Artist { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Url(ErrorMessage = "Url should start with Http:// or Https://")]
        public string ImageUrl { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}
