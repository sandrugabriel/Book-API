using BookAPI.Exceptions;
using BookAPI.Models;
using BookAPI.Repository.interfaces;
using BookAPI.Service.interfaces;

namespace BookAPI.Service
{
    public class QueryService : IQueryService
    {

        private IRepositoryBook _repository;

        public QueryService(IRepositoryBook repository)
        {
            _repository = repository;
        }

        public async Task<List<Book>> GetAll()
        {
            var books = await _repository.GetAllAsync();

            if (books.Count() == 0)
            {
                throw new ItemsDoNotExist(Constants.Constants.ItemsDoNotExist);
            }

            return (List<Book>)books;
        }

        public async Task<Book> GetByName(string name)
        {
            var books = await _repository.GetByName(name);

            if (books.Name == "")
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemsDoNotExist);
            }

            return books;

        }

        public async Task<Book> GetById(int id)
        {
            var books = await _repository.GetByIdAsync(id);

            if (books == null)
            {
                throw new ItemsDoNotExist(Constants.Constants.ItemDoesNotExist);
            }

            return books;
        }

    }
}
