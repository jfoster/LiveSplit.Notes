using System.Drawing;

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

        public void Draw(Graphics g, SizeF s, Font f, Color c)
        {
            var font = AdjustedFont(g, Text, f, s, f.SizeInPoints, 5);
            g.DrawString(Text, font, new SolidBrush(c), new RectangleF(new PointF(0, 0), s), new StringFormat());
        }

        public Font AdjustedFont(Graphics g, string s, Font f, SizeF cs, float maxFontSize, float minFontSize)
        {
            for (float AdjustedSize = maxFontSize; AdjustedSize >= minFontSize; AdjustedSize--)
            {
                Font newFont = new Font(f.Name, AdjustedSize, f.Style);

                SizeF AdjustedSizeNew = g.MeasureString(s, newFont);

                if (cs.Width > AdjustedSizeNew.Width && cs.Height > AdjustedSizeNew.Height)
                {
                    return newFont;
                }
            }

            return f;
        }
    }
}
