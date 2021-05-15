using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Telium.Objects;

namespace Telium.ConsoleFeatures
{
    public class LoadRoom
    {
        // RoomData is the data loaded from the rooms JSON file
        private readonly RoomData _roomData;
        // An array of the objects in the room. These are specified in the objects array in a rooms JSON file
        private readonly JObject[] _jObjects;
        private readonly Action _postLoadInjection;
        
        public LoadRoom(RoomData roomData, Action postLoadInjection)
        {
            _roomData = roomData;
            _postLoadInjection = postLoadInjection;
            _jObjects = roomData.Objects;
            GameData.RoomNumber = roomData.RoomNumber;

            RunDialogue();
        }

        private void SendHeaderMessage()
        {
            // Override entry text exists to reduce the text needed for the json of doors. May be removed soon
            Console.WriteLine($"=====================================================================================================\n{(_roomData.OverrideEntryText ? "" : $"Power: {Player.Power}")}");
            _postLoadInjection?.Invoke();
            DrawMulticoloredLine.Draw(new[]
            {
                new DrawMulticoloredLine.ColoredStringSection("? ", ColorScheme.PromptColor),
                new DrawMulticoloredLine.ColoredStringSection($"{(_roomData.OverrideEntryText ? "" :"You have entered")}{(_roomData.OverrideEntryText ? "" : " ")}{_roomData.Name}. {_roomData.Description}. " +
                                                              $"What would you like to do?\n", ColorScheme.DefaultColor),
            });
        }

        private void RunDialogue()
        {
            // Stores the JObject that is currently selected
            var selectedObject = _jObjects[0];
            Console.CursorVisible = false;

            SendHeaderMessage();

            while (true)
            {
                foreach (var jObject in _jObjects)
                {
                    // If the item is currently selected it is drawn in blue with > in front of it. If it is not it is normal
                    Console.ForegroundColor = selectedObject == jObject ? ColorScheme.SelectionColor : ColorScheme.DefaultColor;
                    Console.Write(selectedObject == jObject ? "> " : "  ");
                    Console.WriteLine(jObject["name"]);
                    Console.ForegroundColor = ColorScheme.DefaultColor;
                }

                // Input from the console is collected here to allow the selection to be changed
                var consoleKey = Console.ReadKey(true);
                switch (consoleKey.Key)
                {
                    case ConsoleKey.UpArrow or ConsoleKey.DownArrow:
                        selectedObject = _jObjects[Math.Clamp(Array.IndexOf(_jObjects, selectedObject) + (consoleKey.Key == ConsoleKey.UpArrow ? -1 : 1), 0, _jObjects.Length - 1)];
                        break;
                    case ConsoleKey.Enter:
                        // When an item is selected the type specified in its JObject is found so the appropriate class can be instanced using the Activator class
                        var type = Type.GetType($"Telium.Objects.{selectedObject["type"]}");
                        Console.WriteLine();
                        Console.CursorVisible = true;
                        
                        // Every time the player moves his power is depleted until there is none left
                        Player.Power--;
                        if (Player.Power == 0)
                        {
                            // Runs the Game over sequence that can be found in the player class when power is out. I may move this function soon
                            Player.GameOver();
                            return;
                        }
                        
                        // Instances the class for the object that was found earlier with the JObjects interaction data specified in its JSON. An exception is thrown if no type is found
                        Activator.CreateInstance(type ?? throw new NullReferenceException(), selectedObject["interactData"]);
                        return;
                }
                
                // The cursor is moved to the start of the options so they are written over next time
                Console.CursorTop -= _jObjects.Length;
            }
        }
    }
}