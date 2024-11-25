namespace EShop.Pages
{
    public interface IDisplayable
    {
        /// <summary>
        /// Вывести на экран
        /// </summary>
        public void Display();
        public Task DisplayAsync();
    }
}
