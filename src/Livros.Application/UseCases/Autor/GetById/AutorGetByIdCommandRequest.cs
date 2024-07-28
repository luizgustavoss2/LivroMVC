using FluentValidation;
using MediatR;
using System;

namespace Livros.Application.UseCases
{
    public class AutorGetByIdCommandRequest : RequestBase<AutorGetByIdCommandRequest>, IRequest<AutorGetByIdCommandResponse>
    {
        public AutorGetByIdCommandRequest(int id)
        {
            Id = id;
            ValidateModel();
        }

        public int Id { get; set; }

        private void ValidateModel()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Code is required.").WithState(x => x.Code = NotificationCode.FieldMissing);
            ValidateModel(this);
        }
    }
}
