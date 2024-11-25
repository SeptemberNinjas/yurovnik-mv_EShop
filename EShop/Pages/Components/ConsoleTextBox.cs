using System.Text;

namespace EShop.Pages.Components
{
    public class ConsoleTextBox : IDisplayable
    {
        private string? _text;
        private ConsoleColor _color;

        /// <summary>
        /// Текст внутри бокса
        /// </summary>
        public string Text
        {
            get { return _text!; }
            set { _text = value; }
        }

        /// <summary>
        /// Цвет бокса
        /// </summary>
        public ConsoleColor Color
        {
            get { return Color; }
            set { Color = value; }
        }

        public ConsoleTextBox(string text, ConsoleColor borderColor = ConsoleColor.White)
        {
            _text = text;
            _color = borderColor;
        }

        /// <summary>
        /// Вывести бокс на экран
        /// </summary>
        public void Display()
        {
            var ulCorner = "╔";
            var llCorner = "╚";
            var urCorner = "╗";
            var lrCorner = "╝";
            var vertical = "║";
            var horizontal = "═";

            var lines = _text!.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var longest = 0;
            foreach (string line in lines)
            {
                if (line.Length > longest)
                    longest = line.Length;
            }
            var width = longest + 2;

            string h = string.Empty;
            for (int i = 0; i < width; i++)
                h += horizontal;

            var sb = new StringBuilder();
            sb.AppendLine(ulCorner + h + urCorner);

            foreach (string line in lines)
            {
                var dblSpaces = (((double)width - (double)line.Length) / (double)2);
                var iSpaces = Convert.ToInt32(dblSpaces);

                if (dblSpaces > iSpaces)
                {
                    iSpaces += 1;
                }

                var beginSpacing = "";
                var endSpacing = "";
                for (int i = 0; i < iSpaces; i++)
                {
                    beginSpacing += " ";

                    if (!(iSpaces > dblSpaces && i == iSpaces - 1))
                    {
                        endSpacing += " ";
                    }
                }

                sb.AppendLine(vertical + beginSpacing + line + endSpacing + vertical);
            }

            sb.AppendLine(llCorner + h + lrCorner);

            Console.ForegroundColor = _color;
            Console.WriteLine(sb.ToString());
            Console.ResetColor();
        }

        public async Task DisplayAsync()
        {
            await Task.Run(() =>
            {
                var ulCorner = "╔";
                var llCorner = "╚";
                var urCorner = "╗";
                var lrCorner = "╝";
                var vertical = "║";
                var horizontal = "═";

                var lines = _text!.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                var longest = 0;
                foreach (string line in lines)
                {
                    if (line.Length > longest)
                        longest = line.Length;
                }
                var width = longest + 2;

                string h = string.Empty;
                for (int i = 0; i < width; i++)
                    h += horizontal;

                var sb = new StringBuilder();
                sb.AppendLine(ulCorner + h + urCorner);

                foreach (string line in lines)
                {
                    var dblSpaces = (((double)width - (double)line.Length) / (double)2);
                    var iSpaces = Convert.ToInt32(dblSpaces);

                    if (dblSpaces > iSpaces)
                    {
                        iSpaces += 1;
                    }

                    var beginSpacing = "";
                    var endSpacing = "";
                    for (int i = 0; i < iSpaces; i++)
                    {
                        beginSpacing += " ";

                        if (!(iSpaces > dblSpaces && i == iSpaces - 1))
                        {
                            endSpacing += " ";
                        }
                    }

                    sb.AppendLine(vertical + beginSpacing + line + endSpacing + vertical);
                }

                sb.AppendLine(llCorner + h + lrCorner);

                Console.ForegroundColor = _color;
                Console.WriteLine(sb.ToString());
                Console.ResetColor();
            });
        }
    }
}

