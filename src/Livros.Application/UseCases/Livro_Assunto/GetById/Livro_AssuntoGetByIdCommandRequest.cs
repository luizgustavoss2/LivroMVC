using FluentValidation;
using MediatR;
using System;

namespace Livros.Application.UseCases
{
    public class Livro_AssuntoGetByIdCommandRequest : RequestBase<Livro_AssuntoGetByIdCommandRequest>, IRequest<Livro_AssuntoGetByIdCommandResponse>
    {
        public Livro_AssuntoGetByIdCommandRequest(int codL)
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
