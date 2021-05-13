using System;
using Telium.ConsoleFeatures;

namespace Telium
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Prompt.Input("Name");
            Prompt.Select(new Room("Rooms/roomOne.json").RoomData);
    
        }
    }
}