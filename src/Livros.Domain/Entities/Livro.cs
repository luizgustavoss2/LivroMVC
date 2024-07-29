using System;
using System.Collections.Generic;

namespace Livros.Domain.Entities
{
    public class Livro
    {
        public Livro()
        {
        }

        public Livro(int? codL, string titulo, string editora, int? edicao, int anoPublicacao)
        {
            CodL = codL;
            Titulo = titulo;
            Editora = editora;
            Edicao = edicao;
            AnoPublicacao = anoPublicacao;
        }
        public int? CodL { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int? Edicao { get; set; }
        public int AnoPublicacao { get; set; }

        public virtual Assunto Assunto { get; set; }
        public virtual IEnumerable<Autor> Autor { get; set; }
        public virtual IEnumerable<Preco> Preco { get; set; }

    }
}
