using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Livros.Application.UseCases;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;
using Livros.Infra.Data.Entities;
using Moq;
using Xunit;
using NUnit.Framework;

namespace Livros.Application.Test
{
    [TestFixture]
    public class AssuntoHandlerTest
    {
        private readonly AssuntoInsertCommandHandler _assuntoInsertCommandHandler;
        private readonly AssuntoUpdateCommandHandler _assuntoUpdateCommandHandler;
        private readonly AssuntoGetByIdCommandHandler _assuntoGetByIdCommandHandler;
        private readonly AssuntoGetCommandHandler _assuntoGetCommandHandler;
        private readonly Mock<IRepositoryAssunto> _repositoryAssuntoMock = new Mock<IRepositoryAssunto>();
        private readonly Mock<IRepository<Assunto>> _repositoryAssuntoGenericMock = new Mock<IRepository<Assunto>>();
        
        public AssuntoHandlerTest()
        {
            _assuntoInsertCommandHandler = new AssuntoInsertCommandHandler(_repositoryAssuntoMock.Object);
            _assuntoUpdateCommandHandler = new AssuntoUpdateCommandHandler(_repositoryAssuntoMock.Object);
            _assuntoGetByIdCommandHandler = new AssuntoGetByIdCommandHandler(_repositoryAssuntoMock.Object);
            _assuntoGetCommandHandler = new AssuntoGetCommandHandler(_repositoryAssuntoGenericMock.Object);
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
            _repositoryAssuntoMock.Verify(repo => repo.CreateAsync(It.IsAny<Assunto>()), Times.Exactly(1));
            Assert.Equal(id, result.CodAs);
        }

        [Fact]
        public async Task Create_Assunto_Description_Empty()
        {
            var request = new AssuntoInsertCommandRequest(string.Empty);
            var id = 10;
            _repositoryAssuntoMock.Setup(repo => repo.CreateAsync(It.IsAny<Assunto>()))
                      .ReturnsAsync(id);

            var result = await _assuntoInsertCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryAssuntoMock.Verify(repo => repo.CreateAsync(It.IsAny<Assunto>()), Times.Never());
            Assert.Equal("Descricao is required.", result.Notifications[0].Message);            
        }

        [Fact]
        public async Task Update_Assunto_Sucess()
        {
            var id = 10;
            var request = new AssuntoUpdateCommandRequest(id,"Novo assunto Update");

            _repositoryAssuntoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync(new Assunto());
            
            _repositoryAssuntoMock.Setup(repo => repo.UpdateAsync(It.IsAny<Assunto>()))
                      .ReturnsAsync(id);

            var result = await _assuntoUpdateCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryAssuntoMock.Verify(repo => repo.UpdateAsync(It.IsAny<Assunto>()), Times.Exactly(1));
            Assert.Equal(id, result.CodAs);
        }

        [Fact]
        public async Task Update_Assunto_Description_Empty()
        {
            var id = 10;
            var request = new AssuntoUpdateCommandRequest(id, String.Empty);

            _repositoryAssuntoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync(new Assunto());

            _repositoryAssuntoMock.Setup(repo => repo.UpdateAsync(It.IsAny<Assunto>()))
                      .ReturnsAsync(id);

            var result = await _assuntoUpdateCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryAssuntoMock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Never());
            _repositoryAssuntoMock.Verify(repo => repo.UpdateAsync(It.IsAny<Assunto>()), Times.Never());
            Assert.Equal("Descricao is required.", result.Notifications[0].Message);
        }

        [Fact]
        public async Task GetById_Assunto_Sucess()
        {
            var id = 10;
            var request = new AssuntoGetByIdCommandRequest(id);

            _repositoryAssuntoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync(new Assunto() { CodAs = 10, Descricao = "Descriçao" });

            var result = await _assuntoGetByIdCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryAssuntoMock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Exactly(1));
            Assert.Equal(id, result.Assunto.CodAs);
        }

        [Fact]
        public async Task GetById_Assunto_Id_Zero()
        {
            var id = 0;
            var request = new AssuntoGetByIdCommandRequest(id);

            _repositoryAssuntoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync(new Assunto());

            var result = await _assuntoGetByIdCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryAssuntoMock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Never);
            Assert.Equal("Code is required.", result.Notifications[0].Message);
        }


        [Fact]
        public async Task GetAll_Assunto_Sucess()
        {
            var request = new AssuntoGetCommandRequest();

            _repositoryAssuntoGenericMock.Setup(repo => repo.GetAsync<AssuntoPersistence>())
                      .ReturnsAsync(new List<AssuntoPersistence>() { new Assunto() { CodAs = 10, Descricao = "Descriçao" } });

            var result = await _assuntoGetCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryAssuntoGenericMock.Verify(repo => repo.GetAsync<AssuntoPersistence>(), Times.Exactly(1));
            Assert.True(result.Assunto.Any());
        }
    }
}
