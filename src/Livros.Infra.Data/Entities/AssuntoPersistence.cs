using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Livros.Infra.Data.Entities
{
    [Table("Assunto")]
    public class AssuntoPersistence : BaseEntity
    {
        public int? CodAs { get; set; }
        public string Descricao { get; set; }

        public static implicit operator AssuntoPersistence(Domain.Entities.Assunto assunto)
        {
            if (assunto == null)
                return null;

            return new AssuntoPersistence()
            {
                CodAs = assunto.CodAs,
                Descricao = assunto.Descricao 
             };
        }

        public static implicit operator Domain.Entities.Assunto(AssuntoPersistence assunto)
        {
            if (assunto == null)
                return null;

            return new Domain.Entities.Assunto(
                codAs: assunto.CodAs, 
                descricao: assunto.Descricao
            );
        }
    }
}
