namespace first_rest_api.ResponseObjects
{
    public class ErrorObject
    {  
        public ErrorObject(){
            status = false;
        }

        public bool status { get; set; }
        public string message {get; set; }

    }
}