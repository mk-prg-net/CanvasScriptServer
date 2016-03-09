using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BoCo = mko.BI.Repositories.Interfaces;

namespace CanvasScriptServer.MVC.Controllers
{
    public class CanvasScriptsController : Controller
    {
        ICanvasScriptServerUnitOfWork unitOfWork;

        public CanvasScriptsController(ICanvasScriptServerUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: MSRMgmt
        public ActionResult Index(string username)
        {
            var Model = CreateIndexViewModel(username);
            return View(Model);
        }

        private Models.CanvasScriptsMgmt.IndexViewModel CreateIndexViewModel(string username)
        {
            var user = unitOfWork.Users.GetBo(username);
            var Model = new Models.CanvasScriptsMgmt.IndexViewModel(user, user.Scripts);
            return Model;
        }

        public ActionResult NewScript(string username)
        {
            var un = unitOfWork.Users.GetBo(username);
            return View(un);
        }


        public ActionResult Create(string username, string scriptname)
        {
            var newList = unitOfWork.CreateScript(username, scriptname);
            unitOfWork.SaveChanges();

            var Model = CreateIndexViewModel(username);
            return RedirectToAction("Index", new { username = username }); //View("Index", Model);

        }

        public ActionResult Delete(string username, string scriptname)
        {
            unitOfWork.deleteScript(scriptname);
            unitOfWork.SaveChanges();

            var Model = CreateIndexViewModel(username);
            return View("Index", Model);
        }

        public ActionResult Edit(string id)
        {
            var script = unitOfWork.Scripts.GetBo(id);

            return View(script);
        }
    }
}