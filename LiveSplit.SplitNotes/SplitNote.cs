using System.Drawing;
using GraphicsExtensions;

namespace LiveSplit.SplitNotes
{
    class SplitNote
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public SplitNote(string name, string[] text)
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

        public SplitNote(string name, string text)
        {
            Name = name;
            Text = text;
        }
    }
}
