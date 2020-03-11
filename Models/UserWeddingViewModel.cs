using System.ComponentModel.DataAnnotations;
using System;
using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class UserWeddingViewModel
    {
        [Key]
        public int UserWeddingViewModelId { get; set; }
        public int UserId { get; set; }
        public int WeddingId { get; set; }
        public User User { get; set; }
        public Wedding Wedding { get; set; }
    }
}