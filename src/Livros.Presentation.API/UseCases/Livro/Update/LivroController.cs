using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Livros.Application.ResponseError;
using Livros.Application.UseCases;

namespace Livros.Presentation.API.UseCases.Livro.Update
{
    [Route("v{ver:apiVersion}/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UpdateLivroPresenter _updateLivroPresenter;
        public LivroController(IMediator mediator, UpdateLivroPresenter updateLivroPresenter)
        {
            _mediator = mediator;
            _updateLivroPresenter = updateLivroPresenter; 
        }

        [HttpPatch("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Patch(Guid id,[FromBody]UpdateLivroRequest updateRequest)
        {
            var request = new LivroUpdateCommandRequest(
              codL: updateRequest.CodL,
              titulo: updateRequest.Titulo,
              editora: updateRequest.Editora,
              edicao: updateRequest.Edicao,
              anoPublicacao: updateRequest.AnoPublicacao,
              codAs: updateRequest.CodAs,
              listCodAu: updateRequest.ListCodAu
            );
            var result = await _mediator.Send(request);
            return _updateLivroPresenter.GetActionResult(result, result.CodL, HttpStatusCode.NoContent);
        }
    }
}
