using BreakDanceBattles.Web.ViewModels.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BreakDanceBattles.Services.Data
{
    public interface IMessagesService
    {
        Task CreateMessageAsync(CreateMessageInputModel input, string userId);
        IEnumerable<MessageInListViewModel> GetAll();
        T GetById<T>(int id);
        Task DeleteAsync(int id);
    }
}
