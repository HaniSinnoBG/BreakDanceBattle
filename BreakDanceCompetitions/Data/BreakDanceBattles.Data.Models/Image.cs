using BreakDanceBattles.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Data.Models
{
    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public int CompetitionId { get; set; }

        public virtual Competition Competition { get; set; }
        public string RemoteImageUrl { get; set; }
        public string Extension { get; set; }
        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}
