using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using first_rest_api.Models;
using first_rest_api.Services;
using first_rest_api.RequestObjects;
using first_rest_api.ResponseObjects;
using first_rest_api.CustomExceptions;
using AutoMapper;
using Microsoft.AspNetCore.Cors;

namespace first_rest_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        //private readonly UserDetailsContext _context;
        private UserDetailsServices userDetailsSer;

        public IMapper Mapper { get; }

        public UserDetailsController(IMapper mapper)
        {
            Mapper = mapper;
            userDetailsSer = new UserDetailsServices();
        }

        // GET: api/UserDetails
        [EnableCors("CorsPolicy")]
        [HttpGet]
        [Route("~/api/UserDetails")]
        public async Task<ActionResult> GetUserDetails()
        {
            ResultModels<UserDetails> userDetails = await userDetailsSer.GetAllUsers();
            List<UserDetailsObject> records = null;

            if(userDetails != null) {
                records = Mapper.Map<List<UserDetails>, List<UserDetailsObject>>(userDetails.Records);
            }
            return Ok(new ReturnedObject<UserDetailsObject>(){
                Data = records
            });
            
        }

        // GET: api/UserDetails/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetUserDetailById(int id)
        {
            UserDetails userDetails = null;
            try {
                userDetails = await userDetailsSer.GetUserDetailsById(id);
            } catch (UserDetailsException ude) {
                return NotFound(new ErrorObject () {message = ude.errorMessage} );
            }

            UserDetailsObject detailsObject = null;
            List<UserDetailsObject> detailsObjectList = new List<UserDetailsObject>();

            if(userDetails != null) {
                detailsObject = Mapper.Map<UserDetails, UserDetailsObject>(userDetails);
                detailsObjectList.Add(detailsObject);
            } 
            
            return Ok(new ReturnedObject<UserDetailsObject>(){
               Data = detailsObjectList
            });

        }

        // POST: api/UserDetails
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult> CreateUserDetails([FromBody] RequestEntities re)
        {
            UserDetails userDetails = new UserDetails();
            userDetails.firstname = re.UserDetails.GetProperty("firstname").GetString();
            userDetails.lastname = re.UserDetails.GetProperty("lastname").GetString();
            userDetails.address = re.UserDetails.GetProperty("address").GetString();
            userDetails.city = re.UserDetails.GetProperty("city").GetString();
            userDetails.country = re.UserDetails.GetProperty("country").GetString();
            userDetails.pincode = Utilities.ConversionUtils.ToInt(re.UserDetails.GetProperty("pincode").GetString());
            userDetails.createdby = "niamuls";
            userDetails.creationdate = DateTime.Now;

            var userDt = await userDetailsSer.CreateUser(userDetails);
            UserDetailsObject udo = Mapper.Map<UserDetails, UserDetailsObject>(userDt);
            List<UserDetailsObject> detailsObjectList = new List<UserDetailsObject>();
            detailsObjectList.Add(udo);
            return Ok(new ReturnedObject<UserDetailsObject>(){
                Data = detailsObjectList
            });
        }
    }
}
