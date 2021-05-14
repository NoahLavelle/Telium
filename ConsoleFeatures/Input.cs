using System;

namespace Telium.ConsoleFeatures
{
    public class Input
    {
        public readonly string Answer;

        public Input(string message)
        {
            message += ": ";

            // Draws the question and waits for an answer. It exists so I can style it
            DrawMulticoloredLine.Draw(new DrawMulticoloredLine.ColoredStringSection[]
                {new("? ", ColorScheme.PromptColor), new(message, ColorScheme.DefaultColor)});
            var answer = Console.ReadLine();
            Answer = answer;
        }
    }
}