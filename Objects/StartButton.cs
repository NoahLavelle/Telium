using Newtonsoft.Json.Linq;
using Telium.ConsoleFeatures;

namespace Telium.Objects
{
    public class StartButton
    {
        
        // This is the type used for the buttons in the title sequence
        public StartButton(JObject interactionData)
        {
            // We get the desired action from the interact data, and deal with it in the switch statement
            var action = interactionData["action"].ToString();
            switch (action)
            {
                case "play":
                    // If play is chosen the npcs are loaded and the new room is loaded
                    NpcHandling.Load();
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