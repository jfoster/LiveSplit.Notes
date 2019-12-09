using System.Drawing;

namespace LiveSplit.SplitNotes
{
    class SplitNote
    {
        public string Name { get; set; }
        public string[] Text { get; set; }

        public SplitNote()
        {

        }

        public void Draw(Graphics g, SizeF s, Font f, Color c)
        {
            var splitText = "";
            if (Text != null && Text.Length > 0)
            {
                splitText = string.Join("\n", Text);
            }
            var font = AdjustedFont(g, splitText, f, s, f.SizeInPoints, 5);
            g.DrawString(splitText, font, new SolidBrush(c), new RectangleF(new PointF(0, 0), s), new StringFormat());
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
