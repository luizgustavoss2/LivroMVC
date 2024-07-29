using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Livros.Application.UseCases;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;
using Moq;
using Xunit;

namespace Livros.Application.Test
{
    [Trait("Livros","Usecases")]
    public class AssuntoHandlerTest
    {
        private readonly UseCases.AssuntoInsertCommandHandler _assuntoInsertCommandHandler;
        private readonly Mock<IRepositoryAssunto> _repositoryAssuntoMock = new Mock<IRepositoryAssunto>();
        private readonly Mock<AssuntoInsertCommandRequest> _assuntoInsertCommandRequest = new Mock<AssuntoInsertCommandRequest>();

        public AssuntoHandlerTest()
        {
            _assuntoInsertCommandHandler = new UseCases.AssuntoInsertCommandHandler(_repositoryAssuntoMock.Object);
        }

        [Fact]
        public async Task Create_Assunto_Sucess()
        {
            var request = new AssuntoInsertCommandRequest("Novo assunto");
            var id = 10;
            _repositoryAssuntoMock.Setup(repo => repo.CreateAsync(It.IsAny<Assunto>()))
                      .ReturnsAsync(id);

            var result = await _assuntoInsertCommandHandler.Handle(request, CancellationToken.None);

            // Assert
        
            Assert.Equal(10, result.CodAs);
        }

    }
}
