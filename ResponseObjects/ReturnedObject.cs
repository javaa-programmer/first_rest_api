namespace first_rest_api.ResponseObjects
{
    public class ReturnedObject<T>
    {  
        public ReturnedObject(){
            status = true;
        }

        public bool status { get; set; }
        public T Data { get; set; }
    }
}