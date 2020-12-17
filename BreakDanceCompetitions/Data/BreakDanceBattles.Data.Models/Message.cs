using BreakDanceBattles.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BreakDanceBattles.Data.Models
{
    public class Message : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(25)]
        [MinLength(4)]
        public string Subject { get; set; }
        [Required]
        [MinLength(20)]
        public string MessageContent { get; set; }

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }
    }
}
