using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Mvc.Facebook;
using Microsoft.AspNet.Mvc.Facebook.Client;
using FBHelpMe.Models;

namespace FBHelpMe.Controllers
{
    public class HomeController : Controller
    {
        [FacebookAuthorize("email", "user_photos")]
        public async Task<ActionResult> Index(FacebookContext context)
        {
            if (ModelState.IsValid)
            {
                var user = await context.Client.GetCurrentUserAsync<MyAppUser>();

                MyAppUserExt _MyAppUserExt = new MyAppUserExt();

                _MyAppUserExt.Id = user.Id;
                _MyAppUserExt.Name = user.Name;
                _MyAppUserExt.Email = user.Email;

                _MyAppUserExt.Friends = user.Friends;
                _MyAppUserExt.Photos = user.Photos;
                _MyAppUserExt.ProfilePicture = user.ProfilePicture;

                _MyAppUserExt.Professions = new List<MyFriendsProfessions> { 
                    new MyFriendsProfessions { Professions = "Engineer", TotalFriends = 50, Percentage = 25.00 },
                    new MyFriendsProfessions { Professions = "Doctor", TotalFriends = 50, Percentage = 20.00 },
                    new MyFriendsProfessions { Professions = "Teacher", TotalFriends = 50, Percentage = 30.00 },
                    new MyFriendsProfessions { Professions = "Jurnalist", TotalFriends = 50, Percentage = 15.00 },
                    new MyFriendsProfessions { Professions = "Writer", TotalFriends = 50, Percentage = 5.00 }
                };

                //var user1 = context.Client.Get(user.Friends.Data[10].Link);

                _MyAppUserExt.ChartData = "[";
                for (int i = 0; i < _MyAppUserExt.Professions.Count; i++)
                {
                    if (i > 0) _MyAppUserExt.ChartData += ",";
                    _MyAppUserExt.ChartData += "['" + _MyAppUserExt.Professions[i].Professions + "'," + _MyAppUserExt.Professions[i].Percentage + "]";
                }
                _MyAppUserExt.ChartData += "]";

                return View(_MyAppUserExt);
                
            }

            return View("Error");
        }

        // This action will handle the redirects from FacebookAuthorizeFilter when
        // the app doesn't have all the required permissions specified in the FacebookAuthorizeAttribute.
        // The path to this action is defined under appSettings (in Web.config) with the key 'Facebook:AuthorizationRedirectPath'.
        public ActionResult Permissions(FacebookRedirectContext context)
        {
            if (ModelState.IsValid)
            {
                return View(context);
            }

            return View("Error");
        }

    }
}
