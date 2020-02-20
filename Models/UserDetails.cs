using System;

namespace first_rest_api.Models {
    public class UserDetails {
        public int id {get; set; }
        public string name {get; set; }
        public string address {get; set; }
        public DateTime createdDate {get; set; }
        public string createdBy {get; set; }
    }
}