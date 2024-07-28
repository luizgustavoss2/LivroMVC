using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Livros.Application.ResponseError;
using Livros.Application.UseCases;

namespace Livros.Presentation.API.UseCases.Autor.Update
{
    [Route("v{ver:apiVersion}/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UpdateAutorPresenter _updateAutorPresenter;
        public AutorController(IMediator mediator, UpdateAutorPresenter updateAutorPresenter)
        {
            _mediator = mediator;
            _updateAutorPresenter = updateAutorPresenter; 
        }

        [HttpPatch("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ApiError), (int)HttpStatusCode.BadRequest)]
        [Produces("application/json")]
        public async Task<IActionResult> Patch(int id,[FromBody]UpdateAutorRequest updateRequest)
        {
            var request = new AutorUpdateCommandRequest(
              codAu: id,
              nome: updateRequest.Nome
            );
            var result = await _mediator.Send(request);
            return _updateAutorPresenter.GetActionResult(result, result.CodAu, HttpStatusCode.NoContent);
        }
    }
}
