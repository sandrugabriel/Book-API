using BookAPI.Constants;
using BookAPI.Controllers;
using BookAPI.Controllers.interfaces;
using BookAPI.Dto;
using BookAPI.Exceptions;
using BookAPI.Models;
using BookAPI.Service.interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Books.Helpers;

namespace Test.Books.UnitTests
{
    public class TestController
    {
        private readonly Mock<ICommandService> _mockCommandService;
        private readonly Mock<IQueryService> _mockQueryService;
        private readonly ControllerAPI bookApiController;

        public TestController()
        {
            _mockCommandService = new Mock<ICommandService>();
            _mockQueryService = new Mock<IQueryService>();

            bookApiController = new ControllerBook(_mockQueryService.Object, _mockCommandService.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mockQueryService.Setup(repo => repo.GetAll()).ThrowsAsync(new ItemsDoNotExist(Constants.ItemsDoNotExist));

            var restult = await bookApiController.GetAll();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(restult.Result);

            Assert.Equal(Constants.ItemsDoNotExist, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task GetAll_ValidData()
        {
            var books = TestBookFactory.CreateBooks(5);
            _mockQueryService.Setup(repo => repo.GetAll()).ReturnsAsync(books);

            var result = await bookApiController.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allbooks = Assert.IsType<List<Book>>(okResult.Value);

            Assert.Equal(4, allbooks.Count);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact]
        public async Task Create_InvalidYear()
        {

            var createRequest = new CreateRequest
            {
                Author = "test5",
                Name = "test1",
                Year = 2000
            };

            _mockCommandService.Setup(repo => repo.Create(It.IsAny<CreateRequest>())).
                ThrowsAsync(new InvalidYear(Constants.InvalidYear));

            var result = await bookApiController.CreateBook(createRequest);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(400, badRequest.StatusCode);
            Assert.Equal(Constants.InvalidYear, badRequest.Value);

        }

        [Fact]
        public async Task Create_ValidData()
        {
            var createRequest = new CreateRequest
            {
                Author = "test5",
                Name = "test1",
                Year = 2000
            };

            var book = TestBookFactory.CreateBook(1);
            book.Year = createRequest.Year;
            book.Name = createRequest.Name;
            book.Author = createRequest.Author;   

            _mockCommandService.Setup(repo => repo.Create(It.IsAny<CreateRequest>())).ReturnsAsync(book);

            var result = await bookApiController.CreateBook(createRequest);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, book);

        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var update = new UpdateRequest
            {
               Year = 2000
            };

            _mockCommandService.Setup(repo => repo.Update(1, update)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await bookApiController.UpdateBook(1, update);

            var ntFound = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(ntFound.StatusCode, 404);
            Assert.Equal(Constants.ItemDoesNotExist, ntFound.Value);

        }
        [Fact]
        public async Task Update_ValidData()
        {
            var update = new UpdateRequest
            {
                Year = 2000
            };

            var book = TestBookFactory.CreateBook(1);

            _mockCommandService.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<UpdateRequest>())).ReturnsAsync(book);

            var result = await bookApiController.UpdateBook(1, update);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, book);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mockCommandService.Setup(repo => repo.Delete(1)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await bookApiController.DeleteBook(1);

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notFound.StatusCode, 404);
            Assert.Equal(notFound.Value, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {

            var book = TestBookFactory.CreateBook(1);

            _mockCommandService.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(book);

            var result = await bookApiController.DeleteBook(1);

            var okresult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okresult.StatusCode);
            Assert.Equal(okresult.Value, book);

        }
    }
}
