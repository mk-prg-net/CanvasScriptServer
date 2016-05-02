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
        ICanvasScriptServerUnitOfWork unitOfWork;

        public UserController(ICanvasScriptServerUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;            
        }


        IEnumerable<IUser> GetUsers(bool OrderUpward)
        {
            //mko.BI.Repositories.DefSortOrder<IUser> order = new CanvasScriptServer.UserRepository.SortName(!OrderUpward);
            //unitOfWork.Users.DefSortOrders(order);
            //return unitOfWork.Users.GetFilteredAndSortedListOfBo();

            var UsersOrderedBld = unitOfWork.Users.getFilteredSortedSetBuilder();

            UsersOrderedBld.sortByUserName(OrderUpward);

            return UsersOrderedBld.GetSet().Get();
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
                unitOfWork.Users.CreateBoAndAdd(NewUser.Name);
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