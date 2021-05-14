using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Telium
{
    public static class NpcHandling
    {
        public static GameNPCData NpcData;
        
        public static void Load()
        {
            // A StreamReader stores the contents of the rooms JSON file in a string
            using StreamReader r = new StreamReader("npcData.json");
            var json = r.ReadToEnd();
            // I then use this string to parse the JSON using the Newtonsoft.Json package.
            // The variables in the JSON are mapped onto the NPCData class
            NpcData = JsonConvert.DeserializeObject<GameNPCData>(json);
            var roomIndexes = new List<int>();
            for (var i = 0; i < 17; i++)
            {
                roomIndexes.Add(i);
            }
            
            // I shuffle the array so the NPCs are in random rooms each time
            Random rnd=new Random();
            roomIndexes = roomIndexes.ToArray().OrderBy(x => rnd.Next()).ToList();
            var overallIndex = 0;

            // Here each type of NPC is looped through, and are assigned a random room index
            if (NpcData == null) return;
            for (var i = 0; i < NpcData.QueenCount; i++)
            {
                overallIndex++;
                NpcData.QueenLocations.Add(roomIndexes[overallIndex]);
            }

            for (var i = 0; i < NpcData.VentilationShaftCount; i++)
            {
                overallIndex++;
                NpcData.VentilationShaftLocations.Add(roomIndexes[overallIndex]);
            }

            for (var i = 0; i < NpcData.InformationPanelCount; i++)
            {
                overallIndex++;
                NpcData.InformationPanelLocations.Add(roomIndexes[overallIndex]);
            }

            for (var i = 0; i < NpcData.WorkerCount; i++)
            {
                overallIndex++;
                NpcData.WorkerLocations.Add(roomIndexes[overallIndex]);
            }
            
            // For the purposes of development the NPCs room numbers are shown here
            Console.WriteLine($"Queen Locations: {string.Join(",", NpcData.QueenLocations)}");
            Console.WriteLine($"Ventilation Shaft Locations: {string.Join(",", NpcData.VentilationShaftLocations)}");
            Console.WriteLine($"Information Panel Locations: {string.Join(",", NpcData.InformationPanelLocations)}");
            Console.WriteLine($"Worker Locations: {string.Join(",", NpcData.WorkerLocations)}");
        }
    }

    public class GameNPCData
    {
        public int QueenCount;
        public int VentilationShaftCount;
        public int InformationPanelCount;
        public int WorkerCount;

        public readonly List<int> QueenLocations = new List<int>();
        public readonly List<int> VentilationShaftLocations = new List<int>();
        public readonly List<int> InformationPanelLocations = new List<int>();
        public readonly List<int> WorkerLocations = new List<int>();
    }
}