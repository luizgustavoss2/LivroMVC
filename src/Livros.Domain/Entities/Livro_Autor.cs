using System;
namespace Livros.Domain.Entities
{
    public class Livro_Autor : BaseEntity
    {
        public Livro_Autor()
        {
        }

        public Livro_Autor(int? livro_CodL, int? autor_CodAu)
        {
            Livro_CodL = livro_CodL;
            Autor_CodAu = autor_CodAu;
        }
        public int? Livro_CodL { get; set; }
        public int? Autor_CodAu { get; set; }

    }
}
