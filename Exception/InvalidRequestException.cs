public class InvalidRequestException : System.Exception
{
    int status { get; set; }
    public InvalidRequestException() { }
    public InvalidRequestException(string message, int status) : base(message) {
        this.status = status;
     }
}