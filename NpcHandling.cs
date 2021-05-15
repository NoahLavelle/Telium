using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Telium
{
    public static class NpcHandling
    {
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
            for (var i = 2; i < 17; i++)
            {
                roomIndexes.Add(i);
            }
            
            // I shuffle the array so the NPCs are in random rooms each time
            Random rnd = new Random();
            roomIndexes = roomIndexes.ToArray().OrderBy(x => rnd.Next()).ToList();

            if (npcData == null) return;
            for (var i = 0; i < npcData.NpcSpawnStack.Length; i++)
            {
                var type = Enum.Parse<NpcType>(npcData.NpcSpawnStack[i]);
                GameNpcData.Data.Add(new Npc(type, roomIndexes[i]));
                Console.WriteLine($"{type} in room {roomIndexes[i]}");
            }
        }
        
        public class Npc
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