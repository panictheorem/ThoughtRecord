using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtRecordApp.DAL.Models
{
    public class Emotion : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        public int EmotionId { get; set; }

        private string name;
        public string Name {
            get
            {
                return name;
            }
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }

        private int initialRating;
        public int InitialRating {
            get
            {
                return initialRating;
            }
            set
            {
                initialRating = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InitialRating"));
            }
        }

        private int subsequentRating;
        public int SubsequentRating {
            get
            {
                return subsequentRating;
            }
            set
            {
                subsequentRating = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SubsequentRating"));
            }
        }

        [ForeignKey(typeof(ThoughtRecord))]
        public int ThoughtRecordId { get; set; }

        [ManyToOne]
        public ThoughtRecord ThoughtRecord { get; set; }
    }
}
