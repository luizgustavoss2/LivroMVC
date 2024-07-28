using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Livros.Application.ResponseError;
using Livros.Application.UseCases;

namespace Livros.Presentation.API.UseCases.Assunto.Delete
{
    [Route("v{ver:apiVersion}/[controller]")]
    [ApiController]
    public class AssuntoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly DeleteAssuntoPresenter _deleteAssuntoPresenter;
        public AssuntoController(IMediator mediator, DeleteAssuntoPresenter deleteAssuntoPresenter)
        {
            _mediator = mediator;
            _deleteAssuntoPresenter = deleteAssuntoPresenter; 
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new AssuntoDeleteCommandRequest(codAs: id);
            var result = await _mediator.Send(request);
            return _deleteAssuntoPresenter.GetActionResult(result, null, HttpStatusCode.NoContent);
        }
    }
}
