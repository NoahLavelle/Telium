using System.IO; 
using System;
using Newtonsoft.Json;

namespace Telium {
    class Room {
        public RoomData roomData;

        public Room(string jsonPath) {
            using (StreamReader r = new StreamReader(jsonPath)) {
                string json = r.ReadToEnd();
                roomData = JsonConvert.DeserializeObject<RoomData>(json);
            }
        }
    }

    public class RoomData {
        public string name;
        public Newtonsoft.Json.Linq.JObject[] doors;
    }
}