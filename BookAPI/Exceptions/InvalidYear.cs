namespace BookAPI.Exceptions
{
    public class InvalidYear : Exception
    {
        public InvalidYear(string? message):base(message) { }
    }
}
