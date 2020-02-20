using System.Collections.Generic;
using System.Threading.Tasks;
using first_rest_api.Models;

namespace first_rest_api.Repositories
{
    public interface IUserDetailsRepository
    {
        List<UserDetails> GetAllUsers();
        
        UserDetails GetUserDetailsById(int id);


        int CreateUser(UserDetails ud);

    }
}