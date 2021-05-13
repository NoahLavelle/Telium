using System;
using Telium.ConsoleFeatures;

namespace Telium
{
    class Program
    {
        static void Main(string[] args)
        {
            Prompt.Input("Name");
            Prompt.Select(new Room("Rooms/roomOne.json").roomData);
    
        }
    }
}