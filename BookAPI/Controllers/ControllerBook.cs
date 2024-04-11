using BookAPI.Controllers.interfaces;
using BookAPI.Dto;
using BookAPI.Exceptions;
using BookAPI.Models;
using BookAPI.Repository.interfaces;
using BookAPI.Service.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    public class ControllerBook : ControllerAPI
    {

        private IQueryService _queryService;
        private ICommandService _commandService;

        public ControllerBook(IQueryService queryService, ICommandService commandService)
        {
            _queryService = queryService;
            _commandService = commandService;
        }

        public override async Task<ActionResult<List<Book>>> GetAll()
        {
            try
            {
                var books = await _queryService.GetAll();

                return Ok(books);

            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<Book>> GetByName([FromQuery] string name)
        {
            try
            {
                var books = await _queryService.GetByName(name);
                return Ok(books);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }

        public override async Task<ActionResult<Book>> GetById(int id)
        {

            try
            {
                var book = await _queryService.GetById(id);
                return Ok(book);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }

        public override async Task<ActionResult<Book>> CreateBook(CreateRequest request)
        {
            try
            {
                var book = await _commandService.Create(request);
                return Ok(book);
            }
            catch (InvalidYear ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<Book>> UpdateBook(int id, UpdateRequest request)
        {
            try
            {
                var book = await _commandService.Update(id, request);
                return Ok(book);
            }
            catch (InvalidYear ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<Book>> DeleteBook(int id)
        {
            try
            {
                var book = await _commandService.Delete(id);
                return Ok(book);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }


        /*
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


                [HttpPost("/create")]
                public async Task<ActionResult<Book>> Create([FromBody] CreateRequest request)
                {
                    var book = await _repository.Create(request);
                    return Ok(book);

                }

                [HttpPut("/update")]
                public async Task<ActionResult<Book>> Update([FromQuery] int id, [FromBody] UpdateRequest request)
                {
                    var book = await _repository.Update(id, request);
                    return Ok(book);
                }

                [HttpDelete("/deleteById")]
                public async Task<ActionResult<Book>> DeleteCarById([FromQuery] int id)
                {
                    var book = await _repository.DeleteById(id);
                    return Ok(book);
                }*/
    }
}
