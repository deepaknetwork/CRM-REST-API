using AnOrg.Models;
using AnOrg.Services;
using AnOrg.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace AnOrg.Controllers
{
    [Route("admin/")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly Servie _servie;
        private readonly OrgService _oservie;

        public AdminController(Servie servie, OrgService oservie)
        {
            _servie = servie;
            _oservie = oservie;
        }

        [HttpPost("signup")]
        public IActionResult usersignup(AdminDetails a)
        {
            int n = _servie.AddAdmin(a);
            if (n == 1) { return Ok(new { Message = "successfully signed up" }); }
            if (n == 2) { return Conflict(new { Message = "admin already exists" }); }
            return StatusCode(500, "Failed to add admin");
        }

        [HttpPost("login")]
        public ActionResult userlogin(AdminLoginView a)
        {
            int n = _servie.VerifyAdmin(a);
            if (n == 1) { return Ok(new { Message = "successfully logged in" }); }
            if (n == 2) { return StatusCode(401, "wrong password"); }
            if (n == 3) { return StatusCode(400, "admin not found"); }
            return StatusCode(500, "Failed to login admin");
        }


        [HttpPost("userdetail")]
        public IActionResult getuser(IdFromView i)
        {
            if (_servie.pre(i.Id))
            {
                return Ok(_servie.gasd(i.Id));
            }
            return StatusCode(400, "user not found");
        }

        
        [HttpPost("admindetail")]
        public IActionResult getadmin(IdFromView i)
        {
            if (_servie.IsAdminIdPresent(i.Id))
            {
                return Ok(_servie.GetAdminDetails(i.Id));
            }
            return StatusCode(400, "admin not found");
        }

        [HttpGet("allcustomers")]
        public IActionResult getAllCustomers()
        {
            return Ok(_servie.GetAllCustomers());
        }


        [HttpGet("alladmins")]
        public IActionResult getAllAdmins()
        {
            return Ok(_servie.GetAllAdmins());
        }

        [HttpPost("makeclient")]
        public IActionResult makeclient(IdFromView i)
        {
            if (_servie.UpdateCustomerAsClient(i.Id))
            {
                return Ok(new { Message = "client add" });
            }
            return StatusCode(500, "error");
        }

        [HttpGet("projectdeals")]
        public ActionResult<List<ProjectDeail>> getuProjectDeals()
        {
            return Ok(_servie.getProjectDeals());
        }

        [HttpPost("projectdealstatus")]
        public ActionResult updateprojectsdealtatus(ProjectDealStatusView p)
        {
            _servie.UpdateProjectDealState(p);
            return Ok();
        }


        [HttpPost("project")]
        public ActionResult AddProject(ProjectDetails projectDetails)
        {
            var result = _servie.AddProject(projectDetails);
            if (result)
            {      return Ok(new { Message = "Project added successfully" }); }
            return StatusCode(500, new { Message = "An error occurred while adding the project" });
            
        }

        [HttpPost("acceptproject")]
        public ActionResult acceptProject(IdLongView i)
        {
            if (_servie.acceptProject(i)) { return Ok(new { Message = "Project Added Successfully" }); }
            return StatusCode(500, new { Message = "An error occurred while adding the project" });
        }

        [HttpGet("projects")]
        public ActionResult<List<ProjectDetails>> getuProjects()
        {
            return Ok(_servie.getProjects());
        }


        [HttpPut("projects")]        
        public ActionResult ubdateProjects(ProjectDetails p)
        {
            if (_servie.updateProject(p)) { return Ok(new { Message = "Successfully Updated" }); }
            return StatusCode(500, new { Message = "An error occurred while updating the project" });
        }


        [HttpPost("callstatus")]
        public ActionResult updatecallstatus(ProjectDealStatusView p)
        {
            _servie.UpdateCallState(p);
            return Ok();
        }

        [HttpPost("dealcall")]
        public ActionResult addprocall(ProjectDealStatusCallView p)
        {
            _servie.UpdateProjectDealCallState(p);
            return Ok();
        }

        [HttpPost("projectcall")]
        public ActionResult addprojectcall(ProjectDealStatusCallView p)
        {
            _servie.UpdateProjectCallState(p);
            return Ok();
        }

        [HttpGet("calls")]
        public IActionResult allcalls()
        {
            return Ok(_servie.allcalls());
        }

        [HttpPost("addmeet")]
        public ActionResult addmeet(MeetView m)
        {
            _servie.addmeet(m);
            return Ok();
        }

        [HttpGet("meets")]
        public IActionResult allmeets()
        {
            return Ok(_servie.allmeets());
        }

        [HttpPost("meetstatus")]
        public ActionResult updatemeetstatus(ProjectDealStatusView p)
        {
            _servie.UpdateMeetState(p);
            return Ok();
        }

        [HttpGet("view")]
        public IActionResult getanalatics()
        {
            return Ok(_oservie.getanalatics());
        }





    }
}
