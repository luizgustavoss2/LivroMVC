using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Livros.Application.ResponseError;
using Livros.Application.UseCases;

namespace Livros.Presentation.API.UseCases.Livro.Insert
{
    [Route("v{ver:apiVersion}/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly InsertLivroPresenter _insertLivroPresenter;
        public LivroController(IMediator mediator, InsertLivroPresenter insertLivroPresenter)
        {
            _mediator = mediator;
            _insertLivroPresenter = insertLivroPresenter; 
        }

        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Post(InsertLivroRequest insertRequest)
        {
            var request = new  LivroInsertCommandRequest(
              titulo: insertRequest.Titulo,
              editora: insertRequest.Editora,
              edicao: insertRequest.Edicao,
              anoPublicacao: insertRequest.AnoPublicacao,
              codAs: insertRequest.CodAs,
              listCodAu: insertRequest.ListCodAu
            );
            var result = await _mediator.Send(request);
            return _insertLivroPresenter.GetActionResult(result, result.CodL, HttpStatusCode.Created);
        }
    }
}
