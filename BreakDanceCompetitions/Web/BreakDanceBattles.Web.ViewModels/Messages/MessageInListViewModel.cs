using BreakDanceBattles.Data.Models;
using BreakDanceBattles.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Web.ViewModels.Messages
{
    public class MessageInListViewModel : IMapFrom<Message>
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string MessageContent { get; set; }
        public ApplicationUser AddedByUser { get; set; }
    }
}
