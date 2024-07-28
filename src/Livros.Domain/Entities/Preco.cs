using System;
namespace Livros.Domain.Entities
{
    public class Preco
    {
        public Preco()
        {
        }

        public Preco(int? codPr, int? livro_CodL, decimal? valor, int? tipo)
        {
            CodPr = codPr;
            Livro_CodL = livro_CodL;
            Valor = valor;
            Tipo = tipo;
        }
        public int? CodPr { get; set; }
        public int? Livro_CodL { get; set; }
        public decimal? Valor { get; set; }
        public int? Tipo { get; set; }

    }
}
