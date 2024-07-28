using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Livros.Infra.Data.Entities
{
    [Table("Livro_Assunto")]
    public class Livro_AssuntoPersistence : BaseEntity
    {
        public int? Livro_CodL { get; set; }
        public int? Assunto_CodAs { get; set; }

        public static implicit operator Livro_AssuntoPersistence(Domain.Entities.Livro_Assunto livro_Assunto)
        {
            if (livro_Assunto == null)
                return null;

            return new Livro_AssuntoPersistence()
            {
                Livro_CodL = livro_Assunto.Livro_CodL,
                Assunto_CodAs = livro_Assunto.Assunto_CodAs 
             };
        }

        public static implicit operator Domain.Entities.Livro_Assunto(Livro_AssuntoPersistence livro_Assunto)
        {
            if (livro_Assunto == null)
                return null;

            return new Domain.Entities.Livro_Assunto(
                livro_CodL: livro_Assunto.Livro_CodL, 
                assunto_CodAs: livro_Assunto.Assunto_CodAs
            );
        }
    }
}
