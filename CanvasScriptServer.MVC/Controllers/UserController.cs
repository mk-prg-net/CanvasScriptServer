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
        ICanvasScriptServerUnitOfWork<DB.Users, DB.Scripts> unitOfWork;

        public UserController(ICanvasScriptServerUnitOfWork<DB.Users, DB.Scripts> unitOfWork)
        {
            this.unitOfWork = unitOfWork;            
        }


        IEnumerable<IUser> GetUsers(bool OrderUpward)
        {
            //mko.BI.Repositories.DefSortOrder<IUser> order = new CanvasScriptServer.UserRepository.SortName(!OrderUpward);
            //unitOfWork.Users.DefSortOrders(order);
            //return unitOfWork.Users.GetFilteredAndSortedListOfBo();
            if (OrderUpward)
                return unitOfWork.Users.Get(orderBy: users => users.OrderBy(user => user.Name.Name));
            else
                return unitOfWork.Users.Get(orderBy: users => users.OrderByDescending(user => user.Name.Name));
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
                unitOfWork.createUser(NewUser.Name);
                unitOfWork.SubmitChanges();

                return View("Index", GetUsers(true));
            }

        }

        [HandleError(ExceptionType = typeof(Exception), View = "Error")]
        public ActionResult Delete(string username)
        {
            unitOfWork.deleteUser(username);
            unitOfWork.SubmitChanges();
            
            return View("Index", GetUsers(true));
        }

    }
}