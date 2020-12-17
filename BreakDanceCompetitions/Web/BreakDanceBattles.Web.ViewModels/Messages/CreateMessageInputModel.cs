using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BreakDanceBattles.Web.ViewModels.Messages
{
    public class CreateMessageInputModel
    {
        [Required]
        [MaxLength(25)]
        [MinLength(4)]
        public string Subject { get; set; }
        [Required]
        [MinLength(20)]
        public string MessageContent { get; set; }
    }
}
