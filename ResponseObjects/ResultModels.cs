using System.Collections.Generic;

namespace first_rest_api.ResponseObjects {
    public class ResultModels<T> where T: class
    {
        public  ResultModels(List<T> c)
        {
            Records = c;
        }

        public List<T> Records { get; }
    } 
}