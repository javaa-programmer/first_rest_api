using System;

namespace first_rest_api.CustomExceptions 
{
    class UserDetailsException : Exception {

        public UserDetailsException(String errMsg) {
            errorMessage = errMsg;
        }

        public string errorMessage {get ; set; }

    }
}