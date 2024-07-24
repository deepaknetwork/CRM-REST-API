using AnOrg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AnOrg.ViewModels;
using AnOrg.Services;

namespace AnOrg.Controllers
{
    [Route("customer/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Servie _servie;

        public UserController(Servie servie)
        {
            _servie = servie;
        }

        [HttpPost("signup")]
        public IActionResult usersignup(CustomerSigninView customerSigninView)
        {
            CustomerDetails c = new CustomerDetails
            {
                Id = customerSigninView.Id,
                Name = customerSigninView.Name,
                Password = customerSigninView.Password,
                Phone = customerSigninView.Phone,
                IsClient = false
            };
            int n=_servie.AddCustomer(c);
            if (n == 1) { return Ok(new { Message="successfully signed up"}); }
            if (n == 2) { return Conflict(new { Message = "User already exists" }); }
            return StatusCode(500, "Failed to add customer"); 


        }

        [HttpPost("login")]
        public IActionResult userlogin(CustomerLoginView customerLoginView)
        {
            int n = _servie.VerifyCustomer(customerLoginView);
            if (n == 1) { return Ok(new { Message = "successfully logged in" }); }
            if (n == 2) { return StatusCode(401, "wrong password"); }
            if (n == 3) { return StatusCode(400, "user not found"); }
            return StatusCode(500, "Failed to login user");
           
        }

        [HttpPost("userdetail")]
        public IActionResult getuser(IdFromView i)
        {
            if (_servie.IsCustomerIdPresent(i.Id))
            {
                return Ok(_servie.GetCustomerDetails(i.Id));
            }
            return StatusCode(400, "user not found");
        }

        [HttpPost("projectdeal")]
        public IActionResult addprojectdeal(ProjectDealView p)
        {
            long i = _servie.AddProjectDeals(p);
            if (i!=0)
            {
                return Ok(new { Message ="project deal added"});
            }
            return StatusCode(500, "error adding prpject deals");

        }

        [HttpPost("projectdeals")]
        public ActionResult<List<ProjectDeail>> getuserProjectDeals(IdFromView i)
        {
            return Ok(_servie.getProjectDealCustomer(i.Id));
        }

        [HttpPost("projects")]
        public ActionResult<List<ProjectDetails>> getuserProjects(IdFromView i)
        {
            return Ok(_servie.getProjectCustomer(i.Id));
        }

        [HttpPost("calls")]
        public IActionResult allcalls(IdFromView i)
        {
            return Ok(_servie.allcustomercalls(i.Id));
        }

        [HttpPost("meets")]
        public ActionResult<List<ProjectDetails>> getallmeets(IdFromView i)
        {
            return Ok(_servie.allcustomermeets(i.Id));
        }







    }


}
