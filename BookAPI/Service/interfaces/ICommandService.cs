using BookAPI.Dto;
using BookAPI.Models;

namespace BookAPI.Service.interfaces
{
    public interface ICommandService
    {

        Task<Book> Create(CreateRequest request);

        Task<Book> Update(int id, UpdateRequest request);

        Task<Book> Delete(int id);

    }
}
