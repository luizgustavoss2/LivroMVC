using System;
using System.Collections.Generic;
using FluentValidation;
using MediatR;

namespace Livros.Application.UseCases
{
    public class LivroInsertCommandRequest : RequestBase<LivroInsertCommandRequest>, IRequest<LivroInsertCommandResponse>
    {
        public LivroInsertCommandRequest(string titulo, string editora, int? edicao, string anoPublicacao, int codAs, List<int> listCodAu)
        {
            Titulo = titulo;
            Editora = editora;
            Edicao = edicao;
            AnoPublicacao = anoPublicacao;
            CodAs = codAs;
            ListCodAu = listCodAu;
           
            ValidateModel();
        }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int? Edicao { get; set; }
        public string AnoPublicacao { get; set; }
        public int CodAs { get; set; }
        public List<int> ListCodAu { get; set; }
        public List<LivroInsertPrecoRequest> Precos { get; set; }

        private void ValidateModel()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("Titulo is required.").WithState(x => x.Code = NotificationCode.FieldMissing);
            RuleFor(x => x.Editora).NotEmpty().WithMessage("Editora is required.").WithState(x => x.Code = NotificationCode.FieldMissing);
            RuleFor(x => x.Edicao).NotEmpty().WithMessage("Edicao is required.").WithState(x => x.Code = NotificationCode.FieldMissing);
            RuleFor(x => x.AnoPublicacao).NotEmpty().WithMessage("AnoPublicacao is required.").WithState(x => x.Code = NotificationCode.FieldMissing);

            ValidateModel(this);
        }
    }
}
