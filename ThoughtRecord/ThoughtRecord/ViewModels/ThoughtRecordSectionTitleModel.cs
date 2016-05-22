using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtRecordApp.ViewModels
{
    /// <summary>
    /// A model of all the section titles for a thought record
    /// </summary>
    public class ThoughtRecordSectionTitleModel
    {
        public string SituationTitle { get; set; }
        public string InitialEmotionsTitle { get; set; }
        public string AutomaticThoughtsTitle { get; set; }
        public string EvidenceAgainstThoughtsTitle { get; set; }
        public string EvidenceForThoughtsTitle { get; set; }
        public string RationalAssessment { get; set; }
        public string SubsequentEmotionsTitle { get; set; }
    }
}
