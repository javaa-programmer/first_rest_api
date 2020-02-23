using System.Collections.Generic;
using System.Threading.Tasks;
using first_rest_api.Models;
using first_rest_api.Repositories;
using first_rest_api.ResponseObjects;

namespace first_rest_api.Services {
    public class UserDetailsServices : IUserDetailsServices
    {

        UserDetailsRepository _userRepository;
        public UserDetailsServices() {
            _userRepository = new UserDetailsRepository();
        }


        public async Task<ResultModels<UserDetails>> GetAllUsers() {
            return await _userRepository.GetAllUsers();
        }           

        public async Task<UserDetails> GetUserDetailsById(int id) {
            UserDetails userDetails = await _userRepository.GetUserDetailsById(id);
            return userDetails;
        }

        public int CreateUser(UserDetails ud) {

            return _userRepository.CreateUser(ud);
        }
    }
}