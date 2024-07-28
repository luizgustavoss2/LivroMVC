using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Livros.Infra.Data.Entities
{
    [Table("Preco")]
    public class PrecoPersistence : BaseEntity
    {
        public int? CodPr { get; set; }
        public int? Livro_CodL { get; set; }
        public decimal? Valor { get; set; }
        public int? Tipo { get; set; }

        public static implicit operator PrecoPersistence(Domain.Entities.Preco preco)
        {
            if (preco == null)
                return null;

            return new PrecoPersistence()
            {
                CodPr = preco.CodPr,
                Livro_CodL = preco.Livro_CodL,
                Valor = preco.Valor,
                Tipo = preco.Tipo 
             };
        }

        public static implicit operator Domain.Entities.Preco(PrecoPersistence preco)
        {
            if (preco == null)
                return null;

            return new Domain.Entities.Preco(
                codPr: preco.CodPr, 
                livro_CodL: preco.Livro_CodL, 
                valor: preco.Valor, 
                tipo: preco.Tipo
            );
        }
    }
}
