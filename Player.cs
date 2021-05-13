using System;
using Telium.ConsoleFeatures;

namespace Telium
{
    public class Player
    {
        public static int Power = 15;

        public static void GameOver()
        {
            Console.WriteLine("=====================================================================================================");
            DrawMulticoloredLine.Draw(new[]
            {
                new DrawMulticoloredLine.ColoredStringSection("(×_×) ", ConsoleColor.DarkRed),
                new DrawMulticoloredLine.ColoredStringSection("You ran out of power. Game Over", ColorScheme.DefaultColor)
            });
        }
    }
}