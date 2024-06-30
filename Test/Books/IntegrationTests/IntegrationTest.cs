using System.Net;
using System.Text;
using BookAPI.Dto;
using BookAPI.Models;
using Newtonsoft.Json;
using Test.Books.Helpers;
using Test.Books.Infrastructure;

namespace Test.Books.IntegrationTests;


public class IntegrationTest : IClassFixture<ApiWebApplicationFactory>
{
    
        private readonly HttpClient _client;

        public IntegrationTest(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllBooks_BooksFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createBookRequest = TestBookFactory.CreateBook(1);
            var content = new StringContent(JsonConvert.SerializeObject(createBookRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/ControllerBook/createBook", content);

            var response = await _client.GetAsync("/api/v1/ControllerBook/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetBookById_BookFound_ReturnsOkStatusCode()
        {
            var createBookRequest = new CreateRequest
            { Name = "test", Author = "ASasdadd", Year = 2000};
            var content = new StringContent(JsonConvert.SerializeObject(createBookRequest), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/ControllerBook/createBook", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Book>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result.Name, createBookRequest.Name);
        }

        [Fact]
        public async Task GetBookById_BookNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/ControllerBook/findById?id=9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode()
        {
            var request = "/api/v1/ControllerBook/createBook";
            
            var createBook = new CreateRequest
                { Name = "test", Author = "ASasdadd", Year = 2000};

            var content = new StringContent(JsonConvert.SerializeObject(createBook), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Book>(responseString);

            Assert.NotNull(result);
            Assert.Equal(createBook.Name, result.Name);
        }

        [Fact]
        public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode()
        {
            var request = "/api/v1/ControllerBook/createBook";
            var createBook = new CreateRequest
                { Name = "test", Author = "ASasdadd", Year = 2000};

            var content = new StringContent(JsonConvert.SerializeObject(createBook), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Book>(responseString);

            request = $"/api/v1/ControllerBook/updateBook?id={result.Id}";
            var updateBook = new UpdateRequest { Year = 20 };
            content = new StringContent(JsonConvert.SerializeObject(updateBook), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);
            var responceStringUp = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Book>(responceStringUp);


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result1.Year, updateBook.Year);
        }

        [Fact]
        public async Task Put_Update_InvalidBookYear_ReturnsBadRequestStatusCode()
        {
            var request = "/api/v1/ControllerBook/createBook";
            var createBook = new CreateRequest
                { Name = "test", Author = "ASasdadd", Year = 2000};

            var content = new StringContent(JsonConvert.SerializeObject(createBook), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Book>(responseString);

            request = $"/api/v1/ControllerBook/updateBook?id={result.Id}";
            var updateBook = new UpdateRequest { Year = -3 };
            content = new StringContent(JsonConvert.SerializeObject(updateBook), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);
            var responceStringUp = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Book>(responseString);


            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEqual(result1.Year, updateBook.Year);
        }

        [Fact]
        public async Task Put_Update_BookDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerBook/updateBook";
            var updateBook = new UpdateRequest { Year = 30 };
            var content = new StringContent(JsonConvert.SerializeObject(updateBook), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Delete_BookExists_ReturnsDeletedBook()
        {
            var request = "/api/v1/ControllerBook/createBook";
            var createBook = new CreateRequest
                { Name = "test", Author = "ASasdadd", Year = 2000};

            var content = new StringContent(JsonConvert.SerializeObject(createBook), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Book>(responseString)!;

            request = $"/api/v1/ControllerBook/deleteBook?id={result.Id}";

            response = await _client.DeleteAsync(request);
            var responceString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Book>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result1.Name, createBook.Name);
        }

        [Fact]
        public async Task Delete_Delete_BookDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerBook/deleteBook?id=7";

            var response = await _client.DeleteAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

}