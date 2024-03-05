
using System.ComponentModel.DataAnnotations;

namespace PracticaProgramada_Grupo3.Models
{

    public class Game
    {
        [Key]
        public int GameId { get; set; }
        [Required]
        public int UserId { get; set; }
        public string Result { get; set; } 
        public TimeSpan GameDuration { get; set; }
        public DateTime DatePlayed { get; set; } = DateTime.Now;
    }

}
