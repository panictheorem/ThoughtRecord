using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtRecordDAL.Models
{
    public class Situation
    {
        [PrimaryKey, AutoIncrement]
        public int SituationId { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }

        [ForeignKey(typeof(ThoughtRecord))]
        public int ThoughtRecordID { get; set; }
        [OneToOne]
        public ThoughtRecord ThoughtRecord { get; set; }
    }
}
