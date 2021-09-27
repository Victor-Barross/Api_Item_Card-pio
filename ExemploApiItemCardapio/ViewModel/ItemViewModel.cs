using System;

namespace ExemploApiItemCardapio.ViewModel
{
    public class ItemViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public double Preco { get; set; }
    }
}
