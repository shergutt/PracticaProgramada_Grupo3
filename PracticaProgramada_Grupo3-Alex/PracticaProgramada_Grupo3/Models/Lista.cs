using System.ComponentModel.DataAnnotations;

namespace PracticaProgramada_Grupo3.Models
{
    public class Lista
    {
        [Key]
        public int idLista { get; set; }
        public string nombreLista { get; set; }
    }
}
