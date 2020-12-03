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
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public string AddedByUserId { get; set; }

        public string AddedByUserUserName { get; set; }

        public string CountryName { get; set; }

        public virtual IEnumerable<CategoriesViewModel> Categories{ get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Competition, CompetitionInListViewModel>()
                .ForMember(x => x.RemoteImageUrl, opt =>
                 opt.MapFrom(x => x.Image.RemoteImageUrl));
        }
    }
}
