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
using Microsoft.Extensions.Primitives;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

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
            if(!isValidRequest()) {
                throw new InvalidRequestException("Invalid Request", 999);
            }

            ResultModels<UserDetails> userDetails = await userDetailsSer.GetAllUsers();
            List<UserDetailsObject> records = null;

            if(userDetails != null) {
                records = Mapper.Map<List<UserDetails>, List<UserDetailsObject>>(userDetails.Records);
            }
            return Ok(new ReturnedObject<UserDetailsObject>(){
                Data = records
            });
            
        }


/*        [HttpPost]
        [Route("~/api/uploadImage")]
        [DisableRequestSizeLimit]
        [RequestFormLimits(MultipartBodyLengthLimit = Int64.MaxValue)]

        public async Task<IActionResult> UploadTest()
        {
	        var files = Request.Form.Files;
	        if (files != null && files.Count == 0)
	        {
		        throw new InvalidRequestException("Invalid Request", 999);
	        }

	        string param1 = Request.Form["Param1"].ToString();
	        string param2 = Request.Form["Param2"].ToString();

            Console.WriteLine("Param 1: "+ param1);
            Console.WriteLine("Param 2: "+ param2);

	        IFormFile file = files[0];
	        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
	        string fileUploadPath = "/path/to/upload"; // usually configured base path to upload files

            var result = await userDetailsSer.UploadTest(file, fileUploadPath);
            List<String> imagePath = new List<String>();
            imagePath.Add(result);

            return Ok(new ReturnedObject<String>(){
	            Data = imagePath
	        });
        }

*/

        // GET: api/UserDetails/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetUserDetailById(int id)
        {
            if(!isValidRequest()) {
                throw new InvalidRequestException("Invalid Request", 999);
            }
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
//        [HttpPost]
/*        public async Task<ActionResult> CreateUserDetails([FromBody] RequestEntities re)
        {
            if(!isValidRequest()) {
                throw new InvalidRequestException("Invalid Request", 999);
            }
            UserDetails userDetails = new UserDetails();
            userDetails.firstname = re.UserDetails.GetProperty("firstname").GetString();
            userDetails.lastname = re.UserDetails.GetProperty("lastname").GetString();
            userDetails.address = re.UserDetails.GetProperty("address").GetString();
            userDetails.city = re.UserDetails.GetProperty("city").GetString();
            userDetails.country = re.UserDetails.GetProperty("country").GetString();
            userDetails.pincode = Utilities.ConversionUtils.ToInt(re.UserDetails.GetProperty("pincode").GetString());
            userDetails.profilepic = re.UserDetails.GetProperty("profilepic").GetString();
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
*/
                // POST: api/UserDetails
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Route("~/api/uploadImage")]
        [DisableRequestSizeLimit]
        [RequestFormLimits(MultipartBodyLengthLimit = Int64.MaxValue)]
        public async Task<ActionResult> EnterUserDetails()
        {
            if(!isValidRequest()) {
                throw new InvalidRequestException("Invalid Request", 999);
            }

            UserDetails userDetails = new UserDetails();
            userDetails.firstname = Request.Form["firstname"].ToString();
            userDetails.lastname = Request.Form["lastname"].ToString();
            userDetails.address = Request.Form["address"].ToString();
            userDetails.city = Request.Form["city"].ToString();
            userDetails.country = Request.Form["country"].ToString();
            userDetails.pincode = Utilities.ConversionUtils.ToInt(Request.Form["pincode"].ToString());
            userDetails.createdby = "niamuls";
            userDetails.creationdate = DateTime.Now;


            var files = Request.Form.Files;
	        if (files != null && files.Count == 0)
	        {
		        throw new InvalidRequestException("Invalid Request", 999);
	        } else {
                IFormFile file = files[0];
	            string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
	            string fileUploadPath = "/path/to/upload"; // usually configured base path to upload files
                var filepath = await userDetailsSer.UploadTest(file, fileUploadPath);
                userDetails.profilepic = filepath;
            }

            var userDt = await userDetailsSer.CreateUser(userDetails);
            UserDetailsObject udo = Mapper.Map<UserDetails, UserDetailsObject>(userDt);
            List<UserDetailsObject> detailsObjectList = new List<UserDetailsObject>();
            detailsObjectList.Add(udo);
            return Ok(new ReturnedObject<UserDetailsObject>(){
                Data = detailsObjectList
            });
        }
        
        /**
         *
         */
        public Boolean isValidRequest() {
            var headerValues = Request.Headers["Authorization"];
            return headerValues.Equals("ums-token");
        }
    }
}
