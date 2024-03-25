using BookAPI.Models;
using BookAPI.Repository.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [ApiController]
    [Route("api/v1/book")]
    public class ControllerBook : ControllerBase
    {

        private readonly ILogger<ControllerBook> _logger;

        private IRepositoryBook _repository;

        public ControllerBook(ILogger<ControllerBook> logger, IRepositoryBook repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll()
        {
            var book = await _repository.GetAllAsync();
            return Ok(book);
        }

        [HttpGet("/findById")]
        public async Task<ActionResult<Book>> GetById([FromQuery] int id)
        {
            var book = await _repository.GetByIdAsync(id);
            return Ok(book);
        }

        [HttpGet("/findByName.{name}")]
        public async Task<ActionResult<Book>> GetByName([FromRoute] string name)
        {
            var book = await _repository.GetByName(name);
            return Ok(book);
        }


    }
}
