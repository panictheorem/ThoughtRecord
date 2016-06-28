using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThoughtRecordApp.Infrastructure.Interfaces;

namespace ThoughtRecordApp.Infrastructure.Implementations
{
    public class CortanaVoiceCommand : IVoiceCommand
    {
        public string Name { get; }

        public CortanaVoiceCommand(string commandName)
        {
            Name = commandName;
        }
    }
}
