using System.IO; 
using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telium.Objects;

namespace Telium {
    internal class Room {
        public readonly RoomData RoomData;

        public Room(string jsonPath)
        {
            // A StreamReader stores the contents of the rooms JSON file in a string
            using StreamReader r = new StreamReader(jsonPath);
            var json = r.ReadToEnd();
            // I then use this string to parse the JSON using the Newtonsoft.Json package.
            // The variables in the JSON are mapped onto the RoomData class
            RoomData = JsonConvert.DeserializeObject<RoomData>(json);
        }
    }

    public class RoomData {
        public string Name;
        public string Description;
        public int RoomNumber;
        public bool OverrideEntryText = false;
        public JObject[] Objects;
    }
}