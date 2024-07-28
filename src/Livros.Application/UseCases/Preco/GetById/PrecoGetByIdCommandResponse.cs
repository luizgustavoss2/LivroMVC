using Livros.Application.Notifications;
using Livros.Domain.Entities;
using System.Collections.Generic;

namespace Livros.Application.UseCases
{
    public class PrecoGetByIdCommandResponse : ResponseBase
    {
         public IEnumerable<Preco> Preco { get; set; }
    }
}
