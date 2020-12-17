using BreakDanceBattles.Data;
using BreakDanceBattles.Data.Common.Repositories;
using BreakDanceBattles.Data.Models;
using BreakDanceBattles.Services.Data.Contracts;
using BreakDanceBattles.Web.ViewModels.Competitions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BreakDanceBattles.Services.Data.Tests
{
   public  class CompetitionServiceTests
    {
        private readonly CompetitionService _competitionService;
        private Mock<IDeletableEntityRepository<Competition>> _competitionsRepository = new Mock<IDeletableEntityRepository<Competition>>();
        private readonly Mock<IRepository<Image>> _imagesRepository = new Mock<IRepository<Image>>();
        private readonly Mock<IDeletableEntityRepository<Category>> _categoriesRespository = new Mock<IDeletableEntityRepository<Category>>();
        private readonly Mock<UserManager<ApplicationUser>> _userManager = new Mock<UserManager<ApplicationUser>>();
        private readonly Mock<IRepository<CompetitionUser>> _joinedRepository = new Mock<IRepository<CompetitionUser>>();

        public CompetitionServiceTests()
        {
            competitionService = new CompetitionService(competitionsRepository.Object, imagesRepository.Object, categoriesRespository.Object, userManager.Object, joinedRepository.Object);

        }
        [Fact]
        public async void CreateCompetitionShouldCreateCompetition() 
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var competitionsRepository = new Mock<IDeletableEntityRepository<Competition>(new ApplicationDbContext(options.Options));
            var imagesRepository = new Mock<IRepository<Image>(new ApplicationDbContext(options.Options));
            var categoriesRepository = new Mock<IRepository<Image>(new ApplicationDbContext(options.Options));
            var imagesRepository = new Mock<IRepository<Image>(new ApplicationDbContext(options.Options));
            var imagesRepository = new Mock<IRepository<Image>(new ApplicationDbContext(options.Options));
        }
        [Fact]
        public async void CreateNewAppointmentShouldCreateApointment()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var appointmentsRepository = new EfDeletableEntityRepository<Appointment>(new ApplicationDbContext(options.Options));
            var notificationsRepository = new EfDeletableEntityRepository<Notification>(new ApplicationDbContext(options.Options));

            var appointmentsService = new AppointmentsService(notificationsRepository, appointmentsRepository);

            await appointmentsService.CreateNewAppointment(new Appointment
            {
                Status = AppointmentStatus.Unprocessed,
                Date = DateTime.UtcNow,
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddMinutes(5),
                DogsitterId = "123",
                OwnerId = "321",
            });

            Assert.Equal(1, appointmentsRepository.All().Count());
        }
    }
}
