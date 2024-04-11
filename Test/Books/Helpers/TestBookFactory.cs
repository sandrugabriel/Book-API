
using BookAPI.Models;

namespace Test.Books.Helpers
{
    public class TestBookFactory
    {

        public static Book CreateBook(int id)
        {
            return new Book
            {
                Id = id,
                Author = "test" + id,
                Name = "test",
                Year = 1900 + id

            };
        }

        public static List<Book> CreateBooks(int count)
        {

            List<Book> list = new List<Book>();
            for (int i = 1; i < count; i++)
            {
                list.Add(CreateBook(i));
            }

            return list;
        }
    }
}
