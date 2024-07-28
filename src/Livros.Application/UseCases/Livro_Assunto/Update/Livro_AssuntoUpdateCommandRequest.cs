using System;
using FluentValidation;
using MediatR;

namespace Livros.Application.UseCases
{
    public class Livro_AssuntoUpdateCommandRequest : RequestBase<Livro_AssuntoUpdateCommandRequest>, IRequest<Livro_AssuntoUpdateCommandResponse>
    {
        public Livro_AssuntoUpdateCommandRequest(int? livro_CodL, int? assunto_CodAs)
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

        public static explicit operator Domain.Entities.Livro_Assunto(Livro_AssuntoUpdateCommandRequest request)
        {
            if (request is null)
                return null;

            return new Domain.Entities.Livro_Assunto()
            {
               Livro_CodL = request.Livro_CodL,
               Assunto_CodAs = request.Assunto_CodAs
            };
        }
    }
}
