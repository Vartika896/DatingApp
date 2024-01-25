

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Data;
using TestAPI.Entities;

namespace TestAPI.Controllers
{
    public class BuggyController: BasApiController
    {
        private readonly DataContext _dataContext;
        public BuggyController(DataContext dataContext)
        {
            this._dataContext = dataContext;
            
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "Secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var error = _dataContext.Users.Find(-1);
            if(error== null)
                return NotFound();
            return error;
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var error = _dataContext.Users.Find(-1);
            var res= error.ToString();
            return res;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This is a bad request");
        }
    }
}