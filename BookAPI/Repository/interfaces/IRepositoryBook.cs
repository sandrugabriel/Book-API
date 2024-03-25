using BookAPI.Models;

namespace BookAPI.Repository.interfaces
{
    public interface IRepositoryBook
    {
        Task<IEnumerable<Book>> GetAllAsync();

        Task<Book> GetByIdAsync(int id);

        Task<Book> GetByName(string name);

    }
}
