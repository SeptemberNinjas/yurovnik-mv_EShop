using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Commands
{
    public interface ICommandExecutable
    {   
        /// <summary>
        /// Результат
        /// </summary>
        public string? Result { get; }
        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="args"></param>
        public void Execute(string[]? args);

        public Task ExecuteAsync(string[]? args);
    }
}
