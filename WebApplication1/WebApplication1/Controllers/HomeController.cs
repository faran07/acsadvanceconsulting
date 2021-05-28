using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Check if user is admin when index page is opened

          /*  Users model = new Users();
            string email = User.Identity.GetUserName();
            var user = Models.MongoDBHelper.MongoDBUserType(email);
            model.userType = user;*/

            // Get campaign and display

            return View();
        }
    }
}
