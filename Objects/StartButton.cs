using Newtonsoft.Json.Linq;
using Telium.ConsoleFeatures;

namespace Telium.Objects
{
    public class StartButton
    {
        
        // This is the type used for the buttons in the title sequence
        public StartButton(JObject interactionData)
        {
            // If the buttons JSON specifies play, the first room and NPCs will be loaded, initiating the game loop
            // Otherwise the instructions will be loaded
            var action = interactionData["action"].ToString();
            switch (action)
            {
                case "play":
                    NpcHandling.Load();;
                    Prompt.LoadRoom(new Room("Rooms/roomOne.json").RoomData);
                    break;
                case "instructions":
                    break;
                case "quit":
                    System.Environment.Exit(0);
                    break;
            }
        }
    }
}