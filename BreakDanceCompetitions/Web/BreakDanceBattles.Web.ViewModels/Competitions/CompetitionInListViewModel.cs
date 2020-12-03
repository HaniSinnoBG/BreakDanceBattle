

namespace BreakDanceBattles.Web.ViewModels.Competitions
{
using AutoMapper;
using BreakDanceBattles.Data.Models;
using BreakDanceBattles.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;    
    public class CompetitionInListViewModel 
    {
        public int Id { get; set; }
        public string RemoteImageUrl { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string DateTime { get; set; }
        public string Categories { get; set; }


    }
}
