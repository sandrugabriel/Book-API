using BookAPI.Models;

namespace BookAPI.Service.interfaces
{
    public interface IQueryService
    {
        Task<List<Book>> GetAll();

        Task<Book> GetById(int id);

        Task<Book> GetByName(string name);
    }
}
