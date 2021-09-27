using System;

namespace ExemploApiItemCardapio.Exceptions
{
    public class ItemNaoCadastradoException: Exception
    {
        public ItemNaoCadastradoException()
            :base("Este item não está cadastrado no cardápio")
        {}
    }
}
