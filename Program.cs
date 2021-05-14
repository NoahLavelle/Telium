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
            // Loads the title screen room. This contains the buttons to start the game and read the instructions.
            // I store the title screen in a room so we can use the existing LoadRoom features to display the options
            Prompt.LoadRoom(new Room("Rooms/titleScreen.json").RoomData);
    
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