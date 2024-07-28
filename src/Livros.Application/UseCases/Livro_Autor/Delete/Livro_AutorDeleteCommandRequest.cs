using FluentValidation;
using MediatR;
using System;

namespace Livros.Application.UseCases
{
    public class Livro_AutorDeleteCommandRequest : RequestBase<Livro_AutorDeleteCommandRequest>, IRequest<Livro_AutorDeleteCommandResponse>
    {
        public Livro_AutorDeleteCommandRequest(int codL)
        {
            CodL = codL;
            ValidateModel();
        }

        public int CodL { get; set; }

        private void ValidateModel()
        {
             ValidateModel(this);
        }
    }
}
