using System.Collections.Generic;
using System.Text.Json;


namespace first_rest_api.ResponseObjects {

    public class ResponseEntities<T> {
        public  ResponseEntities(List<T> c)
        {
            Records = c;
            
        }

        public List<T> Records { get; }

    }
}