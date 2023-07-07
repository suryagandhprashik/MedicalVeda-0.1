using System.ComponentModel.DataAnnotations;

namespace MVC.Boilerplate.Models.Account
{
    public class Register
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string UserName { get; set; }

        [Required]

        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{6,}$", ErrorMessage = "Password must be of 6 character and include atleast 1 Uppercase , 1 AlphaNumeric value and 1 special character.")]
        public string Password { get; set; }

        public string? Message { get; set; }
    }

}
