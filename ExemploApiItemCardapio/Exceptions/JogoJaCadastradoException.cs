using System;

namespace ExemploApiItemCardapio.Exceptions
{
    public class ItemJaCadastradoException : Exception
    {
        public ItemJaCadastradoException()
            : base("Este item já está cadastrado no cardápio")
        { }
    }
}
