using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Livros.Application.ResponseError;
using Livros.Application.UseCases;

namespace Livros.Presentation.API.UseCases.Autor.Delete
{
    [Route("v{ver:apiVersion}/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly DeleteAutorPresenter _deleteAutorPresenter;
        public AutorController(IMediator mediator, DeleteAutorPresenter deleteAutorPresenter)
        {
            _mediator = mediator;
            _deleteAutorPresenter = deleteAutorPresenter; 
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new AutorDeleteCommandRequest(id: id);
            var result = await _mediator.Send(request);
            return _deleteAutorPresenter.GetActionResult(result, null, HttpStatusCode.NoContent);
        }
    }
}
