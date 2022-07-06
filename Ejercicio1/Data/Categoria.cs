using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ejercicio1.Data
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        [Required]
        public string Name { get; set; }
        public bool State { get; set; } = true;
        public virtual ICollection<Producto> productos { get; set; }

    }
}
