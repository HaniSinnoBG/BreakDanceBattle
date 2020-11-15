using System;
using System.Collections.Generic;
using System.Text;

namespace BreakDanceBattles.Data.Models
{
    public class Competition
    {   
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public string AddedByUserId { get; set; }

        //country

      


    }
}
