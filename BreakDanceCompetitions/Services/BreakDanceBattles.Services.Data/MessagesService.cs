using BreakDanceBattles.Data.Common.Repositories;
using BreakDanceBattles.Data.Models;
using BreakDanceBattles.Services.Mapping;
using BreakDanceBattles.Web.ViewModels.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakDanceBattles.Services.Data
{
    public class MessagesService : IMessagesService
    {
        private readonly IDeletableEntityRepository<Message> messagesRepository;

        public MessagesService(IDeletableEntityRepository<Message> messagesRepository)
        {
            this.messagesRepository = messagesRepository;
        }
        public async Task CreateMessageAsync(CreateMessageInputModel input, string userId)
        {
            var Message  = new Message() 
            {
                Subject = input.Subject,
                MessageContent = input.MessageContent,
                AddedByUserId = userId,
            };
            await this.messagesRepository.AddAsync(Message);
            await this.messagesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var message = this.messagesRepository.All().FirstOrDefault(x => x.Id == id);
            this.messagesRepository.Delete(message);
            await this.messagesRepository.SaveChangesAsync();
        }

        public IEnumerable<MessageInListViewModel> GetAll()
        {
            var messages = this.messagesRepository.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .To<MessageInListViewModel>()
                .ToList();
            return messages;

        }

        public T GetById<T>(int id)
        {
            var message = this.messagesRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return message;
        }
    }
}
