using Newtonsoft.Json.Linq;
using Telium.ConsoleFeatures;

namespace Telium.Objects
{
    public class StartButton
    {
        
        // This is the type used for the buttons in the title sequence
        public StartButton(JObject interactionData)
        {
            // If the buttons JSON specifies play, the first room will be loaded, initiating the game loop
            // Otherwise the instructions will be loaded
            if (interactionData["action"].ToString() == "play")
            {
                Prompt.LoadRoom(new Room("Rooms/roomOne.json").RoomData);
            }
        }
    }
}