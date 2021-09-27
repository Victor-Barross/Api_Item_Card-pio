using System.ComponentModel.DataAnnotations;

namespace ExemploApiItemCardapio.InputModel
{
    public class ItemInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do item no cardápio deve conter entre 3 e 100 caracteres")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome da categoria no cardápio deve conter entre 3 e 100 caracteres")]
        public string Categoria { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "O preço deve ser de no mínimo 1 real e no máximo 1000 reais")]
        public double Preco { get; set; }
    }
}
