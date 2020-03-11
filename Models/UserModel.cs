using System.ComponentModel.DataAnnotations;
using System;
using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Your 'First Name' has to be at least (2) characters in length")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Your 'Last Name' has to be at least (2) characters in length")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a properly formatted email address or register under a different email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Your 'Password' has to be at least (2) characters in length")]
        public string Password { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Updated_At { get; set; } = DateTime.Now;

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public List<UserWeddingViewModel> UsertoWedding { get; set; }
    }
    public class LoginUser
    {
        [Required(ErrorMessage = "Please enter your correct profile email address")]
        [EmailAddress]
        public string LoginEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string LoginPassword { get; set; }
    }

}