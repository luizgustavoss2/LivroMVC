using System.Collections.Generic;
using Livros.Application.Notifications;
using Livros.Domain.Entities;
namespace Livros.Application.UseCases
{
    public class Livro_AssuntoGetCommandResponse : ResponseBase
    {
        public IEnumerable<Livro_Assunto> Livro_Assunto { get; set; }
    }
}
