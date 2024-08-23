using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Livros.Application.ResponseError;
using Livros.Application.UseCases;

namespace Livros.Presentation.API.UseCases.Assunto.Insert
{
    [Route("v{ver:apiVersion}/[controller]")]
    [ApiController]
    public class AssuntoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly InsertAssuntoPresenter _insertAssuntoPresenter;
        public AssuntoController(IMediator mediator, InsertAssuntoPresenter insertAssuntoPresenter)
        {
            _mediator = mediator;
            _insertAssuntoPresenter = insertAssuntoPresenter; 
        }

        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Post(InsertAssuntoRequest insertRequest)
        {
            var request = new  AssuntoInsertCommandRequest(
              descricao: insertRequest.Descricao
            );
            var result = await _mediator.Send(request);
            return _insertAssuntoPresenter.GetActionResult(result, result.CodAs, HttpStatusCode.Created);
        }
    }
}
