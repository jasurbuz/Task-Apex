using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApix.Data.Models
{
    public class Region
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }

        #region Relation
        public long CountryId { get; set; }
        public Country Country { get; set; }
        #endregion
    }
}
