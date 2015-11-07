using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace SongWishing.Models
{
    public class MessageViewModel
    {
        public List<Request> allRequests { get; set; }
        public Request Request { get; set; }
        public string Artist { get; set; }
        public string Låt { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string Avsändare { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int? IDRequest { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string SessionID { get; set; }

        public string Message { get; set; }

        // Text message propery
        public int Id { get; set; }
        public string Text { get; set; }

        public string DisplayName
        {
            get { return Text; }
        }

        [DisplayName("Text meddelande:")]
        public bool WillPlayBoolProperty { get; set; }
        [DisplayName("Text meddelande:")]
        public bool WillNotPlayBoolProperty { get; set; }

        private string playText;
        public string PlayText
        {
            get
            {
                return playText ?? (playText = "Välj vilken text som passar för den här låten. Alternativt skriv din egen text.");
            }
            set
            {
                { playText = value; }
            }
        }

    }
}