using FluentValidation;
using MediatR;
using System;

namespace Livros.Application.UseCases
{
    public class Livro_AutorGetByIdCommandRequest : RequestBase<Livro_AutorGetByIdCommandRequest>, IRequest<Livro_AutorGetByIdCommandResponse>
    {
        public Livro_AutorGetByIdCommandRequest(int codL)
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
