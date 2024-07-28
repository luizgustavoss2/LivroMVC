using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Livros.Application.ResponseError;
using Livros.Application.UseCases;

namespace Livros.Presentation.API.UseCases.Livro.Delete
{
    [Route("v{ver:apiVersion}/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly DeleteLivroPresenter _deleteLivroPresenter;
        public LivroController(IMediator mediator, DeleteLivroPresenter deleteLivroPresenter)
        {
            _mediator = mediator;
            _deleteLivroPresenter = deleteLivroPresenter; 
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new LivroDeleteCommandRequest(codL: id);
            var result = await _mediator.Send(request);
            return _deleteLivroPresenter.GetActionResult(result, null, HttpStatusCode.NoContent);
        }
    }
}
