using System.ComponentModel.DataAnnotations;

namespace PracticaProgramada_Grupo3.Models
{
    public class Tarea
    {
        [Key]
        public int id { get; set; }
        public string nombreTarea { get; set; }
    }
}
