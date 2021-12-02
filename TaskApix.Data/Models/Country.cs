using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApix.Data.Models
{
    public class Country
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Code { get; set; }
        public bool IsSchengen { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }

        #region
        public ICollection<Region> Regions { get; set; }
        #endregion
    }
}
