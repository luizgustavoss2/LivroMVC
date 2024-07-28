using System;
using FluentValidation;
using MediatR;

namespace Livros.Application.UseCases
{
    public class AutorInsertCommandRequest : RequestBase<AutorInsertCommandRequest>, IRequest<AutorInsertCommandResponse>
    {
        public AutorInsertCommandRequest(string nome)
        {
            Nome = nome;
            ValidateModel();
        }
        public string Nome { get; set; }

        private void ValidateModel()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome is required.").WithState(x => x.Code = NotificationCode.FieldMissing);

            ValidateModel(this);
        }
    }
}
