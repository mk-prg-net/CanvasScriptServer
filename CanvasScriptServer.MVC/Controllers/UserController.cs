using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using UserCo = mko.BI.Repositories.Interfaces;

namespace CanvasScriptServer.MVC.Controllers
{
    public class UserController : Controller
    {
        UserCo.ICrud<IUser, string> UserCoCud;
        UserCo.IFilterAndSort<IUser> UserCoFilter;
        UserCo.IGetBo<IUser, string> UserCoGet;


        public UserController(UserCo.ICrud<IUser, string> UserCoCud, UserCo.IFilterAndSort<IUser> UserCoFilter, UserCo.IGetBo<IUser, string> UserCoGet)
        {
            this.UserCoCud = UserCoCud;
            this.UserCoFilter = UserCoFilter;
            this.UserCoGet = UserCoGet;
        }


        IQueryable<IUser> GetUsers(bool OrderUpward)
        {
            if (OrderUpward)
                return UserCoFilter.GetFilteredListOfBo().OrderBy(r => r.Name);
            else
                return UserCoFilter.GetFilteredListOfBo().OrderByDescending(r => r.Name);
        }

        // GET: User
        public ActionResult Index(bool OrderUpward)
        {
            return View(GetUsers(OrderUpward));//ListCalc.List.NullObject.User.Username.Name));
        }

        public ActionResult NewUser()
        {
            return View(new Models.UserMgmt.UserViewModel());
        }

        [HandleError(ExceptionType = typeof(Exception), View = "SaveErrorView")]
        public ActionResult SaveUser(Models.UserMgmt.UserViewModel NewUser)
        {
            //NewUser.Name = NewUser.Name.Trim();
            //ValidateModel(NewUser);
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Name", "Der Name wurde nicht festgelegt");
                return View("NewUser");
            }
            else
            {
                // speichern
                var user = UserCoCud.CreateBo();
                user.Name = NewUser.Name;
                UserCoCud.AddToCollection(user);
                UserCoCud.SubmitChanges();

                return View("Index", GetUsers(true));
            }

        }

        [HandleError(ExceptionType = typeof(Exception), View = "Error")]
        public ActionResult Delete(string username)
        {
            var bo =UserCoGet.GetBo(username);
            UserCoCud.RemoveFromCollection(bo);
            UserCoCud.SubmitChanges();

            return View("Index", GetUsers(true));
        }

    }
}