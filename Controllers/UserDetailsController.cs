using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using first_rest_api.Models;
using first_rest_api.Services;
using first_rest_api.Repositories;
using first_rest_api.RequestObjects;

namespace first_rest_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        //private readonly UserDetailsContext _context;
        private UserDetailsServices _userDetailsService;
        private UserDetailsRepository _userDetailsRepository;
        public UserDetailsController()
        {
            _userDetailsRepository = new UserDetailsRepository();
            _userDetailsService = new UserDetailsServices(_userDetailsRepository);

        }

        // GET: api/UserDetails
        [HttpGet]
        [Route("~/api/UserDetails")]
        public ActionResult<IEnumerable<UserDetails>> GetUserDetails()
        {
            List<UserDetails> userDetails = _userDetailsService.GetAllUsers();
            return userDetails;
        }

        // GET: api/UserDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetails>> GetUserDetailById(int id)
        {
            UserDetails p = _userDetailsService.GetUserDetailsById(id);
;            //ResultModels<Technique> techniques = await ModelService.GetAllTechniques();
        //    var userDetails = await _context.UserDetails.FindAsync(id);
            if (p == null)
            {
                return NotFound();
            }

            return p;
        }

        // PUT: api/UserDetails/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDetails(int id, UserDetails userDetails)
        {
            if (id != userDetails.id)
            {
                return BadRequest();
            }

            //_context.Entry(userDetails).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserDetails
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public int CreateUserDetails([FromBody] RequestEntities re)
        {
            UserDetails userDetails = new UserDetails();
            userDetails.name = re.UserDetails.GetProperty("name").GetString();
            userDetails.address = re.UserDetails.GetProperty("address").GetString();
            userDetails.createdBy = "niamuls";
            userDetails.createdDate = DateTime.Now;

            int id = _userDetailsService.CreateUser(userDetails);
            return id;
        }

        // DELETE: api/UserDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDetails>> DeleteUserDetails(int id)
        {
            //var userDetails = await _context.UserDetails.FindAsync(id);
            
            var userDetails = new UserDetails();
            if (userDetails == null)
            {
                return NotFound();
            }

            //_context.UserDetails.Remove(userDetails);
            //await _context.SaveChangesAsync();

            return userDetails;
        }

        private bool UserDetailsExists(int id)
        {
           //return _context.UserDetails.Any(e => e.id == id);
            return false;
        }
    }
}
