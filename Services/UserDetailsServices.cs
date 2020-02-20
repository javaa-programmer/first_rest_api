using System.Collections.Generic;
using System.Threading.Tasks;
using first_rest_api.Models;
using first_rest_api.Repositories;

namespace first_rest_api.Services {
    public class UserDetailsServices : IUserDetailsServices
    {

        UserDetailsRepository _userRepository;
        public UserDetailsServices(UserDetailsRepository userRepository) {
            _userRepository = userRepository;
        }


        public List<UserDetails> GetAllUsers() {
            return _userRepository.GetAllUsers();
        }           

        public UserDetails GetUserDetailsById(int id) {
            return _userRepository.GetUserDetailsById(id);
        }

        public int CreateUser(UserDetails ud) {

            return _userRepository.CreateUser(ud);
        }
    }
}