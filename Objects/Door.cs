using System;

namespace Telium.ConsoleFeatures {
    public class Door : InteractableObject {
            public Door() {
            OnInteraction += SwitchRoom;
            SelectText = "Door";
        }

        void SwitchRoom(object sender, EventArgs e) {
            Console.WriteLine(e.selectedObject["linkedScene"]);
        }
    }
}