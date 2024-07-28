using FluentValidation;
using MediatR;
using System;

namespace Livros.Application.UseCases
{
    public class Livro_AssuntoDeleteCommandRequest : RequestBase<Livro_AssuntoDeleteCommandRequest>, IRequest<Livro_AssuntoDeleteCommandResponse>
    {
        public Livro_AssuntoDeleteCommandRequest(int codL)
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
