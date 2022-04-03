using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EconomicManagementAPP.Models
{
    public class Users
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "Invalid format Email")]
        public string Email { get; set; }
        public string StandarEmail { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Minimum 6 characters required and Maximum 30")]
        public string Password { get; set; }
    }
}
