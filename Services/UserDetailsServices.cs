using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using first_rest_api.Models;
using first_rest_api.Repositories;
using first_rest_api.ResponseObjects;
using Microsoft.AspNetCore.Http;
using first_rest_api.Utilities;

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

        public async Task<UserDetails> CreateUser(UserDetails ud) {
            return await _userRepository.CreateUser(ud);
        }

        // Service
        public async Task<string> UploadImage(IFormFile file, string fileUploadPath)
        {
	        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

	        string folderPath = $"TestPath\\1";

	        string folderWisePath = Path.Combine(fileUploadPath, folderPath);

            System.IO.Directory.CreateDirectory(folderWisePath);
	        string fullPath = Path.Combine(folderWisePath, fileName);

	        await IOHelper.FileCopy(fullPath, file);

	        return fullPath;
        }
    }
}