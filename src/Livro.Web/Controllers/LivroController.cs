using Livro.Presentation.Web.Models;
using Livros.Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livro.Presentation.Web.Controllers
{
    public class LivroController : Controller
    {
        private readonly IMediator _mediator;

        public LivroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Create()
        {
            LoadViewBags();

            return View();
        }

        public IActionResult Index()
        {
            var lstLivroViewModel = new List<LivroViewModel>();

            var request = new LivroGetCommandRequest();
            var result = _mediator.Send(request).Result;

            if (result.Livro != null && result.Livro.Any())
            {
                foreach (var dto in result.Livro)
                {
                    var livro = new LivroViewModel()
                    {
                        CodL = dto.CodL.Value,
                        Titulo = dto.Titulo,
                        Edicao = dto.Edicao.Value,
                        Editora = dto.Editora,
                        AnoPublicacao = dto.AnoPublicacao,
                        Assunto = (dto.Assunto != null ? new AssuntoViewModel() { CodAs = dto.Assunto.CodAs.Value, Descricao = dto.Assunto.Descricao } : new AssuntoViewModel()),
                        Autores = new List<AutorViewModel>()
                    };

                    if (dto.Autor != null && dto.Autor.Any())
                    {
                        foreach (var autorDto in dto.Autor)
                        {
                            livro.Autores.Add(new AutorViewModel() { CodAu = autorDto.CodAu.Value, Nome = autorDto.Nome });
                        }
                    }

                    lstLivroViewModel.Add(livro);
                }
            }

            return View(lstLivroViewModel);
        }

        [HttpPost]
        public IActionResult Create(LivroViewModel livroViewModel)
        {
            LoadViewBags();

            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                var request = new LivroInsertCommandRequest(
                    titulo: livroViewModel.Titulo,
                    editora: livroViewModel.Editora,
                    edicao: livroViewModel.Edicao,
                    anoPublicacao: livroViewModel.AnoPublicacao,
                    codAs: livroViewModel.CodAs,
                    listCodAu: (livroViewModel.Autores != null ? livroViewModel.Autores?.Select(x => Convert.ToInt32(x.Nome.Split('-')[0])).ToList() : null));

                if (livroViewModel.Precos != null && livroViewModel.Precos.Any())
                {
                    request.Precos = new List<LivroInsertPrecoRequest>();
                    foreach (var preco in livroViewModel.Precos)
                    {
                        request.Precos.Add(new LivroInsertPrecoRequest(preco.Valor, preco.Tipo));
                    }
                }

                var result = _mediator.Send(request).Result;

                TempData[Constants.Message.SUCCESS] = "Livro criado com sucesso.";
            }
            catch (Exception ex)
            {
                TempData[Constants.Message.ERROR] = ex.Message;
            }
            return RedirectToAction("Index");
        }


        public IActionResult Update(int id)
        {
            LoadViewBags();
            var request = new LivroGetByIdCommandRequest(id);
            var result = _mediator.Send(request).Result;
            var livroViewModel = new LivroViewModel();

            if (result.Livro != null)
            {
                livroViewModel.CodL = result.Livro.CodL.Value;
                livroViewModel.Titulo = result.Livro.Titulo;
                livroViewModel.Editora = result.Livro.Editora;
                livroViewModel.Edicao = result.Livro.Edicao.Value;
                livroViewModel.AnoPublicacao = result.Livro.AnoPublicacao;
                livroViewModel.Assunto = new AssuntoViewModel();

                if (result.Livro.Assunto != null)
                {
                    livroViewModel.Assunto = new AssuntoViewModel() { CodAs = result.Livro.Assunto.CodAs.Value, Descricao = result.Livro.Assunto.Descricao };
                    livroViewModel.CodAs = result.Livro.Assunto.CodAs.Value;
                }

                livroViewModel.Autores = new List<AutorViewModel>();

                if (result.Livro.Autor != null && result.Livro.Autor.Any())
                {
                    foreach (var autor in result.Livro.Autor.OrderBy(x => x.Nome))
                    {
                        livroViewModel.Autores.Add(new AutorViewModel() { CodAu = autor.CodAu.Value, Nome = autor.Nome });
                    }
                }

                if(result.Livro.Preco != null && result.Livro.Preco.Any())
                {
                    foreach (var preco in result.Livro.Preco)
                    {
                        livroViewModel.Precos.Add(new PrecoViewModel() { Valor = preco.Valor.Value, Tipo = preco.Tipo.Value });
                    }
                }
            }
            return View(livroViewModel);
        }

        [HttpPost]
        public IActionResult Update(LivroViewModel livroViewModel)
        {
            LoadViewBags();
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                var request = new LivroUpdateCommandRequest(
                    codL: livroViewModel.CodL,
                    titulo: livroViewModel.Titulo,
                    editora: livroViewModel.Editora,
                    edicao: livroViewModel.Edicao,
                    anoPublicacao: livroViewModel.AnoPublicacao,
                    codAs: livroViewModel.CodAs,
                     listCodAu: (livroViewModel.Autores != null ? livroViewModel.Autores?.Select(x => Convert.ToInt32(x.Nome.Split('-')[0])).ToList() : null)
                );

                if (livroViewModel.Precos != null && livroViewModel.Precos.Any())
                {
                    request.Precos = new List<LivroUpdatePrecoRequest>();
                    foreach (var preco in livroViewModel.Precos)
                    {
                        request.Precos.Add(new LivroUpdatePrecoRequest(preco.Valor, preco.Tipo));
                    }
                }

                var result = _mediator.Send(request).Result;

                TempData[Constants.Message.SUCCESS] = "Livro atualizado com sucesso.";
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
                var request = new LivroDeleteCommandRequest(codL: id);
                var result = _mediator.Send(request).Result;
                TempData[Constants.Message.SUCCESS] = "Livro excluído com sucesso.";
            }
            catch (Exception ex)
            {
                TempData[Constants.Message.ERROR] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        private void LoadViewBags()
        {
            var requestAssunto = new AssuntoGetCommandRequest();
            var resultAssunto = _mediator.Send(requestAssunto).Result;
            ViewBag.Assuntos = resultAssunto.Assunto.ToList();

            var lstAutorViewModel = new List<AutorViewModel>();

            var requestAutor = new AutorGetCommandRequest();
            var resultAutor = _mediator.Send(requestAutor).Result;

            if (resultAutor.Autor != null && resultAutor.Autor.Any())
            {
                foreach (var aut in resultAutor.Autor)
                {
                    lstAutorViewModel.Add(new AutorViewModel()
                    {
                        CodAu = aut.CodAu.Value,
                        Nome = aut.Nome
                    }); ;
                }
            }

            ViewBag.Autores = lstAutorViewModel.ToList();

            ViewBag.Tipos = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Balcão" },
                new SelectListItem { Value = "2", Text = "Self-Service" },
                new SelectListItem { Value = "3", Text = "Internet" },
                new SelectListItem { Value = "4", Text = "Evento" }
            };

        }
    }
}
