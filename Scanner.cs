using System;
using System.Linq;
using Telium.ConsoleFeatures;

namespace Telium {
    public class Scanner {
        public static void UseScanner() {
            var roomData = new Room("Rooms/scannerSelect.json").RoomData;
            foreach (var option in roomData.Objects) 
            {
                option["interactData"]["lastModule"] = 5;
            }

            Prompt.LoadRoom(roomData);
        }

        public static void Lock() {
            try
            {
                var module = int.Parse(Prompt.Input("Enter a module to lock"));
            }
            catch
            {
                Lock();
                return;
            }

            try 
            {
                var queen = GameNpcData.Data.Where(x => x.RoomNumber == module && x.Type == NpcHandling.NpcType.Queen);
                Console.WriteLine("The Queen is in that module. You cannot lock it");
                Lock();
                return;
            }
            catch {
                Console.WriteLine("Module has been locked");
            }
            
        }
    }
}