using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApix.Data.Models;

namespace TaskApix.Dtos.Region
{
    public class RegionDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public long CountryId { get; set; }
    }
}
