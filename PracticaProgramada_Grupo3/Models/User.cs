using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaProgramada_Grupo3.Models
{
    public class User
    {
        [Key]
        public int idUser { get; set; }

        [Required]
        public string Usuario { get; set; }
        [Required]
        public string password { get; set; }
        public string fotoURL { get; set; }

        [NotMapped]
        public IFormFile ProfileImage { get; set; }
    }
}


