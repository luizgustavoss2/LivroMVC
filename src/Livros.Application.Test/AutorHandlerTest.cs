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

namespace Livros.Application.Test
{
    [Trait("Autor", "Usecases")]
    public class AutorHandlerTest
    {
        private readonly AutorInsertCommandHandler _assuntoInsertCommandHandler;
        private readonly AutorUpdateCommandHandler _assuntoUpdateCommandHandler;
        private readonly AutorGetByIdCommandHandler _assuntoGetByIdCommandHandler;
        private readonly AutorGetCommandHandler _assuntoGetCommandHandler;
        private readonly Mock<IRepositoryAutor> _repositoryAutorMock = new Mock<IRepositoryAutor>();
        private readonly Mock<IRepository<Autor>> _repositoryAutorGenericMock = new Mock<IRepository<Autor>>();
        
        public AutorHandlerTest()
        {
            _assuntoInsertCommandHandler = new AutorInsertCommandHandler(_repositoryAutorMock.Object);
            _assuntoUpdateCommandHandler = new AutorUpdateCommandHandler(_repositoryAutorMock.Object);
            _assuntoGetByIdCommandHandler = new AutorGetByIdCommandHandler(_repositoryAutorMock.Object);
            _assuntoGetCommandHandler = new AutorGetCommandHandler(_repositoryAutorGenericMock.Object);
        }

        [Fact]
        public async Task Create_Autor_Sucess()
        {
            var request = new AutorInsertCommandRequest("Novo Autor");
            var id = 10;
            _repositoryAutorMock.Setup(repo => repo.CreateAsync(It.IsAny<Autor>()))
                      .ReturnsAsync(id);

            var result = await _assuntoInsertCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryAutorMock.Verify(repo => repo.CreateAsync(It.IsAny<Autor>()), Times.Exactly(1));
            Assert.Equal(id, result.CodAu);
        }

        [Fact]
        public async Task Create_Autor_Description_Empty()
        {
            var request = new AutorInsertCommandRequest(string.Empty);
            var id = 10;
            _repositoryAutorMock.Setup(repo => repo.CreateAsync(It.IsAny<Autor>()))
                      .ReturnsAsync(id);

            var result = await _assuntoInsertCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryAutorMock.Verify(repo => repo.CreateAsync(It.IsAny<Autor>()), Times.Never());
            Assert.Equal("Nome is required.", result.Notifications[0].Message);            
        }

        [Fact]
        public async Task Update_Autor_Sucess()
        {
            var id = 10;
            var request = new AutorUpdateCommandRequest(id,"Novo Autor Update");

            _repositoryAutorMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync(new Autor());
            
            _repositoryAutorMock.Setup(repo => repo.UpdateAsync(It.IsAny<Autor>()))
                      .ReturnsAsync(id);

            var result = await _assuntoUpdateCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryAutorMock.Verify(repo => repo.UpdateAsync(It.IsAny<Autor>()), Times.Exactly(1));
            Assert.Equal(id, result.CodAu);
        }

        [Fact]
        public async Task Update_Autor_Description_Empty()
        {
            var id = 10;
            var request = new AutorUpdateCommandRequest(id, String.Empty);

            _repositoryAutorMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync(new Autor());

            _repositoryAutorMock.Setup(repo => repo.UpdateAsync(It.IsAny<Autor>()))
                      .ReturnsAsync(id);

            var result = await _assuntoUpdateCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryAutorMock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Never());
            _repositoryAutorMock.Verify(repo => repo.UpdateAsync(It.IsAny<Autor>()), Times.Never());
            Assert.Equal("Nome is required.", result.Notifications[0].Message);
        }

        [Fact]
        public async Task GetById_Autor_Sucess()
        {
            var id = 10;
            var request = new AutorGetByIdCommandRequest(id);

            _repositoryAutorMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync(new Autor() { CodAu = 10, Nome = "Nome" });

            var result = await _assuntoGetByIdCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryAutorMock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Exactly(1));
            Assert.Equal(id, result.Autor.CodAu);
        }

        [Fact]
        public async Task GetById_Autor_Id_Zero()
        {
            var id = 0;
            var request = new AutorGetByIdCommandRequest(id);

            _repositoryAutorMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync(new Autor());

            var result = await _assuntoGetByIdCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryAutorMock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Never);
            Assert.Equal("Code is required.", result.Notifications[0].Message);
        }


        [Fact]
        public async Task GetAll_Autor_Sucess()
        {
            var request = new AutorGetCommandRequest();

            _repositoryAutorGenericMock.Setup(repo => repo.GetAsync<AutorPersistence>())
                      .ReturnsAsync(new List<AutorPersistence>() { new Autor() { CodAu = 10, Nome = "Nome" } });

            var result = await _assuntoGetCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryAutorGenericMock.Verify(repo => repo.GetAsync<AutorPersistence>(), Times.Exactly(1));
            Assert.True(result.Autor.Any());
        }

    }
}
