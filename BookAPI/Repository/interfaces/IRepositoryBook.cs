using BookAPI.Models;
using BookAPI.Dto;


namespace BookAPI.Repository.interfaces
{
    public interface IRepositoryBook
    {
        Task<IEnumerable<Book>> GetAllAsync();

        Task<Book> GetByIdAsync(int id);

        Task<Book> GetByName(string name);

        Task<Book> Create(CreateRequest request);

        Task<Book> Update(int id, UpdateRequest request);

        Task<Book> DeleteById(int id);


    }
}
