using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Livros.Infra.Data.Entities
{
    [Table("Livro")]
    public class LivroPersistence : BaseEntity
    {
        public int? CodL { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int? Edicao { get; set; }
        public string AnoPublicacao { get; set; }

        public static implicit operator LivroPersistence(Domain.Entities.Livro livro)
        {
            if (livro == null)
                return null;

            return new LivroPersistence()
            {
                CodL = livro.CodL,
                Titulo = livro.Titulo,
                Editora = livro.Editora,
                Edicao = livro.Edicao,
                AnoPublicacao = livro.AnoPublicacao 
             };
        }

        public static implicit operator Domain.Entities.Livro(LivroPersistence livro)
        {
            if (livro == null)
                return null;

            return new Domain.Entities.Livro(
                codL: livro.CodL, 
                titulo: livro.Titulo, 
                editora: livro.Editora, 
                edicao: livro.Edicao, 
                anoPublicacao: livro.AnoPublicacao
            );
        }
    }
}
