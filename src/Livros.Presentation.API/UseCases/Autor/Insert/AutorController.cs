using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Livros.Application.ResponseError;
using Livros.Application.UseCases;

namespace Livros.Presentation.API.UseCases.Autor.Insert
{
    [Route("v{ver:apiVersion}/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly InsertAutorPresenter _insertAutorPresenter;
        public AutorController(IMediator mediator, InsertAutorPresenter insertAutorPresenter)
        {
            _mediator = mediator;
            _insertAutorPresenter = insertAutorPresenter; 
        }

        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Post(InsertAutorRequest insertRequest)
        {
            var request = new  AutorInsertCommandRequest(
              nome: insertRequest.Nome
            );
            var result = await _mediator.Send(request);
            return _insertAutorPresenter.GetActionResult(result, result.CodAu, HttpStatusCode.Created);
        }
    }
}
