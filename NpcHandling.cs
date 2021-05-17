using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Telium
{
    public static class NpcHandling
    {
        // This enum contains each type of npc
        public enum NpcType
        {
            Queen,
            VentilationShaft,
            InformationPanel,
            Worker
        }
        
        public static void Load()
        {
            // A StreamReader stores the contents of the rooms JSON file in a string
            using StreamReader r = new StreamReader("npcData.json");
            var json = r.ReadToEnd();
            // I then use this string to parse the JSON using the Newtonsoft.Json package.
            // The variables in the JSON are mapped onto the NPCData class
            var npcData = JsonConvert.DeserializeObject<GameNpcData>(json);
            var roomIndexes = new List<int>();
            // I loop through each playable room and add it to an array (0 is the title screen and 1 is the start room so I skip them)
            for (var i = 2; i < 17; i++)
            {
                roomIndexes.Add(i);
            }
            
            // I shuffle the array so the NPCs are in random rooms each time
            Random rnd = new Random();
            roomIndexes = roomIndexes.ToArray().OrderBy(x => rnd.Next()).ToList();

            if (npcData == null) return;
            // I loop through each npc in the spawn stack, get its type and create a new npc using that type
            for (var i = 0; i < npcData.NpcSpawnStack.Length; i++)
            {
                var type = Enum.Parse<NpcType>(npcData.NpcSpawnStack[i]);
                GameNpcData.Data.Add(new Npc(type, roomIndexes[i]));
                Console.WriteLine($"{type} in room {roomIndexes[i]}");
            }
        }
        

        // This struct contains the data about the npc like what room it is in and the type of npc it is
        public struct Npc
        {
            public NpcType Type;
            public int RoomNumber;

            public Npc(NpcType type, int roomNumber)
            {
                Type = type;
                RoomNumber = roomNumber;
            }
        }
        
    }

    public class GameNpcData
    {
        public string[] NpcSpawnStack;
        public static List<NpcHandling.Npc> Data = new();
    }
}