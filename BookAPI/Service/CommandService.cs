using BookAPI.Dto;
using BookAPI.Exceptions;
using BookAPI.Models;
using BookAPI.Repository.interfaces;
using BookAPI.Service.interfaces;

namespace BookAPI.Service
{
    public class CommandService : ICommandService
    {

        private IRepositoryBook _repository;

        public CommandService(IRepositoryBook repository)
        {
            _repository = repository;
        }

        public async Task<Book> Create(CreateRequest request)
        {

            if (request.Year <= 1000)
            {
                throw new InvalidYear(Constants.Constants.InvalidYear);
            }

            var book = await _repository.Create(request);

            return book;
        }

        public async Task<Book> Update(int id, UpdateRequest request)
        {

            var book = await _repository.GetByIdAsync(id);
            if (book == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }


            if (request.Year <= 1000)
            {
                throw new InvalidYear(Constants.Constants.InvalidYear);
            }
            book = await _repository.Update(id, request);
            return book;
        }

        public async Task<Book> Delete(int id)
        {

            var book = await _repository.GetByIdAsync(id);
            if (book == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }
            await _repository.DeleteById(id);
            return book;
        }


    }
}
