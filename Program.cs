using System;
using Telium.ConsoleFeatures;

namespace Telium
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            RunTitleScreen();
            Prompt.Input("Name");
            Prompt.Select(new Room("Rooms/titleScreen.json").RoomData);
    
        }

        private static void RunTitleScreen()
        {
            DrawMulticoloredLine.Draw(new []
            {
                new DrawMulticoloredLine.ColoredStringSection("Telium\n", ConsoleColor.Green),
                new DrawMulticoloredLine.ColoredStringSection("A space text adventure game by\n", ColorScheme.DefaultColor),
                new DrawMulticoloredLine.ColoredStringSection("Noah Lavelle\n\n", ColorScheme.NameColor),
            }, true);
        }
    }
}