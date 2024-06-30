using BookAPI.Dto;
using BookAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ControllerAPI : ControllerBase
    {


        [HttpGet("all")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<Book>))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<List<Book>>> GetAll();

        [HttpGet("findByName")]
        [ProducesResponseType(statusCode: 200, type: typeof(Book))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Book>> GetByName([FromQuery] string name);

        [HttpGet("findById")]
        [ProducesResponseType(statusCode: 200, type: typeof(Book))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Book>> GetById(int id);

        [HttpPost("createBook")]
        [ProducesResponseType(statusCode: 201, type: typeof(Book))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Book>> CreateBook(CreateRequest request);

        [HttpPut("updateBook")]
        [ProducesResponseType(statusCode: 200, type: typeof(Book))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Book>> UpdateBook(int id, UpdateRequest request);

        [HttpDelete("deleteBook")]
        [ProducesResponseType(statusCode: 200, type: typeof(Book))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Book>> DeleteBook(int id);


    }
}
