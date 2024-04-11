using BookAPI.Constants;
using BookAPI.Dto;
using BookAPI.Exceptions;
using BookAPI.Models;
using BookAPI.Repository.interfaces;
using BookAPI.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Books.Helpers;

namespace Test.Books.UnitTests
{
    public class TestCommandService
    {
        private readonly Mock<IRepositoryBook> _mock;
        private readonly CommandService _commandService;

        public TestCommandService()
        {
            _mock = new Mock<IRepositoryBook>();
            _commandService = new CommandService(_mock.Object);
        }

        [Fact]
        public async Task Create_InvalidYear()
        {
            var createRequest = new CreateRequest
            {
                Author = "test",
                Year = 0,
                Name = "test"
            };

            _mock.Setup(repo => repo.Create(createRequest)).ReturnsAsync((Book)null);
            var exception = await Assert.ThrowsAsync<InvalidYear>(() => _commandService.Create(createRequest));

            Assert.Equal(Constants.InvalidYear, exception.Message);
        }

        [Fact]
        public async Task Create_ValidData()
        {
            var createRequest = new CreateRequest
            {
                Author = "test",
                Year = 2000,
                Name = "test"
            };

            var Book = TestBookFactory.CreateBook(50);
            Book.Year  = createRequest.Year;

            _mock.Setup(repo => repo.Create(It.IsAny<CreateRequest>())).ReturnsAsync(Book);

            var result = await _commandService.Create(createRequest);

            Assert.NotNull(result);
            Assert.Equal(result.Year, createRequest.Year);
        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var updateRequest = new UpdateRequest
            {
                Year = 2000
            };

            _mock.Setup(repo => repo.GetByIdAsync(50)).ReturnsAsync((Book)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _commandService.Update(50, updateRequest));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task Update_InvalidYear()
        {
            var updateRequest = new UpdateRequest
            {
                Year = 0
            };
            var Book = TestBookFactory.CreateBook(50);
            Book.Year = updateRequest.Year.Value;
            _mock.Setup(repo => repo.GetByIdAsync(50)).ReturnsAsync(Book);

            var exception = await Assert.ThrowsAsync<InvalidYear>(() => _commandService.Update(50, updateRequest));

            Assert.Equal(Constants.InvalidYear, exception.Message);
        }

        [Fact]
        public async Task Update_ValidData()
        {
            var updateREquest = new UpdateRequest
            {
                Year = 2000
            };

            var Book = TestBookFactory.CreateBook(1);
            Book.Year = updateREquest.Year.Value;

            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(Book);
            _mock.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<UpdateRequest>())).ReturnsAsync(Book);

            var result = await _commandService.Update(1, updateREquest);

            Assert.NotNull(result);
            Assert.Equal(Book, result);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.DeleteById(It.IsAny<int>())).ReturnsAsync((Book)null);

            var exception = await Assert.ThrowsAnyAsync<ItemDoesNotExist>(() => _commandService.Delete(1));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            Book Book = TestBookFactory.CreateBook(50);

            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(Book);

            var restul = await _commandService.Delete(50);

            Assert.NotNull(restul);
            Assert.Equal(Book, restul);
        }



    }
}
