using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Telium.Objects;

namespace Telium.ConsoleFeatures
{
    public class LoadRoom
    {
        Random rand = new Random();
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

            RunDialogue(_jObjects);
        }

        private bool SendHeaderMessage()
        {
            // Override entry text exists to reduce the text needed for the json of doors. May be removed soon
            Console.WriteLine($"=====================================================================================================\n{(_roomData.OverrideEntryText ? "" : $"Power: {Player.Power}")}");
            
            if (CheckRoomEvents()) {
                return false;
            }
            
            DrawMulticoloredLine.Draw(new[]
            {
                new DrawMulticoloredLine.ColoredStringSection("? ", ColorScheme.PromptColor),
                new DrawMulticoloredLine.ColoredStringSection($"{(_roomData.OverrideEntryText ? "" :"You have entered")}{(_roomData.OverrideEntryText ? "" : " ")}{_roomData.Name}. {_roomData.Description}. " +
                                                              $"What would you like to do?\n", ColorScheme.DefaultColor),
            });
            return true;
        }

        private bool CheckRoomEvents() {
                // This code is run just after the room is switched. We run npc checking here
                // This finds all npcs where their room is the same as the room you just moved into
                var enumerable = GameNpcData.Data.Where(npc => npc.RoomNumber == GameData.RoomNumber);
                // I loop through each npc and tell the player about it
                foreach (var npc in enumerable)
                {
                    if (npc.Type == NpcHandling.NpcType.VentilationShaft) {
                        VentilationShaftEntered();
                        return true;
                    }

                    DrawMulticoloredLine.Draw(new []
                    {
                        new DrawMulticoloredLine.ColoredStringSection($"There is a {npc.Type} in here\n", ConsoleColor.DarkRed)
                    });
                }

                return false;
        }

        void VentilationShaftEntered() {
            int newFuel = Player.FlamethrowerFuel + ((int) (rand.Next(20, 50) / 10)) * 10;
            DrawMulticoloredLine.Draw(new [] {
                new DrawMulticoloredLine.ColoredStringSection("There is a ", ConsoleColor.White),
                new DrawMulticoloredLine.ColoredStringSection("bank of fuel cells ", ConsoleColor.Cyan),
                new DrawMulticoloredLine.ColoredStringSection($"in here. You load one into your flamethrower. Fuel was {Player.FlamethrowerFuel} but is now reading {newFuel}\n" +
                    "The doors suddenly lock shut... What is happening to the space station? Our only escape is to climb into the ventilation shaft, " +
                    "but we don't know where we are going.\nWe follow the passeges and find ourselves sliding down\n", ConsoleColor.White),
            });

            Player.FlamethrowerFuel = newFuel;
            Prompt.LoadRoom(new Room($"Rooms/room{rand.Next(2, 17)}.json").RoomData);
        }

        private void RunDialogue(JObject[] jObjects)
        {
            // Stores the JObject that is currently selected
            var selectedObject = jObjects[0];
            Console.CursorVisible = false;

            if (!SendHeaderMessage()) {
                return;
            }

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