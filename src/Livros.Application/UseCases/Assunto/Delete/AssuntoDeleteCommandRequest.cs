using FluentValidation;
using MediatR;
using System;

namespace Livros.Application.UseCases
{
    public class AssuntoDeleteCommandRequest : RequestBase<AssuntoDeleteCommandRequest>, IRequest<AssuntoDeleteCommandResponse>
    {
        public AssuntoDeleteCommandRequest(int codAs)
        {
            CodAs = codAs;
            ValidateModel();
        }

        public int CodAs { get; set; }

        private void ValidateModel()
        {
            RuleFor(x => x.CodAs).NotEmpty().WithMessage("Code is required.").WithState(x => x.Code = NotificationCode.FieldMissing);
            ValidateModel(this);
        }
    }
}
