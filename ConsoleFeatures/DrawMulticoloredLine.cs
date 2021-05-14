using System;
using System.Collections.Generic;

namespace Telium.ConsoleFeatures
{
    
    // By default C# does not support sending multicolored messages so this function makes this process easier.
    public static class DrawMulticoloredLine
    {
        
        // I take in an array of my custom struct ColoredStringSection that dictates the text and its color
        public static void Draw(IEnumerable<ColoredStringSection> coloredStringSections, bool centered = false)
        {
            foreach (var coloredStringSection in coloredStringSections)
            {
                // These sections are then sent as separate messages on the same line to give the illusion of a multicolored message
                // If the message should be centered the cursor position is set to the middle of the window
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