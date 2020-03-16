using System;

namespace first_rest_api.ResponseObjects {
    public class UserDetailsObject {
        public int id {get; set; }
        public string firstname {get; set; }
        public string lastname {get; set; }
        public string address {get; set; }
        public string city {get; set; }
        public string country {get; set; }
        public int pincode {get; set; }
        public DateTime creationdate {get; set; }
        public string createdby {get; set; }
    }
}