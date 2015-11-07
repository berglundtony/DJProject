using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SongWishing.Models
{
    public class Songs
    {
        [Key]
        [Column(Order = 0)]
        public int IDSong { get; set; }
        public string Artist { get; set; }
        public string Låtnamn { get; set; }
        public Nullable<short> BPM { get; set; }
        public string Genre_1 { get; set; }
        public string Genre_2 { get; set; }
        public string Skivnamn { get; set; }
        public Nullable<short> Utgivningsår { get; set; }
        public Nullable<System.DateTime> Importdatum { get; set; }
        public bool Omit { get; set; }

        //public virtual ICollection<SongsRequest> SongsRequests { get; set; }
    }



    //public class SongsDBContext : DbContext
    //{
    //    public DbSet<Songs> details { get; set; }
    //}

}