using Microsoft.EntityFrameworkCore;

namespace first_rest_api.Models {
    
    public class UserDetailsContext1 : DbContext {
        
        public UserDetailsContext1(DbContextOptions<UserDetailsContext1> options) 
                : base(options) {
        }

        public DbSet<UserDetails> userDetails {get; set; } 
    }
}