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
        public string WedderOne { get; set; }
        [Required]
        public string WedderTwo { get; set; }
        [Required]
        public DateTime WeddingDate { get; set; }
        [Required]
        public string WeddingAddress { get; set; }
        public int UserId {get; set;}

        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Updated_At { get; set; } = DateTime.Now;
        public List<UserWeddingViewModel> WeddingtoUser { get; set; }
    }

}