using System;
using System.Collections.Generic;
using FluentValidation;
using MediatR;

namespace Livros.Application.UseCases
{
    public class LivroUpdateCommandRequest : RequestBase<LivroUpdateCommandRequest>, IRequest<LivroUpdateCommandResponse>
    {
        public LivroUpdateCommandRequest(int codL, string titulo, string editora, int? edicao, int anoPublicacao, int codAs, List<int> listCodAu)
        {
            CodL = codL;
            Titulo = titulo;
            Editora = editora;
            Edicao = edicao;
            AnoPublicacao = anoPublicacao;
            CodAs = codAs;
            ListCodAu = listCodAu;
            ValidateModel();
        }
        public int CodL { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int? Edicao { get; set; }
        public int AnoPublicacao { get; set; }
        public int CodAs { get; set; }
        public List<int> ListCodAu { get; set; }

        public List<LivroUpdatePrecoRequest> Precos { get; set; }

        private void ValidateModel()
        {
            RuleFor(x => x.CodL).NotEmpty().WithMessage("CodL is required.").WithState(x => x.Code = NotificationCode.FieldMissing);
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("Titulo is required.").WithState(x => x.Code = NotificationCode.FieldMissing);
            RuleFor(x => x.Editora).NotEmpty().WithMessage("Editora is required.").WithState(x => x.Code = NotificationCode.FieldMissing);
            RuleFor(x => x.Edicao).NotEmpty().WithMessage("Edicao is required.").WithState(x => x.Code = NotificationCode.FieldMissing);
            RuleFor(x => x.AnoPublicacao).NotEmpty().WithMessage("AnoPublicacao is required.").WithState(x => x.Code = NotificationCode.FieldMissing);

            ValidateModel(this);
        }

        public static explicit operator Domain.Entities.Livro(LivroUpdateCommandRequest request)
        {
            if (request is null)
                return null;

            return new Domain.Entities.Livro()
            {
               CodL = request.CodL,
               Titulo = request.Titulo,
               Editora = request.Editora,
               Edicao = request.Edicao,
               AnoPublicacao = request.AnoPublicacao
            };
        }
    }
}
