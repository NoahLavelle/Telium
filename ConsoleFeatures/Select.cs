using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Telium.Objects;

namespace Telium.ConsoleFeatures
{
    public class Select
    {
        private readonly RoomData _roomData;
        private readonly JObject[] _jObjects;
        
        public Select(RoomData roomData)
        {
            _roomData = roomData;
            _jObjects = roomData.Objects;
            Player.Power--;
            if (Player.Power == 0)
            {
                Player.GameOver();
                return;
            }
            
            RunSelect();
        }

        private void SendHeaderMessage()
        {
            Console.WriteLine($"=====================================================================================================\n{(_roomData.OverrideEntryText ? "" : $"Power: {Player.Power}")}");
            DrawMulticoloredLine.Draw(new[]
            {
                new DrawMulticoloredLine.ColoredStringSection("? ", ColorScheme.PromptColor),
                new DrawMulticoloredLine.ColoredStringSection($"{(_roomData.OverrideEntryText ? "" :"You have entered")}{(_roomData.OverrideEntryText ? "" : " ")}{_roomData.Name}. {_roomData.Description}. " +
                                                              $"What would you like to do?\n", ColorScheme.DefaultColor),
            });
        }

        private void RunSelect()
        {
            var selectedObject = _jObjects[0];
            Console.CursorVisible = false;

            SendHeaderMessage();

            while (true)
            {
                foreach (var jObject in _jObjects)
                {
                    Console.ForegroundColor = selectedObject == jObject ? ColorScheme.SelectionColor : ColorScheme.DefaultColor;
                    Console.Write(selectedObject == jObject ? "> " : "  ");
                    Console.WriteLine(jObject["name"]);
                    Console.ForegroundColor = ColorScheme.DefaultColor;
                }

                var consoleKey = Console.ReadKey(true);
                switch (consoleKey.Key)
                {
                    case ConsoleKey.UpArrow or ConsoleKey.DownArrow:
                        selectedObject = _jObjects[Math.Clamp(Array.IndexOf(_jObjects, selectedObject) + (consoleKey.Key == ConsoleKey.UpArrow ? -1 : 1), 0, _jObjects.Length - 1)];
                        break;
                    case ConsoleKey.Enter:
                        var type = Type.GetType($"Telium.Objects.{selectedObject["type"]}");
                        Console.WriteLine();
                        Console.CursorVisible = true;
                        Activator.CreateInstance(type ?? throw new InvalidOperationException(), selectedObject["interactData"]);
                        return;
                }
                
                Console.CursorTop -= _jObjects.Length;
            }
        }
    }
}