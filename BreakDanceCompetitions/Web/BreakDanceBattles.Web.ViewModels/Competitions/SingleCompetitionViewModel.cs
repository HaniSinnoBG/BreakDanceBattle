using AutoMapper;
using BreakDanceBattles.Data.Models;
using BreakDanceBattles.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Web.ViewModels.Competitions
{
    public class SingleCompetitionViewModel :IMapFrom<Competition>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public string AddedByUserId { get; set; }

        public string AddedByUserUserName { get; set; }

        public string CountryName { get; set; }
        public string ImageUrl { get; set; }

        public virtual IEnumerable<CategoriesViewModel> Categories{ get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Competition, SingleCompetitionViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                 opt.MapFrom(x => x.Image.RemoteImageUrl));
        }
    }
}
