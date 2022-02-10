using Microsoft.AspNetCore.Mvc;


namespace RestAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string get(){
            return "http GET get get";
        }
        
        [HttpPost]
        public string post(string postData){
            return postData;
        }


    }
}