using System;
namespace Livros.Domain.Entities
{
    public class Livro_Assunto 
    {
        public Livro_Assunto()
        {
        }

        public Livro_Assunto(int? livro_CodL, int? assunto_CodAs)
        {
            Livro_CodL = livro_CodL;
            Assunto_CodAs = assunto_CodAs;
        }
        public int? Livro_CodL { get; set; }
        public int? Assunto_CodAs { get; set; }

    }
}
