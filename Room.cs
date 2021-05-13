using System.IO; 
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Telium {
    internal class Room {
        public readonly RoomData RoomData;

        public Room(string jsonPath)
        {
            using StreamReader r = new StreamReader(jsonPath);
            var json = r.ReadToEnd();
            RoomData = JsonConvert.DeserializeObject<RoomData>(json);
        }
    }

    public class RoomData {
        public string Name;
        public string Description;
        public bool OverrideEntryText = false;
        public JObject[] Objects;
    }
}