using System;
namespace Livros.Domain.Entities
{
    public class Assunto
    {
        public Assunto()
        {
        }

        public Assunto(int? codAs, string descricao)
        {
            CodAs = codAs;
            Descricao = descricao;
        }
        public int? CodAs { get; set; }
        public string Descricao { get; set; }

    }
}
