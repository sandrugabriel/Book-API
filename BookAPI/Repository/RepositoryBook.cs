using AutoMapper;
using BookAPI.Data;
using BookAPI.Models;
using BookAPI.Repository.interfaces;
using Microsoft.EntityFrameworkCore;
using System;

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
    }
}
