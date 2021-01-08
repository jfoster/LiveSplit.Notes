using System.Drawing;
using GraphicsExtensions;

namespace LiveSplit.Notes
{
    class Note
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public Note(string name, string[] text)
        {
            Name = name;

            if (text is null)
            {
                Text = "";
            }
            else if (text.Length > 1)
            {
                Text = string.Join("\n", text);
            }
            else if (text.Length == 1)
            {
                Text = text[0];
            }
        }

        public Note(string name, string text)
        {
            Name = name;
            Text = text;
        }
    }
}
