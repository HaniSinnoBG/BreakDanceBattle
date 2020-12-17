using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Web.ViewModels.Messages
{
    public class MessageListViewModel
    {
        public IEnumerable<MessageInListViewModel> Messages { get; set; }
    }
}
