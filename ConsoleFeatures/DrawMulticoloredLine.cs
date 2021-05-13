using System;
using System.Collections.Generic;

namespace Telium.ConsoleFeatures
{
    public static class DrawMulticoloredLine
    {
        public static void Draw(IEnumerable<ColoredStringSection> coloredStringSections)
        {
            foreach (var coloredStringSection in coloredStringSections)
            {
                Console.ForegroundColor = coloredStringSection.Color;
                Console.Write(coloredStringSection.Text);
            }
        }

        public readonly struct ColoredStringSection
        {
            public readonly string Text;
            public readonly ConsoleColor Color;

            public ColoredStringSection(string text, ConsoleColor color)
            {
                Text = text;
                Color = color;
            }
        }
    }
}