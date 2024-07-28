using System;
using FluentValidation;
using MediatR;

namespace Livros.Application.UseCases
{
    public class AutorUpdateCommandRequest : RequestBase<AutorUpdateCommandRequest>, IRequest<AutorUpdateCommandResponse>
    {
        public AutorUpdateCommandRequest(int codAu, string nome)
        {
            CodAu = codAu;
            Nome = nome;
            ValidateModel();
        }
        public int CodAu { get; set; }
        public string Nome { get; set; }

        private void ValidateModel()
        {
            RuleFor(x => x.CodAu).NotEmpty().WithMessage("CodAu is required.").WithState(x => x.Code = NotificationCode.FieldMissing);
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome is required.").WithState(x => x.Code = NotificationCode.FieldMissing);

            ValidateModel(this);
        }

        public static explicit operator Domain.Entities.Autor(AutorUpdateCommandRequest request)
        {
            if (request is null)
                return null;

            return new Domain.Entities.Autor()
            {
               CodAu = request.CodAu,
               Nome = request.Nome
            };
        }
    }
}
