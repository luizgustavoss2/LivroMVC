using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Livros.Application.UseCases;
using Livros.Domain.Entities;
using Livros.Domain.Interfaces.Repository;
using Moq;
using Xunit;

namespace Livros.Application.Test
{
    [Trait("Livro", "Usecases")]
    public class LivroHandlerTest
    {
        private readonly LivroInsertCommandHandler _assuntoInsertCommandHandler;
        private readonly LivroUpdateCommandHandler _assuntoUpdateCommandHandler;
        private readonly LivroGetByIdCommandHandler _assuntoGetByIdCommandHandler;
        private readonly LivroGetCommandHandler _assuntoGetCommandHandler;
        private readonly Mock<IRepositoryLivro> _repositoryLivroMock = new Mock<IRepositoryLivro>();
        private readonly Mock<IRepositoryLivro_Assunto> _repositoryLivroAssuntoMock = new Mock<IRepositoryLivro_Assunto>();
        private readonly Mock<IRepositoryLivro_Autor> _repositoryLivroAutorMock = new Mock<IRepositoryLivro_Autor>();
        private readonly Mock<IRepositoryAutor> _repositoryAutorMock = new Mock<IRepositoryAutor>();
        private readonly Mock<IRepositoryPreco> _repositoryPrecoMock = new Mock<IRepositoryPreco>();
        private readonly Mock<IRepository<Livro>> _repositoryLivroGenericMock = new Mock<IRepository<Livro>>();
        
        public LivroHandlerTest()
        {
            _assuntoInsertCommandHandler = new LivroInsertCommandHandler(_repositoryLivroMock.Object, _repositoryLivroAssuntoMock.Object, _repositoryLivroAutorMock.Object, _repositoryPrecoMock.Object);
            _assuntoUpdateCommandHandler = new LivroUpdateCommandHandler(_repositoryLivroMock.Object, _repositoryLivroAssuntoMock.Object, _repositoryLivroAutorMock.Object, _repositoryPrecoMock.Object);
            _assuntoGetByIdCommandHandler = new LivroGetByIdCommandHandler(_repositoryLivroMock.Object, _repositoryAutorMock.Object, _repositoryPrecoMock.Object);
            _assuntoGetCommandHandler = new LivroGetCommandHandler(_repositoryLivroMock.Object, _repositoryAutorMock.Object, _repositoryPrecoMock.Object);
        }

        [Fact]
        public async Task Create_Livro_Sucess()
        {
            var request = new LivroInsertCommandRequest("Titulo", "Editora",1,2024,1,new List<int>());
            var id = 10;
            _repositoryLivroMock.Setup(repo => repo.CreateAsync(It.IsAny<Livro>()))
                      .ReturnsAsync(id);

            var result = await _assuntoInsertCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryLivroMock.Verify(repo => repo.CreateAsync(It.IsAny<Livro>()), Times.Exactly(1));
            Assert.Equal(id, result.CodL);
        }

        [Fact]
        public async Task Create_Livro_Titulo_Empty()
        {
            var request = new LivroInsertCommandRequest(String.Empty, "Editora", 1, 2024, 1, new List<int>());
            var id = 10;
            _repositoryLivroMock.Setup(repo => repo.CreateAsync(It.IsAny<Livro>()))
                      .ReturnsAsync(id);

            var result = await _assuntoInsertCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryLivroMock.Verify(repo => repo.CreateAsync(It.IsAny<Livro>()), Times.Never());
            Assert.Equal("Titulo is required.", result.Notifications[0].Message);            
        }

        [Fact]
        public async Task Update_Livro_Sucess()
        {
            var id = 10;
            var request = new LivroUpdateCommandRequest(id, "Titulo", "Editora", 1, 2024, 1, new List<int>());

            _repositoryLivroMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync(new Livro());
            
            _repositoryLivroMock.Setup(repo => repo.UpdateAsync(It.IsAny<Livro>()))
                      .ReturnsAsync(id);

            var result = await _assuntoUpdateCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryLivroMock.Verify(repo => repo.UpdateAsync(It.IsAny<Livro>()), Times.Exactly(1));
            Assert.Equal(id, result.CodL);
        }

        [Fact]
        public async Task Update_Livro_Titulo_Empty()
        {
            var id = 10;
            var request = new LivroUpdateCommandRequest(id, String.Empty, "Editora", 1, 2024, 1, new List<int>());

            _repositoryLivroMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync(new Livro());

            _repositoryLivroMock.Setup(repo => repo.UpdateAsync(It.IsAny<Livro>()))
                      .ReturnsAsync(id);

            var result = await _assuntoUpdateCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryLivroMock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Never());
            _repositoryLivroMock.Verify(repo => repo.UpdateAsync(It.IsAny<Livro>()), Times.Never());
            Assert.Equal("Titulo is required.", result.Notifications[0].Message);
        }

        [Fact]
        public async Task GetById_Livro_Sucess()
        {
            var id = 10;
            var request = new LivroGetByIdCommandRequest(id);

            _repositoryLivroMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync(new Livro() { CodL = 10 });

            var result = await _assuntoGetByIdCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryLivroMock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Exactly(1));
            Assert.Equal(id, result.Livro.CodL);
        }

        [Fact]
        public async Task GetById_Livro_Id_Zero()
        {
            var id = 0;
            var request = new LivroGetByIdCommandRequest(id);

            _repositoryLivroMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                      .ReturnsAsync(new Livro());

            var result = await _assuntoGetByIdCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryLivroMock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Never);
            Assert.Equal("Code is required.", result.Notifications[0].Message);
        }


        [Fact]
        public async Task GetAll_Livro_Sucess()
        {
            var request = new LivroGetCommandRequest();

            _repositoryLivroMock.Setup(repo => repo.GetAllAsync())
                      .ReturnsAsync(new List<Livro>() { new Livro() { CodL = 10 } });

            _repositoryAutorMock.Setup(repo => repo.GetByLivroIdAsync(It.IsAny<int>()))
                     .ReturnsAsync(new List<Autor>() { new Autor() });
            _repositoryPrecoMock.Setup(repo => repo.GetByLivroIdAsync(It.IsAny<int>()))
                     .ReturnsAsync(new List<Preco>() { new Preco() });            

            var result = await _assuntoGetCommandHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryLivroMock.Verify(repo => repo.GetAllAsync(), Times.Exactly(1));
            Assert.True(result.Livro.Any());
        }

    }
}
