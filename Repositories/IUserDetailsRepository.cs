using System.Collections.Generic;
using System.Threading.Tasks;
using first_rest_api.Models;
using first_rest_api.ResponseObjects;

namespace first_rest_api.Repositories
{
    public interface IUserDetailsRepository
    {
        Task<ResultModels<UserDetails>> GetAllUsers();
        
        Task<UserDetails> GetUserDetailsById(int id);

        Task<UserDetails> CreateUser(UserDetails ud);

    }
}