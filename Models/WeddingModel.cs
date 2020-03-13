using System.ComponentModel.DataAnnotations;
using System;
using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingId { get; set; }
        [Required]
        [Display(Name = "Wedder One's First Name")]

        public string WedderOneFirstName { get; set; }
        [Required]
        [Display(Name = "Wedder One's Last Name")]

        public string WedderOneLastName { get; set; }

        [Required]
        [Display(Name = "Wedder Two's First Name")]

        public string WedderTwoFirstName { get; set; }
        [Required]
        [Display(Name = "Wedder Two's Last Name")]

        public string WedderTwoLastName { get; set; }
        [Required]
        [Display(Name = "Date & Time Of Wedding")]
        public DateTime WeddingDate { get; set; }
        [Required]
        [Display(Name = "Street Address")]
        public string WeddingStreetAddress { get; set; }

        [Required]
        [Display(Name = "City")]
        public string WeddingCity { get; set; }
        
        [Required]
        [Display(Name = "State")]
        public string WeddingState { get; set; }
        
        [Required]
        [Display(Name = "Zipcode")]
        public string WeddingZipcode { get; set; }

        public int UserId {get; set;}

        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Updated_At { get; set; } = DateTime.Now;
        public List<UserWeddingViewModel> WeddingtoUser { get; set; }
    }

}