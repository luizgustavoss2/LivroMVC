using System;
using FluentValidation;
using MediatR;

namespace Livros.Application.UseCases
{
    public class Livro_AutorInsertCommandRequest : RequestBase<Livro_AutorInsertCommandRequest>, IRequest<Livro_AutorInsertCommandResponse>
    {
        public Livro_AutorInsertCommandRequest(int? livro_CodL, int? autor_CodAu)
        {
            Livro_CodL = livro_CodL;
            Autor_CodAu = autor_CodAu;
            ValidateModel();
        }
        public int? Livro_CodL { get; set; }
        public int? Autor_CodAu { get; set; }

        private void ValidateModel()
        {
            RuleFor(x => x.Livro_CodL).NotEmpty().WithMessage("Livro_CodL is required.").WithState(x => x.Code = NotificationCode.FieldMissing);
            RuleFor(x => x.Autor_CodAu).NotEmpty().WithMessage("Autor_CodAu is required.").WithState(x => x.Code = NotificationCode.FieldMissing);

            ValidateModel(this);
        }
    }
}
