namespace Telium.ConsoleFeatures
{
    public static class Prompt
    {
        public static string Input(string message)
        {
            var input = new Input(message);
            return input.Answer;
        }

        public static void Select(RoomData roomData)
        {
            var select = new Select(roomData);
        }
    }
}