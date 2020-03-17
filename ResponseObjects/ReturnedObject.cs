using System.Collections.Generic;

namespace first_rest_api.ResponseObjects
{
    public class ReturnedObject<T>
    {  
        public ReturnedObject(){
            status = true;
        }

        public bool status { get; set; }
        public List<T> Data { get; set; }
    }
}