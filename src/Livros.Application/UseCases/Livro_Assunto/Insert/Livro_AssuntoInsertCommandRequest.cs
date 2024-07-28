using System;
using FluentValidation;
using MediatR;

namespace Livros.Application.UseCases
{
    public class Livro_AssuntoInsertCommandRequest : RequestBase<Livro_AssuntoInsertCommandRequest>, IRequest<Livro_AssuntoInsertCommandResponse>
    {
        public Livro_AssuntoInsertCommandRequest(int? livro_CodL, int? assunto_CodAs)
        {
            Livro_CodL = livro_CodL;
            Assunto_CodAs = assunto_CodAs;
            ValidateModel();
        }
        public int? Livro_CodL { get; set; }
        public int? Assunto_CodAs { get; set; }

        private void ValidateModel()
        {
            RuleFor(x => x.Livro_CodL).NotEmpty().WithMessage("Livro_CodL is required.").WithState(x => x.Code = NotificationCode.FieldMissing);
            RuleFor(x => x.Assunto_CodAs).NotEmpty().WithMessage("Assunto_CodAs is required.").WithState(x => x.Code = NotificationCode.FieldMissing);

            ValidateModel(this);
        }
    }
}
