using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BreakDanceBattles.Data.Models;
using BreakDanceBattles.Services.Data;
using BreakDanceBattles.Web.ViewModels.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BreakDanceBattles.Web.Controllers
{
    public class MessagesController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMessagesService messagesService;

        public MessagesController(UserManager<ApplicationUser> userManager,
            IMessagesService messagesService)
        {
            this.userManager = userManager;
            this.messagesService = messagesService;
        }
        [Authorize]
        public IActionResult CreateMessage()
        {
            var virewModel = new CreateMessageInputModel();
            return View(virewModel);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateMessage(CreateMessageInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            var user = await this.userManager.GetUserAsync(this.User);

            await this.messagesService.CreateMessageAsync(input, user.Id);


            return this.Redirect("/Home/Index");
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult AllMessages()
        {
            var viewModel = new MessageListViewModel
            {
                Messages = this.messagesService.GetAll(),

            };
            return this.View(viewModel);
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult ById(int id)
        {
            var viewModel = this.messagesService.GetById<MessageInListViewModel>(id);
            return this.View(viewModel);
             

        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
                await this.messagesService.DeleteAsync(id);
                return this.Redirect("/Messages/AllMessages");
        }
    }
}