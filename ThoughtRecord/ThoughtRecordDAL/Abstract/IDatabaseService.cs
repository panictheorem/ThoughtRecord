using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordDAL.Concrete;
using ThoughtRecordDAL.Models;

namespace ThoughtRecordDAL.Abstract
{
    public interface IDatabaseService
    {
        IRepository<ThoughtRecord> ThoughtRecords { get; }
        IRepository<Situation> Situations { get; }
        IRepository<Emotion> Emotions { get; }
        IRepository<Configuration> Settings { get; }
    }
}
