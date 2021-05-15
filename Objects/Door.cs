using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Telium.ConsoleFeatures;

namespace Telium.Objects {
    public class Door {
        public Door(JObject interactData)
        {
            // The door is simple. When interacted with it gets the linked room name specified in the interaction data and creates a file path with it
            // This file path is used to load the new room
            Prompt.LoadRoom(new Room($"Rooms/{interactData["room"]}.json").RoomData, () =>
            {
                var enumerable = GameNpcData.Data.Where(npc => npc.RoomNumber == GameData.RoomNumber);
                foreach (var npc in enumerable)
                {
                    DrawMulticoloredLine.Draw(new []
                    {
                        new DrawMulticoloredLine.ColoredStringSection($"There is a {npc.Type} in here\n", ConsoleColor.DarkRed)
                    });
                }
            });
        }
    }
}