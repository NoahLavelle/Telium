namespace Telium {
    public class EventArgs {
        public Newtonsoft.Json.Linq.JObject selectedObject;

        public EventArgs(Newtonsoft.Json.Linq.JObject selectedObject) {
            this.selectedObject = selectedObject;    
        }
    }

    public class InteractableObject
    {
        public string SelectText { get; set; }
        
        public delegate void EventHandler(object sender, EventArgs e);
        public EventHandler OnInteraction;
    }
}