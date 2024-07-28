using System;
using FluentValidation;
using MediatR;

namespace Livros.Application.UseCases
{
    public class Livro_AutorUpdateCommandRequest : RequestBase<Livro_AutorUpdateCommandRequest>, IRequest<Livro_AutorUpdateCommandResponse>
    {
        public Livro_AutorUpdateCommandRequest(int? livro_CodL, int? autor_CodAu)
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

        public static explicit operator Domain.Entities.Livro_Autor(Livro_AutorUpdateCommandRequest request)
        {
            if (request is null)
                return null;

            return new Domain.Entities.Livro_Autor()
            {
               Livro_CodL = request.Livro_CodL,
               Autor_CodAu = request.Autor_CodAu
            };
        }
    }
}
