using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThoughtRecordApp.Infrastructure.Interfaces
{
    public interface IStringResourceService
    {
        void SetFile(string fileName);

        string GetString(string key);

        string GetString(string fileName, string key);
    }
}
