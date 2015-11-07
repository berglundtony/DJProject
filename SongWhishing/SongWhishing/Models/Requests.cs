using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SongWishing.Models
{
    public class Requests
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDRequest { get; set; }
        public Nullable<int> IDSong { get; set; }
        public string Artist { get; set; }
        public string Song { get; set; }
        public string Avsändare { get; set; }
        public string SessionID { get; set; }

        //public virtual ICollection<SongsRequest> SongsRequests { get; set; }
    }
}