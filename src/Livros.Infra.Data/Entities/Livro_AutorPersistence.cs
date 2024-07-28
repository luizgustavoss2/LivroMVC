using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Livros.Infra.Data.Entities
{
    [Table("Livro_Autor")]
    public class Livro_AutorPersistence : BaseEntity
    {
        public int? Livro_CodL { get; set; }
        public int? Autor_CodAu { get; set; }

        public static implicit operator Livro_AutorPersistence(Domain.Entities.Livro_Autor livro_Autor)
        {
            if (livro_Autor == null)
                return null;

            return new Livro_AutorPersistence()
            {
                Livro_CodL = livro_Autor.Livro_CodL,
                Autor_CodAu = livro_Autor.Autor_CodAu 
             };
        }

        public static implicit operator Domain.Entities.Livro_Autor(Livro_AutorPersistence livro_Autor)
        {
            if (livro_Autor == null)
                return null;

            return new Domain.Entities.Livro_Autor(
                livro_CodL: livro_Autor.Livro_CodL, 
                autor_CodAu: livro_Autor.Autor_CodAu
            );
        }
    }
}
