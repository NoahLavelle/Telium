using Newtonsoft.Json.Linq;
using Telium.ConsoleFeatures;

namespace Telium.Objects
{
    public class StartButton
    {
        public StartButton(JObject interactionData)
        {
            if (interactionData["action"].ToString() == "play")
            {
                Prompt.Select(new Room("Rooms/roomOne.json").RoomData);
            }
        }
    }
}