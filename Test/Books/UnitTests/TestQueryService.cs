using BookAPI.Constants;
using BookAPI.Exceptions;
using BookAPI.Models;
using BookAPI.Repository.interfaces;
using BookAPI.Service;
using BookAPI.Service.interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Books.Helpers;

namespace Test.Books.UnitTests
{
    public class TestQueryService
    {
        private readonly Mock<IRepositoryBook> _mock;
        private readonly IQueryService _service;

        public TestQueryService()
        {
            _mock = new Mock<IRepositoryBook>();
            _service = new QueryService(_mock.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Book>());

            var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAll());

            Assert.Equal(exception.Message, Constants.ItemsDoNotExist);
        }

        [Fact]
        public async Task GetAll_ValidData()
        {
            var banks = TestBookFactory.CreateBooks(5);

            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(banks);

            var result = await _service.GetAll();

            Assert.NotNull(result);
            Assert.Equal(banks, result);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Book)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetById(1));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);
        }

        [Fact]
        public async Task GetById_ValidData()
        {
            var bank = TestBookFactory.CreateBook(1);
            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(bank);

            var result = await _service.GetById(1);

            Assert.NotNull(result);
            Assert.Equal(bank, result);

        }

        [Fact]
        public async Task GetByName_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByName("")).ReturnsAsync((Book)null);
            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByName(""));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task GetByName_ValidData()
        {
            var bank = TestBookFactory.CreateBook(10);
            _mock.Setup(repo => repo.GetByName("test")).ReturnsAsync(bank);
            var result = await _service.GetByName("test");

            Assert.NotNull(result);
            Assert.Equal(bank, result);

        }



    }
}
