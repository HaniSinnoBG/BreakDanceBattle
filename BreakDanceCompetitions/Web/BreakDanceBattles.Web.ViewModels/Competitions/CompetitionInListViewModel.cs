

namespace BreakDanceBattles.Web.ViewModels.Competitions
{
using AutoMapper;
using BreakDanceBattles.Data.Models;
using BreakDanceBattles.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;    
    public class CompetitionInListViewModel : IMapFrom<Competition>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public DateTime DateTime { get; set; }
        public virtual IEnumerable<CategoriesViewModel> Categories { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Competition, CompetitionInListViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                 opt.MapFrom(x =>
                 x.Image.RemoteImageUrl != null ?
                 x.Image.RemoteImageUrl:
                 "/images/competitions/" + x.Image.Id + "." + x.Image.Extension));
        }
    }
}
