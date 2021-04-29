using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EastViewTasksAssignment.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        [HttpGet]
        [Route("/")]
        public JsonResult Index()
        {
            return new JsonResult(new
            {
                type = "success",
                message = "Api loaded successfuly"
            });
        }
    }
}
