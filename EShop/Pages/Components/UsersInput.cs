

namespace EShop.Pages.Components
{
    public class UsersInput : IDisplayable
    {
        private readonly string Title;
        
        /// <summary>
        /// Пользовательский ввод
        /// </summary>
        public string? Input => Console.ReadLine();
       
        public UsersInput(string title)
        {
           Title = title;
        }

        /// <summary>
        /// Отобразить элемент
        /// </summary>
        public void Display()
        {
            Console.WriteLine(Title);
        }

        public async Task DisplayAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                Console.WriteLine(Title);
            });
        }
    }
}
