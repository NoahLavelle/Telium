using System;
using Newtonsoft.Json.Linq;
using Telium.ConsoleFeatures;

namespace Telium.Objects {
    public class Door {
        public Door(JObject interactData)
        {
            Prompt.Select(new Room($"Rooms/{interactData["room"]}.json").RoomData);
        }
    }
}