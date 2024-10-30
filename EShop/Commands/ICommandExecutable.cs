using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Commands
{
    public interface ICommandExecutable
    {
        public string? Result { get; }
        void Execute();
    }
}
