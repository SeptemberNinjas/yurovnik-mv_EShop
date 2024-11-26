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
        /// Выполнить команду асинхронно
        /// </summary>
        /// <param name="args"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ExecuteAsync(string[]? args, CancellationToken cancellationToken);
    }
}
