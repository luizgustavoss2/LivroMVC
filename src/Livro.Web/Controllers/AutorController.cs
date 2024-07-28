using Livro.Presentation.Web.Models;
using Livros.Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Livro.Presentation.Web.Controllers
{
    public class AutorController : Controller
    {
        private readonly IMediator _mediator;

        public AutorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Index()
        {
            var lstAutorViewModel = new List<AutorViewModel>();

            var request = new AutorGetCommandRequest();
            var result = _mediator.Send(request).Result;

            if (result.Autor != null && result.Autor.Any())
            {
                foreach (var dto in result.Autor)
                {
                    lstAutorViewModel.Add(new AutorViewModel() { CodAu = dto.CodAu.Value, Nome = dto.Nome });
                }
            }

            return View(lstAutorViewModel);
        }

        [HttpPost]
        public IActionResult Create(AutorViewModel contatoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var request = new AutorInsertCommandRequest(
                    nome: contatoViewModel.Nome);

                var result = _mediator.Send(request).Result;

                TempData[Constants.Message.SUCCESS] = "Autor criado com sucesso.";
            }
            catch (Exception ex)
            {
                TempData[Constants.Message.ERROR] = ex.Message;
            }
            return RedirectToAction("Index");
        }


        public IActionResult Update(int id)
        {
            var request = new AutorGetByIdCommandRequest(id);
            var result = _mediator.Send(request).Result;
            var assuntoViewModel = new AutorViewModel();
            if (result.Autor != null)
            {
                assuntoViewModel.CodAu = result.Autor.CodAu.Value;
                assuntoViewModel.Nome = result.Autor.Nome;
            }
            return View(assuntoViewModel);
        }

        [HttpPost]
        public IActionResult Update(AutorViewModel assuntoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                var request = new AutorUpdateCommandRequest(
                    codAu: assuntoViewModel.CodAu,
                    nome: assuntoViewModel.Nome
                );

                var result = _mediator.Send(request).Result;

                TempData[Constants.Message.SUCCESS] = "Autor atualizado com sucesso.";
            }
            catch (Exception ex)
            {
                TempData[Constants.Message.ERROR] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var request = new AutorDeleteCommandRequest(id: id);
                var result = _mediator.Send(request).Result;
                TempData[Constants.Message.SUCCESS] = "Autor excluído com sucesso.";
            }
            catch (Exception ex)
            {
                TempData[Constants.Message.ERROR] = ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
