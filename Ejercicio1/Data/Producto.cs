using System.ComponentModel.DataAnnotations;

namespace Ejercicio1.Data
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int Quantify { get; set; }
        public bool State { get; set; }=true;
        [Required]
        public int CategoriaId { get; set; }
        public Categoria categorias { get; set; }

    }
}
