using System;
using System.Collections.Generic;

namespace Telium.ConsoleFeatures
{
    public static class DrawMulticoloredLine
    {
        public static void Draw(IEnumerable<ColoredStringSection> coloredStringSections, bool centered = false)
        {
            foreach (var coloredStringSection in coloredStringSections)
            {
                if (centered)
                {
                    Console.SetCursorPosition((Console.WindowWidth - coloredStringSection.Text.Length) / 2, Console.CursorTop);
                }
                
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