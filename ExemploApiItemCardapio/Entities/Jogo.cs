using System;

namespace ExemploApiItemCardapio.Entities
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public double Preco { get; set; }
    }
}
