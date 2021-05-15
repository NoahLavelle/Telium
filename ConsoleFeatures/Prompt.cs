using System;

namespace Telium.ConsoleFeatures
{
    
    // Prompt acts as an easy way to run these functions. I could run them directly but this is more organised
    public static class Prompt
    {
        public static string Input(string message)
        {
            var input = new Input(message);
            return input.Answer;
        }

        public static void LoadRoom(RoomData roomData, Action postLoadInjection = null)
        {
            var select = new LoadRoom(roomData, postLoadInjection);
        }
    }
}