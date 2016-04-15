using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordDAL.Infrastructure;

namespace ThoughtRecordDAL.Models
{
    public class ThoughtRecord
    {
        public Situation Situation { get; set; }
        public string AutomaticThoughts { get; set; }
        public string SupportingEvidence { get; set; }
        public string ContradictingEvidence { get; set; }
        public string RationalAssessment { get; set; }
        public DeeplyObservableCollection<Emotion> Emotions { get; set; }

        public ThoughtRecord()
        {
            Situation = new Situation();
            Emotions = new DeeplyObservableCollection<Emotion>();
            Emotions.Add(new Emotion() { Name = "Happy"});
        }
    } 
}
