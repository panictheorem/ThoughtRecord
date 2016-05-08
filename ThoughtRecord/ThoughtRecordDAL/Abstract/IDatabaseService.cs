using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.DAL.Concrete;
using ThoughtRecordApp.DAL.Models;

namespace ThoughtRecordApp.DAL.Abstract
{
    /// <summary>
    /// Defines operations of a database service
    /// </summary>
    public interface IDatabaseService
    {
        IRepository<ThoughtRecord> ThoughtRecords { get; }
        IRepository<Situation> Situations { get; }
        IRepository<Emotion> Emotions { get; }
        IRepository<Configuration> Settings { get; }
    }
}
