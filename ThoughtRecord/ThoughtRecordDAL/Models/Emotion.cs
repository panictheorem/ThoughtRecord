using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtRecordDAL.Models
{
    public class Emotion
    {
        public string Name { get; set; }
        public int InitialRating { get; set; }
        public int SubsequentRating { get; set; }
    }
}
