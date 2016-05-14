
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtRecordApp.DAL.Models
{
    public class ThoughtRecord
    {
        [PrimaryKey, AutoIncrement]
        public int ThoughtRecordId { get; set; }
        [ForeignKey(typeof(Situation))]
        public int SituationId { get; set; }
        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Situation Situation { get; set; }
        public string AutomaticThoughts { get; set; }
        public string SupportingEvidence { get; set; }
        public string ContradictingEvidence { get; set; }
        public string RationalAssessment { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Emotion> Emotions { get; set; }

    } 
}
