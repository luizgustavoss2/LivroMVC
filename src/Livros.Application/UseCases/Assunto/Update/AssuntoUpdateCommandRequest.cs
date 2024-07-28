using System;
using FluentValidation;
using MediatR;

namespace Livros.Application.UseCases
{
    public class AssuntoUpdateCommandRequest : RequestBase<AssuntoUpdateCommandRequest>, IRequest<AssuntoUpdateCommandResponse>
    {
        public AssuntoUpdateCommandRequest(int codAs, string descricao)
        {
            CodAs = codAs;
            Descricao = descricao;
            ValidateModel();
        }
        public int CodAs { get; set; }
        public string Descricao { get; set; }

        private void ValidateModel()
        {
            RuleFor(x => x.CodAs).NotEmpty().WithMessage("CodAs is required.").WithState(x => x.Code = NotificationCode.FieldMissing);
            RuleFor(x => x.Descricao).NotEmpty().WithMessage("Descricao is required.").WithState(x => x.Code = NotificationCode.FieldMissing);

            ValidateModel(this);
        }

        public static explicit operator Domain.Entities.Assunto(AssuntoUpdateCommandRequest request)
        {
            if (request is null)
                return null;

            return new Domain.Entities.Assunto()
            {
               CodAs = request.CodAs,
               Descricao = request.Descricao
            };
        }
    }
}
