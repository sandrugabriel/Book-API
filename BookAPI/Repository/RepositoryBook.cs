using AutoMapper;
using BookAPI.Data;
using BookAPI.Models;
using BookAPI.Repository.interfaces;
using System;
using BookAPI.Dto;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Repository
{
    public class RepositoryBook:IRepositoryBook
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public RepositoryBook(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Book.ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            List<Book> all = await _context.Book.ToListAsync();

            for(int i=0;i<all.Count; i++)
                if (all[i].Id == id)
                    return all[i];

            return null;
        }

        public async Task<Book> GetByName(string name)
        {

            List<Book> all = await _context.Book.ToListAsync();

            for (int i = 0; i < all.Count; i++)
                if (all[i].Name.Equals(name))
                    return all[i];

            return null;
        }


        public async Task<Book> Create(CreateRequest request)
        {

            var book = _mapper.Map<Book>(request);

            _context.Book.Add(book);

            await _context.SaveChangesAsync();

            return book;

        }

        public async Task<Book> Update(int id, UpdateRequest request)
        {

            var book = await _context.Book.FindAsync(id);

            book.Name = request.Name ?? book.Name;
            book.Author = request.Author ?? book.Author;
            book.Year = request.Year ?? book.Year;

            _context.Book.Update(book);

            await _context.SaveChangesAsync();

            return book;

        }

        public async Task<Book> DeleteById(int id)
        {
            var book = await _context.Book.FindAsync(id);

            _context.Book.Remove(book);

            await _context.SaveChangesAsync();

            return book;
        }

    }
}
