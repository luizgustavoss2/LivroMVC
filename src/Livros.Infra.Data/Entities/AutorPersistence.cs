using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Livros.Infra.Data.Entities
{
    [Table("Autor")]
    public class AutorPersistence : BaseEntity
    {
        public int? CodAu { get; set; }
        public string Nome { get; set; }

        public static implicit operator AutorPersistence(Domain.Entities.Autor autor)
        {
            if (autor == null)
                return null;

            return new AutorPersistence()
            {
                CodAu = autor.CodAu,
                Nome = autor.Nome 
             };
        }

        public static implicit operator Domain.Entities.Autor(AutorPersistence autor)
        {
            if (autor == null)
                return null;

            return new Domain.Entities.Autor(
                codAu: autor.CodAu, 
                nome: autor.Nome
            );
        }
    }
}
