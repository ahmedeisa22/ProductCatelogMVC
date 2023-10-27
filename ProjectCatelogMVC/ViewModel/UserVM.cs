using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProductCatelogPL.Models
{
    public class UserVM
    {
        [Required(ErrorMessage = "User  Name is required.")]
        [StringLength(50, ErrorMessage = "user Name must not exceed 50 characters.")]
        [Remote("checkUserName", "Account", ErrorMessage = "UserName is aleardy exist")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter valid email")]
        [Remote("checkEmail", "Account", ErrorMessage = "email is aleardy exist")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = "Password must contain at least one letter, one digit, and one special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password must match Password.")]
        public string ConfirmPassword { get; set; }
    }
}
