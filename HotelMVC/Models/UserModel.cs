using System.ComponentModel.DataAnnotations;

namespace HotelMVC.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Role { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Pole hasło i potwierdź hasło muszą być takie same")]
        public string ConfirmPassword { get; set; }
        

    }
}