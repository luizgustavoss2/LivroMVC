using System.Collections.Generic;
using Livros.Application.Notifications;
using Livros.Domain.Entities;
namespace Livros.Application.UseCases
{
    public class PrecoGetCommandResponse : ResponseBase
    {
        public IEnumerable<Preco> Preco { get; set; }
    }
}
