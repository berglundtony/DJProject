using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SongWishing.Models
{
    public class SearchArtistViewModel
    {
        public string Artist { get; set; }
        public int SongId { get; set; }
        [Required(ErrorMessage = "Välj en låt eller skriv den låt du önskar")]
        [DisplayName("Låt")]
        public string Song { get; set; }
    }
}
