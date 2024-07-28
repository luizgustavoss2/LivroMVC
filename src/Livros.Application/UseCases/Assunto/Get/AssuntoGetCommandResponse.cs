using System.Collections.Generic;
using Livros.Application.Notifications;
using Livros.Domain.Entities;
namespace Livros.Application.UseCases
{
    public class AssuntoGetCommandResponse : ResponseBase
    {
        public IEnumerable<Assunto> Assunto { get; set; }
    }
}
