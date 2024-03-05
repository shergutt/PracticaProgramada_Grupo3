using System.ComponentModel.DataAnnotations;

namespace PracticaProgramada_Grupo3.Models
{
    public class User
    {
        [Key]
        public int idUser { get; set; }
        public string Usuario { get; set; }
        public string password { get; set; }

    }
}


